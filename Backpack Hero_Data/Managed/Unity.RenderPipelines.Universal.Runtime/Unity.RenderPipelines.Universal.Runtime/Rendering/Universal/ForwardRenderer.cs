using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000079 RID: 121
	[Obsolete("ForwardRenderer has been deprecated (UnityUpgradable) -> UniversalRenderer", true)]
	public sealed class ForwardRenderer : ScriptableRenderer
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x000186A6 File Offset: 0x000168A6
		public ForwardRenderer(ForwardRendererData data)
			: base(data)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000186B9 File Offset: 0x000168B9
		public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000186C5 File Offset: 0x000168C5
		public override void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000186D1 File Offset: 0x000168D1
		public override void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000186DD File Offset: 0x000168DD
		public override void FinishRendering(CommandBuffer cmd)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000186E9 File Offset: 0x000168E9
		internal override void SwapColorBuffer(CommandBuffer cmd)
		{
			throw new NotSupportedException(ForwardRenderer.k_ErrorMessage);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000186F5 File Offset: 0x000168F5
		internal override RenderTargetIdentifier GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400031B RID: 795
		private static readonly string k_ErrorMessage = "ForwardRenderer has been deprecated. Use UniversalRenderer instead";
	}
}
