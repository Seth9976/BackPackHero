using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007E RID: 126
	[Obsolete("ForwardRendererData has been deprecated (UnityUpgradable) -> UniversalRendererData", true)]
	[ReloadGroup]
	[ExcludeFromPreset]
	[Serializable]
	public class ForwardRendererData : ScriptableRendererData
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001BA13 File Offset: 0x00019C13
		protected override ScriptableRenderer Create()
		{
			Debug.LogWarning("Forward Renderer Data has been deprecated, " + base.name + " will be upgraded to a UniversalRendererData.");
			return null;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001BA30 File Offset: 0x00019C30
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001BA3C File Offset: 0x00019C3C
		public LayerMask opaqueLayerMask
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001BA48 File Offset: 0x00019C48
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001BA54 File Offset: 0x00019C54
		public LayerMask transparentLayerMask
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001BA60 File Offset: 0x00019C60
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public StencilStateData defaultStencilState
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001BA78 File Offset: 0x00019C78
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001BA84 File Offset: 0x00019C84
		public bool shadowTransparentReceive
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001BA90 File Offset: 0x00019C90
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public RenderingMode renderingMode
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public bool accurateGbufferNormals
		{
			get
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		// Token: 0x0400034A RID: 842
		private const string k_ErrorMessage = "ForwardRendererData has been deprecated. Use UniversalRendererData instead";

		// Token: 0x0400034B RID: 843
		public ForwardRendererData.ShaderResources shaders;

		// Token: 0x0400034C RID: 844
		public PostProcessData postProcessData;

		// Token: 0x0400034D RID: 845
		[Reload("Runtime/Data/XRSystemData.asset", ReloadAttribute.Package.Root)]
		public XRSystemData xrSystemData;

		// Token: 0x0400034E RID: 846
		[SerializeField]
		private LayerMask m_OpaqueLayerMask;

		// Token: 0x0400034F RID: 847
		[SerializeField]
		private LayerMask m_TransparentLayerMask;

		// Token: 0x04000350 RID: 848
		[SerializeField]
		private StencilStateData m_DefaultStencilState;

		// Token: 0x04000351 RID: 849
		[SerializeField]
		private bool m_ShadowTransparentReceive;

		// Token: 0x04000352 RID: 850
		[SerializeField]
		private RenderingMode m_RenderingMode;

		// Token: 0x04000353 RID: 851
		[SerializeField]
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x04000354 RID: 852
		[SerializeField]
		private bool m_AccurateGbufferNormals;

		// Token: 0x04000355 RID: 853
		[SerializeField]
		private bool m_ClusteredRendering;

		// Token: 0x04000356 RID: 854
		[SerializeField]
		private TileSize m_TileSize;

		// Token: 0x02000171 RID: 369
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x0400096F RID: 2415
			[Reload("Shaders/Utils/Blit.shader", ReloadAttribute.Package.Root)]
			public Shader blitPS;

			// Token: 0x04000970 RID: 2416
			[Reload("Shaders/Utils/CopyDepth.shader", ReloadAttribute.Package.Root)]
			public Shader copyDepthPS;

			// Token: 0x04000971 RID: 2417
			[Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature")]
			public Shader screenSpaceShadowPS;

			// Token: 0x04000972 RID: 2418
			[Reload("Shaders/Utils/Sampling.shader", ReloadAttribute.Package.Root)]
			public Shader samplingPS;

			// Token: 0x04000973 RID: 2419
			[Reload("Shaders/Utils/StencilDeferred.shader", ReloadAttribute.Package.Root)]
			public Shader stencilDeferredPS;

			// Token: 0x04000974 RID: 2420
			[Reload("Shaders/Utils/FallbackError.shader", ReloadAttribute.Package.Root)]
			public Shader fallbackErrorPS;

			// Token: 0x04000975 RID: 2421
			[Reload("Shaders/Utils/MaterialError.shader", ReloadAttribute.Package.Root)]
			public Shader materialErrorPS;

			// Token: 0x04000976 RID: 2422
			[Reload("Shaders/Utils/CoreBlit.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitPS;

			// Token: 0x04000977 RID: 2423
			[Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitColorAndDepthPS;

			// Token: 0x04000978 RID: 2424
			[Reload("Shaders/CameraMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionVector;

			// Token: 0x04000979 RID: 2425
			[Reload("Shaders/ObjectMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader objectMotionVector;
		}
	}
}
