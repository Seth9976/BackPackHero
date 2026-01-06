using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002E RID: 46
	[DebuggerDisplay("ComputeBufferResource ({desc.name})")]
	internal class ComputeBufferResource : RenderGraphResource<ComputeBufferDesc, ComputeBuffer>
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000A5FB File Offset: 0x000087FB
		public override string GetName()
		{
			if (this.imported)
			{
				return "ImportedComputeBuffer";
			}
			return this.desc.name;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A618 File Offset: 0x00008818
		public override void CreatePooledGraphicsResource()
		{
			int hashCode = this.desc.GetHashCode();
			if (this.graphicsResource != null)
			{
				throw new InvalidOperationException(string.Format("Trying to create an already created resource ({0}). Resource was probably declared for writing more than once in the same pass.", this.GetName()));
			}
			ComputeBufferPool computeBufferPool = this.m_Pool as ComputeBufferPool;
			if (!computeBufferPool.TryGetResource(hashCode, out this.graphicsResource))
			{
				this.CreateGraphicsResource("");
			}
			this.cachedHash = hashCode;
			computeBufferPool.RegisterFrameAllocation(this.cachedHash, this.graphicsResource);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A694 File Offset: 0x00008894
		public override void ReleasePooledGraphicsResource(int frameIndex)
		{
			if (this.graphicsResource == null)
			{
				throw new InvalidOperationException("Tried to release a resource (" + this.GetName() + ") that was never created. Check that there is at least one pass writing to it first.");
			}
			ComputeBufferPool computeBufferPool = this.m_Pool as ComputeBufferPool;
			if (computeBufferPool != null)
			{
				computeBufferPool.ReleaseResource(this.cachedHash, this.graphicsResource, frameIndex);
				computeBufferPool.UnregisterFrameAllocation(this.cachedHash, this.graphicsResource);
			}
			this.Reset(null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A700 File Offset: 0x00008900
		public override void CreateGraphicsResource(string name = "")
		{
			this.graphicsResource = new ComputeBuffer(this.desc.count, this.desc.stride, this.desc.type);
			this.graphicsResource.name = ((name == "") ? string.Format("RenderGraphComputeBuffer_{0}_{1}_{2}", this.desc.count, this.desc.stride, this.desc.type) : name);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A78E File Offset: 0x0000898E
		public override void ReleaseGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A7A9 File Offset: 0x000089A9
		public override void LogCreation(RenderGraphLogger logger)
		{
			logger.LogLine("Created ComputeBuffer: " + this.desc.name, Array.Empty<object>());
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A7CB File Offset: 0x000089CB
		public override void LogRelease(RenderGraphLogger logger)
		{
			logger.LogLine("Released ComputeBuffer: " + this.desc.name, Array.Empty<object>());
		}
	}
}
