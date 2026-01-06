using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorWideAttribute : Attribute
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00004E40 File Offset: 0x00003040
		public InspectorWideAttribute()
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004E48 File Offset: 0x00003048
		public InspectorWideAttribute(bool toEdge)
		{
			this.toEdge = toEdge;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004E57 File Offset: 0x00003057
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00004E5F File Offset: 0x0000305F
		public bool toEdge { get; private set; }
	}
}
