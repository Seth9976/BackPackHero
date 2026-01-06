using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200000C RID: 12
	[Preserve]
	[CreateAssetMenu(fileName = "Fade Appearance", menuName = "Text Animator/Animations/Appearances/Fade")]
	[EffectInfo("fade", EffectCategory.Appearances)]
	public sealed class FadeAppearance : AppearanceScriptableBase
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000026F8 File Offset: 0x000008F8
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			for (int i = 0; i < 4; i++)
			{
				this.temp = character.current.colors[i];
				this.temp.a = 0;
				character.current.colors[i] = Color32.LerpUnclamped(character.current.colors[i], this.temp, Tween.EaseInOut(1f - character.passedTime / this.duration));
			}
		}

		// Token: 0x04000029 RID: 41
		private Color32 temp;
	}
}
