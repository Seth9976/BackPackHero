using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000005 RID: 5
	public class PolygonSet
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002778 File Offset: 0x00000978
		public PolygonSet()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000278C File Offset: 0x0000098C
		public PolygonSet(Polygon poly)
		{
			this._polygons.Add(poly);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027AC File Offset: 0x000009AC
		public void Add(Polygon p)
		{
			this._polygons.Add(p);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027BC File Offset: 0x000009BC
		public IEnumerable<Polygon> Polygons
		{
			get
			{
				return this._polygons;
			}
		}

		// Token: 0x04000009 RID: 9
		protected List<Polygon> _polygons = new List<Polygon>();
	}
}
