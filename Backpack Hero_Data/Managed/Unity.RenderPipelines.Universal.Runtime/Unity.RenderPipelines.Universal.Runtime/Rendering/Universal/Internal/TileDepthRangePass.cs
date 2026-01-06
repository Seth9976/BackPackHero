using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000115 RID: 277
	internal class TileDepthRangePass : ScriptableRenderPass
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x00039069 File Offset: 0x00037269
		public TileDepthRangePass(RenderPassEvent evt, DeferredLights deferredLights, int passIndex)
		{
			base.profilingSampler = new ProfilingSampler("TileDepthRangePass");
			base.renderPassEvent = evt;
			this.m_DeferredLights = deferredLights;
			this.m_PassIndex = passIndex;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00039098 File Offset: 0x00037298
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			RenderTargetHandle renderTargetHandle;
			RenderTextureDescriptor renderTextureDescriptor;
			if (this.m_PassIndex == 0 && this.m_DeferredLights.HasTileDepthRangeExtraPass())
			{
				int minValue = int.MinValue;
				int num = this.m_DeferredLights.RenderWidth + minValue - 1 >> 31;
				int num2 = this.m_DeferredLights.RenderHeight + minValue - 1 >> 31;
				renderTargetHandle = this.m_DeferredLights.DepthInfoTexture;
				renderTextureDescriptor = new RenderTextureDescriptor(num, num2, GraphicsFormat.R32_UInt, 0);
			}
			else
			{
				int tileXCount = this.m_DeferredLights.GetTiler(0).TileXCount;
				int tileYCount = this.m_DeferredLights.GetTiler(0).TileYCount;
				renderTargetHandle = this.m_DeferredLights.TileDepthInfoTexture;
				renderTextureDescriptor = new RenderTextureDescriptor(tileXCount, tileYCount, GraphicsFormat.R32_UInt, 0);
			}
			cmd.GetTemporaryRT(renderTargetHandle.id, renderTextureDescriptor, FilterMode.Point);
			base.ConfigureTarget(renderTargetHandle.Identifier());
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0003915E File Offset: 0x0003735E
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_PassIndex == 0)
			{
				this.m_DeferredLights.ExecuteTileDepthInfoPass(context, ref renderingData);
				return;
			}
			this.m_DeferredLights.ExecuteDownsampleBitmaskPass(context, ref renderingData);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00039184 File Offset: 0x00037384
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ReleaseTemporaryRT(this.m_DeferredLights.TileDepthInfoTexture.id);
			this.m_DeferredLights.TileDepthInfoTexture = RenderTargetHandle.CameraTarget;
		}

		// Token: 0x04000801 RID: 2049
		private DeferredLights m_DeferredLights;

		// Token: 0x04000802 RID: 2050
		private int m_PassIndex;
	}
}
