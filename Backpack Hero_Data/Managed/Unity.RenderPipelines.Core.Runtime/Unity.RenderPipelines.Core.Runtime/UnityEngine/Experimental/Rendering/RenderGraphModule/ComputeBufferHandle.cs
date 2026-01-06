using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002C RID: 44
	[DebuggerDisplay("ComputeBuffer ({handle.index})")]
	public struct ComputeBufferHandle
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A55E File Offset: 0x0000875E
		public static ComputeBufferHandle nullHandle
		{
			get
			{
				return ComputeBufferHandle.s_NullHandle;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A565 File Offset: 0x00008765
		internal ComputeBufferHandle(int handle, bool shared = false)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.ComputeBuffer, shared);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A575 File Offset: 0x00008775
		public static implicit operator ComputeBuffer(ComputeBufferHandle buffer)
		{
			if (!buffer.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetComputeBuffer(in buffer);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A58E File Offset: 0x0000878E
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x0400012E RID: 302
		private static ComputeBufferHandle s_NullHandle;

		// Token: 0x0400012F RID: 303
		internal ResourceHandle handle;
	}
}
