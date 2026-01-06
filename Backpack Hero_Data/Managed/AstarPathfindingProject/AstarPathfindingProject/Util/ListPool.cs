using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000BD RID: 189
	public static class ListPool<T>
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00036CEC File Offset: 0x00034EEC
		public static List<T> Claim()
		{
			List<List<T>> list = ListPool<T>.pool;
			List<T> list3;
			lock (list)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list2 = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					ListPool<T>.inPool.Remove(list2);
					list3 = list2;
				}
				else
				{
					list3 = new List<T>();
				}
			}
			return list3;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00036D78 File Offset: 0x00034F78
		private static int FindCandidate(List<List<T>> pool, int capacity)
		{
			List<T> list = null;
			int num = -1;
			int num2 = 0;
			while (num2 < pool.Count && num2 < 8)
			{
				List<T> list2 = pool[pool.Count - 1 - num2];
				if ((list == null || list2.Capacity > list.Capacity) && list2.Capacity < capacity * 16)
				{
					list = list2;
					num = pool.Count - 1 - num2;
					if (list.Capacity >= capacity)
					{
						return num;
					}
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00036DE8 File Offset: 0x00034FE8
		public static List<T> Claim(int capacity)
		{
			List<List<T>> list = ListPool<T>.pool;
			List<T> list3;
			lock (list)
			{
				List<List<T>> list2 = ListPool<T>.pool;
				int num = ListPool<T>.FindCandidate(ListPool<T>.pool, capacity);
				if (capacity > 5000)
				{
					int num2 = ListPool<T>.FindCandidate(ListPool<T>.largePool, capacity);
					if (num2 != -1)
					{
						list2 = ListPool<T>.largePool;
						num = num2;
					}
				}
				if (num == -1)
				{
					list3 = new List<T>(capacity);
				}
				else
				{
					List<T> list4 = list2[num];
					ListPool<T>.inPool.Remove(list4);
					list2[num] = list2[list2.Count - 1];
					list2.RemoveAt(list2.Count - 1);
					list3 = list4;
				}
			}
			return list3;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00036EA4 File Offset: 0x000350A4
		public static void Warmup(int count, int size)
		{
			List<List<T>> list = ListPool<T>.pool;
			lock (list)
			{
				List<T>[] array = new List<T>[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = ListPool<T>.Claim(size);
				}
				for (int j = 0; j < count; j++)
				{
					ListPool<T>.Release(array[j]);
				}
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00036F14 File Offset: 0x00035114
		public static void Release(ref List<T> list)
		{
			ListPool<T>.Release(list);
			list = null;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00036F20 File Offset: 0x00035120
		public static void Release(List<T> list)
		{
			list.ClearFast<T>();
			List<List<T>> list2 = ListPool<T>.pool;
			lock (list2)
			{
				if (list.Capacity > 5000)
				{
					ListPool<T>.largePool.Add(list);
					if (ListPool<T>.largePool.Count > 8)
					{
						ListPool<T>.largePool.RemoveAt(0);
					}
				}
				else
				{
					ListPool<T>.pool.Add(list);
				}
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00036F9C File Offset: 0x0003519C
		public static void Clear()
		{
			List<List<T>> list = ListPool<T>.pool;
			lock (list)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00036FE0 File Offset: 0x000351E0
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x040004CF RID: 1231
		private static readonly List<List<T>> pool = new List<List<T>>();

		// Token: 0x040004D0 RID: 1232
		private static readonly List<List<T>> largePool = new List<List<T>>();

		// Token: 0x040004D1 RID: 1233
		private static readonly HashSet<List<T>> inPool = new HashSet<List<T>>();

		// Token: 0x040004D2 RID: 1234
		private const int MaxCapacitySearchLength = 8;

		// Token: 0x040004D3 RID: 1235
		private const int LargeThreshold = 5000;

		// Token: 0x040004D4 RID: 1236
		private const int MaxLargePoolSize = 8;
	}
}
