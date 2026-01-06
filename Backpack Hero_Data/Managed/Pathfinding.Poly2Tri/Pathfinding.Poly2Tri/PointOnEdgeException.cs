using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000010 RID: 16
	public class PointOnEdgeException : NotImplementedException
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004F14 File Offset: 0x00003114
		public PointOnEdgeException(string message, TriangulationPoint a, TriangulationPoint b, TriangulationPoint c)
			: base(string.Concat(new string[]
			{
				message,
				"\n",
				a.ToString(),
				"\n",
				b.ToString(),
				"\n",
				c.ToString()
			}))
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Token: 0x0400002C RID: 44
		public readonly TriangulationPoint A;

		// Token: 0x0400002D RID: 45
		public readonly TriangulationPoint B;

		// Token: 0x0400002E RID: 46
		public readonly TriangulationPoint C;
	}
}
