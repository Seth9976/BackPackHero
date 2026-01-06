using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class WarnBeforeEditingAttribute : Attribute
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00004EE4 File Offset: 0x000030E4
		public WarnBeforeEditingAttribute(string warningTitle, string warningMessage)
		{
			this.warningTitle = warningTitle;
			this.warningMessage = warningMessage;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004EFA File Offset: 0x000030FA
		public WarnBeforeEditingAttribute(string warningTitle, string warningMessage, params object[] emptyValues)
			: this(warningTitle, warningMessage)
		{
			this.emptyValues = emptyValues;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004F0B File Offset: 0x0000310B
		public string warningTitle { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00004F13 File Offset: 0x00003113
		public string warningMessage { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00004F1B File Offset: 0x0000311B
		public object[] emptyValues { get; }
	}
}
