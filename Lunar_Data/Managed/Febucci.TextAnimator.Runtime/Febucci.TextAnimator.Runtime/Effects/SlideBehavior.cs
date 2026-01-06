using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001C RID: 28
	[Preserve]
	[CreateAssetMenu(fileName = "Slide Behavior", menuName = "Text Animator/Animations/Behaviors/Slide")]
	[EffectInfo("slide", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 5f)]
	[DefaultValue("baseFrequency", 3f)]
	[DefaultValue("baseWaveSize", 0f)]
	public sealed class SlideBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000038BC File Offset: 0x00001ABC
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.sin = Mathf.Sin(this.frequency * animator.time.timeSinceStart + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity;
			character.current.positions[0] += Vector3.right * this.sin;
			character.current.positions[3] += Vector3.right * this.sin;
			character.current.positions[1] += Vector3.right * -this.sin;
			character.current.positions[2] += Vector3.right * -this.sin;
		}

		// Token: 0x04000063 RID: 99
		private float sin;
	}
}
