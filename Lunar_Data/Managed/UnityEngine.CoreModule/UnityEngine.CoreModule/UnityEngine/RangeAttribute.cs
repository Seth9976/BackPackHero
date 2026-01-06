using System;

namespace UnityEngine
{
	// Token: 0x020001DA RID: 474
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x060015D1 RID: 5585 RVA: 0x00022F23 File Offset: 0x00021123
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040007AE RID: 1966
		public readonly float min;

		// Token: 0x040007AF RID: 1967
		public readonly float max;
	}
}
