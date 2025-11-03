using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
	public AudioSource thrustAudio;
	public AudioSource fireAudio;
	public AudioClip dieAudio;

	private float _thrustAudioMaxVolume;
	private IEnumerator _thrustFadeInCoroutine;
	private IEnumerator _thrustFadeOutCoroutine;

	private void Awake()
	{
		_thrustAudioMaxVolume = thrustAudio.volume;
	}

	public void Thrust(bool on)
	{
		if(on)
		{
			/* Fade in thrust audio */
			if(_thrustFadeOutCoroutine != null)
			{
				StopCoroutine(_thrustFadeOutCoroutine);
			}
			_thrustFadeInCoroutine = FadeInCo(this.thrustAudio, 0.08f, _thrustAudioMaxVolume);
			StartCoroutine(_thrustFadeInCoroutine);
		}
		else
		{
			/* Fade out thrust audio */
			if(_thrustFadeInCoroutine != null)
			{
				StopCoroutine(_thrustFadeInCoroutine);
			}
			_thrustFadeOutCoroutine = FadeOutCo(this.thrustAudio, 0.05f);
			StartCoroutine(_thrustFadeOutCoroutine);
		}
	}

	public void Fire()
	{
		fireAudio.Play();
	}

	public void Die()
	{
		AudioSource.PlayClipAtPoint(this.dieAudio, this.gameObject.transform.position, 1.0f);
	}

	private IEnumerator FadeOutCo(AudioSource audioSource, float fadeTime)
	{
		float sleepLength = 0.01f;
		float sleepNum = fadeTime / sleepLength;
		float increments = (audioSource.volume - 0) / sleepNum;

		while (sleepNum-- > 0)
		{
			audioSource.volume -= increments;
			yield return new WaitForSeconds(sleepLength);
		}

		audioSource.volume = 0;
		audioSource.Stop();
	}

	private IEnumerator FadeInCo(AudioSource audioSource, float fadeTime, float maxVolume)
	{
		float sleepLength = 0.01f;
		float sleepNum = fadeTime / sleepLength;
		float increments = (maxVolume - audioSource.volume) / sleepNum;

		if (audioSource.isPlaying == false)
		{
			audioSource.Play();
		}

		while (sleepNum-- > 0)
		{
			audioSource.volume += increments;
			yield return new WaitForSeconds(sleepLength);
		}

		audioSource.volume = maxVolume;
	}
}