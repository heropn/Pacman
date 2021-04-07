using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public event Action<GameObject> OnBackButtonClicked;

	[SerializeField]
	private Slider slider;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private AudioMixer audioMixer;

	private float volumeValue;

	private void Start()
	{
		if (PlayerPrefs.HasKey("volume"))
		{
			volumeValue = PlayerPrefs.GetFloat("volume");
		}
		else
		{
			volumeValue = slider.maxValue;
			PlayerPrefs.SetFloat("volume", volumeValue);
		}

		audioMixer.SetFloat("Volume", Mathf.Log10(volumeValue) * 20);
		slider.value = volumeValue;
		slider.onValueChanged.AddListener(SetVolume);
		backButton.onClick.AddListener(BackToMainMenu);
	}

	private void SetVolume(float sliderValue)
	{
		volumeValue = sliderValue;

		audioMixer.SetFloat("Volume", Mathf.Log10(volumeValue) * 20);
	}

	private void BackToMainMenu()
	{
		PlayerPrefs.SetFloat("volume", volumeValue);
		OnBackButtonClicked?.Invoke(gameObject);
	}
}
