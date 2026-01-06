using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000135 RID: 309
	internal static class NumberHelpers
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x000513AC File Offset: 0x0004F5AC
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

		// Token: 0x06001108 RID: 4360 RVA: 0x000513C8 File Offset: 0x0004F5C8
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

		// Token: 0x06001109 RID: 4361 RVA: 0x000513E4 File Offset: 0x0004F5E4
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

		// Token: 0x0600110A RID: 4362 RVA: 0x000513FF File Offset: 0x0004F5FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(double a, double b)
		{
			return Math.Abs(b - a) < Math.Max(1E-06 * Math.Max(Math.Abs(a), Math.Abs(b)), 4E-323);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00051434 File Offset: 0x0004F634
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

		// Token: 0x0600110C RID: 4364 RVA: 0x00051456 File Offset: 0x0004F656
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

		// Token: 0x0600110D RID: 4365 RVA: 0x00051478 File Offset: 0x0004F678
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

		// Token: 0x0600110E RID: 4366 RVA: 0x0005149E File Offset: 0x0004F69E
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

		// Token: 0x0600110F RID: 4367 RVA: 0x000514C4 File Offset: 0x0004F6C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RemapUIntBitsToNormalizeFloatToUIntBits(uint value, uint inBitSize, uint outBitSize)
		{
			uint num = (uint)((1L << (int)inBitSize) - 1L);
			uint num2 = (uint)((1L << (int)outBitSize) - 1L);
			return NumberHelpers.NormalizedFloatToUInt(NumberHelpers.UIntToNormalizedFloat(value, 0U, num), 0U, num2);
		}
	}
}
