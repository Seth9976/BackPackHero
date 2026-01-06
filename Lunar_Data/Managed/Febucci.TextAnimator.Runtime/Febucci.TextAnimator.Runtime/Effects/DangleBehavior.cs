using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000015 RID: 21
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Dangle", fileName = "Dangle Behavior")]
	[EffectInfo("dangle", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 7.87f)]
	[DefaultValue("baseFrequency", 3.37f)]
	[DefaultValue("baseWaveSize", 0.306f)]
	public sealed class DangleBehavior : BehaviorScriptableSine
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003096 File Offset: 0x00001296
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			if (this.anchorBottom)
			{
				this.targetIndex1 = 1;
				this.targetIndex2 = 2;
				return;
			}
			this.targetIndex1 = 0;
			this.targetIndex2 = 3;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000030C4 File Offset: 0x000012C4
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.sin = Mathf.Sin(this.frequency * animator.time.timeSinceStart + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity;
			character.current.positions[this.targetIndex1] += Vector3.right * this.sin;
			character.current.positions[this.targetIndex2] += Vector3.right * this.sin;
		}

		// Token: 0x04000040 RID: 64
		public bool anchorBottom;

		// Token: 0x04000041 RID: 65
		private float sin;

		// Token: 0x04000042 RID: 66
		private int targetIndex1;

		// Token: 0x04000043 RID: 67
		private int targetIndex2;
	}
}
