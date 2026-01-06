using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000BF RID: 191
	public static class GenericPool<T> where T : class, IPoolable
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public static T New(Func<T> constructor)
		{
			object obj = GenericPool<T>.@lock;
			T t2;
			lock (obj)
			{
				if (GenericPool<T>.free.Count == 0)
				{
					GenericPool<T>.free.Push(constructor());
				}
				T t = GenericPool<T>.free.Pop();
				t.New();
				GenericPool<T>.busy.Add(t);
				t2 = t;
			}
			return t2;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000A81C File Offset: 0x00008A1C
		public static void Free(T item)
		{
			object obj = GenericPool<T>.@lock;
			lock (obj)
			{
				if (!GenericPool<T>.busy.Remove(item))
				{
					throw new ArgumentException("The item to free is not in use by the pool.", "item");
				}
				item.Free();
				GenericPool<T>.free.Push(item);
			}
		}

		// Token: 0x04000108 RID: 264
		private static readonly object @lock = new object();

		// Token: 0x04000109 RID: 265
		private static readonly Stack<T> free = new Stack<T>();

		// Token: 0x0400010A RID: 266
		private static readonly HashSet<T> busy = new HashSet<T>(ReferenceEqualityComparer<T>.Instance);
	}
}
