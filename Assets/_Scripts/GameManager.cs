using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public event Action onPointScored;

	[SerializeField]
	private List<Gold> golds = new List<Gold>();

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		foreach (var gold in golds)
		{
			gold.onGoldCollected += ScorePoint;
		}
	}

	private void ScorePoint(Gold gold)
	{
		gold.onGoldCollected -= ScorePoint;
		golds.Remove(gold);
		Destroy(gold.gameObject);
		onPointScored?.Invoke();
	}
}
