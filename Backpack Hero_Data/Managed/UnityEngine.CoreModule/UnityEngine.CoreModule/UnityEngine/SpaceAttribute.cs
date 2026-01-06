using System;

namespace UnityEngine
{
	// Token: 0x020001D8 RID: 472
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x060015CE RID: 5582 RVA: 0x00022EEC File Offset: 0x000210EC
		public SpaceAttribute()
		{
			this.height = 8f;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00022F01 File Offset: 0x00021101
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x040007AC RID: 1964
		public readonly float height;
	}
}
