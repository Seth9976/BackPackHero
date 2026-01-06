using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000017 RID: 23
	[Preserve]
	[CreateAssetMenu(fileName = "Pendulum Behavior", menuName = "Text Animator/Animations/Behaviors/Pendulum")]
	[EffectInfo("pend", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 24.7f)]
	[DefaultValue("baseFrequency", 3.1f)]
	[DefaultValue("baseWaveSize", 0.2f)]
	public sealed class PendulumBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003316 File Offset: 0x00001516
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			if (this.anchorBottom)
			{
				this.targetVertex1 = 0;
				this.targetVertex2 = 3;
				return;
			}
			this.targetVertex1 = 1;
			this.targetVertex2 = 2;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003344 File Offset: 0x00001544
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Sin(-animator.time.timeSinceStart * this.frequency + this.waveSize * (float)character.index) * this.amplitude, (character.current.positions[this.targetVertex1] + character.current.positions[this.targetVertex2]) / 2f);
		}

		// Token: 0x04000049 RID: 73
		public bool anchorBottom;

		// Token: 0x0400004A RID: 74
		private int targetVertex1;

		// Token: 0x0400004B RID: 75
		private int targetVertex2;
	}
}
