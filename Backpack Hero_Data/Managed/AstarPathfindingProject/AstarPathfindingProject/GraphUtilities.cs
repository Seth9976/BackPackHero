using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200003C RID: 60
	public static class GraphUtilities
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000F35C File Offset: 0x0000D55C
		public static List<Vector3> GetContours(NavGraph graph)
		{
			List<Vector3> result = ListPool<Vector3>.Claim();
			if (graph is INavmesh)
			{
				GraphUtilities.GetContours(graph as INavmesh, delegate(List<Int3> vertices, bool cycle)
				{
					int num = (cycle ? (vertices.Count - 1) : 0);
					for (int i = 0; i < vertices.Count; i++)
					{
						result.Add((Vector3)vertices[num]);
						result.Add((Vector3)vertices[i]);
						num = i;
					}
				});
			}
			else if (graph is GridGraph)
			{
				GraphUtilities.GetContours(graph as GridGraph, delegate(Vector3[] vertices)
				{
					int num2 = vertices.Length - 1;
					for (int j = 0; j < vertices.Length; j++)
					{
						result.Add(vertices[num2]);
						result.Add(vertices[j]);
						num2 = j;
					}
				}, 0f, null);
			}
			return result;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public static void GetContours(INavmesh navmesh, Action<List<Int3>, bool> results)
		{
			bool[] uses = new bool[3];
			Dictionary<int, int> outline = new Dictionary<int, int>();
			Dictionary<int, Int3> vertexPositions = new Dictionary<int, Int3>();
			HashSet<int> hasInEdge = new HashSet<int>();
			navmesh.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				uses[0] = (uses[1] = (uses[2] = false));
				if (triangleMeshNode != null)
				{
					for (int i = 0; i < triangleMeshNode.connections.Length; i++)
					{
						Connection connection = triangleMeshNode.connections[i];
						if (connection.shapeEdge != 255)
						{
							uses[(int)connection.shapeEdge] = true;
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (!uses[j])
						{
							int num = j;
							int num2 = (j + 1) % triangleMeshNode.GetVertexCount();
							outline[triangleMeshNode.GetVertexIndex(num)] = triangleMeshNode.GetVertexIndex(num2);
							hasInEdge.Add(triangleMeshNode.GetVertexIndex(num2));
							vertexPositions[triangleMeshNode.GetVertexIndex(num)] = triangleMeshNode.GetVertex(num);
							vertexPositions[triangleMeshNode.GetVertexIndex(num2)] = triangleMeshNode.GetVertex(num2);
						}
					}
				}
			});
			Polygon.TraceContours(outline, hasInEdge, delegate(List<int> chain, bool cycle)
			{
				List<Int3> list = ListPool<Int3>.Claim();
				for (int k = 0; k < chain.Count; k++)
				{
					list.Add(vertexPositions[chain[k]]);
				}
				results(list, cycle);
			});
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000F440 File Offset: 0x0000D640
		public static void GetContours(GridGraph grid, Action<Vector3[]> callback, float yMergeThreshold, GridNodeBase[] nodes = null)
		{
			HashSet<GridNodeBase> hashSet = ((nodes != null) ? new HashSet<GridNodeBase>(nodes) : null);
			LayerGridGraph layerGridGraph = grid as LayerGridGraph;
			if (layerGridGraph != null)
			{
				nodes = nodes ?? layerGridGraph.nodes;
			}
			nodes = nodes ?? grid.nodes;
			int[] neighbourXOffsets = grid.neighbourXOffsets;
			int[] neighbourZOffsets = grid.neighbourZOffsets;
			int[] array;
			if (grid.neighbours != NumNeighbours.Six)
			{
				RuntimeHelpers.InitializeArray(array = new int[4], fieldof(<PrivateImplementationDetails>.BAED642339816AFFB3FE8719792D0E4CE82F12DB72B7373D244EAA65445800FE).FieldHandle);
			}
			else
			{
				array = GridGraph.hexagonNeighbourIndices;
			}
			int[] array2 = array;
			float num = ((grid.neighbours == NumNeighbours.Six) ? 0.33333334f : 0.5f);
			if (nodes != null)
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				HashSet<int> hashSet2 = new HashSet<int>();
				foreach (GridNodeBase gridNodeBase in nodes)
				{
					if (gridNodeBase != null && gridNodeBase.Walkable && (!gridNodeBase.HasConnectionsToAllEightNeighbours || hashSet != null))
					{
						for (int j = 0; j < array2.Length; j++)
						{
							int num2 = (gridNodeBase.NodeIndex << 4) | j;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(array2[j]);
							if ((neighbourAlongDirection == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection))) && !hashSet2.Contains(num2))
							{
								list.ClearFast<Vector3>();
								int num3 = j;
								GridNodeBase gridNodeBase2 = gridNodeBase;
								for (;;)
								{
									int num4 = (gridNodeBase2.NodeIndex << 4) | num3;
									if (num4 == num2 && list.Count > 0)
									{
										break;
									}
									hashSet2.Add(num4);
									GridNodeBase neighbourAlongDirection2 = gridNodeBase2.GetNeighbourAlongDirection(array2[num3]);
									if (neighbourAlongDirection2 == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection2)))
									{
										int num5 = array2[num3];
										num3 = (num3 + 1) % array2.Length;
										int num6 = array2[num3];
										Vector3 vector = new Vector3((float)gridNodeBase2.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase2.ZCoordinateInGrid + 0.5f);
										vector.x += (float)(neighbourXOffsets[num5] + neighbourXOffsets[num6]) * num;
										vector.z += (float)(neighbourZOffsets[num5] + neighbourZOffsets[num6]) * num;
										vector.y = grid.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
										if (list.Count >= 2)
										{
											Vector3 vector2 = list[list.Count - 2];
											Vector3 vector3 = list[list.Count - 1] - vector2;
											Vector3 vector4 = vector - vector2;
											if (((Mathf.Abs(vector3.x) > 0.01f || Mathf.Abs(vector4.x) > 0.01f) && (Mathf.Abs(vector3.z) > 0.01f || Mathf.Abs(vector4.z) > 0.01f)) || Mathf.Abs(vector3.y) > yMergeThreshold || Mathf.Abs(vector4.y) > yMergeThreshold)
											{
												list.Add(vector);
											}
											else
											{
												list[list.Count - 1] = vector;
											}
										}
										else
										{
											list.Add(vector);
										}
									}
									else
									{
										gridNodeBase2 = neighbourAlongDirection2;
										num3 = (num3 + array2.Length / 2 + 1) % array2.Length;
									}
								}
								if (list.Count >= 3)
								{
									Vector3 vector5 = list[list.Count - 2];
									Vector3 vector6 = list[list.Count - 1] - vector5;
									Vector3 vector7 = list[0] - vector5;
									if (((Mathf.Abs(vector6.x) <= 0.01f && Mathf.Abs(vector7.x) <= 0.01f) || (Mathf.Abs(vector6.z) <= 0.01f && Mathf.Abs(vector7.z) <= 0.01f)) && Mathf.Abs(vector6.y) <= yMergeThreshold && Mathf.Abs(vector7.y) <= yMergeThreshold)
									{
										list.RemoveAt(list.Count - 1);
									}
								}
								if (list.Count >= 3)
								{
									Vector3 vector8 = list[list.Count - 1];
									Vector3 vector9 = list[0] - vector8;
									Vector3 vector10 = list[1] - vector8;
									if (((Mathf.Abs(vector9.x) <= 0.01f && Mathf.Abs(vector10.x) <= 0.01f) || (Mathf.Abs(vector9.z) <= 0.01f && Mathf.Abs(vector10.z) <= 0.01f)) && Mathf.Abs(vector9.y) <= yMergeThreshold && Mathf.Abs(vector10.y) <= yMergeThreshold)
									{
										list.RemoveAt(0);
									}
								}
								Vector3[] array3 = list.ToArray();
								grid.transform.Transform(array3);
								callback(array3);
							}
						}
					}
				}
				ListPool<Vector3>.Release(ref list);
			}
		}
	}
}
