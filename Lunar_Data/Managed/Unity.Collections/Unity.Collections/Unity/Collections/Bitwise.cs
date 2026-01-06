using System;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000035 RID: 53
	[BurstCompatible]
	internal struct Bitwise
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000389B File Offset: 0x00001A9B
		internal static int AlignDown(int value, int alignPow2)
		{
			return value & ~(alignPow2 - 1);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000038A3 File Offset: 0x00001AA3
		internal static int AlignUp(int value, int alignPow2)
		{
			return Bitwise.AlignDown(value + alignPow2 - 1, alignPow2);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000038B0 File Offset: 0x00001AB0
		internal static int FromBool(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal static uint ExtractBits(uint input, int pos, uint mask)
		{
			return (input >> pos) & mask;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal static uint ReplaceBits(uint input, int pos, uint mask, uint value)
		{
			uint num = (value & mask) << pos;
			uint num2 = input & ~(mask << pos);
			return num | num2;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000038E5 File Offset: 0x00001AE5
		internal static uint SetBits(uint input, int pos, uint mask, bool value)
		{
			return Bitwise.ReplaceBits(input, pos, mask, (uint)(-(uint)Bitwise.FromBool(value)));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000038F6 File Offset: 0x00001AF6
		internal static ulong ExtractBits(ulong input, int pos, ulong mask)
		{
			return (input >> pos) & mask;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003900 File Offset: 0x00001B00
		internal static ulong ReplaceBits(ulong input, int pos, ulong mask, ulong value)
		{
			ulong num = (value & mask) << pos;
			ulong num2 = input & ~(mask << pos);
			return num | num2;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003921 File Offset: 0x00001B21
		internal static ulong SetBits(ulong input, int pos, ulong mask, bool value)
		{
			return Bitwise.ReplaceBits(input, pos, mask, (ulong)(-(ulong)((long)Bitwise.FromBool(value))));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003933 File Offset: 0x00001B33
		internal static int lzcnt(byte value)
		{
			return math.lzcnt((uint)value) - 24;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000393E File Offset: 0x00001B3E
		internal static int tzcnt(byte value)
		{
			return math.min(8, math.tzcnt((uint)value));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000394C File Offset: 0x00001B4C
		internal static int lzcnt(ushort value)
		{
			return math.lzcnt((uint)value) - 16;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003957 File Offset: 0x00001B57
		internal static int tzcnt(ushort value)
		{
			return math.min(16, math.tzcnt((uint)value));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003968 File Offset: 0x00001B68
		private unsafe static int FindUlong(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num = numBits + 63 >> 6;
			int num2 = 64;
			int i = beginBit / num2;
			int num3 = Bitwise.AlignUp(endBit, num2) / num2;
			while (i < num3)
			{
				if (ptr[i] == 0UL)
				{
					int num4 = i * num2;
					int num5 = math.min(num4 + num2, endBit) - num4;
					if (num4 != beginBit)
					{
						ulong num6 = ptr[num4 / num2 - 1];
						int num7 = math.max(num4 - math.lzcnt(num6), beginBit);
						num5 += num4 - num7;
						num4 = num7;
					}
					for (i++; i < num3; i++)
					{
						if (num5 >= numBits)
						{
							return num4;
						}
						ulong num8 = ptr[i];
						int num9 = i * num2;
						num5 += math.min(num9 + math.tzcnt(num8), endBit) - num9;
						if (num8 != 0UL)
						{
							break;
						}
					}
					if (num5 >= numBits)
					{
						return num4;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003A3C File Offset: 0x00001C3C
		private unsafe static int FindUint(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num = numBits + 31 >> 5;
			int num2 = 32;
			int i = beginBit / num2;
			int num3 = Bitwise.AlignUp(endBit, num2) / num2;
			while (i < num3)
			{
				if (*(uint*)(ptr + (IntPtr)i * 4 / 8) == 0U)
				{
					int num4 = i * num2;
					int num5 = math.min(num4 + num2, endBit) - num4;
					if (num4 != beginBit)
					{
						uint num6 = *(uint*)(ptr + (IntPtr)(num4 / num2 - 1) * 4 / 8);
						int num7 = math.max(num4 - math.lzcnt(num6), beginBit);
						num5 += num4 - num7;
						num4 = num7;
					}
					for (i++; i < num3; i++)
					{
						if (num5 >= numBits)
						{
							return num4;
						}
						uint num8 = *(uint*)(ptr + (IntPtr)i * 4 / 8);
						int num9 = i * num2;
						num5 += math.min(num9 + math.tzcnt(num8), endBit) - num9;
						if (num8 != 0U)
						{
							break;
						}
					}
					if (num5 >= numBits)
					{
						return num4;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003B10 File Offset: 0x00001D10
		private unsafe static int FindUshort(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num = numBits + 15 >> 4;
			int num2 = 16;
			int i = beginBit / num2;
			int num3 = Bitwise.AlignUp(endBit, num2) / num2;
			while (i < num3)
			{
				if (*(ushort*)(ptr + (IntPtr)i * 2 / 8) == 0)
				{
					int num4 = i * num2;
					int num5 = math.min(num4 + num2, endBit) - num4;
					if (num4 != beginBit)
					{
						ushort num6 = *(ushort*)(ptr + (IntPtr)(num4 / num2 - 1) * 2 / 8);
						int num7 = math.max(num4 - Bitwise.lzcnt(num6), beginBit);
						num5 += num4 - num7;
						num4 = num7;
					}
					for (i++; i < num3; i++)
					{
						if (num5 >= numBits)
						{
							return num4;
						}
						ushort num8 = *(ushort*)(ptr + (IntPtr)i * 2 / 8);
						int num9 = i * num2;
						num5 += math.min(num9 + Bitwise.tzcnt(num8), endBit) - num9;
						if (num8 != 0)
						{
							break;
						}
					}
					if (num5 >= numBits)
					{
						return num4;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003BE4 File Offset: 0x00001DE4
		private unsafe static int FindByte(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num = numBits + 7 >> 3;
			int num2 = 8;
			int i = beginBit / num2;
			int num3 = Bitwise.AlignUp(endBit, num2) / num2;
			while (i < num3)
			{
				if (*(byte*)(ptr + i / 8) == 0)
				{
					int num4 = i * num2;
					int num5 = math.min(num4 + num2, endBit) - num4;
					if (num4 != beginBit)
					{
						byte b = *(byte*)(ptr + (num4 / num2 - 1) / 8);
						int num6 = math.max(num4 - Bitwise.lzcnt(b), beginBit);
						num5 += num4 - num6;
						num4 = num6;
					}
					for (i++; i < num3; i++)
					{
						if (num5 >= numBits)
						{
							return num4;
						}
						byte b2 = *(byte*)(ptr + i / 8);
						int num7 = i * num2;
						num5 += math.min(num7 + Bitwise.tzcnt(b2), endBit) - num7;
						if (b2 != 0)
						{
							break;
						}
					}
					if (num5 >= numBits)
					{
						return num4;
					}
				}
				i++;
			}
			return endBit;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003CAC File Offset: 0x00001EAC
		private unsafe static int FindUpto14bits(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			byte b = (byte)(beginBit & 7);
			byte b2 = (byte)(~(byte)(255 << (int)b));
			int num = 0;
			int num2 = beginBit / 8;
			int num3 = Bitwise.AlignUp(endBit, 8) / 8;
			for (int i = num2; i < num3; i++)
			{
				byte b3 = *(byte*)(ptr + i / 8);
				b3 |= ((i == num2) ? b2 : 0);
				if (b3 != 255)
				{
					int num4 = i * 8;
					int num5 = math.min(num4 + Bitwise.tzcnt(b3), endBit) - num4;
					if (num + num5 >= numBits)
					{
						return num4 - num;
					}
					num = Bitwise.lzcnt(b3);
					int num6 = num4 + 8;
					int num7 = math.max(num6 - num, beginBit);
					num = math.min(num6, endBit) - num7;
					if (num >= numBits)
					{
						return num7;
					}
				}
			}
			return endBit;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003D64 File Offset: 0x00001F64
		private unsafe static int FindUpto6bits(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			byte b = (byte)(~(byte)(255 << (beginBit & 7)));
			byte b2 = (byte)(~(byte)(255 >> ((8 - (endBit & 7)) & 7)));
			int num = 1 << numBits - 1;
			int num2 = beginBit / 8;
			int num3 = Bitwise.AlignUp(endBit, 8) / 8;
			for (int i = num2; i < num3; i++)
			{
				byte b3 = *(byte*)(ptr + i / 8);
				b3 |= ((i == num2) ? b : 0);
				b3 |= ((i == num3 - 1) ? b2 : 0);
				if (b3 != 255)
				{
					int j = i * 8;
					int num4 = j + 7;
					while (j < num4)
					{
						int num5 = Bitwise.tzcnt(b3 ^ byte.MaxValue);
						b3 = (byte)(b3 >> num5);
						j += num5;
						if (((int)b3 & num) == 0)
						{
							return j;
						}
						b3 = (byte)(b3 >> 1);
						j++;
					}
				}
			}
			return endBit;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003E48 File Offset: 0x00002048
		internal unsafe static int FindWithBeginEnd(ulong* ptr, int beginBit, int endBit, int numBits)
		{
			int num;
			if (numBits >= 127)
			{
				num = Bitwise.FindUlong(ptr, beginBit, endBit, numBits);
				if (num != endBit)
				{
					return num;
				}
			}
			if (numBits >= 63)
			{
				num = Bitwise.FindUint(ptr, beginBit, endBit, numBits);
				if (num != endBit)
				{
					return num;
				}
			}
			if (numBits >= 128)
			{
				return int.MaxValue;
			}
			if (numBits >= 31)
			{
				num = Bitwise.FindUshort(ptr, beginBit, endBit, numBits);
				if (num != endBit)
				{
					return num;
				}
			}
			if (numBits >= 64)
			{
				return int.MaxValue;
			}
			num = Bitwise.FindByte(ptr, beginBit, endBit, numBits);
			if (num != endBit)
			{
				return num;
			}
			if (numBits < 15)
			{
				num = Bitwise.FindUpto14bits(ptr, beginBit, endBit, numBits);
				if (num != endBit)
				{
					return num;
				}
				if (numBits < 7)
				{
					num = Bitwise.FindUpto6bits(ptr, beginBit, endBit, numBits);
					if (num != endBit)
					{
						return num;
					}
				}
			}
			return int.MaxValue;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003EEB File Offset: 0x000020EB
		internal unsafe static int Find(ulong* ptr, int pos, int count, int numBits)
		{
			return Bitwise.FindWithBeginEnd(ptr, pos, pos + count, numBits);
		}
	}
}
