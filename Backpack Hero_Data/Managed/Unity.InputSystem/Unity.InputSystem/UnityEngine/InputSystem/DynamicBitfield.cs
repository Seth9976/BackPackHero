using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005F RID: 95
	internal struct DynamicBitfield
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x000348FC File Offset: 0x00032AFC
		public void SetLength(int newLength)
		{
			int num = DynamicBitfield.BitCountToULongCount(newLength);
			if (this.array.length < num)
			{
				this.array.SetLength(num);
			}
			this.length = newLength;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00034934 File Offset: 0x00032B34
		public void SetBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] |= 1UL << bitIndex % 64;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00034966 File Offset: 0x00032B66
		public bool TestBit(int bitIndex)
		{
			return (this.array[bitIndex / 64] & (1UL << bitIndex % 64)) > 0UL;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00034988 File Offset: 0x00032B88
		public void ClearBit(int bitIndex)
		{
			ref InlinedArray<ulong> ptr = ref this.array;
			int num = bitIndex / 64;
			ptr[num] &= ~(1UL << bitIndex % 64);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000349BB File Offset: 0x00032BBB
		private static int BitCountToULongCount(int bitCount)
		{
			return (bitCount + 63) / 64;
		}

		// Token: 0x04000300 RID: 768
		public InlinedArray<ulong> array;

		// Token: 0x04000301 RID: 769
		public int length;
	}
}
