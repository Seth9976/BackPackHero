using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000175 RID: 373
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0002BA30 File Offset: 0x00029C30
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002BA39 File Offset: 0x00029C39
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = global::Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x040006B5 RID: 1717
		private bool isReplaced;
	}
}
