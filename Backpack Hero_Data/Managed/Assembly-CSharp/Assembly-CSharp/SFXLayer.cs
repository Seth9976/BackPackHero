using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class SFXLayer : MonoBehaviour
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x00093262 File Offset: 0x00091462
	private void Start()
	{
		base.StartCoroutine("StartSFX");
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00093270 File Offset: 0x00091470
	public IEnumerator StartSFX()
	{
		this.songtoSyncTo = null;
		while (this.songtoSyncTo == null)
		{
			if (SoundManager.main.layeredSong != null && this.songtoSyncToPrefab.GetComponent<LayeredSong>().songName == SoundManager.main.layeredSong.songName && SoundManager.main.layeredSong.playing)
			{
				this.songtoSyncTo = SoundManager.main.layeredSong;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		AudioSource[] components = base.gameObject.GetComponents<AudioSource>();
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
		this.source1 = base.gameObject.AddComponent<AudioSource>();
		this.source2 = base.gameObject.AddComponent<AudioSource>();
		this.source1.mute = false;
		this.source2.mute = false;
		this.source1.volume = 0.5f;
		this.source2.volume = 0.5f;
		this.source1.spatialBlend = 1f;
		this.source1.rolloffMode = AudioRolloffMode.Linear;
		this.source1.minDistance = 5f;
		this.source1.maxDistance = 20f;
		this.source2.spatialBlend = 1f;
		this.source2.rolloffMode = AudioRolloffMode.Linear;
		this.source2.minDistance = 5f;
		this.source2.maxDistance = 20f;
		this.playing = true;
		this.playingSource = 1;
		if (AudioSettings.dspTime < this.songtoSyncTo.introStartTime)
		{
			this.source1.clip = this.introClip;
			this.source1.PlayScheduled(this.songtoSyncTo.introStartTime + (double)Random.Range(-0.04f, 0.04f));
			yield break;
		}
		if (AudioSettings.dspTime < this.songtoSyncTo.introStartTime + this.songtoSyncTo.introLength - (double)this.safetyDelay)
		{
			this.source1.clip = this.introClip;
			this.source1.time = this.songtoSyncTo.songLayers[0].source1.time + Random.Range(-0.04f, 0.04f);
			this.source1.Play();
			yield break;
		}
		this.source1.clip = this.loopClip;
		this.source1.time = ((this.songtoSyncTo.playingSource == 1) ? this.songtoSyncTo.songLayers[0].source1.time : this.songtoSyncTo.songLayers[0].source2.time);
		this.source1.time += this.safetyDelay;
		this.source1.PlayScheduled(AudioSettings.dspTime + (double)this.safetyDelay + (double)Random.Range(-0.04f, 0.04f));
		yield break;
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00093280 File Offset: 0x00091480
	private void Update()
	{
		if (!this.playing)
		{
			return;
		}
		if (SoundManager.main.layeredSong == null)
		{
			base.StartCoroutine(SoundManager.main.FadeOut(this.source1, 1.5f, 0f, false));
			base.StartCoroutine(SoundManager.main.FadeOut(this.source2, 1.5f, 0f, false));
			this.playing = false;
			return;
		}
		this.source1.volume = SoundManager.main.sfxVolume;
		this.source2.volume = SoundManager.main.sfxVolume;
		float num = ((SoundManager.main.layeredSong.playingSource == 1) ? SoundManager.main.layeredSong.songLayers[0].source1.time : SoundManager.main.layeredSong.songLayers[0].source2.time);
		try
		{
			int num2 = this.playingSource;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					if (Mathf.Abs(this.source2.time - num) > 0.05f)
					{
						this.source2.time = Mathf.Clamp(num + Random.Range(-0.04f, 0.04f), 0f, this.source2.clip.length);
					}
				}
			}
			else if (Mathf.Abs(this.source1.time - num) > 0.05f)
			{
				this.source1.time = Mathf.Clamp(num + Random.Range(-0.04f, 0.04f), 0f, this.source1.clip.length);
			}
		}
		catch (Exception)
		{
		}
		if (AudioSettings.dspTime > SoundManager.main.layeredSong.loopStartTime + SoundManager.main.layeredSong.loopLength * (double)SoundManager.main.layeredSong.loopCount - SoundManager.main.layeredSong.safetyLoopDelay)
		{
			if (this.playingSource == 1)
			{
				this.source2.clip = this.loopClip;
				this.source2.PlayScheduled(SoundManager.main.layeredSong.loopStartTime + SoundManager.main.layeredSong.loopLength * (double)SoundManager.main.layeredSong.loopCount);
			}
			else
			{
				this.source1.clip = this.loopClip;
				this.source1.PlayScheduled(SoundManager.main.layeredSong.loopStartTime + SoundManager.main.layeredSong.loopLength * (double)SoundManager.main.layeredSong.loopCount);
			}
			this.playingSource = ((this.playingSource == 1) ? 2 : 1);
		}
	}

	// Token: 0x04000C05 RID: 3077
	public GameObject songtoSyncToPrefab;

	// Token: 0x04000C06 RID: 3078
	[HideInInspector]
	private LayeredSong songtoSyncTo;

	// Token: 0x04000C07 RID: 3079
	public AudioClip introClip;

	// Token: 0x04000C08 RID: 3080
	public AudioClip loopClip;

	// Token: 0x04000C09 RID: 3081
	private AudioSource source1;

	// Token: 0x04000C0A RID: 3082
	private AudioSource source2;

	// Token: 0x04000C0B RID: 3083
	public float safetyDelay = 1f;

	// Token: 0x04000C0C RID: 3084
	public int playingSource = 1;

	// Token: 0x04000C0D RID: 3085
	public bool playing;
}
