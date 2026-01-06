using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000BC RID: 188
	public static class ArrayPool<T>
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0000A494 File Offset: 0x00008694
		public static T[] New(int length)
		{
			object obj = ArrayPool<T>.@lock;
			T[] array2;
			lock (obj)
			{
				if (!ArrayPool<T>.free.ContainsKey(length))
				{
					ArrayPool<T>.free.Add(length, new Stack<T[]>());
				}
				if (ArrayPool<T>.free[length].Count == 0)
				{
					ArrayPool<T>.free[length].Push(new T[length]);
				}
				T[] array = ArrayPool<T>.free[length].Pop();
				ArrayPool<T>.busy.Add(array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000A534 File Offset: 0x00008734
		public static void Free(T[] array)
		{
			object obj = ArrayPool<T>.@lock;
			lock (obj)
			{
				if (!ArrayPool<T>.busy.Contains(array))
				{
					throw new ArgumentException("The array to free is not in use by the pool.", "array");
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = default(T);
				}
				ArrayPool<T>.busy.Remove(array);
				ArrayPool<T>.free[array.Length].Push(array);
			}
		}

		// Token: 0x04000102 RID: 258
		private static readonly object @lock = new object();

		// Token: 0x04000103 RID: 259
		private static readonly Dictionary<int, Stack<T[]>> free = new Dictionary<int, Stack<T[]>>();

		// Token: 0x04000104 RID: 260
		private static readonly HashSet<T[]> busy = new HashSet<T[]>();
	}
}
