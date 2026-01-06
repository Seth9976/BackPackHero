using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000265 RID: 613
	public static class ObjectPoolSimple<T> where T : class, new()
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x0005A928 File Offset: 0x00058B28
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

		// Token: 0x06000E85 RID: 3717 RVA: 0x0005A9B4 File Offset: 0x00058BB4
		public static void Release(ref T obj)
		{
			List<T> list = ObjectPoolSimple<T>.pool;
			lock (list)
			{
				ObjectPoolSimple<T>.pool.Add(obj);
			}
			obj = default(T);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0005AA04 File Offset: 0x00058C04
		public static void Clear()
		{
			List<T> list = ObjectPoolSimple<T>.pool;
			lock (list)
			{
				ObjectPoolSimple<T>.pool.Clear();
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0005AA48 File Offset: 0x00058C48
		public static int GetSize()
		{
			return ObjectPoolSimple<T>.pool.Count;
		}

		// Token: 0x04000AFA RID: 2810
		private static List<T> pool = new List<T>();

		// Token: 0x04000AFB RID: 2811
		private static readonly HashSet<T> inPool = new HashSet<T>();
	}
}
