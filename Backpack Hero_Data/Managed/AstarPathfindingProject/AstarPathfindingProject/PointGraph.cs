using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000068 RID: 104
	[JsonOptIn]
	[Preserve]
	public class PointGraph : NavGraph, IUpdatableGraph
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001F925 File Offset: 0x0001DB25
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001F92D File Offset: 0x0001DB2D
		public int nodeCount { get; protected set; }

		// Token: 0x06000593 RID: 1427 RVA: 0x0001F936 File Offset: 0x0001DB36
		public override int CountNodes()
		{
			return this.nodeCount;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001F940 File Offset: 0x0001DB40
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

		// Token: 0x06000595 RID: 1429 RVA: 0x0001F977 File Offset: 0x0001DB77
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestInternal(position, constraint, true);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001F982 File Offset: 0x0001DB82
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearestInternal(position, constraint, false);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001F990 File Offset: 0x0001DB90
		private NNInfoInternal GetNearestInternal(Vector3 position, NNConstraint constraint, bool fastCheck)
		{
			if (this.nodes == null)
			{
				return default(NNInfoInternal);
			}
			Int3 @int = (Int3)position;
			if (!this.optimizeForSparseGraph)
			{
				float num = ((constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity);
				num *= 1000000f;
				NNInfoInternal nninfoInternal = new NNInfoInternal(null);
				long num2 = long.MaxValue;
				long num3 = long.MaxValue;
				for (int i = 0; i < this.nodeCount; i++)
				{
					PointNode pointNode = this.nodes[i];
					long sqrMagnitudeLong = (@int - pointNode.position).sqrMagnitudeLong;
					if (sqrMagnitudeLong < num2)
					{
						num2 = sqrMagnitudeLong;
						nninfoInternal.node = pointNode;
					}
					if (sqrMagnitudeLong < num3 && (float)sqrMagnitudeLong < num && (constraint == null || constraint.Suitable(pointNode)))
					{
						num3 = sqrMagnitudeLong;
						nninfoInternal.constrainedNode = pointNode;
					}
				}
				if (!fastCheck)
				{
					nninfoInternal.node = nninfoInternal.constrainedNode;
				}
				nninfoInternal.UpdateInfo();
				return nninfoInternal;
			}
			if (this.nearestNodeDistanceMode == PointGraph.NodeDistanceMode.Node)
			{
				return new NNInfoInternal(this.lookupTree.GetNearest(@int, fastCheck ? null : constraint));
			}
			GraphNode nearestConnection = this.lookupTree.GetNearestConnection(@int, fastCheck ? null : constraint, this.maximumConnectionLength);
			if (nearestConnection == null)
			{
				return default(NNInfoInternal);
			}
			return this.FindClosestConnectionPoint(nearestConnection as PointNode, position);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		private NNInfoInternal FindClosestConnectionPoint(PointNode node, Vector3 position)
		{
			Vector3 vector = (Vector3)node.position;
			Connection[] connections = node.connections;
			Vector3 vector2 = (Vector3)node.position;
			float num = float.PositiveInfinity;
			if (connections != null)
			{
				for (int i = 0; i < connections.Length; i++)
				{
					Vector3 vector3 = ((Vector3)connections[i].node.position + vector2) * 0.5f;
					Vector3 vector4 = VectorMath.ClosestPointOnSegment(vector2, vector3, position);
					float sqrMagnitude = (vector4 - position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						num = sqrMagnitude;
						vector = vector4;
					}
				}
			}
			return new NNInfoInternal
			{
				node = node,
				clampedPosition = vector
			};
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001FB97 File Offset: 0x0001DD97
		public PointNode AddNode(Int3 position)
		{
			return this.AddNode<PointNode>(new PointNode(this.active), position);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		public T AddNode<T>(T node, Int3 position) where T : PointNode
		{
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

		// Token: 0x0600059B RID: 1435 RVA: 0x0001FC78 File Offset: 0x0001DE78
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

		// Token: 0x0600059C RID: 1436 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		protected void AddChildren(ref int c, Transform tr)
		{
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				this.nodes[c].position = (Int3)transform.position;
				this.nodes[c].Walkable = true;
				this.nodes[c].gameObject = transform.gameObject;
				c++;
				this.AddChildren(ref c, transform);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001FD70 File Offset: 0x0001DF70
		public void RebuildNodeLookup()
		{
			if (!this.optimizeForSparseGraph || this.nodes == null)
			{
				this.lookupTree = new PointKDTree();
			}
			else
			{
				PointKDTree pointKDTree = this.lookupTree;
				GraphNode[] array = this.nodes;
				pointKDTree.Rebuild(array, 0, this.nodeCount);
			}
			this.RebuildConnectionDistanceLookup();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public void RebuildConnectionDistanceLookup()
		{
			this.maximumConnectionLength = 0L;
			if (this.nearestNodeDistanceMode == PointGraph.NodeDistanceMode.Connection)
			{
				for (int i = 0; i < this.nodeCount; i++)
				{
					PointNode pointNode = this.nodes[i];
					Connection[] connections = pointNode.connections;
					if (connections != null)
					{
						for (int j = 0; j < connections.Length; j++)
						{
							long sqrMagnitudeLong = (pointNode.position - connections[j].node.position).sqrMagnitudeLong;
							this.RegisterConnectionLength(sqrMagnitudeLong);
						}
					}
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001FE3B File Offset: 0x0001E03B
		private void AddToLookup(PointNode node)
		{
			this.lookupTree.Add(node);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001FE49 File Offset: 0x0001E049
		public void RegisterConnectionLength(long sqrLength)
		{
			this.maximumConnectionLength = Math.Max(this.maximumConnectionLength, sqrLength);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001FE60 File Offset: 0x0001E060
		protected virtual PointNode[] CreateNodes(int count)
		{
			PointNode[] array = new PointNode[count];
			for (int i = 0; i < this.nodeCount; i++)
			{
				array[i] = new PointNode(this.active);
			}
			return array;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001FE94 File Offset: 0x0001E094
		protected override IEnumerable<Progress> ScanInternal()
		{
			yield return new Progress(0f, "Searching for GameObjects");
			if (this.root == null)
			{
				GameObject[] gos = ((this.searchTag != null) ? GameObject.FindGameObjectsWithTag(this.searchTag) : null);
				if (gos == null)
				{
					this.nodes = new PointNode[0];
					this.nodeCount = 0;
				}
				else
				{
					yield return new Progress(0.1f, "Creating nodes");
					this.nodeCount = gos.Length;
					this.nodes = this.CreateNodes(this.nodeCount);
					for (int i = 0; i < gos.Length; i++)
					{
						this.nodes[i].position = (Int3)gos[i].transform.position;
						this.nodes[i].Walkable = true;
						this.nodes[i].gameObject = gos[i].gameObject;
					}
				}
				gos = null;
			}
			else
			{
				if (!this.recursive)
				{
					this.nodeCount = this.root.childCount;
					this.nodes = this.CreateNodes(this.nodeCount);
					int num = 0;
					using (IEnumerator enumerator = this.root.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Transform transform = (Transform)obj;
							this.nodes[num].position = (Int3)transform.position;
							this.nodes[num].Walkable = true;
							this.nodes[num].gameObject = transform.gameObject;
							num++;
						}
						goto IL_024C;
					}
				}
				this.nodeCount = PointGraph.CountChildren(this.root);
				this.nodes = this.CreateNodes(this.nodeCount);
				int num2 = 0;
				this.AddChildren(ref num2, this.root);
			}
			IL_024C:
			yield return new Progress(0.15f, "Building node lookup");
			this.RebuildNodeLookup();
			foreach (Progress progress in this.ConnectNodesAsync())
			{
				yield return progress.MapTo(0.15f, 0.95f, null);
			}
			IEnumerator<Progress> enumerator2 = null;
			yield return new Progress(0.95f, "Building connection distances");
			this.RebuildConnectionDistanceLookup();
			yield break;
			yield break;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		public void ConnectNodes()
		{
			IEnumerator<Progress> enumerator = this.ConnectNodesAsync().GetEnumerator();
			while (enumerator.MoveNext())
			{
			}
			this.RebuildConnectionDistanceLookup();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001FECB File Offset: 0x0001E0CB
		private IEnumerable<Progress> ConnectNodesAsync()
		{
			if (this.maxDistance >= 0f)
			{
				List<Connection> connections = new List<Connection>();
				List<GraphNode> candidateConnections = new List<GraphNode>();
				long maxSquaredRange;
				if (this.maxDistance == 0f && (this.limits.x == 0f || this.limits.y == 0f || this.limits.z == 0f))
				{
					maxSquaredRange = long.MaxValue;
				}
				else
				{
					maxSquaredRange = (long)(Mathf.Max(this.limits.x, Mathf.Max(this.limits.y, Mathf.Max(this.limits.z, this.maxDistance))) * 1000f) + 1L;
					maxSquaredRange *= maxSquaredRange;
				}
				int num3;
				for (int i = 0; i < this.nodeCount; i = num3 + 1)
				{
					if (i % 512 == 0)
					{
						yield return new Progress((float)i / (float)this.nodeCount, "Connecting nodes");
					}
					connections.Clear();
					PointNode pointNode = this.nodes[i];
					if (this.optimizeForSparseGraph)
					{
						candidateConnections.Clear();
						this.lookupTree.GetInRange(pointNode.position, maxSquaredRange, candidateConnections);
						for (int j = 0; j < candidateConnections.Count; j++)
						{
							PointNode pointNode2 = candidateConnections[j] as PointNode;
							float num;
							if (pointNode2 != pointNode && this.IsValidConnection(pointNode, pointNode2, out num))
							{
								connections.Add(new Connection(pointNode2, (uint)Mathf.RoundToInt(num * 1000f), byte.MaxValue));
							}
						}
					}
					else
					{
						for (int k = 0; k < this.nodeCount; k++)
						{
							if (i != k)
							{
								PointNode pointNode3 = this.nodes[k];
								float num2;
								if (this.IsValidConnection(pointNode, pointNode3, out num2))
								{
									connections.Add(new Connection(pointNode3, (uint)Mathf.RoundToInt(num2 * 1000f), byte.MaxValue));
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

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001FEDC File Offset: 0x0001E0DC
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

		// Token: 0x060005A6 RID: 1446 RVA: 0x00020191 File Offset: 0x0001E391
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00020194 File Offset: 0x0001E394
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00020196 File Offset: 0x0001E396
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00020198 File Offset: 0x0001E398
		void IUpdatableGraph.UpdateArea(GraphUpdateObject guo)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodeCount; i++)
			{
				PointNode pointNode = this.nodes[i];
				if (guo.bounds.Contains((Vector3)pointNode.position))
				{
					guo.WillUpdateNode(pointNode);
					guo.Apply(pointNode);
				}
			}
			if (guo.updatePhysics)
			{
				Bounds bounds = guo.bounds;
				if (this.thickRaycast)
				{
					bounds.Expand(this.thickRaycastRadius * 2f);
				}
				List<Connection> list = ListPool<Connection>.Claim();
				for (int j = 0; j < this.nodeCount; j++)
				{
					PointNode pointNode2 = this.nodes[j];
					Vector3 vector = (Vector3)pointNode2.position;
					List<Connection> list2 = null;
					for (int k = 0; k < this.nodeCount; k++)
					{
						if (k != j)
						{
							Vector3 vector2 = (Vector3)this.nodes[k].position;
							if (VectorMath.SegmentIntersectsBounds(bounds, vector, vector2))
							{
								PointNode pointNode3 = this.nodes[k];
								bool flag = pointNode2.ContainsConnection(pointNode3);
								float num;
								bool flag2 = this.IsValidConnection(pointNode2, pointNode3, out num);
								if (list2 == null && flag != flag2)
								{
									list.Clear();
									list2 = list;
									list2.AddRange(pointNode2.connections);
								}
								if (!flag && flag2)
								{
									uint num2 = (uint)Mathf.RoundToInt(num * 1000f);
									list2.Add(new Connection(pointNode3, num2, byte.MaxValue));
									this.RegisterConnectionLength((pointNode3.position - pointNode2.position).sqrMagnitudeLong);
								}
								else if (flag && !flag2)
								{
									for (int l = 0; l < list2.Count; l++)
									{
										if (list2[l].node == pointNode3)
										{
											list2.RemoveAt(l);
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
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00020392 File Offset: 0x0001E592
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.RebuildNodeLookup();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002039A File Offset: 0x0001E59A
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			base.RelocateNodes(deltaMatrix);
			this.RebuildNodeLookup();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000203AC File Offset: 0x0001E5AC
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.root = ctx.DeserializeUnityObject() as Transform;
			this.searchTag = ctx.reader.ReadString();
			this.maxDistance = ctx.reader.ReadSingle();
			this.limits = ctx.DeserializeVector3();
			this.raycast = ctx.reader.ReadBoolean();
			this.use2DPhysics = ctx.reader.ReadBoolean();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastRadius = ctx.reader.ReadSingle();
			this.recursive = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
			this.mask = ctx.reader.ReadInt32();
			this.optimizeForSparseGraph = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00020494 File Offset: 0x0001E694
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

		// Token: 0x060005AE RID: 1454 RVA: 0x0002050C File Offset: 0x0001E70C
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

		// Token: 0x04000316 RID: 790
		[JsonMember]
		public Transform root;

		// Token: 0x04000317 RID: 791
		[JsonMember]
		public string searchTag;

		// Token: 0x04000318 RID: 792
		[JsonMember]
		public float maxDistance;

		// Token: 0x04000319 RID: 793
		[JsonMember]
		public Vector3 limits;

		// Token: 0x0400031A RID: 794
		[JsonMember]
		public bool raycast = true;

		// Token: 0x0400031B RID: 795
		[JsonMember]
		public bool use2DPhysics;

		// Token: 0x0400031C RID: 796
		[JsonMember]
		public bool thickRaycast;

		// Token: 0x0400031D RID: 797
		[JsonMember]
		public float thickRaycastRadius = 1f;

		// Token: 0x0400031E RID: 798
		[JsonMember]
		public bool recursive = true;

		// Token: 0x0400031F RID: 799
		[JsonMember]
		public LayerMask mask;

		// Token: 0x04000320 RID: 800
		[JsonMember]
		public bool optimizeForSparseGraph;

		// Token: 0x04000321 RID: 801
		private PointKDTree lookupTree = new PointKDTree();

		// Token: 0x04000322 RID: 802
		private long maximumConnectionLength;

		// Token: 0x04000323 RID: 803
		public PointNode[] nodes;

		// Token: 0x04000324 RID: 804
		[JsonMember]
		public PointGraph.NodeDistanceMode nearestNodeDistanceMode;

		// Token: 0x02000120 RID: 288
		public enum NodeDistanceMode
		{
			// Token: 0x040006B8 RID: 1720
			Node,
			// Token: 0x040006B9 RID: 1721
			Connection
		}
	}
}
