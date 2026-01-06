using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000C1 RID: 193
	public static class ObjectPoolSimple<T> where T : class, new()
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x000373D8 File Offset: 0x000355D8
		public static T Claim()
		{
			List<T> list = ObjectPoolSimple<T>.pool;
			T t2;
			lock (list)
			{
				if (ObjectPoolSimple<T>.pool.Count > 0)
				{
					T t = ObjectPoolSimple<T>.pool[ObjectPoolSimple<T>.pool.Count - 1];
					ObjectPoolSimple<T>.pool.RemoveAt(ObjectPoolSimple<T>.pool.Count - 1);
					ObjectPoolSimple<T>.inPool.Remove(t);
					t2 = t;
				}
				else
				{
					t2 = new T();
				}
			}
			return t2;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00037464 File Offset: 0x00035664
		public static void Release(ref T obj)
		{
			List<T> list = ObjectPoolSimple<T>.pool;
			lock (list)
			{
				ObjectPoolSimple<T>.pool.Add(obj);
			}
			obj = default(T);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000374B4 File Offset: 0x000356B4
		public static void Clear()
		{
			List<T> list = ObjectPoolSimple<T>.pool;
			lock (list)
			{
				ObjectPoolSimple<T>.pool.Clear();
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000374F8 File Offset: 0x000356F8
		public static int GetSize()
		{
			return ObjectPoolSimple<T>.pool.Count;
		}

		// Token: 0x040004D5 RID: 1237
		private static List<T> pool = new List<T>();

		// Token: 0x040004D6 RID: 1238
		private static readonly HashSet<T> inPool = new HashSet<T>();
	}
}
