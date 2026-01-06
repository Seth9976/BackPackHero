using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000005 RID: 5
	internal static class NativeArrayHelpers
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024BC File Offset: 0x000006BC
		public static void ResizeIfNeeded<T>(ref NativeArray<T> nativeArray, int size, Allocator allocator = Allocator.Persistent) where T : struct
		{
			bool flag = nativeArray.IsCreated;
			if (flag && nativeArray.Length != size)
			{
				nativeArray.Dispose();
				flag = false;
			}
			if (!flag)
			{
				nativeArray = new NativeArray<T>(size, allocator, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F8 File Offset: 0x000006F8
		public static void ResizeAndCopyIfNeeded<T>(ref NativeArray<T> nativeArray, int size, Allocator allocator = Allocator.Persistent) where T : struct
		{
			bool isCreated = nativeArray.IsCreated;
			if (isCreated && nativeArray.Length == size)
			{
				return;
			}
			NativeArray<T> nativeArray2 = new NativeArray<T>(size, allocator, NativeArrayOptions.ClearMemory);
			if (isCreated)
			{
				NativeArray<T>.Copy(nativeArray, nativeArray2, (size < nativeArray.Length) ? size : nativeArray.Length);
				nativeArray.Dispose();
			}
			nativeArray = nativeArray2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002551 File Offset: 0x00000751
		public static void DisposeIfCreated<T>(this NativeArray<T> nativeArray) where T : struct
		{
			if (nativeArray.IsCreated)
			{
				nativeArray.Dispose();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002564 File Offset: 0x00000764
		[WriteAccessRequired]
		public unsafe static void CopyFromNativeSlice<T, S>(this NativeArray<T> nativeArray, int dstStartIndex, int dstEndIndex, NativeSlice<S> slice, int srcStartIndex, int srcEndIndex) where T : struct where S : struct
		{
			if (dstEndIndex - dstStartIndex != srcEndIndex - srcStartIndex)
			{
				throw new ArgumentException("Destination and Source copy counts must match.", "slice");
			}
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)slice.GetUnsafeReadOnlyPtr<S>();
			ptr += srcStartIndex * num2;
			byte* ptr2 = (byte*)nativeArray.GetUnsafePtr<T>();
			ptr2 += dstStartIndex * num;
			UnsafeUtility.MemCpyStride((void*)ptr2, num2, (void*)ptr, slice.Stride, num, srcEndIndex - srcStartIndex);
		}
	}
}
