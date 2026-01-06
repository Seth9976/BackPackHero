using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000266 RID: 614
	public static class StackPool<T>
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x0005AA78 File Offset: 0x00058C78
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

		// Token: 0x06000E8B RID: 3723 RVA: 0x0005AAF4 File Offset: 0x00058CF4
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

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005AB30 File Offset: 0x00058D30
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

		// Token: 0x06000E8D RID: 3725 RVA: 0x0005ABA8 File Offset: 0x00058DA8
		public static void Clear()
		{
			List<Stack<T>> list = StackPool<T>.pool;
			lock (list)
			{
				StackPool<T>.pool.Clear();
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0005ABEC File Offset: 0x00058DEC
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x04000AFC RID: 2812
		private static readonly List<Stack<T>> pool = new List<Stack<T>>();
	}
}
