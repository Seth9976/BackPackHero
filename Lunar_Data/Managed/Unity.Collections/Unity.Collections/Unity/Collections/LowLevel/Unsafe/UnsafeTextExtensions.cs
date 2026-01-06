using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000154 RID: 340
	internal static class UnsafeTextExtensions
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x000240E7 File Offset: 0x000222E7
		public static ref UnsafeList<byte> AsUnsafeListOfBytes(this UnsafeText text)
		{
			return UnsafeUtility.As<UntypedUnsafeList, UnsafeList<byte>>(ref text.m_UntypedListData);
		}
	}
}
