using System;
using System.ComponentModel;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004B RID: 75
	[ExcludeFromPreset]
	public class UniversalRenderPipelineAsset : RenderPipelineAsset, ISerializationCallbackReceiver
	{
		// Token: 0x0600024E RID: 590 RVA: 0x000122D6 File Offset: 0x000104D6
		public ScriptableRendererData LoadBuiltinRendererData(RendererType type = RendererType.UniversalRenderer)
		{
			this.m_RendererDataList[0] = null;
			return this.m_RendererDataList[0];
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000122EC File Offset: 0x000104EC
		protected override RenderPipeline CreatePipeline()
		{
			if (this.m_RendererDataList == null)
			{
				this.m_RendererDataList = new ScriptableRendererData[1];
			}
			if (!(this.m_RendererDataList[this.m_DefaultRendererIndex] == null))
			{
				this.CreateRenderers();
				return new UniversalRenderPipeline(this);
			}
			if (this.k_AssetPreviousVersion != this.k_AssetVersion)
			{
				return null;
			}
			if (this.m_RendererDataList[this.m_DefaultRendererIndex].GetType().ToString().Contains("Universal.ForwardRendererData"))
			{
				return null;
			}
			Debug.LogError("Default Renderer is missing, make sure there is a Renderer assigned as the default on the current Universal RP asset:" + UniversalRenderPipeline.asset.name, this);
			return null;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00012380 File Offset: 0x00010580
		internal void DestroyRenderers()
		{
			if (this.m_Renderers == null)
			{
				return;
			}
			for (int i = 0; i < this.m_Renderers.Length; i++)
			{
				this.DestroyRenderer(ref this.m_Renderers[i]);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000123BB File Offset: 0x000105BB
		private void DestroyRenderer(ref ScriptableRenderer renderer)
		{
			if (renderer != null)
			{
				renderer.Dispose();
				renderer = null;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000123CB File Offset: 0x000105CB
		protected override void OnValidate()
		{
			this.DestroyRenderers();
			base.OnValidate();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000123D9 File Offset: 0x000105D9
		protected override void OnDisable()
		{
			this.DestroyRenderers();
			base.OnDisable();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000123E8 File Offset: 0x000105E8
		private void CreateRenderers()
		{
			this.DestroyRenderers();
			if (this.m_Renderers == null || this.m_Renderers.Length != this.m_RendererDataList.Length)
			{
				this.m_Renderers = new ScriptableRenderer[this.m_RendererDataList.Length];
			}
			for (int i = 0; i < this.m_RendererDataList.Length; i++)
			{
				if (this.m_RendererDataList[i] != null)
				{
					this.m_Renderers[i] = this.m_RendererDataList[i].InternalCreateRenderer();
				}
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00012460 File Offset: 0x00010660
		private Material GetMaterial(DefaultMaterialType materialType)
		{
			return null;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00012464 File Offset: 0x00010664
		public ScriptableRenderer scriptableRenderer
		{
			get
			{
				ScriptableRendererData[] rendererDataList = this.m_RendererDataList;
				int? num = ((rendererDataList != null) ? new int?(rendererDataList.Length) : null);
				int defaultRendererIndex = this.m_DefaultRendererIndex;
				if (((num.GetValueOrDefault() > defaultRendererIndex) & (num != null)) && this.m_RendererDataList[this.m_DefaultRendererIndex] == null)
				{
					Debug.LogError("Default renderer is missing from the current Pipeline Asset.", this);
					return null;
				}
				if (this.scriptableRendererData.isInvalidated || this.m_Renderers[this.m_DefaultRendererIndex] == null)
				{
					this.DestroyRenderer(ref this.m_Renderers[this.m_DefaultRendererIndex]);
					this.m_Renderers[this.m_DefaultRendererIndex] = this.scriptableRendererData.InternalCreateRenderer();
				}
				return this.m_Renderers[this.m_DefaultRendererIndex];
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00012524 File Offset: 0x00010724
		public ScriptableRenderer GetRenderer(int index)
		{
			if (index == -1)
			{
				index = this.m_DefaultRendererIndex;
			}
			if (index >= this.m_RendererDataList.Length || index < 0 || this.m_RendererDataList[index] == null)
			{
				Debug.LogWarning("Renderer at index " + index.ToString() + " is missing, falling back to Default Renderer " + this.m_RendererDataList[this.m_DefaultRendererIndex].name, this);
				index = this.m_DefaultRendererIndex;
			}
			if (this.m_Renderers == null || this.m_Renderers.Length < this.m_RendererDataList.Length)
			{
				this.CreateRenderers();
			}
			if (this.m_RendererDataList[index].isInvalidated || this.m_Renderers[index] == null)
			{
				this.DestroyRenderer(ref this.m_Renderers[index]);
				this.m_Renderers[index] = this.m_RendererDataList[index].InternalCreateRenderer();
			}
			return this.m_Renderers[index];
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000125FA File Offset: 0x000107FA
		internal ScriptableRendererData scriptableRendererData
		{
			get
			{
				if (this.m_RendererDataList[this.m_DefaultRendererIndex] == null)
				{
					this.CreatePipeline();
				}
				return this.m_RendererDataList[this.m_DefaultRendererIndex];
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00012628 File Offset: 0x00010828
		internal GraphicsFormat additionalLightsCookieFormat
		{
			get
			{
				GraphicsFormat graphicsFormat = GraphicsFormat.None;
				foreach (GraphicsFormat graphicsFormat2 in UniversalRenderPipelineAsset.s_LightCookieFormatList[(int)this.m_AdditionalLightsCookieFormat])
				{
					if (SystemInfo.IsFormatSupported(graphicsFormat2, FormatUsage.Render))
					{
						graphicsFormat = graphicsFormat2;
						break;
					}
				}
				if (QualitySettings.activeColorSpace == ColorSpace.Gamma)
				{
					graphicsFormat = GraphicsFormatUtility.GetLinearFormat(graphicsFormat);
				}
				if (graphicsFormat == GraphicsFormat.None)
				{
					graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
					Debug.LogWarning(string.Format("Additional Lights Cookie Format ({0}) is not supported by the platform. Falling back to {1}-bit format ({2})", this.m_AdditionalLightsCookieFormat.ToString(), GraphicsFormatUtility.GetBlockSize(graphicsFormat) * 8U, GraphicsFormatUtility.GetFormatString(graphicsFormat)));
				}
				return graphicsFormat;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000126AA File Offset: 0x000108AA
		internal Vector2Int additionalLightsCookieResolution
		{
			get
			{
				return new Vector2Int((int)this.m_AdditionalLightsCookieResolution, (int)this.m_AdditionalLightsCookieResolution);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000126C0 File Offset: 0x000108C0
		internal int[] rendererIndexList
		{
			get
			{
				int[] array = new int[this.m_RendererDataList.Length + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i - 1;
				}
				return array;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000126F2 File Offset: 0x000108F2
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000126FA File Offset: 0x000108FA
		public bool supportsCameraDepthTexture
		{
			get
			{
				return this.m_RequireDepthTexture;
			}
			set
			{
				this.m_RequireDepthTexture = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00012703 File Offset: 0x00010903
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0001270B File Offset: 0x0001090B
		public bool supportsCameraOpaqueTexture
		{
			get
			{
				return this.m_RequireOpaqueTexture;
			}
			set
			{
				this.m_RequireOpaqueTexture = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00012714 File Offset: 0x00010914
		public Downsampling opaqueDownsampling
		{
			get
			{
				return this.m_OpaqueDownsampling;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0001271C File Offset: 0x0001091C
		public bool supportsTerrainHoles
		{
			get
			{
				return this.m_SupportsTerrainHoles;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00012724 File Offset: 0x00010924
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0001272C File Offset: 0x0001092C
		public StoreActionsOptimization storeActionsOptimization
		{
			get
			{
				return this.m_StoreActionsOptimization;
			}
			set
			{
				this.m_StoreActionsOptimization = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00012735 File Offset: 0x00010935
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0001273D File Offset: 0x0001093D
		public bool supportsHDR
		{
			get
			{
				return this.m_SupportsHDR;
			}
			set
			{
				this.m_SupportsHDR = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00012746 File Offset: 0x00010946
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0001274E File Offset: 0x0001094E
		public int msaaSampleCount
		{
			get
			{
				return (int)this.m_MSAA;
			}
			set
			{
				this.m_MSAA = (MsaaQuality)value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00012757 File Offset: 0x00010957
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0001275F File Offset: 0x0001095F
		public float renderScale
		{
			get
			{
				return this.m_RenderScale;
			}
			set
			{
				this.m_RenderScale = this.ValidateRenderScale(value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001276E File Offset: 0x0001096E
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00012776 File Offset: 0x00010976
		public UpscalingFilterSelection upscalingFilter
		{
			get
			{
				return this.m_UpscalingFilter;
			}
			set
			{
				this.m_UpscalingFilter = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001277F File Offset: 0x0001097F
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00012787 File Offset: 0x00010987
		public bool fsrOverrideSharpness
		{
			get
			{
				return this.m_FsrOverrideSharpness;
			}
			set
			{
				this.m_FsrOverrideSharpness = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00012790 File Offset: 0x00010990
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00012798 File Offset: 0x00010998
		public float fsrSharpness
		{
			get
			{
				return this.m_FsrSharpness;
			}
			set
			{
				this.m_FsrSharpness = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000127A1 File Offset: 0x000109A1
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000127A9 File Offset: 0x000109A9
		public LightRenderingMode mainLightRenderingMode
		{
			get
			{
				return this.m_MainLightRenderingMode;
			}
			internal set
			{
				this.m_MainLightRenderingMode = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000127B2 File Offset: 0x000109B2
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000127BA File Offset: 0x000109BA
		public bool supportsMainLightShadows
		{
			get
			{
				return this.m_MainLightShadowsSupported;
			}
			internal set
			{
				this.m_MainLightShadowsSupported = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000127C3 File Offset: 0x000109C3
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000127CB File Offset: 0x000109CB
		public int mainLightShadowmapResolution
		{
			get
			{
				return (int)this.m_MainLightShadowmapResolution;
			}
			internal set
			{
				this.m_MainLightShadowmapResolution = (ShadowResolution)value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000127D4 File Offset: 0x000109D4
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000127DC File Offset: 0x000109DC
		public LightRenderingMode additionalLightsRenderingMode
		{
			get
			{
				return this.m_AdditionalLightsRenderingMode;
			}
			internal set
			{
				this.m_AdditionalLightsRenderingMode = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000127E5 File Offset: 0x000109E5
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000127ED File Offset: 0x000109ED
		public int maxAdditionalLightsCount
		{
			get
			{
				return this.m_AdditionalLightsPerObjectLimit;
			}
			set
			{
				this.m_AdditionalLightsPerObjectLimit = this.ValidatePerObjectLights(value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000127FC File Offset: 0x000109FC
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00012804 File Offset: 0x00010A04
		public bool supportsAdditionalLightShadows
		{
			get
			{
				return this.m_AdditionalLightShadowsSupported;
			}
			internal set
			{
				this.m_AdditionalLightShadowsSupported = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0001280D File Offset: 0x00010A0D
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00012815 File Offset: 0x00010A15
		public int additionalLightsShadowmapResolution
		{
			get
			{
				return (int)this.m_AdditionalLightsShadowmapResolution;
			}
			internal set
			{
				this.m_AdditionalLightsShadowmapResolution = (ShadowResolution)value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0001281E File Offset: 0x00010A1E
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00012826 File Offset: 0x00010A26
		public int additionalLightsShadowResolutionTierLow
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierLow;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierLow = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0001282F File Offset: 0x00010A2F
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00012837 File Offset: 0x00010A37
		public int additionalLightsShadowResolutionTierMedium
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierMedium;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierMedium = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00012840 File Offset: 0x00010A40
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00012848 File Offset: 0x00010A48
		public int additionalLightsShadowResolutionTierHigh
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTierHigh;
			}
			internal set
			{
				this.m_AdditionalLightsShadowResolutionTierHigh = value;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00012851 File Offset: 0x00010A51
		internal int GetAdditionalLightsShadowResolution(int additionalLightsShadowResolutionTier)
		{
			if (additionalLightsShadowResolutionTier <= UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow)
			{
				return this.additionalLightsShadowResolutionTierLow;
			}
			if (additionalLightsShadowResolutionTier == UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierMedium)
			{
				return this.additionalLightsShadowResolutionTierMedium;
			}
			if (additionalLightsShadowResolutionTier >= UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh)
			{
				return this.additionalLightsShadowResolutionTierHigh;
			}
			return this.additionalLightsShadowResolutionTierMedium;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00012886 File Offset: 0x00010A86
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0001288E File Offset: 0x00010A8E
		public bool reflectionProbeBlending
		{
			get
			{
				return this.m_ReflectionProbeBlending;
			}
			internal set
			{
				this.m_ReflectionProbeBlending = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00012897 File Offset: 0x00010A97
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0001289F File Offset: 0x00010A9F
		public bool reflectionProbeBoxProjection
		{
			get
			{
				return this.m_ReflectionProbeBoxProjection;
			}
			internal set
			{
				this.m_ReflectionProbeBoxProjection = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000128A8 File Offset: 0x00010AA8
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000128B0 File Offset: 0x00010AB0
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = Mathf.Max(0f, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000128C3 File Offset: 0x00010AC3
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000128CB File Offset: 0x00010ACB
		public int shadowCascadeCount
		{
			get
			{
				return this.m_ShadowCascadeCount;
			}
			set
			{
				if (value < 1 || value > 4)
				{
					throw new ArgumentException(string.Format("Value ({0}) needs to be between {1} and {2}.", value, 1, 4));
				}
				this.m_ShadowCascadeCount = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000128FE File Offset: 0x00010AFE
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00012906 File Offset: 0x00010B06
		public float cascade2Split
		{
			get
			{
				return this.m_Cascade2Split;
			}
			internal set
			{
				this.m_Cascade2Split = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0001290F File Offset: 0x00010B0F
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00012917 File Offset: 0x00010B17
		public Vector2 cascade3Split
		{
			get
			{
				return this.m_Cascade3Split;
			}
			internal set
			{
				this.m_Cascade3Split = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00012920 File Offset: 0x00010B20
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00012928 File Offset: 0x00010B28
		public Vector3 cascade4Split
		{
			get
			{
				return this.m_Cascade4Split;
			}
			internal set
			{
				this.m_Cascade4Split = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00012931 File Offset: 0x00010B31
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00012939 File Offset: 0x00010B39
		public float cascadeBorder
		{
			get
			{
				return this.m_CascadeBorder;
			}
			set
			{
				this.m_CascadeBorder = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00012942 File Offset: 0x00010B42
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0001294A File Offset: 0x00010B4A
		public float shadowDepthBias
		{
			get
			{
				return this.m_ShadowDepthBias;
			}
			set
			{
				this.m_ShadowDepthBias = this.ValidateShadowBias(value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00012959 File Offset: 0x00010B59
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00012961 File Offset: 0x00010B61
		public float shadowNormalBias
		{
			get
			{
				return this.m_ShadowNormalBias;
			}
			set
			{
				this.m_ShadowNormalBias = this.ValidateShadowBias(value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00012970 File Offset: 0x00010B70
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00012978 File Offset: 0x00010B78
		public bool supportsSoftShadows
		{
			get
			{
				return this.m_SoftShadowsSupported;
			}
			internal set
			{
				this.m_SoftShadowsSupported = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00012981 File Offset: 0x00010B81
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00012989 File Offset: 0x00010B89
		public bool supportsDynamicBatching
		{
			get
			{
				return this.m_SupportsDynamicBatching;
			}
			set
			{
				this.m_SupportsDynamicBatching = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00012992 File Offset: 0x00010B92
		public bool supportsMixedLighting
		{
			get
			{
				return this.m_MixedLightingSupported;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001299A File Offset: 0x00010B9A
		public bool supportsLightLayers
		{
			get
			{
				return this.m_SupportsLightLayers;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000129A2 File Offset: 0x00010BA2
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x000129AA File Offset: 0x00010BAA
		public ShaderVariantLogLevel shaderVariantLogLevel
		{
			get
			{
				return this.m_ShaderVariantLogLevel;
			}
			set
			{
				this.m_ShaderVariantLogLevel = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000129B3 File Offset: 0x00010BB3
		public VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
		{
			get
			{
				return this.m_VolumeFrameworkUpdateMode;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000129BB File Offset: 0x00010BBB
		[Obsolete("PipelineDebugLevel is deprecated. Calling debugLevel is not necessary.", false)]
		public PipelineDebugLevel debugLevel
		{
			get
			{
				return PipelineDebugLevel.Disabled;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000129BE File Offset: 0x00010BBE
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000129C6 File Offset: 0x00010BC6
		public bool useSRPBatcher
		{
			get
			{
				return this.m_UseSRPBatcher;
			}
			set
			{
				this.m_UseSRPBatcher = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000129CF File Offset: 0x00010BCF
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x000129D7 File Offset: 0x00010BD7
		public ColorGradingMode colorGradingMode
		{
			get
			{
				return this.m_ColorGradingMode;
			}
			set
			{
				this.m_ColorGradingMode = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000129E0 File Offset: 0x00010BE0
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000129E8 File Offset: 0x00010BE8
		public int colorGradingLutSize
		{
			get
			{
				return this.m_ColorGradingLutSize;
			}
			set
			{
				this.m_ColorGradingLutSize = Mathf.Clamp(value, 16, 65);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000129FA File Offset: 0x00010BFA
		public bool useFastSRGBLinearConversion
		{
			get
			{
				return this.m_UseFastSRGBLinearConversion;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00012A02 File Offset: 0x00010C02
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00012A0A File Offset: 0x00010C0A
		public bool useAdaptivePerformance
		{
			get
			{
				return this.m_UseAdaptivePerformance;
			}
			set
			{
				this.m_UseAdaptivePerformance = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00012A13 File Offset: 0x00010C13
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00012A1B File Offset: 0x00010C1B
		public bool conservativeEnclosingSphere
		{
			get
			{
				return this.m_ConservativeEnclosingSphere;
			}
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00012A24 File Offset: 0x00010C24
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00012A2C File Offset: 0x00010C2C
		public int numIterationsEnclosingSphere
		{
			get
			{
				return this.m_NumIterationsEnclosingSphere;
			}
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00012A35 File Offset: 0x00010C35
		public override Material defaultMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Standard);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00012A3E File Offset: 0x00010C3E
		public override Material defaultParticleMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Particle);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00012A47 File Offset: 0x00010C47
		public override Material defaultLineMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Particle);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00012A50 File Offset: 0x00010C50
		public override Material defaultTerrainMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Terrain);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00012A59 File Offset: 0x00010C59
		public override Material defaultUIMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.UnityBuiltinDefault);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00012A62 File Offset: 0x00010C62
		public override Material defaultUIOverdrawMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.UnityBuiltinDefault);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00012A6B File Offset: 0x00010C6B
		public override Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.UnityBuiltinDefault);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00012A74 File Offset: 0x00010C74
		public override Material default2DMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Sprite);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00012A7D File Offset: 0x00010C7D
		public override Material default2DMaskMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.SpriteMask);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00012A86 File Offset: 0x00010C86
		public Material decalMaterial
		{
			get
			{
				return this.GetMaterial(DefaultMaterialType.Decal);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00012A8F File Offset: 0x00010C8F
		public override Shader defaultShader
		{
			get
			{
				if (this.m_DefaultShader == null)
				{
					this.m_DefaultShader = Shader.Find(ShaderUtils.GetShaderPath(ShaderPathID.Lit));
				}
				return this.m_DefaultShader;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00012AB6 File Offset: 0x00010CB6
		public override string[] renderingLayerMaskNames
		{
			get
			{
				return UniversalRenderPipelineGlobalSettings.instance.renderingLayerMaskNames;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00012AC2 File Offset: 0x00010CC2
		public override string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return UniversalRenderPipelineGlobalSettings.instance.prefixedRenderingLayerMaskNames;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00012ACE File Offset: 0x00010CCE
		public string[] lightLayerMaskNames
		{
			get
			{
				return UniversalRenderPipelineGlobalSettings.instance.lightLayerNames;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00012ADA File Offset: 0x00010CDA
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00012ADC File Offset: 0x00010CDC
		public void OnAfterDeserialize()
		{
			if (this.k_AssetVersion < 3)
			{
				this.m_SoftShadowsSupported = this.m_ShadowType == ShadowQuality.SoftShadows;
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 3;
			}
			if (this.k_AssetVersion < 4)
			{
				this.m_AdditionalLightShadowsSupported = this.m_LocalShadowsSupported;
				this.m_AdditionalLightsShadowmapResolution = this.m_LocalShadowsAtlasResolution;
				this.m_AdditionalLightsPerObjectLimit = this.m_MaxPixelLights;
				this.m_MainLightShadowmapResolution = this.m_ShadowAtlasResolution;
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 4;
			}
			if (this.k_AssetVersion < 5)
			{
				if (this.m_RendererType == RendererType.Custom)
				{
					this.m_RendererDataList[0] = this.m_RendererData;
				}
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 5;
			}
			if (this.k_AssetVersion < 6)
			{
				int shadowCascades = (int)this.m_ShadowCascades;
				if (shadowCascades == 2)
				{
					this.m_ShadowCascadeCount = 4;
				}
				else
				{
					this.m_ShadowCascadeCount = shadowCascades + 1;
				}
				this.k_AssetVersion = 6;
			}
			if (this.k_AssetVersion < 7)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 7;
			}
			if (this.k_AssetVersion < 8)
			{
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.m_CascadeBorder = 0.1f;
				this.k_AssetVersion = 8;
			}
			if (this.k_AssetVersion < 9)
			{
				if (this.m_AdditionalLightsShadowResolutionTierHigh == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierHigh && this.m_AdditionalLightsShadowResolutionTierMedium == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierMedium && this.m_AdditionalLightsShadowResolutionTierLow == UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierLow)
				{
					this.m_AdditionalLightsShadowResolutionTierHigh = (int)this.m_AdditionalLightsShadowmapResolution;
					this.m_AdditionalLightsShadowResolutionTierMedium = Mathf.Max(this.m_AdditionalLightsShadowResolutionTierHigh / 2, UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
					this.m_AdditionalLightsShadowResolutionTierLow = Mathf.Max(this.m_AdditionalLightsShadowResolutionTierMedium / 2, UniversalAdditionalLightData.AdditionalLightsShadowMinimumResolution);
				}
				this.k_AssetPreviousVersion = this.k_AssetVersion;
				this.k_AssetVersion = 9;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00012C8B File Offset: 0x00010E8B
		private float ValidateShadowBias(float value)
		{
			return Mathf.Max(0f, Mathf.Min(value, UniversalRenderPipeline.maxShadowBias));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00012CA2 File Offset: 0x00010EA2
		private int ValidatePerObjectLights(int value)
		{
			return Math.Max(0, Math.Min(value, UniversalRenderPipeline.maxPerObjectLights));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00012CB5 File Offset: 0x00010EB5
		private float ValidateRenderScale(float value)
		{
			return Mathf.Max(UniversalRenderPipeline.minRenderScale, Mathf.Min(value, UniversalRenderPipeline.maxRenderScale));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00012CCC File Offset: 0x00010ECC
		internal bool ValidateRendererDataList(bool partial = false)
		{
			int num = 0;
			for (int i = 0; i < this.m_RendererDataList.Length; i++)
			{
				num += (this.ValidateRendererData(i) ? 0 : 1);
			}
			if (partial)
			{
				return num == 0;
			}
			return num != this.m_RendererDataList.Length;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00012D14 File Offset: 0x00010F14
		internal bool ValidateRendererData(int index)
		{
			if (index == -1)
			{
				index = this.m_DefaultRendererIndex;
			}
			return index < this.m_RendererDataList.Length && this.m_RendererDataList[index] != null;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00012D40 File Offset: 0x00010F40
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00012D7E File Offset: 0x00010F7E
		[Obsolete("This is obsolete, please use shadowCascadeCount instead.", false)]
		public ShadowCascadesOption shadowCascadeOption
		{
			get
			{
				switch (this.shadowCascadeCount)
				{
				case 1:
					return ShadowCascadesOption.NoCascades;
				case 2:
					return ShadowCascadesOption.TwoCascades;
				case 4:
					return ShadowCascadesOption.FourCascades;
				}
				throw new InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead.");
			}
			set
			{
				switch (value)
				{
				case ShadowCascadesOption.NoCascades:
					this.shadowCascadeCount = 1;
					return;
				case ShadowCascadesOption.TwoCascades:
					this.shadowCascadeCount = 2;
					return;
				case ShadowCascadesOption.FourCascades:
					this.shadowCascadeCount = 4;
					return;
				default:
					throw new InvalidOperationException("Cascade count is not compatible with obsolete API, please use shadowCascadeCount instead.");
				}
			}
		}

		// Token: 0x040001D9 RID: 473
		private Shader m_DefaultShader;

		// Token: 0x040001DA RID: 474
		private ScriptableRenderer[] m_Renderers = new ScriptableRenderer[1];

		// Token: 0x040001DB RID: 475
		[SerializeField]
		private int k_AssetVersion = 9;

		// Token: 0x040001DC RID: 476
		[SerializeField]
		private int k_AssetPreviousVersion = 9;

		// Token: 0x040001DD RID: 477
		[SerializeField]
		private RendererType m_RendererType = RendererType.UniversalRenderer;

		// Token: 0x040001DE RID: 478
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use m_RendererDataList instead.")]
		[SerializeField]
		internal ScriptableRendererData m_RendererData;

		// Token: 0x040001DF RID: 479
		[SerializeField]
		internal ScriptableRendererData[] m_RendererDataList = new ScriptableRendererData[1];

		// Token: 0x040001E0 RID: 480
		[SerializeField]
		internal int m_DefaultRendererIndex;

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private bool m_RequireDepthTexture;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private bool m_RequireOpaqueTexture;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private Downsampling m_OpaqueDownsampling = Downsampling._2xBilinear;

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		private bool m_SupportsTerrainHoles = true;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private StoreActionsOptimization m_StoreActionsOptimization;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		private bool m_SupportsHDR = true;

		// Token: 0x040001E7 RID: 487
		[SerializeField]
		private MsaaQuality m_MSAA = MsaaQuality.Disabled;

		// Token: 0x040001E8 RID: 488
		[SerializeField]
		private float m_RenderScale = 1f;

		// Token: 0x040001E9 RID: 489
		[SerializeField]
		private UpscalingFilterSelection m_UpscalingFilter;

		// Token: 0x040001EA RID: 490
		[SerializeField]
		private bool m_FsrOverrideSharpness;

		// Token: 0x040001EB RID: 491
		[SerializeField]
		private float m_FsrSharpness = 0.92f;

		// Token: 0x040001EC RID: 492
		[SerializeField]
		private LightRenderingMode m_MainLightRenderingMode = LightRenderingMode.PerPixel;

		// Token: 0x040001ED RID: 493
		[SerializeField]
		private bool m_MainLightShadowsSupported = true;

		// Token: 0x040001EE RID: 494
		[SerializeField]
		private ShadowResolution m_MainLightShadowmapResolution = ShadowResolution._2048;

		// Token: 0x040001EF RID: 495
		[SerializeField]
		private LightRenderingMode m_AdditionalLightsRenderingMode = LightRenderingMode.PerPixel;

		// Token: 0x040001F0 RID: 496
		[SerializeField]
		private int m_AdditionalLightsPerObjectLimit = 4;

		// Token: 0x040001F1 RID: 497
		[SerializeField]
		private bool m_AdditionalLightShadowsSupported;

		// Token: 0x040001F2 RID: 498
		[SerializeField]
		private ShadowResolution m_AdditionalLightsShadowmapResolution = ShadowResolution._2048;

		// Token: 0x040001F3 RID: 499
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierLow = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierLow;

		// Token: 0x040001F4 RID: 500
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierMedium = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierMedium;

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTierHigh = UniversalRenderPipelineAsset.AdditionalLightsDefaultShadowResolutionTierHigh;

		// Token: 0x040001F6 RID: 502
		[SerializeField]
		private bool m_ReflectionProbeBlending;

		// Token: 0x040001F7 RID: 503
		[SerializeField]
		private bool m_ReflectionProbeBoxProjection;

		// Token: 0x040001F8 RID: 504
		[SerializeField]
		private float m_ShadowDistance = 50f;

		// Token: 0x040001F9 RID: 505
		[SerializeField]
		private int m_ShadowCascadeCount = 1;

		// Token: 0x040001FA RID: 506
		[SerializeField]
		private float m_Cascade2Split = 0.25f;

		// Token: 0x040001FB RID: 507
		[SerializeField]
		private Vector2 m_Cascade3Split = new Vector2(0.1f, 0.3f);

		// Token: 0x040001FC RID: 508
		[SerializeField]
		private Vector3 m_Cascade4Split = new Vector3(0.067f, 0.2f, 0.467f);

		// Token: 0x040001FD RID: 509
		[SerializeField]
		private float m_CascadeBorder = 0.2f;

		// Token: 0x040001FE RID: 510
		[SerializeField]
		private float m_ShadowDepthBias = 1f;

		// Token: 0x040001FF RID: 511
		[SerializeField]
		private float m_ShadowNormalBias = 1f;

		// Token: 0x04000200 RID: 512
		[SerializeField]
		private bool m_SoftShadowsSupported;

		// Token: 0x04000201 RID: 513
		[SerializeField]
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04000202 RID: 514
		[SerializeField]
		private int m_NumIterationsEnclosingSphere = 64;

		// Token: 0x04000203 RID: 515
		[SerializeField]
		private LightCookieResolution m_AdditionalLightsCookieResolution = LightCookieResolution._2048;

		// Token: 0x04000204 RID: 516
		[SerializeField]
		private LightCookieFormat m_AdditionalLightsCookieFormat = LightCookieFormat.ColorHigh;

		// Token: 0x04000205 RID: 517
		[SerializeField]
		private bool m_UseSRPBatcher = true;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		private bool m_SupportsDynamicBatching;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		private bool m_MixedLightingSupported = true;

		// Token: 0x04000208 RID: 520
		[SerializeField]
		private bool m_SupportsLightLayers;

		// Token: 0x04000209 RID: 521
		[SerializeField]
		[Obsolete]
		private PipelineDebugLevel m_DebugLevel;

		// Token: 0x0400020A RID: 522
		[SerializeField]
		private bool m_UseAdaptivePerformance = true;

		// Token: 0x0400020B RID: 523
		[SerializeField]
		private ColorGradingMode m_ColorGradingMode;

		// Token: 0x0400020C RID: 524
		[SerializeField]
		private int m_ColorGradingLutSize = 32;

		// Token: 0x0400020D RID: 525
		[SerializeField]
		private bool m_UseFastSRGBLinearConversion;

		// Token: 0x0400020E RID: 526
		[SerializeField]
		private ShadowQuality m_ShadowType = ShadowQuality.HardShadows;

		// Token: 0x0400020F RID: 527
		[SerializeField]
		private bool m_LocalShadowsSupported;

		// Token: 0x04000210 RID: 528
		[SerializeField]
		private ShadowResolution m_LocalShadowsAtlasResolution = ShadowResolution._256;

		// Token: 0x04000211 RID: 529
		[SerializeField]
		private int m_MaxPixelLights;

		// Token: 0x04000212 RID: 530
		[SerializeField]
		private ShadowResolution m_ShadowAtlasResolution = ShadowResolution._256;

		// Token: 0x04000213 RID: 531
		[SerializeField]
		private ShaderVariantLogLevel m_ShaderVariantLogLevel;

		// Token: 0x04000214 RID: 532
		[SerializeField]
		private VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateMode;

		// Token: 0x04000215 RID: 533
		public const int k_MinLutSize = 16;

		// Token: 0x04000216 RID: 534
		public const int k_MaxLutSize = 65;

		// Token: 0x04000217 RID: 535
		internal const int k_ShadowCascadeMinCount = 1;

		// Token: 0x04000218 RID: 536
		internal const int k_ShadowCascadeMaxCount = 4;

		// Token: 0x04000219 RID: 537
		public static readonly int AdditionalLightsDefaultShadowResolutionTierLow = 256;

		// Token: 0x0400021A RID: 538
		public static readonly int AdditionalLightsDefaultShadowResolutionTierMedium = 512;

		// Token: 0x0400021B RID: 539
		public static readonly int AdditionalLightsDefaultShadowResolutionTierHigh = 1024;

		// Token: 0x0400021C RID: 540
		private static GraphicsFormat[][] s_LightCookieFormatList = new GraphicsFormat[][]
		{
			new GraphicsFormat[] { GraphicsFormat.R8_UNorm },
			new GraphicsFormat[] { GraphicsFormat.R16_UNorm },
			new GraphicsFormat[]
			{
				GraphicsFormat.R5G6B5_UNormPack16,
				GraphicsFormat.B5G6R5_UNormPack16,
				GraphicsFormat.R5G5B5A1_UNormPack16,
				GraphicsFormat.B5G5R5A1_UNormPack16
			},
			new GraphicsFormat[]
			{
				GraphicsFormat.A2B10G10R10_UNormPack32,
				GraphicsFormat.R8G8B8A8_SRGB,
				GraphicsFormat.B8G8R8A8_SRGB
			},
			new GraphicsFormat[] { GraphicsFormat.B10G11R11_UFloatPack32 }
		};

		// Token: 0x0400021D RID: 541
		[Obsolete("This is obsolete, please use shadowCascadeCount instead.", false)]
		[SerializeField]
		private ShadowCascadesOption m_ShadowCascades;
	}
}
