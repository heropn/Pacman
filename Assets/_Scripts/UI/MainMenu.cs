using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public event Action OnOptionsButtonClicked;
	public event Action OnHighScoresButtonClicked;

	[SerializeField]
	private Button playButton;

	[SerializeField]
	private Button optionsButton;

	[SerializeField]
	private Button highscoreButton;

	[SerializeField]
	private Button exitButton;

	[SerializeField]
	private TMP_InputField inputField;

	private void Start()
	{
		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(QuitGame);
		optionsButton.onClick.AddListener(ShowOptionsMenu);
		highscoreButton.onClick.AddListener(ShowHighScoreMenu);
		inputField.onValueChanged.AddListener(UpdatePlayerName);

		if (PlayerPrefs.HasKey("name"))
		{
			string name = PlayerPrefs.GetString("name");
			inputField.text = name;
		}
		else
		{
			PlayerPrefs.SetString("name", "-");
		}
	}

	private void UpdatePlayerName(string name)
	{
		PlayerPrefs.SetString("name", name);
	}

	private void ShowOptionsMenu()
	{
		OnOptionsButtonClicked?.Invoke();
	}

	private void ShowHighScoreMenu()
	{
		OnHighScoresButtonClicked?.Invoke();
	}

	private void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void QuitGame()
	{
		Debug.Log("QUITED");
		Application.Quit();
	}
}
