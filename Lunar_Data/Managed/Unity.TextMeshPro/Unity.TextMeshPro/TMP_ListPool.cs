using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000043 RID: 67
	internal static class TMP_ListPool<T>
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00022AE5 File Offset: 0x00020CE5
		public static List<T> Get()
		{
			return TMP_ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00022AF1 File Offset: 0x00020CF1
		public static void Release(List<T> toRelease)
		{
			TMP_ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x04000273 RID: 627
		private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
