using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresMenu : MonoBehaviour
{
	public event Action<GameObject> onBackButtonClicked;

	[SerializeField]
	private Button backButton;

	private void Start()
	{
		backButton.onClick.AddListener(BackToMainMenu);
	}

	private void BackToMainMenu()
	{
		onBackButtonClicked?.Invoke(gameObject);
	}
}
