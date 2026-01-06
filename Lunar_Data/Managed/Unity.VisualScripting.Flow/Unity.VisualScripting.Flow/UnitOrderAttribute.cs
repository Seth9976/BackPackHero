using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitOrderAttribute : Attribute
	{
		// Token: 0x06000060 RID: 96 RVA: 0x0000283D File Offset: 0x00000A3D
		public UnitOrderAttribute(int order)
		{
			this.order = order;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000284C File Offset: 0x00000A4C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002854 File Offset: 0x00000A54
		public int order { get; private set; }
	}
}
