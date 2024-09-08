using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _Scripts
{
	public class AudioService : MonoBehaviour
	{
		private const string MusicMixer = "MusicVolume";
		private const string SfxMixer = "SfxVolume";
		private const float SoundOnValue = 1;
		private const float SoundOffValue = 0.0001f;

		[Header("AudioSources")]
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private AudioSource _sfxSource;
		[Header("MasterMixer")]
		[SerializeField] private AudioMixer _mixer;
		[Header("VolumeSliders")]
		[SerializeField] private Slider _musicSlider;
		[SerializeField] private Slider _sfxSlider;

		private void Awake()
		{
			LinkVolume();
		}
	
		public void CheckSound()
		{
			ChangeSfxVolume(PlayerPrefs.GetInt("EffectsEnabled", 1) > 0
				? SoundOnValue
				: SoundOffValue);

			ChangeMusicVolume(PlayerPrefs.GetInt("MusicEnabled", 1) > 0
				? SoundOnValue
				: SoundOffValue);

			_sfxSource.loop = false;
		}

		private void LinkVolume()
		{
			if (_musicSlider != null)
				_musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

			if (_sfxSlider != null)
				_sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
		}

		public void PlaySound(AudioClip clip) =>
			_sfxSource.PlayOneShot(clip);

		public void StopSound(AudioSource source = null)
		{
			if(source == null)
				source = _sfxSource;

			_sfxSource.Stop();
			_sfxSource.clip = null;
			_sfxSource.loop = false;
		}

		public void StopAllSounds() =>
			_sfxSource.Stop();

		public void StopMusic()
			=> _musicSource.Stop();

		public void PauseMusic()
			=> _musicSource.Pause();

		public void UnpauseMusic()
			=> _musicSource.Play();

		private void ChangeMusicVolume(float value)
		{
			var dbValue = Mathf.Log10(value) * 20;
			_mixer.SetFloat(MusicMixer, dbValue);
		}

		private void ChangeSfxVolume(float value)
		{
			var dbValue = Mathf.Log10(value) * 20;
			_mixer.SetFloat(SfxMixer, dbValue);
		}
	}
}
