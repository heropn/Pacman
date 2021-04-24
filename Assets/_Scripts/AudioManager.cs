using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	[SerializeField]
	private List<Audio> audios = new List<Audio>();

	[SerializeField]
	private AudioMixerGroup audioMixer;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		foreach (var a in audios)
		{
			a.source = gameObject.AddComponent<AudioSource>();
			a.source.clip = a.clip;
			a.source.loop = a.isLoop;
			a.source.outputAudioMixerGroup = audioMixer;
		}

		var audio = audios.Find(a => a.name == Audio.Name.Music);
		audio.source.Play();
		audio.source.volume *= 0.5f;
	}

	public void PlayClip(Audio.Name name)
	{
		var audio = audios.Find(a => a.name == name);
		audio.source.Play();
	}

	[System.Serializable]
	public class Audio
	{
		public enum Name
		{
			Music,
			DeathSound,
			WinSound,
			Coin,
			PowerUp,
			Portal
		}

		public Name name;

		public AudioClip clip;

		public bool isLoop;

		[HideInInspector]
		public AudioSource source;
	}
}
