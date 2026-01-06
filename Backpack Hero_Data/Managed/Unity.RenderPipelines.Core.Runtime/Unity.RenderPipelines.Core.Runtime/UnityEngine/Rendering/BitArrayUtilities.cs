using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A2 RID: 162
	public static class BitArrayUtilities
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00018B8A File Offset: 0x00016D8A
		public static bool Get8(uint index, byte data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018B97 File Offset: 0x00016D97
		public static bool Get16(uint index, ushort data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018BA4 File Offset: 0x00016DA4
		public static bool Get32(uint index, uint data)
		{
			return (data & (1U << (int)index)) > 0U;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018BB1 File Offset: 0x00016DB1
		public static bool Get64(uint index, ulong data)
		{
			return (data & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018BC0 File Offset: 0x00016DC0
		public static bool Get128(uint index, ulong data1, ulong data2)
		{
			if (index >= 64U)
			{
				return (data2 & (1UL << (int)(index - 64U))) > 0UL;
			}
			return (data1 & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public static bool Get256(uint index, ulong data1, ulong data2, ulong data3, ulong data4)
		{
			if (index >= 128U)
			{
				if (index >= 192U)
				{
					return (data4 & (1UL << (int)(index - 192U))) > 0UL;
				}
				return (data3 & (1UL << (int)(index - 128U))) > 0UL;
			}
			else
			{
				if (index >= 64U)
				{
					return (data2 & (1UL << (int)(index - 64U))) > 0UL;
				}
				return (data1 & (1UL << (int)index)) > 0UL;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018C51 File Offset: 0x00016E51
		public static void Set8(uint index, ref byte data, bool value)
		{
			data = (byte)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018C6E File Offset: 0x00016E6E
		public static void Set16(uint index, ref ushort data, bool value)
		{
			data = (ushort)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018C8B File Offset: 0x00016E8B
		public static void Set32(uint index, ref uint data, bool value)
		{
			data = (value ? (data | (1U << (int)index)) : (data & ~(1U << (int)index)));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00018CA7 File Offset: 0x00016EA7
		public static void Set64(uint index, ref ulong data, bool value)
		{
			data = (value ? (data | (1UL << (int)index)) : (data & ~(1UL << (int)index)));
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00018CC8 File Offset: 0x00016EC8
		public static void Set128(uint index, ref ulong data1, ref ulong data2, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018D1C File Offset: 0x00016F1C
		public static void Set256(uint index, ref ulong data1, ref ulong data2, ref ulong data3, ref ulong data4, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			if (index < 128U)
			{
				data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
				return;
			}
			if (index < 192U)
			{
				data3 = (value ? (data3 | (1UL << (int)(index - 64U))) : (data3 & ~(1UL << (int)(index - 128U))));
				return;
			}
			data4 = (value ? (data4 | (1UL << (int)(index - 64U))) : (data4 & ~(1UL << (int)(index - 192U))));
		}
	}
}
