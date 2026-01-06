using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000D RID: 13
	[Preserve]
	[CreateAssetMenu(fileName = "Horizontal Expand Appearance", menuName = "Text Animator/Animations/Appearances/Horizontal Expand")]
	[EffectInfo("horiexp", EffectCategory.Appearances)]
	public sealed class HorizontalExpandAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002781 File Offset: 0x00000981
		public override void ResetContext(TAnimCore animator)
		{
			base.ResetContext(animator);
			this.type = HorizontalExpandAppearance.ExpType.Left;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002794 File Offset: 0x00000994
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.pct = Tween.EaseInOut(character.passedTime / this.duration);
			switch (this.type)
			{
			default:
				this.startTop = character.current.positions[1];
				this.startBot = character.current.positions[0];
				character.current.positions[2] = Vector3.LerpUnclamped(this.startTop, character.current.positions[2], this.pct);
				character.current.positions[3] = Vector3.LerpUnclamped(this.startBot, character.current.positions[3], this.pct);
				return;
			case HorizontalExpandAppearance.ExpType.Middle:
				this.startTop = (character.current.positions[1] + character.current.positions[2]) / 2f;
				this.startBot = (character.current.positions[0] + character.current.positions[3]) / 2f;
				character.current.positions[1] = Vector3.LerpUnclamped(this.startTop, character.current.positions[1], this.pct);
				character.current.positions[2] = Vector3.LerpUnclamped(this.startTop, character.current.positions[2], this.pct);
				character.current.positions[0] = Vector3.LerpUnclamped(this.startBot, character.current.positions[0], this.pct);
				character.current.positions[3] = Vector3.LerpUnclamped(this.startBot, character.current.positions[3], this.pct);
				return;
			case HorizontalExpandAppearance.ExpType.Right:
				this.startTop = character.current.positions[2];
				this.startBot = character.current.positions[3];
				character.current.positions[1] = Vector3.LerpUnclamped(this.startTop, character.current.positions[1], this.pct);
				character.current.positions[0] = Vector3.LerpUnclamped(this.startBot, character.current.positions[0], this.pct);
				return;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A7C File Offset: 0x00000C7C
		public override void SetModifier(ModifierInfo modifier)
		{
			if (!(modifier.name == "x"))
			{
				base.SetModifier(modifier);
				return;
			}
			float value = modifier.value;
			if (value == -1f)
			{
				this.type = HorizontalExpandAppearance.ExpType.Left;
				return;
			}
			if (value == 0f)
			{
				this.type = HorizontalExpandAppearance.ExpType.Middle;
				return;
			}
			if (value != 1f)
			{
				Debug.LogError(string.Format("Text Animator: you set an '{0}' modifier with value '{1}' for the HorizontalExpandAppearance effect, but it can only be '-1', '0', or '1'", modifier.name, modifier.value));
				return;
			}
			this.type = HorizontalExpandAppearance.ExpType.Right;
		}

		// Token: 0x0400002A RID: 42
		public HorizontalExpandAppearance.ExpType type;

		// Token: 0x0400002B RID: 43
		private Vector2 startTop;

		// Token: 0x0400002C RID: 44
		private Vector2 startBot;

		// Token: 0x0400002D RID: 45
		private float pct;

		// Token: 0x02000055 RID: 85
		public enum ExpType
		{
			// Token: 0x0400012A RID: 298
			Left,
			// Token: 0x0400012B RID: 299
			Middle,
			// Token: 0x0400012C RID: 300
			Right
		}
	}
}
