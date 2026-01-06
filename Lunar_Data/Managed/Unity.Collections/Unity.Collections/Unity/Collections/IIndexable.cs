using System;

namespace Unity.Collections
{
	// Token: 0x020000B0 RID: 176
	public interface IIndexable<T> where T : struct
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006BD RID: 1725
		// (set) Token: 0x060006BE RID: 1726
		int Length { get; set; }

		// Token: 0x060006BF RID: 1727
		ref T ElementAt(int index);
	}
}
