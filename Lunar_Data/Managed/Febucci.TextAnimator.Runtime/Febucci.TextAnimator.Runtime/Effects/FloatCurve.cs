using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public struct FloatCurve
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004994 File Offset: 0x00002B94
		public FloatCurve(float amplitude, float waveSize, float defaultAmplitude)
		{
			this.defaultAmplitude = defaultAmplitude;
			this.enabled = false;
			this.amplitude = amplitude;
			this.weightOverTime = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.5f, 0.5f),
				new Keyframe(1f, 0f)
			});
			this.weightOverTime.preWrapMode = WrapMode.Loop;
			this.weightOverTime.postWrapMode = WrapMode.Loop;
			this.waveSize = 0f;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A2C File Offset: 0x00002C2C
		public float Evaluate(float passedTime, int charIndex)
		{
			if (!this.enabled)
			{
				return this.defaultAmplitude;
			}
			return Mathf.LerpUnclamped(this.defaultAmplitude, this.amplitude, this.weightOverTime.Evaluate(passedTime + this.waveSize * (float)charIndex));
		}

		// Token: 0x04000098 RID: 152
		public bool enabled;

		// Token: 0x04000099 RID: 153
		private readonly float defaultAmplitude;

		// Token: 0x0400009A RID: 154
		public AnimationCurve weightOverTime;

		// Token: 0x0400009B RID: 155
		public float amplitude;

		// Token: 0x0400009C RID: 156
		public float waveSize;
	}
}
