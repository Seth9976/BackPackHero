using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A4 RID: 420
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class fsPropertyAttribute : Attribute
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0002E85C File Offset: 0x0002CA5C
		public fsPropertyAttribute()
			: this(string.Empty)
		{
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002E869 File Offset: 0x0002CA69
		public fsPropertyAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x04000292 RID: 658
		public string Name;

		// Token: 0x04000293 RID: 659
		public Type Converter;
	}
}
