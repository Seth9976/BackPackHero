using System;
using Febucci.UI.Core;

namespace Febucci.UI.Effects
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public abstract class AppearanceScriptableBase : AnimationScriptableBase
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002F7A File Offset: 0x0000117A
		public override void ResetContext(TAnimCore animator)
		{
			this.duration = this.baseDuration;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F88 File Offset: 0x00001188
		public override float GetMaxDuration()
		{
			return this.duration;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F90 File Offset: 0x00001190
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "d")
			{
				this.duration = this.baseDuration * modifier.value;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FB7 File Offset: 0x000011B7
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return character.passedTime <= this.duration;
		}

		// Token: 0x0400003E RID: 62
		public float baseDuration = 0.5f;

		// Token: 0x0400003F RID: 63
		protected float duration;
	}
}
