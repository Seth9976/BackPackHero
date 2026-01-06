using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000262 RID: 610
	public static class ListPool<T>
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0005A5EC File Offset: 0x000587EC
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

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005A678 File Offset: 0x00058878
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

		// Token: 0x06000E7A RID: 3706 RVA: 0x0005A6E8 File Offset: 0x000588E8
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

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005A7A4 File Offset: 0x000589A4
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

		// Token: 0x06000E7C RID: 3708 RVA: 0x0005A814 File Offset: 0x00058A14
		public static void Release(ref List<T> list)
		{
			ListPool<T>.Release(list);
			list = null;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0005A820 File Offset: 0x00058A20
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

		// Token: 0x06000E7E RID: 3710 RVA: 0x0005A89C File Offset: 0x00058A9C
		public static void Clear()
		{
			List<List<T>> list = ListPool<T>.pool;
			lock (list)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005A8E0 File Offset: 0x00058AE0
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x04000AF4 RID: 2804
		private static readonly List<List<T>> pool = new List<List<T>>();

		// Token: 0x04000AF5 RID: 2805
		private static readonly List<List<T>> largePool = new List<List<T>>();

		// Token: 0x04000AF6 RID: 2806
		private static readonly HashSet<List<T>> inPool = new HashSet<List<T>>();

		// Token: 0x04000AF7 RID: 2807
		private const int MaxCapacitySearchLength = 8;

		// Token: 0x04000AF8 RID: 2808
		private const int LargeThreshold = 5000;

		// Token: 0x04000AF9 RID: 2809
		private const int MaxLargePoolSize = 8;
	}
}
