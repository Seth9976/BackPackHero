using System;
using System.Collections.Generic;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200012B RID: 299
	public class ConstantPath : Path
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x000320BD File Offset: 0x000302BD
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool.GetPath<ConstantPath>();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000320CD File Offset: 0x000302CD
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000320F6 File Offset: 0x000302F6
		protected override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<GraphNode>.Release(ref this.allNodes);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00032111 File Offset: 0x00030311
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

		// Token: 0x06000912 RID: 2322 RVA: 0x00032150 File Offset: 0x00030350
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
			this.pathHandler.AddTemporaryNode(new TemporaryNode
			{
				type = TemporaryNodeType.Start,
				position = (Int3)nearest.position,
				associatedNode = this.startNode.NodeIndex
			});
			this.heuristicObjective = new HeuristicObjective(int3.zero, Heuristic.None, 0f);
			base.AddStartNodesToHeap();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00032202 File Offset: 0x00030402
		protected override void OnHeapExhausted()
		{
			base.CompleteState = PathCompleteState.Complete;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003220B File Offset: 0x0003040B
		protected override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
		{
			throw new InvalidOperationException("ConstantPaths do not have any end nodes");
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00032218 File Offset: 0x00030418
		public override void OnVisitNode(uint pathNode, uint hScore, uint gScore)
		{
			GraphNode node = this.pathHandler.GetNode(pathNode);
			if (this.endingCondition.TargetFound(node, hScore, gScore))
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.allNodes.Add(node);
		}

		// Token: 0x04000644 RID: 1604
		public GraphNode startNode;

		// Token: 0x04000645 RID: 1605
		public Vector3 startPoint;

		// Token: 0x04000646 RID: 1606
		public Vector3 originalStartPoint;

		// Token: 0x04000647 RID: 1607
		public List<GraphNode> allNodes;

		// Token: 0x04000648 RID: 1608
		public PathEndingCondition endingCondition;
	}
}
