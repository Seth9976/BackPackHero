using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000036 RID: 54
	public class ParticleControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00009094 File Offset: 0x00007294
		public static ScriptPlayable<ParticleControlPlayable> Create(PlayableGraph graph, ParticleSystem component, uint randomSeed)
		{
			if (component == null)
			{
				return ScriptPlayable<ParticleControlPlayable>.Null;
			}
			ScriptPlayable<ParticleControlPlayable> scriptPlayable = ScriptPlayable<ParticleControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(component, randomSeed);
			return scriptPlayable;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000090C7 File Offset: 0x000072C7
		// (set) Token: 0x06000289 RID: 649 RVA: 0x000090CF File Offset: 0x000072CF
		public ParticleSystem particleSystem { get; private set; }

		// Token: 0x0600028A RID: 650 RVA: 0x000090D8 File Offset: 0x000072D8
		public void Initialize(ParticleSystem ps, uint randomSeed)
		{
			this.m_RandomSeed = Math.Max(1U, randomSeed);
			this.particleSystem = ps;
			ParticleControlPlayable.SetRandomSeed(this.particleSystem, this.m_RandomSeed);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009100 File Offset: 0x00007300
		private static void SetRandomSeed(ParticleSystem particleSystem, uint randomSeed)
		{
			if (particleSystem == null)
			{
				return;
			}
			particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (particleSystem.useAutoRandomSeed)
			{
				particleSystem.useAutoRandomSeed = false;
				particleSystem.randomSeed = randomSeed;
			}
			for (int i = 0; i < particleSystem.subEmitters.subEmittersCount; i++)
			{
				ParticleControlPlayable.SetRandomSeed(particleSystem.subEmitters.GetSubEmitterSystem(i), randomSeed += 1U);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009168 File Offset: 0x00007368
		public override void PrepareFrame(Playable playable, FrameData data)
		{
			if (this.particleSystem == null || !this.particleSystem.gameObject.activeInHierarchy)
			{
				this.m_LastPlayableTime = float.MaxValue;
				return;
			}
			float num = (float)playable.GetTime<Playable>();
			float time = this.particleSystem.time;
			if (this.m_LastPlayableTime > num || !Mathf.Approximately(time, this.m_LastParticleTime))
			{
				this.Simulate(num, true);
			}
			else if (this.m_LastPlayableTime < num)
			{
				this.Simulate(num - this.m_LastPlayableTime, false);
			}
			this.m_LastPlayableTime = num;
			this.m_LastParticleTime = this.particleSystem.time;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009206 File Offset: 0x00007406
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009213 File Offset: 0x00007413
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009220 File Offset: 0x00007420
		private void Simulate(float time, bool restart)
		{
			float maximumDeltaTime = Time.maximumDeltaTime;
			if (restart)
			{
				this.particleSystem.Simulate(0f, false, true, false);
			}
			while (time > maximumDeltaTime)
			{
				this.particleSystem.Simulate(maximumDeltaTime, false, false, false);
				time -= maximumDeltaTime;
			}
			if (time > 0f)
			{
				this.particleSystem.Simulate(time, false, false, false);
			}
		}

		// Token: 0x040000DB RID: 219
		private const float kUnsetTime = 3.4028235E+38f;

		// Token: 0x040000DC RID: 220
		private float m_LastPlayableTime = float.MaxValue;

		// Token: 0x040000DD RID: 221
		private float m_LastParticleTime = float.MaxValue;

		// Token: 0x040000DE RID: 222
		private uint m_RandomSeed = 1U;
	}
}
