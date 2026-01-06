using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F6 RID: 502
	[UsedByNativeCode]
	[AttributeUsage(4)]
	public class DefaultExecutionOrder : Attribute
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x00023CF9 File Offset: 0x00021EF9
		public DefaultExecutionOrder(int order)
		{
			this.m_Order = order;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00023D0C File Offset: 0x00021F0C
		public int order
		{
			get
			{
				return this.m_Order;
			}
		}

		// Token: 0x040007D6 RID: 2006
		private int m_Order;
	}
}
