using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public struct SphericalHarmonicsL1
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		public static SphericalHarmonicsL1 operator +(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr + rhs.shAr,
				shAg = lhs.shAg + rhs.shAg,
				shAb = lhs.shAb + rhs.shAb
			};
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011B30 File Offset: 0x0000FD30
		public static SphericalHarmonicsL1 operator -(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr - rhs.shAr,
				shAg = lhs.shAg - rhs.shAg,
				shAb = lhs.shAb - rhs.shAb
			};
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011B90 File Offset: 0x0000FD90
		public static SphericalHarmonicsL1 operator *(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr * rhs,
				shAg = lhs.shAg * rhs,
				shAb = lhs.shAb * rhs
			};
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		public static SphericalHarmonicsL1 operator /(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr / rhs,
				shAg = lhs.shAg / rhs,
				shAb = lhs.shAb / rhs
			};
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011C2F File Offset: 0x0000FE2F
		public static bool operator ==(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return lhs.shAr == rhs.shAr && lhs.shAg == rhs.shAg && lhs.shAb == rhs.shAb;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011C6A File Offset: 0x0000FE6A
		public static bool operator !=(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00011C76 File Offset: 0x0000FE76
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL1 && this == (SphericalHarmonicsL1)other;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011C94 File Offset: 0x0000FE94
		public override int GetHashCode()
		{
			return ((391 + this.shAr.GetHashCode()) * 23 + this.shAg.GetHashCode()) * 23 + this.shAb.GetHashCode();
		}

		// Token: 0x0400025D RID: 605
		public Vector4 shAr;

		// Token: 0x0400025E RID: 606
		public Vector4 shAg;

		// Token: 0x0400025F RID: 607
		public Vector4 shAb;

		// Token: 0x04000260 RID: 608
		public static readonly SphericalHarmonicsL1 zero = new SphericalHarmonicsL1
		{
			shAr = Vector4.zero,
			shAg = Vector4.zero,
			shAb = Vector4.zero
		};
	}
}
