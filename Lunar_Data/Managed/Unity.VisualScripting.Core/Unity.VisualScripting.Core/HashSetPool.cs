using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000C0 RID: 192
	public static class HashSetPool<T>
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public static HashSet<T> New()
		{
			object obj = HashSetPool<T>.@lock;
			HashSet<T> hashSet2;
			lock (obj)
			{
				if (HashSetPool<T>.free.Count == 0)
				{
					HashSetPool<T>.free.Push(new HashSet<T>());
				}
				HashSet<T> hashSet = HashSetPool<T>.free.Pop();
				HashSetPool<T>.busy.Add(hashSet);
				hashSet2 = hashSet;
			}
			return hashSet2;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000A920 File Offset: 0x00008B20
		public static void Free(HashSet<T> hashSet)
		{
			object obj = HashSetPool<T>.@lock;
			lock (obj)
			{
				if (!HashSetPool<T>.busy.Remove(hashSet))
				{
					throw new ArgumentException("The hash set to free is not in use by the pool.", "hashSet");
				}
				hashSet.Clear();
				HashSetPool<T>.free.Push(hashSet);
			}
		}

		// Token: 0x0400010B RID: 267
		private static readonly object @lock = new object();

		// Token: 0x0400010C RID: 268
		private static readonly Stack<HashSet<T>> free = new Stack<HashSet<T>>();

		// Token: 0x0400010D RID: 269
		private static readonly HashSet<HashSet<T>> busy = new HashSet<HashSet<T>>();
	}
}
