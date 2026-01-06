using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000022 RID: 34
	public class RenderGraph
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000730B File Offset: 0x0000550B
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007313 File Offset: 0x00005513
		public string name { get; private set; } = "RenderGraph";

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000731C File Offset: 0x0000551C
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00007323 File Offset: 0x00005523
		internal static bool requireDebugData { get; set; } = false;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000732B File Offset: 0x0000552B
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.m_DefaultResources;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007334 File Offset: 0x00005534
		public RenderGraph(string name = "RenderGraph")
		{
			this.name = name;
			this.m_Resources = new RenderGraphResourceRegistry(this.m_DebugParameters, this.m_FrameInformationLogger);
			for (int i = 0; i < 2; i++)
			{
				this.m_CompiledResourcesInfos[i] = new DynamicArray<RenderGraph.CompiledResourceInfo>();
			}
			RenderGraph.s_RegisteredGraphs.Add(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphRegistered;
			if (onGraphRegisteredDelegate == null)
			{
				return;
			}
			onGraphRegisteredDelegate(this);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007439 File Offset: 0x00005639
		public void Cleanup()
		{
			this.m_Resources.Cleanup();
			this.m_DefaultResources.Cleanup();
			this.m_RenderGraphPool.Cleanup();
			RenderGraph.s_RegisteredGraphs.Remove(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphUnregistered;
			if (onGraphRegisteredDelegate == null)
			{
				return;
			}
			onGraphRegisteredDelegate(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007478 File Offset: 0x00005678
		public void RegisterDebug(DebugUI.Panel panel = null)
		{
			this.m_DebugParameters.RegisterDebug(this.name, panel);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000748C File Offset: 0x0000568C
		public void UnRegisterDebug()
		{
			this.m_DebugParameters.UnRegisterDebug(this.name);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000749F File Offset: 0x0000569F
		public static List<RenderGraph> GetRegisteredRenderGraphs()
		{
			return RenderGraph.s_RegisteredGraphs;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000074A8 File Offset: 0x000056A8
		internal RenderGraphDebugData GetDebugData(string executionName)
		{
			RenderGraphDebugData renderGraphDebugData;
			if (this.m_DebugData.TryGetValue(executionName, out renderGraphDebugData))
			{
				return renderGraphDebugData;
			}
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000074C8 File Offset: 0x000056C8
		public void EndFrame()
		{
			this.m_Resources.PurgeUnusedGraphicsResources();
			if (this.m_DebugParameters.logFrameInformation)
			{
				Debug.Log(this.m_FrameInformationLogger.GetAllLogs());
				this.m_DebugParameters.logFrameInformation = false;
			}
			if (this.m_DebugParameters.logResources)
			{
				this.m_Resources.FlushLogs();
				this.m_DebugParameters.logResources = false;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000752D File Offset: 0x0000572D
		public TextureHandle ImportTexture(RTHandle rt)
		{
			return this.m_Resources.ImportTexture(rt);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000753B File Offset: 0x0000573B
		public TextureHandle ImportBackbuffer(RenderTargetIdentifier rt)
		{
			return this.m_Resources.ImportBackbuffer(rt);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007549 File Offset: 0x00005749
		public TextureHandle CreateTexture(in TextureDesc desc)
		{
			return this.m_Resources.CreateTexture(in desc, -1);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007558 File Offset: 0x00005758
		public TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease = false)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be created outside of render graph execution.");
			}
			return this.m_Resources.CreateSharedTexture(in desc, explicitRelease);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000757A File Offset: 0x0000577A
		public void ReleaseSharedTexture(TextureHandle texture)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be release outside of render graph execution.");
			}
			this.m_Resources.ReleaseSharedTexture(texture);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000759C File Offset: 0x0000579C
		public TextureHandle CreateTexture(TextureHandle texture)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(in texture.handle);
			return resources.CreateTexture(in textureResourceDesc, -1);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000075CA File Offset: 0x000057CA
		public void CreateTextureIfInvalid(in TextureDesc desc, ref TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				texture = this.m_Resources.CreateTexture(in desc, -1);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000075E7 File Offset: 0x000057E7
		public TextureDesc GetTextureDesc(TextureHandle texture)
		{
			return this.m_Resources.GetTextureResourceDesc(in texture.handle);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000075FB File Offset: 0x000057FB
		public RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			return this.m_Resources.CreateRendererList(in desc);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007609 File Offset: 0x00005809
		public ComputeBufferHandle ImportComputeBuffer(ComputeBuffer computeBuffer)
		{
			return this.m_Resources.ImportComputeBuffer(computeBuffer);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007617 File Offset: 0x00005817
		public ComputeBufferHandle CreateComputeBuffer(in ComputeBufferDesc desc)
		{
			return this.m_Resources.CreateComputeBuffer(in desc, -1);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007628 File Offset: 0x00005828
		public ComputeBufferHandle CreateComputeBuffer(in ComputeBufferHandle computeBuffer)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			ComputeBufferDesc computeBufferResourceDesc = this.m_Resources.GetComputeBufferResourceDesc(in computeBuffer.handle);
			return resources.CreateComputeBuffer(in computeBufferResourceDesc, -1);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007655 File Offset: 0x00005855
		public ComputeBufferDesc GetComputeBufferDesc(in ComputeBufferHandle computeBuffer)
		{
			return this.m_Resources.GetComputeBufferResourceDesc(in computeBuffer.handle);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007668 File Offset: 0x00005868
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData, ProfilingSampler sampler) where PassData : class, new()
		{
			RenderGraphPass<PassData> renderGraphPass = this.m_RenderGraphPool.Get<RenderGraphPass<PassData>>();
			renderGraphPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, sampler);
			passData = renderGraphPass.data;
			this.m_RenderPasses.Add(renderGraphPass);
			return new RenderGraphBuilder(renderGraphPass, this.m_Resources, this);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000076C4 File Offset: 0x000058C4
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData) where PassData : class, new()
		{
			return this.AddRenderPass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000076D8 File Offset: 0x000058D8
		public RenderGraphExecution RecordAndExecute(in RenderGraphParameters parameters)
		{
			this.m_CurrentFrameIndex = parameters.currentFrameIndex;
			this.m_CurrentExecutionName = ((parameters.executionName != null) ? parameters.executionName : "RenderGraphExecution");
			this.m_HasRenderGraphBegun = true;
			RenderGraphResourceRegistry resources = this.m_Resources;
			int executionCount = this.m_ExecutionCount;
			this.m_ExecutionCount = executionCount + 1;
			resources.BeginRenderGraph(executionCount);
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.Initialize(this.m_CurrentExecutionName);
			}
			this.m_DefaultResources.InitializeForRendering(this);
			this.m_RenderGraphContext.cmd = parameters.commandBuffer;
			this.m_RenderGraphContext.renderContext = parameters.scriptableRenderContext;
			this.m_RenderGraphContext.renderGraphPool = this.m_RenderGraphPool;
			this.m_RenderGraphContext.defaultResources = this.m_DefaultResources;
			if (this.m_DebugParameters.immediateMode)
			{
				this.LogFrameInformation();
				this.m_CompiledPassInfos.Resize(this.m_CompiledPassInfos.capacity, false);
				this.m_CurrentImmediatePassIndex = 0;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_ImmediateModeResourceList[i] == null)
					{
						this.m_ImmediateModeResourceList[i] = new List<int>();
					}
					this.m_ImmediateModeResourceList[i].Clear();
				}
				this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
			}
			return new RenderGraphExecution(this);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007818 File Offset: 0x00005A18
		internal void Execute()
		{
			this.m_ExecutionExceptionWasRaised = false;
			try
			{
				if (this.m_RenderGraphContext.cmd == null)
				{
					throw new InvalidOperationException("RenderGraph.RecordAndExecute was not called before executing the render graph.");
				}
				if (!this.m_DebugParameters.immediateMode)
				{
					this.LogFrameInformation();
					this.CompileRenderGraph();
					this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
					this.ExecuteRenderGraph();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Render Graph Execution error");
				if (!this.m_ExecutionExceptionWasRaised)
				{
					Debug.LogException(ex);
				}
				this.m_ExecutionExceptionWasRaised = true;
			}
			finally
			{
				this.GenerateDebugData();
				if (this.m_DebugParameters.immediateMode)
				{
					this.ReleaseImmediateModeResources();
				}
				this.ClearCompiledGraph();
				this.m_Resources.EndExecute();
				this.InvalidateContext();
				this.m_HasRenderGraphBegun = false;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000078EC File Offset: 0x00005AEC
		public void BeginProfilingSampler(ProfilingSampler sampler)
		{
			RenderGraph.ProfilingScopePassData profilingScopePassData;
			using (RenderGraphBuilder renderGraphBuilder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("BeginProfile", out profilingScopePassData, null))
			{
				profilingScopePassData.sampler = sampler;
				renderGraphBuilder.AllowPassCulling(false);
				renderGraphBuilder.GenerateDebugData(false);
				renderGraphBuilder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.Begin(ctx.cmd);
				});
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007968 File Offset: 0x00005B68
		public void EndProfilingSampler(ProfilingSampler sampler)
		{
			RenderGraph.ProfilingScopePassData profilingScopePassData;
			using (RenderGraphBuilder renderGraphBuilder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("EndProfile", out profilingScopePassData, null))
			{
				profilingScopePassData.sampler = sampler;
				renderGraphBuilder.AllowPassCulling(false);
				renderGraphBuilder.GenerateDebugData(false);
				renderGraphBuilder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.End(ctx.cmd);
				});
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000079E4 File Offset: 0x00005BE4
		internal DynamicArray<RenderGraph.CompiledPassInfo> GetCompiledPassInfos()
		{
			return this.m_CompiledPassInfos;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000079EC File Offset: 0x00005BEC
		internal void ClearCompiledGraph()
		{
			this.ClearRenderPasses();
			this.m_Resources.Clear(this.m_ExecutionExceptionWasRaised);
			this.m_RendererLists.Clear();
			for (int i = 0; i < 2; i++)
			{
				this.m_CompiledResourcesInfos[i].Clear();
			}
			this.m_CompiledPassInfos.Clear();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007A3F File Offset: 0x00005C3F
		private void InvalidateContext()
		{
			this.m_RenderGraphContext.cmd = null;
			this.m_RenderGraphContext.renderGraphPool = null;
			this.m_RenderGraphContext.defaultResources = null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007A65 File Offset: 0x00005C65
		internal void OnPassAdded(RenderGraphPass pass)
		{
			if (this.m_DebugParameters.immediateMode)
			{
				this.ExecutePassImmediatly(pass);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000111 RID: 273 RVA: 0x00007A7C File Offset: 0x00005C7C
		// (remove) Token: 0x06000112 RID: 274 RVA: 0x00007AB0 File Offset: 0x00005CB0
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphRegistered;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000113 RID: 275 RVA: 0x00007AE4 File Offset: 0x00005CE4
		// (remove) Token: 0x06000114 RID: 276 RVA: 0x00007B18 File Offset: 0x00005D18
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000115 RID: 277 RVA: 0x00007B4C File Offset: 0x00005D4C
		// (remove) Token: 0x06000116 RID: 278 RVA: 0x00007B80 File Offset: 0x00005D80
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000117 RID: 279 RVA: 0x00007BB4 File Offset: 0x00005DB4
		// (remove) Token: 0x06000118 RID: 280 RVA: 0x00007BE8 File Offset: 0x00005DE8
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered;

		// Token: 0x06000119 RID: 281 RVA: 0x00007C1C File Offset: 0x00005E1C
		private void InitResourceInfosData(DynamicArray<RenderGraph.CompiledResourceInfo> resourceInfos, int count)
		{
			resourceInfos.Resize(count, false);
			for (int i = 0; i < resourceInfos.size; i++)
			{
				resourceInfos[i].Reset();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007C50 File Offset: 0x00005E50
		private void InitializeCompilationData()
		{
			this.InitResourceInfosData(this.m_CompiledResourcesInfos[0], this.m_Resources.GetTextureResourceCount());
			this.InitResourceInfosData(this.m_CompiledResourcesInfos[1], this.m_Resources.GetComputeBufferResourceCount());
			this.m_CompiledPassInfos.Resize(this.m_RenderPasses.Count, false);
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				this.m_CompiledPassInfos[i].Reset(this.m_RenderPasses[i]);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007CDC File Offset: 0x00005EDC
		private void CountReferences()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				for (int j = 0; j < 2; j++)
				{
					foreach (ResourceHandle resourceHandle in ptr.pass.resourceReadLists[j])
					{
						ref RenderGraph.CompiledResourceInfo ptr2 = ref this.m_CompiledResourcesInfos[j][resourceHandle];
						ptr2.imported = this.m_Resources.IsRenderGraphResourceImported(in resourceHandle);
						ptr2.consumers.Add(i);
						ptr2.refCount++;
					}
					foreach (ResourceHandle resourceHandle2 in ptr.pass.resourceWriteLists[j])
					{
						ref RenderGraph.CompiledResourceInfo ptr3 = ref this.m_CompiledResourcesInfos[j][resourceHandle2];
						ptr3.imported = this.m_Resources.IsRenderGraphResourceImported(in resourceHandle2);
						ptr3.producers.Add(i);
						ptr.hasSideEffect = ptr3.imported;
						ptr.refCount++;
					}
					foreach (ResourceHandle resourceHandle3 in ptr.pass.transientResourceList[j])
					{
						int num = resourceHandle3;
						ref RenderGraph.CompiledResourceInfo ptr4 = ref this.m_CompiledResourcesInfos[j][num];
						ptr4.refCount++;
						ptr4.consumers.Add(i);
						ptr4.producers.Add(i);
					}
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007EB4 File Offset: 0x000060B4
		private unsafe void CullUnusedPasses()
		{
			if (this.m_DebugParameters.disablePassCulling)
			{
				if (this.m_DebugParameters.enableLogging)
				{
					this.m_FrameInformationLogger.LogLine("- Pass Culling Disabled -\n", Array.Empty<object>());
				}
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray = this.m_CompiledResourcesInfos[i];
				this.m_CullingStack.Clear();
				for (int j = 0; j < dynamicArray.size; j++)
				{
					if (dynamicArray[j].refCount == 0)
					{
						this.m_CullingStack.Push(j);
					}
				}
				while (this.m_CullingStack.Count != 0)
				{
					foreach (int num in dynamicArray[this.m_CullingStack.Pop()]->producers)
					{
						ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[num];
						ptr.refCount--;
						if (ptr.refCount == 0 && !ptr.hasSideEffect && ptr.allowPassCulling)
						{
							ptr.culled = true;
							foreach (ResourceHandle resourceHandle in ptr.pass.resourceReadLists[i])
							{
								ref RenderGraph.CompiledResourceInfo ptr2 = ref dynamicArray[resourceHandle];
								ptr2.refCount--;
								if (ptr2.refCount == 0)
								{
									this.m_CullingStack.Push(resourceHandle);
								}
							}
						}
					}
				}
			}
			this.LogCulledPasses();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008070 File Offset: 0x00006270
		private void UpdatePassSynchronization(ref RenderGraph.CompiledPassInfo currentPassInfo, ref RenderGraph.CompiledPassInfo producerPassInfo, int currentPassIndex, int lastProducer, ref int intLastSyncIndex)
		{
			currentPassInfo.syncToPassIndex = lastProducer;
			intLastSyncIndex = lastProducer;
			producerPassInfo.needGraphicsFence = true;
			if (producerPassInfo.syncFromPassIndex == -1)
			{
				producerPassInfo.syncFromPassIndex = currentPassIndex;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008098 File Offset: 0x00006298
		private void UpdateResourceSynchronization(ref int lastGraphicsPipeSync, ref int lastComputePipeSync, int currentPassIndex, in RenderGraph.CompiledResourceInfo resource)
		{
			int latestProducerIndex = this.GetLatestProducerIndex(currentPassIndex, in resource);
			if (latestProducerIndex != -1)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[currentPassIndex];
				if (this.m_CompiledPassInfos[latestProducerIndex].enableAsyncCompute != ptr.enableAsyncCompute)
				{
					if (ptr.enableAsyncCompute)
					{
						if (latestProducerIndex > lastGraphicsPipeSync)
						{
							this.UpdatePassSynchronization(ref ptr, this.m_CompiledPassInfos[latestProducerIndex], currentPassIndex, latestProducerIndex, ref lastGraphicsPipeSync);
							return;
						}
					}
					else if (latestProducerIndex > lastComputePipeSync)
					{
						this.UpdatePassSynchronization(ref ptr, this.m_CompiledPassInfos[latestProducerIndex], currentPassIndex, latestProducerIndex, ref lastComputePipeSync);
					}
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008118 File Offset: 0x00006318
		private int GetLatestProducerIndex(int passIndex, in RenderGraph.CompiledResourceInfo info)
		{
			int num = -1;
			foreach (int num2 in info.producers)
			{
				if (num2 >= passIndex)
				{
					return num;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008178 File Offset: 0x00006378
		private int GetLatestValidReadIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.consumers.Count == 0)
			{
				return -1;
			}
			List<int> consumers = info.consumers;
			for (int i = consumers.Count - 1; i >= 0; i--)
			{
				if (!this.m_CompiledPassInfos[consumers[i]].culled)
				{
					return consumers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000081D0 File Offset: 0x000063D0
		private int GetFirstValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			List<int> producers = info.producers;
			for (int i = 0; i < producers.Count; i++)
			{
				if (!this.m_CompiledPassInfos[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008228 File Offset: 0x00006428
		private int GetLatestValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			List<int> producers = info.producers;
			for (int i = producers.Count - 1; i >= 0; i--)
			{
				if (!this.m_CompiledPassInfos[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008280 File Offset: 0x00006480
		private void CreateRendererLists()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				if (!ptr.culled)
				{
					this.m_RendererLists.AddRange(ptr.pass.usedRendererListList);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, this.m_RendererListCulling);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000082F0 File Offset: 0x000064F0
		private unsafe void UpdateResourceAllocationAndSynchronization()
		{
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				if (!ptr.culled)
				{
					for (int j = 0; j < 2; j++)
					{
						DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray = this.m_CompiledResourcesInfos[j];
						foreach (ResourceHandle resourceHandle in ptr.pass.resourceReadLists[j])
						{
							int num3 = resourceHandle;
							this.UpdateResourceSynchronization(ref num, ref num2, i, dynamicArray[num3]);
						}
						foreach (ResourceHandle resourceHandle2 in ptr.pass.resourceWriteLists[j])
						{
							int num4 = resourceHandle2;
							this.UpdateResourceSynchronization(ref num, ref num2, i, dynamicArray[num4]);
						}
					}
				}
			}
			for (int k = 0; k < 2; k++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray2 = this.m_CompiledResourcesInfos[k];
				for (int l = 0; l < dynamicArray2.size; l++)
				{
					RenderGraph.CompiledResourceInfo compiledResourceInfo = *dynamicArray2[l];
					bool flag = this.m_Resources.IsRenderGraphResourceShared((RenderGraphResourceType)k, l);
					if (!compiledResourceInfo.imported || flag)
					{
						int firstValidWriteIndex = this.GetFirstValidWriteIndex(in compiledResourceInfo);
						if (firstValidWriteIndex != -1)
						{
							this.m_CompiledPassInfos[firstValidWriteIndex].resourceCreateList[k].Add(l);
						}
						int latestValidReadIndex = this.GetLatestValidReadIndex(in compiledResourceInfo);
						int latestValidWriteIndex = this.GetLatestValidWriteIndex(in compiledResourceInfo);
						int num5 = ((firstValidWriteIndex != -1 || compiledResourceInfo.imported) ? Math.Max(latestValidWriteIndex, latestValidReadIndex) : (-1));
						if (num5 != -1)
						{
							if (this.m_CompiledPassInfos[num5].enableAsyncCompute)
							{
								int num6 = num5;
								int num7 = this.m_CompiledPassInfos[num6].syncFromPassIndex;
								while (num7 == -1 && num6++ < this.m_CompiledPassInfos.size - 1)
								{
									if (this.m_CompiledPassInfos[num6].enableAsyncCompute)
									{
										num7 = this.m_CompiledPassInfos[num6].syncFromPassIndex;
									}
								}
								if (num6 == this.m_CompiledPassInfos.size)
								{
									if (!this.m_CompiledPassInfos[num5].hasSideEffect)
									{
										RenderGraphPass renderGraphPass = this.m_RenderPasses[num5];
										string text = "<unknown>";
										throw new InvalidOperationException(string.Format("{0} resource '{1}' in asynchronous pass '{2}' is missing synchronization on the graphics pipeline.", (RenderGraphResourceType)k, text, renderGraphPass.name));
									}
									num7 = num6;
								}
								int num8 = Math.Max(0, num7 - 1);
								while (this.m_CompiledPassInfos[num8].culled)
								{
									num8 = Math.Max(0, num8 - 1);
								}
								this.m_CompiledPassInfos[num8].resourceReleaseList[k].Add(l);
							}
							else
							{
								this.m_CompiledPassInfos[num5].resourceReleaseList[k].Add(l);
							}
						}
						if (flag && (firstValidWriteIndex != -1 || num5 != -1))
						{
							this.m_Resources.UpdateSharedResourceLastFrameIndex(k, l);
						}
					}
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008644 File Offset: 0x00006844
		private bool AreRendererListsEmpty(List<RendererListHandle> rendererLists)
		{
			foreach (RendererListHandle rendererListHandle in rendererLists)
			{
				RendererList rendererList = this.m_Resources.GetRendererList(in rendererListHandle);
				if (this.m_RenderGraphContext.renderContext.QueryRendererListStatus(rendererList) == RendererListStatus.kRendererListPopulated)
				{
					return false;
				}
			}
			return rendererLists.Count > 0;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000086C0 File Offset: 0x000068C0
		private void TryCullPassAtIndex(int passIndex)
		{
			RenderGraphPass pass = this.m_CompiledPassInfos[passIndex].pass;
			if (!this.m_CompiledPassInfos[passIndex].culled && pass.allowPassCulling && pass.allowRendererListCulling && !this.m_CompiledPassInfos[passIndex].hasSideEffect && (this.AreRendererListsEmpty(pass.usedRendererListList) || this.AreRendererListsEmpty(pass.dependsOnRendererListList)))
			{
				this.m_CompiledPassInfos[passIndex].culled = true;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008744 File Offset: 0x00006944
		private void CullRendererLists()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				if (!this.m_CompiledPassInfos[i].culled && !this.m_CompiledPassInfos[i].hasSideEffect)
				{
					RenderGraphPass pass = this.m_CompiledPassInfos[i].pass;
					if (pass.usedRendererListList.Count > 0 || pass.dependsOnRendererListList.Count > 0)
					{
						this.TryCullPassAtIndex(i);
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000087C4 File Offset: 0x000069C4
		internal void CompileRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.CompileRenderGraph)))
			{
				this.InitializeCompilationData();
				this.CountReferences();
				this.CullUnusedPasses();
				this.CreateRendererLists();
				if (this.m_RendererListCulling)
				{
					this.CullRendererLists();
				}
				this.UpdateResourceAllocationAndSynchronization();
				this.LogRendererListsCreation();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000883C File Offset: 0x00006A3C
		private ref RenderGraph.CompiledPassInfo CompilePassImmediatly(RenderGraphPass pass)
		{
			if (this.m_CurrentImmediatePassIndex >= this.m_CompiledPassInfos.size)
			{
				this.m_CompiledPassInfos.Resize(this.m_CompiledPassInfos.size * 2, false);
			}
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfos = this.m_CompiledPassInfos;
			int currentImmediatePassIndex = this.m_CurrentImmediatePassIndex;
			this.m_CurrentImmediatePassIndex = currentImmediatePassIndex + 1;
			ref RenderGraph.CompiledPassInfo ptr = ref compiledPassInfos[currentImmediatePassIndex];
			ptr.Reset(pass);
			ptr.enableAsyncCompute = false;
			for (int i = 0; i < 2; i++)
			{
				foreach (ResourceHandle resourceHandle in pass.resourceWriteLists[i])
				{
					if (!this.m_Resources.IsGraphicsResourceCreated(in resourceHandle))
					{
						ptr.resourceCreateList[i].Add(resourceHandle);
						this.m_ImmediateModeResourceList[i].Add(resourceHandle);
					}
				}
				foreach (ResourceHandle resourceHandle2 in pass.transientResourceList[i])
				{
					ptr.resourceCreateList[i].Add(resourceHandle2);
					ptr.resourceReleaseList[i].Add(resourceHandle2);
				}
				foreach (ResourceHandle resourceHandle3 in pass.resourceReadLists[i])
				{
				}
			}
			foreach (RendererListHandle rendererListHandle in pass.usedRendererListList)
			{
				if (!this.m_Resources.IsRendererListCreated(in rendererListHandle))
				{
					this.m_RendererLists.Add(rendererListHandle);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, false);
			this.m_RendererLists.Clear();
			return ref ptr;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008A50 File Offset: 0x00006C50
		private void ExecutePassImmediatly(RenderGraphPass pass)
		{
			this.ExecuteCompiledPass(this.CompilePassImmediatly(pass), this.m_CurrentImmediatePassIndex - 1);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008A68 File Offset: 0x00006C68
		private void ExecuteCompiledPass(ref RenderGraph.CompiledPassInfo passInfo, int passIndex)
		{
			if (passInfo.culled)
			{
				return;
			}
			if (!passInfo.pass.HasRenderFunc())
			{
				throw new InvalidOperationException(string.Format("RenderPass {0} was not provided with an execute function.", passInfo.pass.name));
			}
			try
			{
				using (new ProfilingScope(this.m_RenderGraphContext.cmd, passInfo.pass.customSampler))
				{
					this.LogRenderPassBegin(in passInfo);
					using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
					{
						this.PreRenderPassExecute(in passInfo, this.m_RenderGraphContext);
						passInfo.pass.Execute(this.m_RenderGraphContext);
						this.PostRenderPassExecute(ref passInfo, this.m_RenderGraphContext);
					}
				}
			}
			catch (Exception ex)
			{
				this.m_ExecutionExceptionWasRaised = true;
				Debug.LogError(string.Format("Render Graph Execution error at pass {0} ({1})", passInfo.pass.name, passIndex));
				Debug.LogException(ex);
				throw;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008B7C File Offset: 0x00006D7C
		private void ExecuteRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.ExecuteRenderGraph)))
			{
				for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
				{
					this.ExecuteCompiledPass(this.m_CompiledPassInfos[i], i);
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008BEC File Offset: 0x00006DEC
		private void PreRenderPassSetRenderTargets(in RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			RenderGraphPass pass = passInfo.pass;
			TextureHandle textureHandle = pass.depthBuffer;
			if (textureHandle.IsValid() || pass.colorBufferMaxIndex != -1)
			{
				RenderTargetIdentifier[] tempArray = rgContext.renderGraphPool.GetTempArray<RenderTargetIdentifier>(pass.colorBufferMaxIndex + 1);
				TextureHandle[] colorBuffers = pass.colorBuffers;
				if (pass.colorBufferMaxIndex > 0)
				{
					for (int i = 0; i <= pass.colorBufferMaxIndex; i++)
					{
						if (!colorBuffers[i].IsValid())
						{
							throw new InvalidOperationException("MRT setup is invalid. Some indices are not used.");
						}
						tempArray[i] = this.m_Resources.GetTexture(in colorBuffers[i]);
					}
					textureHandle = pass.depthBuffer;
					if (textureHandle.IsValid())
					{
						CommandBuffer cmd = rgContext.cmd;
						RenderTargetIdentifier[] array = tempArray;
						RenderGraphResourceRegistry resources = this.m_Resources;
						textureHandle = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd, array, resources.GetTexture(in textureHandle));
						return;
					}
					throw new InvalidOperationException("Setting MRTs without a depth buffer is not supported.");
				}
				else
				{
					textureHandle = pass.depthBuffer;
					if (textureHandle.IsValid())
					{
						if (pass.colorBufferMaxIndex > -1)
						{
							CommandBuffer cmd2 = rgContext.cmd;
							RTHandle texture = this.m_Resources.GetTexture(in pass.colorBuffers[0]);
							RenderGraphResourceRegistry resources2 = this.m_Resources;
							textureHandle = pass.depthBuffer;
							CoreUtils.SetRenderTarget(cmd2, texture, resources2.GetTexture(in textureHandle), 0, CubemapFace.Unknown, -1);
							return;
						}
						CommandBuffer cmd3 = rgContext.cmd;
						RenderGraphResourceRegistry resources3 = this.m_Resources;
						textureHandle = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd3, resources3.GetTexture(in textureHandle), ClearFlag.None, 0, CubemapFace.Unknown, -1);
						return;
					}
					else
					{
						CoreUtils.SetRenderTarget(rgContext.cmd, this.m_Resources.GetTexture(in pass.colorBuffers[0]), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008D6C File Offset: 0x00006F6C
		private void PreRenderPassExecute(in RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			RenderGraphPass pass = passInfo.pass;
			this.m_PreviousCommandBuffer = rgContext.cmd;
			for (int i = 0; i < 2; i++)
			{
				foreach (int num in passInfo.resourceCreateList[i])
				{
					this.m_Resources.CreatePooledResource(rgContext, i, num);
				}
			}
			this.PreRenderPassSetRenderTargets(in passInfo, rgContext);
			if (passInfo.enableAsyncCompute)
			{
				rgContext.renderContext.ExecuteCommandBuffer(rgContext.cmd);
				rgContext.cmd.Clear();
				CommandBuffer commandBuffer = CommandBufferPool.Get(pass.name);
				commandBuffer.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
				rgContext.cmd = commandBuffer;
			}
			if (passInfo.syncToPassIndex != -1)
			{
				rgContext.cmd.WaitOnAsyncGraphicsFence(this.m_CompiledPassInfos[passInfo.syncToPassIndex].fence);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008E5C File Offset: 0x0000705C
		private void PostRenderPassExecute(ref RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			if (passInfo.needGraphicsFence)
			{
				passInfo.fence = rgContext.cmd.CreateAsyncGraphicsFence();
			}
			if (passInfo.enableAsyncCompute)
			{
				rgContext.renderContext.ExecuteCommandBufferAsync(rgContext.cmd, ComputeQueueType.Background);
				CommandBufferPool.Release(rgContext.cmd);
				rgContext.cmd = this.m_PreviousCommandBuffer;
			}
			this.m_RenderGraphPool.ReleaseAllTempAlloc();
			for (int i = 0; i < 2; i++)
			{
				foreach (int num in passInfo.resourceReleaseList[i])
				{
					this.m_Resources.ReleasePooledResource(rgContext, i, num);
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008F1C File Offset: 0x0000711C
		private void ClearRenderPasses()
		{
			foreach (RenderGraphPass renderGraphPass in this.m_RenderPasses)
			{
				renderGraphPass.Release(this.m_RenderGraphPool);
			}
			this.m_RenderPasses.Clear();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008F80 File Offset: 0x00007180
		private void ReleaseImmediateModeResources()
		{
			for (int i = 0; i < 2; i++)
			{
				foreach (int num in this.m_ImmediateModeResourceList[i])
				{
					this.m_Resources.ReleasePooledResource(this.m_RenderGraphContext, i, num);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008FF0 File Offset: 0x000071F0
		private void LogFrameInformation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("==== Staring render graph frame for: " + this.m_CurrentExecutionName + " ====", Array.Empty<object>());
				if (!this.m_DebugParameters.immediateMode)
				{
					this.m_FrameInformationLogger.LogLine("Number of passes declared: {0}\n", new object[] { this.m_RenderPasses.Count });
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009065 File Offset: 0x00007265
		private void LogRendererListsCreation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Number of renderer lists created: {0}\n", new object[] { this.m_RendererLists.Count });
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000090A0 File Offset: 0x000072A0
		private void LogRenderPassBegin(in RenderGraph.CompiledPassInfo passInfo)
		{
			if (this.m_DebugParameters.enableLogging)
			{
				RenderGraphPass pass = passInfo.pass;
				this.m_FrameInformationLogger.LogLine("[{0}][{1}] \"{2}\"", new object[]
				{
					pass.index,
					pass.enableAsyncCompute ? "Compute" : "Graphics",
					pass.name
				});
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					if (passInfo.syncToPassIndex != -1)
					{
						this.m_FrameInformationLogger.LogLine("Synchronize with [{0}]", new object[] { passInfo.syncToPassIndex });
					}
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009164 File Offset: 0x00007364
		private void LogCulledPasses()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Pass Culling Report:", Array.Empty<object>());
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
					{
						if (this.m_CompiledPassInfos[i].culled)
						{
							RenderGraphPass renderGraphPass = this.m_RenderPasses[i];
							this.m_FrameInformationLogger.LogLine("[{0}] {1}", new object[] { renderGraphPass.index, renderGraphPass.name });
						}
					}
					this.m_FrameInformationLogger.LogLine("\n", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000923C File Offset: 0x0000743C
		private ProfilingSampler GetDefaultProfilingSampler(string name)
		{
			int hashCode = name.GetHashCode();
			ProfilingSampler profilingSampler;
			if (!this.m_DefaultProfilingSamplers.TryGetValue(hashCode, out profilingSampler))
			{
				profilingSampler = new ProfilingSampler(name);
				this.m_DefaultProfilingSamplers.Add(hashCode, profilingSampler);
			}
			return profilingSampler;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009278 File Offset: 0x00007478
		private void UpdateImportedResourceLifeTime(ref RenderGraphDebugData.ResourceDebugData data, List<int> passList)
		{
			foreach (int num in passList)
			{
				if (data.creationPassIndex == -1)
				{
					data.creationPassIndex = num;
				}
				else
				{
					data.creationPassIndex = Math.Min(data.creationPassIndex, num);
				}
				if (data.releasePassIndex == -1)
				{
					data.releasePassIndex = num;
				}
				else
				{
					data.releasePassIndex = Math.Max(data.releasePassIndex, num);
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009308 File Offset: 0x00007508
		private void GenerateDebugData()
		{
			if (this.m_ExecutionExceptionWasRaised)
			{
				return;
			}
			if (!RenderGraph.requireDebugData)
			{
				this.CleanupDebugData();
				return;
			}
			RenderGraphDebugData renderGraphDebugData;
			if (!this.m_DebugData.TryGetValue(this.m_CurrentExecutionName, out renderGraphDebugData))
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionRegistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, this.m_CurrentExecutionName);
				}
				renderGraphDebugData = new RenderGraphDebugData();
				this.m_DebugData.Add(this.m_CurrentExecutionName, renderGraphDebugData);
			}
			renderGraphDebugData.Clear();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < this.m_CompiledResourcesInfos[i].size; j++)
				{
					ref RenderGraph.CompiledResourceInfo ptr = ref this.m_CompiledResourcesInfos[i][j];
					RenderGraphDebugData.ResourceDebugData resourceDebugData = new RenderGraphDebugData.ResourceDebugData
					{
						name = this.m_Resources.GetRenderGraphResourceName((RenderGraphResourceType)i, j),
						imported = this.m_Resources.IsRenderGraphResourceImported((RenderGraphResourceType)i, j),
						creationPassIndex = -1,
						releasePassIndex = -1,
						consumerList = new List<int>(ptr.consumers),
						producerList = new List<int>(ptr.producers)
					};
					if (resourceDebugData.imported)
					{
						this.UpdateImportedResourceLifeTime(ref resourceDebugData, resourceDebugData.consumerList);
						this.UpdateImportedResourceLifeTime(ref resourceDebugData, resourceDebugData.producerList);
					}
					renderGraphDebugData.resourceLists[i].Add(resourceDebugData);
				}
			}
			for (int k = 0; k < this.m_CompiledPassInfos.size; k++)
			{
				ref RenderGraph.CompiledPassInfo ptr2 = ref this.m_CompiledPassInfos[k];
				RenderGraphDebugData.PassDebugData passDebugData = default(RenderGraphDebugData.PassDebugData);
				passDebugData.name = ptr2.pass.name;
				passDebugData.culled = ptr2.culled;
				passDebugData.generateDebugData = ptr2.pass.generateDebugData;
				passDebugData.resourceReadLists = new List<int>[2];
				passDebugData.resourceWriteLists = new List<int>[2];
				for (int l = 0; l < 2; l++)
				{
					passDebugData.resourceReadLists[l] = new List<int>();
					passDebugData.resourceWriteLists[l] = new List<int>();
					foreach (ResourceHandle resourceHandle in ptr2.pass.resourceReadLists[l])
					{
						passDebugData.resourceReadLists[l].Add(resourceHandle);
					}
					foreach (ResourceHandle resourceHandle2 in ptr2.pass.resourceWriteLists[l])
					{
						passDebugData.resourceWriteLists[l].Add(resourceHandle2);
					}
					foreach (int num in ptr2.resourceCreateList[l])
					{
						RenderGraphDebugData.ResourceDebugData resourceDebugData2 = renderGraphDebugData.resourceLists[l][num];
						if (!resourceDebugData2.imported)
						{
							resourceDebugData2.creationPassIndex = k;
							renderGraphDebugData.resourceLists[l][num] = resourceDebugData2;
						}
					}
					foreach (int num2 in ptr2.resourceReleaseList[l])
					{
						RenderGraphDebugData.ResourceDebugData resourceDebugData3 = renderGraphDebugData.resourceLists[l][num2];
						if (!resourceDebugData3.imported)
						{
							resourceDebugData3.releasePassIndex = k;
							renderGraphDebugData.resourceLists[l][num2] = resourceDebugData3;
						}
					}
				}
				renderGraphDebugData.passList.Add(passDebugData);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000096C0 File Offset: 0x000078C0
		private void CleanupDebugData()
		{
			foreach (KeyValuePair<string, RenderGraphDebugData> keyValuePair in this.m_DebugData)
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionUnregistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, keyValuePair.Key);
				}
			}
			this.m_DebugData.Clear();
		}

		// Token: 0x040000DB RID: 219
		public static readonly int kMaxMRTCount = 8;

		// Token: 0x040000DC RID: 220
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x040000DD RID: 221
		private RenderGraphObjectPool m_RenderGraphPool = new RenderGraphObjectPool();

		// Token: 0x040000DE RID: 222
		private List<RenderGraphPass> m_RenderPasses = new List<RenderGraphPass>(64);

		// Token: 0x040000DF RID: 223
		private List<RendererListHandle> m_RendererLists = new List<RendererListHandle>(32);

		// Token: 0x040000E0 RID: 224
		private RenderGraphDebugParams m_DebugParameters = new RenderGraphDebugParams();

		// Token: 0x040000E1 RID: 225
		private RenderGraphLogger m_FrameInformationLogger = new RenderGraphLogger();

		// Token: 0x040000E2 RID: 226
		private RenderGraphDefaultResources m_DefaultResources = new RenderGraphDefaultResources();

		// Token: 0x040000E3 RID: 227
		private Dictionary<int, ProfilingSampler> m_DefaultProfilingSamplers = new Dictionary<int, ProfilingSampler>();

		// Token: 0x040000E4 RID: 228
		private bool m_ExecutionExceptionWasRaised;

		// Token: 0x040000E5 RID: 229
		private RenderGraphContext m_RenderGraphContext = new RenderGraphContext();

		// Token: 0x040000E6 RID: 230
		private CommandBuffer m_PreviousCommandBuffer;

		// Token: 0x040000E7 RID: 231
		private int m_CurrentImmediatePassIndex;

		// Token: 0x040000E8 RID: 232
		private List<int>[] m_ImmediateModeResourceList = new List<int>[2];

		// Token: 0x040000E9 RID: 233
		private DynamicArray<RenderGraph.CompiledResourceInfo>[] m_CompiledResourcesInfos = new DynamicArray<RenderGraph.CompiledResourceInfo>[2];

		// Token: 0x040000EA RID: 234
		private DynamicArray<RenderGraph.CompiledPassInfo> m_CompiledPassInfos = new DynamicArray<RenderGraph.CompiledPassInfo>();

		// Token: 0x040000EB RID: 235
		private Stack<int> m_CullingStack = new Stack<int>();

		// Token: 0x040000EC RID: 236
		private int m_ExecutionCount;

		// Token: 0x040000ED RID: 237
		private int m_CurrentFrameIndex;

		// Token: 0x040000EE RID: 238
		private bool m_HasRenderGraphBegun;

		// Token: 0x040000EF RID: 239
		private string m_CurrentExecutionName;

		// Token: 0x040000F0 RID: 240
		private bool m_RendererListCulling;

		// Token: 0x040000F1 RID: 241
		private Dictionary<string, RenderGraphDebugData> m_DebugData = new Dictionary<string, RenderGraphDebugData>();

		// Token: 0x040000F2 RID: 242
		private static List<RenderGraph> s_RegisteredGraphs = new List<RenderGraph>();

		// Token: 0x0200012C RID: 300
		internal struct CompiledResourceInfo
		{
			// Token: 0x06000808 RID: 2056 RVA: 0x00022BC0 File Offset: 0x00020DC0
			public void Reset()
			{
				if (this.producers == null)
				{
					this.producers = new List<int>();
				}
				if (this.consumers == null)
				{
					this.consumers = new List<int>();
				}
				this.producers.Clear();
				this.consumers.Clear();
				this.refCount = 0;
				this.imported = false;
			}

			// Token: 0x040004D5 RID: 1237
			public List<int> producers;

			// Token: 0x040004D6 RID: 1238
			public List<int> consumers;

			// Token: 0x040004D7 RID: 1239
			public int refCount;

			// Token: 0x040004D8 RID: 1240
			public bool imported;
		}

		// Token: 0x0200012D RID: 301
		[DebuggerDisplay("RenderPass: {pass.name} (Index:{pass.index} Async:{enableAsyncCompute})")]
		internal struct CompiledPassInfo
		{
			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000809 RID: 2057 RVA: 0x00022C17 File Offset: 0x00020E17
			public bool allowPassCulling
			{
				get
				{
					return this.pass.allowPassCulling;
				}
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x00022C24 File Offset: 0x00020E24
			public void Reset(RenderGraphPass pass)
			{
				this.pass = pass;
				this.enableAsyncCompute = pass.enableAsyncCompute;
				if (this.resourceCreateList == null)
				{
					this.resourceCreateList = new List<int>[2];
					this.resourceReleaseList = new List<int>[2];
					for (int i = 0; i < 2; i++)
					{
						this.resourceCreateList[i] = new List<int>();
						this.resourceReleaseList[i] = new List<int>();
					}
				}
				for (int j = 0; j < 2; j++)
				{
					this.resourceCreateList[j].Clear();
					this.resourceReleaseList[j].Clear();
				}
				this.refCount = 0;
				this.culled = false;
				this.hasSideEffect = false;
				this.syncToPassIndex = -1;
				this.syncFromPassIndex = -1;
				this.needGraphicsFence = false;
			}

			// Token: 0x040004D9 RID: 1241
			public RenderGraphPass pass;

			// Token: 0x040004DA RID: 1242
			public List<int>[] resourceCreateList;

			// Token: 0x040004DB RID: 1243
			public List<int>[] resourceReleaseList;

			// Token: 0x040004DC RID: 1244
			public int refCount;

			// Token: 0x040004DD RID: 1245
			public bool culled;

			// Token: 0x040004DE RID: 1246
			public bool hasSideEffect;

			// Token: 0x040004DF RID: 1247
			public int syncToPassIndex;

			// Token: 0x040004E0 RID: 1248
			public int syncFromPassIndex;

			// Token: 0x040004E1 RID: 1249
			public bool needGraphicsFence;

			// Token: 0x040004E2 RID: 1250
			public GraphicsFence fence;

			// Token: 0x040004E3 RID: 1251
			public bool enableAsyncCompute;
		}

		// Token: 0x0200012E RID: 302
		private class ProfilingScopePassData
		{
			// Token: 0x040004E4 RID: 1252
			public ProfilingSampler sampler;
		}

		// Token: 0x0200012F RID: 303
		// (Invoke) Token: 0x0600080D RID: 2061
		internal delegate void OnGraphRegisteredDelegate(RenderGraph graph);

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x06000811 RID: 2065
		internal delegate void OnExecutionRegisteredDelegate(RenderGraph graph, string executionName);
	}
}
