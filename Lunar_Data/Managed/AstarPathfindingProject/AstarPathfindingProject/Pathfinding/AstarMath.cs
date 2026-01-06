using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004C RID: 76
	public static class AstarMath
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public static float ThreadSafeRandomFloat()
		{
			object globalRandomLock = AstarMath.GlobalRandomLock;
			float num;
			lock (globalRandomLock)
			{
				num = AstarMath.GlobalRandom.NextFloat();
			}
			return num;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D114 File Offset: 0x0000B314
		public static float2 ThreadSafeRandomFloat2()
		{
			object globalRandomLock = AstarMath.GlobalRandomLock;
			float2 @float;
			lock (globalRandomLock)
			{
				@float = AstarMath.GlobalRandom.NextFloat2();
			}
			return @float;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D15C File Offset: 0x0000B35C
		public static long SaturatingConvertFloatToLong(float v)
		{
			if (v <= 9.223372E+18f)
			{
				return (long)v;
			}
			return long.MaxValue;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D172 File Offset: 0x0000B372
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			return Mathf.Lerp(targetMin, targetMax, Mathf.InverseLerp(startMin, startMax, value));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D184 File Offset: 0x0000B384
		private static int Bit(int a, int b)
		{
			return (a >> b) & 1;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D190 File Offset: 0x0000B390
		public static Color IntToColor(int i, float a)
		{
			float num = (float)(AstarMath.Bit(i, 2) + AstarMath.Bit(i, 3) * 2 + 1);
			int num2 = AstarMath.Bit(i, 1) + AstarMath.Bit(i, 4) * 2 + 1;
			int num3 = AstarMath.Bit(i, 0) + AstarMath.Bit(i, 5) * 2 + 1;
			return new Color(num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public static Color HSVToRGB(float h, float s, float v)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = s * v;
			float num5 = h / 60f;
			float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
			if (num5 < 1f)
			{
				num = num4;
				num2 = num6;
			}
			else if (num5 < 2f)
			{
				num = num6;
				num2 = num4;
			}
			else if (num5 < 3f)
			{
				num2 = num4;
				num3 = num6;
			}
			else if (num5 < 4f)
			{
				num2 = num6;
				num3 = num4;
			}
			else if (num5 < 5f)
			{
				num = num6;
				num3 = num4;
			}
			else if (num5 < 6f)
			{
				num = num4;
				num3 = num6;
			}
			float num7 = v - num4;
			num += num7;
			num2 += num7;
			num3 += num7;
			return new Color(num, num2, num3);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public static float DeltaAngle(float angle1, float angle2)
		{
			float num = (angle2 - angle1 + 3.1415927f) % 6.2831855f - 3.1415927f;
			return math.select(num, num + 6.2831855f, num < -3.1415927f);
		}

		// Token: 0x040001D3 RID: 467
		private static Unity.Mathematics.Random GlobalRandom = Unity.Mathematics.Random.CreateFromIndex(0U);

		// Token: 0x040001D4 RID: 468
		private static object GlobalRandomLock = new object();
	}
}
