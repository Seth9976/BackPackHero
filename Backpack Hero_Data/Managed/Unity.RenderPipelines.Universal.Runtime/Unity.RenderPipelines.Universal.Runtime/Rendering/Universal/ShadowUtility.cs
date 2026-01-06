using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.Universal.LibTessDotNet;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000038 RID: 56
	internal class ShadowUtility
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00011CFC File Offset: 0x0000FEFC
		private static ShadowUtility.Edge CreateEdge(int triangleIndexA, int triangleIndexB, List<Vector3> vertices, List<int> triangles)
		{
			ShadowUtility.Edge edge = default(ShadowUtility.Edge);
			edge.AssignVertexIndices(triangles[triangleIndexA], triangles[triangleIndexB]);
			Vector3 vector = vertices[edge.vertexIndex0];
			vector.z = 0f;
			Vector3 vector2 = vertices[edge.vertexIndex1];
			vector2.z = 0f;
			Vector3 vector3 = Vector3.Normalize(vector2 - vector);
			edge.tangent = Vector3.Cross(-Vector3.forward, vector3);
			return edge;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00011D84 File Offset: 0x0000FF84
		private static void PopulateEdgeArray(List<Vector3> vertices, List<int> triangles, List<ShadowUtility.Edge> edges)
		{
			for (int i = 0; i < triangles.Count; i += 3)
			{
				edges.Add(ShadowUtility.CreateEdge(i, i + 1, vertices, triangles));
				edges.Add(ShadowUtility.CreateEdge(i + 1, i + 2, vertices, triangles));
				edges.Add(ShadowUtility.CreateEdge(i + 2, i, vertices, triangles));
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		private static bool IsOutsideEdge(int edgeIndex, List<ShadowUtility.Edge> edgesToProcess)
		{
			int num = edgeIndex - 1;
			int num2 = edgeIndex + 1;
			int count = edgesToProcess.Count;
			ShadowUtility.Edge edge = edgesToProcess[edgeIndex];
			return (num < 0 || edge.CompareTo(edgesToProcess[edgeIndex - 1]) != 0) && (num2 >= count || edge.CompareTo(edgesToProcess[edgeIndex + 1]) != 0);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00011E2B File Offset: 0x0001002B
		private static void SortEdges(List<ShadowUtility.Edge> edgesToProcess)
		{
			edgesToProcess.Sort();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00011E34 File Offset: 0x00010034
		private static void CreateShadowTriangles(List<Vector3> vertices, List<Color> colors, List<int> triangles, List<Vector4> tangents, List<ShadowUtility.Edge> edges)
		{
			for (int i = 0; i < edges.Count; i++)
			{
				if (ShadowUtility.IsOutsideEdge(i, edges))
				{
					ShadowUtility.Edge edge = edges[i];
					tangents[edge.vertexIndex1] = -edge.tangent;
					int count = vertices.Count;
					vertices.Add(vertices[edge.vertexIndex0]);
					colors.Add(colors[edge.vertexIndex0]);
					tangents.Add(-edge.tangent);
					triangles.Add(edge.vertexIndex0);
					triangles.Add(count);
					triangles.Add(edge.vertexIndex1);
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00011EDE File Offset: 0x000100DE
		private static object InterpCustomVertexData(Vec3 position, object[] data, float[] weights)
		{
			return data[0];
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00011EE4 File Offset: 0x000100E4
		private static void InitializeTangents(int tangentsToAdd, List<Vector4> tangents)
		{
			for (int i = 0; i < tangentsToAdd; i++)
			{
				tangents.Add(Vector4.zero);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00011F08 File Offset: 0x00010108
		internal static Bounds CalculateLocalBounds(Vector3[] inVertices)
		{
			if (inVertices.Length == 0)
			{
				return new Bounds(Vector3.zero, Vector3.zero);
			}
			Vector2 vector = Vector2.positiveInfinity;
			Vector2 vector2 = Vector2.negativeInfinity;
			int num = inVertices.Length;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector3 = new Vector2(inVertices[i].x, inVertices[i].y);
				vector = Vector2.Min(vector, vector3);
				vector2 = Vector2.Max(vector2, vector3);
			}
			return new Bounds
			{
				max = vector2,
				min = vector
			};
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00011F9C File Offset: 0x0001019C
		public static Bounds GenerateShadowMesh(Mesh mesh, Vector3[] shapePath)
		{
			List<Vector3> list = new List<Vector3>();
			List<int> list2 = new List<int>();
			List<Vector4> list3 = new List<Vector4>();
			List<Color> list4 = new List<Color>();
			int num = shapePath.Length;
			ContourVertex[] array = new ContourVertex[2 * num];
			for (int j = 0; j < num; j++)
			{
				Color color = new Color(shapePath[j].x, shapePath[j].y, shapePath[j].x, shapePath[j].y);
				int num2 = (j + 1) % num;
				array[2 * j] = new ContourVertex
				{
					Position = new Vec3
					{
						X = shapePath[j].x,
						Y = shapePath[j].y,
						Z = 0f
					},
					Data = color
				};
				color = new Color(shapePath[j].x, shapePath[j].y, shapePath[num2].x, shapePath[num2].y);
				Vector2 vector = 0.5f * (shapePath[j] + shapePath[num2]);
				array[2 * j + 1] = new ContourVertex
				{
					Position = new Vec3
					{
						X = vector.x,
						Y = vector.y,
						Z = 0f
					},
					Data = color
				};
			}
			Tess tess = new Tess();
			tess.AddContour(array, ContourOrientation.Original);
			tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3, new CombineCallback(ShadowUtility.InterpCustomVertexData));
			int[] array2 = tess.Elements.Select((int i) => i).ToArray<int>();
			Vector3[] array3 = tess.Vertices.Select((ContourVertex v) => new Vector3(v.Position.X, v.Position.Y, 0f)).ToArray<Vector3>();
			Color[] array4 = tess.Vertices.Select((ContourVertex v) => new Color(((Color)v.Data).r, ((Color)v.Data).g, ((Color)v.Data).b, ((Color)v.Data).a)).ToArray<Color>();
			list.AddRange(array3);
			list2.AddRange(array2);
			list4.AddRange(array4);
			ShadowUtility.InitializeTangents(list.Count, list3);
			List<ShadowUtility.Edge> list5 = new List<ShadowUtility.Edge>();
			ShadowUtility.PopulateEdgeArray(list, list2, list5);
			ShadowUtility.SortEdges(list5);
			ShadowUtility.CreateShadowTriangles(list, list4, list2, list3, list5);
			Color[] array5 = list4.ToArray();
			Vector3[] array6 = list.ToArray();
			int[] array7 = list2.ToArray();
			Vector4[] array8 = list3.ToArray();
			mesh.Clear();
			mesh.vertices = array6;
			mesh.triangles = array7;
			mesh.tangents = array8;
			mesh.colors = array5;
			return ShadowUtility.CalculateLocalBounds(array6);
		}

		// Token: 0x0200014C RID: 332
		internal struct Edge : IComparable<ShadowUtility.Edge>
		{
			// Token: 0x0600094E RID: 2382 RVA: 0x0003E43A File Offset: 0x0003C63A
			public void AssignVertexIndices(int vi0, int vi1)
			{
				this.vertexIndex0 = vi0;
				this.vertexIndex1 = vi1;
				this.compareReversed = vi0 > vi1;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0003E454 File Offset: 0x0003C654
			public int Compare(ShadowUtility.Edge a, ShadowUtility.Edge b)
			{
				int num = (a.compareReversed ? a.vertexIndex1 : a.vertexIndex0);
				int num2 = (a.compareReversed ? a.vertexIndex0 : a.vertexIndex1);
				int num3 = (b.compareReversed ? b.vertexIndex1 : b.vertexIndex0);
				int num4 = (b.compareReversed ? b.vertexIndex0 : b.vertexIndex1);
				int num5 = num - num3;
				int num6 = num2 - num4;
				if (num5 == 0)
				{
					return num6;
				}
				return num5;
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0003E4CB File Offset: 0x0003C6CB
			public int CompareTo(ShadowUtility.Edge edgeToCompare)
			{
				return this.Compare(this, edgeToCompare);
			}

			// Token: 0x040008C5 RID: 2245
			public int vertexIndex0;

			// Token: 0x040008C6 RID: 2246
			public int vertexIndex1;

			// Token: 0x040008C7 RID: 2247
			public Vector4 tangent;

			// Token: 0x040008C8 RID: 2248
			private bool compareReversed;
		}
	}
}
