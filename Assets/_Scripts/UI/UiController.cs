using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
	[SerializeField]
	private MainMenu mainMenu;

	[SerializeField]
	private OptionsMenu optionsMenu;

	[SerializeField]
	private HighScoresMenu highScoresMenu;

	private void Start()
	{
		mainMenu.OnHighScoresButtonClicked += ShowHighScoreMenu;
		mainMenu.OnOptionsButtonClicked += ShowOptionsMenu;
		optionsMenu.OnBackButtonClicked += ShowMainMenu;
		highScoresMenu.OnBackButtonClicked += ShowMainMenu;

		optionsMenu.gameObject.SetActive(false);
		highScoresMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}

	private void ShowMainMenu(GameObject obj)
	{
		obj.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}

	private void ShowOptionsMenu()
	{
		mainMenu.gameObject.SetActive(false);
		optionsMenu.gameObject.SetActive(true);
	}

	private void ShowHighScoreMenu()
	{
		mainMenu.gameObject.SetActive(false);
		highScoresMenu.gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		mainMenu.OnHighScoresButtonClicked -= ShowHighScoreMenu;
		mainMenu.OnOptionsButtonClicked -= ShowOptionsMenu;
		optionsMenu.OnBackButtonClicked -= ShowMainMenu;
		highScoresMenu.OnBackButtonClicked -= ShowMainMenu;
	}
}
