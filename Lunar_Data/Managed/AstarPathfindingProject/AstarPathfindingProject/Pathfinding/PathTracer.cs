using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Pathfinding.Drawing;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200014D RID: 333
	[BurstCompile]
	public struct PathTracer
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00036BAA File Offset: 0x00034DAA
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00036BB2 File Offset: 0x00034DB2
		public ushort version { readonly get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00036BBB File Offset: 0x00034DBB
		public readonly bool isCreated
		{
			get
			{
				return this.funnelState.unwrappedPortals.IsCreated;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00036BCD File Offset: 0x00034DCD
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00036BEC File Offset: 0x00034DEC
		public GraphNode startNode
		{
			readonly get
			{
				if (this.startNodeInternal == null || this.startNodeInternal.Destroyed)
				{
					return null;
				}
				return this.startNodeInternal;
			}
			set
			{
				this.startNodeInternal = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00036BF5 File Offset: 0x00034DF5
		public readonly bool isStale
		{
			get
			{
				return !this.endIsUpToDate || !this.startIsUpToDate || this.firstPartContainsDestroyedNodes;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00036C0F File Offset: 0x00034E0F
		public readonly int partCount
		{
			get
			{
				if (this.parts == null)
				{
					return 0;
				}
				return this.parts.Length - this.firstPartIndex;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00036C2A File Offset: 0x00034E2A
		public readonly bool hasPath
		{
			get
			{
				return this.partCount > 0;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00036C35 File Offset: 0x00034E35
		public readonly Vector3 startPoint
		{
			get
			{
				return this.parts[this.firstPartIndex].startPoint;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00036C4D File Offset: 0x00034E4D
		public readonly Vector3 endPoint
		{
			get
			{
				return this.parts[this.parts.Length - 1].endPoint;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00036C69 File Offset: 0x00034E69
		public readonly Vector3 endPointOfFirstPart
		{
			get
			{
				return this.parts[this.firstPartIndex].endPoint;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00036C81 File Offset: 0x00034E81
		public int desiredCornersForGoodSimplification
		{
			get
			{
				if (this.partGraphType != PathTracer.PartGraphType.Grid)
				{
					return 2;
				}
				return 3;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00036C8F File Offset: 0x00034E8F
		public bool isNextPartValidLink
		{
			get
			{
				return this.partCount > 1 && this.GetPartType(1) == Funnel.PartType.OffMeshLink && !this.PartContainsDestroyedNodes(1);
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00036CB0 File Offset: 0x00034EB0
		public PathTracer(Allocator allocator)
		{
			this.funnelState = new Funnel.FunnelState(16, allocator);
			this.parts = null;
			this.nodes = new CircularBuffer<GraphNode>(16);
			this.portalIsNotInnerCorner = new CircularBuffer<byte>(16);
			this.unclampedEndPoint = (this.unclampedStartPoint = Vector3.zero);
			this.firstPartIndex = 0;
			this.startIsUpToDate = false;
			this.endIsUpToDate = false;
			this.firstPartContainsDestroyedNodes = false;
			this.startNodeInternal = null;
			this.version = 1;
			this.nnConstraint = NNConstraint.Walkable;
			this.partGraphType = PathTracer.PartGraphType.Navmesh;
			this.Clear();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00036D42 File Offset: 0x00034F42
		public void Dispose()
		{
			this.Clear();
			this.funnelState.Dispose();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00036D55 File Offset: 0x00034F55
		public Vector3 UpdateStart(Vector3 position, PathTracer.RepairQuality quality, NativeMovementPlane movementPlane, ITraversalProvider traversalProvider, Path path)
		{
			this.Repair(position, true, quality, movementPlane, traversalProvider, path, true);
			return this.parts[this.firstPartIndex].startPoint;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00036D7C File Offset: 0x00034F7C
		public Vector3 UpdateEnd(Vector3 position, PathTracer.RepairQuality quality, NativeMovementPlane movementPlane, ITraversalProvider traversalProvider, Path path)
		{
			this.Repair(position, false, quality, movementPlane, traversalProvider, path, true);
			return this.parts[this.parts.Length - 1].endPoint;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00036DA8 File Offset: 0x00034FA8
		private void AppendNode(bool toStart, GraphNode node)
		{
			int num = (toStart ? this.firstPartIndex : (this.parts.Length - 1));
			ref Funnel.PathPart ptr = ref this.parts[num];
			GraphNode graphNode = ((ptr.endIndex >= ptr.startIndex) ? this.nodes.GetBoundaryValue(toStart) : null);
			if (node == graphNode)
			{
				return;
			}
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			if (ptr.endIndex >= ptr.startIndex + 1 && this.nodes.GetAbsolute(toStart ? (ptr.startIndex + 1) : (ptr.endIndex - 1)) == node)
			{
				if (toStart)
				{
					ptr.startIndex++;
				}
				else
				{
					ptr.endIndex--;
				}
				this.nodes.Pop(toStart);
				if (num == this.firstPartIndex && this.funnelState.leftFunnel.Length > 0)
				{
					this.funnelState.Pop(toStart);
					this.portalIsNotInnerCorner.Pop(toStart);
				}
				return;
			}
			if (num == this.firstPartIndex && graphNode != null)
			{
				Vector3 vector;
				Vector3 vector2;
				if (toStart)
				{
					if (!node.GetPortal(graphNode, out vector, out vector2))
					{
						throw new NotImplementedException();
					}
				}
				else if (!graphNode.GetPortal(node, out vector, out vector2))
				{
					throw new NotImplementedException();
				}
				this.funnelState.Push(toStart, vector, vector2);
				this.portalIsNotInnerCorner.Push(toStart, 0);
			}
			this.nodes.Push(toStart, node);
			if (toStart)
			{
				ptr.startIndex--;
				return;
			}
			ptr.endIndex++;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00036F10 File Offset: 0x00035110
		private unsafe void AppendPath(bool toStart, CircularBuffer<GraphNode> path)
		{
			if (path.Length == 0)
			{
				return;
			}
			while (path.Length > 0)
			{
				this.AppendNode(toStart, path.PopStart());
			}
			if (toStart)
			{
				this.startNode = *this.nodes.First;
				int num = Mathf.Min(this.parts[this.firstPartIndex].startIndex + 5, this.parts[this.firstPartIndex].endIndex);
				bool flag = false;
				for (int i = this.parts[this.firstPartIndex].startIndex; i <= num; i++)
				{
					flag |= !PathTracer.Valid(this.nodes.GetAbsolute(i));
				}
				this.firstPartContainsDestroyedNodes = flag;
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000033F6 File Offset: 0x000015F6
		[Conditional("UNITY_EDITOR")]
		private void CheckInvariants()
		{
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00036FCC File Offset: 0x000351CC
		private bool SplicePath(int startIndex, int toRemove, List<GraphNode> toInsert)
		{
			ref Funnel.PathPart ptr = ref this.parts[this.firstPartIndex];
			if (startIndex < ptr.startIndex || startIndex + toRemove - 1 > ptr.endIndex)
			{
				throw new ArgumentException("This method can only handle splicing the first part of the path");
			}
			if (toInsert != null)
			{
				int num = 0;
				int num2 = 0;
				while (num < toInsert.Count && num < toRemove && toInsert[num] == this.nodes.GetAbsolute(startIndex + num))
				{
					num++;
				}
				if (num == toInsert.Count && num == toRemove)
				{
					return true;
				}
				while (num2 < toInsert.Count - num && num2 < toRemove - num && toInsert[toInsert.Count - num2 - 1] == this.nodes.GetAbsolute(startIndex + toRemove - num2 - 1))
				{
					num2++;
				}
				toInsert.RemoveRange(toInsert.Count - num2, num2);
				toInsert.RemoveRange(0, num);
				startIndex += num;
				toRemove -= num + num2;
			}
			int num3 = ((toInsert != null) ? toInsert.Count : 0);
			if (startIndex - 1 >= ptr.startIndex && !PathTracer.Valid(this.nodes.GetAbsolute(startIndex - 1)))
			{
				return false;
			}
			if (startIndex + toRemove <= ptr.endIndex && !PathTracer.Valid(this.nodes.GetAbsolute(startIndex + toRemove)))
			{
				return false;
			}
			this.nodes.SpliceAbsolute(startIndex, toRemove, toInsert);
			int num4 = num3 - toRemove;
			int num5 = math.max(startIndex - 1, ptr.startIndex);
			int num6 = math.min(startIndex + toRemove, ptr.endIndex) - num5;
			ptr.endIndex += num4;
			for (int i = this.firstPartIndex + 1; i < this.parts.Length; i++)
			{
				Funnel.PathPart[] array = this.parts;
				int num7 = i;
				array[num7].startIndex = array[num7].startIndex + num4;
				Funnel.PathPart[] array2 = this.parts;
				int num8 = i;
				array2[num8].endIndex = array2[num8].endIndex + num4;
			}
			List<float3> list = ListPool<float3>.Claim();
			List<float3> list2 = ListPool<float3>.Claim();
			int num9 = startIndex + num3 - 1;
			int num10 = math.max(startIndex - 1, ptr.startIndex);
			int num11 = math.min(num9 + 1, ptr.endIndex);
			this.CalculateFunnelPortals(num10, num11, list, list2);
			this.funnelState.Splice(num10 - ptr.startIndex, num6, list, list2);
			this.portalIsNotInnerCorner.SpliceUninitialized(num10 - ptr.startIndex, num6, list.Count);
			for (int j = 0; j < list.Count; j++)
			{
				this.portalIsNotInnerCorner[num10 - ptr.startIndex + j] = 0;
			}
			ListPool<float3>.Release(ref list);
			ListPool<float3>.Release(ref list2);
			return true;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00037254 File Offset: 0x00035454
		private static bool ContainsPoint(GraphNode node, Vector3 point, NativeMovementPlane plane)
		{
			TriangleMeshNode triangleMeshNode = node as TriangleMeshNode;
			if (triangleMeshNode != null)
			{
				return triangleMeshNode.ContainsPoint(point, plane);
			}
			return node.ContainsPoint(point);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003727B File Offset: 0x0003547B
		[BurstCompile]
		private static bool ContainsAndProject(ref Int3 a, ref Int3 b, ref Int3 c, ref Vector3 p, float height, ref NativeMovementPlane movementPlane, out Vector3 projected)
		{
			return PathTracer.ContainsAndProject_00000949$BurstDirectCall.Invoke(ref a, ref b, ref c, ref p, height, ref movementPlane, out projected);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0003728C File Offset: 0x0003548C
		private static float3 ProjectOnSurface(float3 a, float3 b, float3 c, float3 p, float3 up)
		{
			float3 @float = math.cross(c - a, b - a);
			float num = math.dot(@float, up);
			if (math.abs(num) > 1.1754944E-38f)
			{
				float3 float2 = p - a;
				float num2 = -math.dot(@float, float2) / num;
				return p + num2 * up;
			}
			return p;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000372E8 File Offset: 0x000354E8
		private void Repair(Vector3 point, bool isStart, PathTracer.RepairQuality quality, NativeMovementPlane movementPlane, ITraversalProvider traversalProvider, Path path, bool allowCache = true)
		{
			int num;
			GraphNode graphNode;
			bool flag;
			if (isStart)
			{
				num = this.firstPartIndex;
				graphNode = this.nodes.GetAbsolute(this.parts[num].startIndex);
				flag = this.unclampedStartPoint == point;
			}
			else
			{
				num = this.parts.Length - 1;
				graphNode = this.nodes.GetAbsolute(this.parts[num].endIndex);
				flag = this.unclampedEndPoint == point;
			}
			if (allowCache && flag && PathTracer.Valid(graphNode))
			{
				return;
			}
			if (this.partGraphType == PathTracer.PartGraphType.OffMeshLink)
			{
				throw new InvalidOperationException("Cannot repair path while on an off-mesh link");
			}
			ref Funnel.PathPart ptr = ref this.parts[num];
			ushort num2;
			if (float.IsFinite(point.x))
			{
				if (PathTracer.Valid(graphNode))
				{
					bool flag2 = false;
					Vector3 vector = Vector3.zero;
					TriangleMeshNode triangleMeshNode = graphNode as TriangleMeshNode;
					if (triangleMeshNode != null)
					{
						Int3 @int;
						Int3 int2;
						Int3 int3;
						triangleMeshNode.GetVertices(out @int, out int2, out int3);
						flag2 = PathTracer.ContainsAndProject(ref @int, ref int2, ref int3, ref point, 1f, ref movementPlane, out vector);
					}
					else
					{
						GridNodeBase gridNodeBase = graphNode as GridNodeBase;
						if (gridNodeBase != null && gridNodeBase.ContainsPoint(point))
						{
							flag2 = true;
							vector = gridNodeBase.ClosestPointOnNode(point);
						}
					}
					if (flag2)
					{
						if (isStart)
						{
							ptr.startPoint = vector;
							this.unclampedStartPoint = point;
							this.startIsUpToDate = true;
							this.startNode = graphNode;
						}
						else
						{
							ptr.endPoint = vector;
							this.unclampedEndPoint = point;
							this.endIsUpToDate = true;
						}
						num2 = this.version;
						this.version = num2 + 1;
						return;
					}
				}
				this.RepairFull(point, isStart, quality, movementPlane, traversalProvider, path);
				num2 = this.version;
				this.version = num2 + 1;
				return;
			}
			if (isStart)
			{
				throw new ArgumentException("Position must be a finite vector");
			}
			this.unclampedEndPoint = point;
			this.endIsUpToDate = false;
			this.RemoveAllPartsExceptFirst();
			ref Funnel.PathPart ptr2 = ref this.parts[this.firstPartIndex];
			if (ptr2.endIndex > ptr2.startIndex)
			{
				this.SplicePath(ptr2.startIndex + 1, ptr2.endIndex - ptr2.startIndex, null);
			}
			ptr2.endPoint = ptr2.startPoint;
			num2 = this.version;
			this.version = num2 + 1;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00037504 File Offset: 0x00035704
		private unsafe void HeuristicallyPopPortals(bool isStartOfPart, Vector3 point)
		{
			ref Funnel.PathPart ptr = ref this.parts[this.firstPartIndex];
			if (isStartOfPart)
			{
				while (this.funnelState.IsReasonableToPopStart(point, ptr.endPoint))
				{
					ptr.startIndex++;
					this.nodes.PopStart();
					this.funnelState.PopStart();
					this.portalIsNotInnerCorner.PopStart();
				}
				if (PathTracer.Valid(*this.nodes.First))
				{
					this.startNode = *this.nodes.First;
				}
			}
			else
			{
				int num = 0;
				while (this.funnelState.IsReasonableToPopEnd(ptr.startPoint, point))
				{
					ptr.endIndex--;
					num++;
					this.funnelState.PopEnd();
					this.portalIsNotInnerCorner.PopEnd();
				}
				if (num > 0)
				{
					this.nodes.SpliceAbsolute(ptr.endIndex + 1, num, null);
					for (int i = this.firstPartIndex + 1; i < this.parts.Length; i++)
					{
						Funnel.PathPart[] array = this.parts;
						int num2 = i;
						array[num2].startIndex = array[num2].startIndex - num;
						Funnel.PathPart[] array2 = this.parts;
						int num3 = i;
						array2[num3].endIndex = array2[num3].endIndex - num;
					}
				}
			}
			int num4 = Mathf.Min(ptr.startIndex + 5, ptr.endIndex);
			bool flag = false;
			for (int j = ptr.startIndex; j <= num4; j++)
			{
				flag |= !PathTracer.Valid(this.nodes.GetAbsolute(j));
			}
			this.firstPartContainsDestroyedNodes = flag;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00037698 File Offset: 0x00035898
		[MethodImpl(256)]
		private static bool Valid(GraphNode node)
		{
			return !node.Destroyed && node.Walkable;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000376AC File Offset: 0x000358AC
		private unsafe void RepairFull(Vector3 point, bool isStart, PathTracer.RepairQuality quality, NativeMovementPlane movementPlane, ITraversalProvider traversalProvider, Path path)
		{
			int num = ((quality == PathTracer.RepairQuality.High) ? 16 : 9);
			int num2 = (isStart ? this.firstPartIndex : (this.parts.Length - 1));
			ref Funnel.PathPart ptr = ref this.parts[num2];
			GraphNode graphNode = this.nodes.GetAbsolute(isStart ? ptr.startIndex : ptr.endIndex);
			if ((!PathTracer.Valid(graphNode) || (ptr.endIndex != ptr.startIndex && !PathTracer.Valid(this.nodes.GetAbsolute(isStart ? (ptr.startIndex + 1) : (ptr.endIndex - 1))))) && num2 == this.firstPartIndex)
			{
				this.HeuristicallyPopPortals(isStart, point);
				graphNode = this.nodes.GetAbsolute(isStart ? ptr.startIndex : ptr.endIndex);
			}
			if (!PathTracer.Valid(graphNode))
			{
				if (!isStart)
				{
					this.unclampedEndPoint = point;
					ptr.endPoint = point;
					this.endIsUpToDate = false;
					return;
				}
				this.firstPartContainsDestroyedNodes = true;
				this.unclampedStartPoint = point;
				this.startIsUpToDate = false;
				NNConstraint nnconstraint = this.nnConstraint;
				nnconstraint.constrainArea = false;
				nnconstraint.distanceMetric = DistanceMetric.ClosestAsSeenFromAboveSoft(movementPlane.ToWorld(float2.zero, 1f));
				GraphNode graphNode2 = ((AstarPath.active != null) ? AstarPath.active.GetNearest(point, nnconstraint).node : null);
				if (traversalProvider != null && !traversalProvider.CanTraverse(path, graphNode2))
				{
					graphNode2 = null;
				}
				this.startNode = graphNode2;
				if (this.startNode == null)
				{
					ptr.startPoint = point;
					return;
				}
				ptr.startPoint = this.startNode.ClosestPointOnNode(point);
				if (ptr.endIndex - ptr.startIndex < 10 && this.partCount <= 1)
				{
					Vector3 startPoint = ptr.startPoint;
					this.Clear();
					this.startNode = graphNode2;
					this.partGraphType = PathTracer.PartGraphTypeFromNode(this.startNode);
					this.unclampedStartPoint = point;
					this.unclampedEndPoint = startPoint;
					this.nodes.PushEnd(graphNode2);
					this.parts = new Funnel.PathPart[1];
					this.parts[0] = new Funnel.PathPart
					{
						startIndex = this.nodes.AbsoluteStartIndex,
						endIndex = this.nodes.AbsoluteEndIndex,
						startPoint = startPoint,
						endPoint = startPoint
					};
					return;
				}
			}
			else
			{
				CircularBuffer<GraphNode> circularBuffer = this.LocalSearch(graphNode, point, num, movementPlane, isStart, traversalProvider, path);
				GraphNode graphNode3 = *circularBuffer.Last;
				NNConstraint nnconstraint2 = this.nnConstraint;
				nnconstraint2.constrainArea = true;
				nnconstraint2.area = (int)graphNode3.Area;
				NNInfo nearest = AstarPath.active.GetNearest(point, nnconstraint2);
				Vector3 vector = (isStart ? ptr.startPoint : ptr.endPoint);
				bool flag;
				Vector3 vector2;
				if (nearest.node == graphNode3)
				{
					flag = true;
					vector2 = nearest.position;
				}
				else
				{
					float sqrMagnitude = ((isStart ? this.unclampedStartPoint : this.unclampedEndPoint) - vector).sqrMagnitude;
					bool flag2 = (isStart ? this.startIsUpToDate : this.endIsUpToDate);
					vector2 = graphNode3.ClosestPointOnNode(point);
					float sqrMagnitude2 = (point - vector2).sqrMagnitude;
					flag = flag2 && sqrMagnitude2 <= sqrMagnitude + 0.010000001f;
				}
				if (!flag && !isStart)
				{
					circularBuffer.Clear();
					vector2 = vector;
				}
				this.AppendPath(isStart, circularBuffer);
				circularBuffer.Pool();
				if (isStart)
				{
					this.startNode = *this.nodes.First;
				}
				if (isStart)
				{
					this.unclampedStartPoint = point;
					ptr.startPoint = vector2;
					this.startIsUpToDate = true;
					return;
				}
				this.unclampedEndPoint = point;
				ptr.endPoint = vector2;
				this.endIsUpToDate = flag;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00037A4C File Offset: 0x00035C4C
		private static float SquaredDistanceToNode(GraphNode node, Vector3 point, ref BBTree.ProjectionParams projectionParams)
		{
			TriangleMeshNode triangleMeshNode = node as TriangleMeshNode;
			if (triangleMeshNode != null)
			{
				Int3 @int;
				Int3 int2;
				Int3 int3;
				triangleMeshNode.GetVerticesInGraphSpace(out @int, out int2, out int3);
				float3 @float;
				float num;
				float num2;
				Polygon.ClosestPointOnTriangleProjected(ref @int, ref int2, ref int3, ref projectionParams, UnsafeUtility.As<Vector3, float3>(ref point), out @float, out num, out num2);
				return num;
			}
			GridNodeBase gridNodeBase = node as GridNodeBase;
			if (gridNodeBase != null)
			{
				Int2 coordinatesInGrid = gridNodeBase.CoordinatesInGrid;
				float num3 = math.clamp(point.x, (float)coordinatesInGrid.x, (float)(coordinatesInGrid.x + 1));
				float num4 = math.clamp(point.z, (float)coordinatesInGrid.y, (float)(coordinatesInGrid.y + 1));
				return math.lengthsq(new float3(num3, 0f, num4) - point);
			}
			Vector3 vector = node.ClosestPointOnNode(point);
			return (point - vector).sqrMagnitude;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00037B14 File Offset: 0x00035D14
		private static bool QueueHasNode(PathTracer.QueueItem[] queue, int count, GraphNode node)
		{
			for (int i = 0; i < count; i++)
			{
				if (queue[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00037B3F File Offset: 0x00035D3F
		private void GetTempQueue(out PathTracer.QueueItem[] queue, out List<GraphNode> connections)
		{
			queue = new PathTracer.QueueItem[16];
			connections = new List<GraphNode>();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00037B54 File Offset: 0x00035D54
		private CircularBuffer<GraphNode> LocalSearch(GraphNode currentNode, Vector3 point, int maxNodesToSearch, NativeMovementPlane movementPlane, bool reverse, ITraversalProvider traversalProvider, Path path)
		{
			NNConstraint nnconstraint = this.nnConstraint;
			nnconstraint.constrainArea = false;
			nnconstraint.distanceMetric = DistanceMetric.ClosestAsSeenFromAboveSoft(movementPlane.up);
			PathTracer.QueueItem[] array;
			List<GraphNode> list;
			this.GetTempQueue(out array, out list);
			int i = 0;
			int num = 0;
			NavGraph graph = currentNode.Graph;
			BBTree.ProjectionParams projectionParams;
			Vector3 vector;
			if (this.partGraphType == PathTracer.PartGraphType.Navmesh)
			{
				NavmeshBase navmeshBase = graph as NavmeshBase;
				projectionParams = new BBTree.ProjectionParams(nnconstraint, navmeshBase.transform);
				vector = navmeshBase.transform.InverseTransform(point);
			}
			else if (this.partGraphType == PathTracer.PartGraphType.Grid)
			{
				projectionParams = default(BBTree.ProjectionParams);
				vector = (graph as GridGraph).transform.InverseTransform(point);
			}
			else
			{
				projectionParams = default(BBTree.ProjectionParams);
				vector = point;
			}
			float num2 = PathTracer.SquaredDistanceToNode(currentNode, vector, ref projectionParams);
			array[0] = new PathTracer.QueueItem
			{
				node = currentNode,
				parent = -1,
				distance = num2
			};
			num++;
			int num3 = 0;
			while (i < num)
			{
				int num4 = i;
				GraphNode node2 = array[num4].node;
				i++;
				if (PathTracer.ContainsPoint(node2, point, movementPlane))
				{
					num3 = num4;
					break;
				}
				float distance = array[num4].distance;
				if (distance < num2)
				{
					num2 = distance;
					num3 = num4;
				}
				float num5 = distance * 1.1024998f + 0.05f;
				node2.GetConnections<List<GraphNode>>(delegate(GraphNode node, ref List<GraphNode> ls)
				{
					ls.Add(node);
				}, ref list, 32);
				for (int j = 0; j < list.Count; j++)
				{
					GraphNode graphNode = list[j];
					if (num < maxNodesToSearch && graphNode.GraphIndex == node2.GraphIndex && nnconstraint.Suitable(graphNode) && (traversalProvider == null || (reverse ? (traversalProvider.CanTraverse(path, graphNode) && traversalProvider.CanTraverse(path, graphNode, node2)) : traversalProvider.CanTraverse(path, node2, graphNode))))
					{
						float num6 = PathTracer.SquaredDistanceToNode(graphNode, vector, ref projectionParams);
						if (num6 < num5 && !PathTracer.QueueHasNode(array, num, graphNode))
						{
							array[num] = new PathTracer.QueueItem
							{
								node = graphNode,
								parent = num4,
								distance = num6
							};
							num++;
						}
					}
				}
				list.Clear();
			}
			CircularBuffer<GraphNode> circularBuffer = new CircularBuffer<GraphNode>(8);
			while (num3 != -1)
			{
				circularBuffer.PushStart(array[num3].node);
				num3 = array[num3].parent;
			}
			list.Clear();
			for (int k = 0; k < num; k++)
			{
				array[k].node = null;
			}
			if (this.partGraphType == PathTracer.PartGraphType.Grid)
			{
				PathTracer.RemoveGridPathDiagonals(null, 0, ref circularBuffer, this.nnConstraint, traversalProvider, path);
			}
			return circularBuffer;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00037E28 File Offset: 0x00036028
		public void DrawFunnel(CommandBuilder draw, NativeMovementPlane movementPlane)
		{
			if (this.parts == null)
			{
				return;
			}
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
			this.funnelState.PushStart(pathPart.startPoint, pathPart.startPoint);
			this.funnelState.PushEnd(pathPart.endPoint, pathPart.endPoint);
			using (draw.WithLineWidth(2f, true))
			{
				draw.Polyline<NativeCircularBuffer<float3>>(this.funnelState.leftFunnel, false);
				draw.Polyline<NativeCircularBuffer<float3>>(this.funnelState.rightFunnel, false);
			}
			if (this.funnelState.unwrappedPortals.Length > 1)
			{
				using (draw.WithLineWidth(1f, true))
				{
					float3 up = movementPlane.up;
					float4x3 float4x = this.funnelState.UnwrappedPortalsToWorldMatrix(up);
					float4x4 float4x2 = new float4x4(float4x.c0, float4x.c1, new float4(0f, 0f, 1f, 0f), float4x.c2);
					using (draw.WithMatrix(float4x2))
					{
						float2 @float = this.funnelState.unwrappedPortals[0].xy;
						float2 float2 = this.funnelState.unwrappedPortals[1].xy;
						for (int i = 0; i < this.funnelState.unwrappedPortals.Length; i++)
						{
							float2 xy = this.funnelState.unwrappedPortals[i].xy;
							float2 zw = this.funnelState.unwrappedPortals[i].zw;
							draw.xy.Line(xy, zw, Palette.Colorbrewer.Set1.Brown);
							draw.xy.Line(@float, xy, Palette.Colorbrewer.Set1.Brown);
							draw.xy.Line(float2, zw, Palette.Colorbrewer.Set1.Brown);
							@float = xy;
							float2 = zw;
						}
					}
				}
			}
			using (draw.WithColor(new Color(0f, 0f, 0f, 0.2f)))
			{
				for (int j = 0; j < this.funnelState.leftFunnel.Length - 1; j++)
				{
					draw.SolidTriangle(this.funnelState.leftFunnel[j], this.funnelState.rightFunnel[j], this.funnelState.rightFunnel[j + 1]);
					draw.SolidTriangle(this.funnelState.leftFunnel[j], this.funnelState.leftFunnel[j + 1], this.funnelState.rightFunnel[j + 1]);
				}
			}
			using (draw.WithColor(new Color(0f, 0f, 1f, 0.5f)))
			{
				for (int k = 0; k < this.funnelState.leftFunnel.Length; k++)
				{
					draw.Line(this.funnelState.leftFunnel[k], this.funnelState.rightFunnel[k]);
				}
			}
			this.funnelState.PopStart();
			this.funnelState.PopEnd();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00038238 File Offset: 0x00036438
		private static Int3 MaybeSetYZero(Int3 p, bool setYToZero)
		{
			if (setYToZero)
			{
				p.y = 0;
			}
			return p;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00038248 File Offset: 0x00036448
		private static bool IsInnerVertex(CircularBuffer<GraphNode> nodes, Funnel.PathPart part, int portalIndex, bool rightSide, List<GraphNode> alternativeNodes, NNConstraint nnConstraint, out int startIndex, out int endIndex, ITraversalProvider traversalProvider, Path path)
		{
			GraphNode absolute = nodes.GetAbsolute(portalIndex);
			if (absolute is TriangleMeshNode)
			{
				return PathTracer.IsInnerVertexTriangleMesh(nodes, part, portalIndex, rightSide, alternativeNodes, nnConstraint, out startIndex, out endIndex, traversalProvider, path);
			}
			if (absolute is GridNodeBase)
			{
				return PathTracer.IsInnerVertexGrid(nodes, part, portalIndex, rightSide, alternativeNodes, nnConstraint, out startIndex, out endIndex, traversalProvider, path);
			}
			startIndex = portalIndex;
			endIndex = portalIndex + 1;
			return false;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000382A8 File Offset: 0x000364A8
		private static bool IsInnerVertexGrid(CircularBuffer<GraphNode> nodes, Funnel.PathPart part, int portalIndex, bool rightSide, List<GraphNode> alternativeNodes, NNConstraint nnConstraint, out int startIndex, out int endIndex, ITraversalProvider traversalProvider, Path path)
		{
			startIndex = portalIndex;
			endIndex = portalIndex + 1;
			return false;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000382C0 File Offset: 0x000364C0
		private static bool IsInnerVertexTriangleMesh(CircularBuffer<GraphNode> nodes, Funnel.PathPart part, int portalIndex, bool rightSide, List<GraphNode> alternativeNodes, NNConstraint nnConstraint, out int startIndex, out int endIndex, ITraversalProvider traversalProvider, Path path)
		{
			startIndex = portalIndex;
			endIndex = portalIndex + 1;
			TriangleMeshNode triangleMeshNode = nodes.GetAbsolute(startIndex) as TriangleMeshNode;
			TriangleMeshNode triangleMeshNode2 = nodes.GetAbsolute(endIndex) as TriangleMeshNode;
			if (triangleMeshNode == null || triangleMeshNode2 == null || !PathTracer.Valid(triangleMeshNode) || !PathTracer.Valid(triangleMeshNode2))
			{
				return false;
			}
			Int3 @int;
			Int3 int2;
			int num;
			int num2;
			if (!triangleMeshNode.GetPortalInGraphSpace(triangleMeshNode2, out @int, out int2, out num, out num2))
			{
				return false;
			}
			bool recalculateNormals = (triangleMeshNode.Graph as NavmeshBase).RecalculateNormals;
			Int3 int3 = PathTracer.MaybeSetYZero(rightSide ? int2 : @int, recalculateNormals);
			while (startIndex > part.startIndex)
			{
				TriangleMeshNode triangleMeshNode3 = nodes.GetAbsolute(startIndex - 1) as TriangleMeshNode;
				Int3 int4;
				Int3 int5;
				if (triangleMeshNode3 == null || !PathTracer.Valid(triangleMeshNode3) || !triangleMeshNode3.GetPortalInGraphSpace(triangleMeshNode, out int4, out int5, out num2, out num) || !(PathTracer.MaybeSetYZero(rightSide ? int5 : int4, recalculateNormals) == int3))
				{
					break;
				}
				triangleMeshNode = triangleMeshNode3;
				startIndex--;
			}
			while (endIndex < part.endIndex)
			{
				TriangleMeshNode triangleMeshNode4 = nodes.GetAbsolute(endIndex + 1) as TriangleMeshNode;
				Int3 int6;
				Int3 int7;
				if (triangleMeshNode4 == null || !PathTracer.Valid(triangleMeshNode4) || !triangleMeshNode2.GetPortalInGraphSpace(triangleMeshNode4, out int6, out int7, out num, out num2) || !(PathTracer.MaybeSetYZero(rightSide ? int7 : int6, recalculateNormals) == int3))
				{
					break;
				}
				triangleMeshNode2 = triangleMeshNode4;
				endIndex++;
				if (triangleMeshNode2 == triangleMeshNode)
				{
					break;
				}
			}
			TriangleMeshNode triangleMeshNode5 = triangleMeshNode;
			int num3 = 0;
			alternativeNodes.Add(triangleMeshNode);
			if (triangleMeshNode == triangleMeshNode2)
			{
				return true;
			}
			for (;;)
			{
				bool flag = false;
				int i = 0;
				while (i < triangleMeshNode5.connections.Length)
				{
					TriangleMeshNode triangleMeshNode6 = triangleMeshNode5.connections[i].node as TriangleMeshNode;
					Int3 int8;
					Int3 int9;
					if (triangleMeshNode6 != null && ((traversalProvider != null) ? traversalProvider.CanTraverse(path, triangleMeshNode5, triangleMeshNode6) : nnConstraint.Suitable(triangleMeshNode6)) && triangleMeshNode5.connections[i].isOutgoing && triangleMeshNode5.GetPortalInGraphSpace(triangleMeshNode6, out int8, out int9, out num2, out num) && PathTracer.MaybeSetYZero(rightSide ? int8 : int9, recalculateNormals) == int3)
					{
						triangleMeshNode5 = triangleMeshNode6;
						alternativeNodes.Add(triangleMeshNode5);
						if (triangleMeshNode5 == triangleMeshNode2)
						{
							return true;
						}
						if (num3++ > 100)
						{
							goto Block_27;
						}
						flag = true;
						break;
					}
					else
					{
						i++;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
			Block_27:
			throw new Exception("Caught in a potentially infinite loop. The navmesh probably contains degenerate geometry.");
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000384F8 File Offset: 0x000366F8
		private bool FirstInnerVertex(NativeArray<int> indices, int numCorners, List<GraphNode> alternativePath, out int alternativeStartIndex, out int alternativeEndIndex, ITraversalProvider traversalProvider, Path path)
		{
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
			this.nnConstraint.constrainArea = false;
			for (int i = 0; i < numCorners; i++)
			{
				int num = indices[i];
				bool flag = (num & 1073741824) != 0;
				int num2 = num & 1073741823;
				if ((this.portalIsNotInnerCorner[num2] & ((flag || 2 != 0) ? 1 : 0)) == 0)
				{
					alternativePath.Clear();
					if (PathTracer.IsInnerVertex(this.nodes, pathPart, pathPart.startIndex + num2, flag, alternativePath, this.nnConstraint, out alternativeStartIndex, out alternativeEndIndex, traversalProvider, path))
					{
						return true;
					}
					this.portalIsNotInnerCorner[num2] = this.portalIsNotInnerCorner[num2] | (flag ? 1 : 2);
				}
			}
			alternativeStartIndex = -1;
			alternativeEndIndex = -1;
			return false;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000385BD File Offset: 0x000367BD
		public float EstimateRemainingPath(int maxCorners, ref NativeMovementPlane movementPlane)
		{
			return PathTracer.EstimateRemainingPath(ref this.funnelState, ref this.parts[this.firstPartIndex], maxCorners, ref movementPlane);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000385DD File Offset: 0x000367DD
		[BurstCompile]
		private static float EstimateRemainingPath(ref Funnel.FunnelState funnelState, ref Funnel.PathPart part, int maxCorners, ref NativeMovementPlane movementPlane)
		{
			return PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Invoke(ref funnelState, ref part, maxCorners, ref movementPlane);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000385E8 File Offset: 0x000367E8
		public void GetNextCorners(NativeList<float3> buffer, int maxCorners, ref NativeArray<int> scratchArray, Allocator allocator, ITraversalProvider traversalProvider, Path path)
		{
			bool flag;
			int nextCornerIndices = this.GetNextCornerIndices(ref scratchArray, maxCorners, allocator, out flag, traversalProvider, path);
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
			this.funnelState.ConvertCornerIndicesToPath(scratchArray, nextCornerIndices, false, pathPart.startPoint, pathPart.endPoint, flag, buffer);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00038644 File Offset: 0x00036844
		public int GetNextCornerIndices(ref NativeArray<int> buffer, int maxCorners, Allocator allocator, out bool lastCorner, ITraversalProvider traversalProvider, Path path)
		{
			int num = 3;
			maxCorners--;
			if (PathTracer.scratchList == null)
			{
				PathTracer.scratchList = new List<GraphNode>(8);
			}
			List<GraphNode> list = PathTracer.scratchList;
			int num3;
			for (;;)
			{
				int num2 = math.max(0, math.min(maxCorners, this.funnelState.leftFunnel.Length));
				if (!buffer.IsCreated || buffer.Length < num2)
				{
					if (buffer.IsCreated)
					{
						buffer.Dispose();
					}
					buffer = new NativeArray<int>(math.ceilpow2(num2), allocator, NativeArrayOptions.UninitializedMemory);
				}
				NativeArray<int> nativeArray = buffer;
				Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
				num3 = this.funnelState.CalculateNextCornerIndices(num2, nativeArray, pathPart.startPoint, pathPart.endPoint, out lastCorner);
				if (num <= 0)
				{
					return num3;
				}
				if (this.partGraphType == PathTracer.PartGraphType.Grid)
				{
					int num4;
					int num5;
					if (!PathTracer.SimplifyGridInnerVertex(ref this.nodes, nativeArray.AsUnsafeSpan<int>().Slice(0, num3), pathPart, ref this.portalIsNotInnerCorner, list, out num4, out num5, this.nnConstraint, traversalProvider, path, lastCorner))
					{
						return num3;
					}
					if (!this.SplicePath(num4, num5 - num4 + 1, list))
					{
						break;
					}
					num--;
					ushort num6 = this.version;
					this.version = num6 + 1;
				}
				else
				{
					int num7;
					int num8;
					if (!this.FirstInnerVertex(nativeArray, num3, list, out num7, out num8, traversalProvider, path))
					{
						return num3;
					}
					if (!this.SplicePath(num7, num8 - num7 + 1, list))
					{
						goto IL_0176;
					}
					num--;
					ushort num6 = this.version;
					this.version = num6 + 1;
				}
			}
			this.firstPartContainsDestroyedNodes = true;
			return num3;
			IL_0176:
			this.firstPartContainsDestroyedNodes = true;
			return num3;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000387D0 File Offset: 0x000369D0
		public void ConvertCornerIndicesToPathProjected(NativeArray<int> cornerIndices, int numCorners, bool lastCorner, NativeList<float3> buffer, float3 up)
		{
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
			this.funnelState.ConvertCornerIndicesToPathProjected(cornerIndices.AsUnsafeReadOnlySpan<int>().Slice(0, numCorners), false, pathPart.startPoint, pathPart.endPoint, lastCorner, buffer, up);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00038826 File Offset: 0x00036A26
		[BurstCompile]
		public static float RemainingDistanceLowerBound(in UnsafeSpan<float3> nextCorners, in float3 endOfPart, in NativeMovementPlane movementPlane)
		{
			return PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Invoke(in nextCorners, in endOfPart, in movementPlane);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00038830 File Offset: 0x00036A30
		public unsafe void PopParts(int count, ITraversalProvider traversalProvider, Path path)
		{
			if (this.firstPartIndex + count >= this.parts.Length)
			{
				throw new InvalidOperationException("Cannot pop the last part of a path");
			}
			this.firstPartIndex += count;
			ushort version = this.version;
			this.version = version + 1;
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex];
			while (this.nodes.AbsoluteStartIndex < pathPart.startIndex)
			{
				this.nodes.PopStart();
			}
			this.startNode = ((this.nodes.Length > 0) ? (*this.nodes.First) : null);
			this.firstPartContainsDestroyedNodes = false;
			if (this.GetPartType(0) == Funnel.PartType.OffMeshLink)
			{
				this.partGraphType = PathTracer.PartGraphType.OffMeshLink;
				this.SetFunnelState(pathPart);
				return;
			}
			this.partGraphType = PathTracer.PartGraphTypeFromNode(this.startNode);
			int i = pathPart.startIndex;
			while (i <= pathPart.endIndex)
			{
				if (!PathTracer.Valid(this.nodes.GetAbsolute(i)))
				{
					this.RemoveAllPartsExceptFirst();
					while (this.nodes.AbsoluteEndIndex > i)
					{
						this.nodes.PopEnd();
					}
					pathPart.endIndex = i;
					this.parts[this.firstPartIndex] = pathPart;
					if (i == pathPart.startIndex)
					{
						this.firstPartContainsDestroyedNodes = true;
						this.funnelState.Clear();
						this.portalIsNotInnerCorner.Clear();
						this.startNode = null;
						return;
					}
					this.endIsUpToDate = false;
					this.nodes.PopEnd();
					pathPart.endIndex = i - 1;
					this.parts[this.firstPartIndex] = pathPart;
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.partGraphType == PathTracer.PartGraphType.Grid)
			{
				PathTracer.RemoveGridPathDiagonals(this.parts, this.firstPartIndex, ref this.nodes, this.nnConstraint, traversalProvider, path);
				pathPart = this.parts[this.firstPartIndex];
			}
			this.SetFunnelState(pathPart);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00038A10 File Offset: 0x00036C10
		private void RemoveAllPartsExceptFirst()
		{
			if (this.partCount <= 1)
			{
				return;
			}
			this.parts = new Funnel.PathPart[] { this.parts[this.firstPartIndex] };
			this.firstPartIndex = 0;
			while (this.nodes.AbsoluteEndIndex > this.parts[0].endIndex)
			{
				this.nodes.PopEnd();
			}
			ushort version = this.version;
			this.version = version + 1;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00038A91 File Offset: 0x00036C91
		public Funnel.PartType GetPartType(int partIndex = 0)
		{
			return this.parts[this.firstPartIndex + partIndex].type;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00038AAC File Offset: 0x00036CAC
		public bool PartContainsDestroyedNodes(int partIndex = 0)
		{
			if (partIndex < 0 || partIndex >= this.partCount)
			{
				throw new ArgumentOutOfRangeException("partIndex");
			}
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex + partIndex];
			for (int i = pathPart.startIndex; i <= pathPart.endIndex; i++)
			{
				if (!PathTracer.Valid(this.nodes.GetAbsolute(i)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00038B14 File Offset: 0x00036D14
		public OffMeshLinks.OffMeshLinkTracer GetLinkInfo(int partIndex = 0)
		{
			if (partIndex < 0 || partIndex >= this.partCount)
			{
				throw new ArgumentOutOfRangeException("partIndex");
			}
			if (this.GetPartType(partIndex) != Funnel.PartType.OffMeshLink)
			{
				throw new ArgumentException("Part is not an off-mesh link");
			}
			Funnel.PathPart pathPart = this.parts[this.firstPartIndex + partIndex];
			LinkNode linkNode = this.nodes.GetAbsolute(pathPart.startIndex) as LinkNode;
			LinkNode linkNode2 = this.nodes.GetAbsolute(pathPart.endIndex) as LinkNode;
			if (linkNode == null)
			{
				throw new Exception("Expected a link node");
			}
			if (linkNode2 == null)
			{
				throw new Exception("Expected a link node");
			}
			if (linkNode.Destroyed)
			{
				throw new Exception("Start node is destroyed");
			}
			if (linkNode2.Destroyed)
			{
				throw new Exception("End node is destroyed");
			}
			bool flag;
			if (linkNode.linkConcrete.startLinkNode == linkNode)
			{
				flag = false;
			}
			else
			{
				if (linkNode.linkConcrete.startLinkNode != linkNode2)
				{
					throw new Exception("Link node is not part of the link");
				}
				flag = true;
			}
			return new OffMeshLinks.OffMeshLinkTracer(linkNode.linkConcrete, flag);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00038C0C File Offset: 0x00036E0C
		private void SetFunnelState(Funnel.PathPart part)
		{
			this.funnelState.Clear();
			this.portalIsNotInnerCorner.Clear();
			if (part.type == Funnel.PartType.NodeSequence)
			{
				GridGraph gridGraph = this.nodes.GetAbsolute(part.startIndex).Graph as GridGraph;
				if (gridGraph != null)
				{
					this.funnelState.projectionAxis = gridGraph.transform.WorldUpAtGraphPosition(Vector3.zero);
				}
				List<float3> list = ListPool<float3>.Claim(part.endIndex - part.startIndex);
				List<float3> list2 = ListPool<float3>.Claim(part.endIndex - part.startIndex);
				this.CalculateFunnelPortals(part.startIndex, part.endIndex, list, list2);
				this.funnelState.Splice(0, 0, list, list2);
				for (int i = 0; i < list.Count; i++)
				{
					this.portalIsNotInnerCorner.PushEnd(0);
				}
				ListPool<float3>.Release(ref list);
				ListPool<float3>.Release(ref list2);
			}
			ushort version = this.version;
			this.version = version + 1;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00038D04 File Offset: 0x00036F04
		private void CalculateFunnelPortals(int startNodeIndex, int endNodeIndex, List<float3> outLeftPortals, List<float3> outRightPortals)
		{
			GraphNode graphNode = this.nodes.GetAbsolute(startNodeIndex);
			for (int i = startNodeIndex + 1; i <= endNodeIndex; i++)
			{
				GraphNode absolute = this.nodes.GetAbsolute(i);
				Vector3 vector;
				Vector3 vector2;
				if (!graphNode.GetPortal(absolute, out vector, out vector2))
				{
					string[] array = new string[6];
					array[0] = "Couldn't find a portal from ";
					int num = 1;
					GraphNode graphNode2 = graphNode;
					array[num] = ((graphNode2 != null) ? graphNode2.ToString() : null);
					array[2] = " ";
					int num2 = 3;
					GraphNode graphNode3 = absolute;
					array[num2] = ((graphNode3 != null) ? graphNode3.ToString() : null);
					array[4] = " ";
					array[5] = graphNode.ContainsOutgoingConnection(absolute).ToString();
					throw new InvalidOperationException(string.Concat(array));
				}
				outLeftPortals.Add(vector);
				outRightPortals.Add(vector2);
				graphNode = absolute;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00038DC8 File Offset: 0x00036FC8
		public void SetFromSingleNode(GraphNode node, Vector3 position, NativeMovementPlane movementPlane)
		{
			this.SetPath(new List<Funnel.PathPart>
			{
				new Funnel.PathPart
				{
					startIndex = 0,
					endIndex = 0,
					startPoint = position,
					endPoint = position
				}
			}, new List<GraphNode> { node }, position, position, movementPlane, null, null);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00038E20 File Offset: 0x00037020
		public void Clear()
		{
			this.funnelState.Clear();
			this.parts = null;
			this.nodes.Clear();
			this.portalIsNotInnerCorner.Clear();
			this.unclampedEndPoint = (this.unclampedStartPoint = Vector3.zero);
			this.firstPartIndex = 0;
			this.startIsUpToDate = false;
			this.endIsUpToDate = false;
			this.firstPartContainsDestroyedNodes = false;
			this.startNodeInternal = null;
			this.partGraphType = PathTracer.PartGraphType.Navmesh;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00038E94 File Offset: 0x00037094
		private unsafe static int2 ResolveNormalizedGridPoint(GridGraph grid, ref CircularBuffer<GraphNode> nodes, UnsafeSpan<int> cornerIndices, Funnel.PathPart part, int index, out int nodeIndex)
		{
			if (index < 0 || index >= cornerIndices.Length)
			{
				Vector3 vector = ((index < 0) ? part.startPoint : part.endPoint);
				nodeIndex = ((index < 0) ? part.startIndex : part.endIndex);
				Vector3 vector2 = grid.transform.InverseTransform(vector);
				Int2 coordinatesInGrid = (nodes.GetAbsolute(nodeIndex) as GridNodeBase).CoordinatesInGrid;
				return new int2(math.clamp((int)(1024f * (vector2.x - (float)coordinatesInGrid.x)), 0, 1024), math.clamp((int)(1024f * (vector2.z - (float)coordinatesInGrid.y)), 0, 1024));
			}
			bool flag = (*cornerIndices[index] & 1073741824) != 0;
			nodeIndex = part.startIndex + (*cornerIndices[index] & 1073741823);
			GridNodeBase gridNodeBase = nodes.GetAbsolute(nodeIndex) as GridNodeBase;
			GridNodeBase gridNodeBase2 = nodes.GetAbsolute(nodeIndex + 1) as GridNodeBase;
			Int2 coordinatesInGrid2 = gridNodeBase.CoordinatesInGrid;
			Int2 coordinatesInGrid3 = gridNodeBase2.CoordinatesInGrid;
			int num = coordinatesInGrid3.x - coordinatesInGrid2.x;
			int num2 = coordinatesInGrid3.y - coordinatesInGrid2.y;
			int num3 = GridNodeBase.OffsetToConnectionDirection(num, num2);
			if (num3 > 4)
			{
				throw new Exception("Diagonal connections are not supported");
			}
			int num4 = GridGraph.neighbourXOffsets[num3] + GridGraph.neighbourXOffsets[(num3 + (flag ? (-1) : 1) + 4) % 4];
			int num5 = GridGraph.neighbourZOffsets[num3] + GridGraph.neighbourZOffsets[(num3 + (flag ? (-1) : 1) + 4) % 4];
			return new int2(512 + 512 * num4, 512 + 512 * num5);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0003903C File Offset: 0x0003723C
		private unsafe static bool SimplifyGridInnerVertex(ref CircularBuffer<GraphNode> nodes, UnsafeSpan<int> cornerIndices, Funnel.PathPart part, ref CircularBuffer<byte> portalIsNotInnerCorner, List<GraphNode> alternativePath, out int alternativeStartIndex, out int alternativeEndIndex, NNConstraint nnConstraint, ITraversalProvider traversalProvider, Path path, bool lastCorner)
		{
			bool flag = (lastCorner ? cornerIndices.Length : (cornerIndices.Length - 1)) != 0;
			alternativeStartIndex = -1;
			alternativeEndIndex = -1;
			if (!flag)
			{
				return false;
			}
			int num = 0;
			int num2 = *cornerIndices[num] & 1073741823;
			int num3 = (int)(portalIsNotInnerCorner[num2] % 32);
			portalIsNotInnerCorner[num2] = (byte)(num3 + 1);
			if ((num3 & 3) != 0)
			{
				return false;
			}
			num3 /= 4;
			GridGraph gridGraph = GridNode.GetGridGraph(nodes.GetAbsolute(part.startIndex).GraphIndex);
			int num4;
			int2 @int = PathTracer.ResolveNormalizedGridPoint(gridGraph, ref nodes, cornerIndices, part, num - 1, out num4);
			int num5;
			int2 int2 = PathTracer.ResolveNormalizedGridPoint(gridGraph, ref nodes, cornerIndices, part, num + 1, out num5);
			int num6;
			int2 int3 = PathTracer.ResolveNormalizedGridPoint(gridGraph, ref nodes, cornerIndices, part, num, out num6);
			GridNodeBase gridNodeBase = nodes.GetAbsolute(num4) as GridNodeBase;
			GridNodeBase gridNodeBase2 = nodes.GetAbsolute(num6) as GridNodeBase;
			GridNodeBase gridNodeBase3 = nodes.GetAbsolute(num5) as GridNodeBase;
			if (num3 > 0)
			{
				int num7 = PathTracer.SplittingCoefficients[num3 * 2];
				int num8 = PathTracer.SplittingCoefficients[num3 * 2 + 1];
				num5 += (num6 - num5) * num7 / num8;
				if (num5 == num6)
				{
					return false;
				}
				Int2 int4 = gridNodeBase3.CoordinatesInGrid;
				Int2 coordinatesInGrid = gridNodeBase2.CoordinatesInGrid;
				int2 int5 = new int2(coordinatesInGrid.x * 1024, coordinatesInGrid.y * 1024) + int3;
				int2 int6 = new int2(int4.x * 1024, int4.y * 1024) + int2;
				gridNodeBase3 = nodes.GetAbsolute(num5) as GridNodeBase;
				int4 = gridNodeBase3.CoordinatesInGrid;
				float num9 = VectorMath.ClosestPointOnLineFactor(new Int2(int5.x, int5.y), new Int2(int6.x, int6.y), new Int2(int4.x * 1024 + 512, int4.y * 1024 + 512));
				int2 int7 = new int2((int)math.lerp((float)int5.x, (float)int6.x, num9), (int)math.lerp((float)int5.y, (float)int6.y, num9)) - new int2(int4.x * 1024, int4.y * 1024);
				int2 = new int2(math.clamp(int7.x, 0, 1024), math.clamp(int7.y, 0, 1024));
			}
			alternativePath.Clear();
			GridHitInfo gridHitInfo;
			if (!gridGraph.Linecast(gridNodeBase, @int, gridNodeBase3, int2, out gridHitInfo, alternativePath, null, false))
			{
				for (int i = 1; i < alternativePath.Count; i++)
				{
					if ((traversalProvider != null) ? (!traversalProvider.CanTraverse(path, alternativePath[i - 1], alternativePath[i])) : (!nnConstraint.Suitable(alternativePath[i])))
					{
						return false;
					}
				}
				uint num10 = 0U;
				for (int j = 0; j < alternativePath.Count; j++)
				{
					num10 += ((traversalProvider != null) ? traversalProvider.GetTraversalCost(path, alternativePath[j]) : DefaultITraversalProvider.GetTraversalCost(path, alternativePath[j]));
				}
				if (num10 > 0U)
				{
					uint num11 = 0U;
					for (int k = num4; k <= num5; k++)
					{
						num11 += ((traversalProvider != null) ? traversalProvider.GetTraversalCost(path, nodes.GetAbsolute(k)) : DefaultITraversalProvider.GetTraversalCost(path, nodes.GetAbsolute(k)));
					}
					if (num10 > num11)
					{
						return false;
					}
				}
				alternativeStartIndex = num4;
				alternativeEndIndex = num5;
				return true;
			}
			return false;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000393B4 File Offset: 0x000375B4
		private static void RemoveGridPathDiagonals(Funnel.PathPart[] parts, int partIndex, ref CircularBuffer<GraphNode> path, NNConstraint nnConstraint, ITraversalProvider traversalProvider, Path pathObject)
		{
			int num = 0;
			Funnel.PathPart pathPart = ((parts != null) ? parts[partIndex] : new Funnel.PathPart
			{
				startIndex = path.AbsoluteStartIndex,
				endIndex = path.AbsoluteEndIndex
			});
			for (int i = pathPart.endIndex - 1; i >= pathPart.startIndex; i--)
			{
				GridNodeBase gridNodeBase = path.GetAbsolute(i) as GridNodeBase;
				GridNodeBase gridNodeBase2 = path.GetAbsolute(i + 1) as GridNodeBase;
				int num2 = gridNodeBase2.XCoordinateInGrid - gridNodeBase.XCoordinateInGrid;
				int num3 = gridNodeBase2.ZCoordinateInGrid - gridNodeBase.ZCoordinateInGrid;
				int num4 = GridNodeBase.OffsetToConnectionDirection(num2, num3);
				if (num4 >= 4)
				{
					int num5 = num4 - 4;
					int num6 = (num4 - 4 + 1) % 4;
					GridNodeBase gridNodeBase3 = gridNodeBase.GetNeighbourAlongDirection(num5);
					if (gridNodeBase3 != null && ((traversalProvider != null) ? (!traversalProvider.CanTraverse(pathObject, gridNodeBase, gridNodeBase3)) : (!nnConstraint.Suitable(gridNodeBase3))))
					{
						gridNodeBase3 = null;
					}
					if (gridNodeBase3 != null && gridNodeBase3.GetNeighbourAlongDirection(num6) == gridNodeBase2 && (traversalProvider == null || traversalProvider.CanTraverse(pathObject, gridNodeBase3, gridNodeBase2)))
					{
						path.InsertAbsolute(i + 1, gridNodeBase3);
						num++;
					}
					else
					{
						GridNodeBase gridNodeBase4 = gridNodeBase.GetNeighbourAlongDirection(num6);
						if (gridNodeBase4 != null && ((traversalProvider != null) ? (!traversalProvider.CanTraverse(pathObject, gridNodeBase, gridNodeBase4)) : (!nnConstraint.Suitable(gridNodeBase4))))
						{
							gridNodeBase4 = null;
						}
						if (gridNodeBase4 == null || gridNodeBase4.GetNeighbourAlongDirection(num5) != gridNodeBase2 || (traversalProvider != null && !traversalProvider.CanTraverse(pathObject, gridNodeBase4, gridNodeBase2)))
						{
							throw new Exception("Axis-aligned connection not found");
						}
						path.InsertAbsolute(i + 1, gridNodeBase4);
						num++;
					}
				}
			}
			if (parts != null)
			{
				parts[partIndex].endIndex = parts[partIndex].endIndex + num;
				for (int j = partIndex + 1; j < parts.Length; j++)
				{
					int num7 = j;
					parts[num7].startIndex = parts[num7].startIndex + num;
					int num8 = j;
					parts[num8].endIndex = parts[num8].endIndex + num;
				}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003959B File Offset: 0x0003779B
		private static PathTracer.PartGraphType PartGraphTypeFromNode(GraphNode node)
		{
			if (node == null)
			{
				return PathTracer.PartGraphType.Navmesh;
			}
			if (node is GridNodeBase)
			{
				return PathTracer.PartGraphType.Grid;
			}
			if (node is TriangleMeshNode)
			{
				return PathTracer.PartGraphType.Navmesh;
			}
			throw new Exception("The PathTracer (and by extension FollowerEntity component) cannot be used on graphs of type " + node.Graph.GetType().Name);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000395D8 File Offset: 0x000377D8
		public void SetPath(ABPath path, NativeMovementPlane movementPlane)
		{
			List<Funnel.PathPart> list = Funnel.SplitIntoParts(path);
			this.nnConstraint.constrainTags = path.nnConstraint.constrainTags;
			this.nnConstraint.tags = path.nnConstraint.tags;
			this.nnConstraint.graphMask = path.nnConstraint.graphMask;
			this.nnConstraint.constrainWalkability = path.nnConstraint.constrainWalkability;
			this.nnConstraint.walkable = path.nnConstraint.walkable;
			this.SetPath(list, path.path, path.originalStartPoint, path.originalEndPoint, movementPlane, path.traversalProvider, path);
			ListPool<Funnel.PathPart>.Release(ref list);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00039684 File Offset: 0x00037884
		public void SetPath(List<Funnel.PathPart> parts, List<GraphNode> nodes, Vector3 unclampedStartPoint, Vector3 unclampedEndPoint, NativeMovementPlane movementPlane, ITraversalProvider traversalProvider, Path path)
		{
			this.startNode = ((nodes.Count > 0) ? nodes[0] : null);
			this.partGraphType = PathTracer.PartGraphTypeFromNode(this.startNode);
			this.unclampedEndPoint = unclampedEndPoint;
			this.unclampedStartPoint = unclampedStartPoint;
			this.firstPartContainsDestroyedNodes = false;
			this.startIsUpToDate = true;
			this.endIsUpToDate = true;
			this.parts = parts.ToArray();
			this.nodes.Clear();
			this.nodes.AddRange(nodes);
			this.firstPartIndex = 0;
			if (this.partGraphType == PathTracer.PartGraphType.Grid)
			{
				PathTracer.RemoveGridPathDiagonals(this.parts, 0, ref this.nodes, this.nnConstraint, traversalProvider, path);
			}
			this.SetFunnelState(this.parts[this.firstPartIndex]);
			ushort version = this.version;
			this.version = version + 1;
			this.Repair(unclampedStartPoint, true, PathTracer.RepairQuality.Low, movementPlane, traversalProvider, path, false);
			this.Repair(unclampedEndPoint, false, PathTracer.RepairQuality.Low, movementPlane, traversalProvider, path, false);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00039778 File Offset: 0x00037978
		public PathTracer Clone()
		{
			return new PathTracer
			{
				parts = ((this.parts != null) ? (this.parts.Clone() as Funnel.PathPart[]) : null),
				nodes = this.nodes.Clone(),
				portalIsNotInnerCorner = this.portalIsNotInnerCorner.Clone(),
				funnelState = this.funnelState.Clone(),
				unclampedEndPoint = this.unclampedEndPoint,
				unclampedStartPoint = this.unclampedStartPoint,
				startNodeInternal = this.startNodeInternal,
				firstPartIndex = this.firstPartIndex,
				startIsUpToDate = this.startIsUpToDate,
				endIsUpToDate = this.endIsUpToDate,
				firstPartContainsDestroyedNodes = this.firstPartContainsDestroyedNodes,
				version = this.version,
				nnConstraint = NNConstraint.Walkable,
				partGraphType = this.partGraphType
			};
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000398C8 File Offset: 0x00037AC8
		[BurstCompile]
		[MethodImpl(256)]
		public static bool ContainsAndProject$BurstManaged(ref Int3 a, ref Int3 b, ref Int3 c, ref Vector3 p, float height, ref NativeMovementPlane movementPlane, out Vector3 projected)
		{
			int3 @int = (int3)a;
			int3 int2 = (int3)b;
			int3 int3 = (int3)c;
			int3 int4 = (int3)((Int3)p);
			if (!Polygon.ContainsPoint(ref @int, ref int2, ref int3, ref int4, ref movementPlane))
			{
				projected = Vector3.zero;
				return false;
			}
			float3 @float = (Vector3)a;
			float3 float2 = (Vector3)b;
			float3 float3 = (Vector3)c;
			float3 float4 = p;
			float num = math.lengthsq(Polygon.ClosestPointOnTriangle(@float, float2, float3, float4) - float4);
			float num2 = height * 0.5f;
			if (num >= num2 * num2)
			{
				projected = Vector3.zero;
				return false;
			}
			float3 float5 = movementPlane.ToWorld(float2.zero, 1f);
			projected = PathTracer.ProjectOnSurface(@float, float2, float3, float4, float5);
			return true;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000399D4 File Offset: 0x00037BD4
		[BurstCompile]
		[MethodImpl(256)]
		public static float EstimateRemainingPath$BurstManaged(ref Funnel.FunnelState funnelState, ref Funnel.PathPart part, int maxCorners, ref NativeMovementPlane movementPlane)
		{
			NativeList<float3> nativeList = new NativeList<float3>(maxCorners, Allocator.Temp);
			NativeArray<int> nativeArray = new NativeArray<int>(maxCorners, Allocator.Temp, NativeArrayOptions.ClearMemory);
			maxCorners--;
			maxCorners = math.max(0, math.min(maxCorners, funnelState.leftFunnel.Length));
			bool flag;
			int num = funnelState.CalculateNextCornerIndices(maxCorners, nativeArray, part.startPoint, part.endPoint, out flag);
			funnelState.ConvertCornerIndicesToPath(nativeArray, num, false, part.startPoint, part.endPoint, flag, nativeList);
			UnsafeSpan<float3> unsafeSpan = nativeList.AsUnsafeSpan<float3>();
			float3 @float = part.endPoint;
			return PathTracer.RemainingDistanceLowerBound(in unsafeSpan, in @float, in movementPlane);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00039A78 File Offset: 0x00037C78
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static float RemainingDistanceLowerBound$BurstManaged(in UnsafeSpan<float3> nextCorners, in float3 endOfPart, in NativeMovementPlane movementPlane)
		{
			if (nextCorners.Length == 0)
			{
				return 0f;
			}
			float3 @float = *nextCorners[0];
			float num = 0f;
			for (int i = 1; i < nextCorners.Length; i++)
			{
				float3 float2 = *nextCorners[i];
				num += math.length(movementPlane.ToPlane(float2 - @float));
				@float = float2;
			}
			return num + math.length(movementPlane.ToPlane(@float - endOfPart));
		}

		// Token: 0x040006B0 RID: 1712
		private Funnel.PathPart[] parts;

		// Token: 0x040006B1 RID: 1713
		private CircularBuffer<GraphNode> nodes;

		// Token: 0x040006B2 RID: 1714
		private CircularBuffer<byte> portalIsNotInnerCorner;

		// Token: 0x040006B3 RID: 1715
		private Funnel.FunnelState funnelState;

		// Token: 0x040006B4 RID: 1716
		private Vector3 unclampedEndPoint;

		// Token: 0x040006B5 RID: 1717
		private Vector3 unclampedStartPoint;

		// Token: 0x040006B6 RID: 1718
		private GraphNode startNodeInternal;

		// Token: 0x040006B7 RID: 1719
		public NNConstraint nnConstraint;

		// Token: 0x040006B8 RID: 1720
		private int firstPartIndex;

		// Token: 0x040006B9 RID: 1721
		private bool startIsUpToDate;

		// Token: 0x040006BA RID: 1722
		private bool endIsUpToDate;

		// Token: 0x040006BB RID: 1723
		private bool firstPartContainsDestroyedNodes;

		// Token: 0x040006BC RID: 1724
		public PathTracer.PartGraphType partGraphType;

		// Token: 0x040006BE RID: 1726
		private static readonly ProfilerMarker MarkerContains = new ProfilerMarker("ContainsNode");

		// Token: 0x040006BF RID: 1727
		private static readonly ProfilerMarker MarkerClosest = new ProfilerMarker("ClosestPointOnNode");

		// Token: 0x040006C0 RID: 1728
		private static readonly ProfilerMarker MarkerGetNearest = new ProfilerMarker("GetNearest");

		// Token: 0x040006C1 RID: 1729
		private const int NODES_TO_CHECK_FOR_DESTRUCTION = 5;

		// Token: 0x040006C2 RID: 1730
		[ThreadStatic]
		private static List<GraphNode> scratchList;

		// Token: 0x040006C3 RID: 1731
		private static int[] SplittingCoefficients = new int[]
		{
			0, 1, 1, 2, 1, 4, 3, 4, 1, 8,
			3, 8, 5, 8, 7, 8
		};

		// Token: 0x040006C4 RID: 1732
		private static readonly ProfilerMarker MarkerSimplify = new ProfilerMarker("Simplify");

		// Token: 0x0200014E RID: 334
		public enum PartGraphType : byte
		{
			// Token: 0x040006C6 RID: 1734
			Navmesh,
			// Token: 0x040006C7 RID: 1735
			Grid,
			// Token: 0x040006C8 RID: 1736
			OffMeshLink
		}

		// Token: 0x0200014F RID: 335
		public enum RepairQuality
		{
			// Token: 0x040006CA RID: 1738
			Low,
			// Token: 0x040006CB RID: 1739
			High
		}

		// Token: 0x02000150 RID: 336
		private struct QueueItem
		{
			// Token: 0x040006CC RID: 1740
			public GraphNode node;

			// Token: 0x040006CD RID: 1741
			public int parent;

			// Token: 0x040006CE RID: 1742
			public float distance;
		}

		// Token: 0x02000152 RID: 338
		// (Invoke) Token: 0x06000A11 RID: 2577
		public delegate bool ContainsAndProject_00000949$PostfixBurstDelegate(ref Int3 a, ref Int3 b, ref Int3 c, ref Vector3 p, float height, ref NativeMovementPlane movementPlane, out Vector3 projected);

		// Token: 0x02000153 RID: 339
		internal static class ContainsAndProject_00000949$BurstDirectCall
		{
			// Token: 0x06000A14 RID: 2580 RVA: 0x00039B0C File Offset: 0x00037D0C
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (PathTracer.ContainsAndProject_00000949$BurstDirectCall.Pointer == 0)
				{
					PathTracer.ContainsAndProject_00000949$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(PathTracer.ContainsAndProject_00000949$BurstDirectCall.DeferredCompilation, methodof(PathTracer.ContainsAndProject$BurstManaged(ref Int3, ref Int3, ref Int3, ref Vector3, float, ref NativeMovementPlane, ref Vector3)).MethodHandle, typeof(PathTracer.ContainsAndProject_00000949$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = PathTracer.ContainsAndProject_00000949$BurstDirectCall.Pointer;
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00039B38 File Offset: 0x00037D38
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				PathTracer.ContainsAndProject_00000949$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x00039B50 File Offset: 0x00037D50
			public static void Constructor()
			{
				PathTracer.ContainsAndProject_00000949$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(PathTracer.ContainsAndProject(ref Int3, ref Int3, ref Int3, ref Vector3, float, ref NativeMovementPlane, ref Vector3)).MethodHandle);
			}

			// Token: 0x06000A17 RID: 2583 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x00039B61 File Offset: 0x00037D61
			// Note: this type is marked as 'beforefieldinit'.
			static ContainsAndProject_00000949$BurstDirectCall()
			{
				PathTracer.ContainsAndProject_00000949$BurstDirectCall.Constructor();
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x00039B68 File Offset: 0x00037D68
			public static bool Invoke(ref Int3 a, ref Int3 b, ref Int3 c, ref Vector3 p, float height, ref NativeMovementPlane movementPlane, out Vector3 projected)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = PathTracer.ContainsAndProject_00000949$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Boolean(Pathfinding.Int3&,Pathfinding.Int3&,Pathfinding.Int3&,UnityEngine.Vector3&,System.Single,Pathfinding.Util.NativeMovementPlane&,UnityEngine.Vector3&), ref a, ref b, ref c, ref p, height, ref movementPlane, ref projected, functionPointer);
					}
				}
				return PathTracer.ContainsAndProject$BurstManaged(ref a, ref b, ref c, ref p, height, ref movementPlane, out projected);
			}

			// Token: 0x040006D1 RID: 1745
			private static IntPtr Pointer;

			// Token: 0x040006D2 RID: 1746
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x02000154 RID: 340
		// (Invoke) Token: 0x06000A1B RID: 2587
		public delegate float EstimateRemainingPath_0000095A$PostfixBurstDelegate(ref Funnel.FunnelState funnelState, ref Funnel.PathPart part, int maxCorners, ref NativeMovementPlane movementPlane);

		// Token: 0x02000155 RID: 341
		internal static class EstimateRemainingPath_0000095A$BurstDirectCall
		{
			// Token: 0x06000A1E RID: 2590 RVA: 0x00039BAB File Offset: 0x00037DAB
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Pointer == 0)
				{
					PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.DeferredCompilation, methodof(PathTracer.EstimateRemainingPath$BurstManaged(ref Funnel.FunnelState, ref Funnel.PathPart, int, ref NativeMovementPlane)).MethodHandle, typeof(PathTracer.EstimateRemainingPath_0000095A$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Pointer;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00039BD8 File Offset: 0x00037DD8
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x00039BF0 File Offset: 0x00037DF0
			public static void Constructor()
			{
				PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(PathTracer.EstimateRemainingPath(ref Funnel.FunnelState, ref Funnel.PathPart, int, ref NativeMovementPlane)).MethodHandle);
			}

			// Token: 0x06000A21 RID: 2593 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000A22 RID: 2594 RVA: 0x00039C01 File Offset: 0x00037E01
			// Note: this type is marked as 'beforefieldinit'.
			static EstimateRemainingPath_0000095A$BurstDirectCall()
			{
				PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.Constructor();
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x00039C08 File Offset: 0x00037E08
			public static float Invoke(ref Funnel.FunnelState funnelState, ref Funnel.PathPart part, int maxCorners, ref NativeMovementPlane movementPlane)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = PathTracer.EstimateRemainingPath_0000095A$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Single(Pathfinding.Funnel/FunnelState&,Pathfinding.Funnel/PathPart&,System.Int32,Pathfinding.Util.NativeMovementPlane&), ref funnelState, ref part, maxCorners, ref movementPlane, functionPointer);
					}
				}
				return PathTracer.EstimateRemainingPath$BurstManaged(ref funnelState, ref part, maxCorners, ref movementPlane);
			}

			// Token: 0x040006D3 RID: 1747
			private static IntPtr Pointer;

			// Token: 0x040006D4 RID: 1748
			private static IntPtr DeferredCompilation;
		}

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06000A25 RID: 2597
		public delegate float RemainingDistanceLowerBound_0000095E$PostfixBurstDelegate(in UnsafeSpan<float3> nextCorners, in float3 endOfPart, in NativeMovementPlane movementPlane);

		// Token: 0x02000157 RID: 343
		internal static class RemainingDistanceLowerBound_0000095E$BurstDirectCall
		{
			// Token: 0x06000A28 RID: 2600 RVA: 0x00039C3F File Offset: 0x00037E3F
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Pointer == 0)
				{
					PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.DeferredCompilation, methodof(PathTracer.RemainingDistanceLowerBound$BurstManaged(ref UnsafeSpan<float3>, ref float3, ref NativeMovementPlane)).MethodHandle, typeof(PathTracer.RemainingDistanceLowerBound_0000095E$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Pointer;
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00039C6C File Offset: 0x00037E6C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x00039C84 File Offset: 0x00037E84
			public static void Constructor()
			{
				PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(PathTracer.RemainingDistanceLowerBound(ref UnsafeSpan<float3>, ref float3, ref NativeMovementPlane)).MethodHandle);
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x00039C95 File Offset: 0x00037E95
			// Note: this type is marked as 'beforefieldinit'.
			static RemainingDistanceLowerBound_0000095E$BurstDirectCall()
			{
				PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.Constructor();
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x00039C9C File Offset: 0x00037E9C
			public static float Invoke(in UnsafeSpan<float3> nextCorners, in float3 endOfPart, in NativeMovementPlane movementPlane)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = PathTracer.RemainingDistanceLowerBound_0000095E$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Single(Pathfinding.Util.UnsafeSpan`1<Unity.Mathematics.float3>&,Unity.Mathematics.float3&,Pathfinding.Util.NativeMovementPlane&), ref nextCorners, ref endOfPart, ref movementPlane, functionPointer);
					}
				}
				return PathTracer.RemainingDistanceLowerBound$BurstManaged(in nextCorners, in endOfPart, in movementPlane);
			}

			// Token: 0x040006D5 RID: 1749
			private static IntPtr Pointer;

			// Token: 0x040006D6 RID: 1750
			private static IntPtr DeferredCompilation;
		}
	}
}
