using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000AF0 RID: 2800
	public static class SequenceReaderExtensions
	{
		// Token: 0x060063BB RID: 25531 RVA: 0x0014E17C File Offset: 0x0014C37C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryRead<[IsUnmanaged] T>(this SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			ReadOnlySpan<byte> unreadSpan = reader.UnreadSpan;
			if (unreadSpan.Length < sizeof(T))
			{
				return SequenceReaderExtensions.TryReadMultisegment<T>(ref reader, out value);
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(unreadSpan));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x0014E1C8 File Offset: 0x0014C3C8
		private unsafe static bool TryReadMultisegment<[IsUnmanaged] T>(ref SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			T t = default(T);
			Span<byte> span = new Span<byte>((void*)(&t), sizeof(T));
			if (!reader.TryCopyTo(span))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(span));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x0014E21E File Offset: 0x0014C41E
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0014E236 File Offset: 0x0014C436
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0014E24E File Offset: 0x0014C44E
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out short value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0014E265 File Offset: 0x0014C465
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0014E27D File Offset: 0x0014C47D
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0014E295 File Offset: 0x0014C495
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out int value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0014E2AC File Offset: 0x0014C4AC
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0014E2C4 File Offset: 0x0014C4C4
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0014E2DC File Offset: 0x0014C4DC
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out long value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}
	}
}
