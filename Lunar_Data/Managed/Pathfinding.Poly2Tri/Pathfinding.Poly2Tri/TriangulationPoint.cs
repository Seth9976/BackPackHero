using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001A RID: 26
	public class TriangulationPoint
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005240 File Offset: 0x00003440
		public TriangulationPoint(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005258 File Offset: 0x00003458
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005260 File Offset: 0x00003460
		public List<DTSweepConstraint> Edges { get; private set; }

		// Token: 0x060000CD RID: 205 RVA: 0x0000526C File Offset: 0x0000346C
		public override string ToString()
		{
			return string.Concat(new object[] { "[", this.X, ",", this.Y, "]" });
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000052B8 File Offset: 0x000034B8
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000052C4 File Offset: 0x000034C4
		public float Xf
		{
			get
			{
				return (float)this.X;
			}
			set
			{
				this.X = (double)value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000052D0 File Offset: 0x000034D0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000052DC File Offset: 0x000034DC
		public float Yf
		{
			get
			{
				return (float)this.Y;
			}
			set
			{
				this.Y = (double)value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000052E8 File Offset: 0x000034E8
		public void AddEdge(DTSweepConstraint e)
		{
			if (this.Edges == null)
			{
				this.Edges = new List<DTSweepConstraint>();
			}
			this.Edges.Add(e);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005318 File Offset: 0x00003518
		public bool HasEdges
		{
			get
			{
				return this.Edges != null;
			}
		}

		// Token: 0x04000046 RID: 70
		public double X;

		// Token: 0x04000047 RID: 71
		public double Y;
	}
}
