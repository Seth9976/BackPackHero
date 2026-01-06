using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000260 RID: 608
	public static class ArrayPool<T>
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x0005A2F8 File Offset: 0x000584F8
		public static T[] Claim(int minimumLength)
		{
			if (minimumLength <= 0)
			{
				return ArrayPool<T>.ClaimWithExactLength(0);
			}
			int num = 0;
			while (1 << num < minimumLength && num < 30)
			{
				num++;
			}
			if (num == 30)
			{
				throw new ArgumentException("Too high minimum length");
			}
			Stack<T[]>[] array = ArrayPool<T>.pool;
			lock (array)
			{
				if (ArrayPool<T>.pool[num] == null)
				{
					ArrayPool<T>.pool[num] = new Stack<T[]>();
				}
				if (ArrayPool<T>.pool[num].Count > 0)
				{
					return ArrayPool<T>.pool[num].Pop();
				}
			}
			return new T[1 << num];
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0005A3A4 File Offset: 0x000585A4
		public static T[] ClaimWithExactLength(int length)
		{
			if (length != 0 && (length & (length - 1)) == 0)
			{
				return ArrayPool<T>.Claim(length);
			}
			if (length <= 256)
			{
				Stack<T[]>[] array = ArrayPool<T>.pool;
				lock (array)
				{
					Stack<T[]> stack = ArrayPool<T>.exactPool[length];
					if (stack != null && stack.Count > 0)
					{
						return stack.Pop();
					}
				}
			}
			return new T[length];
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0005A424 File Offset: 0x00058624
		public static void Release(ref T[] array, bool allowNonPowerOfTwo = false)
		{
			if (array == null)
			{
				return;
			}
			if (array.GetType() != typeof(T[]))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expected array type ",
					typeof(T[]).Name,
					" but found ",
					array.GetType().Name,
					"\nAre you using the correct generic class?\n"
				}));
			}
			bool flag = array.Length != 0 && (array.Length & (array.Length - 1)) == 0;
			if (!flag && !allowNonPowerOfTwo && array.Length != 0)
			{
				throw new ArgumentException("Length is not a power of 2");
			}
			Stack<T[]>[] array2 = ArrayPool<T>.pool;
			lock (array2)
			{
				if (flag)
				{
					int num = 0;
					while (1 << num < array.Length && num < 30)
					{
						num++;
					}
					if (ArrayPool<T>.pool[num] == null)
					{
						ArrayPool<T>.pool[num] = new Stack<T[]>();
					}
					ArrayPool<T>.pool[num].Push(array);
				}
				else if (array.Length <= 256)
				{
					Stack<T[]> stack = ArrayPool<T>.exactPool[array.Length];
					if (stack == null)
					{
						stack = (ArrayPool<T>.exactPool[array.Length] = new Stack<T[]>());
					}
					stack.Push(array);
				}
			}
			array = null;
		}

		// Token: 0x04000AF1 RID: 2801
		private const int MaximumExactArrayLength = 256;

		// Token: 0x04000AF2 RID: 2802
		private static readonly Stack<T[]>[] pool = new Stack<T[]>[31];

		// Token: 0x04000AF3 RID: 2803
		private static readonly Stack<T[]>[] exactPool = new Stack<T[]>[257];
	}
}
