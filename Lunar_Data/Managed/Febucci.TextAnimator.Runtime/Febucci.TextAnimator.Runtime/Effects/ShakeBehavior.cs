using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001A RID: 26
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Shake", fileName = "Shake Behavior")]
	[EffectInfo("shake", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 1.13f)]
	[DefaultValue("baseDelay", 0.1f)]
	[DefaultValue("baseWaveSize", 0.45f)]
	public sealed class ShakeBehavior : BehaviorScriptableBase
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000035C2 File Offset: 0x000017C2
		private void ClampValues()
		{
			this.delay = Mathf.Clamp(this.delay, 0.002f, 500f);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000035DF File Offset: 0x000017DF
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.randIndex = Random.Range(0, 25);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000035F5 File Offset: 0x000017F5
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude;
			this.delay = this.baseDelay;
			this.waveSize = this.baseWaveSize;
			this.ClampValues();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003624 File Offset: 0x00001824
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (!(name == "a"))
			{
				if (!(name == "d"))
				{
					if (name == "w")
					{
						this.waveSize = this.baseWaveSize * modifier.value;
					}
				}
				else
				{
					this.delay = this.baseDelay * modifier.value;
				}
			}
			else
			{
				this.amplitude = this.baseAmplitude * modifier.value;
			}
			this.ClampValues();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000036A4 File Offset: 0x000018A4
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = animator.time.timeSinceStart;
			this.timePassed += (float)character.index * this.waveSize;
			this.randIndex = Mathf.RoundToInt(this.timePassed / this.delay) % 25;
			if (this.randIndex < 0)
			{
				this.randIndex *= -1;
			}
			character.current.positions.MoveChar(TextUtilities.fakeRandoms[this.randIndex] * this.amplitude * character.uniformIntensity);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003745 File Offset: 0x00001945
		private void OnValidate()
		{
			this.ClampValues();
		}

		// Token: 0x04000055 RID: 85
		public float baseAmplitude = 0.085f;

		// Token: 0x04000056 RID: 86
		public float baseDelay = 0.04f;

		// Token: 0x04000057 RID: 87
		public float baseWaveSize = 0.2f;

		// Token: 0x04000058 RID: 88
		private float amplitude;

		// Token: 0x04000059 RID: 89
		private float delay;

		// Token: 0x0400005A RID: 90
		private float waveSize;

		// Token: 0x0400005B RID: 91
		private int randIndex;

		// Token: 0x0400005C RID: 92
		private float timePassed;
	}
}
