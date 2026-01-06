using System;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000022 RID: 34
	[Preserve]
	[CreateAssetMenu(fileName = "Composite Animation", menuName = "Text Animator/Animations/Special/Composite")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class CompositeAnimation : AnimationScriptableBase
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003C40 File Offset: 0x00001E40
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.ValidateArray();
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].InitializeOnce();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C78 File Offset: 0x00001E78
		public override void ResetContext(TAnimCore animator)
		{
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetContext(animator);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public override void SetModifier(ModifierInfo modifier)
		{
			base.SetModifier(modifier);
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetModifier(modifier);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				if (animationScriptableBase.CanApplyEffectTo(character, animator))
				{
					animationScriptableBase.ApplyEffectTo(ref character, animator);
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D15 File Offset: 0x00001F15
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003D18 File Offset: 0x00001F18
		public override float GetMaxDuration()
		{
			float num = -1f;
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				num = Mathf.Max(num, animationScriptableBase.GetMaxDuration());
			}
			return num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003D54 File Offset: 0x00001F54
		private void ValidateArray()
		{
			List<AnimationScriptableBase> list = new List<AnimationScriptableBase>();
			for (int i = 0; i < this.animations.Length; i++)
			{
				if (this.animations[i] != this)
				{
					list.Add(this.animations[i]);
				}
			}
			this.animations = list.ToArray();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003DA4 File Offset: 0x00001FA4
		private void OnValidate()
		{
			this.ValidateArray();
		}

		// Token: 0x0400006D RID: 109
		public AnimationScriptableBase[] animations = new AnimationScriptableBase[0];
	}
}
