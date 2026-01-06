using System;
using System.Runtime.CompilerServices;
using Pathfinding.Graphs.Grid;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016B RID: 363
	internal static class NativeArrayExtensions
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0003B8E4 File Offset: 0x00039AE4
		public static JobMemSet<T> MemSet<[IsUnmanaged] T>(this NativeArray<T> self, T value) where T : struct, ValueType
		{
			return new JobMemSet<T>
			{
				data = self,
				value = value
			};
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0003B90C File Offset: 0x00039B0C
		public static JobAND BitwiseAndWith(this NativeArray<bool> self, NativeArray<bool> other)
		{
			return new JobAND
			{
				result = self,
				data = other
			};
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0003B934 File Offset: 0x00039B34
		public static JobCopy<T> CopyToJob<T>(this NativeArray<T> from, NativeArray<T> to) where T : struct
		{
			return new JobCopy<T>
			{
				from = from,
				to = to
			};
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0003B95C File Offset: 0x00039B5C
		[MethodImpl(256)]
		public static SliceActionJob<T> WithSlice<T>(this T action, Slice3D slice) where T : struct, GridIterationUtilities.ISliceAction
		{
			return new SliceActionJob<T>
			{
				action = action,
				slice = slice
			};
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0003B984 File Offset: 0x00039B84
		[MethodImpl(256)]
		public static IndexActionJob<T> WithLength<T>(this T action, int length) where T : struct, GridIterationUtilities.ISliceAction
		{
			return new IndexActionJob<T>
			{
				action = action,
				length = length
			};
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0003B9AC File Offset: 0x00039BAC
		public static JobRotate3DArray<T> Rotate3D<[IsUnmanaged] T>(this NativeArray<T> arr, int3 size, int dx, int dz) where T : struct, ValueType
		{
			return new JobRotate3DArray<T>
			{
				arr = arr,
				size = size,
				dx = dx,
				dz = dz
			};
		}
	}
}
