using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A7 RID: 167
	[BurstCompile]
	public abstract class Path : IPathInternals
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00019809 File Offset: 0x00017A09
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00019811 File Offset: 0x00017A11
		public PathState PipelineState { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001981A File Offset: 0x00017A1A
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00019824 File Offset: 0x00017A24
		public PathCompleteState CompleteState
		{
			get
			{
				return this.completeState;
			}
			protected set
			{
				lock (this)
				{
					if (this.completeState != PathCompleteState.Error)
					{
						this.completeState = value;
					}
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001986C File Offset: 0x00017A6C
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00019877 File Offset: 0x00017A77
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001987F File Offset: 0x00017A7F
		public string errorLog { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00019888 File Offset: 0x00017A88
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00019890 File Offset: 0x00017A90
		public int searchedNodes { get; protected set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00019899 File Offset: 0x00017A99
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x000198A1 File Offset: 0x00017AA1
		bool IPathInternals.Pooled { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000198AA File Offset: 0x00017AAA
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x000198B2 File Offset: 0x00017AB2
		public ushort pathID { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000198BB File Offset: 0x00017ABB
		internal ref HeuristicObjective heuristicObjectiveInternal
		{
			get
			{
				return ref this.heuristicObjective;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000198C3 File Offset: 0x00017AC3
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x000198DA File Offset: 0x00017ADA
		public int[] tagPenalties
		{
			get
			{
				if (this.internalTagPenalties != Path.ZeroTagPenalties)
				{
					return this.internalTagPenalties;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.internalTagPenalties = Path.ZeroTagPenalties;
					return;
				}
				if (value.Length != 32)
				{
					throw new ArgumentException("tagPenalties must have a length of 32");
				}
				this.internalTagPenalties = value;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00019904 File Offset: 0x00017B04
		public void UseSettings(PathRequestSettings settings)
		{
			this.nnConstraint.graphMask = settings.graphMask;
			this.traversalProvider = settings.traversalProvider;
			this.enabledTags = settings.traversableTags;
			this.tagPenalties = settings.tagPenalties;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001993C File Offset: 0x00017B3C
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00019998 File Offset: 0x00017B98
		public IEnumerator WaitForPath()
		{
			if (this.PipelineState == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.PipelineState != PathState.Returned)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000199A7 File Offset: 0x00017BA7
		public void BlockUntilCalculated()
		{
			AstarPath.BlockUntilCalculated(this);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000199B0 File Offset: 0x00017BB0
		public unsafe bool ShouldConsiderPathNode(uint pathNodeIndex)
		{
			PathNode pathNode = *this.pathHandler.pathNodes[pathNodeIndex];
			return pathNode.pathID != this.pathID || pathNode.heapIndex != ushort.MaxValue;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000199F4 File Offset: 0x00017BF4
		public void OpenCandidateConnectionsToEndNode(Int3 position, uint parentPathNode, uint parentNodeIndex, uint parentG)
		{
			if (this.pathHandler.pathNodes[parentNodeIndex].flag1)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
				{
					uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
					ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
					if (temporaryNode.type == TemporaryNodeType.End && temporaryNode.associatedNode == parentNodeIndex)
					{
						uint costMagnitude = (uint)(position - temporaryNode.position).costMagnitude;
						this.OpenCandidateConnection(parentPathNode, num2, parentG, costMagnitude, 0U, temporaryNode.position);
					}
					num += 1U;
				}
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00019A88 File Offset: 0x00017C88
		public void OpenCandidateConnection(uint parentPathNode, uint targetPathNode, uint parentG, uint connectionCost, uint fractionAlongEdge, Int3 targetNodePosition)
		{
			if (!this.ShouldConsiderPathNode(targetPathNode))
			{
				return;
			}
			uint num;
			uint num2;
			if (this.pathHandler.IsTemporaryNode(targetPathNode))
			{
				num = 0U;
				num2 = 0U;
			}
			else
			{
				GraphNode node = this.pathHandler.GetNode(targetPathNode);
				num = this.GetTraversalCost(node);
				num2 = node.NodeIndex;
			}
			uint num3 = parentG + connectionCost + num;
			Path.OpenCandidateParams openCandidateParams = new Path.OpenCandidateParams
			{
				pathID = this.pathID,
				parentPathNode = parentPathNode,
				targetPathNode = targetPathNode,
				targetNodeIndex = num2,
				candidateG = num3,
				fractionAlongEdge = fractionAlongEdge,
				targetNodePosition = (int3)targetNodePosition,
				pathNodes = this.pathHandler.pathNodes
			};
			Path.OpenCandidateConnectionBurst(ref openCandidateParams, ref this.pathHandler.heap, ref this.heuristicObjective);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00019B52 File Offset: 0x00017D52
		[BurstCompile]
		public static void OpenCandidateConnectionBurst(ref Path.OpenCandidateParams pars, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
		{
			Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Invoke(ref pars, ref heap, ref heuristicObjective);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00019B5C File Offset: 0x00017D5C
		public uint GetTagPenalty(int tag)
		{
			return (uint)this.internalTagPenalties[tag];
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019B66 File Offset: 0x00017D66
		public bool CanTraverse(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.CanTraverse(this, node);
			}
			return node.Walkable && ((this.enabledTags >> (int)node.Tag) & 1) != 0;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00019B9D File Offset: 0x00017D9D
		public bool CanTraverse(GraphNode from, GraphNode to)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.CanTraverse(this, from, to);
			}
			return to.Walkable && ((this.enabledTags >> (int)to.Tag) & 1) != 0;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019BD5 File Offset: 0x00017DD5
		public uint GetTraversalCost(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.GetTraversalCost(this, node);
			}
			return this.GetTagPenalty((int)node.Tag) + node.Penalty;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019C00 File Offset: 0x00017E00
		public bool IsDone()
		{
			return this.PipelineState > PathState.Processing;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00019C0C File Offset: 0x00017E0C
		void IPathInternals.AdvanceState(PathState s)
		{
			lock (this)
			{
				this.PipelineState = (PathState)Math.Max((int)this.PipelineState, (int)s);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00019C54 File Offset: 0x00017E54
		public void FailWithError(string msg)
		{
			this.Error();
			if (this.errorLog != "")
			{
				this.errorLog = this.errorLog + "\n" + msg;
				return;
			}
			this.errorLog = msg;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00019C8D File Offset: 0x00017E8D
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00019C98 File Offset: 0x00017E98
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				this.FailWithError("Please use the static Construct function for creating paths, do not use the normal constructors.");
			}
			if (((IPathInternals)this).Pooled)
			{
				this.FailWithError("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.pathHandler == null)
			{
				this.FailWithError("Field pathHandler is not set. Please report this bug.");
			}
			if (this.PipelineState > PathState.Processing)
			{
				this.FailWithError("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00019CF4 File Offset: 0x00017EF4
		protected virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(ref this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<GraphNode>.Release(ref this.path);
			}
			this.callback = null;
			this.immediateCallback = null;
			this.traversalProvider = null;
			this.pathHandler = null;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00019D44 File Offset: 0x00017F44
		protected virtual void Reset()
		{
			if (AstarPath.active == null)
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.PipelineState = PathState.Created;
			this.releasedNotSilent = false;
			this.pathHandler = null;
			this.callback = null;
			this.immediateCallback = null;
			this.errorLog = "";
			this.completeState = PathCompleteState.NotCalculated;
			this.path = ListPool<GraphNode>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.duration = 0f;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Walkable;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.pathID = AstarPath.active.GetNextPathID();
			this.hTargetNode = null;
			this.traversalProvider = null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00019E20 File Offset: 0x00018020
		public void Claim(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					throw new ArgumentException("You have already claimed the path with that object (" + ((o != null) ? o.ToString() : null) + "). Are you claiming the path with the same object twice?");
				}
			}
			this.claimed.Add(o);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00019E90 File Offset: 0x00018090
		public void Release(object o, bool silent = false)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					this.claimed.RemoveAt(i);
					if (!silent)
					{
						this.releasedNotSilent = true;
					}
					if (this.claimed.Count == 0 && this.releasedNotSilent)
					{
						PathPool.Pool(this);
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + ((o != null) ? o.ToString() : null) + ") twice?\nCheck out the documentation on path pooling for help.");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + ((o != null) ? o.ToString() : null) + "). Are you releasing the path with the same object twice?\nCheck out the documentation on path pooling for help.");
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019F50 File Offset: 0x00018150
		protected virtual void Trace(uint fromPathNodeIndex)
		{
			uint num = fromPathNodeIndex;
			int num2 = 0;
			UnsafeSpan<PathNode> pathNodes = this.pathHandler.pathNodes;
			while (num != 0U)
			{
				num = pathNodes[num].parentIndex;
				num2++;
				if (num2 > 16384)
				{
					Debug.LogWarning("Infinite loop? >16384 node path. Remove this message if you really have that long paths (Path.cs, Trace method)");
					break;
				}
			}
			if (this.path.Capacity < num2)
			{
				this.path.Capacity = num2;
			}
			num = fromPathNodeIndex;
			GraphNode graphNode = null;
			for (int i = 0; i < num2; i++)
			{
				GraphNode graphNode2;
				if (this.pathHandler.IsTemporaryNode(num))
				{
					graphNode2 = this.pathHandler.GetNode(this.pathHandler.GetTemporaryNode(num).associatedNode);
				}
				else
				{
					graphNode2 = this.pathHandler.GetNode(num);
				}
				if (graphNode2 != graphNode)
				{
					this.path.Add(graphNode2);
					graphNode = graphNode2;
				}
				num = pathNodes[num].parentIndex;
			}
			num2 = this.path.Count;
			int num3 = num2 / 2;
			for (int j = 0; j < num3; j++)
			{
				GraphNode graphNode3 = this.path[j];
				this.path[j] = this.path[num2 - j - 1];
				this.path[num2 - j - 1] = graphNode3;
			}
			if (this.vectorPath.Capacity < num2)
			{
				this.vectorPath.Capacity = num2;
			}
			for (int k = 0; k < num2; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001A0D4 File Offset: 0x000182D4
		protected void DebugStringPrefix(PathLog logMode, StringBuilder text)
		{
			text.Append(this.error ? "Path Failed : " : "Path Completed : ");
			text.Append("Computation Time ");
			text.Append(this.duration.ToString((logMode == PathLog.Heavy) ? "0.000 ms " : "0.00 ms "));
			text.Append("Searched Nodes ").Append(this.searchedNodes);
			if (!this.error)
			{
				text.Append(" Path Length ");
				text.Append((this.path == null) ? "Null" : this.path.Count.ToString());
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001A180 File Offset: 0x00018380
		protected void DebugStringSuffix(PathLog logMode, StringBuilder text)
		{
			if (this.error)
			{
				text.Append("\nError: ").Append(this.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.active.IsUsingMultithreading)
			{
				text.Append("\nCallback references ");
				if (this.callback != null)
				{
					text.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					text.AppendLine("NULL");
				}
			}
			text.Append("\nPath Number ").Append(this.pathID).Append(" (unique id)");
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001A220 File Offset: 0x00018420
		protected virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			this.DebugStringPrefix(logMode, debugStringBuilder);
			this.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001A26B File Offset: 0x0001846B
		protected virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001A284 File Offset: 0x00018484
		protected void PrepareBase(PathHandler pathHandler)
		{
			this.pathHandler = pathHandler;
			pathHandler.InitializeForPath(this);
			if (this.internalTagPenalties == null || this.internalTagPenalties.Length != 32)
			{
				this.internalTagPenalties = Path.ZeroTagPenalties;
			}
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.FailWithError(ex.Message);
			}
		}

		// Token: 0x06000552 RID: 1362
		protected abstract void Prepare();

		// Token: 0x06000553 RID: 1363 RVA: 0x0001A2E8 File Offset: 0x000184E8
		protected virtual void Cleanup()
		{
			UnsafeSpan<PathNode> pathNodes = this.pathHandler.pathNodes;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
				GraphNode node = this.pathHandler.GetNode(temporaryNode.associatedNode);
				uint num3 = 0U;
				while ((ulong)num3 < (ulong)((long)node.PathNodeVariants))
				{
					pathNodes[temporaryNode.associatedNode + num3].flag1 = false;
					pathNodes[temporaryNode.associatedNode + num3].flag2 = false;
					num3 += 1U;
				}
				num += 1U;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001A390 File Offset: 0x00018590
		protected int3 FirstTemporaryEndNode()
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
				if (temporaryNode.type == TemporaryNodeType.End)
				{
					return (int3)temporaryNode.position;
				}
				num += 1U;
			}
			throw new InvalidOperationException("There are no end nodes in the path");
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001A3F0 File Offset: 0x000185F0
		protected void TemporaryEndNodesBoundingBox(out int3 mn, out int3 mx)
		{
			mn = int.MaxValue;
			mx = int.MinValue;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
				if (temporaryNode.type == TemporaryNodeType.End)
				{
					mn = math.min(mn, (int3)temporaryNode.position);
					mx = math.max(mx, (int3)temporaryNode.position);
				}
				num += 1U;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001A494 File Offset: 0x00018694
		protected void MarkNodesAdjacentToTemporaryEndNodes()
		{
			UnsafeSpan<PathNode> pathNodes = this.pathHandler.pathNodes;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
				if (temporaryNode.type == TemporaryNodeType.End)
				{
					GraphNode node = this.pathHandler.GetNode(temporaryNode.associatedNode);
					uint num3 = 0U;
					while ((ulong)num3 < (ulong)((long)node.PathNodeVariants))
					{
						pathNodes[temporaryNode.associatedNode + num3].flag1 = true;
						num3 += 1U;
					}
				}
				num += 1U;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001A52C File Offset: 0x0001872C
		protected void AddStartNodesToHeap()
		{
			UnsafeSpan<PathNode> pathNodes = this.pathHandler.pathNodes;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
			{
				uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
				if (this.pathHandler.GetTemporaryNode(num2).type == TemporaryNodeType.Start)
				{
					this.pathHandler.heap.Add(pathNodes, num2, 0U, 0U);
				}
				num += 1U;
			}
		}

		// Token: 0x06000558 RID: 1368
		protected abstract void OnHeapExhausted();

		// Token: 0x06000559 RID: 1369
		protected abstract void OnFoundEndNode(uint pathNode, uint hScore, uint gScore);

		// Token: 0x0600055A RID: 1370 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnVisitNode(uint pathNode, uint hScore, uint gScore)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001A594 File Offset: 0x00018794
		protected unsafe virtual void CalculateStep(long targetTick)
		{
			int num = 0;
			UnsafeSpan<PathNode> pathNodes = this.pathHandler.pathNodes;
			uint temporaryNodeStartIndex = this.pathHandler.temporaryNodeStartIndex;
			while (this.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = this.searchedNodes;
				this.searchedNodes = searchedNodes + 1;
				if (this.pathHandler.heap.isEmpty)
				{
					this.OnHeapExhausted();
					return;
				}
				uint num3;
				uint num4;
				uint num2 = this.pathHandler.heap.Remove(pathNodes, out num3, out num4);
				uint num5 = num4 - num3;
				if (num2 >= temporaryNodeStartIndex)
				{
					TemporaryNode temporaryNode = *this.pathHandler.GetTemporaryNode(num2);
					if (temporaryNode.type == TemporaryNodeType.Start)
					{
						this.pathHandler.GetNode(temporaryNode.associatedNode).OpenAtPoint(this, num2, temporaryNode.position, num3);
					}
					else if (temporaryNode.type == TemporaryNodeType.End)
					{
						this.pathHandler.LogVisitedNode(temporaryNode.associatedNode, num5, num3);
						this.OnFoundEndNode(num2, num5, num3);
						if (this.CompleteState == PathCompleteState.Complete)
						{
							return;
						}
					}
				}
				else
				{
					this.pathHandler.LogVisitedNode(num2, num5, num3);
					this.OnVisitNode(num2, num5, num3);
					this.pathHandler.GetNode(num2).Open(this, num2, num3);
				}
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001A6F4 File Offset: 0x000188F4
		PathHandler IPathInternals.PathHandler
		{
			get
			{
				return this.pathHandler;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001A6FC File Offset: 0x000188FC
		void IPathInternals.OnEnterPool()
		{
			this.OnEnterPool();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001A704 File Offset: 0x00018904
		void IPathInternals.Reset()
		{
			this.Reset();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001A70C File Offset: 0x0001890C
		void IPathInternals.ReturnPath()
		{
			this.ReturnPath();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001A714 File Offset: 0x00018914
		void IPathInternals.PrepareBase(PathHandler handler)
		{
			this.PrepareBase(handler);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001A71D File Offset: 0x0001891D
		void IPathInternals.Prepare()
		{
			this.Prepare();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001A725 File Offset: 0x00018925
		void IPathInternals.Cleanup()
		{
			this.Cleanup();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001A72D File Offset: 0x0001892D
		void IPathInternals.CalculateStep(long targetTick)
		{
			this.CalculateStep(targetTick);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001A736 File Offset: 0x00018936
		string IPathInternals.DebugString(PathLog logMode)
		{
			return this.DebugString(logMode);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001A79C File Offset: 0x0001899C
		[BurstCompile]
		[MethodImpl(256)]
		public static void OpenCandidateConnectionBurst$BurstManaged(ref Path.OpenCandidateParams pars, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
		{
			ushort pathID = pars.pathID;
			uint parentPathNode = pars.parentPathNode;
			uint targetPathNode = pars.targetPathNode;
			uint candidateG = pars.candidateG;
			uint fractionAlongEdge = pars.fractionAlongEdge;
			int3 targetNodePosition = pars.targetNodePosition;
			UnsafeSpan<PathNode> pathNodes = pars.pathNodes;
			ref PathNode ptr = ref pathNodes[targetPathNode];
			if (ptr.pathID != pathID)
			{
				ptr.fractionAlongEdge = fractionAlongEdge;
				ptr.pathID = pathID;
				ptr.parentIndex = parentPathNode;
				uint num = (uint)heuristicObjective.Calculate(targetNodePosition, pars.targetNodeIndex);
				uint num2 = candidateG + num;
				heap.Add(pathNodes, targetPathNode, candidateG, num2);
				return;
			}
			uint g = heap.GetG((int)ptr.heapIndex);
			uint f = heap.GetF((int)ptr.heapIndex);
			uint num3 = f - g;
			uint num4;
			if (ptr.fractionAlongEdge != fractionAlongEdge)
			{
				num4 = (uint)heuristicObjective.Calculate(targetNodePosition, pars.targetNodeIndex);
			}
			else
			{
				num4 = num3;
			}
			uint num5 = candidateG + num4;
			if (num5 < f)
			{
				ptr.fractionAlongEdge = fractionAlongEdge;
				ptr.parentIndex = parentPathNode;
				heap.Add(pathNodes, targetPathNode, candidateG, num5);
			}
		}

		// Token: 0x04000371 RID: 881
		protected PathHandler pathHandler;

		// Token: 0x04000372 RID: 882
		public OnPathDelegate callback;

		// Token: 0x04000373 RID: 883
		public OnPathDelegate immediateCallback;

		// Token: 0x04000375 RID: 885
		public ITraversalProvider traversalProvider;

		// Token: 0x04000376 RID: 886
		protected PathCompleteState completeState;

		// Token: 0x04000378 RID: 888
		public List<GraphNode> path;

		// Token: 0x04000379 RID: 889
		public List<Vector3> vectorPath;

		// Token: 0x0400037A RID: 890
		public float duration;

		// Token: 0x0400037D RID: 893
		protected bool hasBeenReset;

		// Token: 0x0400037E RID: 894
		public NNConstraint nnConstraint = PathNNConstraint.Walkable;

		// Token: 0x0400037F RID: 895
		public Heuristic heuristic;

		// Token: 0x04000380 RID: 896
		public float heuristicScale = 1f;

		// Token: 0x04000382 RID: 898
		protected GraphNode hTargetNode;

		// Token: 0x04000383 RID: 899
		protected HeuristicObjective heuristicObjective;

		// Token: 0x04000384 RID: 900
		public int enabledTags = -1;

		// Token: 0x04000385 RID: 901
		internal static readonly int[] ZeroTagPenalties = new int[32];

		// Token: 0x04000386 RID: 902
		protected int[] internalTagPenalties;

		// Token: 0x04000387 RID: 903
		public static readonly ProfilerMarker MarkerOpenCandidateConnectionsToEnd = new ProfilerMarker("OpenCandidateConnectionsToEnd");

		// Token: 0x04000388 RID: 904
		public static readonly ProfilerMarker MarkerTrace = new ProfilerMarker("Trace");

		// Token: 0x04000389 RID: 905
		private List<object> claimed = new List<object>();

		// Token: 0x0400038A RID: 906
		private bool releasedNotSilent;

		// Token: 0x020000A8 RID: 168
		public struct OpenCandidateParams
		{
			// Token: 0x0400038B RID: 907
			public UnsafeSpan<PathNode> pathNodes;

			// Token: 0x0400038C RID: 908
			public uint parentPathNode;

			// Token: 0x0400038D RID: 909
			public uint targetPathNode;

			// Token: 0x0400038E RID: 910
			public uint targetNodeIndex;

			// Token: 0x0400038F RID: 911
			public uint candidateG;

			// Token: 0x04000390 RID: 912
			public uint fractionAlongEdge;

			// Token: 0x04000391 RID: 913
			public int3 targetNodePosition;

			// Token: 0x04000392 RID: 914
			public ushort pathID;
		}

		// Token: 0x020000AA RID: 170
		// (Invoke) Token: 0x0600056F RID: 1391
		public delegate void OpenCandidateConnectionBurst_000004FC$PostfixBurstDelegate(ref Path.OpenCandidateParams pars, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective);

		// Token: 0x020000AB RID: 171
		internal static class OpenCandidateConnectionBurst_000004FC$BurstDirectCall
		{
			// Token: 0x06000572 RID: 1394 RVA: 0x0001A913 File Offset: 0x00018B13
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Pointer == 0)
				{
					Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.DeferredCompilation, methodof(Path.OpenCandidateConnectionBurst$BurstManaged(ref Path.OpenCandidateParams, ref BinaryHeap, ref HeuristicObjective)).MethodHandle, typeof(Path.OpenCandidateConnectionBurst_000004FC$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Pointer;
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x0001A940 File Offset: 0x00018B40
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x0001A958 File Offset: 0x00018B58
			public static void Constructor()
			{
				Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(Path.OpenCandidateConnectionBurst(ref Path.OpenCandidateParams, ref BinaryHeap, ref HeuristicObjective)).MethodHandle);
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x0001A969 File Offset: 0x00018B69
			// Note: this type is marked as 'beforefieldinit'.
			static OpenCandidateConnectionBurst_000004FC$BurstDirectCall()
			{
				Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.Constructor();
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x0001A970 File Offset: 0x00018B70
			public static void Invoke(ref Path.OpenCandidateParams pars, ref BinaryHeap heap, ref HeuristicObjective heuristicObjective)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = Path.OpenCandidateConnectionBurst_000004FC$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Path/OpenCandidateParams&,Pathfinding.BinaryHeap&,Pathfinding.HeuristicObjective&), ref pars, ref heap, ref heuristicObjective, functionPointer);
						return;
					}
				}
				Path.OpenCandidateConnectionBurst$BurstManaged(ref pars, ref heap, ref heuristicObjective);
			}

			// Token: 0x04000396 RID: 918
			private static IntPtr Pointer;

			// Token: 0x04000397 RID: 919
			private static IntPtr DeferredCompilation;
		}
	}
}
