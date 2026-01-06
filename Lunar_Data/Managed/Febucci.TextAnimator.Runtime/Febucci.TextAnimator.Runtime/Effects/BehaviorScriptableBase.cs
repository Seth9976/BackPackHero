using System;
using Febucci.UI.Core;

namespace Febucci.UI.Effects
{
	// Token: 0x02000020 RID: 32
	public abstract class BehaviorScriptableBase : AnimationScriptableBase
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003B66 File Offset: 0x00001D66
		public override float GetMaxDuration()
		{
			return -1f;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B6D File Offset: 0x00001D6D
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}
	}
}
