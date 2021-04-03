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

	private void Start()
	{
		backButton.onClick.AddListener(BackToMainMenu);
	}

	private void BackToMainMenu()
	{
		OnBackButtonClicked?.Invoke(gameObject);
	}
}
