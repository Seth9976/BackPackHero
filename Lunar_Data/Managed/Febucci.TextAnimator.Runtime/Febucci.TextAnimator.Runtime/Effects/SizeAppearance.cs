using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000011 RID: 17
	[Preserve]
	[CreateAssetMenu(fileName = "Size Appearance", menuName = "Text Animator/Animations/Appearances/Size")]
	[EffectInfo("size", EffectCategory.Appearances)]
	public sealed class SizeAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002D79 File Offset: 0x00000F79
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amplitude = this.baseAmplitude * -1f + 1f;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D9C File Offset: 0x00000F9C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.LerpUnclamped(character.current.positions.GetMiddlePos(), Tween.EaseIn(1f - character.passedTime / this.duration) * this.amplitude);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x04000036 RID: 54
		private float amplitude;

		// Token: 0x04000037 RID: 55
		public float baseAmplitude = 2f;
	}
}
