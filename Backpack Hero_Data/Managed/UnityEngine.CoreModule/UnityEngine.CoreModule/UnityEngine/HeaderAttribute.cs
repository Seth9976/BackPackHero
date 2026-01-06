using System;

namespace UnityEngine
{
	// Token: 0x020001D9 RID: 473
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00022F12 File Offset: 0x00021112
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x040007AD RID: 1965
		public readonly string header;
	}
}
