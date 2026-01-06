using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000C5 RID: 197
	public static class ManualPool<T> where T : class
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000AB5C File Offset: 0x00008D5C
		public static T New(Func<T> constructor)
		{
			object obj = ManualPool<T>.@lock;
			T t2;
			lock (obj)
			{
				if (ManualPool<T>.free.Count == 0)
				{
					ManualPool<T>.free.Push(constructor());
				}
				T t = ManualPool<T>.free.Pop();
				ManualPool<T>.busy.Add(t);
				t2 = t;
			}
			return t2;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public static void Free(T item)
		{
			object obj = ManualPool<T>.@lock;
			lock (obj)
			{
				if (!ManualPool<T>.busy.Contains(item))
				{
					throw new ArgumentException("The item to free is not in use by the pool.", "item");
				}
				ManualPool<T>.busy.Remove(item);
				ManualPool<T>.free.Push(item);
			}
		}

		// Token: 0x04000111 RID: 273
		private static readonly object @lock = new object();

		// Token: 0x04000112 RID: 274
		private static readonly Stack<T> free = new Stack<T>();

		// Token: 0x04000113 RID: 275
		private static readonly HashSet<T> busy = new HashSet<T>();
	}
}
