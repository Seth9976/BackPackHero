using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Util
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public class EuclideanEmbedding
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0003D268 File Offset: 0x0003B468
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0003D270 File Offset: 0x0003B470
		public NativeArray<uint> costs { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0003D279 File Offset: 0x0003B479
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0003D281 File Offset: 0x0003B481
		public int pivotCount { get; private set; }

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0003D28A File Offset: 0x0003B48A
		private uint GetRandom()
		{
			this.rval = 12820163U * this.rval + 1140671485U;
			return this.rval;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003D2AC File Offset: 0x0003B4AC
		public void OnDisable()
		{
			if (this.costs.IsCreated)
			{
				this.costs.Dispose();
			}
			this.costs = default(NativeArray<uint>);
			this.pivotCount = 0;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		public unsafe static uint GetHeuristic(UnsafeSpan<uint> costs, uint pivotCount, uint nodeIndex1, uint nodeIndex2)
		{
			uint num = 0U;
			if ((ulong)nodeIndex1 < (ulong)((long)costs.Length) && (ulong)nodeIndex2 < (ulong)((long)costs.Length))
			{
				for (uint num2 = 0U; num2 < pivotCount; num2 += 1U)
				{
					uint num3 = *costs[nodeIndex1 * pivotCount + num2];
					uint num4 = *costs[nodeIndex2 * pivotCount + num2];
					if (num3 != 4294967295U && num4 != 4294967295U)
					{
						uint num5 = (uint)math.abs((int)(num3 - num4));
						if (num5 > num)
						{
							num = num5;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0003D35C File Offset: 0x0003B55C
		private void GetClosestWalkableNodesToChildrenRecursively(Transform tr, List<GraphNode> nodes)
		{
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				NNInfo nearest = AstarPath.active.GetNearest(transform.position, NNConstraint.Walkable);
				if (nearest.node != null && nearest.node.Walkable)
				{
					nodes.Add(nearest.node);
				}
				this.GetClosestWalkableNodesToChildrenRecursively(transform, nodes);
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003D3E8 File Offset: 0x0003B5E8
		private void PickNRandomNodes(int count, List<GraphNode> buffer)
		{
			int n = 0;
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in AstarPath.active.graphs)
			{
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (!node.Destroyed && node.Walkable)
						{
							int j = n;
							n = j + 1;
							if ((ulong)this.GetRandom() % (ulong)((long)n) < (ulong)((long)count))
							{
								if (buffer.Count < count)
								{
									buffer.Add(node);
									return;
								}
								buffer[n % buffer.Count] = node;
							}
						}
					});
				}
				navGraph.GetNodes(action);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003D458 File Offset: 0x0003B658
		private GraphNode PickAnyWalkableNode()
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			GraphNode first = null;
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in graphs)
			{
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node != null && node.Walkable && first == null)
						{
							first = node;
						}
					});
				}
				navGraph.GetNodes(action);
			}
			return first;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0003D4B8 File Offset: 0x0003B6B8
		public void RecalculatePivots()
		{
			if (this.mode == HeuristicOptimizationMode.None)
			{
				this.pivotCount = 0;
				this.pivots = null;
				return;
			}
			this.rval = (uint)this.seed;
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			switch (this.mode)
			{
			case HeuristicOptimizationMode.Random:
				this.PickNRandomNodes(this.spreadOutCount, list);
				break;
			case HeuristicOptimizationMode.RandomSpreadOut:
			{
				if (this.pivotPointRoot != null)
				{
					this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				}
				if (list.Count == 0)
				{
					GraphNode graphNode = this.PickAnyWalkableNode();
					if (graphNode == null)
					{
						Debug.LogError("Could not find any walkable node in any of the graphs.");
						ListPool<GraphNode>.Release(ref list);
						return;
					}
					list.Add(graphNode);
				}
				int num = this.spreadOutCount - list.Count;
				for (int i = 0; i < num; i++)
				{
					list.Add(null);
				}
				break;
			}
			case HeuristicOptimizationMode.Custom:
				if (this.pivotPointRoot == null)
				{
					throw new Exception("heuristicOptimizationMode is HeuristicOptimizationMode.Custom, but no 'customHeuristicOptimizationPivotsRoot' is set");
				}
				this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				break;
			default:
				throw new Exception("Invalid HeuristicOptimizationMode: " + this.mode.ToString());
			}
			this.pivots = list.ToArray();
			ListPool<GraphNode>.Release(ref list);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003D5EE File Offset: 0x0003B7EE
		public void RecalculateCosts()
		{
			if (this.pivots == null)
			{
				this.RecalculatePivots();
			}
			if (this.mode == HeuristicOptimizationMode.None)
			{
				return;
			}
			this.RecalculateCostsInner();
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003D610 File Offset: 0x0003B810
		private void RecalculateCostsInner()
		{
			this.pivotCount = 0;
			for (int i = 0; i < this.pivots.Length; i++)
			{
				if (this.pivots[i] != null && (this.pivots[i].Destroyed || !this.pivots[i].Walkable))
				{
					throw new Exception("Invalid pivot nodes (destroyed or unwalkable)");
				}
			}
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int j = 0; j < this.pivots.Length; j++)
				{
					if (this.pivots[j] == null)
					{
						throw new Exception("Invalid pivot nodes (null)");
					}
				}
			}
			this.pivotCount = this.pivots.Length;
			Action<int> startCostCalculation = null;
			int numComplete = 0;
			uint nextNodeIndex = AstarPath.active.nodeStorage.nextNodeIndex;
			if (this.costs.IsCreated)
			{
				this.costs.Dispose();
			}
			this.costs = new NativeArray<uint>((int)(nextNodeIndex * (uint)this.pivotCount), Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.costs.AsUnsafeSpan<uint>().Fill(uint.MaxValue);
			startCostCalculation = delegate(int pivotIndex)
			{
				GraphNode graphNode = this.pivots[pivotIndex];
				EuclideanEmbedding.EuclideanEmbeddingSearchPath path = EuclideanEmbedding.EuclideanEmbeddingSearchPath.Construct(this.costs.AsUnsafeSpan<uint>(), (uint)this.pivotCount, (uint)pivotIndex, graphNode);
				path.immediateCallback = delegate(Path _)
				{
					if (this.mode == HeuristicOptimizationMode.RandomSpreadOut && pivotIndex < this.pivots.Length - 1)
					{
						if (this.pivots[pivotIndex + 1] == null)
						{
							this.pivots[pivotIndex + 1] = path.furthestNode;
							if (path.furthestNode == null)
							{
								Debug.LogError("Failed generating random pivot points for heuristic optimizations");
								return;
							}
						}
						startCostCalculation(pivotIndex + 1);
					}
					int numComplete2 = numComplete;
					numComplete = numComplete2 + 1;
					if (numComplete == this.pivotCount)
					{
						this.ApplyGridGraphEndpointSpecialCase();
					}
				};
				AstarPath.StartPath(path, true, true);
			};
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int k = 0; k < this.pivots.Length; k++)
				{
					startCostCalculation(k);
				}
			}
			else
			{
				startCostCalculation(0);
			}
			this.dirty = false;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0003D770 File Offset: 0x0003B970
		private unsafe void ApplyGridGraphEndpointSpecialCase()
		{
			UnsafeSpan<uint> unsafeSpan = this.costs.AsUnsafeSpan<uint>();
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (gridGraph != null)
				{
					GridNodeBase[] nodes = gridGraph.nodes;
					int num = ((gridGraph.neighbours == NumNeighbours.Four) ? 4 : ((gridGraph.neighbours == NumNeighbours.Eight) ? 8 : 6));
					for (int j = 0; j < gridGraph.depth; j++)
					{
						for (int k = 0; k < gridGraph.width; k++)
						{
							GridNodeBase gridNodeBase = nodes[j * gridGraph.width + k];
							if (!gridNodeBase.Walkable)
							{
								uint num2 = gridNodeBase.NodeIndex * (uint)this.pivotCount;
								for (int l = 0; l < this.pivotCount; l++)
								{
									*unsafeSpan[num2 + (uint)l] = uint.MaxValue;
								}
								for (int m = 0; m < num; m++)
								{
									int num3;
									int num4;
									if (gridGraph.neighbours == NumNeighbours.Six)
									{
										num3 = k + GridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[m]];
										num4 = j + GridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[m]];
									}
									else
									{
										num3 = k + GridGraph.neighbourXOffsets[m];
										num4 = j + GridGraph.neighbourZOffsets[m];
									}
									if (num3 >= 0 && num4 >= 0 && num3 < gridGraph.width && num4 < gridGraph.depth)
									{
										GridNodeBase gridNodeBase2 = gridGraph.nodes[num4 * gridGraph.width + num3];
										if (gridNodeBase2.Walkable)
										{
											uint num5 = 0U;
											while ((ulong)num5 < (ulong)((long)this.pivotCount))
											{
												uint num6 = *unsafeSpan[gridNodeBase2.NodeIndex * (uint)this.pivotCount + num5] + gridGraph.neighbourCosts[m];
												*unsafeSpan[num2 + num5] = Math.Min(*unsafeSpan[num2 + num5], num6);
												num5 += 1U;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003D964 File Offset: 0x0003BB64
		public void OnDrawGizmos()
		{
			if (this.pivots != null)
			{
				for (int i = 0; i < this.pivots.Length; i++)
				{
					if (this.pivots[i] != null && !this.pivots[i].Destroyed)
					{
						Draw.SolidBox((Vector3)this.pivots[i].position, Vector3.one, new Color(0.62352943f, 0.36862746f, 0.7607843f, 0.8f));
					}
				}
			}
		}

		// Token: 0x0400076E RID: 1902
		public HeuristicOptimizationMode mode;

		// Token: 0x0400076F RID: 1903
		public int seed;

		// Token: 0x04000770 RID: 1904
		public Transform pivotPointRoot;

		// Token: 0x04000771 RID: 1905
		public int spreadOutCount = 1;

		// Token: 0x04000772 RID: 1906
		[NonSerialized]
		public bool dirty;

		// Token: 0x04000775 RID: 1909
		private GraphNode[] pivots;

		// Token: 0x04000776 RID: 1910
		private const uint ra = 12820163U;

		// Token: 0x04000777 RID: 1911
		private const uint rc = 1140671485U;

		// Token: 0x04000778 RID: 1912
		private uint rval;

		// Token: 0x02000194 RID: 404
		private class EuclideanEmbeddingSearchPath : Path
		{
			// Token: 0x06000AF1 RID: 2801 RVA: 0x0003D9F3 File Offset: 0x0003BBF3
			public static EuclideanEmbedding.EuclideanEmbeddingSearchPath Construct(UnsafeSpan<uint> costs, uint costIndexStride, uint pivotIndex, GraphNode startNode)
			{
				EuclideanEmbedding.EuclideanEmbeddingSearchPath path = PathPool.GetPath<EuclideanEmbedding.EuclideanEmbeddingSearchPath>();
				path.costs = costs;
				path.costIndexStride = costIndexStride;
				path.pivotIndex = pivotIndex;
				path.startNode = startNode;
				path.furthestNodeScore = 0U;
				path.furthestNode = null;
				return path;
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x0003DA24 File Offset: 0x0003BC24
			protected override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06000AF3 RID: 2803 RVA: 0x00032202 File Offset: 0x00030402
			protected override void OnHeapExhausted()
			{
				base.CompleteState = PathCompleteState.Complete;
			}

			// Token: 0x06000AF4 RID: 2804 RVA: 0x0003DA2C File Offset: 0x0003BC2C
			public unsafe override void OnVisitNode(uint pathNode, uint hScore, uint gScore)
			{
				if (!this.pathHandler.IsTemporaryNode(pathNode))
				{
					GraphNode node = this.pathHandler.GetNode(pathNode);
					uint num = node.NodeIndex * this.costIndexStride;
					*this.costs[num + this.pivotIndex] = math.min(*this.costs[num + this.pivotIndex], gScore);
					uint num2 = uint.MaxValue;
					int num3 = 0;
					while ((long)num3 <= (long)((ulong)this.pivotIndex))
					{
						num2 = math.min(num2, *this.costs[num + (uint)num3]);
						num3++;
					}
					if (num2 > this.furthestNodeScore || this.furthestNode == null)
					{
						this.furthestNodeScore = num2;
						this.furthestNode = node;
					}
				}
			}

			// Token: 0x06000AF5 RID: 2805 RVA: 0x0003DAE0 File Offset: 0x0003BCE0
			protected override void Prepare()
			{
				this.pathHandler.AddTemporaryNode(new TemporaryNode
				{
					associatedNode = this.startNode.NodeIndex,
					position = this.startNode.position,
					type = TemporaryNodeType.Start
				});
				this.heuristicObjective = new HeuristicObjective(0, Heuristic.None, 0f);
				base.MarkNodesAdjacentToTemporaryEndNodes();
				base.AddStartNodesToHeap();
			}

			// Token: 0x04000779 RID: 1913
			public UnsafeSpan<uint> costs;

			// Token: 0x0400077A RID: 1914
			public uint costIndexStride;

			// Token: 0x0400077B RID: 1915
			public uint pivotIndex;

			// Token: 0x0400077C RID: 1916
			public GraphNode startNode;

			// Token: 0x0400077D RID: 1917
			public uint furthestNodeScore;

			// Token: 0x0400077E RID: 1918
			public GraphNode furthestNode;
		}
	}
}
