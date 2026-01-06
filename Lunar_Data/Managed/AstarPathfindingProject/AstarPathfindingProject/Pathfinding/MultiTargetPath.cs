using System;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000131 RID: 305
	public class MultiTargetPath : ABPath
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000327EF File Offset: 0x000309EF
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000327F7 File Offset: 0x000309F7
		public bool inverted { get; protected set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00018013 File Offset: 0x00016213
		public override bool endPointKnownBeforeCalculation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00032816 File Offset: 0x00030A16
		public static MultiTargetPath Construct(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(target, startPoints, callbackDelegates, callback);
			multiTargetPath.inverted = true;
			return multiTargetPath;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00032828 File Offset: 0x00030A28
		public static MultiTargetPath Construct(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath path = PathPool.GetPath<MultiTargetPath>();
			path.Setup(start, targets, callbackDelegates, callback);
			return path;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003283C File Offset: 0x00030A3C
		protected void Setup(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback)
		{
			this.inverted = false;
			this.callback = callback;
			this.callbacks = callbackDelegates;
			if (this.callbacks != null && this.callbacks.Length != targets.Length)
			{
				throw new ArgumentException("The targets array must have the same length as the callbackDelegates array");
			}
			this.targetPoints = targets;
			this.originalStartPoint = start;
			this.startPoint = start;
			if (targets.Length == 0)
			{
				base.FailWithError("No targets were assigned to the MultiTargetPath");
				return;
			}
			this.endPoint = targets[0];
			this.originalTargetPoints = new Vector3[this.targetPoints.Length];
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				this.originalTargetPoints[i] = this.targetPoints[i];
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000328EF File Offset: 0x00030AEF
		protected override void Reset()
		{
			base.Reset();
			this.pathsForAll = true;
			this.chosenTarget = -1;
			this.inverted = true;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003290C File Offset: 0x00030B0C
		protected override void OnEnterPool()
		{
			if (this.vectorPaths != null)
			{
				for (int i = 0; i < this.vectorPaths.Length; i++)
				{
					if (this.vectorPaths[i] != null)
					{
						ListPool<Vector3>.Release(this.vectorPaths[i]);
					}
				}
			}
			this.vectorPaths = null;
			this.vectorPath = null;
			if (this.nodePaths != null)
			{
				for (int j = 0; j < this.nodePaths.Length; j++)
				{
					if (this.nodePaths[j] != null)
					{
						ListPool<GraphNode>.Release(this.nodePaths[j]);
					}
				}
			}
			this.nodePaths = null;
			this.path = null;
			this.callbacks = null;
			this.targetNodes = null;
			this.targetsFound = null;
			this.targetPathCosts = null;
			this.targetPoints = null;
			this.originalTargetPoints = null;
			base.OnEnterPool();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000329CC File Offset: 0x00030BCC
		private void ChooseShortestPath()
		{
			this.chosenTarget = -1;
			if (this.nodePaths != null)
			{
				uint num = uint.MaxValue;
				for (int i = 0; i < this.nodePaths.Length; i++)
				{
					if (this.nodePaths[i] != null)
					{
						uint num2 = this.targetPathCosts[i];
						if (num2 < num)
						{
							this.chosenTarget = i;
							num = num2;
						}
					}
				}
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00032A20 File Offset: 0x00030C20
		private void SetPathParametersForReturn(int target)
		{
			this.path = this.nodePaths[target];
			this.vectorPath = this.vectorPaths[target];
			if (this.inverted)
			{
				this.startPoint = this.targetPoints[target];
				this.originalStartPoint = this.originalTargetPoints[target];
			}
			else
			{
				this.endPoint = this.targetPoints[target];
				this.originalEndPoint = this.originalTargetPoints[target];
			}
			this.cost = ((this.path != null) ? this.targetPathCosts[target] : 0U);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00032AB4 File Offset: 0x00030CB4
		protected override void ReturnPath()
		{
			if (base.error)
			{
				if (this.callbacks != null)
				{
					for (int i = 0; i < this.callbacks.Length; i++)
					{
						if (this.callbacks[i] != null)
						{
							this.callbacks[i](this);
						}
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				return;
			}
			bool flag = false;
			if (this.inverted)
			{
				this.endPoint = this.startPoint;
				this.originalEndPoint = this.originalStartPoint;
			}
			for (int j = 0; j < this.nodePaths.Length; j++)
			{
				if (this.nodePaths[j] != null)
				{
					this.completeState = PathCompleteState.Complete;
					flag = true;
				}
				else
				{
					this.completeState = PathCompleteState.Error;
				}
				if (this.callbacks != null && this.callbacks[j] != null)
				{
					this.SetPathParametersForReturn(j);
					this.callbacks[j](this);
					this.vectorPaths[j] = this.vectorPath;
				}
			}
			if (flag)
			{
				this.completeState = PathCompleteState.Complete;
				this.SetPathParametersForReturn(this.chosenTarget);
			}
			else
			{
				this.completeState = PathCompleteState.Error;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00032BCC File Offset: 0x00030DCC
		protected void RebuildOpenList()
		{
			BinaryHeap heap = this.pathHandler.heap;
			for (int i = 0; i < heap.numberOfItems; i++)
			{
				uint pathNodeIndex = heap.GetPathNodeIndex(i);
				Int3 @int;
				if (this.pathHandler.IsTemporaryNode(pathNodeIndex))
				{
					@int = this.pathHandler.GetTemporaryNode(pathNodeIndex).position;
				}
				else
				{
					@int = this.pathHandler.GetNode(pathNodeIndex).DecodeVariantPosition(pathNodeIndex, this.pathHandler.pathNodes[pathNodeIndex].fractionAlongEdge);
				}
				uint num = (uint)this.heuristicObjective.Calculate((int3)@int, 0U);
				heap.SetH(i, num);
			}
			this.pathHandler.heap.Rebuild(this.pathHandler.pathNodes);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00032C84 File Offset: 0x00030E84
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			GraphNode node = nearest.node;
			if (this.endingCondition != null)
			{
				base.FailWithError("Multi target paths cannot use custom ending conditions");
				return;
			}
			if (node == null)
			{
				base.FailWithError("Could not find start node for multi target path");
				return;
			}
			if (!base.CanTraverse(node))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.pathHandler.AddTemporaryNode(new TemporaryNode
			{
				associatedNode = nearest.node.NodeIndex,
				position = (Int3)nearest.position,
				type = TemporaryNodeType.Start
			});
			this.vectorPaths = new List<Vector3>[this.targetPoints.Length];
			this.nodePaths = new List<GraphNode>[this.targetPoints.Length];
			this.targetNodes = new GraphNode[this.targetPoints.Length];
			this.targetsFound = new bool[this.targetPoints.Length];
			this.targetPathCosts = new uint[this.targetPoints.Length];
			this.targetNodeCount = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				Vector3 vector = this.targetPoints[i];
				NNInfo nearest2 = AstarPath.active.GetNearest(vector, this.nnConstraint);
				this.targetNodes[i] = nearest2.node;
				this.targetPoints[i] = nearest2.position;
				if (this.targetNodes[i] != null)
				{
					flag3 = true;
				}
				bool flag4 = false;
				if (nearest2.node != null && base.CanTraverse(nearest2.node))
				{
					flag = true;
				}
				else
				{
					flag4 = true;
				}
				if (nearest2.node != null && nearest2.node.Area == node.Area)
				{
					flag2 = true;
				}
				else
				{
					flag4 = true;
				}
				if (flag4)
				{
					this.targetsFound[i] = true;
				}
				else
				{
					this.targetNodeCount++;
					if (!this.EndPointGridGraphSpecialCase(nearest2.node, vector, i))
					{
						this.pathHandler.AddTemporaryNode(new TemporaryNode
						{
							associatedNode = nearest2.node.NodeIndex,
							position = (Int3)nearest2.position,
							targetIndex = i,
							type = TemporaryNodeType.End
						});
					}
				}
			}
			this.startPoint = nearest.position;
			if (!flag3)
			{
				base.FailWithError("Couldn't find a valid node close to the any of the end points");
				return;
			}
			if (!flag)
			{
				base.FailWithError("No target nodes could be traversed");
				return;
			}
			if (!flag2)
			{
				base.FailWithError("There's no valid path to any of the given targets");
				return;
			}
			base.MarkNodesAdjacentToTemporaryEndNodes();
			base.AddStartNodesToHeap();
			this.RecalculateHTarget();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00032F48 File Offset: 0x00031148
		private void RecalculateHTarget()
		{
			if (this.pathsForAll)
			{
				int3 @int = base.FirstTemporaryEndNode();
				this.heuristicObjective = new HeuristicObjective(@int, @int, this.heuristic, this.heuristicScale, 0U, null);
			}
			else
			{
				int3 int2;
				int3 int3;
				base.TemporaryEndNodesBoundingBox(out int2, out int3);
				this.heuristicObjective = new HeuristicObjective(int2, int3, this.heuristic, this.heuristicScale, 0U, null);
			}
			this.RebuildOpenList();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00032FAC File Offset: 0x000311AC
		protected override void Cleanup()
		{
			this.ChooseShortestPath();
			base.Cleanup();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00032202 File Offset: 0x00030402
		protected override void OnHeapExhausted()
		{
			base.CompleteState = PathCompleteState.Complete;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00032FBC File Offset: 0x000311BC
		protected override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
		{
			if (!this.pathHandler.IsTemporaryNode(pathNode))
			{
				base.FailWithError("Expected the end node to be a temporary node. Cannot determine which path it belongs to. This could happen if you are using a custom ending condition for the path.");
				return;
			}
			int targetIndex = this.pathHandler.GetTemporaryNode(pathNode).targetIndex;
			if (this.targetsFound[targetIndex])
			{
				throw new ArgumentException("This target has already been found");
			}
			this.Trace(pathNode);
			this.vectorPaths[targetIndex] = this.vectorPath;
			this.nodePaths[targetIndex] = this.path;
			this.vectorPath = ListPool<Vector3>.Claim();
			this.path = ListPool<GraphNode>.Claim();
			this.targetsFound[targetIndex] = true;
			this.targetPathCosts[targetIndex] = gScore;
			this.targetNodeCount--;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
				if (temporaryNode.type == TemporaryNodeType.End && temporaryNode.targetIndex == targetIndex)
				{
					temporaryNode.type = TemporaryNodeType.Ignore;
				}
				num += 1U;
			}
			if (!this.pathsForAll)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.targetNodeCount = 0;
				return;
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.RecalculateHTarget();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000330E0 File Offset: 0x000312E0
		protected override void Trace(uint pathNodeIndex)
		{
			base.Trace(pathNodeIndex);
			if (this.inverted)
			{
				int num = this.path.Count / 2;
				for (int i = 0; i < num; i++)
				{
					GraphNode graphNode = this.path[i];
					this.path[i] = this.path[this.path.Count - i - 1];
					this.path[this.path.Count - i - 1] = graphNode;
				}
				for (int j = 0; j < num; j++)
				{
					Vector3 vector = this.vectorPath[j];
					this.vectorPath[j] = this.vectorPath[this.vectorPath.Count - j - 1];
					this.vectorPath[this.vectorPath.Count - j - 1] = vector;
				}
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000331C4 File Offset: 0x000313C4
		protected override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			base.DebugStringPrefix(logMode, debugStringBuilder);
			if (!base.error)
			{
				debugStringBuilder.Append("\nShortest path was ");
				debugStringBuilder.Append((this.chosenTarget == -1) ? "undefined" : this.nodePaths[this.chosenTarget].Count.ToString());
				debugStringBuilder.Append(" nodes long");
				if (logMode == PathLog.Heavy)
				{
					debugStringBuilder.Append("\nPaths (").Append(this.targetsFound.Length).Append("):");
					for (int i = 0; i < this.targetsFound.Length; i++)
					{
						debugStringBuilder.Append("\n\n\tPath ").Append(i).Append(" Found: ")
							.Append(this.targetsFound[i]);
						if (this.nodePaths[i] != null)
						{
							debugStringBuilder.Append("\n\t\tLength: ");
							debugStringBuilder.Append(this.nodePaths[i].Count);
						}
					}
					debugStringBuilder.Append("\nStart Node");
					debugStringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder = debugStringBuilder;
					Vector3 endPoint = this.endPoint;
					stringBuilder.Append(endPoint.ToString());
					debugStringBuilder.Append("\n\tGraph: ");
					debugStringBuilder.Append(base.startNode.GraphIndex);
					debugStringBuilder.Append("\nBinary Heap size at completion: ");
					debugStringBuilder.AppendLine((this.pathHandler.heap.numberOfItems - 2).ToString());
				}
			}
			base.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x04000653 RID: 1619
		public OnPathDelegate[] callbacks;

		// Token: 0x04000654 RID: 1620
		public GraphNode[] targetNodes;

		// Token: 0x04000655 RID: 1621
		protected int targetNodeCount;

		// Token: 0x04000656 RID: 1622
		public bool[] targetsFound;

		// Token: 0x04000657 RID: 1623
		public uint[] targetPathCosts;

		// Token: 0x04000658 RID: 1624
		public Vector3[] targetPoints;

		// Token: 0x04000659 RID: 1625
		public Vector3[] originalTargetPoints;

		// Token: 0x0400065A RID: 1626
		public List<Vector3>[] vectorPaths;

		// Token: 0x0400065B RID: 1627
		public List<GraphNode>[] nodePaths;

		// Token: 0x0400065C RID: 1628
		public bool pathsForAll = true;

		// Token: 0x0400065D RID: 1629
		public int chosenTarget = -1;
	}
}
