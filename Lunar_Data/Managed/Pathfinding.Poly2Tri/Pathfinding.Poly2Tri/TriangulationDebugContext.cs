using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000018 RID: 24
	public abstract class TriangulationDebugContext
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005230 File Offset: 0x00003430
		public TriangulationDebugContext(TriangulationContext tcx)
		{
			this._tcx = tcx;
		}

		// Token: 0x060000C9 RID: 201
		public abstract void Clear();

		// Token: 0x04000041 RID: 65
		protected TriangulationContext _tcx;
	}
}
