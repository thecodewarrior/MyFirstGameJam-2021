using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public string currentMusicPlaying;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			print("Sound: " + name + " not found!");
			return;
		}

		s.source.Play();
	}

    public void FadeAndPlayMusic(string musicName, float fadeTime, Action completion = null)
    {
        FadeOutSound(currentMusicPlaying, fadeTime, () =>
        {
            PlayMusic(musicName);
            completion?.Invoke();
        });
    }
    
	public void PlayMusic(string musicName)
	{
		Sound s = Array.Find(sounds, item => item.name == musicName);
		if (s == null)
		{
			print("Music: " + name + " not found!");
			return;
		}
		currentMusicPlaying = musicName;
		s.source.Play();
	}

	public void FadeOutSound(string name, float fadeTime, Action completion = null)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null)
		{
			StartCoroutine(FadeOut(s.source, fadeTime, completion));
		}
	}

	public void FadeOutAll(float fadeTime)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
			StartCoroutine(FadeOut(sounds[i].source, fadeTime));
        }
    }

	public IEnumerator FadeOut(AudioSource audioSource, float FadeTime, Action completion = null)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			float previousVolume = audioSource.volume;

			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			if (audioSource.volume != previousVolume)
			{
				yield return null;
			}
			else
			{
				audioSource.volume = 0;
			}
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
        completion?.Invoke();
    }

	public void StopFadeOut()
    {
		StopAllCoroutines();
		foreach (Sound s in sounds)
		{
			s.source.volume = s.volume;
		}
	}
}
