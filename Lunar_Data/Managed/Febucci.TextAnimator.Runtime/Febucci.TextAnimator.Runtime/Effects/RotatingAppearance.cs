using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000010 RID: 16
	[Preserve]
	[CreateAssetMenu(fileName = "Rotating Appearance", menuName = "Text Animator/Animations/Appearances/Rotating")]
	[EffectInfo("rot", EffectCategory.Appearances)]
	[DefaultValue("baseDuration", 0.7f)]
	public sealed class RotatingAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002CEE File Offset: 0x00000EEE
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.targetAngle = this.baseTargetAngle;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D03 File Offset: 0x00000F03
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(Mathf.Lerp(this.targetAngle, 0f, Tween.EaseInOut(character.passedTime / this.duration)));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D37 File Offset: 0x00000F37
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.targetAngle = this.baseTargetAngle * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x04000034 RID: 52
		public float baseTargetAngle = 50f;

		// Token: 0x04000035 RID: 53
		private float targetAngle;
	}
}
