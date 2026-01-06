using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F1 RID: 241
	internal static class Geom
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00027208 File Offset: 0x00025408
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

		// Token: 0x060006E9 RID: 1769 RVA: 0x00027260 File Offset: 0x00025460
		public static bool VertCCW(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			return u._s * (v._t - w._t) + v._s * (w._t - u._t) + w._s * (u._t - v._t) >= 0f;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000272B5 File Offset: 0x000254B5
		public static bool VertEq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s == rhs._s && lhs._t == rhs._t;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000272D5 File Offset: 0x000254D5
		public static bool VertLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s < rhs._s || (lhs._s == rhs._s && lhs._t <= rhs._t);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00027308 File Offset: 0x00025508
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

		// Token: 0x060006ED RID: 1773 RVA: 0x00027388 File Offset: 0x00025588
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

		// Token: 0x060006EE RID: 1774 RVA: 0x000273E0 File Offset: 0x000255E0
		public static bool TransLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._t < rhs._t || (lhs._t == rhs._t && lhs._s <= rhs._s);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00027414 File Offset: 0x00025614
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

		// Token: 0x060006F0 RID: 1776 RVA: 0x00027494 File Offset: 0x00025694
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

		// Token: 0x060006F1 RID: 1777 RVA: 0x000274EC File Offset: 0x000256EC
		public static bool EdgeGoesLeft(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Dst, e._Org);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000274FF File Offset: 0x000256FF
		public static bool EdgeGoesRight(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Org, e._Dst);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00027512 File Offset: 0x00025712
		public static float VertL1dist(MeshUtils.Vertex u, MeshUtils.Vertex v)
		{
			return Math.Abs(u._s - v._s) + Math.Abs(u._t - v._t);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00027539 File Offset: 0x00025739
		public static void AddWinding(MeshUtils.Edge eDst, MeshUtils.Edge eSrc)
		{
			eDst._winding += eSrc._winding;
			eDst._Sym._winding += eSrc._Sym._winding;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0002756C File Offset: 0x0002576C
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

		// Token: 0x060006F6 RID: 1782 RVA: 0x000275C4 File Offset: 0x000257C4
		private static void Swap(ref MeshUtils.Vertex a, ref MeshUtils.Vertex b)
		{
			MeshUtils.Vertex vertex = a;
			a = b;
			b = vertex;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000275DC File Offset: 0x000257DC
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
