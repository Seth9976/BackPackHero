using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008F RID: 143
	public class RandomPath : ABPath
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0002A1AB File Offset: 0x000283AB
		public override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002A1AE File Offset: 0x000283AE
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0002A1B4 File Offset: 0x000283B4
		protected override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0002A22D File Offset: 0x0002842D
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool.GetPath<RandomPath>();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0002A240 File Offset: 0x00028440
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			return this;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0002A28C File Offset: 0x0002848C
		protected override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0002A314 File Offset: 0x00028514
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startPoint = nearest.position;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.FailWithError("Couldn't find close nodes to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0002A3E0 File Offset: 0x000285E0
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			if (this.searchLength + this.spread <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(pathNode);
				return;
			}
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			int searchedNodes = base.searchedNodes;
			base.searchedNodes = searchedNodes + 1;
			if (this.pathHandler.heap.isEmpty)
			{
				base.FailWithError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0002A4C0 File Offset: 0x000286C0
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = base.searchedNodes;
				base.searchedNodes = searchedNodes + 1;
				if ((ulong)this.currentR.G >= (ulong)((long)this.searchLength))
				{
					if ((ulong)this.currentR.G > (ulong)((long)(this.searchLength + this.spread)))
					{
						if (this.chosenNodeR == null)
						{
							this.chosenNodeR = this.currentR;
						}
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					this.nodesEvaluatedRep++;
					if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
					{
						this.chosenNodeR = this.currentR;
					}
				}
				else if ((ulong)this.currentR.G > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.G;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					base.FailWithError("Not a single node found to search");
					break;
				}
				else
				{
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x04000401 RID: 1025
		public int searchLength;

		// Token: 0x04000402 RID: 1026
		public int spread = 5000;

		// Token: 0x04000403 RID: 1027
		public float aimStrength;

		// Token: 0x04000404 RID: 1028
		private PathNode chosenNodeR;

		// Token: 0x04000405 RID: 1029
		private PathNode maxGScoreNodeR;

		// Token: 0x04000406 RID: 1030
		private int maxGScore;

		// Token: 0x04000407 RID: 1031
		public Vector3 aim;

		// Token: 0x04000408 RID: 1032
		private int nodesEvaluatedRep;

		// Token: 0x04000409 RID: 1033
		private readonly Random rnd = new Random();
	}
}
