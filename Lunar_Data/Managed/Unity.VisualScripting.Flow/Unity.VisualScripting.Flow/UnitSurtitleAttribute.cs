using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class UnitSurtitleAttribute : Attribute
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000289D File Offset: 0x00000A9D
		public UnitSurtitleAttribute(string surtitle)
		{
			this.surtitle = surtitle;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000028AC File Offset: 0x00000AAC
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000028B4 File Offset: 0x00000AB4
		public string surtitle { get; private set; }
	}
}
