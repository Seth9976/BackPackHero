using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001B RID: 27
	public class TriangulationUtil
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00005340 File Offset: 0x00003540
		public static bool SmartIncircle(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc, TriangulationPoint pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			double num7 = num5 - num6;
			if (num7 <= 0.0)
			{
				return false;
			}
			double num8 = pc.X - x;
			double num9 = pc.Y - y;
			double num10 = num8 * num2;
			double num11 = num * num9;
			double num12 = num10 - num11;
			if (num12 <= 0.0)
			{
				return false;
			}
			double num13 = num3 * num9;
			double num14 = num8 * num4;
			double num15 = num * num + num2 * num2;
			double num16 = num3 * num3 + num4 * num4;
			double num17 = num8 * num8 + num9 * num9;
			double num18 = num15 * (num13 - num14) + num16 * num12 + num17 * num7;
			return num18 > 0.0;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005434 File Offset: 0x00003634
		public static bool InScanArea(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc, TriangulationPoint pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			double num7 = num5 - num6;
			if (num7 <= 0.0)
			{
				return false;
			}
			double num8 = pc.X - x;
			double num9 = pc.Y - y;
			double num10 = num8 * num2;
			double num11 = num * num9;
			double num12 = num10 - num11;
			return num12 > 0.0;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000054D4 File Offset: 0x000036D4
		public static Orientation Orient2d(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc)
		{
			double num = (pa.X - pc.X) * (pb.Y - pc.Y);
			double num2 = (pa.Y - pc.Y) * (pb.X - pc.X);
			double num3 = num - num2;
			if (num3 > -TriangulationUtil.EPSILON && num3 < TriangulationUtil.EPSILON)
			{
				return Orientation.Collinear;
			}
			if (num3 > 0.0)
			{
				return Orientation.CCW;
			}
			return Orientation.CW;
		}

		// Token: 0x04000049 RID: 73
		public static double EPSILON = 1E-12;
	}
}
