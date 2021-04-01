using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public event Action onPointScored;

	[SerializeField]
	private GameObject goldHolder;

	[SerializeField]
	private GameObject powerUpsHolder;

	[SerializeField]
	private GameObject enemyHolder;

	private List<Gold> golds = new List<Gold>();

	private List<PowerUP> powerUps;

	private List<Enemy> enemies;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		golds = goldHolder.GetComponentsInChildren<Gold>().ToList();
		powerUps = powerUpsHolder.GetComponentsInChildren<PowerUP>().ToList();
		enemies = enemyHolder.GetComponentsInChildren<Enemy>().ToList();

		foreach (var gold in golds)
		{
			gold.onGoldCollected += ScorePoint;
		}

		foreach (var powerUp in powerUps)
		{
			powerUp.onPowerUpPicked += TakePowerUP;
		}
	}

	private void TakePowerUP(PowerUP powerUp)
	{
		powerUp.onPowerUpPicked -= TakePowerUP;
		powerUps.Remove(powerUp);
		Destroy(powerUp.gameObject);

		//Make enemies vulurable for some time
	}

	private void ScorePoint(Gold gold)
	{
		gold.onGoldCollected -= ScorePoint;
		golds.Remove(gold);
		Destroy(gold.gameObject);
		onPointScored?.Invoke();
	}
}
