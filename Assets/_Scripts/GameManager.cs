using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public event Action<int> OnPointScored;

	public event Action OnGameWon;
	public event Action OnGameLost;

	[SerializeField]
	private float enemiesVulnarableTime = 5.0f;

	[SerializeField]
	private float timeBetweenSpawningEnemies = 5.0f;

	[SerializeField]
	private int howManyEnemies = 3;

	[SerializeField]
	private int pointsFromGold = 10;

	[SerializeField]
	private int pointsFromEnemy = 100;

	[SerializeField]
	private int pointsFromPowerUP = 50;

	[Space(10)]
	[SerializeField]
	private Player player;

	[SerializeField]
	private FinalObjective finalObjective;

	[Space(10)]
	[SerializeField]
	private GameObject goldHolder;

	[SerializeField]
	private GameObject powerUpsHolder;

	[SerializeField]
	private GameObject portalsHolder;

	[Space(10)]
	[SerializeField]
	private Transform enemyHolderTransform;

	[SerializeField]
	private List<GameObject> enemiesPrefabs = new List<GameObject>();

	private List<Gold> golds = new List<Gold>();

	private List<PowerUP> powerUps;

	private List<Enemy> currentEnemies = new List<Enemy>();

	private List<Portal> portals;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		player.enabled = false;
		player.GetComponent<Animator>().enabled = false;
		golds = goldHolder.GetComponentsInChildren<Gold>().ToList();
		powerUps = powerUpsHolder.GetComponentsInChildren<PowerUP>().ToList();
		portals = portalsHolder.GetComponentsInChildren<Portal>().ToList();

		foreach (var gold in golds)
		{
			gold.onGoldCollected += ScorePoint;
		}

		foreach (var powerUp in powerUps)
		{
			powerUp.onPowerUpPicked += TakePowerUP;
		}

		foreach (var portal in portals)
		{
			portal.onPortalEnter += TeleportPlayer;
		}

		player.OnPlayerCollidedWithEnemy += EnemyEnter;
		finalObjective.OnPlayerEnter += WinGame;

		StartCoroutine(StartGame());
	}

	private IEnumerator StartGame()
	{
		yield return new WaitForSeconds(1.0f);
		player.GetComponent<Animator>().enabled = true;
		player.enabled = true;
		StartCoroutine(SpawnEnemies());
	}

	private void SpawnEnemy()
	{
		int index = UnityEngine.Random.Range(0, enemiesPrefabs.Count);
		var enemy = Instantiate(enemiesPrefabs[index], enemyHolderTransform);
		currentEnemies.Add(enemy.GetComponent<Enemy>());
	}

	private IEnumerator SpawnEnemies()
	{
		for (int i = 0; i < howManyEnemies; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(timeBetweenSpawningEnemies);
		}
	}

	private void EnemyEnter(Enemy enemy)
	{
		if (enemy.currentState == Enemy.State.Normal)
		{
			StopLevel();
			OnGameLost?.Invoke();
		}
		else
		{
			currentEnemies.Remove(enemy);
			OnPointScored?.Invoke(pointsFromEnemy);
			enemy.Destroy();
			SpawnEnemy();
		}
	}

	private void StopLevel()
	{
		player.enabled = false;
		finalObjective.enabled = false;

		foreach (var enemy in currentEnemies)
		{
			enemy.enabled = false;
		}

		foreach (var portal in portals)
		{
			var colorP = portal.GetComponent<SpriteRenderer>().color;
			colorP.a = 0.3f;
			portal.GetComponent<SpriteRenderer>().color = colorP;
		}

		foreach (var enemy in currentEnemies)
		{
			var colorP = enemy.GetComponent<SpriteRenderer>().color;
			colorP.a = 0.3f;
			enemy.GetComponent<SpriteRenderer>().color = colorP;
		}

		foreach (var powerup in powerUps)
		{
			var colorP = powerup.GetComponent<SpriteRenderer>().color;
			colorP.a = 0.3f;
			powerup.GetComponent<SpriteRenderer>().color = colorP;
		}

		var spriteRenderer = Level.Instance.GetComponentInChildren<SpriteRenderer>();
		var color = spriteRenderer.color;
		color.a = 0.3f;
		spriteRenderer.color = color;
	}

	private void WinGame()
	{
		StopLevel();
		OnGameWon?.Invoke();
	}

	private void TeleportPlayer(Portal destinationPortal)
	{
		StartCoroutine(destinationPortal.DisableCollider(1.0f));
		player.TeleportToLocation(destinationPortal.transform.position);
	}

	private void TakePowerUP(PowerUP powerUp)
	{
		powerUp.onPowerUpPicked -= TakePowerUP;
		powerUps.Remove(powerUp);
		Destroy(powerUp.gameObject);

		OnPointScored?.Invoke(pointsFromPowerUP);

		StartCoroutine(MakeEnemiesVulnarable());
	}

	private IEnumerator MakeEnemiesVulnarable()
	{
		foreach (var enemy in currentEnemies)
		{
			enemy.ChangeState(Enemy.State.Vulnerable);
		}

		yield return new WaitForSeconds(enemiesVulnarableTime);

		foreach (var enemy in currentEnemies)
		{
			enemy.ChangeState(Enemy.State.Normal);
		}
	}

	private void ScorePoint(Gold gold)
	{
		gold.onGoldCollected -= ScorePoint;
		golds.Remove(gold);
		Destroy(gold.gameObject);
		OnPointScored?.Invoke(pointsFromGold);

		if (golds.Count == 0)
		{
			finalObjective.gameObject.SetActive(true);
			Level.Instance.SetActiveFinalGate(false);
		}
	}

	private void OnDestroy()
	{
		foreach (var portal in portals)
		{
			portal.onPortalEnter -= TeleportPlayer;
		}

		foreach (var gold in golds)
		{
			gold.onGoldCollected -= ScorePoint;
		}

		foreach (var powerUp in powerUps)
		{
			powerUp.onPowerUpPicked -= TakePowerUP;
		}

		player.OnPlayerCollidedWithEnemy -= EnemyEnter;
		finalObjective.OnPlayerEnter -= WinGame;
	}
}
