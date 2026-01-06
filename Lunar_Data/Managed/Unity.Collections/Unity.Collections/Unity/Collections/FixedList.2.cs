using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000053 RID: 83
	[BurstCompatible]
	internal struct FixedList
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000525E File Offset: 0x0000345E
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		internal static int PaddingBytes<T>() where T : struct
		{
			return math.max(0, math.min(6, (1 << math.tzcnt(UnsafeUtility.SizeOf<T>())) - 2));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000527D File Offset: 0x0000347D
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int StorageBytes<BUFFER, T>() where BUFFER : struct where T : struct
		{
			return UnsafeUtility.SizeOf<BUFFER>() - FixedList.PaddingBytes<T>();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000528A File Offset: 0x0000348A
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int Capacity<BUFFER, T>() where BUFFER : struct where T : struct
		{
			return FixedList.StorageBytes<BUFFER, T>() / UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005298 File Offset: 0x00003498
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckResize<BUFFER, T>(int newLength) where BUFFER : struct where T : struct
		{
			int num = FixedList.Capacity<BUFFER, T>();
			if (newLength < 0 || newLength > num)
			{
				throw new IndexOutOfRangeException(string.Format("NewLength {0} is out of range of '{1}' Capacity.", newLength, num));
			}
		}
	}
}
