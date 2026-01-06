using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000014 RID: 20
	public interface Triangulatable
	{
		// Token: 0x060000AE RID: 174
		void Prepare(TriangulationContext tcx);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AF RID: 175
		IList<TriangulationPoint> Points { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B0 RID: 176
		IList<DelaunayTriangle> Triangles { get; }

		// Token: 0x060000B1 RID: 177
		void AddTriangle(DelaunayTriangle t);

		// Token: 0x060000B2 RID: 178
		void AddTriangles(IEnumerable<DelaunayTriangle> list);

		// Token: 0x060000B3 RID: 179
		void ClearTriangles();

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B4 RID: 180
		TriangulationMode TriangulationMode { get; }
	}
}
