using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000002 RID: 2
	internal class ActivationMixerPlayable : PlayableBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ScriptPlayable<ActivationMixerPlayable> Create(PlayableGraph graph, int inputCount)
		{
			return ScriptPlayable<ActivationMixerPlayable>.Create(graph, inputCount);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public ActivationTrack.PostPlaybackState postPlaybackState
		{
			get
			{
				return this.m_PostPlaybackState;
			}
			set
			{
				this.m_PostPlaybackState = value;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.m_BoundGameObject == null)
			{
				return;
			}
			switch (this.m_PostPlaybackState)
			{
			case ActivationTrack.PostPlaybackState.Active:
				this.m_BoundGameObject.SetActive(true);
				return;
			case ActivationTrack.PostPlaybackState.Inactive:
				this.m_BoundGameObject.SetActive(false);
				return;
			case ActivationTrack.PostPlaybackState.Revert:
				this.m_BoundGameObject.SetActive(this.m_BoundGameObjectInitialStateIsActive);
				break;
			case ActivationTrack.PostPlaybackState.LeaveAsIs:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.m_BoundGameObject == null)
			{
				this.m_BoundGameObject = playerData as GameObject;
				this.m_BoundGameObjectInitialStateIsActive = this.m_BoundGameObject != null && this.m_BoundGameObject.activeSelf;
			}
			if (this.m_BoundGameObject == null)
			{
				return;
			}
			int inputCount = playable.GetInputCount<Playable>();
			bool flag = false;
			for (int i = 0; i < inputCount; i++)
			{
				if (playable.GetInputWeight(i) > 0f)
				{
					flag = true;
					break;
				}
			}
			this.m_BoundGameObject.SetActive(flag);
		}

		// Token: 0x04000001 RID: 1
		private ActivationTrack.PostPlaybackState m_PostPlaybackState;

		// Token: 0x04000002 RID: 2
		private bool m_BoundGameObjectInitialStateIsActive;

		// Token: 0x04000003 RID: 3
		private GameObject m_BoundGameObject;
	}
}
