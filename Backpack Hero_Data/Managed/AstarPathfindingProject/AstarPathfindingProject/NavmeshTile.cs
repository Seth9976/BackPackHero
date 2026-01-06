using System;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x0200006D RID: 109
	public class NavmeshTile : INavmeshHolder, ITransformedGraph, INavmesh
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00022A1D File Offset: 0x00020C1D
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			x = this.x;
			z = this.z;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00022A2F File Offset: 0x00020C2F
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00022A38 File Offset: 0x00020C38
		public Int3 GetVertex(int index)
		{
			int num = index & 4095;
			return this.verts[num];
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00022A59 File Offset: 0x00020C59
		public Int3 GetVertexInGraphSpace(int index)
		{
			return this.vertsInGraphSpace[index & 4095];
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00022A6D File Offset: 0x00020C6D
		public GraphTransform transform
		{
			get
			{
				return this.graph.transform;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00022A7C File Offset: 0x00020C7C
		public void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x04000359 RID: 857
		public int[] tris;

		// Token: 0x0400035A RID: 858
		public Int3[] verts;

		// Token: 0x0400035B RID: 859
		public Int3[] vertsInGraphSpace;

		// Token: 0x0400035C RID: 860
		public int x;

		// Token: 0x0400035D RID: 861
		public int z;

		// Token: 0x0400035E RID: 862
		public int w;

		// Token: 0x0400035F RID: 863
		public int d;

		// Token: 0x04000360 RID: 864
		public TriangleMeshNode[] nodes;

		// Token: 0x04000361 RID: 865
		public BBTree bbTree;

		// Token: 0x04000362 RID: 866
		public bool flag;

		// Token: 0x04000363 RID: 867
		public NavmeshBase graph;
	}
}
