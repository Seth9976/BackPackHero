using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001F RID: 31
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Wiggle", fileName = "Wiggle Behavior")]
	[EffectInfo("wiggle", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 4.74f)]
	[DefaultValue("baseFrequency", 7.82f)]
	[DefaultValue("baseWaveSize", 0.551f)]
	public sealed class WiggleBehavior : BehaviorScriptableSine
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003A84 File Offset: 0x00001C84
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.directions = new Vector3[23];
			for (int i = 0; i < 23; i++)
			{
				this.directions[i] = TextUtilities.fakeRandoms[Random.Range(0, 24)] * Mathf.Sign(Mathf.Sin((float)i));
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.indexCache = character.index % 23;
			character.current.positions.MoveChar(this.directions[this.indexCache] * Mathf.Sin(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity);
		}

		// Token: 0x04000064 RID: 100
		private const int maxDirections = 23;

		// Token: 0x04000065 RID: 101
		private Vector3[] directions;

		// Token: 0x04000066 RID: 102
		private int indexCache;
	}
}
