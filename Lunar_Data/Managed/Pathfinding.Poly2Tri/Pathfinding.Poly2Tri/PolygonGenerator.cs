using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001D RID: 29
	public class PolygonGenerator
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000565C File Offset: 0x0000385C
		public static Polygon RandomCircleSweep(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			PolygonPoint[] array = new PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					if (i % 250 == 0)
					{
						num += scale / 2.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					else if (i % 50 == 0)
					{
						num += scale / 5.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					else
					{
						num += 25.0 * scale / (double)vertexCount * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					num = ((num <= scale / 2.0) ? num : (scale / 2.0));
					num = ((num >= scale / 10.0) ? num : (scale / 10.0));
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				PolygonPoint polygonPoint = new PolygonPoint(num * Math.Cos(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), num * Math.Sin(PolygonGenerator.PI_2 * (double)i / (double)vertexCount));
				array[i] = polygonPoint;
			}
			return new Polygon(array);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000057B8 File Offset: 0x000039B8
		public static Polygon RandomCircleSweep2(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			PolygonPoint[] array = new PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					num += scale / 5.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					num = ((num <= scale / 2.0) ? num : (scale / 2.0));
					num = ((num >= scale / 10.0) ? num : (scale / 10.0));
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				PolygonPoint polygonPoint = new PolygonPoint(num * Math.Cos(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), num * Math.Sin(PolygonGenerator.PI_2 * (double)i / (double)vertexCount));
				array[i] = polygonPoint;
			}
			return new Polygon(array);
		}

		// Token: 0x0400004B RID: 75
		private static readonly Random RNG = new Random();

		// Token: 0x0400004C RID: 76
		private static double PI_2 = 6.283185307179586;
	}
}
