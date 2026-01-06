using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class LayeredSong : MonoBehaviour
{
	// Token: 0x06000EC3 RID: 3779 RVA: 0x00092CA4 File Offset: 0x00090EA4
	private void Awake()
	{
		AudioSource[] components = base.gameObject.GetComponents<AudioSource>();
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
		foreach (LayeredSong.Layer layer in this.songLayers)
		{
			layer.source1 = base.gameObject.AddComponent<AudioSource>();
			layer.source1.clip = layer.introClip;
			layer.source1.playOnAwake = false;
			layer.source2 = base.gameObject.AddComponent<AudioSource>();
			layer.source2.clip = layer.loopClip;
			layer.source2.playOnAwake = false;
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00092D70 File Offset: 0x00090F70
	public void StartSong()
	{
		this.playing = true;
		this.playingSource = 1;
		this.introStartTime = AudioSettings.dspTime + this.safetyDelay;
		this.loopStartTime = this.introStartTime + this.introLength;
		foreach (LayeredSong.Layer layer in this.songLayers)
		{
			layer.source1.PlayScheduled(this.introStartTime);
			layer.internalVolume = (float)((this.progress >= layer.activatesAtStage) ? 100 : 0);
			layer.source1.volume = ((this.progress >= layer.activatesAtStage) ? (SoundManager.main.musicVolume * this.songVolume) : 0f);
			layer.internalVolume = (float)((this.progress >= layer.activatesAtStage) ? 100 : 0);
			layer.source2.volume = ((this.progress >= layer.activatesAtStage) ? (SoundManager.main.musicVolume * this.songVolume) : 0f);
		}
		this.latestStartTime = this.introStartTime;
		SFXLayer[] array = Object.FindObjectsOfType<SFXLayer>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].StartCoroutine("StartSFX");
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00092ECC File Offset: 0x000910CC
	private void Update()
	{
		if (!this.playing)
		{
			return;
		}
		foreach (LayeredSong.Layer layer in this.songLayers)
		{
			if (this.progress >= layer.activatesAtStage && this.progress < layer.deactivatesAtStage)
			{
				layer.internalVolume = (layer.internalVolume * 50f + 100f) / 51f;
			}
			else
			{
				layer.internalVolume *= 0.98f;
			}
			layer.source1.volume = SoundManager.main.musicVolume * layer.internalVolume * this.songVolume / 100f;
			layer.source2.volume = SoundManager.main.musicVolume * layer.internalVolume * this.songVolume / 100f;
			layer.source1.mute = layer.source1.volume <= 0.01f;
			layer.source2.mute = layer.source2.volume <= 0.01f;
		}
		double num = AudioSettings.dspTime - this.latestStartTime;
		if (num > 0.5)
		{
			foreach (LayeredSong.Layer layer2 in this.songLayers)
			{
				try
				{
					int num2 = this.playingSource;
					AudioSource audioSource;
					if (num2 != 1)
					{
						if (num2 != 2)
						{
							audioSource = layer2.source1;
						}
						else
						{
							audioSource = layer2.source2;
						}
					}
					else
					{
						audioSource = layer2.source1;
					}
					float num3 = audioSource.time - (float)num;
					float num4 = Mathf.Abs(num3);
					if (num4 > 0.1f)
					{
						audioSource.time = (float)num;
						Debug.Log("Fixed sync ");
					}
					else if (num4 > 0.01f)
					{
						float num5 = num3 * -0.02f;
						audioSource.pitch = 1f + num5;
					}
					else if (num4 < 0.001f)
					{
						audioSource.pitch = 1f;
					}
				}
				catch
				{
				}
			}
		}
		if (AudioSettings.dspTime > this.loopStartTime + this.loopLength * (double)this.loopCount - this.safetyLoopDelay)
		{
			foreach (LayeredSong.Layer layer3 in this.songLayers)
			{
				if (this.playingSource == 1)
				{
					layer3.source2.clip = layer3.loopClip;
					layer3.source2.PlayScheduled(this.loopStartTime + this.loopLength * (double)this.loopCount);
					this.latestStartTime = this.loopStartTime + this.loopLength * (double)this.loopCount;
				}
				else
				{
					layer3.source1.clip = layer3.loopClip;
					layer3.source1.PlayScheduled(this.loopStartTime + this.loopLength * (double)this.loopCount);
					this.latestStartTime = this.loopStartTime + this.loopLength * (double)this.loopCount;
				}
			}
			this.loopCount += 1L;
			this.playingSource = ((this.playingSource == 1) ? 2 : 1);
		}
	}

	// Token: 0x04000BF7 RID: 3063
	public List<LayeredSong.Layer> songLayers;

	// Token: 0x04000BF8 RID: 3064
	public string songName;

	// Token: 0x04000BF9 RID: 3065
	[Range(0f, 100f)]
	public float progress;

	// Token: 0x04000BFA RID: 3066
	public double introLength;

	// Token: 0x04000BFB RID: 3067
	public double loopLength;

	// Token: 0x04000BFC RID: 3068
	public float songVolume;

	// Token: 0x04000BFD RID: 3069
	public bool playing;

	// Token: 0x04000BFE RID: 3070
	public int playingSource = 1;

	// Token: 0x04000BFF RID: 3071
	private double latestStartTime;

	// Token: 0x04000C00 RID: 3072
	public double safetyDelay;

	// Token: 0x04000C01 RID: 3073
	public double safetyLoopDelay = 1.0;

	// Token: 0x04000C02 RID: 3074
	[HideInInspector]
	public double loopStartTime;

	// Token: 0x04000C03 RID: 3075
	[HideInInspector]
	public double introStartTime;

	// Token: 0x04000C04 RID: 3076
	[HideInInspector]
	public long loopCount;

	// Token: 0x0200043E RID: 1086
	[Serializable]
	public class Layer
	{
		// Token: 0x0400194A RID: 6474
		public string name;

		// Token: 0x0400194B RID: 6475
		[SerializeField]
		public AudioClip introClip;

		// Token: 0x0400194C RID: 6476
		[SerializeField]
		public AudioClip loopClip;

		// Token: 0x0400194D RID: 6477
		public float activatesAtStage;

		// Token: 0x0400194E RID: 6478
		public float deactivatesAtStage = 999999f;

		// Token: 0x0400194F RID: 6479
		public bool isFx;

		// Token: 0x04001950 RID: 6480
		[HideInInspector]
		public float fxDistance;

		// Token: 0x04001951 RID: 6481
		[HideInInspector]
		public float internalVolume;

		// Token: 0x04001952 RID: 6482
		[HideInInspector]
		public AudioSource source1;

		// Token: 0x04001953 RID: 6483
		[HideInInspector]
		public AudioSource source2;
	}
}
