using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000002 RID: 2
	public static class P2T
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		public static void Triangulate(PolygonSet ps)
		{
			TriangulationContext triangulationContext = P2T.CreateContext(P2T._defaultAlgorithm);
			foreach (Polygon polygon in ps.Polygons)
			{
				triangulationContext.PrepareTriangulation(polygon);
				P2T.Triangulate(triangulationContext);
				triangulationContext.Clear();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
		public static void Triangulate(Polygon p)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, p);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000217C File Offset: 0x0000037C
		public static void Triangulate(ConstrainedPointSet cps)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, cps);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000218C File Offset: 0x0000038C
		public static void Triangulate(PointSet ps)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, ps);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000039C
		public static TriangulationContext CreateContext(TriangulationAlgorithm algorithm)
		{
			if (algorithm != TriangulationAlgorithm.DTSweep)
			{
			}
			return new DTSweepContext();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		public static void Triangulate(TriangulationAlgorithm algorithm, Triangulatable t)
		{
			TriangulationContext triangulationContext = P2T.CreateContext(algorithm);
			triangulationContext.PrepareTriangulation(t);
			P2T.Triangulate(triangulationContext);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E0 File Offset: 0x000003E0
		public static void Triangulate(TriangulationContext tcx)
		{
			TriangulationAlgorithm algorithm = tcx.Algorithm;
			if (algorithm != TriangulationAlgorithm.DTSweep)
			{
			}
			DTSweep.Triangulate((DTSweepContext)tcx);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		public static void Warmup()
		{
		}

		// Token: 0x04000001 RID: 1
		private static TriangulationAlgorithm _defaultAlgorithm;
	}
}
