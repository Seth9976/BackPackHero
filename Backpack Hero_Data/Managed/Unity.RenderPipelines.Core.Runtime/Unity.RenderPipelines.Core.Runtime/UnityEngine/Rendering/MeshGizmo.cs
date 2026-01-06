using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AE RID: 174
	internal class MeshGizmo : IDisposable
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C068 File Offset: 0x0001A268
		public MeshGizmo(int capacity = 0)
		{
			this.vertices = new List<Vector3>(capacity);
			this.indices = new List<int>(capacity);
			this.colors = new List<Color>(capacity);
			this.mesh = new Mesh
			{
				indexFormat = IndexFormat.UInt32,
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C0B9 File Offset: 0x0001A2B9
		public void Clear()
		{
			this.vertices.Clear();
			this.indices.Clear();
			this.colors.Clear();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public void AddWireCube(Vector3 center, Vector3 size, Color color)
		{
			MeshGizmo.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.color = color;
			Vector3 vector = size / 2f;
			Vector3 vector2 = new Vector3(vector.x, vector.y, vector.z);
			Vector3 vector3 = new Vector3(-vector.x, vector.y, vector.z);
			Vector3 vector4 = new Vector3(-vector.x, -vector.y, vector.z);
			Vector3 vector5 = new Vector3(vector.x, -vector.y, vector.z);
			Vector3 vector6 = new Vector3(vector.x, vector.y, -vector.z);
			Vector3 vector7 = new Vector3(-vector.x, vector.y, -vector.z);
			Vector3 vector8 = new Vector3(-vector.x, -vector.y, -vector.z);
			Vector3 vector9 = new Vector3(vector.x, -vector.y, -vector.z);
			this.<AddWireCube>g__AddEdge|10_0(center + vector2, center + vector3, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector3, center + vector4, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector4, center + vector5, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector5, center + vector2, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector6, center + vector7, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector7, center + vector8, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector8, center + vector9, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector9, center + vector6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector2, center + vector6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector3, center + vector7, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector4, center + vector8, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + vector5, center + vector9, ref CS$<>8__locals1);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		private void DrawMesh(Matrix4x4 trs, Material mat, MeshTopology topology, CompareFunction depthTest, string gizmoName)
		{
			this.mesh.Clear();
			this.mesh.SetVertices(this.vertices);
			this.mesh.SetColors(this.colors);
			this.mesh.SetIndices(this.indices, topology, 0, true, 0);
			mat.SetFloat("_HandleZTest", (float)depthTest);
			CommandBuffer commandBuffer = CommandBufferPool.Get(gizmoName ?? "Mesh Gizmo Rendering");
			commandBuffer.DrawMesh(this.mesh, trs, mat, 0, 0);
			Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C376 File Offset: 0x0001A576
		public void RenderWireframe(Matrix4x4 trs, CompareFunction depthTest = CompareFunction.LessEqual, string gizmoName = null)
		{
			this.DrawMesh(trs, this.wireMaterial, MeshTopology.Lines, depthTest, gizmoName);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C388 File Offset: 0x0001A588
		public void Dispose()
		{
			CoreUtils.Destroy(this.mesh);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		[CompilerGenerated]
		private void <AddWireCube>g__AddEdge|10_0(Vector3 p1, Vector3 p2, ref MeshGizmo.<>c__DisplayClass10_0 A_3)
		{
			this.vertices.Add(p1);
			this.vertices.Add(p2);
			this.indices.Add(this.indices.Count);
			this.indices.Add(this.indices.Count);
			this.colors.Add(A_3.color);
			this.colors.Add(A_3.color);
		}

		// Token: 0x04000376 RID: 886
		public static readonly int vertexCountPerCube = 24;

		// Token: 0x04000377 RID: 887
		public Mesh mesh;

		// Token: 0x04000378 RID: 888
		private List<Vector3> vertices;

		// Token: 0x04000379 RID: 889
		private List<int> indices;

		// Token: 0x0400037A RID: 890
		private List<Color> colors;

		// Token: 0x0400037B RID: 891
		private Material wireMaterial;

		// Token: 0x0400037C RID: 892
		private Material dottedWireMaterial;

		// Token: 0x0400037D RID: 893
		private Material solidMaterial;
	}
}
