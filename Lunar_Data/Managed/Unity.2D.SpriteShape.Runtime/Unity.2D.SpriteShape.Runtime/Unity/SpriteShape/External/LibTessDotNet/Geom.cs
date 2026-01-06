using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x02000005 RID: 5
	internal static class Geom
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000246C File Offset: 0x0000066C
		public static bool IsWindingInside(WindingRule rule, int n)
		{
			switch (rule)
			{
			case WindingRule.EvenOdd:
				return (n & 1) == 1;
			case WindingRule.NonZero:
				return n != 0;
			case WindingRule.Positive:
				return n > 0;
			case WindingRule.Negative:
				return n < 0;
			case WindingRule.AbsGeqTwo:
				return n >= 2 || n <= -2;
			default:
				throw new Exception("Wrong winding rule");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024C4 File Offset: 0x000006C4
		public static bool VertCCW(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			return u._s * (v._t - w._t) + v._s * (w._t - u._t) + w._s * (u._t - v._t) >= 0f;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002519 File Offset: 0x00000719
		public static bool VertEq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s == rhs._s && lhs._t == rhs._t;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002539 File Offset: 0x00000739
		public static bool VertLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s < rhs._s || (lhs._s == rhs._s && lhs._t <= rhs._t);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000256C File Offset: 0x0000076C
		public static float EdgeEval(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._s - u._s;
			float num2 = w._s - v._s;
			if (num + num2 <= 0f)
			{
				return 0f;
			}
			if (num < num2)
			{
				return v._t - u._t + (u._t - w._t) * (num / (num + num2));
			}
			return v._t - w._t + (w._t - u._t) * (num2 / (num + num2));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025EC File Offset: 0x000007EC
		public static float EdgeSign(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._s - u._s;
			float num2 = w._s - v._s;
			if (num + num2 > 0f)
			{
				return (v._t - w._t) * num + (v._t - u._t) * num2;
			}
			return 0f;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002644 File Offset: 0x00000844
		public static bool TransLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._t < rhs._t || (lhs._t == rhs._t && lhs._s <= rhs._s);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002678 File Offset: 0x00000878
		public static float TransEval(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._t - u._t;
			float num2 = w._t - v._t;
			if (num + num2 <= 0f)
			{
				return 0f;
			}
			if (num < num2)
			{
				return v._s - u._s + (u._s - w._s) * (num / (num + num2));
			}
			return v._s - w._s + (w._s - u._s) * (num2 / (num + num2));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F8 File Offset: 0x000008F8
		public static float TransSign(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._t - u._t;
			float num2 = w._t - v._t;
			if (num + num2 > 0f)
			{
				return (v._s - w._s) * num + (v._s - u._s) * num2;
			}
			return 0f;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002750 File Offset: 0x00000950
		public static bool EdgeGoesLeft(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Dst, e._Org);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002763 File Offset: 0x00000963
		public static bool EdgeGoesRight(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Org, e._Dst);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002776 File Offset: 0x00000976
		public static float VertL1dist(MeshUtils.Vertex u, MeshUtils.Vertex v)
		{
			return Math.Abs(u._s - v._s) + Math.Abs(u._t - v._t);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000279D File Offset: 0x0000099D
		public static void AddWinding(MeshUtils.Edge eDst, MeshUtils.Edge eSrc)
		{
			eDst._winding += eSrc._winding;
			eDst._Sym._winding += eSrc._Sym._winding;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027D0 File Offset: 0x000009D0
		public static float Interpolate(float a, float x, float b, float y)
		{
			if (a < 0f)
			{
				a = 0f;
			}
			if (b < 0f)
			{
				b = 0f;
			}
			if (a > b)
			{
				return y + (x - y) * (b / (a + b));
			}
			if (b != 0f)
			{
				return x + (y - x) * (a / (a + b));
			}
			return (x + y) / 2f;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002828 File Offset: 0x00000A28
		private static void Swap(ref MeshUtils.Vertex a, ref MeshUtils.Vertex b)
		{
			MeshUtils.Vertex vertex = a;
			a = b;
			b = vertex;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002840 File Offset: 0x00000A40
		public static void EdgeIntersect(MeshUtils.Vertex o1, MeshUtils.Vertex d1, MeshUtils.Vertex o2, MeshUtils.Vertex d2, MeshUtils.Vertex v)
		{
			if (!Geom.VertLeq(o1, d1))
			{
				Geom.Swap(ref o1, ref d1);
			}
			if (!Geom.VertLeq(o2, d2))
			{
				Geom.Swap(ref o2, ref d2);
			}
			if (!Geom.VertLeq(o1, o2))
			{
				Geom.Swap(ref o1, ref o2);
				Geom.Swap(ref d1, ref d2);
			}
			if (!Geom.VertLeq(o2, d1))
			{
				v._s = (o2._s + d1._s) / 2f;
			}
			else if (Geom.VertLeq(d1, d2))
			{
				float num = Geom.EdgeEval(o1, o2, d1);
				float num2 = Geom.EdgeEval(o2, d1, d2);
				if (num + num2 < 0f)
				{
					num = -num;
					num2 = -num2;
				}
				v._s = Geom.Interpolate(num, o2._s, num2, d1._s);
			}
			else
			{
				float num3 = Geom.EdgeSign(o1, o2, d1);
				float num4 = -Geom.EdgeSign(o1, d2, d1);
				if (num3 + num4 < 0f)
				{
					num3 = -num3;
					num4 = -num4;
				}
				v._s = Geom.Interpolate(num3, o2._s, num4, d2._s);
			}
			if (!Geom.TransLeq(o1, d1))
			{
				Geom.Swap(ref o1, ref d1);
			}
			if (!Geom.TransLeq(o2, d2))
			{
				Geom.Swap(ref o2, ref d2);
			}
			if (!Geom.TransLeq(o1, o2))
			{
				Geom.Swap(ref o1, ref o2);
				Geom.Swap(ref d1, ref d2);
			}
			if (!Geom.TransLeq(o2, d1))
			{
				v._t = (o2._t + d1._t) / 2f;
				return;
			}
			if (Geom.TransLeq(d1, d2))
			{
				float num5 = Geom.TransEval(o1, o2, d1);
				float num6 = Geom.TransEval(o2, d1, d2);
				if (num5 + num6 < 0f)
				{
					num5 = -num5;
					num6 = -num6;
				}
				v._t = Geom.Interpolate(num5, o2._t, num6, d1._t);
				return;
			}
			float num7 = Geom.TransSign(o1, o2, d1);
			float num8 = -Geom.TransSign(o1, d2, d1);
			if (num7 + num8 < 0f)
			{
				num7 = -num7;
				num8 = -num8;
			}
			v._t = Geom.Interpolate(num7, o2._t, num8, d2._t);
		}
	}
}
