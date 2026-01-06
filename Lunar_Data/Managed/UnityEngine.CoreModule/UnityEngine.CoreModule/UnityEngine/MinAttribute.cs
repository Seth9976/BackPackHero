using System;

namespace UnityEngine
{
	// Token: 0x020001DB RID: 475
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x060015D2 RID: 5586 RVA: 0x00022F3B File Offset: 0x0002113B
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x040007B0 RID: 1968
		public readonly float min;
	}
}
