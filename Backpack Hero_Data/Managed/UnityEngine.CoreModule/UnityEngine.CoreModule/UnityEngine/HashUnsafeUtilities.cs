using System;

namespace UnityEngine
{
	// Token: 0x020001AF RID: 431
	public static class HashUnsafeUtilities
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x00019FAB File Offset: 0x000181AB
		public unsafe static void ComputeHash128(void* data, ulong dataSize, ulong* hash1, ulong* hash2)
		{
			SpookyHash.Hash(data, dataSize, hash1, hash2);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00019FB8 File Offset: 0x000181B8
		public unsafe static void ComputeHash128(void* data, ulong dataSize, Hash128* hash)
		{
			ulong u64_ = hash->u64_0;
			ulong u64_2 = hash->u64_1;
			HashUnsafeUtilities.ComputeHash128(data, dataSize, &u64_, &u64_2);
			*hash = new Hash128(u64_, u64_2);
		}
	}
}
