using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Pathfinding.Util
{
	// Token: 0x02000258 RID: 600
	public static class SpanExtensions
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x0005875E File Offset: 0x0005695E
		public unsafe static void FillZeros<[IsUnmanaged] T>(this UnsafeSpan<T> span) where T : struct, ValueType
		{
			if (span.length > 0U)
			{
				UnsafeUtility.MemSet((void*)span.ptr, 0, (long)sizeof(T) * (long)((ulong)span.length));
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00058784 File Offset: 0x00056984
		public unsafe static void Fill<[IsUnmanaged] T>(this UnsafeSpan<T> span, T value) where T : struct, ValueType
		{
			if (span.length > 0U)
			{
				if ((long)sizeof(T) * (long)((ulong)span.length) > 2147483647L)
				{
					throw new ArgumentException("Span is too large to fill");
				}
				UnsafeUtility.MemCpyReplicate((void*)span.ptr, (void*)(&value), sizeof(T), (int)span.length);
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000587D6 File Offset: 0x000569D6
		public static void CopyFrom<[IsUnmanaged] T>(this UnsafeSpan<T> span, NativeArray<T> array) where T : struct, ValueType
		{
			span.CopyFrom(array.AsUnsafeReadOnlySpan<T>());
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000587E4 File Offset: 0x000569E4
		public unsafe static void CopyFrom<[IsUnmanaged] T>(this UnsafeSpan<T> span, UnsafeSpan<T> other) where T : struct, ValueType
		{
			if (other.Length > span.Length)
			{
				throw new InvalidOperationException();
			}
			if (other.Length == 0)
			{
				return;
			}
			UnsafeUtility.MemCpy((void*)span.ptr, (void*)other.ptr, (long)sizeof(T) * (long)other.Length);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00058834 File Offset: 0x00056A34
		public unsafe static void CopyFrom<[IsUnmanaged] T>(this UnsafeSpan<T> span, T[] array) where T : struct, ValueType
		{
			if (array.Length > span.Length)
			{
				throw new InvalidOperationException();
			}
			if (array.Length == 0)
			{
				return;
			}
			ulong num;
			void* ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out num);
			UnsafeUtility.MemCpy((void*)span.ptr, ptr, (long)sizeof(T) * (long)array.Length);
			UnsafeUtility.ReleaseGCObject(num);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00058880 File Offset: 0x00056A80
		[MethodImpl(256)]
		public unsafe static UnsafeSpan<T> AsUnsafeSpan<[IsUnmanaged] T>(this UnsafeAppendBuffer buffer) where T : struct, ValueType
		{
			int num = buffer.Length / UnsafeUtility.SizeOf<T>();
			if (num * UnsafeUtility.SizeOf<T>() != buffer.Length)
			{
				throw new ArgumentException("Buffer length is not a multiple of the element size");
			}
			return new UnsafeSpan<T>((void*)buffer.Ptr, num);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000588C0 File Offset: 0x00056AC0
		[MethodImpl(256)]
		public static UnsafeSpan<T> AsUnsafeSpan<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return new UnsafeSpan<T>(list.GetUnsafePtr<T>(), list.Length);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000588D4 File Offset: 0x00056AD4
		[MethodImpl(256)]
		public static UnsafeSpan<T> AsUnsafeSpan<[IsUnmanaged] T>(this NativeArray<T> arr) where T : struct, ValueType
		{
			return new UnsafeSpan<T>(arr.GetUnsafePtr<T>(), arr.Length);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000588E8 File Offset: 0x00056AE8
		[MethodImpl(256)]
		public static UnsafeSpan<T> AsUnsafeSpanNoChecks<[IsUnmanaged] T>(this NativeArray<T> arr) where T : struct, ValueType
		{
			return new UnsafeSpan<T>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(arr), arr.Length);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000588FC File Offset: 0x00056AFC
		[MethodImpl(256)]
		public static UnsafeSpan<T> AsUnsafeReadOnlySpan<[IsUnmanaged] T>(this NativeArray<T> arr) where T : struct, ValueType
		{
			return new UnsafeSpan<T>(arr.GetUnsafeReadOnlyPtr<T>(), arr.Length);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00058910 File Offset: 0x00056B10
		[MethodImpl(256)]
		public unsafe static UnsafeSpan<T> AsUnsafeSpan<[IsUnmanaged] T>(this UnsafeList<T> arr) where T : struct, ValueType
		{
			return new UnsafeSpan<T>((void*)arr.Ptr, arr.Length);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00058924 File Offset: 0x00056B24
		[MethodImpl(256)]
		public static UnsafeSpan<T> AsUnsafeSpan<[IsUnmanaged] T>(this NativeSlice<T> slice) where T : struct, ValueType
		{
			return new UnsafeSpan<T>(slice.GetUnsafePtr<T>(), slice.Length);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00058938 File Offset: 0x00056B38
		public static bool Contains<[IsUnmanaged] T>(this UnsafeSpan<T> span, T value) where T : struct, ValueType, IEquatable<T>
		{
			return span.IndexOf(value) != -1;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00058947 File Offset: 0x00056B47
		public unsafe static int IndexOf<[IsUnmanaged] T>(this UnsafeSpan<T> span, T value) where T : struct, ValueType, IEquatable<T>
		{
			return new ReadOnlySpan<T>((void*)span.ptr, (int)span.length).IndexOf(value);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00058960 File Offset: 0x00056B60
		public static void Sort<[IsUnmanaged] T>(this UnsafeSpan<T> span) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.Sort<T>(span.ptr, span.Length);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00058974 File Offset: 0x00056B74
		public static void Sort<[IsUnmanaged] T, U>(this UnsafeSpan<T> span, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.Sort<T, U>(span.ptr, span.Length, comp);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0005898C File Offset: 0x00056B8C
		public static void InsertRange<[IsUnmanaged] T>(this NativeList<T> list, int index, int count) where T : struct, ValueType
		{
			list.ResizeUninitialized(list.Length + count);
			list.AsUnsafeSpan<T>().Move(index, index + count, list.Length - (index + count));
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000589C8 File Offset: 0x00056BC8
		public static void AddReplicate<[IsUnmanaged] T>(this NativeList<T> list, T value, int count) where T : struct, ValueType
		{
			int length = list.Length;
			list.ResizeUninitialized(length + count);
			list.AsUnsafeSpan<T>().Slice(length).Fill(value);
		}
	}
}
