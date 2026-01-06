using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000416 RID: 1046
	public class SupportedRenderingFeatures
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0003D32C File Offset: 0x0003B52C
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x0003D359 File Offset: 0x0003B559
		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0003D362 File Offset: 0x0003B562
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x0003D36A File Offset: 0x0003B56A
		public SupportedRenderingFeatures.ReflectionProbeModes reflectionProbeModes { get; set; } = SupportedRenderingFeatures.ReflectionProbeModes.None;

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x0003D373 File Offset: 0x0003B573
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x0003D37B File Offset: 0x0003B57B
		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.None;

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x0003D384 File Offset: 0x0003B584
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x0003D38C File Offset: 0x0003B58C
		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask;

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x0003D395 File Offset: 0x0003B595
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x0003D39D File Offset: 0x0003B59D
		public LightmapBakeType lightmapBakeTypes { get; set; } = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x0003D3A6 File Offset: 0x0003B5A6
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x0003D3AE File Offset: 0x0003B5AE
		public LightmapsMode lightmapsModes { get; set; } = LightmapsMode.CombinedDirectional;

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x0003D3B7 File Offset: 0x0003B5B7
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x0003D3BF File Offset: 0x0003B5BF
		public bool enlightenLightmapper { get; set; } = true;

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		// (set) Token: 0x06002439 RID: 9273 RVA: 0x0003D3D0 File Offset: 0x0003B5D0
		public bool enlighten { get; set; } = true;

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x0003D3D9 File Offset: 0x0003B5D9
		// (set) Token: 0x0600243B RID: 9275 RVA: 0x0003D3E1 File Offset: 0x0003B5E1
		public bool lightProbeProxyVolumes { get; set; } = true;

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x0003D3EA File Offset: 0x0003B5EA
		// (set) Token: 0x0600243D RID: 9277 RVA: 0x0003D3F2 File Offset: 0x0003B5F2
		public bool motionVectors { get; set; } = true;

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x0003D3FB File Offset: 0x0003B5FB
		// (set) Token: 0x0600243F RID: 9279 RVA: 0x0003D403 File Offset: 0x0003B603
		public bool receiveShadows { get; set; } = true;

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x0003D40C File Offset: 0x0003B60C
		// (set) Token: 0x06002441 RID: 9281 RVA: 0x0003D414 File Offset: 0x0003B614
		public bool reflectionProbes { get; set; } = true;

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0003D41D File Offset: 0x0003B61D
		// (set) Token: 0x06002443 RID: 9283 RVA: 0x0003D425 File Offset: 0x0003B625
		public bool reflectionProbesBlendDistance { get; set; } = true;

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x0003D42E File Offset: 0x0003B62E
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x0003D436 File Offset: 0x0003B636
		public bool rendererPriority { get; set; } = false;

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x0003D43F File Offset: 0x0003B63F
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x0003D447 File Offset: 0x0003B647
		public bool rendersUIOverlay { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x0003D450 File Offset: 0x0003B650
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x0003D458 File Offset: 0x0003B658
		public bool overridesEnvironmentLighting { get; set; } = false;

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0003D461 File Offset: 0x0003B661
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x0003D469 File Offset: 0x0003B669
		public bool overridesFog { get; set; } = false;

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0003D472 File Offset: 0x0003B672
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x0003D47A File Offset: 0x0003B67A
		public bool overridesRealtimeReflectionProbes { get; set; } = false;

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0003D483 File Offset: 0x0003B683
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x0003D48B File Offset: 0x0003B68B
		public bool overridesOtherLightingSettings { get; set; } = false;

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0003D494 File Offset: 0x0003B694
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0003D49C File Offset: 0x0003B69C
		public bool editableMaterialRenderQueue { get; set; } = true;

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0003D4A5 File Offset: 0x0003B6A5
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0003D4AD File Offset: 0x0003B6AD
		public bool overridesLODBias { get; set; } = false;

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0003D4B6 File Offset: 0x0003B6B6
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0003D4BE File Offset: 0x0003B6BE
		public bool overridesMaximumLODLevel { get; set; } = false;

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x0003D4C7 File Offset: 0x0003B6C7
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x0003D4CF File Offset: 0x0003B6CF
		public bool rendererProbes { get; set; } = true;

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0003D4D8 File Offset: 0x0003B6D8
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x0003D4E0 File Offset: 0x0003B6E0
		public bool particleSystemInstancing { get; set; } = true;

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x0003D4F1 File Offset: 0x0003B6F1
		public bool autoAmbientProbeBaking { get; set; } = true;

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0003D4FA File Offset: 0x0003B6FA
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x0003D502 File Offset: 0x0003B702
		public bool autoDefaultReflectionProbeBaking { get; set; } = true;

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x0003D50B File Offset: 0x0003B70B
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0003D513 File Offset: 0x0003B713
		public bool overridesShadowmask { get; set; } = false;

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0003D51C File Offset: 0x0003B71C
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x0003D524 File Offset: 0x0003B724
		public string overrideShadowmaskMessage { get; set; } = "";

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0003D530 File Offset: 0x0003B730
		public string shadowmaskMessage
		{
			get
			{
				bool flag = !this.overridesShadowmask;
				string text;
				if (flag)
				{
					text = "The Shadowmask Mode used at run time can be set in the Quality Settings panel.";
				}
				else
				{
					text = this.overrideShadowmaskMessage;
				}
				return text;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0003D560 File Offset: 0x0003B760
		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode mixedLightingMode;
			SupportedRenderingFeatures.FallbackMixedLightingModeByRef(new IntPtr((void*)(&mixedLightingMode)));
			return mixedLightingMode;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0003D584 File Offset: 0x0003B784
		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)(void*)fallbackModePtr;
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				SupportedRenderingFeatures.LightmapMixedBakeModes lightmapMixedBakeModes = defaultMixedLightingModes;
				if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*ptr = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*ptr = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*ptr = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*ptr = MixedLightingMode.Subtractive;
					}
					else
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0003D620 File Offset: 0x0003B820
		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool flag;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0003D644 File Offset: 0x0003B844
		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*ptr = false;
			}
			else
			{
				*ptr = (mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask);
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0003D6AC File Offset: 0x0003B8AC
		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0003D6D0 File Offset: 0x0003B8D0
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool flag2 = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag3 = !flag2 || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag3)
				{
					*ptr = false;
					return;
				}
			}
			*ptr = (SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType;
			bool flag4 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag4)
			{
				*ptr = false;
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0003D744 File Offset: 0x0003B944
		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapsModeSupportedByRef(mode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0003D768 File Offset: 0x0003B968
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = (SupportedRenderingFeatures.active.lightmapsModes & mode) == mode;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0003D790 File Offset: 0x0003B990
		internal unsafe static bool IsLightmapperSupported(int lightmapper)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapperSupportedByRef(lightmapper, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = lightmapper != 0 || SupportedRenderingFeatures.active.enlightenLightmapper;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0003D800 File Offset: 0x0003BA00
		[RequiredByNativeCode]
		internal unsafe static void IsAutoAmbientProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.autoAmbientProbeBaking;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0003D824 File Offset: 0x0003BA24
		[RequiredByNativeCode]
		internal unsafe static void IsAutoDefaultReflectionProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.autoDefaultReflectionProbeBaking;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0003D848 File Offset: 0x0003BA48
		internal unsafe static int FallbackLightmapper()
		{
			int num;
			SupportedRenderingFeatures.FallbackLightmapperByRef(new IntPtr((void*)(&num)));
			return num;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0003D86C File Offset: 0x0003BA6C
		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* ptr = (int*)(void*)lightmapperPtr;
			*ptr = 1;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x0003D884 File Offset: 0x0003BA84
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("terrainDetailUnsupported is deprecated.")]
		public bool terrainDetailUnsupported
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x04000D63 RID: 3427
		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		// Token: 0x02000417 RID: 1047
		[Flags]
		public enum ReflectionProbeModes
		{
			// Token: 0x04000D80 RID: 3456
			None = 0,
			// Token: 0x04000D81 RID: 3457
			Rotation = 1
		}

		// Token: 0x02000418 RID: 1048
		[Flags]
		public enum LightmapMixedBakeModes
		{
			// Token: 0x04000D83 RID: 3459
			None = 0,
			// Token: 0x04000D84 RID: 3460
			IndirectOnly = 1,
			// Token: 0x04000D85 RID: 3461
			Subtractive = 2,
			// Token: 0x04000D86 RID: 3462
			Shadowmask = 4
		}
	}
}
