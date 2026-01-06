using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000004 RID: 4
	[TrackClipType(typeof(ActivationPlayableAsset))]
	[TrackBindingType(typeof(GameObject))]
	[ExcludeFromPreset]
	[Serializable]
	public class ActivationTrack : TrackAsset
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000217B File Offset: 0x0000037B
		internal override bool CanCompileClips()
		{
			return !base.hasClips || base.CanCompileClips();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000218D File Offset: 0x0000038D
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002195 File Offset: 0x00000395
		public ActivationTrack.PostPlaybackState postPlaybackState
		{
			get
			{
				return this.m_PostPlaybackState;
			}
			set
			{
				this.m_PostPlaybackState = value;
				this.UpdateTrackMode();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A4 File Offset: 0x000003A4
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<ActivationMixerPlayable> scriptPlayable = ActivationMixerPlayable.Create(graph, inputCount);
			this.m_ActivationMixer = scriptPlayable.GetBehaviour();
			this.UpdateTrackMode();
			return scriptPlayable;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D2 File Offset: 0x000003D2
		internal void UpdateTrackMode()
		{
			if (this.m_ActivationMixer != null)
			{
				this.m_ActivationMixer.postPlaybackState = this.m_PostPlaybackState;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F0 File Offset: 0x000003F0
		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			GameObject gameObjectBinding = base.GetGameObjectBinding(director);
			if (gameObjectBinding != null)
			{
				driver.AddFromName(gameObjectBinding, "m_IsActive");
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000221A File Offset: 0x0000041A
		protected override void OnCreateClip(TimelineClip clip)
		{
			clip.displayName = "Active";
			base.OnCreateClip(clip);
		}

		// Token: 0x04000004 RID: 4
		[SerializeField]
		private ActivationTrack.PostPlaybackState m_PostPlaybackState = ActivationTrack.PostPlaybackState.LeaveAsIs;

		// Token: 0x04000005 RID: 5
		private ActivationMixerPlayable m_ActivationMixer;

		// Token: 0x02000055 RID: 85
		public enum PostPlaybackState
		{
			// Token: 0x0400010C RID: 268
			Active,
			// Token: 0x0400010D RID: 269
			Inactive,
			// Token: 0x0400010E RID: 270
			Revert,
			// Token: 0x0400010F RID: 271
			LeaveAsIs
		}
	}
}
