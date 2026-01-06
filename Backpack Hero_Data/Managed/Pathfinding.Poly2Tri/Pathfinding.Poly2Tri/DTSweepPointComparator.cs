using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200000F RID: 15
	public class DTSweepPointComparator : IComparer<TriangulationPoint>
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004EB8 File Offset: 0x000030B8
		public int Compare(TriangulationPoint p1, TriangulationPoint p2)
		{
			if (p1.Y < p2.Y)
			{
				return -1;
			}
			if (p1.Y > p2.Y)
			{
				return 1;
			}
			if (p1.X < p2.X)
			{
				return -1;
			}
			if (p1.X > p2.X)
			{
				return 1;
			}
			return 0;
		}
	}
}
