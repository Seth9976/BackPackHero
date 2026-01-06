using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000012 RID: 18
	public class ConstrainedPointSet : PointSet
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004F80 File Offset: 0x00003180
		public ConstrainedPointSet(List<TriangulationPoint> points, int[] index)
			: base(points)
		{
			this.EdgeIndex = index;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004F90 File Offset: 0x00003190
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004F98 File Offset: 0x00003198
		public int[] EdgeIndex { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004FA4 File Offset: 0x000031A4
		public override TriangulationMode TriangulationMode
		{
			get
			{
				return TriangulationMode.Constrained;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004FA8 File Offset: 0x000031A8
		public override void Prepare(TriangulationContext tcx)
		{
			base.Prepare(tcx);
			for (int i = 0; i < this.EdgeIndex.Length; i += 2)
			{
				tcx.NewConstraint(base.Points[this.EdgeIndex[i]], base.Points[this.EdgeIndex[i + 1]]);
			}
		}
	}
}
