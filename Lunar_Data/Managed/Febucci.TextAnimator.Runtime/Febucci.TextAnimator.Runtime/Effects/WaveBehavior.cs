using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001E RID: 30
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Wave", fileName = "Wave Behavior")]
	[EffectInfo("wave", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 7.27f)]
	[DefaultValue("baseFrequency", 4f)]
	[DefaultValue("baseWaveSize", 0.4f)]
	public sealed class WaveBehavior : BehaviorScriptableSine
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003A18 File Offset: 0x00001C18
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.MoveChar(Vector3.up * Mathf.Sin(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity);
		}
	}
}
