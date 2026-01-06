using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000C1 RID: 193
	public static class XHashSetPool
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public static HashSet<T> ToHashSetPooled<T>(this IEnumerable<T> source)
		{
			HashSet<T> hashSet = HashSetPool<T>.New();
			foreach (T t in source)
			{
				hashSet.Add(t);
			}
			return hashSet;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public static void Free<T>(this HashSet<T> hashSet)
		{
			HashSetPool<T>.Free(hashSet);
		}
	}
}
