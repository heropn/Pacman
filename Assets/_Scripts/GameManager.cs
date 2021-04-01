using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public event Action<int> onPointScored;
	public event Action onGameLost;

	[SerializeField]
	private float enemiesVulnarableTime = 5.0f;

	[SerializeField]
	private int pointsFromGold = 10;

	[SerializeField]
	private int pointsFromEnemy = 100;

	[SerializeField]
	private int pointsFromPowerUP = 50;

	[Space(10)]
	[SerializeField]
	private Player player;

	[Space(10)]
	[SerializeField]
	private GameObject goldHolder;

	[SerializeField]
	private GameObject powerUpsHolder;

	[SerializeField]
	private GameObject enemyHolder;

	[SerializeField]
	private GameObject portalsHolder;

	private List<Gold> golds = new List<Gold>();

	private List<PowerUP> powerUps;

	private List<Enemy> enemies;

	private List<Portal> portals;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		golds = goldHolder.GetComponentsInChildren<Gold>().ToList();
		powerUps = powerUpsHolder.GetComponentsInChildren<PowerUP>().ToList();
		enemies = enemyHolder.GetComponentsInChildren<Enemy>().ToList();
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

		player.onPlayerCollidedWithEnemy += EnemyEnter;
	}

	private void EnemyEnter(Enemy enemy)
	{
		if (enemy.currentState == Enemy.State.Normal)
		{
			Debug.Log("GAME LOST");
			onGameLost?.Invoke();
		}
		else
		{
			enemies.Remove(enemy);
			onPointScored?.Invoke(pointsFromEnemy); //Score some more points;
			enemy.Destroy();
		}
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

		onPointScored?.Invoke(pointsFromPowerUP);

		StartCoroutine(MakeEnemiesVulnarable());
	}

	private IEnumerator MakeEnemiesVulnarable()
	{
		foreach (var enemy in enemies)
		{
			enemy.ChangeState(Enemy.State.Vulnarable);
		}

		yield return new WaitForSeconds(enemiesVulnarableTime);

		foreach (var enemy in enemies)
		{
			enemy.ChangeState(Enemy.State.Normal);
		}
	}

	private void ScorePoint(Gold gold)
	{
		gold.onGoldCollected -= ScorePoint;
		golds.Remove(gold);
		Destroy(gold.gameObject);
		onPointScored?.Invoke(pointsFromGold);
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

		player.onPlayerCollidedWithEnemy -= EnemyEnter;
	}
}
