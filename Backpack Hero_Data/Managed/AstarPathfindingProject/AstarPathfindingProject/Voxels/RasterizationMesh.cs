using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000A3 RID: 163
	public class RasterizationMesh
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0002E415 File Offset: 0x0002C615
		public RasterizationMesh()
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002E420 File Offset: 0x0002C620
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds)
		{
			this.matrix = Matrix4x4.identity;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002E474 File Offset: 0x0002C674
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		public void RecalculateBounds()
		{
			Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
			for (int i = 1; i < this.numVertices; i++)
			{
				bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
			}
			this.bounds = bounds;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002E52A File Offset: 0x0002C72A
		public void Pool()
		{
			if (this.pool)
			{
				ArrayPool<int>.Release(ref this.triangles, false);
				ArrayPool<Vector3>.Release(ref this.vertices, false);
			}
		}

		// Token: 0x04000459 RID: 1113
		public MeshFilter original;

		// Token: 0x0400045A RID: 1114
		public int area;

		// Token: 0x0400045B RID: 1115
		public Vector3[] vertices;

		// Token: 0x0400045C RID: 1116
		public int[] triangles;

		// Token: 0x0400045D RID: 1117
		public int numVertices;

		// Token: 0x0400045E RID: 1118
		public int numTriangles;

		// Token: 0x0400045F RID: 1119
		public Bounds bounds;

		// Token: 0x04000460 RID: 1120
		public Matrix4x4 matrix;

		// Token: 0x04000461 RID: 1121
		public bool pool;
	}
}
