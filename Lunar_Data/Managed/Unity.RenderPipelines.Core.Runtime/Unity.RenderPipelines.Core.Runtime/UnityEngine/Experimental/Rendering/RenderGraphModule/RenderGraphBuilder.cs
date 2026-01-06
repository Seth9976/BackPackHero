using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000024 RID: 36
	public struct RenderGraphBuilder : IDisposable
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00009795 File Offset: 0x00007995
		public TextureHandle UseColorBuffer(in TextureHandle input, int index)
		{
			this.CheckResource(in input.handle, true);
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.SetColorBuffer(input, index);
			return input;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000097D0 File Offset: 0x000079D0
		public TextureHandle UseDepthBuffer(in TextureHandle input, DepthAccess flags)
		{
			this.CheckResource(in input.handle, true);
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.m_Resources.IncrementWriteCount(in input.handle);
			}
			if ((flags & DepthAccess.Read) != (DepthAccess)0 && !this.m_Resources.IsRenderGraphResourceImported(in input.handle) && this.m_Resources.TextureNeedsFallback(in input))
			{
				this.WriteTexture(in input);
			}
			this.m_RenderPass.SetDepthBuffer(input, flags);
			return input;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009848 File Offset: 0x00007A48
		public TextureHandle ReadTexture(in TextureHandle input)
		{
			this.CheckResource(in input.handle, false);
			if (!this.m_Resources.IsRenderGraphResourceImported(in input.handle) && this.m_Resources.TextureNeedsFallback(in input))
			{
				TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(in input.handle);
				if (!textureResourceDesc.bindTextureMS)
				{
					if (textureResourceDesc.dimension == TextureXR.dimension)
					{
						return this.m_RenderGraph.defaultResources.blackTextureXR;
					}
					if (textureResourceDesc.dimension == TextureDimension.Tex3D)
					{
						return this.m_RenderGraph.defaultResources.blackTexture3DXR;
					}
					return this.m_RenderGraph.defaultResources.blackTexture;
				}
				else
				{
					if (!textureResourceDesc.clearBuffer)
					{
						this.m_Resources.ForceTextureClear(in input.handle, Color.black);
					}
					this.WriteTexture(in input);
				}
			}
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009929 File Offset: 0x00007B29
		public TextureHandle WriteTexture(in TextureHandle input)
		{
			this.CheckResource(in input.handle, false);
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			return input;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009960 File Offset: 0x00007B60
		public TextureHandle ReadWriteTexture(in TextureHandle input)
		{
			this.CheckResource(in input.handle, false);
			this.m_Resources.IncrementWriteCount(in input.handle);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000099B4 File Offset: 0x00007BB4
		public TextureHandle CreateTransientTexture(in TextureDesc desc)
		{
			TextureHandle textureHandle = this.m_Resources.CreateTexture(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in textureHandle.handle);
			return textureHandle;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000099EC File Offset: 0x00007BEC
		public TextureHandle CreateTransientTexture(in TextureHandle texture)
		{
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(in texture.handle);
			TextureHandle textureHandle = this.m_Resources.CreateTexture(in textureResourceDesc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in textureHandle.handle);
			return textureHandle;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009A37 File Offset: 0x00007C37
		public RendererListHandle UseRendererList(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(input);
			return input;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009A50 File Offset: 0x00007C50
		public ComputeBufferHandle ReadComputeBuffer(in ComputeBufferHandle input)
		{
			this.CheckResource(in input.handle, false);
			this.m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009A76 File Offset: 0x00007C76
		public ComputeBufferHandle WriteComputeBuffer(in ComputeBufferHandle input)
		{
			this.CheckResource(in input.handle, false);
			this.m_RenderPass.AddResourceWrite(in input.handle);
			this.m_Resources.IncrementWriteCount(in input.handle);
			return input;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public ComputeBufferHandle CreateTransientComputeBuffer(in ComputeBufferDesc desc)
		{
			ComputeBufferHandle computeBufferHandle = this.m_Resources.CreateComputeBuffer(in desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in computeBufferHandle.handle);
			return computeBufferHandle;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public ComputeBufferHandle CreateTransientComputeBuffer(in ComputeBufferHandle computebuffer)
		{
			ComputeBufferDesc computeBufferResourceDesc = this.m_Resources.GetComputeBufferResourceDesc(in computebuffer.handle);
			ComputeBufferHandle computeBufferHandle = this.m_Resources.CreateComputeBuffer(in computeBufferResourceDesc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(in computeBufferHandle.handle);
			return computeBufferHandle;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009B33 File Offset: 0x00007D33
		public void SetRenderFunc<PassData>(RenderFunc<PassData> renderFunc) where PassData : class, new()
		{
			((RenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009B46 File Offset: 0x00007D46
		public void EnableAsyncCompute(bool value)
		{
			this.m_RenderPass.EnableAsyncCompute(value);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009B54 File Offset: 0x00007D54
		public void AllowPassCulling(bool value)
		{
			this.m_RenderPass.AllowPassCulling(value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009B62 File Offset: 0x00007D62
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009B6B File Offset: 0x00007D6B
		public void AllowRendererListCulling(bool value)
		{
			this.m_RenderPass.AllowRendererListCulling(value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009B79 File Offset: 0x00007D79
		public RendererListHandle DependsOn(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(input);
			return input;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009B92 File Offset: 0x00007D92
		internal RenderGraphBuilder(RenderGraphPass renderPass, RenderGraphResourceRegistry resources, RenderGraph renderGraph)
		{
			this.m_RenderPass = renderPass;
			this.m_Resources = resources;
			this.m_RenderGraph = renderGraph;
			this.m_Disposed = false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_RenderGraph.OnPassAdded(this.m_RenderPass);
			this.m_Disposed = true;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009BD3 File Offset: 0x00007DD3
		private void CheckResource(in ResourceHandle res, bool dontCheckTransientReadWrite = false)
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009BD5 File Offset: 0x00007DD5
		internal void GenerateDebugData(bool value)
		{
			this.m_RenderPass.GenerateDebugData(value);
		}

		// Token: 0x040000FC RID: 252
		private RenderGraphPass m_RenderPass;

		// Token: 0x040000FD RID: 253
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x040000FE RID: 254
		private RenderGraph m_RenderGraph;

		// Token: 0x040000FF RID: 255
		private bool m_Disposed;
	}
}
