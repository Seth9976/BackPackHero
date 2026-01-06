using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000004 RID: 4
	public class PolygonPoint : TriangulationPoint
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002744 File Offset: 0x00000944
		public PolygonPoint(double x, double y)
			: base(x, y)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002758 File Offset: 0x00000958
		public PolygonPoint Next { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002764 File Offset: 0x00000964
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000276C File Offset: 0x0000096C
		public PolygonPoint Previous { get; set; }
	}
}
