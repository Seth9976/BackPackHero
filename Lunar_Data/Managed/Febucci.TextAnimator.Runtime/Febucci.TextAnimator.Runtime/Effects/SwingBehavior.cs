using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001D RID: 29
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Swing", fileName = "Swing Behavior")]
	[EffectInfo("swing", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 22.74f)]
	[DefaultValue("baseFrequency", 3.65f)]
	[DefaultValue("baseWaveSize", 0.171f)]
	public sealed class SwingBehavior : BehaviorScriptableSine
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000039D1 File Offset: 0x00001BD1
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Cos(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude);
		}
	}
}
