using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000041 RID: 65
	public static class CommandBufferPool
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000CEC6 File Offset: 0x0000B0C6
		public static CommandBuffer Get()
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = "";
			return commandBuffer;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CEDD File Offset: 0x0000B0DD
		public static CommandBuffer Get(string name)
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = name;
			return commandBuffer;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public static void Release(CommandBuffer buffer)
		{
			CommandBufferPool.s_BufferPool.Release(buffer);
		}

		// Token: 0x040001A0 RID: 416
		private static ObjectPool<CommandBuffer> s_BufferPool = new ObjectPool<CommandBuffer>(null, delegate(CommandBuffer x)
		{
			x.Clear();
		}, true);
	}
}
