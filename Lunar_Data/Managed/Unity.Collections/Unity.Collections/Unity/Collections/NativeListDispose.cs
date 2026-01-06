using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000B4 RID: 180
	[NativeContainer]
	[BurstCompatible]
	internal struct NativeListDispose
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0001604C File Offset: 0x0001424C
		public unsafe void Dispose()
		{
			UnsafeList<int>* listData = (UnsafeList<int>*)this.m_ListData;
			UnsafeList<int>.Destroy(listData);
		}

		// Token: 0x04000282 RID: 642
		[NativeDisableUnsafePtrRestriction]
		public unsafe UntypedUnsafeList* m_ListData;
	}
}
