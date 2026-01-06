using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000103 RID: 259
	internal static class UnsafePtrListExtensions
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		public static ref UnsafeList ListData(this UnsafePtrList from)
		{
			return UnsafeUtility.As<UnsafePtrList, UnsafeList>(ref from);
		}
	}
}
