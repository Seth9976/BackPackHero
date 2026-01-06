using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000016 RID: 22
	[Preserve]
	[CreateAssetMenu(fileName = "Fade Behavior", menuName = "Text Animator/Animations/Behaviors/Fade")]
	[EffectInfo("fade", EffectCategory.Behaviors)]
	public sealed class FadeBehavior : BehaviorScriptableBase
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000317F File Offset: 0x0000137F
		public override void ResetContext(TAnimCore animator)
		{
			this.delay = this.baseDelay;
			this.SetTimeToShow(this.baseSpeed);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003199 File Offset: 0x00001399
		private void SetTimeToShow(float speed)
		{
			this.timeToShow = 1f / speed;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000031A8 File Offset: 0x000013A8
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.SetTimeToShow(this.baseSpeed * modifier.value);
				return;
			}
			if (!(name == "d"))
			{
				return;
			}
			this.delay = this.baseDelay * modifier.value;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003200 File Offset: 0x00001400
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			if (character.passedTime <= this.delay)
			{
				return;
			}
			float num = (character.passedTime - this.delay) / this.timeToShow;
			if (num > 1f)
			{
				num = 1f;
			}
			if (num < 1f && num >= 0f)
			{
				for (int i = 0; i < 4; i++)
				{
					this.temp = character.current.colors[i];
					this.temp.a = 0;
					character.current.colors[i] = Color32.LerpUnclamped(character.current.colors[i], this.temp, Tween.EaseInOut(num));
				}
				return;
			}
			for (int j = 0; j < 4; j++)
			{
				this.temp = character.current.colors[j];
				this.temp.a = 0;
				character.current.colors[j] = this.temp;
			}
		}

		// Token: 0x04000044 RID: 68
		private Color32 temp;

		// Token: 0x04000045 RID: 69
		public float baseSpeed = 0.5f;

		// Token: 0x04000046 RID: 70
		public float baseDelay = 1f;

		// Token: 0x04000047 RID: 71
		private float delay;

		// Token: 0x04000048 RID: 72
		private float timeToShow;
	}
}
