using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E9 RID: 233
	internal class UniversalRenderPipelineGlobalSettings : RenderPipelineGlobalSettings, ISerializationCallbackReceiver
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x000255C3 File Offset: 0x000237C3
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000255C5 File Offset: 0x000237C5
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000255C7 File Offset: 0x000237C7
		public static UniversalRenderPipelineGlobalSettings instance
		{
			get
			{
				if (UniversalRenderPipelineGlobalSettings.cachedInstance == null)
				{
					UniversalRenderPipelineGlobalSettings.cachedInstance = GraphicsSettings.GetSettingsForRenderPipeline<UniversalRenderPipeline>() as UniversalRenderPipelineGlobalSettings;
				}
				return UniversalRenderPipelineGlobalSettings.cachedInstance;
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000255EA File Offset: 0x000237EA
		internal static void UpdateGraphicsSettings(UniversalRenderPipelineGlobalSettings newSettings)
		{
			if (newSettings == UniversalRenderPipelineGlobalSettings.cachedInstance)
			{
				return;
			}
			if (newSettings != null)
			{
				GraphicsSettings.RegisterRenderPipelineSettings<UniversalRenderPipeline>(newSettings);
			}
			else
			{
				GraphicsSettings.UnregisterRenderPipelineSettings<UniversalRenderPipeline>();
			}
			UniversalRenderPipelineGlobalSettings.cachedInstance = newSettings;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00025616 File Offset: 0x00023816
		private void Reset()
		{
			this.UpdateRenderingLayerNames();
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0002561E File Offset: 0x0002381E
		private string[] renderingLayerNames
		{
			get
			{
				if (this.m_RenderingLayerNames == null)
				{
					this.UpdateRenderingLayerNames();
				}
				return this.m_RenderingLayerNames;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00025634 File Offset: 0x00023834
		private string[] prefixedRenderingLayerNames
		{
			get
			{
				if (this.m_PrefixedRenderingLayerNames == null)
				{
					this.UpdateRenderingLayerNames();
				}
				return this.m_PrefixedRenderingLayerNames;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0002564A File Offset: 0x0002384A
		public string[] renderingLayerMaskNames
		{
			get
			{
				return this.renderingLayerNames;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00025652 File Offset: 0x00023852
		public string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return this.prefixedRenderingLayerNames;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002565C File Offset: 0x0002385C
		internal void UpdateRenderingLayerNames()
		{
			if (this.m_RenderingLayerNames == null)
			{
				this.m_RenderingLayerNames = new string[32];
			}
			int num = 0;
			this.m_RenderingLayerNames[num++] = this.lightLayerName0;
			this.m_RenderingLayerNames[num++] = this.lightLayerName1;
			this.m_RenderingLayerNames[num++] = this.lightLayerName2;
			this.m_RenderingLayerNames[num++] = this.lightLayerName3;
			this.m_RenderingLayerNames[num++] = this.lightLayerName4;
			this.m_RenderingLayerNames[num++] = this.lightLayerName5;
			this.m_RenderingLayerNames[num++] = this.lightLayerName6;
			this.m_RenderingLayerNames[num++] = this.lightLayerName7;
			for (int i = num; i < this.m_RenderingLayerNames.Length; i++)
			{
				this.m_RenderingLayerNames[i] = string.Format("Unused {0}", i);
			}
			if (this.m_PrefixedRenderingLayerNames == null)
			{
				this.m_PrefixedRenderingLayerNames = new string[32];
			}
			if (this.m_PrefixedLightLayerNames == null)
			{
				this.m_PrefixedLightLayerNames = new string[8];
			}
			for (int j = 0; j < this.m_PrefixedRenderingLayerNames.Length; j++)
			{
				this.m_PrefixedRenderingLayerNames[j] = string.Format("{0}: {1}", j, this.m_RenderingLayerNames[j]);
				if (j < 8)
				{
					this.m_PrefixedLightLayerNames[j] = this.m_PrefixedRenderingLayerNames[j];
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000257AB File Offset: 0x000239AB
		public string[] prefixedLightLayerNames
		{
			get
			{
				if (this.m_PrefixedLightLayerNames == null)
				{
					this.UpdateRenderingLayerNames();
				}
				return this.m_PrefixedLightLayerNames;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000257C4 File Offset: 0x000239C4
		public string[] lightLayerNames
		{
			get
			{
				if (this.m_LightLayerNames == null)
				{
					this.m_LightLayerNames = new string[8];
				}
				this.m_LightLayerNames[0] = this.lightLayerName0;
				this.m_LightLayerNames[1] = this.lightLayerName1;
				this.m_LightLayerNames[2] = this.lightLayerName2;
				this.m_LightLayerNames[3] = this.lightLayerName3;
				this.m_LightLayerNames[4] = this.lightLayerName4;
				this.m_LightLayerNames[5] = this.lightLayerName5;
				this.m_LightLayerNames[6] = this.lightLayerName6;
				this.m_LightLayerNames[7] = this.lightLayerName7;
				return this.m_LightLayerNames;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002585C File Offset: 0x00023A5C
		internal void ResetRenderingLayerNames()
		{
			this.lightLayerName0 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[0];
			this.lightLayerName1 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[1];
			this.lightLayerName2 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[2];
			this.lightLayerName3 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[3];
			this.lightLayerName4 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[4];
			this.lightLayerName5 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[5];
			this.lightLayerName6 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[6];
			this.lightLayerName7 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[7];
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000258D1 File Offset: 0x00023AD1
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x000258D9 File Offset: 0x00023AD9
		public bool stripDebugVariants
		{
			get
			{
				return this.m_StripDebugVariants;
			}
			set
			{
				this.m_StripDebugVariants = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000258E2 File Offset: 0x00023AE2
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x000258EA File Offset: 0x00023AEA
		public bool stripUnusedPostProcessingVariants
		{
			get
			{
				return this.m_StripUnusedPostProcessingVariants;
			}
			set
			{
				this.m_StripUnusedPostProcessingVariants = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000258F3 File Offset: 0x00023AF3
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x000258FB File Offset: 0x00023AFB
		public bool stripUnusedVariants
		{
			get
			{
				return this.m_StripUnusedVariants;
			}
			set
			{
				this.m_StripUnusedVariants = value;
			}
		}

		// Token: 0x04000656 RID: 1622
		[SerializeField]
		private int k_AssetVersion = 2;

		// Token: 0x04000657 RID: 1623
		private static UniversalRenderPipelineGlobalSettings cachedInstance = null;

		// Token: 0x04000658 RID: 1624
		public static readonly string defaultAssetName = "UniversalRenderPipelineGlobalSettings";

		// Token: 0x04000659 RID: 1625
		[NonSerialized]
		private string[] m_RenderingLayerNames;

		// Token: 0x0400065A RID: 1626
		[NonSerialized]
		private string[] m_PrefixedRenderingLayerNames;

		// Token: 0x0400065B RID: 1627
		[NonSerialized]
		private string[] m_PrefixedLightLayerNames;

		// Token: 0x0400065C RID: 1628
		private static readonly string[] k_DefaultLightLayerNames = new string[] { "Light Layer default", "Light Layer 1", "Light Layer 2", "Light Layer 3", "Light Layer 4", "Light Layer 5", "Light Layer 6", "Light Layer 7" };

		// Token: 0x0400065D RID: 1629
		public string lightLayerName0 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[0];

		// Token: 0x0400065E RID: 1630
		public string lightLayerName1 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[1];

		// Token: 0x0400065F RID: 1631
		public string lightLayerName2 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[2];

		// Token: 0x04000660 RID: 1632
		public string lightLayerName3 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[3];

		// Token: 0x04000661 RID: 1633
		public string lightLayerName4 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[4];

		// Token: 0x04000662 RID: 1634
		public string lightLayerName5 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[5];

		// Token: 0x04000663 RID: 1635
		public string lightLayerName6 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[6];

		// Token: 0x04000664 RID: 1636
		public string lightLayerName7 = UniversalRenderPipelineGlobalSettings.k_DefaultLightLayerNames[7];

		// Token: 0x04000665 RID: 1637
		[NonSerialized]
		private string[] m_LightLayerNames;

		// Token: 0x04000666 RID: 1638
		[SerializeField]
		private bool m_StripDebugVariants = true;

		// Token: 0x04000667 RID: 1639
		[SerializeField]
		private bool m_StripUnusedPostProcessingVariants;

		// Token: 0x04000668 RID: 1640
		[SerializeField]
		private bool m_StripUnusedVariants = true;

		// Token: 0x04000669 RID: 1641
		[Obsolete("Please use stripRuntimeDebugShaders instead.", false)]
		public bool supportRuntimeDebugDisplay;
	}
}
