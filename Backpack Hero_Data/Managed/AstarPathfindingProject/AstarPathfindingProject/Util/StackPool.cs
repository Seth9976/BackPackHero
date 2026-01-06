using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000C3 RID: 195
	public static class StackPool<T>
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x00037CA8 File Offset: 0x00035EA8
		public static Stack<T> Claim()
		{
			List<Stack<T>> list = StackPool<T>.pool;
			lock (list)
			{
				if (StackPool<T>.pool.Count > 0)
				{
					Stack<T> stack = StackPool<T>.pool[StackPool<T>.pool.Count - 1];
					StackPool<T>.pool.RemoveAt(StackPool<T>.pool.Count - 1);
					return stack;
				}
			}
			return new Stack<T>();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00037D24 File Offset: 0x00035F24
		public static void Warmup(int count)
		{
			Stack<T>[] array = new Stack<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = StackPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				StackPool<T>.Release(array[j]);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00037D60 File Offset: 0x00035F60
		public static void Release(Stack<T> stack)
		{
			stack.Clear();
			List<Stack<T>> list = StackPool<T>.pool;
			lock (list)
			{
				for (int i = 0; i < StackPool<T>.pool.Count; i++)
				{
					if (StackPool<T>.pool[i] == stack)
					{
						Debug.LogError("The Stack is released even though it is inside the pool");
					}
				}
				StackPool<T>.pool.Add(stack);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00037DD8 File Offset: 0x00035FD8
		public static void Clear()
		{
			List<Stack<T>> list = StackPool<T>.pool;
			lock (list)
			{
				StackPool<T>.pool.Clear();
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00037E1C File Offset: 0x0003601C
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x040004DD RID: 1245
		private static readonly List<Stack<T>> pool = new List<Stack<T>>();
	}
}
