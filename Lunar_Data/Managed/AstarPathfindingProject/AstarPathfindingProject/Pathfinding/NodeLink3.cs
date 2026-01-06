using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007D RID: 125
	[AddComponentMenu("Pathfinding/Link3")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/nodelink3.html")]
	public class NodeLink3 : GraphModifier
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00014E80 File Offset: 0x00013080
		public static NodeLink3 GetNodeLink(GraphNode node)
		{
			NodeLink3 nodeLink;
			NodeLink3.reference.TryGetValue(node, out nodeLink);
			return nodeLink;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000144ED File Offset: 0x000126ED
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00014E9C File Offset: 0x0001309C
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00014EA4 File Offset: 0x000130A4
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00014EAC File Offset: 0x000130AC
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014EB4 File Offset: 0x000130B4
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool _)
			{
				this.InternalOnPostScan();
				return true;
			}));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014EE4 File Offset: 0x000130E4
		public void InternalOnPostScan()
		{
			if (AstarPath.active.data.pointGraph == null)
			{
				AstarPath.active.data.AddGraph(typeof(PointGraph));
			}
			this.startNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.StartTransform.position);
			this.startNode.link = this;
			this.endNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.EndTransform.position);
			this.endNode.link = this;
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink3.reference[this.startNode] = this;
			NodeLink3.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014FFC File Offset: 0x000131FC
		public override void OnGraphsPostUpdateBeforeAreaRecalculation()
		{
			if (!AstarPath.active.isScanning)
			{
				if (this.connectedNode1 != null && this.connectedNode1.Destroyed)
				{
					this.connectedNode1 = null;
				}
				if (this.connectedNode2 != null && this.connectedNode2.Destroyed)
				{
					this.connectedNode2 = null;
				}
				if (!this.postScanCalled)
				{
					this.OnPostScan();
					return;
				}
				this.Apply(false);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015063 File Offset: 0x00013263
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Application.isPlaying && AstarPath.active != null && AstarPath.active.data != null && AstarPath.active.data.pointGraph != null)
			{
				this.OnGraphsPostUpdate();
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000150A4 File Offset: 0x000132A4
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				NodeLink3.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				NodeLink3.reference.Remove(this.endNode);
			}
			if (this.startNode != null && this.endNode != null)
			{
				this.startNode.RemovePartialConnection(this.endNode);
				this.endNode.RemovePartialConnection(this.startNode);
				if (this.connectedNode1 != null && this.connectedNode2 != null)
				{
					this.startNode.RemovePartialConnection(this.connectedNode1);
					this.connectedNode1.RemovePartialConnection(this.startNode);
					this.endNode.RemovePartialConnection(this.connectedNode2);
					this.connectedNode2.RemovePartialConnection(this.endNode);
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00015176 File Offset: 0x00013376
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001517F File Offset: 0x0001337F
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			if (Application.isPlaying)
			{
				this.Apply(true);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015190 File Offset: 0x00013390
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			none.distanceMetric = DistanceMetric.ClosestAsSeenFromAboveSoft();
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			bool flag = true;
			NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
			flag &= nearest.node == this.connectedNode1 && nearest.node != null;
			this.connectedNode1 = nearest.node as MeshNode;
			this.clamped1 = nearest.position;
			if (this.connectedNode1 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode1.position, Vector3.up * 5f, Color.red);
			}
			NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
			flag &= nearest2.node == this.connectedNode2 && nearest2.node != null;
			this.connectedNode2 = nearest2.node as MeshNode;
			this.clamped2 = nearest2.position;
			if (this.connectedNode2 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode2.position, Vector3.up * 5f, Color.cyan);
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			if (flag && !forceNewCheck)
			{
				return;
			}
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint num = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			GraphNode.Connect(this.startNode, this.endNode, num, OffMeshLinks.Directionality.TwoWay);
			Int3 @int = this.connectedNode2.position - this.connectedNode1.position;
			for (int i = 0; i < this.connectedNode1.GetVertexCount(); i++)
			{
				Int3 vertex = this.connectedNode1.GetVertex(i);
				Int3 vertex2 = this.connectedNode1.GetVertex((i + 1) % this.connectedNode1.GetVertexCount());
				if (Int3.DotLong((vertex2 - vertex).Normal2D(), @int) <= 0L)
				{
					for (int j = 0; j < this.connectedNode2.GetVertexCount(); j++)
					{
						Int3 vertex3 = this.connectedNode2.GetVertex(j);
						Int3 vertex4 = this.connectedNode2.GetVertex((j + 1) % this.connectedNode2.GetVertexCount());
						if (Int3.DotLong((vertex4 - vertex3).Normal2D(), @int) >= 0L && (double)Int3.Angle(vertex4 - vertex3, vertex2 - vertex) > 2.967059810956319)
						{
							float num2 = 0f;
							float num3 = 1f;
							num3 = Math.Min(num3, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex3));
							num2 = Math.Max(num2, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex4));
							if (num3 >= num2)
							{
								Vector3 vector = (Vector3)(vertex2 - vertex) * num2 + (Vector3)vertex;
								Vector3 vector2 = (Vector3)(vertex2 - vertex) * num3 + (Vector3)vertex;
								this.startNode.portalA = vector;
								this.startNode.portalB = vector2;
								this.endNode.portalA = vector2;
								this.endNode.portalB = vector;
								GraphNode.Connect(this.connectedNode1, this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor), OffMeshLinks.Directionality.TwoWay);
								GraphNode.Connect(this.endNode, this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor), OffMeshLinks.Directionality.TwoWay);
								return;
							}
							Debug.LogError(string.Concat(new string[]
							{
								"Something went wrong! ",
								num2.ToString(),
								" ",
								num3.ToString(),
								" ",
								vertex,
								" ",
								vertex2,
								" ",
								vertex3,
								" ",
								vertex4,
								"\nTODO, how can this happen?"
							}));
						}
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015678 File Offset: 0x00013878
		public override void DrawGizmos()
		{
			bool flag = GizmoContext.InActiveSelection(this);
			Color color = (flag ? NodeLink3.GizmosColorSelected : NodeLink3.GizmosColor);
			if (this.StartTransform != null)
			{
				Draw.xz.Circle(this.StartTransform.position, 0.4f, color);
			}
			if (this.EndTransform != null)
			{
				Draw.xz.Circle(this.EndTransform.position, 0.4f, color);
			}
			if (this.StartTransform != null && this.EndTransform != null)
			{
				NodeLink.DrawArch(this.StartTransform.position, this.EndTransform.position, Vector3.up, color);
				if (flag)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, this.EndTransform.position - this.StartTransform.position).normalized;
					NodeLink.DrawArch(this.StartTransform.position + normalized * 0.1f, this.EndTransform.position + normalized * 0.1f, Vector3.up, color);
					NodeLink.DrawArch(this.StartTransform.position - normalized * 0.1f, this.EndTransform.position - normalized * 0.1f, Vector3.up, color);
				}
			}
		}

		// Token: 0x040002AA RID: 682
		protected static Dictionary<GraphNode, NodeLink3> reference = new Dictionary<GraphNode, NodeLink3>();

		// Token: 0x040002AB RID: 683
		public Transform end;

		// Token: 0x040002AC RID: 684
		public float costFactor = 1f;

		// Token: 0x040002AD RID: 685
		private NodeLink3Node startNode;

		// Token: 0x040002AE RID: 686
		private NodeLink3Node endNode;

		// Token: 0x040002AF RID: 687
		private MeshNode connectedNode1;

		// Token: 0x040002B0 RID: 688
		private MeshNode connectedNode2;

		// Token: 0x040002B1 RID: 689
		private Vector3 clamped1;

		// Token: 0x040002B2 RID: 690
		private Vector3 clamped2;

		// Token: 0x040002B3 RID: 691
		private bool postScanCalled;

		// Token: 0x040002B4 RID: 692
		private static readonly Color GizmosColor = new Color(0.80784315f, 0.53333336f, 0.1882353f, 0.5f);

		// Token: 0x040002B5 RID: 693
		private static readonly Color GizmosColorSelected = new Color(0.92156863f, 0.48235294f, 0.1254902f, 1f);
	}
}
