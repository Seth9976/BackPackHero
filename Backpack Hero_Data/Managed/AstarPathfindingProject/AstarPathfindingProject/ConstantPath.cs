using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000088 RID: 136
	public class ConstantPath : Path
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000289E5 File Offset: 0x00026BE5
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000289E8 File Offset: 0x00026BE8
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool.GetPath<ConstantPath>();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000289F8 File Offset: 0x00026BF8
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00028A21 File Offset: 0x00026C21
		protected override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<GraphNode>.Release(ref this.allNodes);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00028A3C File Offset: 0x00026C3C
		protected override void Reset()
		{
			base.Reset();
			this.allNodes = ListPool<GraphNode>.Claim();
			this.endingCondition = null;
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00028A7C File Offset: 0x00026C7C
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.FailWithError("Could not find close node to the start point");
				return;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00028AD4 File Offset: 0x00026CD4
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			int searchedNodes = base.searchedNodes;
			base.searchedNodes = searchedNodes + 1;
			pathNode.flag1 = true;
			this.allNodes.Add(this.startNode);
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00028BB0 File Offset: 0x00026DB0
		protected override void Cleanup()
		{
			int count = this.allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.pathHandler.GetPathNode(this.allNodes[i]).flag1 = false;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00028BF4 File Offset: 0x00026DF4
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = base.searchedNodes;
				base.searchedNodes = searchedNodes + 1;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
				if (!this.currentR.flag1)
				{
					this.allNodes.Add(this.currentR.node);
					this.currentR.flag1 = true;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
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

		// Token: 0x040003E7 RID: 999
		public GraphNode startNode;

		// Token: 0x040003E8 RID: 1000
		public Vector3 startPoint;

		// Token: 0x040003E9 RID: 1001
		public Vector3 originalStartPoint;

		// Token: 0x040003EA RID: 1002
		public List<GraphNode> allNodes;

		// Token: 0x040003EB RID: 1003
		public PathEndingCondition endingCondition;
	}
}
