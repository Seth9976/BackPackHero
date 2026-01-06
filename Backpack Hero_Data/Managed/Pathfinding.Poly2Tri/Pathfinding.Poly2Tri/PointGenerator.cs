using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001C RID: 28
	public class PointGenerator
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000555C File Offset: 0x0000375C
		public static List<TriangulationPoint> UniformDistribution(int n, double scale)
		{
			List<TriangulationPoint> list = new List<TriangulationPoint>();
			for (int i = 0; i < n; i++)
			{
				list.Add(new TriangulationPoint(scale * (0.5 - PointGenerator.RNG.NextDouble()), scale * (0.5 - PointGenerator.RNG.NextDouble())));
			}
			return list;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000055BC File Offset: 0x000037BC
		public static List<TriangulationPoint> UniformGrid(int n, double scale)
		{
			double num = scale / (double)n;
			double num2 = 0.5 * scale;
			List<TriangulationPoint> list = new List<TriangulationPoint>();
			for (int i = 0; i < n + 1; i++)
			{
				double num3 = num2 - (double)i * num;
				for (int j = 0; j < n + 1; j++)
				{
					list.Add(new TriangulationPoint(num3, num2 - (double)j * num));
				}
			}
			return list;
		}

		// Token: 0x0400004A RID: 74
		private static readonly Random RNG = new Random();
	}
}
