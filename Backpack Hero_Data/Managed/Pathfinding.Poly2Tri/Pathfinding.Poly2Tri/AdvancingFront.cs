using System;
using System.Text;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x02000007 RID: 7
	public class AdvancingFront
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002F88 File Offset: 0x00001188
		public AdvancingFront(AdvancingFrontNode head, AdvancingFrontNode tail)
		{
			this.Head = head;
			this.Tail = tail;
			this.Search = head;
			this.AddNode(head);
			this.AddNode(tail);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FB4 File Offset: 0x000011B4
		public void AddNode(AdvancingFrontNode node)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FB8 File Offset: 0x000011B8
		public void RemoveNode(AdvancingFrontNode node)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FBC File Offset: 0x000011BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (AdvancingFrontNode advancingFrontNode = this.Head; advancingFrontNode != this.Tail; advancingFrontNode = advancingFrontNode.Next)
			{
				stringBuilder.Append(advancingFrontNode.Point.X).Append("->");
			}
			stringBuilder.Append(this.Tail.Point.X);
			return stringBuilder.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003028 File Offset: 0x00001228
		private AdvancingFrontNode FindSearchNode(double x)
		{
			return this.Search;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003030 File Offset: 0x00001230
		public AdvancingFrontNode LocateNode(TriangulationPoint point)
		{
			return this.LocateNode(point.X);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003040 File Offset: 0x00001240
		private AdvancingFrontNode LocateNode(double x)
		{
			AdvancingFrontNode advancingFrontNode = this.FindSearchNode(x);
			if (x < advancingFrontNode.Value)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null)
				{
					if (x >= advancingFrontNode.Value)
					{
						this.Search = advancingFrontNode;
						return advancingFrontNode;
					}
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null)
				{
					if (x < advancingFrontNode.Value)
					{
						this.Search = advancingFrontNode.Prev;
						return advancingFrontNode.Prev;
					}
				}
			}
			return null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000030C0 File Offset: 0x000012C0
		public AdvancingFrontNode LocatePoint(TriangulationPoint point)
		{
			double x = point.X;
			AdvancingFrontNode advancingFrontNode = this.FindSearchNode(x);
			double x2 = advancingFrontNode.Point.X;
			if (x == x2)
			{
				if (point != advancingFrontNode.Point)
				{
					if (point == advancingFrontNode.Prev.Point)
					{
						advancingFrontNode = advancingFrontNode.Prev;
					}
					else
					{
						if (point != advancingFrontNode.Next.Point)
						{
							throw new Exception("Failed to find Node for given afront point");
						}
						advancingFrontNode = advancingFrontNode.Next;
					}
				}
			}
			else if (x < x2)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null)
				{
					if (point == advancingFrontNode.Point)
					{
						break;
					}
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null)
				{
					if (point == advancingFrontNode.Point)
					{
						break;
					}
				}
			}
			this.Search = advancingFrontNode;
			return advancingFrontNode;
		}

		// Token: 0x0400000F RID: 15
		public AdvancingFrontNode Head;

		// Token: 0x04000010 RID: 16
		public AdvancingFrontNode Tail;

		// Token: 0x04000011 RID: 17
		protected AdvancingFrontNode Search;
	}
}
