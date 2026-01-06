using System;

namespace UnityEngine
{
	// Token: 0x020001D4 RID: 468
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00022EA1 File Offset: 0x000210A1
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00022EA9 File Offset: 0x000210A9
		public int order { get; set; }
	}
}
