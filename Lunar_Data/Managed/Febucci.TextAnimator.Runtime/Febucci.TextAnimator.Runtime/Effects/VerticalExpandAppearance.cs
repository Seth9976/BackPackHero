using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000012 RID: 18
	[Preserve]
	[CreateAssetMenu(fileName = "Vertical Expand Appearance", menuName = "Text Animator/Animations/Appearances/Vertical Expand")]
	[EffectInfo("vertexp", EffectCategory.Appearances)]
	public sealed class VerticalExpandAppearance : AppearanceScriptableBase
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002E2A File Offset: 0x0000102A
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.SetOrientation(this.startsFromBottom);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E3F File Offset: 0x0000103F
		private void SetOrientation(bool fromBottom)
		{
			if (fromBottom)
			{
				this.startA = 0;
				this.targetA = 1;
				this.startB = 3;
				this.targetB = 2;
				return;
			}
			this.startA = 1;
			this.targetA = 0;
			this.startB = 2;
			this.targetB = 3;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E80 File Offset: 0x00001080
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.pct = Tween.EaseInOut(character.passedTime / this.duration);
			character.current.positions[this.targetA] = Vector3.LerpUnclamped(character.current.positions[this.startA], character.current.positions[this.targetA], this.pct);
			character.current.positions[this.targetB] = Vector3.LerpUnclamped(character.current.positions[this.startB], character.current.positions[this.targetB], this.pct);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F3F File Offset: 0x0000113F
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "bot")
			{
				this.SetOrientation((int)modifier.value == 1);
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x04000038 RID: 56
		public bool startsFromBottom = true;

		// Token: 0x04000039 RID: 57
		private int startA;

		// Token: 0x0400003A RID: 58
		private int targetA;

		// Token: 0x0400003B RID: 59
		private int startB;

		// Token: 0x0400003C RID: 60
		private int targetB;

		// Token: 0x0400003D RID: 61
		private float pct;
	}
}
