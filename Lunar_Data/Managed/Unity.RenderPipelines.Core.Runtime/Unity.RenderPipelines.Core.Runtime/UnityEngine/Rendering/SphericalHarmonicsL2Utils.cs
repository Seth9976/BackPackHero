using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007A RID: 122
	public class SphericalHarmonicsL2Utils
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00011D24 File Offset: 0x0000FF24
		public static void GetL1(SphericalHarmonicsL2 sh, out Vector3 L1_R, out Vector3 L1_G, out Vector3 L1_B)
		{
			L1_R = new Vector3(sh[0, 1], sh[0, 2], sh[0, 3]);
			L1_G = new Vector3(sh[1, 1], sh[1, 2], sh[1, 3]);
			L1_B = new Vector3(sh[2, 1], sh[2, 2], sh[2, 3]);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public static void GetL2(SphericalHarmonicsL2 sh, out Vector3 L2_0, out Vector3 L2_1, out Vector3 L2_2, out Vector3 L2_3, out Vector3 L2_4)
		{
			L2_0 = new Vector3(sh[0, 4], sh[1, 4], sh[2, 4]);
			L2_1 = new Vector3(sh[0, 5], sh[1, 5], sh[2, 5]);
			L2_2 = new Vector3(sh[0, 6], sh[1, 6], sh[2, 6]);
			L2_3 = new Vector3(sh[0, 7], sh[1, 7], sh[2, 7]);
			L2_4 = new Vector3(sh[0, 8], sh[1, 8], sh[2, 8]);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011E71 File Offset: 0x00010071
		public static void SetL0(ref SphericalHarmonicsL2 sh, Vector3 L0)
		{
			sh[0, 0] = L0.x;
			sh[1, 0] = L0.y;
			sh[2, 0] = L0.z;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011E9D File Offset: 0x0001009D
		public static void SetL1R(ref SphericalHarmonicsL2 sh, Vector3 L1_R)
		{
			sh[0, 1] = L1_R.x;
			sh[0, 2] = L1_R.y;
			sh[0, 3] = L1_R.z;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011EC9 File Offset: 0x000100C9
		public static void SetL1G(ref SphericalHarmonicsL2 sh, Vector3 L1_G)
		{
			sh[1, 1] = L1_G.x;
			sh[1, 2] = L1_G.y;
			sh[1, 3] = L1_G.z;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011EF5 File Offset: 0x000100F5
		public static void SetL1B(ref SphericalHarmonicsL2 sh, Vector3 L1_B)
		{
			sh[2, 1] = L1_B.x;
			sh[2, 2] = L1_B.y;
			sh[2, 3] = L1_B.z;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011F21 File Offset: 0x00010121
		public static void SetL1(ref SphericalHarmonicsL2 sh, Vector3 L1_R, Vector3 L1_G, Vector3 L1_B)
		{
			SphericalHarmonicsL2Utils.SetL1R(ref sh, L1_R);
			SphericalHarmonicsL2Utils.SetL1G(ref sh, L1_G);
			SphericalHarmonicsL2Utils.SetL1B(ref sh, L1_B);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011F38 File Offset: 0x00010138
		public static void SetCoefficient(ref SphericalHarmonicsL2 sh, int index, Vector3 coefficient)
		{
			sh[0, index] = coefficient.x;
			sh[1, index] = coefficient.y;
			sh[2, index] = coefficient.z;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011F64 File Offset: 0x00010164
		public static Vector3 GetCoefficient(SphericalHarmonicsL2 sh, int index)
		{
			return new Vector3(sh[0, index], sh[1, index], sh[2, index]);
		}
	}
}
