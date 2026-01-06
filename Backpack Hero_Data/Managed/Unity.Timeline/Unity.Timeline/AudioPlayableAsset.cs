using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class AudioPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000066DF File Offset: 0x000048DF
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000066E7 File Offset: 0x000048E7
		internal float bufferingTime
		{
			get
			{
				return this.m_bufferingTime;
			}
			set
			{
				this.m_bufferingTime = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000066F0 File Offset: 0x000048F0
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000066F8 File Offset: 0x000048F8
		public AudioClip clip
		{
			get
			{
				return this.m_Clip;
			}
			set
			{
				this.m_Clip = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006701 File Offset: 0x00004901
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006709 File Offset: 0x00004909
		public bool loop
		{
			get
			{
				return this.m_Loop;
			}
			set
			{
				this.m_Loop = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006712 File Offset: 0x00004912
		public override double duration
		{
			get
			{
				if (this.m_Clip == null)
				{
					return base.duration;
				}
				return (double)this.m_Clip.samples / (double)this.m_Clip.frequency;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006742 File Offset: 0x00004942
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006754 File Offset: 0x00004954
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.m_Clip == null)
			{
				return Playable.Null;
			}
			AudioClipPlayable audioClipPlayable = AudioClipPlayable.Create(graph, this.m_Clip, this.m_Loop);
			audioClipPlayable.GetHandle().SetScriptInstance(this.m_ClipProperties.Clone());
			return audioClipPlayable;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000067A8 File Offset: 0x000049A8
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Blending | (this.m_Loop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private AudioClip m_Clip;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private bool m_Loop;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		[HideInInspector]
		private float m_bufferingTime = 0.1f;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private AudioClipProperties m_ClipProperties = new AudioClipProperties();
	}
}
