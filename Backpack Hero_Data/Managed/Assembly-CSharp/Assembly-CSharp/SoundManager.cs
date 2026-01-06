using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class SoundManager : MonoBehaviour
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00093589 File Offset: 0x00091789
	public static SoundManager main
	{
		get
		{
			return SoundManager._instance;
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00093590 File Offset: 0x00091790
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

	// Token: 0x06000ED1 RID: 3793 RVA: 0x000935C9 File Offset: 0x000917C9
	private void Start()
	{
		this.playingClips = new List<PlayingClip>();
		SoundManager.main.ChangeAllVolume();
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000935E0 File Offset: 0x000917E0
	public void ChangeAllVolume()
	{
		base.StopAllCoroutines();
		AudioSource[] componentsInChildren = this.musicParent.GetComponentsInChildren<AudioSource>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].volume = this.musicVolume;
		}
		this.sfxAudioSource.volume = this.sfxVolume;
		this.pitchedAudioSource.volume = this.sfxVolume;
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x00093640 File Offset: 0x00091840
	private void Update()
	{
		this.time += (double)Time.deltaTime;
		foreach (PlayingClip playingClip in new List<PlayingClip>(this.playingClips))
		{
			if (this.time > playingClip.length + playingClip.timeStarted)
			{
				this.playingClips.Remove(playingClip);
			}
		}
		if (!this.sfxAudioSource.isPlaying && !this.pitchedAudioSource.isPlaying)
		{
			this.playingClips = new List<PlayingClip>();
			this.time = 0.0;
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x000936FC File Offset: 0x000918FC
	public void PlaySFX(string name, float delay)
	{
		base.StartCoroutine(this.DelayThenPlay(name, delay));
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0009370D File Offset: 0x0009190D
	private IEnumerator DelayThenPlay(string name, float delay)
	{
		yield return new WaitForSeconds(delay);
		this.PlaySFX(name);
		yield break;
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0009372C File Offset: 0x0009192C
	public void PlaySFX(string name)
	{
		foreach (PlayingClip playingClip in this.playingClips)
		{
			if (playingClip.name == name && this.time < playingClip.timeStarted + 0.15000000596046448)
			{
				return;
			}
		}
		foreach (AudioClip audioClip in this.audioClips)
		{
			if (audioClip.name == name)
			{
				this.sfxAudioSource.PlayOneShot(audioClip);
				this.playingClips.Add(new PlayingClip(name, this.time, (double)audioClip.length));
				return;
			}
		}
		Debug.Log(name + " could not be found");
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00093808 File Offset: 0x00091A08
	public void PlaySFXPitched(string name, float pitch, bool bypassOverlapPrevention = false)
	{
		this.PlaySFXPitched(name, pitch, 1f, bypassOverlapPrevention);
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x00093818 File Offset: 0x00091A18
	public void PlaySFXPitched(string name, float pitch, float volume, bool bypassOverlapPrevention = false)
	{
		if (!bypassOverlapPrevention)
		{
			foreach (PlayingClip playingClip in this.playingClips)
			{
				if (playingClip.name == name && this.time < playingClip.timeStarted + 0.15000000596046448)
				{
					return;
				}
			}
		}
		foreach (AudioClip audioClip in this.audioClips)
		{
			if (audioClip.name == name)
			{
				this.pitchedAudioSource.pitch = pitch;
				this.pitchedAudioSource.PlayOneShot(audioClip, volume);
				this.playingClips.Add(new PlayingClip(name, this.time, (double)audioClip.length));
				return;
			}
		}
		Debug.Log(name + " could not be found");
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x00093908 File Offset: 0x00091B08
	public void PlayOrContinueSong(AudioClip audioClip)
	{
		base.StopAllCoroutines();
		bool flag = false;
		AudioSource[] componentsInChildren = this.musicParent.GetComponentsInChildren<AudioSource>();
		string name = audioClip.name;
		foreach (AudioSource audioSource in componentsInChildren)
		{
			if (audioSource.clip.name == name)
			{
				base.StartCoroutine(this.FadeIn(audioSource, 4f, 2.5f));
				flag = true;
			}
			else
			{
				base.StartCoroutine(this.FadeOut(audioSource, 3f, 0f, true));
			}
		}
		if (flag)
		{
			return;
		}
		AudioSource component = Object.Instantiate<GameObject>(this.musicPrefab, Vector3.zero, Quaternion.identity, this.musicParent).GetComponent<AudioSource>();
		component.clip = audioClip;
		component.volume = 0f;
		component.Play();
		base.StartCoroutine(this.FadeIn(component, 4f, 1.5f));
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000939E8 File Offset: 0x00091BE8
	public void PlayOrContinueSong(string name, float progress = 0f)
	{
		base.StopAllCoroutines();
		bool flag = false;
		foreach (AudioSource audioSource in this.musicParent.GetComponentsInChildren<AudioSource>())
		{
			if (audioSource.clip.name == name)
			{
				base.StartCoroutine(this.FadeIn(audioSource, 4f, 2.5f));
				flag = true;
			}
			else
			{
				base.StartCoroutine(this.FadeOut(audioSource, 3f, 0f, true));
			}
		}
		foreach (LayeredSong layeredSong in this.musicParent.GetComponentsInChildren<LayeredSong>())
		{
			if (layeredSong.songName == name)
			{
				base.StartCoroutine(this.FadeIn(layeredSong, 4f, 2.5f));
				flag = true;
			}
			else
			{
				base.StartCoroutine(this.FadeOut(layeredSong, 3f, 0f, true));
			}
		}
		if (flag)
		{
			return;
		}
		foreach (AudioClip audioClip in this.musicClips)
		{
			if (audioClip.name == name)
			{
				AudioSource component = Object.Instantiate<GameObject>(this.musicPrefab, Vector3.zero, Quaternion.identity, this.musicParent).GetComponent<AudioSource>();
				component.clip = audioClip;
				component.volume = 0f;
				component.Play();
				base.StartCoroutine(this.FadeIn(component, 4f, 1.5f));
				return;
			}
		}
		foreach (GameObject gameObject in this.layeredSongs)
		{
			if (gameObject.GetComponent<LayeredSong>().songName == name)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, this.musicParent);
				this.layeredSong = gameObject2.GetComponent<LayeredSong>();
				this.layeredSong.progress = progress;
				this.layeredSong.songVolume = 0f;
				this.layeredSong.StartSong();
				base.StartCoroutine(this.FadeIn(this.layeredSong, 4f, 1.5f));
				return;
			}
		}
		Debug.Log(name + " could not be found");
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x00093C05 File Offset: 0x00091E05
	public void PlaySongSudden(string name, float delay, float progress = 0f, bool loop = true)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.PlaySongSudden2(name, delay, loop, progress));
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00093C1F File Offset: 0x00091E1F
	public IEnumerator PlaySongSudden2(string name, float delay, bool loop = true, float progress = 0f)
	{
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		foreach (AudioClip audioClip in this.musicClips)
		{
			if (audioClip.name == name)
			{
				AudioSource component = Object.Instantiate<GameObject>(this.musicPrefab, Vector3.zero, Quaternion.identity, this.musicParent).GetComponent<AudioSource>();
				component.mute = false;
				component.clip = audioClip;
				component.volume = this.musicVolume;
				component.loop = loop;
				component.Play();
				yield break;
			}
		}
		foreach (GameObject gameObject in this.layeredSongs)
		{
			if (gameObject.GetComponent<LayeredSong>().songName == name)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, this.musicParent);
				this.layeredSong = gameObject2.GetComponent<LayeredSong>();
				this.layeredSong.progress = progress;
				this.layeredSong.songVolume = 1f;
				this.layeredSong.StartSong();
				yield break;
			}
		}
		Debug.Log(name + " could not be found");
		yield break;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x00093C4C File Offset: 0x00091E4C
	public void MuteAllSongs()
	{
		foreach (object obj in this.musicParent)
		{
			Transform transform = (Transform)obj;
			LayeredSong component = transform.GetComponent<LayeredSong>();
			if (component == null)
			{
				AudioSource component2 = transform.GetComponent<AudioSource>();
				component2.volume = 0f;
				component2.mute = true;
			}
			else
			{
				component.songVolume = 0f;
			}
		}
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00093CD4 File Offset: 0x00091ED4
	public void FadeOutAllSongs(float time = 0f)
	{
		foreach (object obj in this.musicParent)
		{
			Transform transform = (Transform)obj;
			LayeredSong component = transform.GetComponent<LayeredSong>();
			if (component == null)
			{
				AudioSource component2 = transform.GetComponent<AudioSource>();
				base.StartCoroutine(this.FadeOut(component2, time, 0f, true));
			}
			else
			{
				base.StartCoroutine(this.FadeOut(component, time, 0f, true));
			}
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x00093D6C File Offset: 0x00091F6C
	public void StopAllSongs()
	{
		foreach (object obj in this.musicParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00093DC8 File Offset: 0x00091FC8
	private IEnumerator FadeIn(AudioSource audioSource, float totalTime = 1.5f, float delay = 0f)
	{
		yield return new WaitForSeconds(delay);
		if (!audioSource)
		{
			yield break;
		}
		float time = 0f;
		float startingVolume = audioSource.volume;
		audioSource.mute = false;
		while (time < totalTime)
		{
			if (!audioSource)
			{
				yield break;
			}
			time += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(startingVolume, this.musicVolume, time / totalTime);
			yield return null;
		}
		audioSource.volume = this.musicVolume;
		yield break;
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00093DEC File Offset: 0x00091FEC
	private IEnumerator FadeIn(LayeredSong song, float totalTime = 1.5f, float delay = 0f)
	{
		yield return new WaitForSeconds(delay);
		float time = 0f;
		float startingVolume = song.songVolume;
		while (time < totalTime)
		{
			time += Time.deltaTime;
			song.songVolume = Mathf.Lerp(startingVolume, 1f, time / totalTime);
			yield return null;
		}
		song.songVolume = 1f;
		yield break;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00093E09 File Offset: 0x00092009
	public IEnumerator FadeOut(AudioSource audioSource, float totalTime = 1.5f, float delay = 0f, bool destroy = false)
	{
		yield return new WaitForSeconds(delay);
		float time = 0f;
		float startingSoundLevel = 1f;
		if (audioSource)
		{
			startingSoundLevel = Mathf.Min(this.musicVolume, audioSource.volume);
		}
		audioSource.volume = startingSoundLevel;
		while (time < totalTime && audioSource)
		{
			time += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(startingSoundLevel, 0f, time / totalTime);
			yield return null;
		}
		if (audioSource)
		{
			audioSource.volume = 0f;
			audioSource.mute = true;
		}
		if (destroy && audioSource && audioSource.gameObject)
		{
			Object.Destroy(audioSource.gameObject);
		}
		yield break;
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x00093E35 File Offset: 0x00092035
	public IEnumerator FadeOut(LayeredSong song, float totalTime = 1.5f, float delay = 0f, bool destroy = false)
	{
		yield return new WaitForSeconds(delay);
		float time = 0f;
		float startingSoundLevel = 1f;
		if (song)
		{
			startingSoundLevel = song.songVolume;
		}
		while (time < totalTime && song)
		{
			time += Time.deltaTime;
			song.songVolume = Mathf.Lerp(startingSoundLevel, 0f, time / totalTime);
			yield return null;
		}
		if (song)
		{
			song.songVolume = 0f;
		}
		if (destroy && song && song.gameObject)
		{
			Object.Destroy(song.gameObject);
			if (song == this.layeredSong)
			{
				this.layeredSong = null;
			}
		}
		yield break;
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x00093E61 File Offset: 0x00092061
	public void SetProgress(float progress)
	{
		if (!this.layeredSong)
		{
			return;
		}
		this.layeredSong.progress = progress;
	}

	// Token: 0x04000C11 RID: 3089
	private static SoundManager _instance;

	// Token: 0x04000C12 RID: 3090
	[SerializeField]
	private AudioSource sfxAudioSource;

	// Token: 0x04000C13 RID: 3091
	[SerializeField]
	private AudioSource pitchedAudioSource;

	// Token: 0x04000C14 RID: 3092
	[SerializeField]
	private Transform musicParent;

	// Token: 0x04000C15 RID: 3093
	[SerializeField]
	private GameObject musicPrefab;

	// Token: 0x04000C16 RID: 3094
	[SerializeField]
	private AudioClip[] audioClips;

	// Token: 0x04000C17 RID: 3095
	[SerializeField]
	private AudioClip[] musicClips;

	// Token: 0x04000C18 RID: 3096
	[SerializeField]
	private GameObject[] layeredSongs;

	// Token: 0x04000C19 RID: 3097
	public float sfxVolume;

	// Token: 0x04000C1A RID: 3098
	public float musicVolume;

	// Token: 0x04000C1B RID: 3099
	public LayeredSong layeredSong;

	// Token: 0x04000C1C RID: 3100
	private List<PlayingClip> playingClips;

	// Token: 0x04000C1D RID: 3101
	private double time;
}
