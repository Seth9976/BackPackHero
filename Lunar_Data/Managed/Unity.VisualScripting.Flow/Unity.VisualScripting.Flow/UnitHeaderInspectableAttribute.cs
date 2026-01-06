using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class UnitHeaderInspectableAttribute : Attribute
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000281E File Offset: 0x00000A1E
		public UnitHeaderInspectableAttribute()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002826 File Offset: 0x00000A26
		public UnitHeaderInspectableAttribute(string label)
		{
			this.label = label;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002835 File Offset: 0x00000A35
		public string label { get; }
	}
}
