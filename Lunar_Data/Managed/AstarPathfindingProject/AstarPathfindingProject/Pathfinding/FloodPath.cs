using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200012E RID: 302
	public class FloodPath : Path
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x000322C8 File Offset: 0x000304C8
		public bool HasPathTo(GraphNode node)
		{
			if (this.parents != null)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)node.PathNodeVariants))
				{
					if (this.parents.ContainsKey(node.NodeIndex + num))
					{
						return true;
					}
					num += 1U;
				}
			}
			return false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00032308 File Offset: 0x00030508
		internal bool IsValid(GlobalNodeStorage nodeStorage)
		{
			return nodeStorage.destroyedNodesVersion == this.validationHash;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00032318 File Offset: 0x00030518
		public uint GetParent(uint node)
		{
			uint num;
			if (!this.parents.TryGetValue(node, out num))
			{
				return 0U;
			}
			return num;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00032347 File Offset: 0x00030547
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00032356 File Offset: 0x00030556
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

		// Token: 0x06000922 RID: 2338 RVA: 0x00032373 File Offset: 0x00030573
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00032391 File Offset: 0x00030591
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000323CA File Offset: 0x000305CA
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<uint, uint>();
			this.saveParents = true;
			this.validationHash = 0U;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00032408 File Offset: 0x00030608
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
			this.pathHandler.AddTemporaryNode(new TemporaryNode
			{
				type = TemporaryNodeType.Start,
				position = (Int3)this.startPoint,
				associatedNode = this.startNode.NodeIndex
			});
			this.heuristicObjective = new HeuristicObjective(int3.zero, Heuristic.None, 0f);
			base.AddStartNodesToHeap();
			this.validationHash = this.pathHandler.nodeStorage.destroyedNodesVersion;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00032202 File Offset: 0x00030402
		protected override void OnHeapExhausted()
		{
			base.CompleteState = PathCompleteState.Complete;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003252F File Offset: 0x0003072F
		protected override void OnFoundEndNode(uint pathNode, uint hScore, uint gScore)
		{
			throw new InvalidOperationException("FloodPaths do not have any end nodes");
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003253C File Offset: 0x0003073C
		public override void OnVisitNode(uint pathNode, uint hScore, uint gScore)
		{
			if (this.saveParents)
			{
				uint parentIndex = this.pathHandler.pathNodes[pathNode].parentIndex;
				this.parents[pathNode] = parentIndex | (this.pathHandler.IsTemporaryNode(parentIndex) ? 2147483648U : 0U);
			}
		}

		// Token: 0x0400064A RID: 1610
		public Vector3 originalStartPoint;

		// Token: 0x0400064B RID: 1611
		public Vector3 startPoint;

		// Token: 0x0400064C RID: 1612
		public GraphNode startNode;

		// Token: 0x0400064D RID: 1613
		public bool saveParents = true;

		// Token: 0x0400064E RID: 1614
		protected Dictionary<uint, uint> parents;

		// Token: 0x0400064F RID: 1615
		private uint validationHash;

		// Token: 0x04000650 RID: 1616
		public const uint TemporaryNodeBit = 2147483648U;
	}
}
