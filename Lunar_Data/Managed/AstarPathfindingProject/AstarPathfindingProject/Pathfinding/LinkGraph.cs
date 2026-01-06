using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C9 RID: 201
	[JsonOptIn]
	[Preserve]
	public class LinkGraph : NavGraph
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00016F22 File Offset: 0x00015122
		public override bool isScanned
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00018013 File Offset: 0x00016213
		public override bool persistent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00018013 File Offset: 0x00016213
		public override bool showInInspector
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00021B2B File Offset: 0x0001FD2B
		public override int CountNodes()
		{
			return this.nodeCount;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00021B33 File Offset: 0x0001FD33
		protected override void DestroyAllNodes()
		{
			base.DestroyAllNodes();
			this.nodes = new LinkNode[0];
			this.nodeCount = 0;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021B50 File Offset: 0x0001FD50
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodeCount; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00021B88 File Offset: 0x0001FD88
		internal LinkNode AddNode()
		{
			base.AssertSafeToUpdateGraph();
			if (this.nodeCount >= this.nodes.Length)
			{
				Memory.Realloc<LinkNode>(ref this.nodes, Mathf.Max(16, this.nodeCount * 2));
			}
			this.nodeCount++;
			LinkNode[] array = this.nodes;
			int num = this.nodeCount - 1;
			LinkNode linkNode = new LinkNode(this.active);
			linkNode.nodeInGraphIndex = this.nodeCount - 1;
			linkNode.GraphIndex = this.graphIndex;
			linkNode.Walkable = true;
			LinkNode linkNode2 = linkNode;
			array[num] = linkNode;
			return linkNode2;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00021C14 File Offset: 0x0001FE14
		internal void RemoveNode(LinkNode node)
		{
			if (this.nodes[node.nodeInGraphIndex] != node)
			{
				throw new ArgumentException("Node is not in this graph");
			}
			this.nodeCount--;
			this.nodes[node.nodeInGraphIndex] = this.nodes[this.nodeCount];
			this.nodes[node.nodeInGraphIndex].nodeInGraphIndex = node.nodeInGraphIndex;
			this.nodes[this.nodeCount] = null;
			node.Destroy();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00021C90 File Offset: 0x0001FE90
		public override float NearestNodeDistanceSqrLowerBound(Vector3 position, NNConstraint constraint = null)
		{
			return float.PositiveInfinity;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021C98 File Offset: 0x0001FE98
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, float maxDistanceSqr)
		{
			return default(NNInfo);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021CAE File Offset: 0x0001FEAE
		public override void OnDrawGizmos(DrawingData gizmos, bool drawNodes, RedrawScope redrawScope)
		{
			base.OnDrawGizmos(gizmos, drawNodes, redrawScope);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00021CB9 File Offset: 0x0001FEB9
		protected override IGraphUpdatePromise ScanInternal()
		{
			return new LinkGraph.LinkGraphUpdatePromise
			{
				graph = this
			};
		}

		// Token: 0x04000458 RID: 1112
		private LinkNode[] nodes = new LinkNode[0];

		// Token: 0x04000459 RID: 1113
		private int nodeCount;

		// Token: 0x020000CA RID: 202
		private class LinkGraphUpdatePromise : IGraphUpdatePromise
		{
			// Token: 0x06000668 RID: 1640 RVA: 0x00021CDB File Offset: 0x0001FEDB
			public void Apply(IGraphUpdateContext ctx)
			{
				this.graph.DestroyAllNodes();
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x000146B9 File Offset: 0x000128B9
			public IEnumerator<JobHandle> Prepare()
			{
				return null;
			}

			// Token: 0x0400045A RID: 1114
			public LinkGraph graph;
		}
	}
}
