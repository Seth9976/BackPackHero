using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000C4 RID: 196
	public static class XListPool
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000AB04 File Offset: 0x00008D04
		public static List<T> ToListPooled<T>(this IEnumerable<T> source)
		{
			List<T> list = ListPool<T>.New();
			foreach (T t in source)
			{
				list.Add(t);
			}
			return list;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000AB54 File Offset: 0x00008D54
		public static void Free<T>(this List<T> list)
		{
			ListPool<T>.Free(list);
		}
	}
}
