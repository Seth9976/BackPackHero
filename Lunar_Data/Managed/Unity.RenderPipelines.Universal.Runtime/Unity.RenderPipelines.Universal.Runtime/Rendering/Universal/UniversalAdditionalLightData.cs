using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D4 RID: 212
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Light))]
	public class UniversalAdditionalLightData : MonoBehaviour, IAdditionalData
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0002101E File Offset: 0x0001F21E
		internal int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00021026 File Offset: 0x0001F226
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0002102E File Offset: 0x0001F22E
		public bool usePipelineSettings
		{
			get
			{
				return this.m_UsePipelineSettings;
			}
			set
			{
				this.m_UsePipelineSettings = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00021037 File Offset: 0x0001F237
		public int additionalLightsShadowResolutionTier
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTier;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0002103F File Offset: 0x0001F23F
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00021047 File Offset: 0x0001F247
		public LightLayerEnum lightLayerMask
		{
			get
			{
				return this.m_LightLayerMask;
			}
			set
			{
				this.m_LightLayerMask = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00021050 File Offset: 0x0001F250
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00021058 File Offset: 0x0001F258
		public bool customShadowLayers
		{
			get
			{
				return this.m_CustomShadowLayers;
			}
			set
			{
				this.m_CustomShadowLayers = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00021061 File Offset: 0x0001F261
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00021069 File Offset: 0x0001F269
		public LightLayerEnum shadowLayerMask
		{
			get
			{
				return this.m_ShadowLayerMask;
			}
			set
			{
				this.m_ShadowLayerMask = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00021072 File Offset: 0x0001F272
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0002107A File Offset: 0x0001F27A
		public Vector2 lightCookieSize
		{
			get
			{
				return this.m_LightCookieSize;
			}
			set
			{
				this.m_LightCookieSize = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00021083 File Offset: 0x0001F283
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0002108B File Offset: 0x0001F28B
		public Vector2 lightCookieOffset
		{
			get
			{
				return this.m_LightCookieOffset;
			}
			set
			{
				this.m_LightCookieOffset = value;
			}
		}

		// Token: 0x040004EA RID: 1258
		[SerializeField]
		private int m_Version = 1;

		// Token: 0x040004EB RID: 1259
		[Tooltip("Controls if light Shadow Bias parameters use pipeline settings.")]
		[SerializeField]
		private bool m_UsePipelineSettings = true;

		// Token: 0x040004EC RID: 1260
		public static readonly int AdditionalLightsShadowResolutionTierCustom = -1;

		// Token: 0x040004ED RID: 1261
		public static readonly int AdditionalLightsShadowResolutionTierLow = 0;

		// Token: 0x040004EE RID: 1262
		public static readonly int AdditionalLightsShadowResolutionTierMedium = 1;

		// Token: 0x040004EF RID: 1263
		public static readonly int AdditionalLightsShadowResolutionTierHigh = 2;

		// Token: 0x040004F0 RID: 1264
		public static readonly int AdditionalLightsShadowDefaultResolutionTier = UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh;

		// Token: 0x040004F1 RID: 1265
		public static readonly int AdditionalLightsShadowDefaultCustomResolution = 128;

		// Token: 0x040004F2 RID: 1266
		public static readonly int AdditionalLightsShadowMinimumResolution = 128;

		// Token: 0x040004F3 RID: 1267
		[Tooltip("Controls if light shadow resolution uses pipeline settings.")]
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTier = UniversalAdditionalLightData.AdditionalLightsShadowDefaultResolutionTier;

		// Token: 0x040004F4 RID: 1268
		[SerializeField]
		private LightLayerEnum m_LightLayerMask = LightLayerEnum.LightLayerDefault;

		// Token: 0x040004F5 RID: 1269
		[SerializeField]
		private bool m_CustomShadowLayers;

		// Token: 0x040004F6 RID: 1270
		[SerializeField]
		private LightLayerEnum m_ShadowLayerMask = LightLayerEnum.LightLayerDefault;

		// Token: 0x040004F7 RID: 1271
		[Tooltip("Controls the size of the cookie mask currently assigned to the light.")]
		[SerializeField]
		private Vector2 m_LightCookieSize = Vector2.one;

		// Token: 0x040004F8 RID: 1272
		[Tooltip("Controls the offset of the cookie mask currently assigned to the light.")]
		[SerializeField]
		private Vector2 m_LightCookieOffset = Vector2.zero;
	}
}
