using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000029 RID: 41
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class RenderGraphPass
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000A191 File Offset: 0x00008391
		public RenderFunc<PassData> GetExecuteDelegate<PassData>() where PassData : class, new()
		{
			return ((RenderGraphPass<PassData>)this).renderFunc;
		}

		// Token: 0x0600017D RID: 381
		public abstract void Execute(RenderGraphContext renderGraphContext);

		// Token: 0x0600017E RID: 382
		public abstract void Release(RenderGraphObjectPool pool);

		// Token: 0x0600017F RID: 383
		public abstract bool HasRenderFunc();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000A19E File Offset: 0x0000839E
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000A1A6 File Offset: 0x000083A6
		public string name { get; protected set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A1AF File Offset: 0x000083AF
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000A1B7 File Offset: 0x000083B7
		public int index { get; protected set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000A1C0 File Offset: 0x000083C0
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public ProfilingSampler customSampler { get; protected set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000A1D1 File Offset: 0x000083D1
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000A1D9 File Offset: 0x000083D9
		public bool enableAsyncCompute { get; protected set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000A1E2 File Offset: 0x000083E2
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000A1EA File Offset: 0x000083EA
		public bool allowPassCulling { get; protected set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000A1F3 File Offset: 0x000083F3
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000A1FB File Offset: 0x000083FB
		public TextureHandle depthBuffer { get; protected set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000A204 File Offset: 0x00008404
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000A20C File Offset: 0x0000840C
		public TextureHandle[] colorBuffers { get; protected set; } = new TextureHandle[RenderGraph.kMaxMRTCount];

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000A215 File Offset: 0x00008415
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000A21D File Offset: 0x0000841D
		public int colorBufferMaxIndex { get; protected set; } = -1;

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000A226 File Offset: 0x00008426
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000A22E File Offset: 0x0000842E
		public int refCount { get; protected set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000A237 File Offset: 0x00008437
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000A23F File Offset: 0x0000843F
		public bool generateDebugData { get; protected set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000A248 File Offset: 0x00008448
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000A250 File Offset: 0x00008450
		public bool allowRendererListCulling { get; protected set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000A25C File Offset: 0x0000845C
		public RenderGraphPass()
		{
			for (int i = 0; i < 2; i++)
			{
				this.resourceReadLists[i] = new List<ResourceHandle>();
				this.resourceWriteLists[i] = new List<ResourceHandle>();
				this.transientResourceList[i] = new List<ResourceHandle>();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public void Clear()
		{
			this.name = "";
			this.index = -1;
			this.customSampler = null;
			for (int i = 0; i < 2; i++)
			{
				this.resourceReadLists[i].Clear();
				this.resourceWriteLists[i].Clear();
				this.transientResourceList[i].Clear();
			}
			this.usedRendererListList.Clear();
			this.dependsOnRendererListList.Clear();
			this.enableAsyncCompute = false;
			this.allowPassCulling = true;
			this.allowRendererListCulling = true;
			this.generateDebugData = true;
			this.refCount = 0;
			this.colorBufferMaxIndex = -1;
			this.depthBuffer = TextureHandle.nullHandle;
			for (int j = 0; j < RenderGraph.kMaxMRTCount; j++)
			{
				this.colorBuffers[j] = TextureHandle.nullHandle;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000A3BC File Offset: 0x000085BC
		public void AddResourceWrite(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceWriteLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000A3EC File Offset: 0x000085EC
		public void AddResourceRead(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceReadLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A41C File Offset: 0x0000861C
		public void AddTransientResource(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.transientResourceList;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000A449 File Offset: 0x00008649
		public void UseRendererList(RendererListHandle rendererList)
		{
			this.usedRendererListList.Add(rendererList);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A457 File Offset: 0x00008657
		public void DependsOnRendererList(RendererListHandle rendererList)
		{
			this.dependsOnRendererListList.Add(rendererList);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A465 File Offset: 0x00008665
		public void EnableAsyncCompute(bool value)
		{
			this.enableAsyncCompute = value;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A46E File Offset: 0x0000866E
		public void AllowPassCulling(bool value)
		{
			this.allowPassCulling = value;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A477 File Offset: 0x00008677
		public void AllowRendererListCulling(bool value)
		{
			this.allowRendererListCulling = value;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A480 File Offset: 0x00008680
		public void GenerateDebugData(bool value)
		{
			this.generateDebugData = value;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A489 File Offset: 0x00008689
		public void SetColorBuffer(TextureHandle resource, int index)
		{
			this.colorBufferMaxIndex = Math.Max(this.colorBufferMaxIndex, index);
			this.colorBuffers[index] = resource;
			this.AddResourceWrite(in resource.handle);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public void SetDepthBuffer(TextureHandle resource, DepthAccess flags)
		{
			this.depthBuffer = resource;
			if ((flags & DepthAccess.Read) != (DepthAccess)0)
			{
				this.AddResourceRead(in resource.handle);
			}
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.AddResourceWrite(in resource.handle);
			}
		}

		// Token: 0x04000122 RID: 290
		public List<ResourceHandle>[] resourceReadLists = new List<ResourceHandle>[2];

		// Token: 0x04000123 RID: 291
		public List<ResourceHandle>[] resourceWriteLists = new List<ResourceHandle>[2];

		// Token: 0x04000124 RID: 292
		public List<ResourceHandle>[] transientResourceList = new List<ResourceHandle>[2];

		// Token: 0x04000125 RID: 293
		public List<RendererListHandle> usedRendererListList = new List<RendererListHandle>();

		// Token: 0x04000126 RID: 294
		public List<RendererListHandle> dependsOnRendererListList = new List<RendererListHandle>();
	}
}
