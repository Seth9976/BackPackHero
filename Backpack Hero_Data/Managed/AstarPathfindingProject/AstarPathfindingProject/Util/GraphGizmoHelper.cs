using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000CD RID: 205
	public class GraphGizmoHelper : IAstarPooledObject, IDisposable
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0003AD1F File Offset: 0x00038F1F
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0003AD27 File Offset: 0x00038F27
		public RetainedGizmos.Hasher hasher { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003AD30 File Offset: 0x00038F30
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0003AD38 File Offset: 0x00038F38
		public RetainedGizmos.Builder builder { get; private set; }

		// Token: 0x060008B5 RID: 2229 RVA: 0x0003AD41 File Offset: 0x00038F41
		public GraphGizmoHelper()
		{
			this.drawConnection = new Action<GraphNode>(this.DrawConnection);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0003AD5C File Offset: 0x00038F5C
		public void Init(AstarPath active, RetainedGizmos.Hasher hasher, RetainedGizmos gizmos)
		{
			if (active != null)
			{
				this.debugData = active.debugPathData;
				this.debugPathID = active.debugPathID;
				this.debugMode = active.debugMode;
				this.debugFloor = active.debugFloor;
				this.debugRoof = active.debugRoof;
				this.showSearchTree = active.showSearchTree && this.debugData != null;
			}
			this.gizmos = gizmos;
			this.hasher = hasher;
			this.builder = ObjectPool<RetainedGizmos.Builder>.Claim();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0003ADE4 File Offset: 0x00038FE4
		public void OnEnterPool()
		{
			RetainedGizmos.Builder builder = this.builder;
			ObjectPool<RetainedGizmos.Builder>.Release(ref builder);
			this.builder = null;
			this.debugData = null;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0003AE10 File Offset: 0x00039010
		public void DrawConnections(GraphNode node)
		{
			if (this.showSearchTree)
			{
				if (GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID) && this.debugData.GetPathNode(node).parent != null)
				{
					this.builder.DrawLine((Vector3)node.position, (Vector3)this.debugData.GetPathNode(node).parent.node.position, this.NodeColor(node));
					return;
				}
			}
			else
			{
				this.drawConnectionColor = this.NodeColor(node);
				this.drawConnectionStart = (Vector3)node.position;
				node.GetConnections(this.drawConnection);
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0003AEB4 File Offset: 0x000390B4
		private void DrawConnection(GraphNode other)
		{
			this.builder.DrawLine(this.drawConnectionStart, Vector3.Lerp((Vector3)other.position, this.drawConnectionStart, 0.5f), this.drawConnectionColor);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003AEE8 File Offset: 0x000390E8
		public Color NodeColor(GraphNode node)
		{
			if (this.showSearchTree && !GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID))
			{
				return Color.clear;
			}
			Color color;
			if (node.Walkable)
			{
				switch (this.debugMode)
				{
				case GraphDebugMode.SolidColor:
					return AstarColor.SolidColor;
				case GraphDebugMode.Penalty:
					return Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (node.Penalty - this.debugFloor) / (this.debugRoof - this.debugFloor));
				case GraphDebugMode.Areas:
					return AstarColor.GetAreaColor(node.Area);
				case GraphDebugMode.Tags:
					return AstarColor.GetTagColor(node.Tag);
				case GraphDebugMode.HierarchicalNode:
					return AstarColor.GetTagColor((uint)node.HierarchicalNodeIndex);
				}
				if (this.debugData == null)
				{
					color = AstarColor.SolidColor;
				}
				else
				{
					PathNode pathNode = this.debugData.GetPathNode(node);
					float num;
					if (this.debugMode == GraphDebugMode.G)
					{
						num = pathNode.G;
					}
					else if (this.debugMode == GraphDebugMode.H)
					{
						num = pathNode.H;
					}
					else
					{
						num = pathNode.F;
					}
					color = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (num - this.debugFloor) / (this.debugRoof - this.debugFloor));
				}
			}
			else
			{
				color = AstarColor.UnwalkableNode;
			}
			return color;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0003B03B File Offset: 0x0003923B
		public static bool InSearchTree(GraphNode node, PathHandler handler, ushort pathID)
		{
			return handler.GetPathNode(node).pathID == pathID;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003B04C File Offset: 0x0003924C
		public void DrawWireTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
			this.builder.DrawLine(a, b, color);
			this.builder.DrawLine(b, c, color);
			this.builder.DrawLine(c, a, color);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0003B07C File Offset: 0x0003927C
		public void DrawTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			List<int> list = ListPool<int>.Claim(numTriangles);
			for (int i = 0; i < numTriangles * 3; i++)
			{
				list.Add(i);
			}
			this.builder.DrawMesh(this.gizmos, vertices, list, colors);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003B0C0 File Offset: 0x000392C0
		public void DrawWireTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			for (int i = 0; i < numTriangles; i++)
			{
				this.DrawWireTriangle(vertices[i * 3], vertices[i * 3 + 1], vertices[i * 3 + 2], colors[i * 3]);
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003B107 File Offset: 0x00039307
		public void Submit()
		{
			this.builder.Submit(this.gizmos, this.hasher);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003B120 File Offset: 0x00039320
		void IDisposable.Dispose()
		{
			GraphGizmoHelper graphGizmoHelper = this;
			this.Submit();
			ObjectPool<GraphGizmoHelper>.Release(ref graphGizmoHelper);
		}

		// Token: 0x04000509 RID: 1289
		private RetainedGizmos gizmos;

		// Token: 0x0400050A RID: 1290
		private PathHandler debugData;

		// Token: 0x0400050B RID: 1291
		private ushort debugPathID;

		// Token: 0x0400050C RID: 1292
		private GraphDebugMode debugMode;

		// Token: 0x0400050D RID: 1293
		private bool showSearchTree;

		// Token: 0x0400050E RID: 1294
		private float debugFloor;

		// Token: 0x0400050F RID: 1295
		private float debugRoof;

		// Token: 0x04000511 RID: 1297
		private Vector3 drawConnectionStart;

		// Token: 0x04000512 RID: 1298
		private Color drawConnectionColor;

		// Token: 0x04000513 RID: 1299
		private readonly Action<GraphNode> drawConnection;
	}
}
