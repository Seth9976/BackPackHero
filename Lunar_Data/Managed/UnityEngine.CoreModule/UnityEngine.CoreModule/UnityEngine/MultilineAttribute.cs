using System;

namespace UnityEngine
{
	// Token: 0x020001DC RID: 476
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x00022F4C File Offset: 0x0002114C
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00022F5D File Offset: 0x0002115D
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x040007B1 RID: 1969
		public readonly int lines;
	}
}
