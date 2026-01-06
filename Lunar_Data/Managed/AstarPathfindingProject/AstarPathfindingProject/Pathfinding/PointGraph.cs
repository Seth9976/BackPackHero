using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F2 RID: 242
	[JsonOptIn]
	[Preserve]
	public class PointGraph : NavGraph, IUpdatableGraph
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000291C7 File Offset: 0x000273C7
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x000291CF File Offset: 0x000273CF
		public int nodeCount { get; protected set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000291D8 File Offset: 0x000273D8
		public override bool isScanned
		{
			get
			{
				return this.nodes != null;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000291E3 File Offset: 0x000273E3
		public override int CountNodes()
		{
			return this.nodeCount;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000291EC File Offset: 0x000273EC
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			int nodeCount = this.nodeCount;
			for (int i = 0; i < nodeCount; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00029224 File Offset: 0x00027424
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, float maxDistanceSqr)
		{
			if (this.nodes == null)
			{
				return NNInfo.Empty;
			}
			Int3 @int = (Int3)position;
			if (this.optimizeForSparseGraph)
			{
				if (this.nearestNodeDistanceMode == PointGraph.NodeDistanceMode.Node)
				{
					float num = maxDistanceSqr;
					GraphNode nearest = this.lookupTree.GetNearest(@int, constraint, ref num);
					return new NNInfo(nearest, (Vector3)nearest.position, num);
				}
				GraphNode nearestConnection = this.lookupTree.GetNearestConnection(@int, constraint, this.maximumConnectionLength);
				if (nearestConnection == null)
				{
					return NNInfo.Empty;
				}
				return this.FindClosestConnectionPoint(nearestConnection as PointNode, position, maxDistanceSqr);
			}
			else
			{
				PointNode pointNode = null;
				long num2 = AstarMath.SaturatingConvertFloatToLong(maxDistanceSqr * 1000f * 1000f);
				for (int i = 0; i < this.nodeCount; i++)
				{
					PointNode pointNode2 = this.nodes[i];
					long sqrMagnitudeLong = (@int - pointNode2.position).sqrMagnitudeLong;
					if (sqrMagnitudeLong < num2 && (constraint == null || constraint.Suitable(pointNode2)))
					{
						num2 = sqrMagnitudeLong;
						pointNode = pointNode2;
					}
				}
				if (1.0000001E-06f * (float)num2 >= maxDistanceSqr || pointNode == null)
				{
					return NNInfo.Empty;
				}
				return new NNInfo(pointNode, (Vector3)pointNode.position, 1.0000001E-06f * (float)num2);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002933C File Offset: 0x0002753C
		private NNInfo FindClosestConnectionPoint(PointNode node, Vector3 position, float maxDistanceSqr)
		{
			Vector3 vector = (Vector3)node.position;
			Connection[] connections = node.connections;
			Vector3 vector2 = (Vector3)node.position;
			if (connections != null)
			{
				for (int i = 0; i < connections.Length; i++)
				{
					Vector3 vector3 = ((Vector3)connections[i].node.position + vector2) * 0.5f;
					Vector3 vector4 = VectorMath.ClosestPointOnSegment(vector2, vector3, position);
					float sqrMagnitude = (vector4 - position).sqrMagnitude;
					if (sqrMagnitude < maxDistanceSqr)
					{
						maxDistanceSqr = sqrMagnitude;
						vector = vector4;
					}
				}
			}
			return new NNInfo(node, vector, maxDistanceSqr);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000293D3 File Offset: 0x000275D3
		public PointNode AddNode(Int3 position)
		{
			return this.AddNode<PointNode>(new PointNode(this.active), position);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000293E8 File Offset: 0x000275E8
		public T AddNode<T>(T node, Int3 position) where T : PointNode
		{
			base.AssertSafeToUpdateGraph();
			if (this.nodes == null || this.nodeCount == this.nodes.Length)
			{
				PointNode[] array = new PointNode[(this.nodes != null) ? Math.Max(this.nodes.Length + 4, this.nodes.Length * 2) : 4];
				if (this.nodes != null)
				{
					this.nodes.CopyTo(array, 0);
				}
				this.nodes = array;
			}
			node.SetPosition(position);
			node.GraphIndex = this.graphIndex;
			node.Walkable = true;
			this.nodes[this.nodeCount] = node;
			int nodeCount = this.nodeCount;
			this.nodeCount = nodeCount + 1;
			if (this.optimizeForSparseGraph)
			{
				this.AddToLookup(node);
			}
			return node;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000294BC File Offset: 0x000276BC
		protected static int CountChildren(Transform tr)
		{
			int num = 0;
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				num++;
				num += PointGraph.CountChildren(transform);
			}
			return num;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002951C File Offset: 0x0002771C
		protected static void AddChildren(PointNode[] nodes, ref int c, Transform tr)
		{
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				nodes[c].position = (Int3)transform.position;
				nodes[c].Walkable = true;
				nodes[c].gameObject = transform.gameObject;
				c++;
				PointGraph.AddChildren(nodes, ref c, transform);
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000295A4 File Offset: 0x000277A4
		public void RebuildNodeLookup()
		{
			this.lookupTree = PointGraph.BuildNodeLookup(this.nodes, this.nodeCount, this.optimizeForSparseGraph);
			this.RebuildConnectionDistanceLookup();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000295CC File Offset: 0x000277CC
		private static PointKDTree BuildNodeLookup(PointNode[] nodes, int nodeCount, bool optimizeForSparseGraph)
		{
			if (optimizeForSparseGraph && nodes != null)
			{
				PointKDTree pointKDTree = new PointKDTree();
				pointKDTree.Rebuild(nodes, 0, nodeCount);
				return pointKDTree;
			}
			return null;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000295F1 File Offset: 0x000277F1
		public void RebuildConnectionDistanceLookup()
		{
			if (this.nearestNodeDistanceMode == PointGraph.NodeDistanceMode.Connection)
			{
				this.maximumConnectionLength = PointGraph.LongestConnectionLength(this.nodes, this.nodeCount);
				return;
			}
			this.maximumConnectionLength = 0L;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002961C File Offset: 0x0002781C
		private static long LongestConnectionLength(PointNode[] nodes, int nodeCount)
		{
			long num = 0L;
			for (int i = 0; i < nodeCount; i++)
			{
				PointNode pointNode = nodes[i];
				Connection[] connections = pointNode.connections;
				if (connections != null)
				{
					for (int j = 0; j < connections.Length; j++)
					{
						long sqrMagnitudeLong = (pointNode.position - connections[j].node.position).sqrMagnitudeLong;
						num = Math.Max(num, sqrMagnitudeLong);
					}
				}
			}
			return num;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002968A File Offset: 0x0002788A
		private void AddToLookup(PointNode node)
		{
			this.lookupTree.Add(node);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00029698 File Offset: 0x00027898
		public void RegisterConnectionLength(long sqrLength)
		{
			this.maximumConnectionLength = Math.Max(this.maximumConnectionLength, sqrLength);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000296AC File Offset: 0x000278AC
		protected virtual PointNode[] CreateNodes(int count)
		{
			PointNode[] array = new PointNode[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new PointNode(this.active);
			}
			return array;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000296DB File Offset: 0x000278DB
		protected override IGraphUpdatePromise ScanInternal()
		{
			return new PointGraph.PointGraphScanPromise
			{
				graph = this
			};
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000296EC File Offset: 0x000278EC
		public void ConnectNodes()
		{
			base.AssertSafeToUpdateGraph();
			IEnumerator<float> enumerator = PointGraph.ConnectNodesAsync(this.nodes, this.nodeCount, this.lookupTree, this.maxDistance, this.limits, this).GetEnumerator();
			while (enumerator.MoveNext())
			{
			}
			this.RebuildConnectionDistanceLookup();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00029737 File Offset: 0x00027937
		private static IEnumerable<float> ConnectNodesAsync(PointNode[] nodes, int nodeCount, PointKDTree lookupTree, float maxDistance, Vector3 limits, PointGraph graph)
		{
			if (maxDistance >= 0f)
			{
				List<Connection> connections = new List<Connection>();
				List<GraphNode> candidateConnections = new List<GraphNode>();
				long maxSquaredRange;
				if (maxDistance == 0f && (limits.x == 0f || limits.y == 0f || limits.z == 0f))
				{
					maxSquaredRange = long.MaxValue;
				}
				else
				{
					maxSquaredRange = (long)(Mathf.Max(limits.x, Mathf.Max(limits.y, Mathf.Max(limits.z, maxDistance))) * 1000f) + 1L;
					maxSquaredRange *= maxSquaredRange;
				}
				int num3;
				for (int i = 0; i < nodeCount; i = num3 + 1)
				{
					if (i % 512 == 0)
					{
						yield return (float)i / (float)nodeCount;
					}
					connections.Clear();
					PointNode pointNode = nodes[i];
					if (lookupTree != null)
					{
						candidateConnections.Clear();
						lookupTree.GetInRange(pointNode.position, maxSquaredRange, candidateConnections);
						for (int j = 0; j < candidateConnections.Count; j++)
						{
							PointNode pointNode2 = candidateConnections[j] as PointNode;
							float num;
							if (pointNode2 != pointNode && graph.IsValidConnection(pointNode, pointNode2, out num))
							{
								connections.Add(new Connection(pointNode2, (uint)Mathf.RoundToInt(num * 1000f), true, true));
							}
						}
					}
					else
					{
						for (int k = 0; k < nodeCount; k++)
						{
							if (i != k)
							{
								PointNode pointNode3 = nodes[k];
								float num2;
								if (graph.IsValidConnection(pointNode, pointNode3, out num2))
								{
									connections.Add(new Connection(pointNode3, (uint)Mathf.RoundToInt(num2 * 1000f), true, true));
								}
							}
						}
					}
					pointNode.connections = connections.ToArray();
					pointNode.SetConnectivityDirty();
					num3 = i;
				}
				connections = null;
				candidateConnections = null;
			}
			yield break;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0002976C File Offset: 0x0002796C
		public virtual bool IsValidConnection(GraphNode a, GraphNode b, out float dist)
		{
			dist = 0f;
			if (!a.Walkable || !b.Walkable)
			{
				return false;
			}
			Vector3 vector = (Vector3)(b.position - a.position);
			if ((!Mathf.Approximately(this.limits.x, 0f) && Mathf.Abs(vector.x) > this.limits.x) || (!Mathf.Approximately(this.limits.y, 0f) && Mathf.Abs(vector.y) > this.limits.y) || (!Mathf.Approximately(this.limits.z, 0f) && Mathf.Abs(vector.z) > this.limits.z))
			{
				return false;
			}
			dist = vector.magnitude;
			if (this.maxDistance != 0f && dist >= this.maxDistance)
			{
				return false;
			}
			if (!this.raycast)
			{
				return true;
			}
			Ray ray = new Ray((Vector3)a.position, vector);
			Ray ray2 = new Ray((Vector3)b.position, -vector);
			if (this.use2DPhysics)
			{
				if (this.thickRaycast)
				{
					return !Physics2D.CircleCast(ray.origin, this.thickRaycastRadius, ray.direction, dist, this.mask) && !Physics2D.CircleCast(ray2.origin, this.thickRaycastRadius, ray2.direction, dist, this.mask);
				}
				return !Physics2D.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics2D.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
			else
			{
				if (this.thickRaycast)
				{
					return !Physics.SphereCast(ray, this.thickRaycastRadius, dist, this.mask) && !Physics.SphereCast(ray2, this.thickRaycastRadius, dist, this.mask);
				}
				return !Physics.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00029A21 File Offset: 0x00027C21
		IGraphUpdatePromise IUpdatableGraph.ScheduleGraphUpdates(List<GraphUpdateObject> graphUpdates)
		{
			if (!this.isScanned)
			{
				return null;
			}
			return new PointGraph.PointGraphUpdatePromise
			{
				graph = this,
				graphUpdates = graphUpdates
			};
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00029A40 File Offset: 0x00027C40
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.RebuildNodeLookup();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00029A48 File Offset: 0x00027C48
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			base.RelocateNodes(deltaMatrix);
			this.RebuildNodeLookup();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00029A58 File Offset: 0x00027C58
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
			}
			ctx.writer.Write(this.nodeCount);
			for (int i = 0; i < this.nodeCount; i++)
			{
				if (this.nodes[i] == null)
				{
					ctx.writer.Write(-1);
				}
				else
				{
					ctx.writer.Write(0);
					this.nodes[i].SerializeNode(ctx);
				}
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00029AD0 File Offset: 0x00027CD0
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new PointNode[num];
			this.nodeCount = num;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (ctx.reader.ReadInt32() != -1)
				{
					this.nodes[i] = new PointNode(this.active);
					this.nodes[i].DeserializeNode(ctx);
				}
			}
		}

		// Token: 0x040004EC RID: 1260
		[JsonMember]
		public Transform root;

		// Token: 0x040004ED RID: 1261
		[JsonMember]
		public string searchTag;

		// Token: 0x040004EE RID: 1262
		[JsonMember]
		public float maxDistance;

		// Token: 0x040004EF RID: 1263
		[JsonMember]
		public Vector3 limits;

		// Token: 0x040004F0 RID: 1264
		[JsonMember]
		public bool raycast = true;

		// Token: 0x040004F1 RID: 1265
		[JsonMember]
		public bool use2DPhysics;

		// Token: 0x040004F2 RID: 1266
		[JsonMember]
		public bool thickRaycast;

		// Token: 0x040004F3 RID: 1267
		[JsonMember]
		public float thickRaycastRadius = 1f;

		// Token: 0x040004F4 RID: 1268
		[JsonMember]
		public bool recursive = true;

		// Token: 0x040004F5 RID: 1269
		[JsonMember]
		public LayerMask mask;

		// Token: 0x040004F6 RID: 1270
		[JsonMember]
		public bool optimizeForSparseGraph;

		// Token: 0x040004F7 RID: 1271
		private PointKDTree lookupTree = new PointKDTree();

		// Token: 0x040004F8 RID: 1272
		private long maximumConnectionLength;

		// Token: 0x040004F9 RID: 1273
		public PointNode[] nodes;

		// Token: 0x040004FA RID: 1274
		[JsonMember]
		public PointGraph.NodeDistanceMode nearestNodeDistanceMode;

		// Token: 0x020000F3 RID: 243
		public enum NodeDistanceMode
		{
			// Token: 0x040004FD RID: 1277
			Node,
			// Token: 0x040004FE RID: 1278
			Connection
		}

		// Token: 0x020000F4 RID: 244
		private class PointGraphScanPromise : IGraphUpdatePromise
		{
			// Token: 0x06000809 RID: 2057 RVA: 0x00029B76 File Offset: 0x00027D76
			public IEnumerator<JobHandle> Prepare()
			{
				Transform root = this.graph.root;
				if (root == null)
				{
					GameObject[] array = ((this.graph.searchTag != null) ? GameObject.FindGameObjectsWithTag(this.graph.searchTag) : null);
					if (array == null)
					{
						this.nodes = new PointNode[0];
					}
					else
					{
						this.nodes = this.graph.CreateNodes(array.Length);
						for (int i = 0; i < array.Length; i++)
						{
							PointNode pointNode = this.nodes[i];
							pointNode.position = (Int3)array[i].transform.position;
							pointNode.Walkable = true;
							pointNode.gameObject = array[i].gameObject;
						}
					}
				}
				else
				{
					if (!this.graph.recursive)
					{
						int childCount = root.childCount;
						this.nodes = this.graph.CreateNodes(childCount);
						int num = 0;
						using (IEnumerator enumerator = root.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Transform transform = (Transform)obj;
								PointNode pointNode2 = this.nodes[num];
								pointNode2.position = (Int3)transform.position;
								pointNode2.Walkable = true;
								pointNode2.gameObject = transform.gameObject;
								num++;
							}
							goto IL_01A9;
						}
					}
					int num2 = PointGraph.CountChildren(root);
					this.nodes = this.graph.CreateNodes(num2);
					int num3 = 0;
					PointGraph.AddChildren(this.nodes, ref num3, root);
				}
				IL_01A9:
				JobHandle jobHandle = default(JobHandle);
				this.lookupTree = PointGraph.BuildNodeLookup(this.nodes, this.nodes.Length, this.graph.optimizeForSparseGraph);
				foreach (float num4 in PointGraph.ConnectNodesAsync(this.nodes, this.nodes.Length, this.lookupTree, this.graph.maxDistance, this.graph.limits, this.graph))
				{
					jobHandle = default(JobHandle);
				}
				IEnumerator<float> enumerator2 = null;
				yield break;
				yield break;
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x00029B88 File Offset: 0x00027D88
			public void Apply(IGraphUpdateContext ctx)
			{
				this.graph.DestroyAllNodes();
				this.graph.lookupTree = this.lookupTree;
				this.graph.nodes = this.nodes;
				this.graph.nodeCount = this.nodes.Length;
				this.graph.maximumConnectionLength = ((this.graph.nearestNodeDistanceMode == PointGraph.NodeDistanceMode.Connection) ? PointGraph.LongestConnectionLength(this.nodes, this.nodes.Length) : 0L);
			}

			// Token: 0x040004FF RID: 1279
			public PointGraph graph;

			// Token: 0x04000500 RID: 1280
			private PointKDTree lookupTree;

			// Token: 0x04000501 RID: 1281
			private PointNode[] nodes;
		}

		// Token: 0x020000F6 RID: 246
		private class PointGraphUpdatePromise : IGraphUpdatePromise
		{
			// Token: 0x06000813 RID: 2067 RVA: 0x00029F4C File Offset: 0x0002814C
			public void Apply(IGraphUpdateContext ctx)
			{
				PointNode[] nodes = this.graph.nodes;
				for (int i = 0; i < this.graphUpdates.Count; i++)
				{
					GraphUpdateObject graphUpdateObject = this.graphUpdates[i];
					for (int j = 0; j < this.graph.nodeCount; j++)
					{
						PointNode pointNode = nodes[j];
						if (graphUpdateObject.bounds.Contains((Vector3)pointNode.position))
						{
							graphUpdateObject.WillUpdateNode(pointNode);
							graphUpdateObject.Apply(pointNode);
						}
					}
					if (graphUpdateObject.updatePhysics)
					{
						Bounds bounds = graphUpdateObject.bounds;
						if (this.graph.thickRaycast)
						{
							bounds.Expand(this.graph.thickRaycastRadius * 2f);
						}
						List<Connection> list = ListPool<Connection>.Claim();
						for (int k = 0; k < this.graph.nodeCount; k++)
						{
							PointNode pointNode2 = this.graph.nodes[k];
							Vector3 vector = (Vector3)pointNode2.position;
							List<Connection> list2 = null;
							for (int l = 0; l < this.graph.nodeCount; l++)
							{
								if (l != k)
								{
									Vector3 vector2 = (Vector3)nodes[l].position;
									if (VectorMath.SegmentIntersectsBounds(bounds, vector, vector2))
									{
										PointNode pointNode3 = nodes[l];
										bool flag = pointNode2.ContainsOutgoingConnection(pointNode3);
										float num;
										bool flag2 = this.graph.IsValidConnection(pointNode2, pointNode3, out num);
										if (list2 == null && flag != flag2)
										{
											list.Clear();
											list2 = list;
											list2.AddRange(pointNode2.connections);
										}
										if (!flag && flag2)
										{
											uint num2 = (uint)Mathf.RoundToInt(num * 1000f);
											list2.Add(new Connection(pointNode3, num2, true, true));
											this.graph.RegisterConnectionLength((pointNode3.position - pointNode2.position).sqrMagnitudeLong);
										}
										else if (flag && !flag2)
										{
											for (int m = 0; m < list2.Count; m++)
											{
												if (list2[m].node == pointNode3)
												{
													list2.RemoveAt(m);
													break;
												}
											}
										}
									}
								}
							}
							if (list2 != null)
							{
								pointNode2.connections = list2.ToArray();
								pointNode2.SetConnectivityDirty();
							}
						}
						ListPool<Connection>.Release(ref list);
						ctx.DirtyBounds(graphUpdateObject.bounds);
					}
				}
				ListPool<GraphUpdateObject>.Release(ref this.graphUpdates);
			}

			// Token: 0x04000506 RID: 1286
			public PointGraph graph;

			// Token: 0x04000507 RID: 1287
			public List<GraphUpdateObject> graphUpdates;
		}
	}
}
