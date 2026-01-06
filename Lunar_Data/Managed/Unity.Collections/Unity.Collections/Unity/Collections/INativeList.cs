using System;

namespace Unity.Collections
{
	// Token: 0x020000B1 RID: 177
	public interface INativeList<T> : IIndexable<T> where T : struct
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006C0 RID: 1728
		// (set) Token: 0x060006C1 RID: 1729
		int Capacity { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006C2 RID: 1730
		bool IsEmpty { get; }

		// Token: 0x170000B6 RID: 182
		T this[int index] { get; set; }

		// Token: 0x060006C5 RID: 1733
		void Clear();
	}
}
