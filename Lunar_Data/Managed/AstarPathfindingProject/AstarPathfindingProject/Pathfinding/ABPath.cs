using System;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200012A RID: 298
	public class ABPath : Path
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000317FD File Offset: 0x0002F9FD
		public GraphNode startNode
		{
			get
			{
				if (this.path.Count <= 0)
				{
					return null;
				}
				return this.path[0];
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0003181B File Offset: 0x0002FA1B
		public GraphNode endNode
		{
			get
			{
				if (this.path.Count <= 0)
				{
					return null;
				}
				return this.path[this.path.Count - 1];
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00016F22 File Offset: 0x00015122
		protected virtual bool hasEndPoint
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00016F22 File Offset: 0x00015122
		public virtual bool endPointKnownBeforeCalculation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0003185B File Offset: 0x0002FA5B
		public static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			ABPath path = PathPool.GetPath<ABPath>();
			path.Setup(start, end, callback);
			return path;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003186B File Offset: 0x0002FA6B
		protected void Setup(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.callback = callbackDelegate;
			this.UpdateStartEnd(start, end);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003187C File Offset: 0x0002FA7C
		public static ABPath FakePath(List<Vector3> vectorPath, List<GraphNode> nodePath = null)
		{
			ABPath path = PathPool.GetPath<ABPath>();
			for (int i = 0; i < vectorPath.Count; i++)
			{
				path.vectorPath.Add(vectorPath[i]);
			}
			path.completeState = PathCompleteState.Complete;
			((IPathInternals)path).AdvanceState(PathState.Returned);
			if (vectorPath.Count > 0)
			{
				path.UpdateStartEnd(vectorPath[0], vectorPath[vectorPath.Count - 1]);
			}
			if (nodePath != null)
			{
				for (int j = 0; j < nodePath.Count; j++)
				{
					path.path.Add(nodePath[j]);
				}
			}
			return path;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003190B File Offset: 0x0002FB0B
		protected void UpdateStartEnd(Vector3 start, Vector3 end)
		{
			this.originalStartPoint = start;
			this.originalEndPoint = end;
			this.startPoint = start;
			this.endPoint = end;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003192C File Offset: 0x0002FB2C
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.endPoint = Vector3.zero;
			this.calculatePartial = false;
			this.partialBestTargetPathNodeIndex = 0U;
			this.partialBestTargetHScore = uint.MaxValue;
			this.partialBestTargetGScore = uint.MaxValue;
			this.cost = 0U;
			this.endingCondition = null;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00031998 File Offset: 0x0002FB98
		protected virtual bool EndPointGridGraphSpecialCase(GraphNode closestWalkableEndNode, Vector3 originalEndPoint, int targetIndex)
		{
			GridNode gridNode = closestWalkableEndNode as GridNode;
			if (gridNode != null)
			{
				GridGraph gridGraph = GridNode.GetGridGraph(gridNode.GraphIndex);
				GridNode gridNode2 = gridGraph.GetNearest(originalEndPoint, ABPath.NNConstraintNone).node as GridNode;
				if (gridNode != gridNode2 && gridNode2 != null)
				{
					int num = gridNode.NodeInGridIndex % gridGraph.width;
					int num2 = gridNode.NodeInGridIndex / gridGraph.width;
					int num3 = gridNode2.NodeInGridIndex % gridGraph.width;
					int num4 = gridNode2.NodeInGridIndex / gridGraph.width;
					bool flag = false;
					switch (gridGraph.neighbours)
					{
					case NumNeighbours.Four:
						if ((num == num3 && Math.Abs(num2 - num4) == 1) || (num2 == num4 && Math.Abs(num - num3) == 1))
						{
							flag = true;
						}
						break;
					case NumNeighbours.Eight:
						if (Math.Abs(num - num3) <= 1 && Math.Abs(num2 - num4) <= 1)
						{
							flag = true;
						}
						break;
					case NumNeighbours.Six:
					{
						for (int i = 0; i < 6; i++)
						{
							int num5 = num3 + GridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[i]];
							int num6 = num4 + GridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[i]];
							if (num == num5 && num2 == num6)
							{
								flag = true;
								break;
							}
						}
						break;
					}
					default:
						throw new Exception("Unhandled NumNeighbours");
					}
					if (flag)
					{
						this.AddEndpointsForSurroundingGridNodes(gridNode2, originalEndPoint, targetIndex);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00031AF0 File Offset: 0x0002FCF0
		private void AddEndpointsForSurroundingGridNodes(GridNode gridNode, Vector3 desiredPoint, int targetIndex)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(gridNode.GraphIndex);
			int num = ((gridGraph.neighbours == NumNeighbours.Four) ? 4 : ((gridGraph.neighbours == NumNeighbours.Eight) ? 8 : 6));
			int num2 = gridNode.NodeInGridIndex % gridGraph.width;
			int num3 = gridNode.NodeInGridIndex / gridGraph.width;
			for (int i = 0; i < num; i++)
			{
				int num4;
				int num5;
				if (gridGraph.neighbours == NumNeighbours.Six)
				{
					num4 = num2 + GridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[i]];
					num5 = num3 + GridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[i]];
				}
				else
				{
					num4 = num2 + GridGraph.neighbourXOffsets[i];
					num5 = num3 + GridGraph.neighbourZOffsets[i];
				}
				GridNodeBase node = gridGraph.GetNode(num4, num5);
				if (node != null)
				{
					this.pathHandler.AddTemporaryNode(new TemporaryNode
					{
						type = TemporaryNodeType.End,
						position = (Int3)node.ClosestPointOnNode(desiredPoint),
						associatedNode = node.NodeIndex,
						targetIndex = targetIndex
					});
				}
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.startPoint = nearest.position;
			if (nearest.node == null)
			{
				base.FailWithError("Couldn't find a node close to the start point");
				return;
			}
			if (!base.CanTraverse(nearest.node))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.pathHandler.AddTemporaryNode(new TemporaryNode
			{
				associatedNode = nearest.node.NodeIndex,
				position = (Int3)nearest.position,
				type = TemporaryNodeType.Start
			});
			uint num = 0U;
			if (this.hasEndPoint)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.originalEndPoint, this.nnConstraint);
				this.endPoint = nearest2.position;
				if (nearest2.node == null)
				{
					base.FailWithError("Couldn't find a node close to the end point");
					return;
				}
				if (!base.CanTraverse(nearest2.node))
				{
					base.FailWithError("The node closest to the end point could not be traversed");
					return;
				}
				if (nearest.node.Area != nearest2.node.Area)
				{
					base.FailWithError("There is no valid path to the target");
					return;
				}
				num = nearest2.node.NodeIndex;
				if (!this.EndPointGridGraphSpecialCase(nearest2.node, this.originalEndPoint, 0))
				{
					this.pathHandler.AddTemporaryNode(new TemporaryNode
					{
						associatedNode = nearest2.node.NodeIndex,
						position = (Int3)nearest2.position,
						type = TemporaryNodeType.End
					});
				}
			}
			int3 @int;
			int3 int2;
			base.TemporaryEndNodesBoundingBox(out @int, out int2);
			this.heuristicObjective = new HeuristicObjective(@int, int2, this.heuristic, this.heuristicScale, num, AstarPath.active.euclideanEmbedding);
			base.MarkNodesAdjacentToTemporaryEndNodes();
			base.AddStartNodesToHeap();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00031DEC File Offset: 0x0002FFEC
		private void CompletePartial()
		{
			base.CompleteState = PathCompleteState.Partial;
			this.endPoint = this.pathHandler.GetNode(this.partialBestTargetPathNodeIndex).ClosestPointOnNode(this.originalEndPoint);
			this.cost = this.partialBestTargetGScore;
			this.Trace(this.partialBestTargetPathNodeIndex);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00031E3A File Offset: 0x0003003A
		protected override void OnHeapExhausted()
		{
			if (this.calculatePartial && this.partialBestTargetPathNodeIndex != 0U)
			{
				this.CompletePartial();
				return;
			}
			base.FailWithError("Searched all reachable nodes, but could not find target. This can happen if you have nodes with a different tag blocking the way to the goal. You can enable path.calculatePartial to handle that case as a workaround (though this comes with a performance cost).");
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00031E60 File Offset: 0x00030060
		protected unsafe override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
		{
			if (this.pathHandler.IsTemporaryNode(pathNode))
			{
				TemporaryNode temporaryNode = *this.pathHandler.GetTemporaryNode(pathNode);
				GraphNode node = this.pathHandler.GetNode(temporaryNode.associatedNode);
				if (this.endingCondition != null && !this.endingCondition.TargetFound(node, this.partialBestTargetHScore, gScore))
				{
					return;
				}
				this.endPoint = (Vector3)temporaryNode.position;
				this.endPoint = node.ClosestPointOnNode(this.endPoint);
			}
			else
			{
				GraphNode node2 = this.pathHandler.GetNode(pathNode);
				this.endPoint = (Vector3)node2.position;
			}
			this.cost = gScore;
			base.CompleteState = PathCompleteState.Complete;
			this.Trace(pathNode);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00031F18 File Offset: 0x00030118
		public override void OnVisitNode(uint pathNode, uint hScore, uint gScore)
		{
			if (base.CompleteState != PathCompleteState.NotCalculated)
			{
				return;
			}
			if (this.endingCondition != null)
			{
				GraphNode node = this.pathHandler.GetNode(pathNode);
				if (this.endingCondition.TargetFound(node, hScore, gScore))
				{
					this.OnFoundEndNode(pathNode, hScore, gScore);
					if (base.CompleteState == PathCompleteState.Complete)
					{
						return;
					}
				}
			}
			if (hScore < this.partialBestTargetHScore)
			{
				this.partialBestTargetPathNodeIndex = pathNode;
				this.partialBestTargetHScore = hScore;
				this.partialBestTargetGScore = gScore;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00031F84 File Offset: 0x00030184
		protected override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			base.DebugStringPrefix(logMode, stringBuilder);
			if (!base.error)
			{
				stringBuilder.Append(" Path Cost: ");
				stringBuilder.Append(this.cost);
			}
			if (!base.error && logMode == PathLog.Heavy)
			{
				Vector3 vector;
				if (this.hasEndPoint && this.endNode != null)
				{
					stringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder2 = stringBuilder;
					vector = this.endPoint;
					stringBuilder2.Append(vector.ToString());
					stringBuilder.Append("\n\tGraph: ");
					stringBuilder.Append(this.endNode.GraphIndex);
				}
				stringBuilder.Append("\nStart Node");
				stringBuilder.Append("\n\tPoint: ");
				StringBuilder stringBuilder3 = stringBuilder;
				vector = this.startPoint;
				stringBuilder3.Append(vector.ToString());
				stringBuilder.Append("\n\tGraph: ");
				if (this.startNode != null)
				{
					stringBuilder.Append(this.startNode.GraphIndex);
				}
				else
				{
					stringBuilder.Append("< null startNode >");
				}
			}
			base.DebugStringSuffix(logMode, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x04000639 RID: 1593
		public Vector3 originalStartPoint;

		// Token: 0x0400063A RID: 1594
		public Vector3 originalEndPoint;

		// Token: 0x0400063B RID: 1595
		public Vector3 startPoint;

		// Token: 0x0400063C RID: 1596
		public Vector3 endPoint;

		// Token: 0x0400063D RID: 1597
		public uint cost;

		// Token: 0x0400063E RID: 1598
		public bool calculatePartial;

		// Token: 0x0400063F RID: 1599
		protected uint partialBestTargetPathNodeIndex;

		// Token: 0x04000640 RID: 1600
		protected uint partialBestTargetHScore = uint.MaxValue;

		// Token: 0x04000641 RID: 1601
		protected uint partialBestTargetGScore = uint.MaxValue;

		// Token: 0x04000642 RID: 1602
		public PathEndingCondition endingCondition;

		// Token: 0x04000643 RID: 1603
		private static readonly NNConstraint NNConstraintNone = NNConstraint.None;
	}
}
