using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001B RID: 27
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Size", fileName = "Size Behavior")]
	[EffectInfo("incr", EffectCategory.Behaviors)]
	public sealed class SizeBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003776 File Offset: 0x00001976
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude * -1f + 1f;
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037A8 File Offset: 0x000019A8
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value * -1f + 1f;
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

		// Token: 0x06000066 RID: 102 RVA: 0x0000382C File Offset: 0x00001A2C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.LerpUnclamped(character.current.positions.GetMiddlePos(), (Mathf.Cos(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) + 1f) / 2f * this.amplitude);
		}

		// Token: 0x0400005D RID: 93
		public float baseAmplitude = 1.5f;

		// Token: 0x0400005E RID: 94
		public float baseFrequency = 4f;

		// Token: 0x0400005F RID: 95
		public float baseWaveSize = 0.2f;

		// Token: 0x04000060 RID: 96
		private float amplitude;

		// Token: 0x04000061 RID: 97
		private float frequency;

		// Token: 0x04000062 RID: 98
		private float waveSize;
	}
}
