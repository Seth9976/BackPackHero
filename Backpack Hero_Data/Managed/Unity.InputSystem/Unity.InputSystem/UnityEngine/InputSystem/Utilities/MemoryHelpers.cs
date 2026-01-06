using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000131 RID: 305
	internal static class MemoryHelpers
	{
		// Token: 0x060010DC RID: 4316 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public unsafe static bool Compare(void* ptr1, void* ptr2, MemoryHelpers.BitRegion region)
		{
			if (region.sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit(ptr1, region.bitOffset) == MemoryHelpers.ReadSingleBit(ptr2, region.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion(ptr1, ptr2, region.bitOffset, region.sizeInBits, null);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000506DF File Offset: 0x0004E8DF
		public static uint ComputeFollowingByteOffset(uint byteOffset, uint sizeInBits)
		{
			return (uint)((ulong)(byteOffset + sizeInBits / 8U) + (ulong)((sizeInBits % 8U > 0U) ? 1L : 0L));
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000506F4 File Offset: 0x0004E8F4
		public unsafe static void WriteSingleBit(void* ptr, uint bitOffset, bool value)
		{
			uint num = bitOffset >> 3;
			bitOffset &= 7U;
			if (value)
			{
				byte* ptr2 = (byte*)ptr + num;
				*ptr2 |= (byte)(1 << (int)bitOffset);
				return;
			}
			byte* ptr3 = (byte*)ptr + num;
			*ptr3 &= (byte)(~(byte)(1 << (int)bitOffset));
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00050730 File Offset: 0x0004E930
		public unsafe static bool ReadSingleBit(void* ptr, uint bitOffset)
		{
			uint num = bitOffset >> 3;
			bitOffset &= 7U;
			return ((int)((byte*)ptr)[num] & (1 << (int)bitOffset)) != 0;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00050758 File Offset: 0x0004E958
		public unsafe static void MemCpyBitRegion(void* destination, void* source, uint bitOffset, uint bitCount)
		{
			byte* ptr = (byte*)destination;
			byte* ptr2 = (byte*)source;
			if (bitOffset >= 8U)
			{
				uint num = bitOffset / 8U;
				ptr += num;
				ptr2 += num;
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int num2 = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					num2 &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				*ptr = (byte)((((int)(*ptr) & ~num2) | ((int)(*ptr2) & num2)) & 255);
				if (bitCount + bitOffset <= 8U)
				{
					return;
				}
				ptr++;
				ptr2++;
				bitCount -= 8U - bitOffset;
			}
			uint num3 = bitCount / 8U;
			if (num3 >= 1U)
			{
				UnsafeUtility.MemCpy((void*)ptr, (void*)ptr2, (long)((ulong)num3));
			}
			uint num4 = bitCount % 8U;
			if (num4 > 0U)
			{
				ptr += num3;
				ptr2 += num3;
				int num5 = 255 >> (int)(8U - num4);
				*ptr = (byte)((((int)(*ptr) & ~num5) | ((int)(*ptr2) & num5)) & 255);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00050820 File Offset: 0x0004EA20
		public unsafe static bool MemCmpBitRegion(void* ptr1, void* ptr2, uint bitOffset, uint bitCount, void* mask = null)
		{
			byte* ptr3 = (byte*)ptr1;
			byte* ptr4 = (byte*)ptr2;
			byte* ptr5 = (byte*)mask;
			if (bitOffset >= 8U)
			{
				uint num = bitOffset / 8U;
				ptr3 += num;
				ptr4 += num;
				if (ptr5 != null)
				{
					ptr5 += num;
				}
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int num2 = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					num2 &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				if (ptr5 != null)
				{
					num2 &= (int)(*ptr5);
					ptr5++;
				}
				int num3 = (int)(*ptr3) & num2;
				int num4 = (int)(*ptr4) & num2;
				if (num3 != num4)
				{
					return false;
				}
				if (bitCount + bitOffset <= 8U)
				{
					return true;
				}
				ptr3++;
				ptr4++;
				bitCount -= 8U - bitOffset;
			}
			uint num5 = bitCount / 8U;
			if (num5 >= 1U)
			{
				if (ptr5 != null)
				{
					int num6 = 0;
					while ((long)num6 < (long)((ulong)num5))
					{
						byte b = ptr3[num6];
						byte b2 = ptr4[num6];
						byte b3 = ptr5[num6];
						if ((b & b3) != (b2 & b3))
						{
							return false;
						}
						num6++;
					}
				}
				else if (UnsafeUtility.MemCmp((void*)ptr3, (void*)ptr4, (long)((ulong)num5)) != 0)
				{
					return false;
				}
			}
			uint num7 = bitCount % 8U;
			if (num7 > 0U)
			{
				ptr3 += num5;
				ptr4 += num5;
				int num8 = 255 >> (int)(8U - num7);
				if (ptr5 != null)
				{
					ptr5 += num5;
					num8 &= (int)(*ptr5);
				}
				int num9 = (int)(*ptr3) & num8;
				int num10 = (int)(*ptr4) & num8;
				if (num9 != num10)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00050950 File Offset: 0x0004EB50
		public unsafe static void MemSet(void* destination, int numBytes, byte value)
		{
			int num = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + num) = (long)(((ulong)value << 56) | ((ulong)value << 48) | ((ulong)value << 40) | ((ulong)value << 32) | ((ulong)value << 24) | ((ulong)value << 16) | ((ulong)value << 8) | (ulong)value);
				numBytes -= 8;
				num += 8;
			}
			while (numBytes >= 4)
			{
				*(int*)((byte*)destination + num) = ((int)value << 24) | ((int)value << 16) | ((int)value << 8) | (int)value;
				numBytes -= 4;
				num += 4;
			}
			while (numBytes > 0)
			{
				((byte*)destination)[num] = value;
				numBytes--;
				num++;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000509D8 File Offset: 0x0004EBD8
		public unsafe static void MemCpyMasked(void* destination, void* source, int numBytes, void* mask)
		{
			int num = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + num) &= ~(*(long*)((byte*)mask + num));
				*(long*)((byte*)destination + num) |= *(long*)((byte*)source + num) & *(long*)((byte*)mask + num);
				numBytes -= 8;
				num += 8;
			}
			while (numBytes >= 4)
			{
				*(uint*)((byte*)destination + num) &= ~(*(uint*)((byte*)mask + num));
				*(uint*)((byte*)destination + num) |= *(uint*)((byte*)source + num) & *(uint*)((byte*)mask + num);
				numBytes -= 4;
				num += 4;
			}
			while (numBytes > 0)
			{
				byte* ptr = (byte*)destination + num;
				*ptr &= ~((byte*)mask)[num];
				byte* ptr2 = (byte*)destination + num;
				*ptr2 |= ((byte*)source)[num] & ((byte*)mask)[num];
				numBytes--;
				num++;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00050A74 File Offset: 0x0004EC74
		public unsafe static uint ReadMultipleBitsAsUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to read more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int num = (int)(bitOffset % 32U);
				int num2 = (int)((bitOffset - (uint)num) / 32U);
				ptr = (void*)((byte*)ptr + num2 * 4);
				bitOffset = (uint)num;
			}
			if (bitOffset + bitCount <= 8U)
			{
				uint num3 = (uint)((byte)(*(byte*)ptr >> (int)bitOffset));
				uint num4 = 255U >> (int)(8U - bitCount);
				return num3 & num4;
			}
			if (bitOffset + bitCount <= 16U)
			{
				uint num5 = (uint)((ushort)(*(ushort*)ptr >> (int)bitOffset));
				uint num6 = 65535U >> (int)(16U - bitCount);
				return num5 & num6;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint num7 = *(uint*)ptr >> (int)bitOffset;
				uint num8 = uint.MaxValue >> (int)(32U - bitCount);
				return num7 & num8;
			}
			throw new NotImplementedException("Reading int straddling int boundary");
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00050B28 File Offset: 0x0004ED28
		public unsafe static void WriteUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, uint value)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to write more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int num = (int)(bitOffset % 32U);
				int num2 = (int)((bitOffset - (uint)num) / 32U);
				ptr = (void*)((byte*)ptr + num2 * 4);
				bitOffset = (uint)num;
			}
			if (bitOffset + bitCount <= 8U)
			{
				byte b = (byte)value;
				b = (byte)(b << (int)bitOffset);
				uint num3 = ~(255U >> (int)(8U - bitCount) << (int)bitOffset);
				*(byte*)ptr = (byte)(((uint)(*(byte*)ptr) & num3) | (uint)b);
				return;
			}
			if (bitOffset + bitCount <= 16U)
			{
				ushort num4 = (ushort)value;
				num4 = (ushort)(num4 << (int)bitOffset);
				uint num5 = ~(65535U >> (int)(16U - bitCount) << (int)bitOffset);
				*(short*)ptr = (short)((ushort)(((uint)(*(ushort*)ptr) & num5) | (uint)num4));
				return;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint num6 = value << (int)bitOffset;
				uint num7 = ~(uint.MaxValue >> (int)(32U - bitCount) << (int)bitOffset);
				*(int*)ptr = (int)((*(uint*)ptr & num7) | num6);
				return;
			}
			throw new NotImplementedException("Writing int straddling int boundary");
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00050C12 File Offset: 0x0004EE12
		public unsafe static int ReadTwosComplementMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			return (int)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00050C1C File Offset: 0x0004EE1C
		public unsafe static void WriteIntAsTwosComplementMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)value);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00050C28 File Offset: 0x0004EE28
		public unsafe static int ReadExcessKMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			long num = (long)((ulong)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount));
			long num2 = (1L << (int)bitCount) / 2L;
			return (int)(num - num2);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00050C4C File Offset: 0x0004EE4C
		public unsafe static void WriteIntAsExcessKMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			long num = (1L << (int)bitCount) / 2L + (long)value;
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)num);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00050C74 File Offset: 0x0004EE74
		public unsafe static float ReadMultipleBitsAsNormalizedUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			uint num = MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
			uint num2 = (uint)((1L << (int)bitCount) - 1L);
			return NumberHelpers.UIntToNormalizedFloat(num, 0U, num2);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00050C9C File Offset: 0x0004EE9C
		public unsafe static void WriteNormalizedUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, float value)
		{
			uint num = (uint)((1L << (int)bitCount) - 1L);
			uint num2 = NumberHelpers.NormalizedFloatToUInt(value, 0U, num);
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, num2);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00050CC8 File Offset: 0x0004EEC8
		public unsafe static void SetBitsInBuffer(void* buffer, int byteOffset, int bitOffset, int sizeInBits, bool value)
		{
			if (buffer == null)
			{
				throw new ArgumentException("A buffer must be provided to apply the bitmask on", "buffer");
			}
			if (sizeInBits < 0)
			{
				throw new ArgumentException("Negative sizeInBits", "sizeInBits");
			}
			if (bitOffset < 0)
			{
				throw new ArgumentException("Negative bitOffset", "bitOffset");
			}
			if (byteOffset < 0)
			{
				throw new ArgumentException("Negative byteOffset", "byteOffset");
			}
			if (bitOffset >= 8)
			{
				int num = bitOffset / 8;
				byteOffset += num;
				bitOffset %= 8;
			}
			byte* ptr = (byte*)buffer + byteOffset;
			int i = sizeInBits;
			if (bitOffset != 0)
			{
				int num2 = 255 << bitOffset;
				if (i + bitOffset < 8)
				{
					num2 &= 255 >> 8 - (i + bitOffset);
				}
				if (value)
				{
					byte* ptr2 = ptr;
					*ptr2 |= (byte)num2;
				}
				else
				{
					byte* ptr3 = ptr;
					*ptr3 &= (byte)(~(byte)num2);
				}
				ptr++;
				i -= 8 - bitOffset;
			}
			while (i >= 8)
			{
				*ptr = (value ? byte.MaxValue : 0);
				ptr++;
				i -= 8;
			}
			if (i > 0)
			{
				byte b = (byte)(255 >> 8 - i);
				if (value)
				{
					byte* ptr4 = ptr;
					*ptr4 |= b;
					return;
				}
				byte* ptr5 = ptr;
				*ptr5 &= ~b;
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00050DCC File Offset: 0x0004EFCC
		public static void Swap<TValue>(ref TValue a, ref TValue b)
		{
			TValue tvalue = a;
			a = b;
			b = tvalue;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00050DF4 File Offset: 0x0004EFF4
		public static uint AlignNatural(uint offset, uint sizeInBytes)
		{
			uint num = Math.Min(8U, sizeInBytes);
			return offset.AlignToMultipleOf(num);
		}

		// Token: 0x0200023C RID: 572
		public struct BitRegion
		{
			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x060015AF RID: 5551 RVA: 0x0006386B File Offset: 0x00061A6B
			public bool isEmpty
			{
				get
				{
					return this.sizeInBits == 0U;
				}
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x00063876 File Offset: 0x00061A76
			public BitRegion(uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x060015B1 RID: 5553 RVA: 0x00063886 File Offset: 0x00061A86
			public BitRegion(uint byteOffset, uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = byteOffset * 8U + bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x0006389C File Offset: 0x00061A9C
			public MemoryHelpers.BitRegion Overlap(MemoryHelpers.BitRegion other)
			{
				uint num = this.bitOffset + this.sizeInBits;
				uint num2 = other.bitOffset + other.sizeInBits;
				if (num <= other.bitOffset || num2 <= this.bitOffset)
				{
					return default(MemoryHelpers.BitRegion);
				}
				uint num3 = Math.Min(num, num2);
				uint num4 = Math.Max(this.bitOffset, other.bitOffset);
				return new MemoryHelpers.BitRegion(num4, num3 - num4);
			}

			// Token: 0x04000C0B RID: 3083
			public uint bitOffset;

			// Token: 0x04000C0C RID: 3084
			public uint sizeInBits;
		}
	}
}
