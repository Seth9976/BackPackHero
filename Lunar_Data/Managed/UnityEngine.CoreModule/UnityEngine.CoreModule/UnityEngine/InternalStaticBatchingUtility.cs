using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200023F RID: 575
	internal class InternalStaticBatchingUtility
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x000280B7 File Offset: 0x000262B7
		public static void CombineRoot(GameObject staticBatchRoot, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot, false, false, sorter);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000280C4 File Offset: 0x000262C4
		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			GameObject[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				GameObject gameObject = array2[i];
				bool flag = staticBatchRoot != null;
				if (!flag)
				{
					goto IL_0053;
				}
				bool flag2 = !gameObject.transform.IsChildOf(staticBatchRoot.transform);
				if (!flag2)
				{
					goto IL_0053;
				}
				IL_0075:
				i++;
				continue;
				IL_0053:
				bool flag3 = combineOnlyStatic && !gameObject.isStaticBatchable;
				if (flag3)
				{
					goto IL_0075;
				}
				list.Add(gameObject);
				goto IL_0075;
			}
			array = list.ToArray();
			InternalStaticBatchingUtility.CombineGameObjects(array, staticBatchRoot, isEditorPostprocessScene, sorter);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00028164 File Offset: 0x00026364
		private static uint GetMeshFormatHash(Mesh mesh)
		{
			bool flag = mesh == null;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				uint num2 = 1U;
				int vertexAttributeCount = mesh.vertexAttributeCount;
				for (int i = 0; i < vertexAttributeCount; i++)
				{
					VertexAttributeDescriptor vertexAttribute = mesh.GetVertexAttribute(i);
					uint num3 = (uint)(vertexAttribute.attribute | (VertexAttribute)((int)vertexAttribute.format << 4) | (VertexAttribute)((uint)vertexAttribute.dimension << 8));
					num2 = num2 * 2654435761U + num3;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000281DC File Offset: 0x000263DC
		private static GameObject[] SortGameObjectsForStaticBatching(GameObject[] gos, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			gos = Enumerable.ToArray<GameObject>(Enumerable.ThenBy<GameObject, long>(Enumerable.ThenBy<GameObject, uint>(Enumerable.ThenBy<GameObject, int>(Enumerable.ThenBy<GameObject, long>(Enumerable.OrderBy<GameObject, bool>(gos, (GameObject g) => InternalStaticBatchingUtility.StaticBatcherGOSorter.GetScaleFlip(g)), delegate(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetMaterialId(renderer);
			}), delegate(GameObject g)
			{
				Renderer renderer2 = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetLightmapIndex(renderer2);
			}), delegate(GameObject g)
			{
				Mesh mesh = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetMesh(g);
				return InternalStaticBatchingUtility.GetMeshFormatHash(mesh);
			}), delegate(GameObject g)
			{
				Renderer renderer3 = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetRendererId(renderer3);
			}));
			return gos;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00028280 File Offset: 0x00026480
		public static void CombineGameObjects(GameObject[] gos, GameObject staticBatchRoot, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			Matrix4x4 matrix4x = Matrix4x4.identity;
			Transform transform = null;
			bool flag = staticBatchRoot;
			if (flag)
			{
				matrix4x = staticBatchRoot.transform.worldToLocalMatrix;
				transform = staticBatchRoot.transform;
			}
			int num = 0;
			int num2 = 0;
			List<MeshSubsetCombineUtility.MeshContainer> list = new List<MeshSubsetCombineUtility.MeshContainer>();
			using (StaticBatchingUtility.s_SortMarker.Auto())
			{
				gos = InternalStaticBatchingUtility.SortGameObjectsForStaticBatching(gos, sorter ?? new InternalStaticBatchingUtility.StaticBatcherGOSorter());
			}
			uint num3 = 0U;
			bool flag2 = false;
			foreach (GameObject gameObject in gos)
			{
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				bool flag3 = meshFilter == null;
				if (!flag3)
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					bool flag4 = sharedMesh == null || (!isEditorPostprocessScene && !sharedMesh.canAccess);
					if (!flag4)
					{
						bool flag5 = !StaticBatchingHelper.IsMeshBatchable(sharedMesh);
						if (!flag5)
						{
							Renderer component = meshFilter.GetComponent<Renderer>();
							bool flag6 = component == null || !component.enabled;
							if (!flag6)
							{
								bool flag7 = component.staticBatchIndex != 0;
								if (!flag7)
								{
									Material[] array2 = component.sharedMaterials;
									bool flag8 = Enumerable.Any<Material>(array2, (Material m) => m != null && m.shader != null && m.shader.disableBatching > DisableBatchingType.False);
									if (!flag8)
									{
										int vertexCount = sharedMesh.vertexCount;
										bool flag9 = vertexCount == 0;
										if (!flag9)
										{
											MeshRenderer meshRenderer = component as MeshRenderer;
											bool flag10 = meshRenderer != null;
											if (flag10)
											{
												bool flag11 = meshRenderer.additionalVertexStreams != null;
												if (flag11)
												{
													bool flag12 = vertexCount != meshRenderer.additionalVertexStreams.vertexCount;
													if (flag12)
													{
														goto IL_0485;
													}
												}
												bool flag13 = meshRenderer.enlightenVertexStream != null;
												if (flag13)
												{
													bool flag14 = vertexCount != meshRenderer.enlightenVertexStream.vertexCount;
													if (flag14)
													{
														goto IL_0485;
													}
												}
											}
											uint meshFormatHash = InternalStaticBatchingUtility.GetMeshFormatHash(sharedMesh);
											bool scaleFlip = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetScaleFlip(gameObject);
											bool flag15 = num2 + vertexCount > 64000 || meshFormatHash != num3 || scaleFlip != flag2;
											if (flag15)
											{
												InternalStaticBatchingUtility.MakeBatch(list, transform, num++);
												list.Clear();
												num2 = 0;
												flag2 = scaleFlip;
											}
											num3 = meshFormatHash;
											MeshSubsetCombineUtility.MeshInstance meshInstance = default(MeshSubsetCombineUtility.MeshInstance);
											meshInstance.meshInstanceID = sharedMesh.GetInstanceID();
											meshInstance.rendererInstanceID = component.GetInstanceID();
											bool flag16 = meshRenderer != null;
											if (flag16)
											{
												bool flag17 = meshRenderer.additionalVertexStreams != null;
												if (flag17)
												{
													meshInstance.additionalVertexStreamsMeshInstanceID = meshRenderer.additionalVertexStreams.GetInstanceID();
												}
												bool flag18 = meshRenderer.enlightenVertexStream != null;
												if (flag18)
												{
													meshInstance.enlightenVertexStreamMeshInstanceID = meshRenderer.enlightenVertexStream.GetInstanceID();
												}
											}
											meshInstance.transform = matrix4x * meshFilter.transform.localToWorldMatrix;
											meshInstance.lightmapScaleOffset = component.lightmapScaleOffset;
											meshInstance.realtimeLightmapScaleOffset = component.realtimeLightmapScaleOffset;
											MeshSubsetCombineUtility.MeshContainer meshContainer = new MeshSubsetCombineUtility.MeshContainer
											{
												gameObject = gameObject,
												instance = meshInstance,
												subMeshInstances = new List<MeshSubsetCombineUtility.SubMeshInstance>()
											};
											list.Add(meshContainer);
											bool flag19 = array2.Length > sharedMesh.subMeshCount;
											if (flag19)
											{
												Debug.LogWarning(string.Concat(new string[]
												{
													"Mesh '",
													sharedMesh.name,
													"' has more materials (",
													array2.Length.ToString(),
													") than subsets (",
													sharedMesh.subMeshCount.ToString(),
													")"
												}), component);
												Material[] array3 = new Material[sharedMesh.subMeshCount];
												for (int j = 0; j < sharedMesh.subMeshCount; j++)
												{
													array3[j] = component.sharedMaterials[j];
												}
												component.sharedMaterials = array3;
												array2 = array3;
											}
											for (int k = 0; k < Math.Min(array2.Length, sharedMesh.subMeshCount); k++)
											{
												MeshSubsetCombineUtility.SubMeshInstance subMeshInstance = default(MeshSubsetCombineUtility.SubMeshInstance);
												subMeshInstance.meshInstanceID = meshFilter.sharedMesh.GetInstanceID();
												subMeshInstance.vertexOffset = num2;
												subMeshInstance.subMeshIndex = k;
												subMeshInstance.gameObjectInstanceID = gameObject.GetInstanceID();
												subMeshInstance.transform = meshInstance.transform;
												meshContainer.subMeshInstances.Add(subMeshInstance);
											}
											num2 += sharedMesh.vertexCount;
										}
									}
								}
							}
						}
					}
				}
				IL_0485:;
			}
			InternalStaticBatchingUtility.MakeBatch(list, transform, num);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00028740 File Offset: 0x00026940
		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshContainer> meshes, Transform staticBatchRootTransform, int batchIndex)
		{
			bool flag = meshes.Count < 2;
			if (!flag)
			{
				using (StaticBatchingUtility.s_MakeBatchMarker.Auto())
				{
					List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
					List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
					foreach (MeshSubsetCombineUtility.MeshContainer meshContainer in meshes)
					{
						list.Add(meshContainer.instance);
						list2.AddRange(meshContainer.subMeshInstances);
					}
					string text = "Combined Mesh";
					text = text + " (root: " + ((staticBatchRootTransform != null) ? staticBatchRootTransform.name : "scene") + ")";
					bool flag2 = batchIndex > 0;
					if (flag2)
					{
						text = text + " " + (batchIndex + 1).ToString();
					}
					Mesh mesh = StaticBatchingHelper.InternalCombineVertices(list.ToArray(), text);
					StaticBatchingHelper.InternalCombineIndices(list2.ToArray(), mesh);
					int num = 0;
					foreach (MeshSubsetCombineUtility.MeshContainer meshContainer2 in meshes)
					{
						MeshFilter meshFilter = (MeshFilter)meshContainer2.gameObject.GetComponent(typeof(MeshFilter));
						meshFilter.sharedMesh = mesh;
						int count = meshContainer2.subMeshInstances.Count;
						Renderer component = meshContainer2.gameObject.GetComponent<Renderer>();
						component.SetStaticBatchInfo(num, count);
						component.staticBatchRootTransform = staticBatchRootTransform;
						component.enabled = false;
						component.enabled = true;
						MeshRenderer meshRenderer = component as MeshRenderer;
						bool flag3 = meshRenderer != null;
						if (flag3)
						{
							meshRenderer.additionalVertexStreams = null;
							meshRenderer.enlightenVertexStream = null;
						}
						num += count;
					}
				}
			}
		}

		// Token: 0x04000849 RID: 2121
		private const int MaxVerticesInBatch = 64000;

		// Token: 0x0400084A RID: 2122
		private const string CombinedMeshPrefix = "Combined Mesh";

		// Token: 0x02000240 RID: 576
		public class StaticBatcherGOSorter
		{
			// Token: 0x060018A5 RID: 6309 RVA: 0x00028968 File Offset: 0x00026B68
			public virtual long GetMaterialId(Renderer renderer)
			{
				bool flag = renderer == null || renderer.sharedMaterial == null;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = (long)renderer.sharedMaterial.GetInstanceID();
				}
				return num;
			}

			// Token: 0x060018A6 RID: 6310 RVA: 0x000289A8 File Offset: 0x00026BA8
			public int GetLightmapIndex(Renderer renderer)
			{
				bool flag = renderer == null;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					num = renderer.lightmapIndex;
				}
				return num;
			}

			// Token: 0x060018A7 RID: 6311 RVA: 0x000289D0 File Offset: 0x00026BD0
			public static Renderer GetRenderer(GameObject go)
			{
				bool flag = go == null;
				Renderer renderer;
				if (flag)
				{
					renderer = null;
				}
				else
				{
					MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
					bool flag2 = meshFilter == null;
					if (flag2)
					{
						renderer = null;
					}
					else
					{
						renderer = meshFilter.GetComponent<Renderer>();
					}
				}
				return renderer;
			}

			// Token: 0x060018A8 RID: 6312 RVA: 0x00028A1C File Offset: 0x00026C1C
			public static Mesh GetMesh(GameObject go)
			{
				bool flag = go == null;
				Mesh mesh;
				if (flag)
				{
					mesh = null;
				}
				else
				{
					MeshFilter component = go.GetComponent<MeshFilter>();
					bool flag2 = component == null;
					if (flag2)
					{
						mesh = null;
					}
					else
					{
						mesh = component.sharedMesh;
					}
				}
				return mesh;
			}

			// Token: 0x060018A9 RID: 6313 RVA: 0x00028A5C File Offset: 0x00026C5C
			public virtual long GetRendererId(Renderer renderer)
			{
				bool flag = renderer == null;
				long num;
				if (flag)
				{
					num = -1L;
				}
				else
				{
					num = (long)renderer.GetInstanceID();
				}
				return num;
			}

			// Token: 0x060018AA RID: 6314 RVA: 0x00028A88 File Offset: 0x00026C88
			public static bool GetScaleFlip(GameObject go)
			{
				Transform transform = go.transform;
				float determinant = transform.localToWorldMatrix.determinant;
				return determinant < 0f;
			}
		}
	}
}
