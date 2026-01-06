using System;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008E RID: 142
	public class MultiTargetPath : ABPath
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0002920C File Offset: 0x0002740C
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00029214 File Offset: 0x00027414
		public bool inverted { get; protected set; }

		// Token: 0x060006E1 RID: 1761 RVA: 0x0002923A File Offset: 0x0002743A
		public static MultiTargetPath Construct(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(target, startPoints, callbackDelegates, callback);
			multiTargetPath.inverted = true;
			return multiTargetPath;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0002924C File Offset: 0x0002744C
		public static MultiTargetPath Construct(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath path = PathPool.GetPath<MultiTargetPath>();
			path.Setup(start, targets, callbackDelegates, callback);
			return path;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00029260 File Offset: 0x00027460
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
			this.startIntPoint = (Int3)start;
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002931F File Offset: 0x0002751F
		protected override void Reset()
		{
			base.Reset();
			this.pathsForAll = true;
			this.chosenTarget = -1;
			this.sequentialTarget = 0;
			this.inverted = true;
			this.heuristicMode = MultiTargetPath.HeuristicMode.Sequential;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0002934C File Offset: 0x0002754C
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
			this.targetPoints = null;
			this.originalTargetPoints = null;
			base.OnEnterPool();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00029404 File Offset: 0x00027604
		private void ChooseShortestPath()
		{
			this.chosenTarget = -1;
			if (this.nodePaths != null)
			{
				uint num = 2147483647U;
				for (int i = 0; i < this.nodePaths.Length; i++)
				{
					List<GraphNode> list = this.nodePaths[i];
					if (list != null)
					{
						uint g = this.pathHandler.GetPathNode(list[this.inverted ? 0 : (list.Count - 1)]).G;
						if (this.chosenTarget == -1 || g < num)
						{
							this.chosenTarget = i;
							num = g;
						}
					}
				}
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00029488 File Offset: 0x00027688
		private void SetPathParametersForReturn(int target)
		{
			this.path = this.nodePaths[target];
			this.vectorPath = this.vectorPaths[target];
			if (this.inverted)
			{
				this.startNode = this.targetNodes[target];
				this.startPoint = this.targetPoints[target];
				this.originalStartPoint = this.originalTargetPoints[target];
				return;
			}
			this.endNode = this.targetNodes[target];
			this.endPoint = this.targetPoints[target];
			this.originalEndPoint = this.originalTargetPoints[target];
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00029520 File Offset: 0x00027720
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
				this.endNode = this.startNode;
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

		// Token: 0x060006E9 RID: 1769 RVA: 0x00029644 File Offset: 0x00027844
		protected void FoundTarget(PathNode nodeR, int i)
		{
			nodeR.flag1 = false;
			this.Trace(nodeR);
			this.vectorPaths[i] = this.vectorPath;
			this.nodePaths[i] = this.path;
			this.vectorPath = ListPool<Vector3>.Claim();
			this.path = ListPool<GraphNode>.Claim();
			this.targetsFound[i] = true;
			this.targetNodeCount--;
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
			this.RecalculateHTarget(false);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000296D8 File Offset: 0x000278D8
		protected void RebuildOpenList()
		{
			BinaryHeap heap = this.pathHandler.heap;
			for (int i = 0; i < heap.numberOfItems; i++)
			{
				PathNode node = heap.GetNode(i);
				node.H = base.CalculateHScore(node.node);
				heap.SetF(i, node.F);
			}
			this.pathHandler.heap.Rebuild();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0002973C File Offset: 0x0002793C
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.FailWithError("Could not find start node for multi target path");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.vectorPaths = new List<Vector3>[this.targetPoints.Length];
			this.nodePaths = new List<GraphNode>[this.targetPoints.Length];
			this.targetNodes = new GraphNode[this.targetPoints.Length];
			this.targetsFound = new bool[this.targetPoints.Length];
			this.targetNodeCount = this.targetPoints.Length;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.targetPoints[i], this.nnConstraint);
				this.targetNodes[i] = nearest2.node;
				this.targetPoints[i] = nearest2.position;
				if (this.targetNodes[i] != null)
				{
					flag3 = true;
					this.endNode = this.targetNodes[i];
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
				if (nearest2.node != null && nearest2.node.Area == this.startNode.Area)
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
					this.targetNodeCount--;
				}
			}
			this.startPoint = nearest.position;
			this.startIntPoint = (Int3)this.startPoint;
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
				base.FailWithError("There are no valid paths to the targets");
				return;
			}
			this.RecalculateHTarget(true);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00029960 File Offset: 0x00027B60
		private void RecalculateHTarget(bool firstTime)
		{
			if (!this.pathsForAll)
			{
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
				return;
			}
			switch (this.heuristicMode)
			{
			case MultiTargetPath.HeuristicMode.None:
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
				goto IL_0223;
			case MultiTargetPath.HeuristicMode.Average:
				if (!firstTime)
				{
					return;
				}
				break;
			case MultiTargetPath.HeuristicMode.MovingAverage:
				break;
			case MultiTargetPath.HeuristicMode.Midpoint:
				if (!firstTime)
				{
					return;
				}
				goto IL_00D6;
			case MultiTargetPath.HeuristicMode.MovingMidpoint:
				goto IL_00D6;
			case MultiTargetPath.HeuristicMode.Sequential:
			{
				if (!firstTime && !this.targetsFound[this.sequentialTarget])
				{
					return;
				}
				float num = 0f;
				for (int i = 0; i < this.targetPoints.Length; i++)
				{
					if (!this.targetsFound[i])
					{
						float sqrMagnitude = (this.targetNodes[i].position - this.startNode.position).sqrMagnitude;
						if (sqrMagnitude > num)
						{
							num = sqrMagnitude;
							this.hTarget = (Int3)this.targetPoints[i];
							this.sequentialTarget = i;
						}
					}
				}
				goto IL_0223;
			}
			default:
				goto IL_0223;
			}
			Vector3 vector = Vector3.zero;
			int num2 = 0;
			for (int j = 0; j < this.targetPoints.Length; j++)
			{
				if (!this.targetsFound[j])
				{
					vector += (Vector3)this.targetNodes[j].position;
					num2++;
				}
			}
			if (num2 == 0)
			{
				throw new Exception("Should not happen");
			}
			vector /= (float)num2;
			this.hTarget = (Int3)vector;
			goto IL_0223;
			IL_00D6:
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			bool flag = false;
			for (int k = 0; k < this.targetPoints.Length; k++)
			{
				if (!this.targetsFound[k])
				{
					if (!flag)
					{
						vector2 = (Vector3)this.targetNodes[k].position;
						vector3 = (Vector3)this.targetNodes[k].position;
						flag = true;
					}
					else
					{
						vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
						vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
					}
				}
			}
			Int3 @int = (Int3)((vector2 + vector3) * 0.5f);
			this.hTarget = @int;
			IL_0223:
			if (!firstTime)
			{
				this.RebuildOpenList();
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00029B9C File Offset: 0x00027D9C
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				if (this.startNode == this.targetNodes[i])
				{
					this.FoundTarget(pathNode, i);
				}
				else if (this.targetNodes[i] != null)
				{
					this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = true;
				}
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
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

		// Token: 0x060006EE RID: 1774 RVA: 0x00029CBD File Offset: 0x00027EBD
		protected override void Cleanup()
		{
			this.ChooseShortestPath();
			this.ResetFlags();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00029CCC File Offset: 0x00027ECC
		private void ResetFlags()
		{
			if (this.targetNodes != null)
			{
				for (int i = 0; i < this.targetNodes.Length; i++)
				{
					if (this.targetNodes[i] != null)
					{
						this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = false;
					}
				}
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00029D18 File Offset: 0x00027F18
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = base.searchedNodes;
				base.searchedNodes = searchedNodes + 1;
				if (this.currentR.flag1)
				{
					for (int i = 0; i < this.targetNodes.Length; i++)
					{
						if (!this.targetsFound[i] && this.currentR.node == this.targetNodes[i])
						{
							this.FoundTarget(this.currentR, i);
							if (base.CompleteState != PathCompleteState.NotCalculated)
							{
								break;
							}
						}
					}
					if (this.targetNodeCount <= 0)
					{
						base.CompleteState = PathCompleteState.Complete;
						return;
					}
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
				}
				num++;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00029E18 File Offset: 0x00028018
		protected override void Trace(PathNode node)
		{
			base.Trace(node);
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

		// Token: 0x060006F2 RID: 1778 RVA: 0x00029EFC File Offset: 0x000280FC
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
					Vector3 vector;
					for (int i = 0; i < this.targetsFound.Length; i++)
					{
						debugStringBuilder.Append("\n\n\tPath ").Append(i).Append(" Found: ")
							.Append(this.targetsFound[i]);
						if (this.nodePaths[i] != null)
						{
							debugStringBuilder.Append("\n\t\tLength: ");
							debugStringBuilder.Append(this.nodePaths[i].Count);
							if (this.nodePaths[i][this.nodePaths[i].Count - 1] != null)
							{
								PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
								if (pathNode != null)
								{
									debugStringBuilder.Append("\n\t\tEnd Node");
									debugStringBuilder.Append("\n\t\t\tG: ");
									debugStringBuilder.Append(pathNode.G);
									debugStringBuilder.Append("\n\t\t\tH: ");
									debugStringBuilder.Append(pathNode.H);
									debugStringBuilder.Append("\n\t\t\tF: ");
									debugStringBuilder.Append(pathNode.F);
									debugStringBuilder.Append("\n\t\t\tPoint: ");
									StringBuilder stringBuilder = debugStringBuilder;
									vector = this.endPoint;
									stringBuilder.Append(vector.ToString());
									debugStringBuilder.Append("\n\t\t\tGraph: ");
									debugStringBuilder.Append(this.endNode.GraphIndex);
								}
								else
								{
									debugStringBuilder.Append("\n\t\tEnd Node: Null");
								}
							}
						}
					}
					debugStringBuilder.Append("\nStart Node");
					debugStringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder2 = debugStringBuilder;
					vector = this.endPoint;
					stringBuilder2.Append(vector.ToString());
					debugStringBuilder.Append("\n\tGraph: ");
					debugStringBuilder.Append(this.startNode.GraphIndex);
					debugStringBuilder.Append("\nBinary Heap size at completion: ");
					debugStringBuilder.AppendLine((this.pathHandler.heap == null) ? "Null" : (this.pathHandler.heap.numberOfItems - 2).ToString());
				}
			}
			base.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x040003F4 RID: 1012
		public OnPathDelegate[] callbacks;

		// Token: 0x040003F5 RID: 1013
		public GraphNode[] targetNodes;

		// Token: 0x040003F6 RID: 1014
		protected int targetNodeCount;

		// Token: 0x040003F7 RID: 1015
		public bool[] targetsFound;

		// Token: 0x040003F8 RID: 1016
		public Vector3[] targetPoints;

		// Token: 0x040003F9 RID: 1017
		public Vector3[] originalTargetPoints;

		// Token: 0x040003FA RID: 1018
		public List<Vector3>[] vectorPaths;

		// Token: 0x040003FB RID: 1019
		public List<GraphNode>[] nodePaths;

		// Token: 0x040003FC RID: 1020
		public bool pathsForAll = true;

		// Token: 0x040003FD RID: 1021
		public int chosenTarget = -1;

		// Token: 0x040003FE RID: 1022
		private int sequentialTarget;

		// Token: 0x040003FF RID: 1023
		public MultiTargetPath.HeuristicMode heuristicMode = MultiTargetPath.HeuristicMode.Sequential;

		// Token: 0x0200013F RID: 319
		public enum HeuristicMode
		{
			// Token: 0x0400074E RID: 1870
			None,
			// Token: 0x0400074F RID: 1871
			Average,
			// Token: 0x04000750 RID: 1872
			MovingAverage,
			// Token: 0x04000751 RID: 1873
			Midpoint,
			// Token: 0x04000752 RID: 1874
			MovingMidpoint,
			// Token: 0x04000753 RID: 1875
			Sequential
		}
	}
}
