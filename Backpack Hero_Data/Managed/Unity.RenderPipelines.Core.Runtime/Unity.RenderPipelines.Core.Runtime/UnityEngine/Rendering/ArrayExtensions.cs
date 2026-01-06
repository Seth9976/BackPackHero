using System;
using Unity.Collections;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009A RID: 154
	public static class ArrayExtensions
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00017C50 File Offset: 0x00015E50
		public static void ResizeArray<T>(this NativeArray<T> array, int capacity) where T : struct
		{
			NativeArray<T> nativeArray = new NativeArray<T>(capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			if (array.IsCreated)
			{
				NativeArray<T>.Copy(array, nativeArray, array.Length);
				array.Dispose();
			}
			array = nativeArray;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017C90 File Offset: 0x00015E90
		public static void ResizeArray(this TransformAccessArray array, int capacity)
		{
			TransformAccessArray transformAccessArray = new TransformAccessArray(capacity, -1);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					transformAccessArray.Add(array[i]);
				}
				array.Dispose();
			}
			array = transformAccessArray;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017CDA File Offset: 0x00015EDA
		public static void ResizeArray<T>(ref T[] array, int capacity)
		{
			if (array == null)
			{
				array = new T[capacity];
				return;
			}
			Array.Resize<T>(ref array, capacity);
		}
	}
}
