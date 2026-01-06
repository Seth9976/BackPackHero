using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class InspectableIfAttribute : Attribute, IInspectableAttribute
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public InspectableIfAttribute(string conditionMember)
		{
			this.conditionMember = conditionMember;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004CE7 File Offset: 0x00002EE7
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00004CEF File Offset: 0x00002EEF
		public int order { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public string conditionMember { get; }
	}
}
