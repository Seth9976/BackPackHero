using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000132 RID: 306
	public class RandomPath : ABPath
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00018013 File Offset: 0x00016213
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00018013 File Offset: 0x00016213
		public override bool endPointKnownBeforeCalculation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00033374 File Offset: 0x00031574
		protected override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.aimStrength = 0f;
			this.chosenPathNodeIndex = uint.MaxValue;
			this.maxGScorePathNodeIndex = uint.MaxValue;
			this.chosenPathNodeGScore = 0U;
			this.maxGScore = 0U;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000333F4 File Offset: 0x000315F4
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool.GetPath<RandomPath>();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00033405 File Offset: 0x00031605
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			return this;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003343A File Offset: 0x0003163A
		protected override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.originalEndPoint = this.endPoint;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00033474 File Offset: 0x00031674
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startPoint = nearest.position;
			this.endPoint = this.startPoint;
			if (nearest.node == null)
			{
				base.FailWithError("Couldn't find close nodes to the start point");
				return;
			}
			if (!base.CanTraverse(nearest.node))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.heuristicScale = this.aimStrength;
			this.pathHandler.AddTemporaryNode(new TemporaryNode
			{
				type = TemporaryNodeType.Start,
				position = (Int3)nearest.position,
				associatedNode = nearest.node.NodeIndex
			});
			this.heuristicObjective = new HeuristicObjective((int3)((Int3)this.aim), this.heuristic, this.heuristicScale);
			base.AddStartNodesToHeap();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003356C File Offset: 0x0003176C
		protected override void OnHeapExhausted()
		{
			if (this.chosenPathNodeIndex == 4294967295U && this.maxGScorePathNodeIndex != 4294967295U)
			{
				this.chosenPathNodeIndex = this.maxGScorePathNodeIndex;
				this.chosenPathNodeGScore = this.maxGScore;
			}
			if (this.chosenPathNodeIndex != 4294967295U)
			{
				this.OnFoundEndNode(this.chosenPathNodeIndex, 0U, this.chosenPathNodeGScore);
				return;
			}
			base.FailWithError("Not a single node found to search");
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000335CC File Offset: 0x000317CC
		protected override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
		{
			if (this.pathHandler.IsTemporaryNode(pathNode))
			{
				base.OnFoundEndNode(pathNode, hScore, gScore);
				return;
			}
			GraphNode node = this.pathHandler.GetNode(pathNode);
			this.endPoint = node.RandomPointOnSurface();
			this.cost = gScore;
			base.CompleteState = PathCompleteState.Complete;
			this.Trace(pathNode);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00033620 File Offset: 0x00031820
		public override void OnVisitNode(uint pathNode, uint hScore, uint gScore)
		{
			if (base.CompleteState != PathCompleteState.NotCalculated)
			{
				return;
			}
			if ((ulong)gScore >= (ulong)((long)this.searchLength))
			{
				if ((ulong)gScore > (ulong)((long)(this.searchLength + this.spread)))
				{
					if (this.chosenPathNodeIndex == 4294967295U)
					{
						this.chosenPathNodeIndex = pathNode;
						this.chosenPathNodeGScore = gScore;
					}
					this.OnFoundEndNode(this.chosenPathNodeIndex, 0U, this.chosenPathNodeGScore);
					return;
				}
				this.nodesEvaluatedRep++;
				if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
				{
					this.chosenPathNodeIndex = pathNode;
					this.chosenPathNodeGScore = gScore;
					return;
				}
			}
			else if (gScore > this.maxGScore)
			{
				this.maxGScore = gScore;
				this.maxGScorePathNodeIndex = pathNode;
			}
		}

		// Token: 0x0400065F RID: 1631
		public int searchLength;

		// Token: 0x04000660 RID: 1632
		public int spread = 5000;

		// Token: 0x04000661 RID: 1633
		public float aimStrength;

		// Token: 0x04000662 RID: 1634
		private uint chosenPathNodeIndex;

		// Token: 0x04000663 RID: 1635
		private uint chosenPathNodeGScore;

		// Token: 0x04000664 RID: 1636
		private uint maxGScorePathNodeIndex;

		// Token: 0x04000665 RID: 1637
		private uint maxGScore;

		// Token: 0x04000666 RID: 1638
		public Vector3 aim;

		// Token: 0x04000667 RID: 1639
		private int nodesEvaluatedRep;

		// Token: 0x04000668 RID: 1640
		private readonly global::System.Random rnd = new global::System.Random();
	}
}
