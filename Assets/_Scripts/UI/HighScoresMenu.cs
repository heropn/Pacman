using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresMenu : MonoBehaviour
{
	public event Action<GameObject> OnBackButtonClicked;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private GameObject highScoreHolder;

	[SerializeField]
	private GameObject highscorePrefab;

	private List<HighScoreEntity> highScoresStored = new List<HighScoreEntity>();

	private List<HighScoreTemplate> highScoresDisplayed = new List<HighScoreTemplate>();

	private float templateHeight = 30.0f;

	private void Start()
	{
		if (backButton != null)
			backButton.onClick.AddListener(BackToMainMenu);
	}

	private void CreateHighScoreDisplay(HighScoreEntity highScore)
	{
		if (highScoresDisplayed.Count < 5)
		{
			var rectTransform = Instantiate(highscorePrefab, highScoreHolder.transform).GetComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 1);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.pivot = new Vector2(0.5f, 1);
			rectTransform.anchoredPosition = new Vector2(0, -templateHeight * highScoresDisplayed.Count);
			rectTransform.sizeDelta = new Vector2(0, 0);

			var highScoreTemplate = rectTransform.GetComponent<HighScoreTemplate>();
			highScoreTemplate.SetValues(highScoresDisplayed.Count + 1, highScore.score, highScore.name);

			highScoresDisplayed.Add(highScoreTemplate);
		}
	}

	public void AddHighScore(int score, string name)
	{
		if (PlayerPrefs.HasKey("highScoresList"))
		{
			string jsonString = PlayerPrefs.GetString("highScoresList");
			highScoresStored = JsonUtility.FromJson<HighScores>(jsonString).list;
		}

		var hsEntity = new HighScoreEntity { score = score, name = name };
		highScoresStored.Add(hsEntity);
		SortHighScoresEntityList();

		HighScores highScores = new HighScores { list = highScoresStored };

		string json = JsonUtility.ToJson(highScores);
		PlayerPrefs.SetString("highScoresList", json);
	}

	private void BackToMainMenu()
	{
		for (int i = 0; i < highScoresDisplayed.Count; i++)
		{
			Destroy(highScoresDisplayed[i].gameObject);
		}

		highScoresDisplayed.Clear();

		OnBackButtonClicked?.Invoke(gameObject);
	}

	public void UpdateAndShowHighScores()
	{
		if (PlayerPrefs.HasKey("highScoresList"))
		{
			string jsonString = PlayerPrefs.GetString("highScoresList");
			highScoresStored = JsonUtility.FromJson<HighScores>(jsonString).list;
		}
		else
		{
			HighScores highScores = new HighScores { list = highScoresStored };
			string json = JsonUtility.ToJson(highScores);
			PlayerPrefs.SetString("highScoresList", json);
		}

		foreach (var highscore in highScoresStored)
		{
			CreateHighScoreDisplay(highscore);
		}
	}

	private void SortHighScoresEntityList()
	{
		for (int i = 0; i < highScoresStored.Count; i++)
		{
			for (int j = 0; j < highScoresStored.Count; j++)
			{
				if (highScoresStored[j].score < highScoresStored[i].score)
				{
					var temp = highScoresStored[i];
					highScoresStored[i] = highScoresStored[j];
					highScoresStored[j] = temp;
				}
			}
		}

		for (int i = 5; i < highScoresStored.Count; i++)
		{
			highScoresStored.RemoveAt(i);
		}
	}

	public class HighScores
	{
		public List<HighScoreEntity> list;
	}

	[Serializable]
	public class HighScoreEntity
	{
		public int score;
		public string name;
	}
}
