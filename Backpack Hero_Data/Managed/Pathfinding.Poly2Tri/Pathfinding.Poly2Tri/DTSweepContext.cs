using System;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200000C RID: 12
	public class DTSweepContext : TriangulationContext
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000499C File Offset: 0x00002B9C
		public DTSweepContext()
		{
			this.Clear();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000049E4 File Offset: 0x00002BE4
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000049EC File Offset: 0x00002BEC
		public TriangulationPoint Head { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000049F8 File Offset: 0x00002BF8
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00004A00 File Offset: 0x00002C00
		public TriangulationPoint Tail { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00004A14 File Offset: 0x00002C14
		public override bool IsDebugEnabled
		{
			get
			{
				return base.IsDebugEnabled;
			}
			protected set
			{
				if (value && base.DebugContext == null)
				{
					base.DebugContext = new DTSweepDebugContext(this);
				}
				base.IsDebugEnabled = value;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004A48 File Offset: 0x00002C48
		public void RemoveFromList(DelaunayTriangle triangle)
		{
			this.Triangles.Remove(triangle);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A58 File Offset: 0x00002C58
		public void MeshClean(DelaunayTriangle triangle)
		{
			this.MeshCleanReq(triangle);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A64 File Offset: 0x00002C64
		private void MeshCleanReq(DelaunayTriangle triangle)
		{
			if (triangle != null && !triangle.IsInterior)
			{
				triangle.IsInterior = true;
				base.Triangulatable.AddTriangle(triangle);
				for (int i = 0; i < 3; i++)
				{
					if (!triangle.EdgeIsConstrained[i])
					{
						this.MeshCleanReq(triangle.Neighbors[i]);
					}
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004ACC File Offset: 0x00002CCC
		public override void Clear()
		{
			base.Clear();
			this.Triangles.Clear();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public void AddNode(AdvancingFrontNode node)
		{
			this.Front.AddNode(node);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public void RemoveNode(AdvancingFrontNode node)
		{
			this.Front.RemoveNode(node);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004B00 File Offset: 0x00002D00
		public AdvancingFrontNode LocateNode(TriangulationPoint point)
		{
			return this.Front.LocateNode(point);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004B10 File Offset: 0x00002D10
		public void CreateAdvancingFront()
		{
			DelaunayTriangle delaunayTriangle = new DelaunayTriangle(this.Points[0], this.Tail, this.Head);
			this.Triangles.Add(delaunayTriangle);
			AdvancingFrontNode advancingFrontNode = new AdvancingFrontNode(delaunayTriangle.Points[1]);
			advancingFrontNode.Triangle = delaunayTriangle;
			AdvancingFrontNode advancingFrontNode2 = new AdvancingFrontNode(delaunayTriangle.Points[0]);
			advancingFrontNode2.Triangle = delaunayTriangle;
			AdvancingFrontNode advancingFrontNode3 = new AdvancingFrontNode(delaunayTriangle.Points[2]);
			this.Front = new AdvancingFront(advancingFrontNode, advancingFrontNode3);
			this.Front.AddNode(advancingFrontNode2);
			this.Front.Head.Next = advancingFrontNode2;
			advancingFrontNode2.Next = this.Front.Tail;
			advancingFrontNode2.Prev = this.Front.Head;
			this.Front.Tail.Prev = advancingFrontNode2;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public void MapTriangleToNodes(DelaunayTriangle t)
		{
			for (int i = 0; i < 3; i++)
			{
				if (t.Neighbors[i] == null)
				{
					AdvancingFrontNode advancingFrontNode = this.Front.LocatePoint(t.PointCWFrom(t.Points[i]));
					if (advancingFrontNode != null)
					{
						advancingFrontNode.Triangle = t;
					}
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004C44 File Offset: 0x00002E44
		public override void PrepareTriangulation(Triangulatable t)
		{
			base.PrepareTriangulation(t);
			double num2;
			double num = (num2 = this.Points[0].X);
			double num4;
			double num3 = (num4 = this.Points[0].Y);
			foreach (TriangulationPoint triangulationPoint in this.Points)
			{
				if (triangulationPoint.X > num2)
				{
					num2 = triangulationPoint.X;
				}
				if (triangulationPoint.X < num)
				{
					num = triangulationPoint.X;
				}
				if (triangulationPoint.Y > num4)
				{
					num4 = triangulationPoint.Y;
				}
				if (triangulationPoint.Y < num3)
				{
					num3 = triangulationPoint.Y;
				}
			}
			double num5 = (double)this.ALPHA * (num2 - num);
			double num6 = (double)this.ALPHA * (num4 - num3);
			TriangulationPoint triangulationPoint2 = new TriangulationPoint(num2 + num5, num3 - num6);
			TriangulationPoint triangulationPoint3 = new TriangulationPoint(num - num5, num3 - num6);
			this.Head = triangulationPoint2;
			this.Tail = triangulationPoint3;
			this.Points.Sort(this._comparator);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004D84 File Offset: 0x00002F84
		public void FinalizeTriangulation()
		{
			base.Triangulatable.AddTriangles(this.Triangles);
			this.Triangles.Clear();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public override TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b)
		{
			return new DTSweepConstraint(a, b);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public override TriangulationAlgorithm Algorithm
		{
			get
			{
				return TriangulationAlgorithm.DTSweep;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly float ALPHA = 0.3f;

		// Token: 0x0400001F RID: 31
		public AdvancingFront Front;

		// Token: 0x04000020 RID: 32
		public DTSweepBasin Basin = new DTSweepBasin();

		// Token: 0x04000021 RID: 33
		public DTSweepEdgeEvent EdgeEvent = new DTSweepEdgeEvent();

		// Token: 0x04000022 RID: 34
		private DTSweepPointComparator _comparator = new DTSweepPointComparator();
	}
}
