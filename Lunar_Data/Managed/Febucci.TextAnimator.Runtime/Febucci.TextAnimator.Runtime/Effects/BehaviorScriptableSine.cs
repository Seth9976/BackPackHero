using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000021 RID: 33
	public abstract class BehaviorScriptableSine : BehaviorScriptableBase
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003B78 File Offset: 0x00001D78
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude;
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value;
				return;
			}
			if (name == "f")
			{
				this.frequency = this.baseFrequency * modifier.value;
				return;
			}
			if (!(name == "w"))
			{
				return;
			}
			this.waveSize = this.baseWaveSize * modifier.value;
		}

		// Token: 0x04000067 RID: 103
		public float baseAmplitude = 1f;

		// Token: 0x04000068 RID: 104
		public float baseFrequency = 1f;

		// Token: 0x04000069 RID: 105
		[Range(0f, 1f)]
		public float baseWaveSize = 0.2f;

		// Token: 0x0400006A RID: 106
		protected float amplitude;

		// Token: 0x0400006B RID: 107
		protected float frequency;

		// Token: 0x0400006C RID: 108
		protected float waveSize;
	}
}
