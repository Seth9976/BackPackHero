using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006C RID: 108
	internal class DecalEntityManager : IDisposable
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00016115 File Offset: 0x00014315
		public Material errorMaterial
		{
			get
			{
				if (this.m_ErrorMaterial == null)
				{
					this.m_ErrorMaterial = CoreUtils.CreateEngineMaterial(Shader.Find("Hidden/InternalErrorShader"));
				}
				return this.m_ErrorMaterial;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00016140 File Offset: 0x00014340
		public Mesh decalProjectorMesh
		{
			get
			{
				if (this.m_DecalProjectorMesh == null)
				{
					this.m_DecalProjectorMesh = CoreUtils.CreateCubeMesh(new Vector4(-0.5f, -0.5f, -0.5f, 1f), new Vector4(0.5f, 0.5f, 0.5f, 1f));
				}
				return this.m_DecalProjectorMesh;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000161A8 File Offset: 0x000143A8
		public DecalEntityManager()
		{
			this.m_AddDecalSampler = new ProfilingSampler("DecalEntityManager.CreateDecalEntity");
			this.m_ResizeChunks = new ProfilingSampler("DecalEntityManager.ResizeChunks");
			this.m_SortChunks = new ProfilingSampler("DecalEntityManager.SortChunks");
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00016243 File Offset: 0x00014443
		public bool IsValid(DecalEntity decalEntity)
		{
			return this.m_DecalEntityIndexer.IsValid(decalEntity);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00016254 File Offset: 0x00014454
		public DecalEntity CreateDecalEntity(DecalProjector decalProjector)
		{
			Material material = decalProjector.material;
			if (material == null)
			{
				material = this.errorMaterial;
			}
			DecalEntity decalEntity2;
			using (new ProfilingScope(null, this.m_AddDecalSampler))
			{
				int num = this.CreateChunkIndex(material);
				int count = this.entityChunks[num].count;
				DecalEntity decalEntity = this.m_DecalEntityIndexer.CreateDecalEntity(count, num);
				DecalEntityChunk decalEntityChunk = this.entityChunks[num];
				DecalCachedChunk decalCachedChunk = this.cachedChunks[num];
				DecalCulledChunk decalCulledChunk = this.culledChunks[num];
				DecalDrawCallChunk decalDrawCallChunk = this.drawCallChunks[num];
				if (this.entityChunks[num].capacity == this.entityChunks[num].count)
				{
					using (new ProfilingScope(null, this.m_ResizeChunks))
					{
						int num2 = this.entityChunks[num].capacity + this.entityChunks[num].capacity;
						num2 = math.max(8, num2);
						decalEntityChunk.SetCapacity(num2);
						decalCachedChunk.SetCapacity(num2);
						decalCulledChunk.SetCapacity(num2);
						decalDrawCallChunk.SetCapacity(num2);
					}
				}
				decalEntityChunk.Push();
				decalCachedChunk.Push();
				decalCulledChunk.Push();
				decalDrawCallChunk.Push();
				decalEntityChunk.decalProjectors[count] = decalProjector;
				decalEntityChunk.decalEntities[count] = decalEntity;
				decalEntityChunk.transformAccessArray.Add(decalProjector.transform);
				this.UpdateDecalEntityData(decalEntity, decalProjector);
				decalEntity2 = decalEntity;
			}
			return decalEntity2;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00016418 File Offset: 0x00014618
		private int CreateChunkIndex(Material material)
		{
			int num;
			if (!this.m_MaterialToChunkIndex.TryGetValue(material, out num))
			{
				MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
				materialPropertyBlock.SetMatrixArray("_NormalToWorld", new Matrix4x4[250]);
				this.entityChunks.Add(new DecalEntityChunk
				{
					material = material
				});
				this.cachedChunks.Add(new DecalCachedChunk
				{
					propertyBlock = materialPropertyBlock
				});
				this.culledChunks.Add(new DecalCulledChunk());
				this.drawCallChunks.Add(new DecalDrawCallChunk
				{
					subCallCounts = new NativeArray<int>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory)
				});
				this.m_CombinedChunks.Add(default(DecalEntityManager.CombinedChunks));
				this.m_CombinedChunkRemmap.Add(0);
				this.m_MaterialToChunkIndex.Add(material, this.chunkCount);
				int num2 = this.chunkCount;
				this.chunkCount = num2 + 1;
				return num2;
			}
			return num;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000164F4 File Offset: 0x000146F4
		public void UpdateDecalEntityData(DecalEntity decalEntity, DecalProjector decalProjector)
		{
			DecalEntityIndexer.DecalEntityItem item = this.m_DecalEntityIndexer.GetItem(decalEntity);
			int chunkIndex = item.chunkIndex;
			int arrayIndex = item.arrayIndex;
			DecalCachedChunk decalCachedChunk = this.cachedChunks[chunkIndex];
			decalCachedChunk.sizeOffsets[arrayIndex] = Matrix4x4.Translate(decalProjector.decalOffset) * Matrix4x4.Scale(decalProjector.decalSize);
			float drawDistance = decalProjector.drawDistance;
			float fadeScale = decalProjector.fadeScale;
			float startAngleFade = decalProjector.startAngleFade;
			float endAngleFade = decalProjector.endAngleFade;
			Vector4 uvScaleBias = decalProjector.uvScaleBias;
			int layer = decalProjector.gameObject.layer;
			ulong sceneCullingMask = decalProjector.gameObject.sceneCullingMask;
			float fadeFactor = decalProjector.fadeFactor;
			decalCachedChunk.drawDistances[arrayIndex] = new Vector2(drawDistance, fadeScale);
			if (startAngleFade == 180f)
			{
				decalCachedChunk.angleFades[arrayIndex] = new Vector2(0f, 0f);
			}
			else
			{
				float num = startAngleFade / 180f;
				float num2 = endAngleFade / 180f;
				float num3 = Mathf.Max(0.0001f, num2 - num);
				decalCachedChunk.angleFades[arrayIndex] = new Vector2(1f - (0.25f - num) / num3, -0.25f / num3);
			}
			decalCachedChunk.uvScaleBias[arrayIndex] = uvScaleBias;
			decalCachedChunk.layerMasks[arrayIndex] = layer;
			decalCachedChunk.sceneLayerMasks[arrayIndex] = sceneCullingMask;
			decalCachedChunk.fadeFactors[arrayIndex] = fadeFactor;
			decalCachedChunk.scaleModes[arrayIndex] = decalProjector.scaleMode;
			decalCachedChunk.positions[arrayIndex] = decalProjector.transform.position;
			decalCachedChunk.rotation[arrayIndex] = decalProjector.transform.rotation;
			decalCachedChunk.scales[arrayIndex] = decalProjector.transform.lossyScale;
			decalCachedChunk.dirty[arrayIndex] = true;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000166E4 File Offset: 0x000148E4
		public void DestroyDecalEntity(DecalEntity decalEntity)
		{
			if (!this.m_DecalEntityIndexer.IsValid(decalEntity))
			{
				return;
			}
			DecalEntityIndexer.DecalEntityItem item = this.m_DecalEntityIndexer.GetItem(decalEntity);
			this.m_DecalEntityIndexer.DestroyDecalEntity(decalEntity);
			int chunkIndex = item.chunkIndex;
			int arrayIndex = item.arrayIndex;
			DecalEntityChunk decalEntityChunk = this.entityChunks[chunkIndex];
			DecalCachedChunk decalCachedChunk = this.cachedChunks[chunkIndex];
			DecalCulledChunk decalCulledChunk = this.culledChunks[chunkIndex];
			DecalChunk decalChunk = this.drawCallChunks[chunkIndex];
			int num = decalEntityChunk.count - 1;
			if (arrayIndex != num)
			{
				this.m_DecalEntityIndexer.UpdateIndex(decalEntityChunk.decalEntities[num], arrayIndex);
			}
			decalEntityChunk.RemoveAtSwapBack(arrayIndex);
			decalCachedChunk.RemoveAtSwapBack(arrayIndex);
			decalCulledChunk.RemoveAtSwapBack(arrayIndex);
			decalChunk.RemoveAtSwapBack(arrayIndex);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000167A0 File Offset: 0x000149A0
		public void Update()
		{
			using (new ProfilingScope(null, this.m_SortChunks))
			{
				for (int i = 0; i < this.chunkCount; i++)
				{
					if (this.entityChunks[i].material == null)
					{
						this.entityChunks[i].material = this.errorMaterial;
					}
				}
				for (int j = 0; j < this.chunkCount; j++)
				{
					this.m_CombinedChunks[j] = new DecalEntityManager.CombinedChunks
					{
						entityChunk = this.entityChunks[j],
						cachedChunk = this.cachedChunks[j],
						culledChunk = this.culledChunks[j],
						drawCallChunk = this.drawCallChunks[j],
						previousChunkIndex = j,
						valid = (this.entityChunks[j].count != 0)
					};
				}
				this.m_CombinedChunks.Sort(delegate(DecalEntityManager.CombinedChunks a, DecalEntityManager.CombinedChunks b)
				{
					if (a.valid && !b.valid)
					{
						return -1;
					}
					if (!a.valid && b.valid)
					{
						return 1;
					}
					if (a.cachedChunk.drawOrder < b.cachedChunk.drawOrder)
					{
						return -1;
					}
					if (a.cachedChunk.drawOrder > b.cachedChunk.drawOrder)
					{
						return 1;
					}
					return a.entityChunk.material.GetHashCode().CompareTo(b.entityChunk.material.GetHashCode());
				});
				bool flag = false;
				for (int k = 0; k < this.chunkCount; k++)
				{
					if (this.m_CombinedChunks[k].previousChunkIndex != k || !this.m_CombinedChunks[k].valid)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					int num = 0;
					this.m_MaterialToChunkIndex.Clear();
					for (int l = 0; l < this.chunkCount; l++)
					{
						DecalEntityManager.CombinedChunks combinedChunks = this.m_CombinedChunks[l];
						if (!this.m_CombinedChunks[l].valid)
						{
							combinedChunks.entityChunk.currentJobHandle.Complete();
							combinedChunks.cachedChunk.currentJobHandle.Complete();
							combinedChunks.culledChunk.currentJobHandle.Complete();
							combinedChunks.drawCallChunk.currentJobHandle.Complete();
							combinedChunks.entityChunk.Dispose();
							combinedChunks.cachedChunk.Dispose();
							combinedChunks.culledChunk.Dispose();
							combinedChunks.drawCallChunk.Dispose();
						}
						else
						{
							this.entityChunks[l] = combinedChunks.entityChunk;
							this.cachedChunks[l] = combinedChunks.cachedChunk;
							this.culledChunks[l] = combinedChunks.culledChunk;
							this.drawCallChunks[l] = combinedChunks.drawCallChunk;
							if (!this.m_MaterialToChunkIndex.ContainsKey(this.entityChunks[l].material))
							{
								this.m_MaterialToChunkIndex.Add(this.entityChunks[l].material, l);
							}
							this.m_CombinedChunkRemmap[combinedChunks.previousChunkIndex] = l;
							num++;
						}
					}
					if (this.chunkCount > num)
					{
						this.entityChunks.RemoveRange(num, this.chunkCount - num);
						this.cachedChunks.RemoveRange(num, this.chunkCount - num);
						this.culledChunks.RemoveRange(num, this.chunkCount - num);
						this.drawCallChunks.RemoveRange(num, this.chunkCount - num);
						this.m_CombinedChunks.RemoveRange(num, this.chunkCount - num);
						this.chunkCount = num;
					}
					this.m_DecalEntityIndexer.RemapChunkIndices(this.m_CombinedChunkRemmap);
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00016B48 File Offset: 0x00014D48
		public void Dispose()
		{
			CoreUtils.Destroy(this.m_ErrorMaterial);
			CoreUtils.Destroy(this.m_DecalProjectorMesh);
			foreach (DecalEntityChunk decalEntityChunk in this.entityChunks)
			{
				decalEntityChunk.currentJobHandle.Complete();
			}
			foreach (DecalCachedChunk decalCachedChunk in this.cachedChunks)
			{
				decalCachedChunk.currentJobHandle.Complete();
			}
			foreach (DecalCulledChunk decalCulledChunk in this.culledChunks)
			{
				decalCulledChunk.currentJobHandle.Complete();
			}
			foreach (DecalDrawCallChunk decalDrawCallChunk in this.drawCallChunks)
			{
				decalDrawCallChunk.currentJobHandle.Complete();
			}
			foreach (DecalEntityChunk decalEntityChunk2 in this.entityChunks)
			{
				decalEntityChunk2.Dispose();
			}
			foreach (DecalCachedChunk decalCachedChunk2 in this.cachedChunks)
			{
				decalCachedChunk2.Dispose();
			}
			foreach (DecalCulledChunk decalCulledChunk2 in this.culledChunks)
			{
				decalCulledChunk2.Dispose();
			}
			foreach (DecalDrawCallChunk decalDrawCallChunk2 in this.drawCallChunks)
			{
				decalDrawCallChunk2.Dispose();
			}
			this.m_DecalEntityIndexer.Clear();
			this.m_MaterialToChunkIndex.Clear();
			this.entityChunks.Clear();
			this.cachedChunks.Clear();
			this.culledChunks.Clear();
			this.drawCallChunks.Clear();
			this.m_CombinedChunks.Clear();
			this.chunkCount = 0;
		}

		// Token: 0x040002B7 RID: 695
		public List<DecalEntityChunk> entityChunks = new List<DecalEntityChunk>();

		// Token: 0x040002B8 RID: 696
		public List<DecalCachedChunk> cachedChunks = new List<DecalCachedChunk>();

		// Token: 0x040002B9 RID: 697
		public List<DecalCulledChunk> culledChunks = new List<DecalCulledChunk>();

		// Token: 0x040002BA RID: 698
		public List<DecalDrawCallChunk> drawCallChunks = new List<DecalDrawCallChunk>();

		// Token: 0x040002BB RID: 699
		public int chunkCount;

		// Token: 0x040002BC RID: 700
		private ProfilingSampler m_AddDecalSampler;

		// Token: 0x040002BD RID: 701
		private ProfilingSampler m_ResizeChunks;

		// Token: 0x040002BE RID: 702
		private ProfilingSampler m_SortChunks;

		// Token: 0x040002BF RID: 703
		private DecalEntityIndexer m_DecalEntityIndexer = new DecalEntityIndexer();

		// Token: 0x040002C0 RID: 704
		private Dictionary<Material, int> m_MaterialToChunkIndex = new Dictionary<Material, int>();

		// Token: 0x040002C1 RID: 705
		private List<DecalEntityManager.CombinedChunks> m_CombinedChunks = new List<DecalEntityManager.CombinedChunks>();

		// Token: 0x040002C2 RID: 706
		private List<int> m_CombinedChunkRemmap = new List<int>();

		// Token: 0x040002C3 RID: 707
		private Material m_ErrorMaterial;

		// Token: 0x040002C4 RID: 708
		private Mesh m_DecalProjectorMesh;

		// Token: 0x02000168 RID: 360
		private struct CombinedChunks
		{
			// Token: 0x0400093E RID: 2366
			public DecalEntityChunk entityChunk;

			// Token: 0x0400093F RID: 2367
			public DecalCachedChunk cachedChunk;

			// Token: 0x04000940 RID: 2368
			public DecalCulledChunk culledChunk;

			// Token: 0x04000941 RID: 2369
			public DecalDrawCallChunk drawCallChunk;

			// Token: 0x04000942 RID: 2370
			public int previousChunkIndex;

			// Token: 0x04000943 RID: 2371
			public bool valid;
		}
	}
}
