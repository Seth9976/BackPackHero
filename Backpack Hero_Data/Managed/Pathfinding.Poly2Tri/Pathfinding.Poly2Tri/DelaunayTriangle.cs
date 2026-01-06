using System;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000006 RID: 6
	public class DelaunayTriangle
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000027C4 File Offset: 0x000009C4
		public DelaunayTriangle(TriangulationPoint p1, TriangulationPoint p2, TriangulationPoint p3)
		{
			this.Points[0] = p1;
			this.Points[1] = p2;
			this.Points[2] = p3;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002808 File Offset: 0x00000A08
		public bool IsInterior { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002814 File Offset: 0x00000A14
		public int IndexOf(TriangulationPoint p)
		{
			int num = this.Points.IndexOf(p);
			if (num == -1)
			{
				throw new Exception("Calling index with a point that doesn't exist in triangle");
			}
			return num;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002844 File Offset: 0x00000A44
		public int IndexCWFrom(TriangulationPoint p)
		{
			return (this.IndexOf(p) + 2) % 3;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002854 File Offset: 0x00000A54
		public int IndexCCWFrom(TriangulationPoint p)
		{
			return (this.IndexOf(p) + 1) % 3;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002864 File Offset: 0x00000A64
		public bool Contains(TriangulationPoint p)
		{
			return this.Points.Contains(p);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002874 File Offset: 0x00000A74
		private void MarkNeighbor(TriangulationPoint p1, TriangulationPoint p2, DelaunayTriangle t)
		{
			int num = this.EdgeIndex(p1, p2);
			if (num == -1)
			{
				throw new Exception("Error marking neighbors -- t doesn't contain edge p1-p2!");
			}
			this.Neighbors[num] = t;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028AC File Offset: 0x00000AAC
		public void MarkNeighbor(DelaunayTriangle t)
		{
			bool flag = t.Contains(this.Points[0]);
			bool flag2 = t.Contains(this.Points[1]);
			bool flag3 = t.Contains(this.Points[2]);
			if (flag2 && flag3)
			{
				this.Neighbors[0] = t;
				t.MarkNeighbor(this.Points[1], this.Points[2], this);
			}
			else if (flag && flag3)
			{
				this.Neighbors[1] = t;
				t.MarkNeighbor(this.Points[0], this.Points[2], this);
			}
			else
			{
				if (!flag || !flag2)
				{
					throw new Exception("Failed to mark neighbor, doesn't share an edge!");
				}
				this.Neighbors[2] = t;
				t.MarkNeighbor(this.Points[0], this.Points[1], this);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029B4 File Offset: 0x00000BB4
		public TriangulationPoint OppositePoint(DelaunayTriangle t, TriangulationPoint p)
		{
			return this.PointCWFrom(t.PointCWFrom(p));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029C4 File Offset: 0x00000BC4
		public DelaunayTriangle NeighborCWFrom(TriangulationPoint point)
		{
			return this.Neighbors[(this.Points.IndexOf(point) + 1) % 3];
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029E4 File Offset: 0x00000BE4
		public DelaunayTriangle NeighborCCWFrom(TriangulationPoint point)
		{
			return this.Neighbors[(this.Points.IndexOf(point) + 2) % 3];
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A04 File Offset: 0x00000C04
		public DelaunayTriangle NeighborAcrossFrom(TriangulationPoint point)
		{
			return this.Neighbors[this.Points.IndexOf(point)];
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A20 File Offset: 0x00000C20
		public TriangulationPoint PointCCWFrom(TriangulationPoint point)
		{
			return this.Points[(this.IndexOf(point) + 1) % 3];
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A38 File Offset: 0x00000C38
		public TriangulationPoint PointCWFrom(TriangulationPoint point)
		{
			return this.Points[(this.IndexOf(point) + 2) % 3];
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A50 File Offset: 0x00000C50
		private void RotateCW()
		{
			TriangulationPoint triangulationPoint = this.Points[2];
			this.Points[2] = this.Points[1];
			this.Points[1] = this.Points[0];
			this.Points[0] = triangulationPoint;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void Legalize(TriangulationPoint oPoint, TriangulationPoint nPoint)
		{
			this.RotateCW();
			this.Points[this.IndexCCWFrom(oPoint)] = nPoint;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Points[0],
				",",
				this.Points[1],
				",",
				this.Points[2]
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B1C File Offset: 0x00000D1C
		public void MarkNeighborEdges()
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.EdgeIsConstrained[i] && this.Neighbors[i] != null)
				{
					this.Neighbors[i].MarkConstrainedEdge(this.Points[(i + 1) % 3], this.Points[(i + 2) % 3]);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B90 File Offset: 0x00000D90
		public void MarkEdge(DelaunayTriangle triangle)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.EdgeIsConstrained[i])
				{
					triangle.MarkConstrainedEdge(this.Points[(i + 1) % 3], this.Points[(i + 2) % 3]);
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public void MarkEdge(List<DelaunayTriangle> tList)
		{
			foreach (DelaunayTriangle delaunayTriangle in tList)
			{
				for (int i = 0; i < 3; i++)
				{
					if (delaunayTriangle.EdgeIsConstrained[i])
					{
						this.MarkConstrainedEdge(delaunayTriangle.Points[(i + 1) % 3], delaunayTriangle.Points[(i + 2) % 3]);
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C90 File Offset: 0x00000E90
		public void MarkConstrainedEdge(int index)
		{
			this.EdgeIsConstrained[index] = true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public void MarkConstrainedEdge(DTSweepConstraint edge)
		{
			this.MarkConstrainedEdge(edge.P, edge.Q);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public void MarkConstrainedEdge(TriangulationPoint p, TriangulationPoint q)
		{
			int num = this.EdgeIndex(p, q);
			if (num != -1)
			{
				this.EdgeIsConstrained[num] = true;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public double Area()
		{
			double num = this.Points[0].X - this.Points[1].X;
			double num2 = this.Points[2].Y - this.Points[1].Y;
			return Math.Abs(num * num2 * 0.5);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D48 File Offset: 0x00000F48
		public TriangulationPoint Centroid()
		{
			double num = (this.Points[0].X + this.Points[1].X + this.Points[2].X) / 3.0;
			double num2 = (this.Points[0].Y + this.Points[1].Y + this.Points[2].Y) / 3.0;
			return new TriangulationPoint(num, num2);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002DDC File Offset: 0x00000FDC
		public int EdgeIndex(TriangulationPoint p1, TriangulationPoint p2)
		{
			int num = this.Points.IndexOf(p1);
			int num2 = this.Points.IndexOf(p2);
			bool flag = num == 0 || num2 == 0;
			bool flag2 = num == 1 || num2 == 1;
			bool flag3 = num == 2 || num2 == 2;
			if (flag2 && flag3)
			{
				return 0;
			}
			if (flag && flag3)
			{
				return 1;
			}
			if (flag && flag2)
			{
				return 2;
			}
			return -1;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E60 File Offset: 0x00001060
		public bool GetConstrainedEdgeCCW(TriangulationPoint p)
		{
			return this.EdgeIsConstrained[(this.IndexOf(p) + 2) % 3];
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E78 File Offset: 0x00001078
		public bool GetConstrainedEdgeCW(TriangulationPoint p)
		{
			return this.EdgeIsConstrained[(this.IndexOf(p) + 1) % 3];
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E90 File Offset: 0x00001090
		public bool GetConstrainedEdgeAcross(TriangulationPoint p)
		{
			return this.EdgeIsConstrained[this.IndexOf(p)];
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002EA4 File Offset: 0x000010A4
		public void SetConstrainedEdgeCCW(TriangulationPoint p, bool ce)
		{
			this.EdgeIsConstrained[(this.IndexOf(p) + 2) % 3] = ce;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void SetConstrainedEdgeCW(TriangulationPoint p, bool ce)
		{
			this.EdgeIsConstrained[(this.IndexOf(p) + 1) % 3] = ce;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EDC File Offset: 0x000010DC
		public void SetConstrainedEdgeAcross(TriangulationPoint p, bool ce)
		{
			this.EdgeIsConstrained[this.IndexOf(p)] = ce;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EF4 File Offset: 0x000010F4
		public bool GetDelaunayEdgeCCW(TriangulationPoint p)
		{
			return this.EdgeIsDelaunay[(this.IndexOf(p) + 2) % 3];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F0C File Offset: 0x0000110C
		public bool GetDelaunayEdgeCW(TriangulationPoint p)
		{
			return this.EdgeIsDelaunay[(this.IndexOf(p) + 1) % 3];
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F24 File Offset: 0x00001124
		public bool GetDelaunayEdgeAcross(TriangulationPoint p)
		{
			return this.EdgeIsDelaunay[this.IndexOf(p)];
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F38 File Offset: 0x00001138
		public void SetDelaunayEdgeCCW(TriangulationPoint p, bool ce)
		{
			this.EdgeIsDelaunay[(this.IndexOf(p) + 2) % 3] = ce;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F54 File Offset: 0x00001154
		public void SetDelaunayEdgeCW(TriangulationPoint p, bool ce)
		{
			this.EdgeIsDelaunay[(this.IndexOf(p) + 1) % 3] = ce;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F70 File Offset: 0x00001170
		public void SetDelaunayEdgeAcross(TriangulationPoint p, bool ce)
		{
			this.EdgeIsDelaunay[this.IndexOf(p)] = ce;
		}

		// Token: 0x0400000A RID: 10
		public FixedArray3<TriangulationPoint> Points;

		// Token: 0x0400000B RID: 11
		public FixedArray3<DelaunayTriangle> Neighbors;

		// Token: 0x0400000C RID: 12
		public FixedBitArray3 EdgeIsConstrained;

		// Token: 0x0400000D RID: 13
		public FixedBitArray3 EdgeIsDelaunay;
	}
}
