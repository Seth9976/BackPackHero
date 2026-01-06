using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Util;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007E RID: 126
	public class OffMeshLinks
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00015868 File Offset: 0x00013A68
		public OffMeshLinks(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00015898 File Offset: 0x00013A98
		public List<NavGraph> ConnectedGraphs(OffMeshLinks.OffMeshLinkSource link)
		{
			List<NavGraph> list = ListPool<NavGraph>.Claim();
			if (link.status != OffMeshLinks.OffMeshLinkStatus.Active)
			{
				return list;
			}
			OffMeshLinks.OffMeshLinkConcrete concrete = this.tree[link.treeKey].concrete;
			for (int i = 0; i < concrete.startNodes.Length; i++)
			{
				NavGraph graph = concrete.startNodes[i].Graph;
				if (!list.Contains(graph))
				{
					list.Add(graph);
				}
			}
			for (int j = 0; j < concrete.endNodes.Length; j++)
			{
				NavGraph graph2 = concrete.endNodes[j].Graph;
				if (!list.Contains(graph2))
				{
					list.Add(graph2);
				}
			}
			return list;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00015938 File Offset: 0x00013B38
		public void Add(OffMeshLinks.OffMeshLinkSource link)
		{
			if (link == null)
			{
				throw new ArgumentNullException("link");
			}
			if (link.status != OffMeshLinks.OffMeshLinkStatus.Inactive)
			{
				throw new ArgumentException("Link is already added");
			}
			this.pendingAdd.Add(link);
			link.status = OffMeshLinks.OffMeshLinkStatus.Pending;
			this.ScheduleUpdate();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00015978 File Offset: 0x00013B78
		internal void OnDisable()
		{
			List<OffMeshLinks.OffMeshLinkCombined> list = new List<OffMeshLinks.OffMeshLinkCombined>();
			this.tree.Query(new Bounds(Vector3.zero, Vector3.positiveInfinity), list);
			for (int i = 0; i < list.Count; i++)
			{
				list[i].source.status = OffMeshLinks.OffMeshLinkStatus.Inactive;
				list[i].source.treeKey = default(AABBTree<OffMeshLinks.OffMeshLinkCombined>.Key);
			}
			this.tree.Clear();
			for (int j = 0; j < this.pendingAdd.Count; j++)
			{
				this.pendingAdd[j].status = OffMeshLinks.OffMeshLinkStatus.Inactive;
				this.pendingAdd[j].treeKey = default(AABBTree<OffMeshLinks.OffMeshLinkCombined>.Key);
			}
			this.pendingAdd.Clear();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00015A38 File Offset: 0x00013C38
		public void Remove(OffMeshLinks.OffMeshLinkSource link)
		{
			if (link == null)
			{
				throw new ArgumentNullException("link");
			}
			if (link.status == OffMeshLinks.OffMeshLinkStatus.Inactive || (link.status & OffMeshLinks.OffMeshLinkStatus.PendingRemoval) != (OffMeshLinks.OffMeshLinkStatus)0)
			{
				return;
			}
			if (link.status == OffMeshLinks.OffMeshLinkStatus.Pending)
			{
				link.status = OffMeshLinks.OffMeshLinkStatus.Inactive;
				this.pendingAdd.Remove(link);
			}
			else
			{
				link.status |= OffMeshLinks.OffMeshLinkStatus.Pending | OffMeshLinks.OffMeshLinkStatus.PendingRemoval;
				this.tree.Tag(link.treeKey);
			}
			this.ScheduleUpdate();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00015AB0 File Offset: 0x00013CB0
		private bool ClampSegment(OffMeshLinks.Anchor anchor, GraphMask graphMask, float maxSnappingDistance, out OffMeshLinks.Anchor result, List<GraphNode> nodes)
		{
			NNConstraint nnconstraint = this.cachedNNConstraint;
			nnconstraint.distanceMetric = DistanceMetric.Euclidean;
			nnconstraint.graphMask = graphMask;
			NNInfo nninfo = this.astar.GetNearest(0.5f * (anchor.point1 + anchor.point2), nnconstraint);
			if (nninfo.distanceCostSqr > maxSnappingDistance * maxSnappingDistance)
			{
				nninfo = default(NNInfo);
			}
			if (nninfo.node == null)
			{
				result = default(OffMeshLinks.Anchor);
				return false;
			}
			if (anchor.width > 0f)
			{
				IRaycastableGraph raycastableGraph = nninfo.node.Graph as IRaycastableGraph;
				if (raycastableGraph != null)
				{
					Vector3 vector = 0.5f * (anchor.point2 - anchor.point1);
					GraphHitInfo graphHitInfo;
					raycastableGraph.Linecast(nninfo.position, nninfo.position - vector, nninfo.node, out graphHitInfo, nodes, null);
					GraphHitInfo graphHitInfo2;
					raycastableGraph.Linecast(nninfo.position, nninfo.position + vector, nninfo.node, out graphHitInfo2, nodes, null);
					result = new OffMeshLinks.Anchor
					{
						center = (graphHitInfo.point + graphHitInfo2.point) * 0.5f,
						rotation = anchor.rotation,
						width = (graphHitInfo.point - graphHitInfo2.point).magnitude
					};
					nodes.Sort((GraphNode a, GraphNode b) => a.NodeIndex.CompareTo(b.NodeIndex));
					for (int i = nodes.Count - 1; i >= 0; i--)
					{
						GraphNode graphNode = nodes[i];
						for (int j = i - 1; j >= 0; j--)
						{
							if (nodes[j] == graphNode)
							{
								nodes.RemoveAtSwapBack(i);
								break;
							}
						}
					}
					return true;
				}
			}
			result = new OffMeshLinks.Anchor
			{
				center = nninfo.position,
				rotation = anchor.rotation,
				width = 0f
			};
			nodes.Add(nninfo.node);
			return true;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015CD2 File Offset: 0x00013ED2
		public void DirtyBounds(Bounds bounds)
		{
			this.tree.Tag(bounds);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00015CE0 File Offset: 0x00013EE0
		public void Dirty(OffMeshLinks.OffMeshLinkSource link)
		{
			this.DirtyNoSchedule(link);
			this.ScheduleUpdate();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00015CEF File Offset: 0x00013EEF
		internal void DirtyNoSchedule(OffMeshLinks.OffMeshLinkSource link)
		{
			this.tree.Tag(link.treeKey);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00015D04 File Offset: 0x00013F04
		private void ScheduleUpdate()
		{
			if (!this.updateScheduled && !this.astar.isScanning && !this.astar.IsAnyWorkItemInProgress)
			{
				this.updateScheduled = true;
				this.astar.AddWorkItem(delegate
				{
				});
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015D64 File Offset: 0x00013F64
		public OffMeshLinks.OffMeshLinkTracer GetNearest(Vector3 point, float maxDistance)
		{
			if (maxDistance < 0f)
			{
				return default(OffMeshLinks.OffMeshLinkTracer);
			}
			if (!float.IsFinite(maxDistance))
			{
				throw new ArgumentOutOfRangeException("maxDistance");
			}
			List<OffMeshLinks.OffMeshLinkCombined> list = ListPool<OffMeshLinks.OffMeshLinkCombined>.Claim();
			this.tree.Query(new Bounds(point, new Vector3(2f * maxDistance, 2f * maxDistance, 2f * maxDistance)), list);
			OffMeshLinks.OffMeshLinkConcrete offMeshLinkConcrete = null;
			bool flag = false;
			float num = maxDistance * maxDistance;
			for (int i = 0; i < list.Count; i++)
			{
				OffMeshLinks.OffMeshLinkConcrete concrete = list[i].concrete;
				float num2 = VectorMath.SqrDistancePointSegment(concrete.start.point1, concrete.start.point2, point);
				if (num2 < num)
				{
					num = num2;
					offMeshLinkConcrete = concrete;
					flag = false;
				}
				num2 = VectorMath.SqrDistancePointSegment(concrete.end.point1, concrete.end.point2, point);
				if (num2 < num)
				{
					num = num2;
					offMeshLinkConcrete = concrete;
					flag = true;
				}
			}
			ListPool<OffMeshLinks.OffMeshLinkCombined>.Release(ref list);
			if (offMeshLinkConcrete == null)
			{
				return default(OffMeshLinks.OffMeshLinkTracer);
			}
			return new OffMeshLinks.OffMeshLinkTracer(offMeshLinkConcrete, flag);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00015E6C File Offset: 0x0001406C
		internal void Refresh()
		{
			this.updateScheduled = false;
			List<OffMeshLinks.OffMeshLinkCombined> list = ListPool<OffMeshLinks.OffMeshLinkCombined>.Claim();
			this.tree.QueryTagged(list, true);
			for (int i = 0; i < this.pendingAdd.Count; i++)
			{
				OffMeshLinks.OffMeshLinkSource offMeshLinkSource = this.pendingAdd[i];
				OffMeshLinks.OffMeshLinkCombined offMeshLinkCombined = new OffMeshLinks.OffMeshLinkCombined
				{
					source = offMeshLinkSource,
					concrete = null
				};
				offMeshLinkSource.treeKey = this.tree.Add(offMeshLinkSource.bounds, offMeshLinkCombined);
				list.Add(offMeshLinkCombined);
			}
			this.pendingAdd.Clear();
			List<GraphNode> list2 = ListPool<GraphNode>.Claim();
			List<GraphNode> list3 = ListPool<GraphNode>.Claim();
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (list[j].source == list[k].source)
					{
						throw new Exception("Duplicate link");
					}
				}
				OffMeshLinks.OffMeshLinkSource source = list[j].source;
				OffMeshLinks.OffMeshLinkCombined offMeshLinkCombined2 = this.tree[source.treeKey];
				OffMeshLinks.OffMeshLinkConcrete concrete = offMeshLinkCombined2.concrete;
				if ((source.status & OffMeshLinks.OffMeshLinkStatus.PendingRemoval) != (OffMeshLinks.OffMeshLinkStatus)0)
				{
					if (concrete != null)
					{
						concrete.Disconnect();
						offMeshLinkCombined2.concrete = null;
					}
					this.tree.Remove(source.treeKey);
					source.treeKey = default(AABBTree<OffMeshLinks.OffMeshLinkCombined>.Key);
					source.status = OffMeshLinks.OffMeshLinkStatus.Inactive;
				}
				else
				{
					list2.Clear();
					OffMeshLinks.Anchor anchor;
					if (!this.ClampSegment(source.start, source.graphMask, source.maxSnappingDistance, out anchor, list2))
					{
						if (concrete != null)
						{
							concrete.Disconnect();
							offMeshLinkCombined2.concrete = null;
						}
						source.status = OffMeshLinks.OffMeshLinkStatus.FailedToConnectStart;
					}
					else
					{
						list3.Clear();
						OffMeshLinks.Anchor anchor2;
						if (!this.ClampSegment(source.end, source.graphMask, source.maxSnappingDistance, out anchor2, list3))
						{
							if (concrete != null)
							{
								concrete.Disconnect();
								offMeshLinkCombined2.concrete = null;
							}
							source.status = OffMeshLinks.OffMeshLinkStatus.FailedToConnectEnd;
						}
						else
						{
							OffMeshLinks.OffMeshLinkConcrete offMeshLinkConcrete = new OffMeshLinks.OffMeshLinkConcrete
							{
								start = anchor,
								end = anchor2,
								startNodes = list2.ToArrayFromPool<GraphNode>(),
								endNodes = list3.ToArrayFromPool<GraphNode>(),
								source = source,
								directionality = source.directionality,
								tag = source.tag,
								costFactor = source.costFactor
							};
							if (concrete != null && !concrete.staleConnections && concrete.Equivalent(offMeshLinkConcrete))
							{
								source.status &= ~OffMeshLinks.OffMeshLinkStatus.Pending;
							}
							else
							{
								if (concrete != null)
								{
									concrete.Disconnect();
									ArrayPool<GraphNode>.Release(ref concrete.startNodes, false);
									ArrayPool<GraphNode>.Release(ref concrete.endNodes, false);
								}
								if (this.astar.data.linkGraph == null)
								{
									this.astar.data.AddGraph<LinkGraph>();
								}
								offMeshLinkConcrete.Connect(this.astar.data.linkGraph, source);
								offMeshLinkCombined2.concrete = offMeshLinkConcrete;
								source.status = OffMeshLinks.OffMeshLinkStatus.Active;
							}
						}
					}
				}
			}
			ListPool<OffMeshLinks.OffMeshLinkCombined>.Release(ref list);
			ListPool<GraphNode>.Release(ref list2);
			ListPool<GraphNode>.Release(ref list3);
		}

		// Token: 0x040002B6 RID: 694
		private AABBTree<OffMeshLinks.OffMeshLinkCombined> tree = new AABBTree<OffMeshLinks.OffMeshLinkCombined>();

		// Token: 0x040002B7 RID: 695
		private List<OffMeshLinks.OffMeshLinkSource> pendingAdd = new List<OffMeshLinks.OffMeshLinkSource>();

		// Token: 0x040002B8 RID: 696
		private bool updateScheduled;

		// Token: 0x040002B9 RID: 697
		private AstarPath astar;

		// Token: 0x040002BA RID: 698
		private NNConstraint cachedNNConstraint = NNConstraint.Walkable;

		// Token: 0x0200007F RID: 127
		public struct Anchor
		{
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001616F File Offset: 0x0001436F
			public readonly Vector3 point1
			{
				get
				{
					return this.center + this.rotation * new Vector3(-0.5f * this.width, 0f, 0f);
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x000161A2 File Offset: 0x000143A2
			public readonly Vector3 point2
			{
				get
				{
					return this.center + this.rotation * new Vector3(0.5f * this.width, 0f, 0f);
				}
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x000161D5 File Offset: 0x000143D5
			public static bool operator ==(OffMeshLinks.Anchor a, OffMeshLinks.Anchor b)
			{
				return a.center == b.center && a.rotation == b.rotation && a.width == b.width;
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0001620D File Offset: 0x0001440D
			public static bool operator !=(OffMeshLinks.Anchor a, OffMeshLinks.Anchor b)
			{
				return a.center != b.center || a.rotation != b.rotation || a.width != b.width;
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x00016248 File Offset: 0x00014448
			public override bool Equals(object obj)
			{
				return obj is OffMeshLinks.Anchor && this == (OffMeshLinks.Anchor)obj;
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x00016265 File Offset: 0x00014465
			public override int GetHashCode()
			{
				return (((this.center.GetHashCode() * 23) ^ this.rotation.GetHashCode()) * 23) ^ this.width.GetHashCode();
			}

			// Token: 0x040002BB RID: 699
			public Vector3 center;

			// Token: 0x040002BC RID: 700
			public Quaternion rotation;

			// Token: 0x040002BD RID: 701
			public float width;
		}

		// Token: 0x02000080 RID: 128
		public enum Directionality
		{
			// Token: 0x040002BF RID: 703
			OneWay,
			// Token: 0x040002C0 RID: 704
			TwoWay
		}

		// Token: 0x02000081 RID: 129
		[Flags]
		public enum OffMeshLinkStatus
		{
			// Token: 0x040002C2 RID: 706
			Inactive = 1,
			// Token: 0x040002C3 RID: 707
			Pending = 2,
			// Token: 0x040002C4 RID: 708
			Active = 4,
			// Token: 0x040002C5 RID: 709
			FailedToConnectStart = 9,
			// Token: 0x040002C6 RID: 710
			FailedToConnectEnd = 17,
			// Token: 0x040002C7 RID: 711
			PendingRemoval = 32
		}

		// Token: 0x02000082 RID: 130
		public readonly struct OffMeshLinkTracer
		{
			// Token: 0x06000428 RID: 1064 RVA: 0x0001629C File Offset: 0x0001449C
			public OffMeshLinkTracer(OffMeshLinks.OffMeshLinkConcrete link, bool reversed)
			{
				this.link = link;
				this.relativeStart = (reversed ? link.end.center : link.start.center);
				this.relativeEnd = (reversed ? link.start.center : link.end.center);
				this.isReverse = reversed;
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x000162F9 File Offset: 0x000144F9
			public OffMeshLinkTracer(OffMeshLinks.OffMeshLinkConcrete link, Vector3 relativeStart, Vector3 relativeEnd, bool isReverse)
			{
				this.link = link;
				this.relativeStart = relativeStart;
				this.relativeEnd = relativeEnd;
				this.isReverse = isReverse;
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x00016318 File Offset: 0x00014518
			public Component component
			{
				get
				{
					return this.link.component;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00016325 File Offset: 0x00014525
			public GameObject gameObject
			{
				get
				{
					return this.link.gameObject;
				}
			}

			// Token: 0x040002C8 RID: 712
			public readonly OffMeshLinks.OffMeshLinkConcrete link;

			// Token: 0x040002C9 RID: 713
			public readonly Vector3 relativeStart;

			// Token: 0x040002CA RID: 714
			public readonly Vector3 relativeEnd;

			// Token: 0x040002CB RID: 715
			public readonly bool isReverse;
		}

		// Token: 0x02000083 RID: 131
		public class OffMeshLinkSource
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x00016332 File Offset: 0x00014532
			public GameObject gameObject
			{
				get
				{
					if (!(this.component != null))
					{
						return null;
					}
					return this.component.gameObject;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001634F File Offset: 0x0001454F
			// (set) Token: 0x0600042E RID: 1070 RVA: 0x00016357 File Offset: 0x00014557
			public OffMeshLinks.OffMeshLinkStatus status { get; internal set; } = OffMeshLinks.OffMeshLinkStatus.Inactive;

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x00016360 File Offset: 0x00014560
			public Bounds bounds
			{
				get
				{
					Bounds bounds = default(Bounds);
					bounds.SetMinMax(this.start.point1, this.start.point2);
					bounds.Encapsulate(this.end.point1);
					bounds.Encapsulate(this.end.point2);
					bounds.Expand(this.maxSnappingDistance * 2f);
					return bounds;
				}
			}

			// Token: 0x040002CC RID: 716
			public OffMeshLinks.Anchor start;

			// Token: 0x040002CD RID: 717
			public OffMeshLinks.Anchor end;

			// Token: 0x040002CE RID: 718
			public OffMeshLinks.Directionality directionality;

			// Token: 0x040002CF RID: 719
			public PathfindingTag tag;

			// Token: 0x040002D0 RID: 720
			public float costFactor;

			// Token: 0x040002D1 RID: 721
			public float maxSnappingDistance;

			// Token: 0x040002D2 RID: 722
			public GraphMask graphMask;

			// Token: 0x040002D3 RID: 723
			public IOffMeshLinkHandler handler;

			// Token: 0x040002D4 RID: 724
			public Component component;

			// Token: 0x040002D5 RID: 725
			internal AABBTree<OffMeshLinks.OffMeshLinkCombined>.Key treeKey;
		}

		// Token: 0x02000084 RID: 132
		internal class OffMeshLinkCombined
		{
			// Token: 0x040002D7 RID: 727
			public OffMeshLinks.OffMeshLinkSource source;

			// Token: 0x040002D8 RID: 728
			public OffMeshLinks.OffMeshLinkConcrete concrete;
		}

		// Token: 0x02000085 RID: 133
		public class OffMeshLinkConcrete
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x000163D9 File Offset: 0x000145D9
			public IOffMeshLinkHandler handler
			{
				get
				{
					return this.source.handler;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x000163E6 File Offset: 0x000145E6
			public Component component
			{
				get
				{
					return this.source.component;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x000163F3 File Offset: 0x000145F3
			public GameObject gameObject
			{
				get
				{
					if (!(this.source.component != null))
					{
						return null;
					}
					return this.source.component.gameObject;
				}
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0001641C File Offset: 0x0001461C
			public bool Equivalent(OffMeshLinks.OffMeshLinkConcrete other)
			{
				if (this.start != other.start)
				{
					return false;
				}
				if (this.end != other.end)
				{
					return false;
				}
				if (this.startNodes.Length != other.startNodes.Length || this.endNodes.Length != other.endNodes.Length)
				{
					return false;
				}
				if (this.directionality != other.directionality || this.tag != other.tag || this.costFactor != other.costFactor)
				{
					return false;
				}
				for (int i = 0; i < this.startNodes.Length; i++)
				{
					if (this.startNodes[i] != other.startNodes[i])
					{
						return false;
					}
				}
				for (int j = 0; j < this.endNodes.Length; j++)
				{
					if (this.endNodes[j] != other.endNodes[j])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x00016500 File Offset: 0x00014700
			public void Disconnect()
			{
				if (this.startLinkNode != null && !this.startLinkNode.Destroyed)
				{
					LinkGraph linkGraph = this.startLinkNode.Graph as LinkGraph;
					linkGraph.RemoveNode(this.startLinkNode);
					linkGraph.RemoveNode(this.endLinkNode);
				}
				this.startLinkNode = null;
				this.endLinkNode = null;
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x00016558 File Offset: 0x00014758
			public void Connect(LinkGraph linkGraph, OffMeshLinks.OffMeshLinkSource source)
			{
				this.startLinkNode = linkGraph.AddNode();
				this.startLinkNode.linkSource = source;
				this.startLinkNode.linkConcrete = this;
				this.startLinkNode.position = (Int3)this.start.center;
				this.startLinkNode.Tag = this.tag;
				this.endLinkNode = linkGraph.AddNode();
				this.endLinkNode.position = (Int3)this.end.center;
				this.endLinkNode.linkSource = source;
				this.endLinkNode.linkConcrete = this;
				this.endLinkNode.Tag = this.tag;
				for (int i = 0; i < this.startNodes.Length; i++)
				{
					float magnitude = (VectorMath.ClosestPointOnSegment(this.start.point1, this.start.point2, (Vector3)this.startNodes[i].position) - (Vector3)this.startNodes[i].position).magnitude;
					uint num = (uint)(1000f * magnitude);
					GraphNode.Connect(this.startNodes[i], this.startLinkNode, num, this.directionality);
				}
				for (int j = 0; j < this.endNodes.Length; j++)
				{
					float magnitude2 = (VectorMath.ClosestPointOnSegment(this.end.point1, this.end.point2, (Vector3)this.endNodes[j].position) - (Vector3)this.endNodes[j].position).magnitude;
					uint num2 = (uint)(1000f * magnitude2);
					GraphNode.Connect(this.endLinkNode, this.endNodes[j], num2, this.directionality);
				}
				uint num3 = (uint)(1000f * this.costFactor * (this.end.center - this.start.center).magnitude);
				GraphNode.Connect(this.startLinkNode, this.endLinkNode, num3, this.directionality);
				this.staleConnections = false;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x00016777 File Offset: 0x00014977
			public OffMeshLinks.OffMeshLinkTracer GetTracer(LinkNode firstNode)
			{
				return new OffMeshLinks.OffMeshLinkTracer(this, firstNode == this.endLinkNode);
			}

			// Token: 0x040002D9 RID: 729
			public OffMeshLinks.Anchor start;

			// Token: 0x040002DA RID: 730
			public OffMeshLinks.Anchor end;

			// Token: 0x040002DB RID: 731
			public GraphNode[] startNodes;

			// Token: 0x040002DC RID: 732
			public GraphNode[] endNodes;

			// Token: 0x040002DD RID: 733
			public LinkNode startLinkNode;

			// Token: 0x040002DE RID: 734
			public LinkNode endLinkNode;

			// Token: 0x040002DF RID: 735
			public OffMeshLinks.Directionality directionality;

			// Token: 0x040002E0 RID: 736
			public PathfindingTag tag;

			// Token: 0x040002E1 RID: 737
			public float costFactor;

			// Token: 0x040002E2 RID: 738
			internal bool staleConnections;

			// Token: 0x040002E3 RID: 739
			internal OffMeshLinks.OffMeshLinkSource source;
		}
	}
}
