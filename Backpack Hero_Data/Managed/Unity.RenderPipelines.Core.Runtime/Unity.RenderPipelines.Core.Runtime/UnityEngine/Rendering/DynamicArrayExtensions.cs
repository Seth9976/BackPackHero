using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004D RID: 77
	public static class DynamicArrayExtensions
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		private static int Partition<T>(T[] data, int left, int right) where T : IComparable<T>, new()
		{
			T t = data[left];
			left--;
			right++;
			for (;;)
			{
				T t2 = default(T);
				int num;
				do
				{
					left++;
					t2 = data[left];
					num = t2.CompareTo(t);
				}
				while (num < 0);
				T t3 = default(T);
				do
				{
					right--;
					t3 = data[right];
					num = t3.CompareTo(t);
				}
				while (num > 0);
				if (left >= right)
				{
					break;
				}
				data[right] = t2;
				data[left] = t3;
			}
			return right;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E034 File Offset: 0x0000C234
		private static void QuickSort<T>(T[] data, int left, int right) where T : IComparable<T>, new()
		{
			if (left < right)
			{
				int num = DynamicArrayExtensions.Partition<T>(data, left, right);
				if (num >= 1)
				{
					DynamicArrayExtensions.QuickSort<T>(data, left, num);
				}
				if (num + 1 < right)
				{
					DynamicArrayExtensions.QuickSort<T>(data, num + 1, right);
				}
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E06A File Offset: 0x0000C26A
		public static void QuickSort<T>(this DynamicArray<T> array) where T : IComparable<T>, new()
		{
			DynamicArrayExtensions.QuickSort<T>(array, 0, array.size - 1);
		}
	}
}
