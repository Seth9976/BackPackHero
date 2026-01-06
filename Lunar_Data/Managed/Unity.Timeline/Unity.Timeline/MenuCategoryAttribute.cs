using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(AttributeTargets.Class)]
	internal class MenuCategoryAttribute : Attribute
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00009A3B File Offset: 0x00007C3B
		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}

		// Token: 0x040000F4 RID: 244
		public readonly string category;
	}
}
