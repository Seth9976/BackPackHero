using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000C3 RID: 195
	public static class ListPool<T>
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000AA00 File Offset: 0x00008C00
		public static List<T> New()
		{
			object obj = ListPool<T>.@lock;
			List<T> list2;
			lock (obj)
			{
				if (ListPool<T>.free.Count == 0)
				{
					ListPool<T>.free.Push(new List<T>());
				}
				List<T> list = ListPool<T>.free.Pop();
				ListPool<T>.busy.Add(list);
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000AA70 File Offset: 0x00008C70
		public static void Free(List<T> list)
		{
			object obj = ListPool<T>.@lock;
			lock (obj)
			{
				if (!ListPool<T>.busy.Contains(list))
				{
					throw new ArgumentException("The list to free is not in use by the pool.", "list");
				}
				list.Clear();
				ListPool<T>.busy.Remove(list);
				ListPool<T>.free.Push(list);
			}
		}

		// Token: 0x0400010E RID: 270
		private static readonly object @lock = new object();

		// Token: 0x0400010F RID: 271
		private static readonly Stack<List<T>> free = new Stack<List<T>>();

		// Token: 0x04000110 RID: 272
		private static readonly HashSet<List<T>> busy = new HashSet<List<T>>();
	}
}
