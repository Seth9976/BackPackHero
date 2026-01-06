using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class EmissionCurve
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000048AD File Offset: 0x00002AAD
		public float GetMaxDuration()
		{
			if (this.cycles <= 0)
			{
				return -1f;
			}
			return this.duration * (float)this.cycles;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000048CC File Offset: 0x00002ACC
		public EmissionCurve()
		{
			this.cycles = -1;
			this.duration = 1f;
			this.weightOverTime = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000492E File Offset: 0x00002B2E
		public EmissionCurve(params Keyframe[] keyframes)
		{
			this.cycles = -1;
			this.duration = 1f;
			this.weightOverTime = new AnimationCurve(keyframes);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004954 File Offset: 0x00002B54
		public float Evaluate(float timePassed)
		{
			if (this.cycles > 0 && timePassed > this.duration * (float)this.cycles)
			{
				return 0f;
			}
			return this.weightOverTime.Evaluate(timePassed % this.duration);
		}

		// Token: 0x04000095 RID: 149
		public int cycles;

		// Token: 0x04000096 RID: 150
		public float duration;

		// Token: 0x04000097 RID: 151
		[SerializeField]
		public AnimationCurve weightOverTime;
	}
}
