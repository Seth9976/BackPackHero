using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000032 RID: 50
	internal class RenderGraphResourceRegistry
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000AD58 File Offset: 0x00008F58
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000AD5F File Offset: 0x00008F5F
		internal static RenderGraphResourceRegistry current
		{
			get
			{
				return RenderGraphResourceRegistry.m_CurrentRegistry;
			}
			set
			{
				RenderGraphResourceRegistry.m_CurrentRegistry = value;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000AD68 File Offset: 0x00008F68
		internal RTHandle GetTexture(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			if (!textureHandle.IsValid())
			{
				return null;
			}
			TextureResource textureResource = this.GetTextureResource(in handle.handle);
			RTHandle graphicsResource = textureResource.graphicsResource;
			if (graphicsResource == null)
			{
				if (handle.fallBackResource != TextureHandle.nullHandle.handle)
				{
					return this.GetTextureResource(in handle.fallBackResource).graphicsResource;
				}
				if (!textureResource.imported)
				{
					throw new InvalidOperationException("Trying to use a texture (" + textureResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
				}
			}
			return graphicsResource;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		internal bool TextureNeedsFallback(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			return textureHandle.IsValid() && this.GetTextureResource(in handle.handle).NeedsFallBack();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AE20 File Offset: 0x00009020
		internal RendererList GetRendererList(in RendererListHandle handle)
		{
			RendererListHandle rendererListHandle = handle;
			if (!rendererListHandle.IsValid() || handle >= this.m_RendererListResources.size)
			{
				return RendererList.nullRendererList;
			}
			return this.m_RendererListResources[handle].rendererList;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000AE78 File Offset: 0x00009078
		internal ComputeBuffer GetComputeBuffer(in ComputeBufferHandle handle)
		{
			ComputeBufferHandle computeBufferHandle = handle;
			if (!computeBufferHandle.IsValid())
			{
				return null;
			}
			ComputeBuffer graphicsResource = this.GetComputeBufferResource(in handle.handle).graphicsResource;
			if (graphicsResource == null)
			{
				throw new InvalidOperationException("Trying to use a compute buffer ({bufferResource.GetName()}) that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
			return graphicsResource;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AEB6 File Offset: 0x000090B6
		private RenderGraphResourceRegistry()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AEF0 File Offset: 0x000090F0
		internal RenderGraphResourceRegistry(RenderGraphDebugParams renderGraphDebug, RenderGraphLogger frameInformationLogger)
		{
			this.m_RenderGraphDebug = renderGraphDebug;
			this.m_FrameInformationLogger = frameInformationLogger;
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i] = new RenderGraphResourceRegistry.RenderGraphResourcesData();
			}
			this.m_RenderGraphResources[0].createResourceCallback = new RenderGraphResourceRegistry.ResourceCallback(this.CreateTextureCallback);
			this.m_RenderGraphResources[0].releaseResourceCallback = new RenderGraphResourceRegistry.ResourceCallback(this.ReleaseTextureCallback);
			this.m_RenderGraphResources[0].pool = new TexturePool();
			this.m_RenderGraphResources[1].pool = new ComputeBufferPool();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AFB2 File Offset: 0x000091B2
		internal void BeginRenderGraph(int executionCount)
		{
			this.m_ExecutionCount = executionCount;
			ResourceHandle.NewFrame(executionCount);
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.Initialize("RenderGraph Resources");
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AFDE File Offset: 0x000091DE
		internal void BeginExecute(int currentFrameIndex)
		{
			this.m_CurrentFrameIndex = currentFrameIndex;
			this.ManageSharedRenderGraphResources();
			RenderGraphResourceRegistry.current = this;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000AFF3 File Offset: 0x000091F3
		internal void EndExecute()
		{
			RenderGraphResourceRegistry.current = null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AFFC File Offset: 0x000091FC
		private void CheckHandleValidity(in ResourceHandle res)
		{
			RenderGraphResourceType type = res.type;
			ResourceHandle resourceHandle = res;
			this.CheckHandleValidity(type, resourceHandle.index);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B024 File Offset: 0x00009224
		private void CheckHandleValidity(RenderGraphResourceType type, int index)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[(int)type].resourceArray;
			if (index >= resourceArray.size)
			{
				throw new ArgumentException(string.Format("Trying to access resource of type {0} with an invalid resource index {1}", type, index));
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B064 File Offset: 0x00009264
		internal unsafe void IncrementWriteCount(in ResourceHandle res)
		{
			this.CheckHandleValidity(in res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			resourceArray[resourceHandle.index]->IncrementWriteCount();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B0AC File Offset: 0x000092AC
		internal unsafe string GetRenderGraphResourceName(in ResourceHandle res)
		{
			this.CheckHandleValidity(in res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->GetName();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B0F3 File Offset: 0x000092F3
		internal unsafe string GetRenderGraphResourceName(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->GetName();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B118 File Offset: 0x00009318
		internal unsafe bool IsRenderGraphResourceImported(in ResourceHandle res)
		{
			this.CheckHandleValidity(in res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->imported;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B15F File Offset: 0x0000935F
		internal bool IsRenderGraphResourceShared(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return index < this.m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B17C File Offset: 0x0000937C
		internal unsafe bool IsGraphicsResourceCreated(in ResourceHandle res)
		{
			this.CheckHandleValidity(in res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->IsCreated();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B1C3 File Offset: 0x000093C3
		internal bool IsRendererListCreated(in RendererListHandle res)
		{
			return this.m_RendererListResources[res].rendererList.isValid;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B1E5 File Offset: 0x000093E5
		internal unsafe bool IsRenderGraphResourceImported(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->imported;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B208 File Offset: 0x00009408
		internal unsafe int GetRenderGraphResourceTransientIndex(in ResourceHandle res)
		{
			this.CheckHandleValidity(in res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->transientPassIndex;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B250 File Offset: 0x00009450
		internal TextureHandle ImportTexture(RTHandle rt)
		{
			TextureResource textureResource;
			int num = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.graphicsResource = rt;
			textureResource.imported = true;
			return new TextureHandle(num, false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B284 File Offset: 0x00009484
		internal unsafe TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[0];
			int sharedResourcesCount = renderGraphResourcesData.sharedResourcesCount;
			TextureResource textureResource = null;
			int num = -1;
			for (int i = 0; i < sharedResourcesCount; i++)
			{
				if (!renderGraphResourcesData.resourceArray[i]->shared)
				{
					textureResource = (TextureResource)(*renderGraphResourcesData.resourceArray[i]);
					num = i;
					break;
				}
			}
			if (textureResource == null)
			{
				num = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, false);
				renderGraphResourcesData.sharedResourcesCount++;
			}
			textureResource.imported = true;
			textureResource.shared = true;
			textureResource.sharedExplicitRelease = explicitRelease;
			textureResource.desc = desc;
			return new TextureHandle(num, true);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B330 File Offset: 0x00009530
		internal void ReleaseSharedTexture(TextureHandle texture)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[0];
			if (texture.handle >= renderGraphResourcesData.sharedResourcesCount)
			{
				throw new InvalidOperationException("Tried to release a non shared texture.");
			}
			if (texture.handle == renderGraphResourcesData.sharedResourcesCount - 1)
			{
				renderGraphResourcesData.sharedResourcesCount--;
			}
			TextureResource textureResource = this.GetTextureResource(in texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.Reset(null);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B3A0 File Offset: 0x000095A0
		internal TextureHandle ImportBackbuffer(RenderTargetIdentifier rt)
		{
			if (this.m_CurrentBackbuffer != null)
			{
				this.m_CurrentBackbuffer.SetTexture(rt);
			}
			else
			{
				this.m_CurrentBackbuffer = RTHandles.Alloc(rt, "Backbuffer");
			}
			TextureResource textureResource;
			int num = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.graphicsResource = this.m_CurrentBackbuffer;
			textureResource.imported = true;
			return new TextureHandle(num, false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B400 File Offset: 0x00009600
		internal TextureHandle CreateTexture(in TextureDesc desc, int transientPassIndex = -1)
		{
			this.ValidateTextureDesc(in desc);
			TextureResource textureResource;
			int num = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.desc = desc;
			textureResource.transientPassIndex = transientPassIndex;
			textureResource.requestFallBack = desc.fallBackToBlackTexture;
			return new TextureHandle(num, false);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B449 File Offset: 0x00009649
		internal int GetTextureResourceCount()
		{
			return this.m_RenderGraphResources[0].resourceArray.size;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B45D File Offset: 0x0000965D
		private unsafe TextureResource GetTextureResource(in ResourceHandle handle)
		{
			return (*this.m_RenderGraphResources[0].resourceArray[handle]) as TextureResource;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B482 File Offset: 0x00009682
		internal unsafe TextureDesc GetTextureResourceDesc(in ResourceHandle handle)
		{
			return ((*this.m_RenderGraphResources[0].resourceArray[handle]) as TextureResource).desc;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B4AC File Offset: 0x000096AC
		internal void ForceTextureClear(in ResourceHandle handle, Color clearColor)
		{
			this.GetTextureResource(in handle).desc.clearBuffer = true;
			this.GetTextureResource(in handle).desc.clearColor = clearColor;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B4D4 File Offset: 0x000096D4
		internal RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			this.ValidateRendererListDesc(in desc);
			DynamicArray<RendererListResource> rendererListResources = this.m_RendererListResources;
			RendererListResource rendererListResource = new RendererListResource(in desc);
			return new RendererListHandle(rendererListResources.Add(in rendererListResource));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B504 File Offset: 0x00009704
		internal ComputeBufferHandle ImportComputeBuffer(ComputeBuffer computeBuffer)
		{
			ComputeBufferResource computeBufferResource;
			int num = this.m_RenderGraphResources[1].AddNewRenderGraphResource<ComputeBufferResource>(out computeBufferResource, true);
			computeBufferResource.graphicsResource = computeBuffer;
			computeBufferResource.imported = true;
			return new ComputeBufferHandle(num, false);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B538 File Offset: 0x00009738
		internal ComputeBufferHandle CreateComputeBuffer(in ComputeBufferDesc desc, int transientPassIndex = -1)
		{
			this.ValidateComputeBufferDesc(in desc);
			ComputeBufferResource computeBufferResource;
			int num = this.m_RenderGraphResources[1].AddNewRenderGraphResource<ComputeBufferResource>(out computeBufferResource, true);
			computeBufferResource.desc = desc;
			computeBufferResource.transientPassIndex = transientPassIndex;
			return new ComputeBufferHandle(num, false);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B575 File Offset: 0x00009775
		internal unsafe ComputeBufferDesc GetComputeBufferResourceDesc(in ResourceHandle handle)
		{
			return ((*this.m_RenderGraphResources[1].resourceArray[handle]) as ComputeBufferResource).desc;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B59F File Offset: 0x0000979F
		internal int GetComputeBufferResourceCount()
		{
			return this.m_RenderGraphResources[1].resourceArray.size;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B5B3 File Offset: 0x000097B3
		private unsafe ComputeBufferResource GetComputeBufferResource(in ResourceHandle handle)
		{
			return (*this.m_RenderGraphResources[1].resourceArray[handle]) as ComputeBufferResource;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B5D8 File Offset: 0x000097D8
		internal unsafe void UpdateSharedResourceLastFrameIndex(int type, int index)
		{
			this.m_RenderGraphResources[type].resourceArray[index]->sharedResourceLastFrameUsed = this.m_ExecutionCount;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B5FC File Offset: 0x000097FC
		private unsafe void ManageSharedRenderGraphResources()
		{
			for (int i = 0; i < 2; i++)
			{
				RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[i];
				for (int j = 0; j < renderGraphResourcesData.sharedResourcesCount; j++)
				{
					IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[i].resourceArray[j];
					bool flag = renderGraphResource.IsCreated();
					if (renderGraphResource.sharedResourceLastFrameUsed == this.m_ExecutionCount && !flag)
					{
						renderGraphResource.CreateGraphicsResource(renderGraphResource.GetName());
					}
					else if (flag && !renderGraphResource.sharedExplicitRelease && renderGraphResource.sharedResourceLastFrameUsed + 30 < this.m_ExecutionCount)
					{
						renderGraphResource.ReleaseGraphicsResource();
					}
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B694 File Offset: 0x00009894
		internal unsafe void CreatePooledResource(RenderGraphContext rgContext, int type, int index)
		{
			IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				renderGraphResource.CreatePooledGraphicsResource();
				if (this.m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogCreation(this.m_FrameInformationLogger);
				}
				RenderGraphResourceRegistry.ResourceCallback createResourceCallback = this.m_RenderGraphResources[type].createResourceCallback;
				if (createResourceCallback == null)
				{
					return;
				}
				createResourceCallback(rgContext, renderGraphResource);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private void CreateTextureCallback(RenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource textureResource = res as TextureResource;
			FastMemoryDesc fastMemoryDesc = textureResource.desc.fastMemoryDesc;
			if (fastMemoryDesc.inFastMemory)
			{
				textureResource.graphicsResource.SwitchToFastMemory(rgContext.cmd, fastMemoryDesc.residencyFraction, fastMemoryDesc.flags, false);
			}
			if (textureResource.desc.clearBuffer || this.m_RenderGraphDebug.clearRenderTargetsAtCreation)
			{
				bool flag = this.m_RenderGraphDebug.clearRenderTargetsAtCreation && !textureResource.desc.clearBuffer;
				using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(flag ? RenderGraphProfileId.RenderGraphClearDebug : RenderGraphProfileId.RenderGraphClear)))
				{
					ClearFlag clearFlag = ((textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.DepthStencil : ClearFlag.Color);
					Color color = (flag ? Color.magenta : textureResource.desc.clearColor);
					CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.graphicsResource, clearFlag, color, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B7F8 File Offset: 0x000099F8
		internal unsafe void ReleasePooledResource(RenderGraphContext rgContext, int type, int index)
		{
			IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback = this.m_RenderGraphResources[type].releaseResourceCallback;
				if (releaseResourceCallback != null)
				{
					releaseResourceCallback(rgContext, renderGraphResource);
				}
				if (this.m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogRelease(this.m_FrameInformationLogger);
				}
				renderGraphResource.ReleasePooledGraphicsResource(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B864 File Offset: 0x00009A64
		private void ReleaseTextureCallback(RenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource textureResource = res as TextureResource;
			if (this.m_RenderGraphDebug.clearRenderTargetsAtRelease)
			{
				using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.RenderGraphClearDebug)))
				{
					ClearFlag clearFlag = ((textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.DepthStencil : ClearFlag.Color);
					CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.graphicsResource, clearFlag, Color.magenta, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		private void ValidateTextureDesc(in TextureDesc desc)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B8E6 File Offset: 0x00009AE6
		private void ValidateRendererListDesc(in RendererListDesc desc)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		private void ValidateComputeBufferDesc(in ComputeBufferDesc desc)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B8EC File Offset: 0x00009AEC
		internal void CreateRendererLists(List<RendererListHandle> rendererLists, ScriptableRenderContext context, bool manualDispatch = false)
		{
			this.m_ActiveRendererLists.Clear();
			foreach (RendererListHandle rendererListHandle in rendererLists)
			{
				ref RendererListResource ptr = ref this.m_RendererListResources[rendererListHandle];
				ref RendererListDesc ptr2 = ref ptr.desc;
				ptr.rendererList = context.CreateRendererList(ptr2);
				this.m_ActiveRendererLists.Add(ptr.rendererList);
			}
			if (manualDispatch)
			{
				context.PrepareRendererListsAsync(this.m_ActiveRendererLists);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B98C File Offset: 0x00009B8C
		internal void Clear(bool onException)
		{
			this.LogResources();
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].Clear(onException, this.m_CurrentFrameIndex);
			}
			this.m_RendererListResources.Clear();
			this.m_ActiveRendererLists.Clear();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		internal void PurgeUnusedGraphicsResources()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].PurgeUnusedGraphicsResources(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BA04 File Offset: 0x00009C04
		internal void Cleanup()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].Cleanup();
			}
			RTHandles.Release(this.m_CurrentBackbuffer);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000BA35 File Offset: 0x00009C35
		internal void FlushLogs()
		{
			Debug.Log(this.m_ResourceLogger.GetAllLogs());
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000BA48 File Offset: 0x00009C48
		private void LogResources()
		{
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.LogLine("==== Allocated Resources ====\n", Array.Empty<object>());
				for (int i = 0; i < 2; i++)
				{
					this.m_RenderGraphResources[i].pool.LogResources(this.m_ResourceLogger);
					this.m_ResourceLogger.LogLine("", Array.Empty<object>());
				}
			}
		}

		// Token: 0x04000139 RID: 313
		private const int kSharedResourceLifetime = 30;

		// Token: 0x0400013A RID: 314
		private static RenderGraphResourceRegistry m_CurrentRegistry;

		// Token: 0x0400013B RID: 315
		private RenderGraphResourceRegistry.RenderGraphResourcesData[] m_RenderGraphResources = new RenderGraphResourceRegistry.RenderGraphResourcesData[2];

		// Token: 0x0400013C RID: 316
		private DynamicArray<RendererListResource> m_RendererListResources = new DynamicArray<RendererListResource>();

		// Token: 0x0400013D RID: 317
		private RenderGraphDebugParams m_RenderGraphDebug;

		// Token: 0x0400013E RID: 318
		private RenderGraphLogger m_ResourceLogger = new RenderGraphLogger();

		// Token: 0x0400013F RID: 319
		private RenderGraphLogger m_FrameInformationLogger;

		// Token: 0x04000140 RID: 320
		private int m_CurrentFrameIndex;

		// Token: 0x04000141 RID: 321
		private int m_ExecutionCount;

		// Token: 0x04000142 RID: 322
		private RTHandle m_CurrentBackbuffer;

		// Token: 0x04000143 RID: 323
		private const int kInitialRendererListCount = 256;

		// Token: 0x04000144 RID: 324
		private List<RendererList> m_ActiveRendererLists = new List<RendererList>(256);

		// Token: 0x02000136 RID: 310
		// (Invoke) Token: 0x06000827 RID: 2087
		private delegate void ResourceCallback(RenderGraphContext rgContext, IRenderGraphResource res);

		// Token: 0x02000137 RID: 311
		private class RenderGraphResourcesData
		{
			// Token: 0x0600082A RID: 2090 RVA: 0x00022E3A File Offset: 0x0002103A
			public void Clear(bool onException, int frameIndex)
			{
				this.resourceArray.Resize(this.sharedResourcesCount, false);
				this.pool.CheckFrameAllocation(onException, frameIndex);
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00022E5C File Offset: 0x0002105C
			public unsafe void Cleanup()
			{
				for (int i = 0; i < this.sharedResourcesCount; i++)
				{
					IRenderGraphResource renderGraphResource = *this.resourceArray[i];
					if (renderGraphResource != null)
					{
						renderGraphResource.ReleaseGraphicsResource();
					}
				}
				this.pool.Cleanup();
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x00022E9C File Offset: 0x0002109C
			public void PurgeUnusedGraphicsResources(int frameIndex)
			{
				this.pool.PurgeUnusedResources(frameIndex);
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x00022EAC File Offset: 0x000210AC
			public unsafe int AddNewRenderGraphResource<ResType>(out ResType outRes, bool pooledResource = true) where ResType : IRenderGraphResource, new()
			{
				int size = this.resourceArray.size;
				this.resourceArray.Resize(this.resourceArray.size + 1, true);
				if (*this.resourceArray[size] == null)
				{
					*this.resourceArray[size] = new ResType();
				}
				outRes = (*this.resourceArray[size]) as ResType;
				outRes.Reset(pooledResource ? this.pool : null);
				return size;
			}

			// Token: 0x040004EF RID: 1263
			public DynamicArray<IRenderGraphResource> resourceArray = new DynamicArray<IRenderGraphResource>();

			// Token: 0x040004F0 RID: 1264
			public int sharedResourcesCount;

			// Token: 0x040004F1 RID: 1265
			public IRenderGraphResourcePool pool;

			// Token: 0x040004F2 RID: 1266
			public RenderGraphResourceRegistry.ResourceCallback createResourceCallback;

			// Token: 0x040004F3 RID: 1267
			public RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback;
		}
	}
}
