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
		gameManager.onPointScored += ScorePoint;
		textMeshPro = GetComponent<TextMeshProUGUI>();
		score = 0;

		textMeshPro.text = scoreString + score.ToString();
	}

	private void ScorePoint()
	{
		textMeshPro.text = scoreString + (++score).ToString();
	}
}
