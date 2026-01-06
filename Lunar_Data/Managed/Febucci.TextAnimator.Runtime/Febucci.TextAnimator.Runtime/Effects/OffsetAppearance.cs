using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000E RID: 14
	[Preserve]
	[CreateAssetMenu(fileName = "Offset Appearance", menuName = "Text Animator/Animations/Appearances/Offset")]
	[EffectInfo("offset", EffectCategory.Appearances)]
	public sealed class OffsetAppearance : AppearanceScriptableBase
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002B04 File Offset: 0x00000D04
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amount = this.baseAmount;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B1C File Offset: 0x00000D1C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.MoveChar(this.baseDirection * this.amount * character.uniformIntensity * Tween.EaseIn(1f - character.passedTime / this.duration));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B77 File Offset: 0x00000D77
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amount = this.baseAmount * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x0400002E RID: 46
		public float baseAmount = 10f;

		// Token: 0x0400002F RID: 47
		private float amount;

		// Token: 0x04000030 RID: 48
		public Vector2 baseDirection = Vector2.one;
	}
}
