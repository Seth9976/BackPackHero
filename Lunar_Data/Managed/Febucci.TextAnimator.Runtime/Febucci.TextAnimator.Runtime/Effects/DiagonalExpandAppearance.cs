using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000B RID: 11
	[Preserve]
	[CreateAssetMenu(fileName = "Diagonal Expand Appearance", menuName = "Text Animator/Animations/Appearances/Diagonal Expand")]
	[EffectInfo("diagexp", EffectCategory.Appearances)]
	public sealed class DiagonalExpandAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025C8 File Offset: 0x000007C8
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.diagonalFromBttmLeft = true;
			this.UpdateOrientation();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025DE File Offset: 0x000007DE
		private void UpdateOrientation()
		{
			if (this.diagonalFromBttmLeft)
			{
				this.targetA = 0;
				this.targetB = 2;
				return;
			}
			this.targetA = 1;
			this.targetB = 3;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002608 File Offset: 0x00000808
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.middlePos = character.current.positions.GetMiddlePos();
			this.pct = Tween.EaseInOut(character.passedTime / this.duration);
			character.current.positions[this.targetA] = Vector3.LerpUnclamped(this.middlePos, character.current.positions[this.targetA], this.pct);
			character.current.positions[this.targetB] = Vector3.LerpUnclamped(this.middlePos, character.current.positions[this.targetB], this.pct);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026BD File Offset: 0x000008BD
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "bot")
			{
				this.diagonalFromBttmLeft = (int)modifier.value == 1;
				this.UpdateOrientation();
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x04000024 RID: 36
		public bool diagonalFromBttmLeft;

		// Token: 0x04000025 RID: 37
		private int targetA;

		// Token: 0x04000026 RID: 38
		private int targetB;

		// Token: 0x04000027 RID: 39
		private Vector3 middlePos;

		// Token: 0x04000028 RID: 40
		private float pct;
	}
}
