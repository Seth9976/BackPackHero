using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000013 RID: 19
	public class PointSet : Triangulatable
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005004 File Offset: 0x00003204
		public PointSet(List<TriangulationPoint> points)
		{
			this.Points = new List<TriangulationPoint>(points);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005018 File Offset: 0x00003218
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005020 File Offset: 0x00003220
		public IList<TriangulationPoint> Points { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000502C File Offset: 0x0000322C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005034 File Offset: 0x00003234
		public IList<DelaunayTriangle> Triangles { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005040 File Offset: 0x00003240
		public virtual TriangulationMode TriangulationMode
		{
			get
			{
				return TriangulationMode.Unconstrained;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005044 File Offset: 0x00003244
		public void AddTriangle(DelaunayTriangle t)
		{
			this.Triangles.Add(t);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005054 File Offset: 0x00003254
		public void AddTriangles(IEnumerable<DelaunayTriangle> list)
		{
			foreach (DelaunayTriangle delaunayTriangle in list)
			{
				this.Triangles.Add(delaunayTriangle);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000050BC File Offset: 0x000032BC
		public void ClearTriangles()
		{
			this.Triangles.Clear();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000050CC File Offset: 0x000032CC
		public virtual void Prepare(TriangulationContext tcx)
		{
			if (this.Triangles == null)
			{
				this.Triangles = new List<DelaunayTriangle>(this.Points.Count);
			}
			else
			{
				this.Triangles.Clear();
			}
			tcx.Points.AddRange(this.Points);
		}
	}
}
