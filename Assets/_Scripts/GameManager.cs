using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public event Action onPointScored;

	private List<Gold> golds = new List<Gold>();

	[SerializeField]
	private GameObject goldHolder;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		golds = goldHolder.GetComponentsInChildren<Gold>().ToList();

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
