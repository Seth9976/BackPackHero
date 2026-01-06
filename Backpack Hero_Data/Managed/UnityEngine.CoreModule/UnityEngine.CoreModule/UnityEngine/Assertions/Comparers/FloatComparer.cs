using System;
using System.Collections.Generic;

namespace UnityEngine.Assertions.Comparers
{
	// Token: 0x02000487 RID: 1159
	public class FloatComparer : IEqualityComparer<float>
	{
		// Token: 0x0600291A RID: 10522 RVA: 0x00043D5E File Offset: 0x00041F5E
		public FloatComparer()
			: this(1E-05f, false)
		{
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00043D6E File Offset: 0x00041F6E
		public FloatComparer(bool relative)
			: this(1E-05f, relative)
		{
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00043D7E File Offset: 0x00041F7E
		public FloatComparer(float error)
			: this(error, false)
		{
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00043D8A File Offset: 0x00041F8A
		public FloatComparer(float error, bool relative)
		{
			this.m_Error = error;
			this.m_Relative = relative;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x00043DA4 File Offset: 0x00041FA4
		public bool Equals(float a, float b)
		{
			return this.m_Relative ? FloatComparer.AreEqualRelative(a, b, this.m_Error) : FloatComparer.AreEqual(a, b, this.m_Error);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x00043DDC File Offset: 0x00041FDC
		public int GetHashCode(float obj)
		{
			return base.GetHashCode();
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x00043DF4 File Offset: 0x00041FF4
		public static bool AreEqual(float expected, float actual, float error)
		{
			return Math.Abs(actual - expected) <= error;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x00043E14 File Offset: 0x00042014
		public static bool AreEqualRelative(float expected, float actual, float error)
		{
			bool flag = expected == actual;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = Math.Abs(expected);
				float num2 = Math.Abs(actual);
				float num3 = Math.Abs((actual - expected) / ((num > num2) ? num : num2));
				flag2 = num3 <= error;
			}
			return flag2;
		}

		// Token: 0x04000F97 RID: 3991
		private readonly float m_Error;

		// Token: 0x04000F98 RID: 3992
		private readonly bool m_Relative;

		// Token: 0x04000F99 RID: 3993
		public static readonly FloatComparer s_ComparerWithDefaultTolerance = new FloatComparer(1E-05f);

		// Token: 0x04000F9A RID: 3994
		public const float kEpsilon = 1E-05f;
	}
}
