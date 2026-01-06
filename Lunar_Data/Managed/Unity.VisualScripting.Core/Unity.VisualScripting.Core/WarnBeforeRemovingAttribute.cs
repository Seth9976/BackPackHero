using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class WarnBeforeRemovingAttribute : Attribute
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00004F23 File Offset: 0x00003123
		public WarnBeforeRemovingAttribute(string warningTitle, string warningMessage)
		{
			this.warningTitle = warningTitle;
			this.warningMessage = warningMessage;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00004F39 File Offset: 0x00003139
		public string warningTitle { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004F41 File Offset: 0x00003141
		public string warningMessage { get; }
	}
}
