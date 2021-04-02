using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public event Action<GameObject> onBackButtonClicked;

	[SerializeField]
	private Slider slider;

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
