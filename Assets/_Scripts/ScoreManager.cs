using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private GameManager gameManager;

	private TextMeshProUGUI textMeshPro;

	private const string scoreString = "Score: ";

	private int score;

	private void Start()
	{
		gameManager = GameManager.Instance;
		gameManager.OnPointScored += ScorePoints;
		textMeshPro = GetComponent<TextMeshProUGUI>();
		score = 0;

		textMeshPro.text = scoreString + score.ToString();
	}

	private void ScorePoints(int points)
	{
		score += points;
		textMeshPro.text = scoreString + (score).ToString();
	}

	private void OnDestroy()
	{
		gameManager.OnPointScored -= ScorePoints;
	}
}
