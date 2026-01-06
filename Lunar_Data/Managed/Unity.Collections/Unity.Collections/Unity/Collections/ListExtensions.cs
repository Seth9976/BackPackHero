using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000A8 RID: 168
	public static class ListExtensions
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0001531C File Offset: 0x0001351C
		public static bool RemoveSwapBack<T>(this List<T> list, T value)
		{
			int num = list.IndexOf(value);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00015340 File Offset: 0x00013540
		public static bool RemoveSwapBack<T>(this List<T> list, Predicate<T> matcher)
		{
			int num = list.FindIndex(matcher);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00015364 File Offset: 0x00013564
		public static void RemoveAtSwapBack<T>(this List<T> list, int index)
		{
			int num = list.Count - 1;
			list[index] = list[num];
			list.RemoveAt(num);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00015390 File Offset: 0x00013590
		public static NativeList<T> ToNativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeList<T> nativeList = new NativeList<T>(list.Count, allocator);
			for (int i = 0; i < list.Count; i++)
			{
				nativeList.AddNoResize(list[i]);
			}
			return nativeList;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000153CC File Offset: 0x000135CC
		public static NativeArray<T> ToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(list.Count, allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < list.Count; i++)
			{
				nativeArray[i] = list[i];
			}
			return nativeArray;
		}
	}
}
