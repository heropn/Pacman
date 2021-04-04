using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI score;

	[SerializeField]
	private TextMeshProUGUI title;

	private void Start()
	{
		GameManager.Instance.OnGameWon += ShowScreen;
	}

	public void ShowScreen()
	{
		score.enabled = true;
		title.enabled = true;

		score.text = "SCORE: " + ScoreManager.score.ToString();
	}
}
