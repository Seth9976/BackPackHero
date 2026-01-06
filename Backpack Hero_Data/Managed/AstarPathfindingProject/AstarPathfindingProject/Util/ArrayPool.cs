using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000BA RID: 186
	public static class ArrayPool<T>
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x000365F8 File Offset: 0x000347F8
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

		// Token: 0x06000828 RID: 2088 RVA: 0x000366A4 File Offset: 0x000348A4
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

		// Token: 0x06000829 RID: 2089 RVA: 0x00036724 File Offset: 0x00034924
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

		// Token: 0x040004C8 RID: 1224
		private const int MaximumExactArrayLength = 256;

		// Token: 0x040004C9 RID: 1225
		private static readonly Stack<T[]>[] pool = new Stack<T[]>[31];

		// Token: 0x040004CA RID: 1226
		private static readonly Stack<T[]>[] exactPool = new Stack<T[]>[257];
	}
}
