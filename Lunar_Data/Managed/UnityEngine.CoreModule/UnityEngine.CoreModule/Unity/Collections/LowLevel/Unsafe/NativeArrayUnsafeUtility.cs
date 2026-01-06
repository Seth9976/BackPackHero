using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000AA RID: 170
	public static class NativeArrayUnsafeUtility
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000562C File Offset: 0x0000382C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckConvertArguments<T>(int length, Allocator allocator) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			NativeArray<T>.IsUnmanagedAndThrow();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00005658 File Offset: 0x00003858
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator
			};
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00005690 File Offset: 0x00003890
		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000056A8 File Offset: 0x000038A8
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000056C0 File Offset: 0x000038C0
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T>.ReadOnly nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000056D8 File Offset: 0x000038D8
		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
