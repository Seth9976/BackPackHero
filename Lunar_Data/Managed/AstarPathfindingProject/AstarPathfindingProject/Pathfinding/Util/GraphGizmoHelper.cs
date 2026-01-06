using System;
using Pathfinding.Drawing;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000276 RID: 630
	public class GraphGizmoHelper : IAstarPooledObject, IDisposable
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0005C915 File Offset: 0x0005AB15
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0005C91D File Offset: 0x0005AB1D
		public DrawingData.Hasher hasher { get; private set; }

		// Token: 0x06000F03 RID: 3843 RVA: 0x0005C926 File Offset: 0x0005AB26
		public GraphGizmoHelper()
		{
			this.drawConnection = new Action<GraphNode>(this.DrawConnection);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0005C940 File Offset: 0x0005AB40
		public static GraphGizmoHelper GetSingleFrameGizmoHelper(DrawingData gizmos, AstarPath active, RedrawScope redrawScope)
		{
			return GraphGizmoHelper.GetGizmoHelper(gizmos, active, DrawingData.Hasher.NotSupplied, redrawScope);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0005C94F File Offset: 0x0005AB4F
		public static GraphGizmoHelper GetGizmoHelper(DrawingData gizmos, AstarPath active, DrawingData.Hasher hasher, RedrawScope redrawScope)
		{
			GraphGizmoHelper graphGizmoHelper = ObjectPool<GraphGizmoHelper>.Claim();
			graphGizmoHelper.Init(active, hasher, gizmos, redrawScope);
			return graphGizmoHelper;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0005C960 File Offset: 0x0005AB60
		public void Init(AstarPath active, DrawingData.Hasher hasher, DrawingData gizmos, RedrawScope redrawScope)
		{
			if (active != null)
			{
				this.debugData = active.debugPathData;
				this.debugPathID = active.debugPathID;
				this.debugMode = active.debugMode;
				this.debugFloor = active.debugFloor;
				this.debugRoof = active.debugRoof;
				this.nodeStorage = active.nodeStorage;
				this.showSearchTree = false;
			}
			this.hasher = hasher;
			this.builder = gizmos.GetBuilder(hasher, redrawScope, false);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0005C9DC File Offset: 0x0005ABDC
		public void OnEnterPool()
		{
			this.builder.Dispose();
			this.debugData = null;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		public void DrawConnections(GraphNode node)
		{
			if (!this.showSearchTree)
			{
				this.drawConnectionColor = this.NodeColor(node);
				this.drawConnectionStart = (Vector3)node.position;
				node.GetConnections(this.drawConnection, 32);
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0005CA26 File Offset: 0x0005AC26
		private void DrawConnection(GraphNode other)
		{
			this.builder.Line(this.drawConnectionStart, ((Vector3)other.position + this.drawConnectionStart) * 0.5f, this.drawConnectionColor);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0005CA60 File Offset: 0x0005AC60
		public Color NodeColor(GraphNode node)
		{
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
				case GraphDebugMode.NavmeshBorderObstacles:
					return AstarColor.GetTagColor((uint)node.HierarchicalNodeIndex);
				}
				color = AstarColor.SolidColor;
			}
			else
			{
				color = AstarColor.UnwalkableNode;
			}
			return color;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0005CB1B File Offset: 0x0005AD1B
		public void DrawWireTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
			this.builder.Line(a, b, color);
			this.builder.Line(b, c, color);
			this.builder.Line(c, a, color);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0005CB4C File Offset: 0x0005AD4C
		public void DrawTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			int[] array = ArrayPool<int>.Claim(numTriangles * 3);
			for (int i = 0; i < numTriangles * 3; i++)
			{
				array[i] = i;
			}
			this.builder.SolidMesh(vertices, array, colors, numTriangles * 3, numTriangles * 3);
			ArrayPool<int>.Release(ref array, false);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0005CB90 File Offset: 0x0005AD90
		public void DrawWireTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			for (int i = 0; i < numTriangles; i++)
			{
				this.DrawWireTriangle(vertices[i * 3], vertices[i * 3 + 1], vertices[i * 3 + 2], colors[i * 3]);
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0005CBD8 File Offset: 0x0005ADD8
		void IDisposable.Dispose()
		{
			GraphGizmoHelper graphGizmoHelper = this;
			ObjectPool<GraphGizmoHelper>.Release(ref graphGizmoHelper);
		}

		// Token: 0x04000B37 RID: 2871
		private PathHandler debugData;

		// Token: 0x04000B38 RID: 2872
		private ushort debugPathID;

		// Token: 0x04000B39 RID: 2873
		private GraphDebugMode debugMode;

		// Token: 0x04000B3A RID: 2874
		private bool showSearchTree;

		// Token: 0x04000B3B RID: 2875
		private float debugFloor;

		// Token: 0x04000B3C RID: 2876
		private float debugRoof;

		// Token: 0x04000B3D RID: 2877
		public CommandBuilder builder;

		// Token: 0x04000B3E RID: 2878
		private Vector3 drawConnectionStart;

		// Token: 0x04000B3F RID: 2879
		private Color drawConnectionColor;

		// Token: 0x04000B40 RID: 2880
		private readonly Action<GraphNode> drawConnection;

		// Token: 0x04000B41 RID: 2881
		private GlobalNodeStorage nodeStorage;
	}
}
