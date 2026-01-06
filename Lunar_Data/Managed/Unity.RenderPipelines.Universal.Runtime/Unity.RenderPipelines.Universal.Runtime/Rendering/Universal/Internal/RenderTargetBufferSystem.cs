using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000117 RID: 279
	internal sealed class RenderTargetBufferSystem
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0003927D File Offset: 0x0003747D
		private RenderTargetBufferSystem.SwapBuffer backBuffer
		{
			get
			{
				if (!RenderTargetBufferSystem.m_AisBackBuffer)
				{
					return this.m_B;
				}
				return this.m_A;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00039293 File Offset: 0x00037493
		private RenderTargetBufferSystem.SwapBuffer frontBuffer
		{
			get
			{
				if (!RenderTargetBufferSystem.m_AisBackBuffer)
				{
					return this.m_A;
				}
				return this.m_B;
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000392AC File Offset: 0x000374AC
		public RenderTargetBufferSystem(string name)
		{
			this.m_A.name = Shader.PropertyToID(name + "A");
			this.m_B.name = Shader.PropertyToID(name + "B");
			this.m_A.rt.Init(name + "A");
			this.m_B.rt.Init(name + "B");
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00039332 File Offset: 0x00037532
		public RenderTargetHandle GetBackBuffer()
		{
			return this.backBuffer.rt;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003933F File Offset: 0x0003753F
		public RenderTargetHandle GetBackBuffer(CommandBuffer cmd)
		{
			if (!this.m_RTisAllocated)
			{
				this.Initialize(cmd);
			}
			return this.backBuffer.rt;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003935C File Offset: 0x0003755C
		public RenderTargetHandle GetFrontBuffer(CommandBuffer cmd)
		{
			if (!this.m_RTisAllocated)
			{
				this.Initialize(cmd);
			}
			int msaaSamples = RenderTargetBufferSystem.m_Desc.msaaSamples;
			int msaa = this.frontBuffer.msaa;
			if (this.m_AllowMSAA && msaa != msaaSamples)
			{
				RenderTextureDescriptor desc = RenderTargetBufferSystem.m_Desc;
				if (RenderTargetBufferSystem.m_AisBackBuffer)
				{
					desc.depthBufferBits = 0;
				}
				cmd.ReleaseTemporaryRT(this.frontBuffer.name);
				cmd.GetTemporaryRT(this.frontBuffer.name, desc, this.m_FilterMode);
				if (RenderTargetBufferSystem.m_AisBackBuffer)
				{
					this.m_B.msaa = desc.msaaSamples;
				}
				else
				{
					this.m_A.msaa = desc.msaaSamples;
				}
			}
			else if (!this.m_AllowMSAA && msaa > 1)
			{
				RenderTextureDescriptor desc2 = RenderTargetBufferSystem.m_Desc;
				desc2.msaaSamples = 1;
				if (RenderTargetBufferSystem.m_AisBackBuffer)
				{
					desc2.depthBufferBits = 0;
				}
				cmd.ReleaseTemporaryRT(this.frontBuffer.name);
				cmd.GetTemporaryRT(this.frontBuffer.name, desc2, this.m_FilterMode);
				if (RenderTargetBufferSystem.m_AisBackBuffer)
				{
					this.m_B.msaa = desc2.msaaSamples;
				}
				else
				{
					this.m_A.msaa = desc2.msaaSamples;
				}
			}
			return this.frontBuffer.rt;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00039495 File Offset: 0x00037695
		public void Swap()
		{
			RenderTargetBufferSystem.m_AisBackBuffer = !RenderTargetBufferSystem.m_AisBackBuffer;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000394A4 File Offset: 0x000376A4
		private void Initialize(CommandBuffer cmd)
		{
			this.m_A.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
			this.m_B.msaa = RenderTargetBufferSystem.m_Desc.msaaSamples;
			cmd.GetTemporaryRT(this.m_A.name, RenderTargetBufferSystem.m_Desc, this.m_FilterMode);
			RenderTextureDescriptor desc = RenderTargetBufferSystem.m_Desc;
			cmd.GetTemporaryRT(this.m_B.name, desc, this.m_FilterMode);
			this.m_RTisAllocated = true;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0003951C File Offset: 0x0003771C
		public void Clear(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(this.m_A.name);
			cmd.ReleaseTemporaryRT(this.m_B.name);
			RenderTargetBufferSystem.m_AisBackBuffer = true;
			this.m_AllowMSAA = true;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0003954D File Offset: 0x0003774D
		public void SetCameraSettings(CommandBuffer cmd, RenderTextureDescriptor desc, FilterMode filterMode)
		{
			this.Clear(cmd);
			RenderTargetBufferSystem.m_Desc = desc;
			this.m_FilterMode = filterMode;
			this.Initialize(cmd);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003956A File Offset: 0x0003776A
		public void SetCameraSettings(RenderTextureDescriptor desc, FilterMode filterMode)
		{
			RenderTargetBufferSystem.m_Desc = desc;
			this.m_FilterMode = filterMode;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00039579 File Offset: 0x00037779
		public RenderTargetHandle GetBufferA()
		{
			return this.m_A.rt;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00039586 File Offset: 0x00037786
		public void EnableMSAA(bool enable)
		{
			this.m_AllowMSAA = enable;
		}

		// Token: 0x04000809 RID: 2057
		private RenderTargetBufferSystem.SwapBuffer m_A;

		// Token: 0x0400080A RID: 2058
		private RenderTargetBufferSystem.SwapBuffer m_B;

		// Token: 0x0400080B RID: 2059
		private static bool m_AisBackBuffer = true;

		// Token: 0x0400080C RID: 2060
		private static RenderTextureDescriptor m_Desc;

		// Token: 0x0400080D RID: 2061
		private FilterMode m_FilterMode;

		// Token: 0x0400080E RID: 2062
		private bool m_AllowMSAA = true;

		// Token: 0x0400080F RID: 2063
		private bool m_RTisAllocated;

		// Token: 0x020001AF RID: 431
		private struct SwapBuffer
		{
			// Token: 0x04000B2D RID: 2861
			public RenderTargetHandle rt;

			// Token: 0x04000B2E RID: 2862
			public int name;

			// Token: 0x04000B2F RID: 2863
			public int msaa;
		}
	}
}
