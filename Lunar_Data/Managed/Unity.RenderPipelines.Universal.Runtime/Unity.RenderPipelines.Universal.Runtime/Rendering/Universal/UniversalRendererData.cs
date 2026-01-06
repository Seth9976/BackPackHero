using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D9 RID: 217
	[ReloadGroup]
	[ExcludeFromPreset]
	[Serializable]
	public class UniversalRendererData : ScriptableRendererData, ISerializationCallbackReceiver
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00022D20 File Offset: 0x00020F20
		protected override ScriptableRenderer Create()
		{
			if (!Application.isPlaying)
			{
				this.ReloadAllNullProperties();
			}
			return new UniversalRenderer(this);
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00022D35 File Offset: 0x00020F35
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00022D3D File Offset: 0x00020F3D
		public LayerMask opaqueLayerMask
		{
			get
			{
				return this.m_OpaqueLayerMask;
			}
			set
			{
				base.SetDirty();
				this.m_OpaqueLayerMask = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00022D4C File Offset: 0x00020F4C
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00022D54 File Offset: 0x00020F54
		public LayerMask transparentLayerMask
		{
			get
			{
				return this.m_TransparentLayerMask;
			}
			set
			{
				base.SetDirty();
				this.m_TransparentLayerMask = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00022D63 File Offset: 0x00020F63
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00022D6B File Offset: 0x00020F6B
		public StencilStateData defaultStencilState
		{
			get
			{
				return this.m_DefaultStencilState;
			}
			set
			{
				base.SetDirty();
				this.m_DefaultStencilState = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00022D7A File Offset: 0x00020F7A
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00022D82 File Offset: 0x00020F82
		public bool shadowTransparentReceive
		{
			get
			{
				return this.m_ShadowTransparentReceive;
			}
			set
			{
				base.SetDirty();
				this.m_ShadowTransparentReceive = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00022D91 File Offset: 0x00020F91
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00022D99 File Offset: 0x00020F99
		public RenderingMode renderingMode
		{
			get
			{
				return this.m_RenderingMode;
			}
			set
			{
				base.SetDirty();
				this.m_RenderingMode = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00022DA8 File Offset: 0x00020FA8
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00022DB0 File Offset: 0x00020FB0
		public DepthPrimingMode depthPrimingMode
		{
			get
			{
				return this.m_DepthPrimingMode;
			}
			set
			{
				base.SetDirty();
				this.m_DepthPrimingMode = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00022DBF File Offset: 0x00020FBF
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00022DC7 File Offset: 0x00020FC7
		public CopyDepthMode copyDepthMode
		{
			get
			{
				return this.m_CopyDepthMode;
			}
			set
			{
				base.SetDirty();
				this.m_CopyDepthMode = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00022DD6 File Offset: 0x00020FD6
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00022DDE File Offset: 0x00020FDE
		public bool accurateGbufferNormals
		{
			get
			{
				return this.m_AccurateGbufferNormals;
			}
			set
			{
				base.SetDirty();
				this.m_AccurateGbufferNormals = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00022DED File Offset: 0x00020FED
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00022DF5 File Offset: 0x00020FF5
		internal bool clusteredRendering
		{
			get
			{
				return this.m_ClusteredRendering;
			}
			set
			{
				base.SetDirty();
				this.m_ClusteredRendering = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00022E04 File Offset: 0x00021004
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00022E0C File Offset: 0x0002100C
		internal TileSize tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				base.SetDirty();
				this.m_TileSize = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00022E1B File Offset: 0x0002101B
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00022E23 File Offset: 0x00021023
		public IntermediateTextureMode intermediateTextureMode
		{
			get
			{
				return this.m_IntermediateTextureMode;
			}
			set
			{
				base.SetDirty();
				this.m_IntermediateTextureMode = value;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00022E32 File Offset: 0x00021032
		protected override void OnValidate()
		{
			base.OnValidate();
			if (!this.m_TileSize.IsValid())
			{
				this.m_TileSize = TileSize._32;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00022E4F File Offset: 0x0002104F
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.shaders == null)
			{
				return;
			}
			this.ReloadAllNullProperties();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00022E66 File Offset: 0x00021066
		private void ReloadAllNullProperties()
		{
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00022E68 File Offset: 0x00021068
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_AssetVersion = 2;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00022E71 File Offset: 0x00021071
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_AssetVersion <= 0)
			{
				this.m_IntermediateTextureMode = IntermediateTextureMode.Always;
			}
			if (this.m_AssetVersion <= 1)
			{
				this.m_CopyDepthMode = CopyDepthMode.AfterOpaques;
			}
			this.m_AssetVersion = 2;
		}

		// Token: 0x04000538 RID: 1336
		public PostProcessData postProcessData;

		// Token: 0x04000539 RID: 1337
		[Reload("Runtime/Data/XRSystemData.asset", ReloadAttribute.Package.Root)]
		public XRSystemData xrSystemData;

		// Token: 0x0400053A RID: 1338
		public UniversalRendererData.ShaderResources shaders;

		// Token: 0x0400053B RID: 1339
		private const int k_LatestAssetVersion = 2;

		// Token: 0x0400053C RID: 1340
		[SerializeField]
		private int m_AssetVersion;

		// Token: 0x0400053D RID: 1341
		[SerializeField]
		private LayerMask m_OpaqueLayerMask = -1;

		// Token: 0x0400053E RID: 1342
		[SerializeField]
		private LayerMask m_TransparentLayerMask = -1;

		// Token: 0x0400053F RID: 1343
		[SerializeField]
		private StencilStateData m_DefaultStencilState = new StencilStateData
		{
			passOperation = StencilOp.Replace
		};

		// Token: 0x04000540 RID: 1344
		[SerializeField]
		private bool m_ShadowTransparentReceive = true;

		// Token: 0x04000541 RID: 1345
		[SerializeField]
		private RenderingMode m_RenderingMode;

		// Token: 0x04000542 RID: 1346
		[SerializeField]
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x04000543 RID: 1347
		[SerializeField]
		private CopyDepthMode m_CopyDepthMode;

		// Token: 0x04000544 RID: 1348
		[SerializeField]
		private bool m_AccurateGbufferNormals;

		// Token: 0x04000545 RID: 1349
		[SerializeField]
		private bool m_ClusteredRendering;

		// Token: 0x04000546 RID: 1350
		private const TileSize k_DefaultTileSize = TileSize._32;

		// Token: 0x04000547 RID: 1351
		[SerializeField]
		private TileSize m_TileSize = TileSize._32;

		// Token: 0x04000548 RID: 1352
		[SerializeField]
		private IntermediateTextureMode m_IntermediateTextureMode = IntermediateTextureMode.Always;

		// Token: 0x0200018B RID: 395
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040009FA RID: 2554
			[Reload("Shaders/Utils/Blit.shader", ReloadAttribute.Package.Root)]
			public Shader blitPS;

			// Token: 0x040009FB RID: 2555
			[Reload("Shaders/Utils/CopyDepth.shader", ReloadAttribute.Package.Root)]
			public Shader copyDepthPS;

			// Token: 0x040009FC RID: 2556
			[Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature")]
			public Shader screenSpaceShadowPS;

			// Token: 0x040009FD RID: 2557
			[Reload("Shaders/Utils/Sampling.shader", ReloadAttribute.Package.Root)]
			public Shader samplingPS;

			// Token: 0x040009FE RID: 2558
			[Reload("Shaders/Utils/StencilDeferred.shader", ReloadAttribute.Package.Root)]
			public Shader stencilDeferredPS;

			// Token: 0x040009FF RID: 2559
			[Reload("Shaders/Utils/FallbackError.shader", ReloadAttribute.Package.Root)]
			public Shader fallbackErrorPS;

			// Token: 0x04000A00 RID: 2560
			[Reload("Shaders/Utils/MaterialError.shader", ReloadAttribute.Package.Root)]
			public Shader materialErrorPS;

			// Token: 0x04000A01 RID: 2561
			[Reload("Shaders/Utils/CoreBlit.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitPS;

			// Token: 0x04000A02 RID: 2562
			[Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", ReloadAttribute.Package.Root)]
			[SerializeField]
			internal Shader coreBlitColorAndDepthPS;

			// Token: 0x04000A03 RID: 2563
			[Reload("Shaders/CameraMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionVector;

			// Token: 0x04000A04 RID: 2564
			[Reload("Shaders/ObjectMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader objectMotionVector;
		}
	}
}
