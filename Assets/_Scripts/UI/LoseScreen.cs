using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI score;

	[SerializeField]
	private TextMeshProUGUI title;

	[SerializeField]
	private Button backToMenuButton;

	private void Start()
	{
		GameManager.Instance.OnGameLost += ShowScreen;
		score.gameObject.SetActive(false);
		title.gameObject.SetActive(false);
		backToMenuButton.gameObject.SetActive(false);

		backToMenuButton.onClick.AddListener(BackToMenu);
	}

	private void BackToMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void ShowScreen()
	{
		backToMenuButton.gameObject.SetActive(true);
		score.gameObject.SetActive(true);
		title.gameObject.SetActive(true);

		score.text = "SCORE: " + ScoreManager.Instance.score.ToString();

		string name = PlayerPrefs.GetString("name");
		GetComponent<HighScoresMenu>().AddHighScore(ScoreManager.Instance.score, name);
	}

	private void OnDestroy()
	{
		GameManager.Instance.OnGameLost -= ShowScreen;
	}
}
