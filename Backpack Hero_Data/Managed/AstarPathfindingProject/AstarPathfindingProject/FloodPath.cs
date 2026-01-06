using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008B RID: 139
	public class FloodPath : Path
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00028D69 File Offset: 0x00026F69
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00028D6C File Offset: 0x00026F6C
		public bool HasPathTo(GraphNode node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00028D84 File Offset: 0x00026F84
		public GraphNode GetParent(GraphNode node)
		{
			return this.parents[node];
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00028DA1 File Offset: 0x00026FA1
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00028DB0 File Offset: 0x00026FB0
		public static FloodPath Construct(GraphNode start, OnPathDelegate callback = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00028DCD File Offset: 0x00026FCD
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00028DEB File Offset: 0x00026FEB
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00028E24 File Offset: 0x00027024
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<GraphNode, GraphNode>();
			this.saveParents = true;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00028E5C File Offset: 0x0002705C
		protected override void Prepare()
		{
			if (this.startNode == null)
			{
				this.nnConstraint.tags = this.enabledTags;
				NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
				this.startPoint = nearest.position;
				this.startNode = nearest.node;
			}
			else
			{
				if (this.startNode.Destroyed)
				{
					base.FailWithError("Start node has been destroyed");
					return;
				}
				this.startPoint = (Vector3)this.startNode.position;
			}
			if (this.startNode == null)
			{
				base.FailWithError("Couldn't find a close node to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00028F10 File Offset: 0x00027110
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.parents[this.startNode] = null;
			this.startNode.Open(this, pathNode, this.pathHandler);
			int searchedNodes = base.searchedNodes;
			base.searchedNodes = searchedNodes + 1;
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00028FE4 File Offset: 0x000271E4
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = base.searchedNodes;
				base.searchedNodes = searchedNodes + 1;
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.saveParents)
				{
					this.parents[this.currentR.node] = this.currentR.parent.node;
				}
				if (this.pathHandler.heap.isEmpty)
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
				this.currentR = this.pathHandler.heap.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (base.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
		}

		// Token: 0x040003ED RID: 1005
		public Vector3 originalStartPoint;

		// Token: 0x040003EE RID: 1006
		public Vector3 startPoint;

		// Token: 0x040003EF RID: 1007
		public GraphNode startNode;

		// Token: 0x040003F0 RID: 1008
		public bool saveParents = true;

		// Token: 0x040003F1 RID: 1009
		protected Dictionary<GraphNode, GraphNode> parents;
	}
}
