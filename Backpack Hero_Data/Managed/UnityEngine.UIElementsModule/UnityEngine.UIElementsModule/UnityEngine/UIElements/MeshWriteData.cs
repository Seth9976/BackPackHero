using System;
using Unity.Collections;

namespace UnityEngine.UIElements
{
	// Token: 0x02000257 RID: 599
	public class MeshWriteData
	{
		// Token: 0x06001209 RID: 4617 RVA: 0x00045F58 File Offset: 0x00044158
		internal MeshWriteData()
		{
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00045F64 File Offset: 0x00044164
		public int vertexCount
		{
			get
			{
				return this.m_Vertices.Length;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00045F84 File Offset: 0x00044184
		public int indexCount
		{
			get
			{
				return this.m_Indices.Length;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00045FA4 File Offset: 0x000441A4
		public Rect uvRegion
		{
			get
			{
				return this.m_UVRegion;
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00045FBC File Offset: 0x000441BC
		public void SetNextVertex(Vertex vertex)
		{
			int num = this.currentVertex;
			this.currentVertex = num + 1;
			this.m_Vertices[num] = vertex;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00045FE8 File Offset: 0x000441E8
		public void SetNextIndex(ushort index)
		{
			int num = this.currentIndex;
			this.currentIndex = num + 1;
			this.m_Indices[num] = index;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00046014 File Offset: 0x00044214
		public void SetAllVertices(Vertex[] vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0004605C File Offset: 0x0004425C
		public void SetAllVertices(NativeSlice<Vertex> vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000460A4 File Offset: 0x000442A4
		public void SetAllIndices(ushort[] indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000460EC File Offset: 0x000442EC
		public void SetAllIndices(NativeSlice<ushort> indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00046134 File Offset: 0x00044334
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = new Rect(0f, 0f, 1f, 1f);
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00046180 File Offset: 0x00044380
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Rect uvRegion)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = uvRegion;
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x04000819 RID: 2073
		internal NativeSlice<Vertex> m_Vertices;

		// Token: 0x0400081A RID: 2074
		internal NativeSlice<ushort> m_Indices;

		// Token: 0x0400081B RID: 2075
		internal Rect m_UVRegion;

		// Token: 0x0400081C RID: 2076
		internal int currentIndex;

		// Token: 0x0400081D RID: 2077
		internal int currentVertex;
	}
}
