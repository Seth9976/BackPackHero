using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine.U2D.Common;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000030 RID: 48
	internal class SpriteSkinComposite : ScriptableObject
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005150 File Offset: 0x00003350
		public static SpriteSkinComposite instance
		{
			get
			{
				if (SpriteSkinComposite.s_Instance == null)
				{
					SpriteSkinComposite[] array = Resources.FindObjectsOfTypeAll<SpriteSkinComposite>();
					if (array.Length != 0)
					{
						SpriteSkinComposite.s_Instance = array[0];
					}
					else
					{
						SpriteSkinComposite.s_Instance = ScriptableObject.CreateInstance<SpriteSkinComposite>();
					}
					SpriteSkinComposite.s_Instance.hideFlags = HideFlags.HideAndDontSave;
					SpriteSkinComposite.s_Instance.Init();
				}
				return SpriteSkinComposite.s_Instance;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000051A3 File Offset: 0x000033A3
		internal GameObject helperGameObject
		{
			get
			{
				return this.m_Helper;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000051AB File Offset: 0x000033AB
		internal void RemoveTransformById(int transformId)
		{
			this.m_LocalToWorldTransformAccessJob.RemoveTransformById(transformId);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000051BC File Offset: 0x000033BC
		internal void AddSpriteSkinBoneTransform(SpriteSkin spriteSkin)
		{
			if (spriteSkin == null)
			{
				return;
			}
			if (spriteSkin.boneTransforms != null)
			{
				foreach (Transform transform in spriteSkin.boneTransforms)
				{
					if (transform != null)
					{
						this.m_LocalToWorldTransformAccessJob.AddTransform(transform);
					}
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005209 File Offset: 0x00003409
		internal void AddSpriteSkinRootBoneTransform(SpriteSkin spriteSkin)
		{
			if (spriteSkin == null || spriteSkin.rootBone == null)
			{
				return;
			}
			this.m_LocalToWorldTransformAccessJob.AddTransform(spriteSkin.rootBone);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005234 File Offset: 0x00003434
		internal void AddSpriteSkin(SpriteSkin spriteSkin)
		{
			if (spriteSkin == null)
			{
				return;
			}
			if (!this.m_SpriteSkins.Contains(spriteSkin))
			{
				this.m_SpriteSkins.Add(spriteSkin);
				int count = this.m_SpriteSkins.Count;
				Array.Resize<SpriteRenderer>(ref this.m_SpriteRenderers, count);
				this.m_SpriteRenderers[count - 1] = spriteSkin.spriteRenderer;
				this.m_WorldToLocalTransformAccessJob.AddTransform(spriteSkin.transform);
				if (this.m_IsSpriteSkinActiveForDeform.IsCreated)
				{
					NativeArrayHelpers.ResizeAndCopyIfNeeded<bool>(ref this.m_IsSpriteSkinActiveForDeform, count, Allocator.Persistent);
					NativeArrayHelpers.ResizeAndCopyIfNeeded<PerSkinJobData>(ref this.m_PerSkinJobData, count, Allocator.Persistent);
					NativeArrayHelpers.ResizeAndCopyIfNeeded<SpriteSkinData>(ref this.m_SpriteSkinData, count, Allocator.Persistent);
					NativeArrayHelpers.ResizeAndCopyIfNeeded<Bounds>(ref this.m_BoundsData, count, Allocator.Persistent);
					NativeArrayHelpers.ResizeAndCopyIfNeeded<IntPtr>(ref this.m_Buffers, count, Allocator.Persistent);
					NativeArrayHelpers.ResizeAndCopyIfNeeded<int>(ref this.m_BufferSizes, count, Allocator.Persistent);
					this.CopyToSpriteSkinData(count - 1);
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005308 File Offset: 0x00003508
		internal void CopyToSpriteSkinData(SpriteSkin spriteSkin)
		{
			if (spriteSkin == null)
			{
				return;
			}
			int num = this.m_SpriteSkins.IndexOf(spriteSkin);
			if (num < 0)
			{
				return;
			}
			this.CopyToSpriteSkinData(num);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005338 File Offset: 0x00003538
		private void CopyToSpriteSkinData(int index)
		{
			if (index < 0 || index >= this.m_SpriteSkins.Count || !this.m_SpriteSkinData.IsCreated)
			{
				return;
			}
			SpriteSkinData spriteSkinData = default(SpriteSkinData);
			SpriteSkin spriteSkin = this.m_SpriteSkins[index];
			spriteSkin.CopyToSpriteSkinData(ref spriteSkinData, index);
			this.m_SpriteSkinData[index] = spriteSkinData;
			this.m_SpriteRenderers[index] = spriteSkin.spriteRenderer;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000053A0 File Offset: 0x000035A0
		internal void RemoveSpriteSkin(SpriteSkin spriteSkin)
		{
			int num = this.m_SpriteSkins.IndexOf(spriteSkin);
			if (num < 0)
			{
				return;
			}
			if (num < this.m_SpriteSkins.Count - 1)
			{
				this.m_SpriteSkins.RemoveAtSwapBack(num);
				this.CopyToSpriteSkinData(num);
			}
			else
			{
				this.m_SpriteSkins.RemoveAt(num);
			}
			int count = this.m_SpriteSkins.Count;
			Array.Resize<SpriteRenderer>(ref this.m_SpriteRenderers, count);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<bool>(ref this.m_IsSpriteSkinActiveForDeform, count, Allocator.Persistent);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<PerSkinJobData>(ref this.m_PerSkinJobData, count, Allocator.Persistent);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<SpriteSkinData>(ref this.m_SpriteSkinData, count, Allocator.Persistent);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<Bounds>(ref this.m_BoundsData, count, Allocator.Persistent);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<IntPtr>(ref this.m_Buffers, count, Allocator.Persistent);
			NativeArrayHelpers.ResizeAndCopyIfNeeded<int>(ref this.m_BufferSizes, count, Allocator.Persistent);
			this.m_WorldToLocalTransformAccessJob.RemoveTransform(spriteSkin.transform);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005467 File Offset: 0x00003667
		internal void AddSpriteSkinForLateUpdate(SpriteSkin spriteSkin)
		{
			if (spriteSkin == null)
			{
				return;
			}
			if (!this.m_SpriteSkinLateUpdate.Contains(spriteSkin))
			{
				this.m_SpriteSkinLateUpdate.Add(spriteSkin);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000548D File Offset: 0x0000368D
		internal void RemoveSpriteSkinForLateUpdate(SpriteSkin spriteSkin)
		{
			this.m_SpriteSkinLateUpdate.Remove(spriteSkin);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000549C File Offset: 0x0000369C
		private void Init()
		{
			if (this.m_LocalToWorldTransformAccessJob == null)
			{
				this.m_LocalToWorldTransformAccessJob = new TransformAccessJob();
			}
			if (this.m_WorldToLocalTransformAccessJob == null)
			{
				this.m_WorldToLocalTransformAccessJob = new TransformAccessJob();
			}
			this.CreateHelper();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000054CC File Offset: 0x000036CC
		private void CreateHelper()
		{
			if (this.m_Helper != null)
			{
				return;
			}
			this.m_Helper = new GameObject("SpriteSkinUpdateHelper");
			this.m_Helper.hideFlags = HideFlags.HideAndDontSave;
			SpriteSkinUpdateHelper spriteSkinUpdateHelper = this.m_Helper.AddComponent<SpriteSkinUpdateHelper>();
			spriteSkinUpdateHelper.onDestroyingComponent = (Action<GameObject>)Delegate.Combine(spriteSkinUpdateHelper.onDestroyingComponent, new Action<GameObject>(this.OnHelperDestroyed));
			Object.DontDestroyOnLoad(this.m_Helper);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000553C File Offset: 0x0000373C
		private void OnHelperDestroyed(GameObject helperGo)
		{
			if (this.m_Helper != helperGo)
			{
				return;
			}
			this.m_Helper = null;
			this.CreateHelper();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000555C File Offset: 0x0000375C
		internal void ResetComposite()
		{
			foreach (SpriteSkin spriteSkin in this.m_SpriteSkins)
			{
				spriteSkin.batchSkinning = false;
			}
			this.m_SpriteSkins.Clear();
			this.m_LocalToWorldTransformAccessJob.Destroy();
			this.m_WorldToLocalTransformAccessJob.Destroy();
			this.m_LocalToWorldTransformAccessJob = new TransformAccessJob();
			this.m_WorldToLocalTransformAccessJob = new TransformAccessJob();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000055E4 File Offset: 0x000037E4
		public void OnEnable()
		{
			SpriteSkinComposite.s_Instance = this;
			this.m_FinalBoneTransforms = new NativeArray<float4x4>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_BoneLookupData = new NativeArray<int2>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_VertexLookupData = new NativeArray<int2>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_SkinBatchArray = new NativeArray<PerSkinJobData>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.Init();
			int count = this.m_SpriteSkins.Count;
			if (count > 0)
			{
				this.m_IsSpriteSkinActiveForDeform = new NativeArray<bool>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.m_PerSkinJobData = new NativeArray<PerSkinJobData>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.m_SpriteSkinData = new NativeArray<SpriteSkinData>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.m_BoundsData = new NativeArray<Bounds>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.m_Buffers = new NativeArray<IntPtr>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.m_BufferSizes = new NativeArray<int>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < count; i++)
				{
					this.m_SpriteSkins[i].batchSkinning = true;
					this.CopyToSpriteSkinData(i);
				}
				return;
			}
			this.m_IsSpriteSkinActiveForDeform = new NativeArray<bool>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_PerSkinJobData = new NativeArray<PerSkinJobData>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_SpriteSkinData = new NativeArray<SpriteSkinData>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_BoundsData = new NativeArray<Bounds>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_Buffers = new NativeArray<IntPtr>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_BufferSizes = new NativeArray<int>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005714 File Offset: 0x00003914
		private void OnDisable()
		{
			this.m_DeformJobHandle.Complete();
			this.m_BoundJobHandle.Complete();
			this.m_CopyJobHandle.Complete();
			foreach (SpriteSkin spriteSkin in this.m_SpriteSkins)
			{
				spriteSkin.batchSkinning = false;
			}
			this.m_SpriteSkins.Clear();
			this.m_SpriteRenderers = new SpriteRenderer[0];
			BufferManager.instance.ReturnBuffer(base.GetInstanceID());
			this.m_IsSpriteSkinActiveForDeform.DisposeIfCreated<bool>();
			this.m_PerSkinJobData.DisposeIfCreated<PerSkinJobData>();
			this.m_SpriteSkinData.DisposeIfCreated<SpriteSkinData>();
			this.m_Buffers.DisposeIfCreated<IntPtr>();
			this.m_BufferSizes.DisposeIfCreated<int>();
			this.m_BoneLookupData.DisposeIfCreated<int2>();
			this.m_VertexLookupData.DisposeIfCreated<int2>();
			this.m_SkinBatchArray.DisposeIfCreated<PerSkinJobData>();
			this.m_FinalBoneTransforms.DisposeIfCreated<float4x4>();
			this.m_BoundsData.DisposeIfCreated<Bounds>();
			if (this.m_Helper != null)
			{
				SpriteSkinUpdateHelper component = this.m_Helper.GetComponent<SpriteSkinUpdateHelper>();
				component.onDestroyingComponent = (Action<GameObject>)Delegate.Remove(component.onDestroyingComponent, new Action<GameObject>(this.OnHelperDestroyed));
				Object.DestroyImmediate(this.m_Helper);
			}
			this.m_LocalToWorldTransformAccessJob.Destroy();
			this.m_WorldToLocalTransformAccessJob.Destroy();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005878 File Offset: 0x00003A78
		internal void LateUpdate()
		{
			foreach (SpriteSkin spriteSkin in this.m_SpriteSkinLateUpdate)
			{
				if (spriteSkin != null)
				{
					spriteSkin.OnLateUpdate();
				}
			}
			if (this.m_SpriteSkins.Count == 0)
			{
				return;
			}
			using (SpriteSkinComposite.Profiling.validateSpriteSkinData.Auto())
			{
				for (int i = 0; i < this.m_SpriteSkins.Count; i++)
				{
					SpriteSkin spriteSkin2 = this.m_SpriteSkins[i];
					this.m_IsSpriteSkinActiveForDeform[i] = spriteSkin2.BatchValidate();
					if (this.m_IsSpriteSkinActiveForDeform[i] && spriteSkin2.NeedUpdateCompositeCache())
					{
						this.CopyToSpriteSkinData(i);
					}
				}
			}
			JobHandle jobHandle = this.m_LocalToWorldTransformAccessJob.StartLocalToWorldJob();
			JobHandle jobHandle2 = this.m_WorldToLocalTransformAccessJob.StartWorldToLocalJob();
			using (SpriteSkinComposite.Profiling.getSpriteSkinBatchData.Auto())
			{
				NativeArrayHelpers.ResizeIfNeeded<PerSkinJobData>(ref this.m_SkinBatchArray, 1, Allocator.Persistent);
				new FillPerSkinJobSingleThread
				{
					isSpriteSkinValidForDeformArray = this.m_IsSpriteSkinActiveForDeform,
					combinedSkinBatchArray = this.m_SkinBatchArray,
					spriteSkinDataArray = this.m_SpriteSkinData,
					perSkinJobDataArray = this.m_PerSkinJobData
				}.Run<FillPerSkinJobSingleThread>();
			}
			PerSkinJobData perSkinJobData = this.m_SkinBatchArray[0];
			int length = this.m_SpriteSkinData.Length;
			int deformVerticesStartPos = perSkinJobData.deformVerticesStartPos;
			if (deformVerticesStartPos <= 0)
			{
				jobHandle.Complete();
				jobHandle2.Complete();
				this.DeactivateDeformableBuffers();
				return;
			}
			using (SpriteSkinComposite.Profiling.resizeBuffers.Auto())
			{
				this.m_DeformedVerticesBuffer = BufferManager.instance.GetBuffer(base.GetInstanceID(), deformVerticesStartPos);
				NativeArrayHelpers.ResizeIfNeeded<float4x4>(ref this.m_FinalBoneTransforms, perSkinJobData.bindPosesIndex.y, Allocator.Persistent);
				NativeArrayHelpers.ResizeIfNeeded<int2>(ref this.m_BoneLookupData, perSkinJobData.bindPosesIndex.y, Allocator.Persistent);
				NativeArrayHelpers.ResizeIfNeeded<int2>(ref this.m_VertexLookupData, perSkinJobData.verticesIndex.y, Allocator.Persistent);
			}
			JobHandle jobHandle3 = new PrepareDeformJob
			{
				batchDataSize = length,
				perSkinJobData = this.m_PerSkinJobData,
				boneLookupData = this.m_BoneLookupData,
				vertexLookupData = this.m_VertexLookupData
			}.Schedule(default(JobHandle));
			BoneDeformBatchedJob boneDeformBatchedJob = new BoneDeformBatchedJob
			{
				boneTransform = this.m_LocalToWorldTransformAccessJob.transformMatrix,
				rootTransform = this.m_WorldToLocalTransformAccessJob.transformMatrix,
				spriteSkinData = this.m_SpriteSkinData,
				boneLookupData = this.m_BoneLookupData,
				finalBoneTransforms = this.m_FinalBoneTransforms,
				rootTransformIndex = this.m_WorldToLocalTransformAccessJob.transformData,
				boneTransformIndex = this.m_LocalToWorldTransformAccessJob.transformData
			};
			jobHandle3 = JobHandle.CombineDependencies(jobHandle, jobHandle2, jobHandle3);
			jobHandle3 = boneDeformBatchedJob.Schedule(perSkinJobData.bindPosesIndex.y, 8, jobHandle3);
			SkinDeformBatchedJob skinDeformBatchedJob = new SkinDeformBatchedJob
			{
				vertices = this.m_DeformedVerticesBuffer.array,
				vertexLookupData = this.m_VertexLookupData,
				spriteSkinData = this.m_SpriteSkinData,
				perSkinJobData = this.m_PerSkinJobData,
				finalBoneTransforms = this.m_FinalBoneTransforms
			};
			this.m_DeformJobHandle = skinDeformBatchedJob.Schedule(perSkinJobData.verticesIndex.y, 16, jobHandle3);
			CopySpriteRendererBuffersJob copySpriteRendererBuffersJob = new CopySpriteRendererBuffersJob
			{
				isSpriteSkinValidForDeformArray = this.m_IsSpriteSkinActiveForDeform,
				spriteSkinData = this.m_SpriteSkinData,
				ptrVertices = (IntPtr)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_DeformedVerticesBuffer.array),
				buffers = this.m_Buffers,
				bufferSizes = this.m_BufferSizes
			};
			this.m_CopyJobHandle = copySpriteRendererBuffersJob.Schedule(length, 16, jobHandle3);
			CalculateSpriteSkinAABBJob calculateSpriteSkinAABBJob = new CalculateSpriteSkinAABBJob
			{
				vertices = this.m_DeformedVerticesBuffer.array,
				isSpriteSkinValidForDeformArray = this.m_IsSpriteSkinActiveForDeform,
				spriteSkinData = this.m_SpriteSkinData,
				bounds = this.m_BoundsData
			};
			this.m_BoundJobHandle = calculateSpriteSkinAABBJob.Schedule(length, 4, this.m_DeformJobHandle);
			JobHandle.ScheduleBatchedJobs();
			JobHandle.CombineDependencies(this.m_BoundJobHandle, this.m_CopyJobHandle).Complete();
			using (SpriteSkinComposite.Profiling.setBatchDeformableBufferAndLocalAABB.Auto())
			{
				InternalEngineBridge.SetBatchDeformableBufferAndLocalAABBArray(this.m_SpriteRenderers, this.m_Buffers, this.m_BufferSizes, this.m_BoundsData);
			}
			this.DeactivateDeformableBuffers();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005D58 File Offset: 0x00003F58
		private void DeactivateDeformableBuffers()
		{
			using (SpriteSkinComposite.Profiling.deactivateDeformableBuffer.Auto())
			{
				for (int i = 0; i < this.m_IsSpriteSkinActiveForDeform.Length; i++)
				{
					if (!this.m_IsSpriteSkinActiveForDeform[i] && !InternalEngineBridge.IsUsingDeformableBuffer(this.m_SpriteRenderers[i], IntPtr.Zero))
					{
						this.m_SpriteRenderers[i].DeactivateDeformableBuffer();
					}
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005DD8 File Offset: 0x00003FD8
		internal bool HasDeformableBufferForSprite(int dataIndex)
		{
			if (dataIndex < 0 && this.m_IsSpriteSkinActiveForDeform.Length >= dataIndex)
			{
				throw new InvalidOperationException("Invalid index for deformable buffer");
			}
			return this.m_IsSpriteSkinActiveForDeform[dataIndex];
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005E04 File Offset: 0x00004004
		internal unsafe NativeArray<byte> GetDeformableBufferForSprite(int dataIndex)
		{
			if (dataIndex < 0 && this.m_SpriteSkinData.Length >= dataIndex)
			{
				throw new InvalidOperationException("Invalid index for deformable buffer");
			}
			if (!this.m_DeformJobHandle.IsCompleted)
			{
				this.m_DeformJobHandle.Complete();
			}
			SpriteSkinData spriteSkinData = this.m_SpriteSkinData[dataIndex];
			if (spriteSkinData.deformVerticesStartPos < 0)
			{
				throw new InvalidOperationException("There are no currently deformed vertices.");
			}
			int num = spriteSkinData.spriteVertexCount * spriteSkinData.spriteVertexStreamSize;
			byte* ptr = (byte*)this.m_DeformedVerticesBuffer.array.GetUnsafeReadOnlyPtr<byte>();
			ptr += spriteSkinData.deformVerticesStartPos;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)ptr, num, Allocator.None);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005E98 File Offset: 0x00004098
		internal string GetDebugLog()
		{
			string text = "";
			text = "====SpriteSkinLateUpdate===\n";
			text = text + "Count: " + this.m_SpriteSkinLateUpdate.Count.ToString() + "\n";
			foreach (SpriteSkin spriteSkin in this.m_SpriteSkinLateUpdate)
			{
				text += ((spriteSkin == null) ? "null" : spriteSkin.name);
				text += "\n";
			}
			text += "\n";
			text += "===SpriteSkinBatch===\n";
			text = text + "Count: " + this.m_SpriteSkins.Count.ToString() + "\n";
			foreach (SpriteSkin spriteSkin2 in this.m_SpriteSkins)
			{
				text += ((spriteSkin2 == null) ? "null" : spriteSkin2.name);
				text += "\n";
			}
			text += "===LocalToWorldTransformAccessJob===\n";
			text += this.m_LocalToWorldTransformAccessJob.GetDebugLog();
			text += "\n";
			text += "===WorldToLocalTransformAccessJob===\n";
			text += "\n";
			text += this.m_WorldToLocalTransformAccessJob.GetDebugLog();
			return text;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006038 File Offset: 0x00004238
		internal SpriteSkin[] GetSpriteSkins()
		{
			return this.m_SpriteSkins.ToArray();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006045 File Offset: 0x00004245
		internal TransformAccessJob GetWorldToLocalTransformAccessJob()
		{
			return this.m_WorldToLocalTransformAccessJob;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000604D File Offset: 0x0000424D
		internal TransformAccessJob GetLocalToWorldTransformAccessJob()
		{
			return this.m_LocalToWorldTransformAccessJob;
		}

		// Token: 0x0400009A RID: 154
		private static SpriteSkinComposite s_Instance;

		// Token: 0x0400009B RID: 155
		private List<SpriteSkin> m_SpriteSkins = new List<SpriteSkin>();

		// Token: 0x0400009C RID: 156
		private List<SpriteSkin> m_SpriteSkinLateUpdate = new List<SpriteSkin>();

		// Token: 0x0400009D RID: 157
		private SpriteRenderer[] m_SpriteRenderers = new SpriteRenderer[0];

		// Token: 0x0400009E RID: 158
		private NativeByteArray m_DeformedVerticesBuffer;

		// Token: 0x0400009F RID: 159
		private NativeArray<float4x4> m_FinalBoneTransforms;

		// Token: 0x040000A0 RID: 160
		private NativeArray<bool> m_IsSpriteSkinActiveForDeform;

		// Token: 0x040000A1 RID: 161
		private NativeArray<SpriteSkinData> m_SpriteSkinData;

		// Token: 0x040000A2 RID: 162
		private NativeArray<PerSkinJobData> m_PerSkinJobData;

		// Token: 0x040000A3 RID: 163
		private NativeArray<Bounds> m_BoundsData;

		// Token: 0x040000A4 RID: 164
		private NativeArray<IntPtr> m_Buffers;

		// Token: 0x040000A5 RID: 165
		private NativeArray<int> m_BufferSizes;

		// Token: 0x040000A6 RID: 166
		private NativeArray<int2> m_BoneLookupData;

		// Token: 0x040000A7 RID: 167
		private NativeArray<int2> m_VertexLookupData;

		// Token: 0x040000A8 RID: 168
		private NativeArray<PerSkinJobData> m_SkinBatchArray;

		// Token: 0x040000A9 RID: 169
		private TransformAccessJob m_LocalToWorldTransformAccessJob;

		// Token: 0x040000AA RID: 170
		private TransformAccessJob m_WorldToLocalTransformAccessJob;

		// Token: 0x040000AB RID: 171
		private JobHandle m_BoundJobHandle;

		// Token: 0x040000AC RID: 172
		private JobHandle m_DeformJobHandle;

		// Token: 0x040000AD RID: 173
		private JobHandle m_CopyJobHandle;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private GameObject m_Helper;

		// Token: 0x02000031 RID: 49
		private static class Profiling
		{
			// Token: 0x040000AF RID: 175
			public static readonly ProfilerMarker prepareData = new ProfilerMarker("SpriteSkinComposite.PrepareData");

			// Token: 0x040000B0 RID: 176
			public static readonly ProfilerMarker validateSpriteSkinData = new ProfilerMarker("SpriteSkinComposite.ValidateSpriteSkinData");

			// Token: 0x040000B1 RID: 177
			public static readonly ProfilerMarker transformAccessJob = new ProfilerMarker("SpriteSkinComposite.TransformAccessJob");

			// Token: 0x040000B2 RID: 178
			public static readonly ProfilerMarker getSpriteSkinBatchData = new ProfilerMarker("SpriteSkinComposite.GetSpriteSkinBatchData");

			// Token: 0x040000B3 RID: 179
			public static readonly ProfilerMarker resizeBuffers = new ProfilerMarker("SpriteSkinComposite.ResizeBuffers");

			// Token: 0x040000B4 RID: 180
			public static readonly ProfilerMarker prepare = new ProfilerMarker("SpriteSkinComposite.Prepare");

			// Token: 0x040000B5 RID: 181
			public static readonly ProfilerMarker scheduleJobs = new ProfilerMarker("SpriteSkinComposite.ScheduleJobs");

			// Token: 0x040000B6 RID: 182
			public static readonly ProfilerMarker setBatchDeformableBufferAndLocalAABB = new ProfilerMarker("SpriteSkinComposite.SetBatchDeformableBufferAndLocalAABB");

			// Token: 0x040000B7 RID: 183
			public static readonly ProfilerMarker deactivateDeformableBuffer = new ProfilerMarker("SpriteSkinComposite.DeactivateDeformableBuffer");
		}
	}
}
