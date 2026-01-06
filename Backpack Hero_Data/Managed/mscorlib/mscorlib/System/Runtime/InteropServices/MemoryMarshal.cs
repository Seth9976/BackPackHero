using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C8 RID: 1736
	public static class MemoryMarshal
	{
		// Token: 0x06003FE0 RID: 16352 RVA: 0x000DFE80 File Offset: 0x000DE080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new Span<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000DFEB5 File Offset: 0x000DE0B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new ReadOnlySpan<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000DFEEA File Offset: 0x000DE0EA
		public unsafe static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000DFEF8 File Offset: 0x000DE0F8
		public static ref T GetReference<T>(Span<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000DFF14 File Offset: 0x000DE114
		public static ref T GetReference<T>(ReadOnlySpan<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000DFF30 File Offset: 0x000DE130
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(Span<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000DFF5C File Offset: 0x000DE15C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000DFF88 File Offset: 0x000DE188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			checked
			{
				int num3;
				if (num == num2)
				{
					num3 = (int)length;
				}
				else if (num == 1U)
				{
					num3 = (int)(length / num2);
				}
				else
				{
					num3 = (int)(unchecked((ulong)length * (ulong)num / (ulong)num2));
				}
				return new Span<TTo>(Unsafe.As<TFrom, TTo>(span._pointer.Value), num3);
			}
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000E000C File Offset: 0x000DE20C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			checked
			{
				int num3;
				if (num == num2)
				{
					num3 = (int)length;
				}
				else if (num == 1U)
				{
					num3 = (int)(length / num2);
				}
				else
				{
					num3 = (int)(unchecked((ulong)length * (ulong)num / (ulong)num2));
				}
				return new ReadOnlySpan<TTo>(Unsafe.As<TFrom, TTo>(MemoryMarshal.GetReference<TFrom>(span)), num3);
			}
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000E0086 File Offset: 0x000DE286
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> CreateSpan<T>(ref T reference, int length)
		{
			return new Span<T>(ref reference, length);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000E008F File Offset: 0x000DE28F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
		{
			return new ReadOnlySpan<T>(ref reference, length);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000E0098 File Offset: 0x000DE298
		public static bool TryGetArray<T>(ReadOnlyMemory<T> memory, out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			if (num < 0)
			{
				ArraySegment<T> arraySegment;
				if (((MemoryManager<T>)objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + (num & int.MaxValue), num2);
					return true;
				}
			}
			else
			{
				T[] array = objectStartLength as T[];
				if (array != null)
				{
					segment = new ArraySegment<T>(array, num, num2 & int.MaxValue);
					return true;
				}
			}
			if ((num2 & 2147483647) == 0)
			{
				segment = ArraySegment<T>.Empty;
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000E012C File Offset: 0x000DE32C
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager) where TManager : MemoryManager<T>
		{
			int num;
			int num2;
			manager = memory.GetObjectStartLength(out num, out num2) as TManager;
			return manager != null;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000E0164 File Offset: 0x000DE364
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager, out int start, out int length) where TManager : MemoryManager<T>
		{
			manager = memory.GetObjectStartLength(out start, out length) as TManager;
			start &= int.MaxValue;
			if (manager == null)
			{
				start = 0;
				length = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000E01AC File Offset: 0x000DE3AC
		public unsafe static IEnumerable<T> ToEnumerable<T>(ReadOnlyMemory<T> memory)
		{
			int num;
			for (int i = 0; i < memory.Length; i = num + 1)
			{
				yield return *memory.Span[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000E01BC File Offset: 0x000DE3BC
		public static bool TryGetString(ReadOnlyMemory<char> memory, out string text, out int start, out int length)
		{
			int num;
			int num2;
			string text2 = memory.GetObjectStartLength(out num, out num2) as string;
			if (text2 != null)
			{
				text = text2;
				start = num;
				length = num2;
				return true;
			}
			text = null;
			start = 0;
			length = 0;
			return false;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000E01F2 File Offset: 0x000DE3F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000E022C File Offset: 0x000DE42C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryRead<T>(ReadOnlySpan<byte> source, out T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)source.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
			return true;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000E027A File Offset: 0x000DE47A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000E02B8 File Offset: 0x000DE4B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWrite<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)destination.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000E02F4 File Offset: 0x000DE4F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Memory<T> CreateFromPinnedArray<T>(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Memory<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(array, start, length | int.MinValue);
		}
	}
}
