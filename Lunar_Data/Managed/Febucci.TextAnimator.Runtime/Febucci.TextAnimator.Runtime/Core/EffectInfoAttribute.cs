using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Class)]
	public class EffectInfoAttribute : TagInfoAttribute
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00006EC8 File Offset: 0x000050C8
		public EffectInfoAttribute(string tagID, EffectCategory category)
			: base(tagID)
		{
			this.category = category;
		}

		// Token: 0x04000104 RID: 260
		public readonly EffectCategory category;
	}
}
