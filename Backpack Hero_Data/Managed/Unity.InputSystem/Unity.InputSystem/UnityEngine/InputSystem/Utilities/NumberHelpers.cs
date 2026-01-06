using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000135 RID: 309
	internal static class NumberHelpers
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x000515C0 File Offset: 0x0004F7C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int AlignToMultipleOf(this int number, int alignment)
		{
			int num = number % alignment;
			if (num == 0)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000515DC File Offset: 0x0004F7DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long AlignToMultipleOf(this long number, long alignment)
		{
			long num = number % alignment;
			if (num == 0L)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000515F8 File Offset: 0x0004F7F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint AlignToMultipleOf(this uint number, uint alignment)
		{
			uint num = number % alignment;
			if (num == 0U)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00051613 File Offset: 0x0004F813
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(double a, double b)
		{
			return Math.Abs(b - a) < Math.Max(1E-06 * Math.Max(Math.Abs(a), Math.Abs(b)), 4E-323);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00051648 File Offset: 0x0004F848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IntToNormalizedFloat(int value, int minValue, int maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)(((double)value - (double)minValue) / ((double)maxValue - (double)minValue));
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0005166A File Offset: 0x0004F86A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int NormalizedFloatToInt(float value, int intMinValue, int intMaxValue)
		{
			if (value <= 0f)
			{
				return intMinValue;
			}
			if (value >= 1f)
			{
				return intMaxValue;
			}
			return (int)((double)value * ((double)intMaxValue - (double)intMinValue) + (double)intMinValue);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0005168C File Offset: 0x0004F88C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float UIntToNormalizedFloat(uint value, uint minValue, uint maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)((value - minValue) / (maxValue - minValue));
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000516B2 File Offset: 0x0004F8B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NormalizedFloatToUInt(float value, uint uintMinValue, uint uintMaxValue)
		{
			if (value <= 0f)
			{
				return uintMinValue;
			}
			if (value >= 1f)
			{
				return uintMaxValue;
			}
			return (uint)((double)value * (uintMaxValue - uintMinValue) + uintMinValue);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000516D8 File Offset: 0x0004F8D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RemapUIntBitsToNormalizeFloatToUIntBits(uint value, uint inBitSize, uint outBitSize)
		{
			uint num = (uint)((1L << (int)inBitSize) - 1L);
			uint num2 = (uint)((1L << (int)outBitSize) - 1L);
			return NumberHelpers.NormalizedFloatToUInt(NumberHelpers.UIntToNormalizedFloat(value, 0U, num), 0U, num2);
		}
	}
}
