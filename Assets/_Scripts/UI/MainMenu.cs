using System;
using System.Collections;
using System.Collections.Generic;
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

	private void Start()
	{
		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(QuitGame);
		optionsButton.onClick.AddListener(ShowOptionsMenu);
		highscoreButton.onClick.AddListener(ShowHighScoreMenu);
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
