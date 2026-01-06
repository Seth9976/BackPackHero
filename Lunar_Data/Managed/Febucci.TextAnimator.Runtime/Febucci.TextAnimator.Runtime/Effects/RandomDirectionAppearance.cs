using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000F RID: 15
	[Preserve]
	[CreateAssetMenu(fileName = "RandomDir Appearance", menuName = "Text Animator/Animations/Appearances/Random Direction")]
	[EffectInfo("rdir", EffectCategory.Appearances)]
	public sealed class RandomDirectionAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.amount = this.baseAmount;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BDC File Offset: 0x00000DDC
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.directions = new Vector3[20];
			for (int i = 0; i < this.directions.Length; i++)
			{
				this.directions[i] = TextUtilities.fakeRandoms[Random.Range(0, 24)] * Mathf.Sign(Mathf.Sin((float)i));
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C40 File Offset: 0x00000E40
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			int num = character.index % this.directions.Length;
			character.current.positions.MoveChar(this.directions[num] * this.amount * character.uniformIntensity * Tween.EaseIn(1f - character.passedTime / this.duration));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CAC File Offset: 0x00000EAC
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "a")
			{
				this.amount = this.baseAmount * modifier.value;
				return;
			}
			base.SetModifier(modifier);
		}

		// Token: 0x04000031 RID: 49
		public float baseAmount = 10f;

		// Token: 0x04000032 RID: 50
		private float amount;

		// Token: 0x04000033 RID: 51
		private Vector3[] directions;
	}
}
