using System;

namespace UnityEngine
{
	// Token: 0x020001F0 RID: 496
	[AttributeUsage(4, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00023C38 File Offset: 0x00021E38
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00023C40 File Offset: 0x00021E40
		public string menuName { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00023C49 File Offset: 0x00021E49
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00023C51 File Offset: 0x00021E51
		public string fileName { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00023C5A File Offset: 0x00021E5A
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x00023C62 File Offset: 0x00021E62
		public int order { get; set; }
	}
}
