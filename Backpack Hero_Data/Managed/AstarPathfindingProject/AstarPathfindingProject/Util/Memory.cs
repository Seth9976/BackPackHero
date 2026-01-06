using System;

namespace Pathfinding.Util
{
	// Token: 0x020000CA RID: 202
	public static class Memory
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0003A63C File Offset: 0x0003883C
		public static void MemSet<T>(T[] array, T value, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, num2 - i) * byteSize);
				i += num;
				num *= 2;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0003A6A0 File Offset: 0x000388A0
		public static void MemSet<T>(T[] array, T value, int totalSize, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, totalSize);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			while (i < totalSize)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, totalSize - i) * byteSize);
				i += num;
				num *= 2;
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003A700 File Offset: 0x00038900
		public static T[] ShrinkArray<T>(T[] arr, int newLength)
		{
			newLength = Math.Min(newLength, arr.Length);
			T[] array = new T[newLength];
			Array.Copy(arr, array, newLength);
			return array;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003A728 File Offset: 0x00038928
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}
	}
}
