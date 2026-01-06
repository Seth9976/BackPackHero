using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000033 RID: 51
	public class DirectorControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static ScriptPlayable<DirectorControlPlayable> Create(PlayableGraph graph, PlayableDirector director)
		{
			if (director == null)
			{
				return ScriptPlayable<DirectorControlPlayable>.Null;
			}
			ScriptPlayable<DirectorControlPlayable> scriptPlayable = ScriptPlayable<DirectorControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().director = director;
			return scriptPlayable;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008C1A File Offset: 0x00006E1A
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				this.director.Stop();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008C48 File Offset: 0x00006E48
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			this.m_SyncTime |= info.evaluationType == FrameData.EvaluationType.Evaluate || this.DetectDiscontinuity(playable, info);
			this.SyncSpeed((double)info.effectiveSpeed);
			this.SyncStart(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008CC5 File Offset: 0x00006EC5
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_SyncTime = true;
			if (this.director != null && this.director.playableAsset != null)
			{
				this.m_AssetDuration = this.director.playableAsset.duration;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008D08 File Offset: 0x00006F08
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				if (info.effectivePlayState == PlayState.Playing)
				{
					this.director.Pause();
					return;
				}
				this.director.Stop();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008D58 File Offset: 0x00006F58
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			if (this.m_SyncTime || this.DetectOutOfSync(playable))
			{
				this.UpdateTime(playable);
				if (this.director.playableGraph.IsValid())
				{
					this.director.playableGraph.Evaluate();
					this.director.playableGraph.SynchronizeEvaluation(playable.GetGraph<Playable>());
				}
				else
				{
					this.director.Evaluate();
				}
			}
			this.m_SyncTime = false;
			this.SyncStop(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008E14 File Offset: 0x00007014
		private void SyncSpeed(double speed)
		{
			if (this.director.playableGraph.IsValid())
			{
				int rootPlayableCount = this.director.playableGraph.GetRootPlayableCount();
				for (int i = 0; i < rootPlayableCount; i++)
				{
					Playable rootPlayable = this.director.playableGraph.GetRootPlayable(i);
					if (rootPlayable.IsValid<Playable>())
					{
						rootPlayable.SetSpeed(speed);
					}
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008E7C File Offset: 0x0000707C
		private void SyncStart(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Playing || !graph.IsPlaying() || (this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration))
			{
				return;
			}
			if (graph.IsMatchFrameRateEnabled())
			{
				this.director.Play(graph.GetFrameRate());
				return;
			}
			this.director.Play();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008EDF File Offset: 0x000070DF
		private void SyncStop(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Paused)
			{
				return;
			}
			if ((this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration) || !graph.IsPlaying())
			{
				this.director.Pause();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008F1F File Offset: 0x0000711F
		private bool DetectDiscontinuity(Playable playable, FrameData info)
		{
			return Math.Abs(playable.GetTime<Playable>() - playable.GetPreviousTime<Playable>() - info.m_DeltaTime * (double)info.m_EffectiveSpeed) > DiscreteTime.tickValue;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008F4C File Offset: 0x0000714C
		private bool DetectOutOfSync(Playable playable)
		{
			double num = playable.GetTime<Playable>();
			if (playable.GetTime<Playable>() >= this.m_AssetDuration)
			{
				switch (this.director.extrapolationMode)
				{
				case DirectorWrapMode.Hold:
					num = this.m_AssetDuration;
					break;
				case DirectorWrapMode.Loop:
					num %= this.m_AssetDuration;
					break;
				case DirectorWrapMode.None:
					num = this.m_AssetDuration;
					break;
				}
			}
			return !Mathf.Approximately((float)num, (float)this.director.time);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008FC4 File Offset: 0x000071C4
		private void UpdateTime(Playable playable)
		{
			double num = Math.Max(0.1, this.director.playableAsset.duration);
			switch (this.director.extrapolationMode)
			{
			case DirectorWrapMode.Hold:
				this.director.time = Math.Min(num, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			case DirectorWrapMode.Loop:
				this.director.time = Math.Max(0.0, playable.GetTime<Playable>() % num);
				return;
			case DirectorWrapMode.None:
				this.director.time = Math.Min(num, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			default:
				return;
			}
		}

		// Token: 0x040000D4 RID: 212
		public PlayableDirector director;

		// Token: 0x040000D5 RID: 213
		private bool m_SyncTime;

		// Token: 0x040000D6 RID: 214
		private double m_AssetDuration = double.MaxValue;
	}
}
