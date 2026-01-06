using System;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	[AttributeUsage(64)]
	public class BeforeRenderOrderAttribute : Attribute
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0000BACB File Offset: 0x00009CCB
		public int order { get; private set; }

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public BeforeRenderOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
