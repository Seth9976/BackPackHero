using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pathfinding.Drawing
{
	// Token: 0x02000004 RID: 4
	public class AlineURPRenderPassFeature : ScriptableRendererFeature
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public override void Create()
		{
			this.m_ScriptablePass = new AlineURPRenderPassFeature.AlineURPRenderPass();
			this.m_ScriptablePass.renderPassEvent = (RenderPassEvent)549;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002075 File Offset: 0x00000275
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			this.AddRenderPasses(renderer);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		public void AddRenderPasses(ScriptableRenderer renderer)
		{
			renderer.EnqueuePass(this.m_ScriptablePass);
		}

		// Token: 0x04000001 RID: 1
		private AlineURPRenderPassFeature.AlineURPRenderPass m_ScriptablePass;

		// Token: 0x02000005 RID: 5
		public class AlineURPRenderPass : ScriptableRenderPass
		{
			// Token: 0x06000007 RID: 7 RVA: 0x00002094 File Offset: 0x00000294
			public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
			{
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002096 File Offset: 0x00000296
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				DrawingManager.instance.ExecuteCustomRenderPass(context, renderingData.cameraData.camera);
			}

			// Token: 0x06000009 RID: 9 RVA: 0x000020AE File Offset: 0x000002AE
			public AlineURPRenderPass()
			{
				base.profilingSampler = new ProfilingSampler("ALINE");
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002094 File Offset: 0x00000294
			public override void FrameCleanup(CommandBuffer cmd)
			{
			}
		}
	}
}
