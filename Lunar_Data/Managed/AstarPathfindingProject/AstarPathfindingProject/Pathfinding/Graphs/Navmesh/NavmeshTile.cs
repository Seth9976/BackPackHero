using System;
using Pathfinding.Util;
using Unity.Collections;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001B2 RID: 434
	public class NavmeshTile : INavmeshHolder, ITransformedGraph, INavmesh
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00040F03 File Offset: 0x0003F103
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			x = this.x;
			z = this.z;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00022406 File Offset: 0x00020606
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00040F18 File Offset: 0x0003F118
		public unsafe Int3 GetVertex(int index)
		{
			int num = index & 4095;
			return *this.verts[num];
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00040F3E File Offset: 0x0003F13E
		public unsafe Int3 GetVertexInGraphSpace(int index)
		{
			return *this.vertsInGraphSpace[index & 4095];
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00040F57 File Offset: 0x0003F157
		public GraphTransform transform
		{
			get
			{
				return this.graph.transform;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00040F64 File Offset: 0x0003F164
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

		// Token: 0x06000B7E RID: 2942 RVA: 0x00040F9B File Offset: 0x0003F19B
		public void Dispose()
		{
			this.bbTree.Dispose();
			this.vertsInGraphSpace.Free(Allocator.Persistent);
			this.verts.Free(Allocator.Persistent);
			this.tris.Free(Allocator.Persistent);
		}

		// Token: 0x040007DF RID: 2015
		public UnsafeSpan<Int3> vertsInGraphSpace;

		// Token: 0x040007E0 RID: 2016
		public UnsafeSpan<Int3> verts;

		// Token: 0x040007E1 RID: 2017
		public UnsafeSpan<int> tris;

		// Token: 0x040007E2 RID: 2018
		public int x;

		// Token: 0x040007E3 RID: 2019
		public int z;

		// Token: 0x040007E4 RID: 2020
		public int w;

		// Token: 0x040007E5 RID: 2021
		public int d;

		// Token: 0x040007E6 RID: 2022
		public TriangleMeshNode[] nodes;

		// Token: 0x040007E7 RID: 2023
		public BBTree bbTree;

		// Token: 0x040007E8 RID: 2024
		public bool flag;

		// Token: 0x040007E9 RID: 2025
		public NavmeshBase graph;
	}
}
