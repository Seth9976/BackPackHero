using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Graphs.Navmesh.Voxelization.Burst;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001B4 RID: 436
	[BurstCompile]
	public class RecastMeshGatherer
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x00040FE0 File Offset: 0x0003F1E0
		public RecastMeshGatherer(Scene scene, Bounds bounds, int terrainDownsamplingFactor, LayerMask mask, List<string> tagMask, float maxColliderApproximationError)
		{
			terrainDownsamplingFactor = Math.Max(terrainDownsamplingFactor, 1);
			this.bounds = bounds;
			this.terrainDownsamplingFactor = terrainDownsamplingFactor;
			this.mask = mask;
			this.tagMask = tagMask ?? new List<string>();
			this.maxColliderApproximationError = maxColliderApproximationError;
			this.scene = scene;
			this.meshes = ListPool<RecastMeshGatherer.GatheredMesh>.Claim();
			this.vertexBuffers = ListPool<NativeArray<Vector3>>.Claim();
			this.triangleBuffers = ListPool<NativeArray<int>>.Claim();
			this.cachedMeshes = ObjectPoolSimple<Dictionary<RecastMeshGatherer.MeshCacheItem, int>>.Claim();
			this.meshData = ListPool<Mesh>.Claim();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0004107F File Offset: 0x0003F27F
		[BurstCompile]
		private static void CalculateBounds(ref UnsafeSpan<float3> vertices, ref float4x4 localToWorldMatrix, out Bounds bounds)
		{
			RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Invoke(ref vertices, ref localToWorldMatrix, out bounds);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004108C File Offset: 0x0003F28C
		public RecastMeshGatherer.MeshCollection Finalize()
		{
			Mesh.MeshDataArray meshDataArray = Mesh.AcquireReadOnlyMeshData(this.meshData);
			NativeArray<RasterizationMesh> nativeArray = new NativeArray<RasterizationMesh>(this.meshes.Count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int count = this.vertexBuffers.Count;
			for (int i = 0; i < meshDataArray.Length; i++)
			{
				NativeArray<Vector3> nativeArray2;
				NativeArray<int> nativeArray3;
				MeshUtility.GetMeshData(meshDataArray, i, out nativeArray2, out nativeArray3);
				this.vertexBuffers.Add(nativeArray2);
				this.triangleBuffers.Add(nativeArray3);
			}
			for (int j = 0; j < nativeArray.Length; j++)
			{
				RecastMeshGatherer.GatheredMesh gatheredMesh = this.meshes[j];
				int num;
				if (gatheredMesh.meshDataIndex >= 0)
				{
					num = count + gatheredMesh.meshDataIndex;
				}
				else
				{
					num = -(gatheredMesh.meshDataIndex + 1);
				}
				Bounds bounds = gatheredMesh.bounds;
				UnsafeSpan<float3> unsafeSpan = this.vertexBuffers[num].Reinterpret<float3>().AsUnsafeReadOnlySpan<float3>();
				if (bounds == default(Bounds))
				{
					float4x4 float4x = gatheredMesh.matrix;
					RecastMeshGatherer.CalculateBounds(ref unsafeSpan, ref float4x, out bounds);
				}
				NativeArray<int> nativeArray4 = this.triangleBuffers[num];
				nativeArray[j] = new RasterizationMesh
				{
					vertices = unsafeSpan,
					triangles = nativeArray4.AsUnsafeSpan<int>().Slice(gatheredMesh.indexStart, ((gatheredMesh.indexEnd != -1) ? gatheredMesh.indexEnd : nativeArray4.Length) - gatheredMesh.indexStart),
					area = gatheredMesh.area,
					areaIsTag = gatheredMesh.areaIsTag,
					bounds = bounds,
					matrix = gatheredMesh.matrix,
					solid = gatheredMesh.solid,
					doubleSided = gatheredMesh.doubleSided,
					flatten = gatheredMesh.flatten
				};
			}
			this.cachedMeshes.Clear();
			ObjectPoolSimple<Dictionary<RecastMeshGatherer.MeshCacheItem, int>>.Release(ref this.cachedMeshes);
			ListPool<RecastMeshGatherer.GatheredMesh>.Release(ref this.meshes);
			meshDataArray.Dispose();
			return new RecastMeshGatherer.MeshCollection(this.vertexBuffers, this.triangleBuffers, nativeArray);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004129E File Offset: 0x0003F49E
		private int AddMeshBuffers(Vector3[] vertices, int[] triangles)
		{
			return this.AddMeshBuffers(new NativeArray<Vector3>(vertices, Allocator.Persistent), new NativeArray<int>(triangles, Allocator.Persistent));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000412B4 File Offset: 0x0003F4B4
		private int AddMeshBuffers(NativeArray<Vector3> vertices, NativeArray<int> triangles)
		{
			int num = -this.vertexBuffers.Count - 1;
			this.vertexBuffers.Add(vertices);
			this.triangleBuffers.Add(triangles);
			return num;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000412DC File Offset: 0x0003F4DC
		private bool MeshFilterShouldBeIncluded(MeshFilter filter)
		{
			Renderer renderer;
			RecastMeshObj recastMeshObj;
			return filter.TryGetComponent<Renderer>(out renderer) && filter.sharedMesh != null && renderer.enabled && (((1 << filter.gameObject.layer) & this.mask) != 0 || this.tagMask.Contains(filter.tag)) && (!filter.TryGetComponent<RecastMeshObj>(out recastMeshObj) || !recastMeshObj.enabled);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00041350 File Offset: 0x0003F550
		private void AddNewMesh(Renderer renderer, Mesh mesh, int area, int submeshStart, int submeshCount, bool solid = false, bool areaIsTag = false)
		{
			if (!mesh.HasVertexAttribute(VertexAttribute.Position))
			{
				return;
			}
			if (!mesh.isReadable)
			{
				if (!this.anyNonReadableMesh)
				{
					Debug.LogError("Some meshes could not be included when scanning the graph because they are marked as not readable. This includes the mesh '" + mesh.name + "'. You need to mark the mesh with read/write enabled in the mesh importer. Alternatively you can only rasterize colliders and not meshes. Mesh Collider meshes still need to be readable.", mesh);
				}
				this.anyNonReadableMesh = true;
				return;
			}
			int num = 0;
			int num2 = -1;
			if (submeshStart > 0 || submeshCount < mesh.subMeshCount)
			{
				SubMeshDescriptor subMesh = mesh.GetSubMesh(submeshStart);
				SubMeshDescriptor subMesh2 = mesh.GetSubMesh(submeshStart + submeshCount - 1);
				num = subMesh.indexStart;
				num2 = subMesh2.indexStart + subMesh2.indexCount;
			}
			int count;
			if (!this.cachedMeshes.TryGetValue(new RecastMeshGatherer.MeshCacheItem(mesh), out count))
			{
				count = this.meshData.Count;
				this.meshData.Add(mesh);
				this.cachedMeshes[new RecastMeshGatherer.MeshCacheItem(mesh)] = count;
			}
			this.meshes.Add(new RecastMeshGatherer.GatheredMesh
			{
				meshDataIndex = count,
				bounds = renderer.bounds,
				indexStart = num,
				indexEnd = num2,
				areaIsTag = areaIsTag,
				area = area,
				solid = solid,
				matrix = renderer.localToWorldMatrix,
				doubleSided = false,
				flatten = false
			});
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00041490 File Offset: 0x0003F690
		private RecastMeshGatherer.GatheredMesh? GetColliderMesh(MeshCollider collider, Matrix4x4 localToWorldMatrix)
		{
			if (!(collider.sharedMesh != null))
			{
				return null;
			}
			Mesh sharedMesh = collider.sharedMesh;
			if (!sharedMesh.HasVertexAttribute(VertexAttribute.Position))
			{
				return null;
			}
			if (!sharedMesh.isReadable)
			{
				if (!this.anyNonReadableMesh)
				{
					Debug.LogError("Some mesh collider meshes could not be included when scanning the graph because they are marked as not readable. This includes the mesh '" + sharedMesh.name + "'. You need to mark the mesh with read/write enabled in the mesh importer.", sharedMesh);
				}
				this.anyNonReadableMesh = true;
				return null;
			}
			int count;
			if (!this.cachedMeshes.TryGetValue(new RecastMeshGatherer.MeshCacheItem(sharedMesh), out count))
			{
				count = this.meshData.Count;
				this.meshData.Add(sharedMesh);
				this.cachedMeshes[new RecastMeshGatherer.MeshCacheItem(sharedMesh)] = count;
			}
			return new RecastMeshGatherer.GatheredMesh?(new RecastMeshGatherer.GatheredMesh
			{
				meshDataIndex = count,
				bounds = collider.bounds,
				areaIsTag = false,
				area = 0,
				indexStart = 0,
				indexEnd = -1,
				solid = collider.convex,
				matrix = localToWorldMatrix,
				doubleSided = false,
				flatten = false
			});
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000415B8 File Offset: 0x0003F7B8
		public void CollectSceneMeshes()
		{
			if (this.tagMask.Count > 0 || this.mask != 0)
			{
				MeshFilter[] array = UnityCompatibility.FindObjectsByTypeSorted<MeshFilter>();
				bool flag = false;
				List<Material> list = ListPool<Material>.Claim();
				foreach (MeshFilter meshFilter in array)
				{
					if (this.MeshFilterShouldBeIncluded(meshFilter))
					{
						Renderer renderer;
						meshFilter.TryGetComponent<Renderer>(out renderer);
						if (renderer.isPartOfStaticBatch)
						{
							flag = true;
						}
						else if (renderer.bounds.Intersects(this.bounds))
						{
							renderer.GetSharedMaterials(list);
							Renderer renderer2 = renderer;
							Mesh sharedMesh = meshFilter.sharedMesh;
							int num = 0;
							MeshRenderer meshRenderer = renderer as MeshRenderer;
							this.AddNewMesh(renderer2, sharedMesh, num, (meshRenderer != null) ? meshRenderer.subMeshStartIndex : 0, list.Count, false, false);
						}
					}
				}
				if (flag)
				{
					Debug.LogWarning("Some meshes were statically batched. These meshes can not be used for navmesh calculation due to technical constraints.\nDuring runtime scripts cannot access the data of meshes which have been statically batched.\nOne way to solve this problem is to use cached startup (Save & Load tab in the inspector) to only calculate the graph when the game is not playing.");
				}
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00041684 File Offset: 0x0003F884
		private static int RecastAreaFromRecastMeshObj(RecastMeshObj obj)
		{
			switch (obj.mode)
			{
			default:
				return -1;
			case RecastMeshObj.Mode.WalkableSurface:
				return 0;
			case RecastMeshObj.Mode.WalkableSurfaceWithSeam:
			case RecastMeshObj.Mode.WalkableSurfaceWithTag:
				return obj.surfaceID;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000416BC File Offset: 0x0003F8BC
		public void CollectRecastMeshObjs()
		{
			List<RecastMeshObj> list = ListPool<RecastMeshObj>.Claim();
			RecastMeshObj.GetAllInBounds(list, this.bounds);
			for (int i = 0; i < list.Count; i++)
			{
				this.AddRecastMeshObj(list[i]);
			}
			ListPool<RecastMeshObj>.Release(ref list);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00041700 File Offset: 0x0003F900
		private void AddRecastMeshObj(RecastMeshObj recastMeshObj)
		{
			if (recastMeshObj.includeInScan == RecastMeshObj.ScanInclusion.AlwaysExclude)
			{
				return;
			}
			if (recastMeshObj.includeInScan == RecastMeshObj.ScanInclusion.Auto && ((this.mask >> recastMeshObj.gameObject.layer) & 1) == 0 && !this.tagMask.Contains(recastMeshObj.tag))
			{
				return;
			}
			MeshFilter meshFilter;
			Collider collider;
			Collider2D collider2D;
			recastMeshObj.ResolveMeshSource(out meshFilter, out collider, out collider2D);
			if (meshFilter != null)
			{
				Mesh sharedMesh = meshFilter.sharedMesh;
				MeshRenderer meshRenderer;
				if (meshFilter.TryGetComponent<MeshRenderer>(out meshRenderer) && sharedMesh != null)
				{
					this.AddNewMesh(meshRenderer, meshFilter.sharedMesh, RecastMeshGatherer.RecastAreaFromRecastMeshObj(recastMeshObj), meshRenderer.subMeshStartIndex, meshRenderer.sharedMaterials.Length, recastMeshObj.solid, recastMeshObj.mode == RecastMeshObj.Mode.WalkableSurfaceWithTag);
					return;
				}
			}
			else if (collider != null)
			{
				RecastMeshGatherer.GatheredMesh? colliderMesh = this.GetColliderMesh(collider);
				if (colliderMesh != null)
				{
					RecastMeshGatherer.GatheredMesh valueOrDefault = colliderMesh.GetValueOrDefault();
					valueOrDefault.area = RecastMeshGatherer.RecastAreaFromRecastMeshObj(recastMeshObj);
					valueOrDefault.areaIsTag = recastMeshObj.mode == RecastMeshObj.Mode.WalkableSurfaceWithTag;
					valueOrDefault.solid |= recastMeshObj.solid;
					this.meshes.Add(valueOrDefault);
					return;
				}
			}
			else if (!(collider2D != null))
			{
				if (recastMeshObj.geometrySource == RecastMeshObj.GeometrySource.Auto)
				{
					Debug.LogError("Couldn't get geometry source for RecastMeshObject (" + recastMeshObj.gameObject.name + "). It didn't have a collider or MeshFilter+Renderer attached", recastMeshObj.gameObject);
					return;
				}
				Debug.LogError(string.Concat(new string[]
				{
					"Couldn't get geometry source for RecastMeshObject (",
					recastMeshObj.gameObject.name,
					"). It didn't have a ",
					recastMeshObj.geometrySource.ToString(),
					" attached"
				}), recastMeshObj.gameObject);
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000418A4 File Offset: 0x0003FAA4
		public void CollectTerrainMeshes(bool rasterizeTrees, float desiredChunkSize)
		{
			Terrain[] activeTerrains = Terrain.activeTerrains;
			if (activeTerrains.Length != 0)
			{
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					if (!(activeTerrains[i].terrainData == null))
					{
						this.GenerateTerrainChunks(activeTerrains[i], this.bounds, desiredChunkSize);
						if (rasterizeTrees)
						{
							this.CollectTreeMeshes(activeTerrains[i]);
						}
					}
				}
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000418F8 File Offset: 0x0003FAF8
		private void GenerateTerrainChunks(Terrain terrain, Bounds bounds, float desiredChunkSize)
		{
			TerrainData terrainData = terrain.terrainData;
			if (terrainData == null)
			{
				throw new ArgumentException("Terrain contains no terrain data");
			}
			Vector3 position = terrain.GetPosition();
			Vector3 vector = position + terrainData.size * 0.5f;
			Bounds bounds2 = new Bounds(vector, terrainData.size);
			if (!bounds2.Intersects(bounds))
			{
				return;
			}
			int heightmapResolution = terrainData.heightmapResolution;
			int heightmapResolution2 = terrainData.heightmapResolution;
			Vector3 heightmapScale = terrainData.heightmapScale;
			heightmapScale.y = terrainData.size.y;
			int num = Mathf.CeilToInt(Mathf.Max(desiredChunkSize / (heightmapScale.x * (float)this.terrainDownsamplingFactor), 12f)) * this.terrainDownsamplingFactor;
			int num2 = Mathf.CeilToInt(Mathf.Max(desiredChunkSize / (heightmapScale.z * (float)this.terrainDownsamplingFactor), 12f)) * this.terrainDownsamplingFactor;
			num = Mathf.Min(num, heightmapResolution);
			num2 = Mathf.Min(num2, heightmapResolution2);
			float num3 = (float)num * heightmapScale.x;
			float num4 = (float)num2 * heightmapScale.z;
			IntRect intRect = new IntRect(0, 0, heightmapResolution / num, heightmapResolution2 / num2);
			IntRect intRect2 = (float.IsFinite(bounds.size.x) ? new IntRect(Mathf.FloorToInt((bounds.min.x - position.x) / num3), Mathf.FloorToInt((bounds.min.z - position.z) / num4), Mathf.FloorToInt((bounds.max.x - position.x) / num3), Mathf.FloorToInt((bounds.max.z - position.z) / num4)) : intRect);
			intRect2 = IntRect.Intersection(intRect2, intRect);
			if (!intRect2.IsValid())
			{
				return;
			}
			IntRect intRect3 = new IntRect(intRect2.xmin * num, intRect2.ymin * num2, Mathf.Min(heightmapResolution, (intRect2.xmax + 1) * num) - 1, Mathf.Min(heightmapResolution2, (intRect2.ymax + 1) * num2) - 1);
			float[,] heights = terrainData.GetHeights(intRect3.xmin, intRect3.ymin, intRect3.Width, intRect3.Height);
			bool[,] holes = terrainData.GetHoles(intRect3.xmin, intRect3.ymin, intRect3.Width - 1, intRect3.Height - 1);
			Vector3 vector2 = position + new Vector3((float)(intRect2.xmin * num) * heightmapScale.x, 0f, (float)(intRect2.ymin * num2) * heightmapScale.z);
			for (int i = intRect2.ymin; i <= intRect2.ymax; i++)
			{
				for (int j = intRect2.xmin; j <= intRect2.xmax; j++)
				{
					RecastMeshGatherer.GatheredMesh gatheredMesh = this.GenerateHeightmapChunk(heights, holes, heightmapScale, vector2, (j - intRect2.xmin) * num, (i - intRect2.ymin) * num2, num, num2, this.terrainDownsamplingFactor);
					this.meshes.Add(gatheredMesh);
				}
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00041BF7 File Offset: 0x0003FDF7
		private static int CeilDivision(int lhs, int rhs)
		{
			return (lhs + rhs - 1) / rhs;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00041C00 File Offset: 0x0003FE00
		private unsafe RecastMeshGatherer.GatheredMesh GenerateHeightmapChunk(float[,] heights, bool[,] holes, Vector3 sampleSize, Vector3 offset, int x0, int z0, int width, int depth, int stride)
		{
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			int num = RecastMeshGatherer.CeilDivision(Mathf.Min(width, length2 - x0), stride) + 1;
			int num2 = RecastMeshGatherer.CeilDivision(Mathf.Min(depth, length - z0), stride) + 1;
			int num3 = num * num2;
			NativeArray<Vector3> nativeArray = new NativeArray<Vector3>(num3, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			int num4 = (num - 1) * (num2 - 1) * 2 * 3;
			NativeArray<int> nativeArray2 = new NativeArray<int>(num4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			UnsafeSpan<Vector3> unsafeSpan = nativeArray.AsUnsafeSpan<Vector3>();
			for (int i = 0; i < num2; i++)
			{
				int num5 = Math.Min(z0 + i * stride, length - 1);
				for (int j = 0; j < num; j++)
				{
					int num6 = Math.Min(x0 + j * stride, length2 - 1);
					*unsafeSpan[i * num + j] = new Vector3((float)num6 * sampleSize.x, heights[num5, num6] * sampleSize.y, (float)num5 * sampleSize.z) + offset;
				}
			}
			int num7 = 0;
			UnsafeSpan<int> unsafeSpan2 = nativeArray2.AsUnsafeSpan<int>();
			for (int k = 0; k < num2 - 1; k++)
			{
				for (int l = 0; l < num - 1; l++)
				{
					int num8 = Math.Min(x0 + stride / 2 + l * stride, length2 - 2);
					int num9 = Math.Min(z0 + stride / 2 + k * stride, length - 2);
					if (holes[num9, num8])
					{
						*unsafeSpan2[num7] = k * num + l;
						*unsafeSpan2[num7 + 1] = (k + 1) * num + l + 1;
						*unsafeSpan2[num7 + 2] = k * num + l + 1;
						num7 += 3;
						*unsafeSpan2[num7] = k * num + l;
						*unsafeSpan2[num7 + 1] = (k + 1) * num + l;
						*unsafeSpan2[num7 + 2] = (k + 1) * num + l + 1;
						num7 += 3;
					}
				}
			}
			int num10 = this.AddMeshBuffers(nativeArray, nativeArray2);
			return new RecastMeshGatherer.GatheredMesh
			{
				meshDataIndex = num10,
				bounds = default(Bounds),
				indexStart = 0,
				indexEnd = num7,
				areaIsTag = false,
				area = 0,
				solid = false,
				matrix = Matrix4x4.identity,
				doubleSided = false,
				flatten = false
			};
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00041E74 File Offset: 0x00040074
		private void CollectTreeMeshes(Terrain terrain)
		{
			TerrainData terrainData = terrain.terrainData;
			TreeInstance[] treeInstances = terrainData.treeInstances;
			TreePrototype[] treePrototypes = terrainData.treePrototypes;
			foreach (TreeInstance treeInstance in treeInstances)
			{
				TreePrototype treePrototype = treePrototypes[treeInstance.prototypeIndex];
				if (!(treePrototype.prefab == null))
				{
					RecastMeshGatherer.TreeInfo treeInfo;
					if (!this.cachedTreePrefabs.TryGetValue(treePrototype.prefab, out treeInfo))
					{
						treeInfo.submeshes = new List<RecastMeshGatherer.GatheredMesh>();
						LODGroup lodgroup;
						treeInfo.supportsRotation = treePrototype.prefab.TryGetComponent<LODGroup>(out lodgroup);
						List<Collider> list = ListPool<Collider>.Claim();
						Matrix4x4 inverse = treePrototype.prefab.transform.localToWorldMatrix.inverse;
						treePrototype.prefab.GetComponentsInChildren<Collider>(false, list);
						for (int j = 0; j < list.Count; j++)
						{
							RecastMeshGatherer.GatheredMesh? colliderMesh = this.GetColliderMesh(list[j], inverse * list[j].transform.localToWorldMatrix);
							if (colliderMesh != null)
							{
								RecastMeshGatherer.GatheredMesh valueOrDefault = colliderMesh.GetValueOrDefault();
								RecastMeshObj recastMeshObj;
								if (list[j].gameObject.TryGetComponent<RecastMeshObj>(out recastMeshObj) && recastMeshObj.enabled)
								{
									if (recastMeshObj.includeInScan == RecastMeshObj.ScanInclusion.AlwaysExclude)
									{
										goto IL_0163;
									}
									valueOrDefault.area = RecastMeshGatherer.RecastAreaFromRecastMeshObj(recastMeshObj);
									valueOrDefault.solid |= recastMeshObj.solid;
									valueOrDefault.areaIsTag = recastMeshObj.mode == RecastMeshObj.Mode.WalkableSurfaceWithTag;
								}
								valueOrDefault.RecalculateBounds();
								treeInfo.submeshes.Add(valueOrDefault);
							}
							IL_0163:;
						}
						ListPool<Collider>.Release(ref list);
						this.cachedTreePrefabs[treePrototype.prefab] = treeInfo;
					}
					Vector3 vector = terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size);
					Vector3 vector2 = Vector3.Scale(new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale), treePrototype.prefab.transform.localScale);
					float num = (treeInfo.supportsRotation ? treeInstance.rotation : 0f);
					Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.AngleAxis(num * 57.29578f, Vector3.up), vector2);
					for (int k = 0; k < treeInfo.submeshes.Count; k++)
					{
						RecastMeshGatherer.GatheredMesh gatheredMesh = treeInfo.submeshes[k];
						gatheredMesh.matrix = matrix4x * gatheredMesh.matrix;
						this.meshes.Add(gatheredMesh);
					}
				}
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000420F8 File Offset: 0x000402F8
		private bool ShouldIncludeCollider(Collider collider)
		{
			RecastMeshObj recastMeshObj;
			if (!collider.enabled || collider.isTrigger || !collider.bounds.Intersects(this.bounds) || (collider.TryGetComponent<RecastMeshObj>(out recastMeshObj) && recastMeshObj.enabled))
			{
				return false;
			}
			GameObject gameObject = collider.gameObject;
			if (((this.mask >> gameObject.layer) & 1) != 0)
			{
				return true;
			}
			for (int i = 0; i < this.tagMask.Count; i++)
			{
				if (gameObject.CompareTag(this.tagMask[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00042190 File Offset: 0x00040390
		public void CollectColliderMeshes()
		{
			if (this.tagMask.Count == 0 && this.mask == 0)
			{
				return;
			}
			PhysicsScene physicsScene = this.scene.GetPhysicsScene();
			int num = 256;
			Collider[] array = null;
			bool flag = math.all(math.isfinite(this.bounds.extents));
			if (!flag)
			{
				array = UnityCompatibility.FindObjectsByTypeSorted<Collider>();
				num = array.Length;
			}
			else
			{
				do
				{
					if (array != null)
					{
						ArrayPool<Collider>.Release(ref array, false);
					}
					array = ArrayPool<Collider>.Claim(num * 4);
					num = physicsScene.OverlapBox(this.bounds.center, this.bounds.extents, array, Quaternion.identity, -1, QueryTriggerInteraction.Ignore);
				}
				while (num == array.Length);
			}
			for (int i = 0; i < num; i++)
			{
				Collider collider = array[i];
				if (this.ShouldIncludeCollider(collider))
				{
					RecastMeshGatherer.GatheredMesh? colliderMesh = this.GetColliderMesh(collider);
					if (colliderMesh != null)
					{
						RecastMeshGatherer.GatheredMesh valueOrDefault = colliderMesh.GetValueOrDefault();
						this.meshes.Add(valueOrDefault);
					}
				}
			}
			if (flag)
			{
				ArrayPool<Collider>.Release(ref array, false);
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00042296 File Offset: 0x00040496
		private RecastMeshGatherer.GatheredMesh? GetColliderMesh(Collider col)
		{
			return this.GetColliderMesh(col, col.transform.localToWorldMatrix);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000422AC File Offset: 0x000404AC
		private RecastMeshGatherer.GatheredMesh? GetColliderMesh(Collider col, Matrix4x4 localToWorldMatrix)
		{
			BoxCollider boxCollider = col as BoxCollider;
			if (boxCollider != null)
			{
				return new RecastMeshGatherer.GatheredMesh?(this.RasterizeBoxCollider(boxCollider, localToWorldMatrix));
			}
			if (col is SphereCollider || col is CapsuleCollider)
			{
				SphereCollider sphereCollider = col as SphereCollider;
				CapsuleCollider capsuleCollider = col as CapsuleCollider;
				float num = ((sphereCollider != null) ? sphereCollider.radius : capsuleCollider.radius);
				float num2 = ((sphereCollider != null) ? 0f : (capsuleCollider.height * 0.5f / num - 1f));
				Quaternion quaternion = Quaternion.identity;
				if (capsuleCollider != null)
				{
					quaternion = Quaternion.Euler((float)((capsuleCollider.direction == 2) ? 90 : 0), 0f, (float)((capsuleCollider.direction == 0) ? 90 : 0));
				}
				Matrix4x4 matrix4x = Matrix4x4.TRS((sphereCollider != null) ? sphereCollider.center : capsuleCollider.center, quaternion, Vector3.one * num);
				matrix4x = localToWorldMatrix * matrix4x;
				return new RecastMeshGatherer.GatheredMesh?(this.RasterizeCapsuleCollider(num, num2, col.bounds, matrix4x));
			}
			MeshCollider meshCollider = col as MeshCollider;
			if (meshCollider != null)
			{
				return this.GetColliderMesh(meshCollider, localToWorldMatrix);
			}
			return null;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000423D8 File Offset: 0x000405D8
		private RecastMeshGatherer.GatheredMesh RasterizeBoxCollider(BoxCollider collider, Matrix4x4 localToWorldMatrix)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(collider.center, Quaternion.identity, collider.size * 0.5f);
			matrix4x = localToWorldMatrix * matrix4x;
			int num;
			if (!this.cachedMeshes.TryGetValue(RecastMeshGatherer.MeshCacheItem.Box, out num))
			{
				num = this.AddMeshBuffers(RecastMeshGatherer.BoxColliderVerts, RecastMeshGatherer.BoxColliderTris);
				this.cachedMeshes[RecastMeshGatherer.MeshCacheItem.Box] = num;
			}
			return new RecastMeshGatherer.GatheredMesh
			{
				meshDataIndex = num,
				bounds = collider.bounds,
				indexStart = 0,
				indexEnd = -1,
				areaIsTag = false,
				area = 0,
				solid = true,
				matrix = matrix4x,
				doubleSided = false,
				flatten = false
			};
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000424A4 File Offset: 0x000406A4
		private static int CircleSteps(Matrix4x4 matrix, float radius, float maxError)
		{
			float num = math.sqrt(math.max(math.max(math.lengthsq(matrix.GetColumn(0)), math.lengthsq(matrix.GetColumn(1))), math.lengthsq(matrix.GetColumn(2))));
			float num2 = radius * num;
			float num3 = 1f - maxError / num2;
			if (num3 >= 0f)
			{
				return (int)math.ceil(3.1415927f / math.acos(num3));
			}
			return 3;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00042530 File Offset: 0x00040730
		private static float CircleRadiusAdjustmentFactor(int steps)
		{
			return 0.5f * (1f - math.cos(6.2831855f / (float)steps));
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004254C File Offset: 0x0004074C
		private RecastMeshGatherer.GatheredMesh RasterizeCapsuleCollider(float radius, float height, Bounds bounds, Matrix4x4 localToWorldMatrix)
		{
			int num = RecastMeshGatherer.CircleSteps(localToWorldMatrix, radius, this.maxColliderApproximationError);
			int num2 = num;
			RecastMeshGatherer.MeshCacheItem meshCacheItem = new RecastMeshGatherer.MeshCacheItem
			{
				type = RecastMeshGatherer.MeshType.Capsule,
				mesh = null,
				rows = num,
				quantizedHeight = Mathf.RoundToInt(height / this.maxColliderApproximationError)
			};
			int num3;
			if (!this.cachedMeshes.TryGetValue(meshCacheItem, out num3))
			{
				NativeArray<Vector3> nativeArray = new NativeArray<Vector3>(num * num2 + 2, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				NativeArray<int> nativeArray2 = new NativeArray<int>(num * num2 * 2 * 3, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						nativeArray[j + i * num2] = new Vector3(Mathf.Cos((float)j * 3.1415927f * 2f / (float)num2) * Mathf.Sin((float)i * 3.1415927f / (float)(num - 1)), Mathf.Cos((float)i * 3.1415927f / (float)(num - 1)) + ((i < num / 2) ? height : (-height)), Mathf.Sin((float)j * 3.1415927f * 2f / (float)num2) * Mathf.Sin((float)i * 3.1415927f / (float)(num - 1)));
					}
				}
				nativeArray[nativeArray.Length - 1] = Vector3.up;
				nativeArray[nativeArray.Length - 2] = Vector3.down;
				int num4 = 0;
				int k = 0;
				int num5 = num2 - 1;
				while (k < num2)
				{
					nativeArray2[num4] = nativeArray.Length - 1;
					nativeArray2[num4 + 1] = num5;
					nativeArray2[num4 + 2] = k;
					num4 += 3;
					num5 = k++;
				}
				for (int l = 1; l < num; l++)
				{
					int m = 0;
					int num6 = num2 - 1;
					while (m < num2)
					{
						nativeArray2[num4] = l * num2 + m;
						nativeArray2[num4 + 1] = l * num2 + num6;
						nativeArray2[num4 + 2] = (l - 1) * num2 + m;
						num4 += 3;
						nativeArray2[num4] = (l - 1) * num2 + num6;
						nativeArray2[num4 + 1] = (l - 1) * num2 + m;
						nativeArray2[num4 + 2] = l * num2 + num6;
						num4 += 3;
						num6 = m++;
					}
				}
				int n = 0;
				int num7 = num2 - 1;
				while (n < num2)
				{
					nativeArray2[num4] = nativeArray.Length - 2;
					nativeArray2[num4 + 1] = (num - 1) * num2 + num7;
					nativeArray2[num4 + 2] = (num - 1) * num2 + n;
					num4 += 3;
					num7 = n++;
				}
				num3 = this.AddMeshBuffers(nativeArray, nativeArray2);
				this.cachedMeshes[meshCacheItem] = num3;
			}
			return new RecastMeshGatherer.GatheredMesh
			{
				meshDataIndex = num3,
				bounds = bounds,
				areaIsTag = false,
				area = 0,
				indexStart = 0,
				indexEnd = -1,
				solid = true,
				matrix = localToWorldMatrix,
				doubleSided = false,
				flatten = false
			};
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00042878 File Offset: 0x00040A78
		private bool ShouldIncludeCollider2D(Collider2D collider)
		{
			if (((this.mask >> collider.gameObject.layer) & 1) != 0)
			{
				return true;
			}
			RecastMeshObj recastMeshObj;
			if ((collider.attachedRigidbody ?? collider).TryGetComponent<RecastMeshObj>(out recastMeshObj) && recastMeshObj.enabled && recastMeshObj.includeInScan == RecastMeshObj.ScanInclusion.AlwaysInclude)
			{
				return true;
			}
			for (int i = 0; i < this.tagMask.Count; i++)
			{
				if (collider.CompareTag(this.tagMask[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000428FC File Offset: 0x00040AFC
		public void Collect2DColliderMeshes()
		{
			if (this.tagMask.Count == 0 && this.mask == 0)
			{
				return;
			}
			PhysicsScene2D physicsScene2D = this.scene.GetPhysicsScene2D();
			int num = 256;
			Collider2D[] array = null;
			bool flag = math.isfinite(this.bounds.extents.x) && math.isfinite(this.bounds.extents.y);
			if (!flag)
			{
				array = UnityCompatibility.FindObjectsByTypeSorted<Collider2D>();
				num = array.Length;
			}
			else
			{
				Vector2 vector = this.bounds.min;
				Vector2 vector2 = this.bounds.max;
				ContactFilter2D contactFilter2D = default(ContactFilter2D).NoFilter();
				contactFilter2D.useTriggers = false;
				do
				{
					if (array != null)
					{
						ArrayPool<Collider2D>.Release(ref array, false);
					}
					array = ArrayPool<Collider2D>.Claim(num * 4);
					num = physicsScene2D.OverlapArea(vector, vector2, contactFilter2D, array);
				}
				while (num == array.Length);
			}
			for (int i = 0; i < num; i++)
			{
				if (!this.ShouldIncludeCollider2D(array[i]))
				{
					array[i] = null;
				}
			}
			NativeArray<float3> nativeArray;
			NativeArray<int> nativeArray2;
			NativeArray<ColliderMeshBuilder2D.ShapeMesh> nativeArray3;
			int num2 = ColliderMeshBuilder2D.GenerateMeshesFromColliders(array, num, this.maxColliderApproximationError, out nativeArray, out nativeArray2, out nativeArray3);
			int num3 = this.AddMeshBuffers(nativeArray.Reinterpret<Vector3>(), nativeArray2);
			for (int j = 0; j < num2; j++)
			{
				ColliderMeshBuilder2D.ShapeMesh shapeMesh = nativeArray3[j];
				if (this.bounds.Intersects(shapeMesh.bounds))
				{
					Collider2D collider2D = array[shapeMesh.tag];
					RecastMeshObj recastMeshObj;
					(collider2D.attachedRigidbody ?? collider2D).TryGetComponent<RecastMeshObj>(out recastMeshObj);
					int num4 = -1;
					bool flag2 = false;
					if (recastMeshObj != null)
					{
						if (recastMeshObj.includeInScan == RecastMeshObj.ScanInclusion.AlwaysExclude)
						{
							goto IL_022A;
						}
						num4 = RecastMeshGatherer.RecastAreaFromRecastMeshObj(recastMeshObj);
						flag2 = recastMeshObj.mode == RecastMeshObj.Mode.WalkableSurfaceWithTag;
					}
					this.meshes.Add(new RecastMeshGatherer.GatheredMesh
					{
						meshDataIndex = num3,
						bounds = shapeMesh.bounds,
						indexStart = shapeMesh.startIndex,
						indexEnd = shapeMesh.endIndex,
						areaIsTag = flag2,
						area = num4,
						solid = false,
						matrix = shapeMesh.matrix,
						doubleSided = true,
						flatten = true
					});
				}
				IL_022A:;
			}
			if (flag)
			{
				ArrayPool<Collider2D>.Release(ref array, false);
			}
			nativeArray3.Dispose();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00042C5C File Offset: 0x00040E5C
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static void CalculateBounds$BurstManaged(ref UnsafeSpan<float3> vertices, ref float4x4 localToWorldMatrix, out Bounds bounds)
		{
			if (vertices.Length == 0)
			{
				bounds = default(Bounds);
				return;
			}
			float3 @float = float.NegativeInfinity;
			float3 float2 = float.PositiveInfinity;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)vertices.Length))
			{
				float3 float3 = math.transform(localToWorldMatrix, *vertices[num]);
				@float = math.max(@float, float3);
				float2 = math.min(float2, float3);
				num += 1U;
			}
			bounds = new Bounds((float2 + @float) * 0.5f, @float - float2);
		}

		// Token: 0x040007EA RID: 2026
		private readonly int terrainDownsamplingFactor;

		// Token: 0x040007EB RID: 2027
		private readonly LayerMask mask;

		// Token: 0x040007EC RID: 2028
		private readonly List<string> tagMask;

		// Token: 0x040007ED RID: 2029
		private readonly float maxColliderApproximationError;

		// Token: 0x040007EE RID: 2030
		private readonly Bounds bounds;

		// Token: 0x040007EF RID: 2031
		private readonly Scene scene;

		// Token: 0x040007F0 RID: 2032
		private Dictionary<RecastMeshGatherer.MeshCacheItem, int> cachedMeshes = new Dictionary<RecastMeshGatherer.MeshCacheItem, int>();

		// Token: 0x040007F1 RID: 2033
		private readonly Dictionary<GameObject, RecastMeshGatherer.TreeInfo> cachedTreePrefabs = new Dictionary<GameObject, RecastMeshGatherer.TreeInfo>();

		// Token: 0x040007F2 RID: 2034
		private readonly List<NativeArray<Vector3>> vertexBuffers;

		// Token: 0x040007F3 RID: 2035
		private readonly List<NativeArray<int>> triangleBuffers;

		// Token: 0x040007F4 RID: 2036
		private readonly List<Mesh> meshData;

		// Token: 0x040007F5 RID: 2037
		private bool anyNonReadableMesh;

		// Token: 0x040007F6 RID: 2038
		private List<RecastMeshGatherer.GatheredMesh> meshes;

		// Token: 0x040007F7 RID: 2039
		private static readonly int[] BoxColliderTris = new int[]
		{
			0, 1, 2, 0, 2, 3, 6, 5, 4, 7,
			6, 4, 0, 5, 1, 0, 4, 5, 1, 6,
			2, 1, 5, 6, 2, 7, 3, 2, 6, 7,
			3, 4, 0, 3, 7, 4
		};

		// Token: 0x040007F8 RID: 2040
		private static readonly Vector3[] BoxColliderVerts = new Vector3[]
		{
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f)
		};

		// Token: 0x020001B5 RID: 437
		private struct TreeInfo
		{
			// Token: 0x040007F9 RID: 2041
			public List<RecastMeshGatherer.GatheredMesh> submeshes;

			// Token: 0x040007FA RID: 2042
			public bool supportsRotation;
		}

		// Token: 0x020001B6 RID: 438
		public struct MeshCollection : IArenaDisposable
		{
			// Token: 0x06000B9F RID: 2975 RVA: 0x00042CF7 File Offset: 0x00040EF7
			public MeshCollection(List<NativeArray<Vector3>> vertexBuffers, List<NativeArray<int>> triangleBuffers, NativeArray<RasterizationMesh> meshes)
			{
				this.vertexBuffers = vertexBuffers;
				this.triangleBuffers = triangleBuffers;
				this.meshes = meshes;
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x00042D10 File Offset: 0x00040F10
			void IArenaDisposable.DisposeWith(DisposeArena arena)
			{
				for (int i = 0; i < this.vertexBuffers.Count; i++)
				{
					arena.Add<Vector3>(this.vertexBuffers[i]);
					arena.Add<int>(this.triangleBuffers[i]);
				}
				arena.Add<RasterizationMesh>(this.meshes);
			}

			// Token: 0x040007FB RID: 2043
			private List<NativeArray<Vector3>> vertexBuffers;

			// Token: 0x040007FC RID: 2044
			private List<NativeArray<int>> triangleBuffers;

			// Token: 0x040007FD RID: 2045
			public NativeArray<RasterizationMesh> meshes;
		}

		// Token: 0x020001B7 RID: 439
		public struct GatheredMesh
		{
			// Token: 0x06000BA1 RID: 2977 RVA: 0x00042D63 File Offset: 0x00040F63
			public void RecalculateBounds()
			{
				this.bounds = default(Bounds);
			}

			// Token: 0x040007FE RID: 2046
			public int meshDataIndex;

			// Token: 0x040007FF RID: 2047
			public bool areaIsTag;

			// Token: 0x04000800 RID: 2048
			public int area;

			// Token: 0x04000801 RID: 2049
			public int indexStart;

			// Token: 0x04000802 RID: 2050
			public int indexEnd;

			// Token: 0x04000803 RID: 2051
			public Bounds bounds;

			// Token: 0x04000804 RID: 2052
			public Matrix4x4 matrix;

			// Token: 0x04000805 RID: 2053
			public bool solid;

			// Token: 0x04000806 RID: 2054
			public bool doubleSided;

			// Token: 0x04000807 RID: 2055
			public bool flatten;
		}

		// Token: 0x020001B8 RID: 440
		private enum MeshType
		{
			// Token: 0x04000809 RID: 2057
			Mesh,
			// Token: 0x0400080A RID: 2058
			Box,
			// Token: 0x0400080B RID: 2059
			Capsule
		}

		// Token: 0x020001B9 RID: 441
		private struct MeshCacheItem : IEquatable<RecastMeshGatherer.MeshCacheItem>
		{
			// Token: 0x06000BA2 RID: 2978 RVA: 0x00042D71 File Offset: 0x00040F71
			public MeshCacheItem(Mesh mesh)
			{
				this.type = RecastMeshGatherer.MeshType.Mesh;
				this.mesh = mesh;
				this.rows = 0;
				this.quantizedHeight = 0;
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x00042D90 File Offset: 0x00040F90
			public bool Equals(RecastMeshGatherer.MeshCacheItem other)
			{
				return this.type == other.type && this.mesh == other.mesh && this.rows == other.rows && this.quantizedHeight == other.quantizedHeight;
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x00042DDC File Offset: 0x00040FDC
			public override int GetHashCode()
			{
				return (int)((((((this.type * (RecastMeshGatherer.MeshType)31) ^ (RecastMeshGatherer.MeshType)((this.mesh != null) ? this.mesh.GetHashCode() : (-1))) * (RecastMeshGatherer.MeshType)31) ^ (RecastMeshGatherer.MeshType)this.rows) * (RecastMeshGatherer.MeshType)31) ^ (RecastMeshGatherer.MeshType)this.quantizedHeight);
			}

			// Token: 0x0400080C RID: 2060
			public RecastMeshGatherer.MeshType type;

			// Token: 0x0400080D RID: 2061
			public Mesh mesh;

			// Token: 0x0400080E RID: 2062
			public int rows;

			// Token: 0x0400080F RID: 2063
			public int quantizedHeight;

			// Token: 0x04000810 RID: 2064
			public static readonly RecastMeshGatherer.MeshCacheItem Box = new RecastMeshGatherer.MeshCacheItem
			{
				type = RecastMeshGatherer.MeshType.Box,
				mesh = null,
				rows = 0,
				quantizedHeight = 0
			};
		}

		// Token: 0x020001BA RID: 442
		// (Invoke) Token: 0x06000BA7 RID: 2983
		public delegate void CalculateBounds_00000A9B$PostfixBurstDelegate(ref UnsafeSpan<float3> vertices, ref float4x4 localToWorldMatrix, out Bounds bounds);

		// Token: 0x020001BB RID: 443
		internal static class CalculateBounds_00000A9B$BurstDirectCall
		{
			// Token: 0x06000BAA RID: 2986 RVA: 0x00042E53 File Offset: 0x00041053
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Pointer == 0)
				{
					RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.DeferredCompilation, methodof(RecastMeshGatherer.CalculateBounds$BurstManaged(ref UnsafeSpan<float3>, ref float4x4, ref Bounds)).MethodHandle, typeof(RecastMeshGatherer.CalculateBounds_00000A9B$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Pointer;
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00042E80 File Offset: 0x00041080
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00042E98 File Offset: 0x00041098
			public static void Constructor()
			{
				RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(RecastMeshGatherer.CalculateBounds(ref UnsafeSpan<float3>, ref float4x4, ref Bounds)).MethodHandle);
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x00042EA9 File Offset: 0x000410A9
			// Note: this type is marked as 'beforefieldinit'.
			static CalculateBounds_00000A9B$BurstDirectCall()
			{
				RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.Constructor();
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x00042EB0 File Offset: 0x000410B0
			public static void Invoke(ref UnsafeSpan<float3> vertices, ref float4x4 localToWorldMatrix, out Bounds bounds)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = RecastMeshGatherer.CalculateBounds_00000A9B$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Util.UnsafeSpan`1<Unity.Mathematics.float3>&,Unity.Mathematics.float4x4&,UnityEngine.Bounds&), ref vertices, ref localToWorldMatrix, ref bounds, functionPointer);
						return;
					}
				}
				RecastMeshGatherer.CalculateBounds$BurstManaged(ref vertices, ref localToWorldMatrix, out bounds);
			}

			// Token: 0x04000811 RID: 2065
			private static IntPtr Pointer;

			// Token: 0x04000812 RID: 2066
			private static IntPtr DeferredCompilation;
		}
	}
}
