using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000094 RID: 148
public class SoundManager : MonoBehaviour
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000136C7 File Offset: 0x000118C7
	public static SoundManager instance
	{
		get
		{
			return SoundManager._instance;
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000136CE File Offset: 0x000118CE
	private void Awake()
	{
		if (SoundManager._instance != null && SoundManager._instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SoundManager._instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00013707 File Offset: 0x00011907
	private void Start()
	{
		this.ChangeAllVolume();
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00013710 File Offset: 0x00011910
	private void Update()
	{
		for (int i = 0; i < this.playingClips.Count; i++)
		{
			SoundManager.PlayingClip playingClip = this.playingClips[i];
			playingClip.currentVolume = Mathf.MoveTowards(playingClip.currentVolume, playingClip.setVolume, Time.unscaledDeltaTime * 2f);
			playingClip.currentVolumeExtra = Mathf.MoveTowards(playingClip.currentVolumeExtra, playingClip.setVolumeExtra, Time.unscaledDeltaTime * 4f);
			if (playingClip.currentVolume == 0f && playingClip.removeOnFadeOut)
			{
				this.playingClips.Remove(playingClip);
				Object.Destroy(playingClip.audioSource.gameObject);
				i--;
			}
			else
			{
				this.ChangeVolume(playingClip);
			}
		}
		float num = this.currentMusicEffectWet;
		this.currentMusicEffectWet = Mathf.MoveTowards(this.currentMusicEffectWet, this.musicEffect ? 1f : 0f, Time.unscaledDeltaTime);
		if (num != this.currentMusicEffectWet)
		{
			this.musicMixer.SetFloat("Volume", this.currentMusicEffectWet * 3.5f);
			this.musicMixer.SetFloat("LPWet", SoundManager.ExpoEaseIn(this.currentMusicEffectWet) * 80f - 80f);
			this.musicMixer.SetFloat("HPWet", SoundManager.ExpoEaseIn(this.currentMusicEffectWet) * 80f - 80f);
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0001386E File Offset: 0x00011A6E
	public static float ExpoEaseIn(float k)
	{
		if (k != 1f)
		{
			return 1f - Mathf.Pow(2f, -10f * k);
		}
		return 1f;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00013898 File Offset: 0x00011A98
	public void ChangeAllVolume()
	{
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			this.ChangeVolume(playingClip);
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000138EC File Offset: 0x00011AEC
	private void ChangeVolume(SoundManager.PlayingClip playingClip)
	{
		if (playingClip.type == SoundManager.PlayingClip.Type.SFX)
		{
			playingClip.audioSource.volume = this.sfxVolume * playingClip.currentVolume;
			return;
		}
		playingClip.audioSource.volume = this.musicVolume * playingClip.currentVolume;
		if (playingClip.audioSourceExtra)
		{
			playingClip.audioSourceExtra.volume = this.musicVolume * playingClip.currentVolumeExtra * playingClip.currentVolume;
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00013960 File Offset: 0x00011B60
	public void PlaySFX(string name, double playIfNotPlayedFor = double.PositiveInfinity)
	{
		if (name == "")
		{
			return;
		}
		AudioClip[] array = this.audioClips;
		for (int i = 0; i < array.Length; i++)
		{
			AudioClip audioClip = array[i];
			if (Utils.CompareStrings(audioClip.name, name))
			{
				if (this.sfxLastPlayed.Any((KeyValuePair<AudioClip, double> x) => x.Key == audioClip) && playIfNotPlayedFor >= 0.0)
				{
					if (playIfNotPlayedFor == double.PositiveInfinity)
					{
						if (this.sfxLastPlayed[audioClip] + (double)audioClip.length > AudioSettings.dspTime)
						{
							return;
						}
					}
					else if (this.sfxLastPlayed[audioClip] + playIfNotPlayedFor > AudioSettings.dspTime)
					{
						return;
					}
				}
				this.sfxLastPlayed[audioClip] = AudioSettings.dspTime;
				this.sfxAudioSource.PlayOneShot(audioClip, this.sfxVolume);
				return;
			}
		}
		Debug.LogError("SFX " + name + " could not be found in SoundManager");
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00013A70 File Offset: 0x00011C70
	public void PlayOrContinueSong(string name)
	{
		foreach (AudioClip audioClip in this.musicClips)
		{
			if (Utils.CompareStrings(audioClip.name, name))
			{
				this.PlayOrContinueSong(audioClip);
				return;
			}
		}
		Debug.LogError("Song " + name + " could not be found in SoundManager");
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00013AC4 File Offset: 0x00011CC4
	public void PlayOrContinueSong(AudioClip audioClip)
	{
		this.FadeOutAllSongs();
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.type == SoundManager.PlayingClip.Type.Music && playingClip.audioClip == audioClip)
			{
				playingClip.setVolume = 1f;
				return;
			}
		}
		this.AddNewSong(audioClip, 0f);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00013B48 File Offset: 0x00011D48
	private void PlaySongSudden(string name)
	{
		foreach (AudioClip audioClip in this.musicClips)
		{
			if (Utils.CompareStrings(audioClip.name, name))
			{
				this.PlaySongSudden(audioClip);
				return;
			}
		}
		Debug.LogError("Song " + name + " could not be found in SoundManager");
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00013B9C File Offset: 0x00011D9C
	public void PlaySongSudden(AudioClip audioClip)
	{
		this.MuteAllSongs();
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.type == SoundManager.PlayingClip.Type.Music && playingClip.audioClip == audioClip)
			{
				playingClip.currentVolume = 1f;
				playingClip.setVolume = 1f;
				this.ChangeVolume(playingClip);
				return;
			}
		}
		this.AddNewSong(audioClip, 1f);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00013C30 File Offset: 0x00011E30
	private void AddNewSong(AudioClip audioClip, float currentVolume = 1f)
	{
		AudioClip[] array = this.musicClips;
		int i = 0;
		Func<AudioClip, bool> <>9__0;
		while (i < array.Length)
		{
			AudioClip audioClip2 = array[i];
			if (audioClip == audioClip2)
			{
				IEnumerable<AudioClip> enumerable = this.extraClips;
				Func<AudioClip, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (AudioClip x) => x.name == audioClip.name + "_extra");
				}
				AudioClip audioClip3 = enumerable.FirstOrDefault(func);
				if (!audioClip3)
				{
					AudioSource component = Object.Instantiate<GameObject>(this.musicPrefab, Vector3.zero, Quaternion.identity, this.musicParent).GetComponent<AudioSource>();
					component.mute = false;
					component.clip = audioClip;
					component.volume = this.musicVolume;
					component.Play();
					SoundManager.PlayingClip playingClip = new SoundManager.PlayingClip();
					playingClip.audioClip = audioClip;
					playingClip.audioSource = component;
					playingClip.type = SoundManager.PlayingClip.Type.Music;
					playingClip.currentVolume = currentVolume * this.musicVolume;
					playingClip.setVolume = 1f;
					this.playingClips.Add(playingClip);
					return;
				}
				GameObject gameObject = Object.Instantiate<GameObject>(this.musicWithTimePrefab, Vector3.zero, Quaternion.identity, this.musicParent);
				AudioSource component2 = gameObject.GetComponent<AudioSource>();
				component2.mute = false;
				component2.clip = audioClip;
				component2.volume = this.musicVolume;
				AudioSource component3 = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
				component3.mute = false;
				component3.clip = audioClip3;
				component3.volume = 0f;
				component2.PlayScheduled(AudioSettings.dspTime + (double)this.audioBufferLength);
				component3.PlayScheduled(AudioSettings.dspTime + (double)this.audioBufferLength);
				SoundManager.PlayingClip playingClip2 = new SoundManager.PlayingClip();
				playingClip2.audioClip = audioClip;
				playingClip2.audioSource = component2;
				playingClip2.audioSourceExtra = component3;
				playingClip2.type = SoundManager.PlayingClip.Type.Music;
				playingClip2.currentVolume = currentVolume * this.musicVolume;
				playingClip2.setVolume = 1f;
				this.playingClips.Add(playingClip2);
				return;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00013E40 File Offset: 0x00012040
	private void MuteAllSongs()
	{
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.type == SoundManager.PlayingClip.Type.Music)
			{
				playingClip.setVolume = 0f;
				playingClip.currentVolume = 0f;
				playingClip.removeOnFadeOut = false;
				this.ChangeVolume(playingClip);
			}
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00013EBC File Offset: 0x000120BC
	public void FadeOutAllSongs()
	{
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.type == SoundManager.PlayingClip.Type.Music)
			{
				playingClip.setVolume = 0f;
				playingClip.removeOnFadeOut = true;
			}
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00013F24 File Offset: 0x00012124
	public void SetVolumeExtra(float extraVolume)
	{
		foreach (SoundManager.PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.audioSourceExtra)
			{
				playingClip.setVolumeExtra = extraVolume;
			}
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00013F84 File Offset: 0x00012184
	public AudioClip GetSongClipByName(string name)
	{
		foreach (AudioClip audioClip in this.musicClips)
		{
			if (audioClip.name == name)
			{
				return audioClip;
			}
		}
		return null;
	}

	// Token: 0x04000301 RID: 769
	[SerializeField]
	private List<SoundManager.PlayingClip> playingClips = new List<SoundManager.PlayingClip>();

	// Token: 0x04000302 RID: 770
	private static SoundManager _instance;

	// Token: 0x04000303 RID: 771
	[SerializeField]
	private AudioSource sfxAudioSource;

	// Token: 0x04000304 RID: 772
	[SerializeField]
	private Transform musicParent;

	// Token: 0x04000305 RID: 773
	[SerializeField]
	private AudioMixer musicMixer;

	// Token: 0x04000306 RID: 774
	[SerializeField]
	private GameObject musicPrefab;

	// Token: 0x04000307 RID: 775
	[SerializeField]
	private GameObject musicWithTimePrefab;

	// Token: 0x04000308 RID: 776
	[SerializeField]
	private AudioClip[] audioClips;

	// Token: 0x04000309 RID: 777
	[SerializeField]
	private AudioClip[] musicClips;

	// Token: 0x0400030A RID: 778
	[SerializeField]
	private AudioClip[] extraClips;

	// Token: 0x0400030B RID: 779
	public float sfxVolume;

	// Token: 0x0400030C RID: 780
	public float musicVolume;

	// Token: 0x0400030D RID: 781
	public float audioBufferLength = 0.5f;

	// Token: 0x0400030E RID: 782
	public bool musicEffect;

	// Token: 0x0400030F RID: 783
	public float currentMusicEffectWet = 0.01f;

	// Token: 0x04000310 RID: 784
	private Dictionary<AudioClip, double> sfxLastPlayed = new Dictionary<AudioClip, double>();

	// Token: 0x02000115 RID: 277
	private class PlayingClip
	{
		// Token: 0x040004FA RID: 1274
		public SoundManager.PlayingClip.Type type;

		// Token: 0x040004FB RID: 1275
		public AudioClip audioClip;

		// Token: 0x040004FC RID: 1276
		public AudioSource audioSource;

		// Token: 0x040004FD RID: 1277
		public AudioSource audioSourceExtra;

		// Token: 0x040004FE RID: 1278
		public float currentVolume = 1f;

		// Token: 0x040004FF RID: 1279
		public float setVolume = 1f;

		// Token: 0x04000500 RID: 1280
		public float currentVolumeExtra;

		// Token: 0x04000501 RID: 1281
		public float setVolumeExtra;

		// Token: 0x04000502 RID: 1282
		public bool removeOnFadeOut;

		// Token: 0x0200012E RID: 302
		public enum Type
		{
			// Token: 0x0400056A RID: 1386
			SFX,
			// Token: 0x0400056B RID: 1387
			Music
		}
	}
}
