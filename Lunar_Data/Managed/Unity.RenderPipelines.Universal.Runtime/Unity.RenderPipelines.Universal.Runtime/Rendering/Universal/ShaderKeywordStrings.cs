using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E7 RID: 231
	public static class ShaderKeywordStrings
	{
		// Token: 0x040005E9 RID: 1513
		public const string MainLightShadows = "_MAIN_LIGHT_SHADOWS";

		// Token: 0x040005EA RID: 1514
		public const string MainLightShadowCascades = "_MAIN_LIGHT_SHADOWS_CASCADE";

		// Token: 0x040005EB RID: 1515
		public const string MainLightShadowScreen = "_MAIN_LIGHT_SHADOWS_SCREEN";

		// Token: 0x040005EC RID: 1516
		public const string CastingPunctualLightShadow = "_CASTING_PUNCTUAL_LIGHT_SHADOW";

		// Token: 0x040005ED RID: 1517
		public const string AdditionalLightsVertex = "_ADDITIONAL_LIGHTS_VERTEX";

		// Token: 0x040005EE RID: 1518
		public const string AdditionalLightsPixel = "_ADDITIONAL_LIGHTS";

		// Token: 0x040005EF RID: 1519
		internal const string ClusteredRendering = "_CLUSTERED_RENDERING";

		// Token: 0x040005F0 RID: 1520
		public const string AdditionalLightShadows = "_ADDITIONAL_LIGHT_SHADOWS";

		// Token: 0x040005F1 RID: 1521
		public const string ReflectionProbeBoxProjection = "_REFLECTION_PROBE_BOX_PROJECTION";

		// Token: 0x040005F2 RID: 1522
		public const string ReflectionProbeBlending = "_REFLECTION_PROBE_BLENDING";

		// Token: 0x040005F3 RID: 1523
		public const string SoftShadows = "_SHADOWS_SOFT";

		// Token: 0x040005F4 RID: 1524
		public const string MixedLightingSubtractive = "_MIXED_LIGHTING_SUBTRACTIVE";

		// Token: 0x040005F5 RID: 1525
		public const string LightmapShadowMixing = "LIGHTMAP_SHADOW_MIXING";

		// Token: 0x040005F6 RID: 1526
		public const string ShadowsShadowMask = "SHADOWS_SHADOWMASK";

		// Token: 0x040005F7 RID: 1527
		public const string LightLayers = "_LIGHT_LAYERS";

		// Token: 0x040005F8 RID: 1528
		public const string RenderPassEnabled = "_RENDER_PASS_ENABLED";

		// Token: 0x040005F9 RID: 1529
		public const string BillboardFaceCameraPos = "BILLBOARD_FACE_CAMERA_POS";

		// Token: 0x040005FA RID: 1530
		public const string LightCookies = "_LIGHT_COOKIES";

		// Token: 0x040005FB RID: 1531
		public const string DepthNoMsaa = "_DEPTH_NO_MSAA";

		// Token: 0x040005FC RID: 1532
		public const string DepthMsaa2 = "_DEPTH_MSAA_2";

		// Token: 0x040005FD RID: 1533
		public const string DepthMsaa4 = "_DEPTH_MSAA_4";

		// Token: 0x040005FE RID: 1534
		public const string DepthMsaa8 = "_DEPTH_MSAA_8";

		// Token: 0x040005FF RID: 1535
		public const string LinearToSRGBConversion = "_LINEAR_TO_SRGB_CONVERSION";

		// Token: 0x04000600 RID: 1536
		internal const string UseFastSRGBLinearConversion = "_USE_FAST_SRGB_LINEAR_CONVERSION";

		// Token: 0x04000601 RID: 1537
		public const string DBufferMRT1 = "_DBUFFER_MRT1";

		// Token: 0x04000602 RID: 1538
		public const string DBufferMRT2 = "_DBUFFER_MRT2";

		// Token: 0x04000603 RID: 1539
		public const string DBufferMRT3 = "_DBUFFER_MRT3";

		// Token: 0x04000604 RID: 1540
		public const string DecalNormalBlendLow = "_DECAL_NORMAL_BLEND_LOW";

		// Token: 0x04000605 RID: 1541
		public const string DecalNormalBlendMedium = "_DECAL_NORMAL_BLEND_MEDIUM";

		// Token: 0x04000606 RID: 1542
		public const string DecalNormalBlendHigh = "_DECAL_NORMAL_BLEND_HIGH";

		// Token: 0x04000607 RID: 1543
		public const string SmaaLow = "_SMAA_PRESET_LOW";

		// Token: 0x04000608 RID: 1544
		public const string SmaaMedium = "_SMAA_PRESET_MEDIUM";

		// Token: 0x04000609 RID: 1545
		public const string SmaaHigh = "_SMAA_PRESET_HIGH";

		// Token: 0x0400060A RID: 1546
		public const string PaniniGeneric = "_GENERIC";

		// Token: 0x0400060B RID: 1547
		public const string PaniniUnitDistance = "_UNIT_DISTANCE";

		// Token: 0x0400060C RID: 1548
		public const string BloomLQ = "_BLOOM_LQ";

		// Token: 0x0400060D RID: 1549
		public const string BloomHQ = "_BLOOM_HQ";

		// Token: 0x0400060E RID: 1550
		public const string BloomLQDirt = "_BLOOM_LQ_DIRT";

		// Token: 0x0400060F RID: 1551
		public const string BloomHQDirt = "_BLOOM_HQ_DIRT";

		// Token: 0x04000610 RID: 1552
		public const string UseRGBM = "_USE_RGBM";

		// Token: 0x04000611 RID: 1553
		public const string Distortion = "_DISTORTION";

		// Token: 0x04000612 RID: 1554
		public const string ChromaticAberration = "_CHROMATIC_ABERRATION";

		// Token: 0x04000613 RID: 1555
		public const string HDRGrading = "_HDR_GRADING";

		// Token: 0x04000614 RID: 1556
		public const string TonemapACES = "_TONEMAP_ACES";

		// Token: 0x04000615 RID: 1557
		public const string TonemapNeutral = "_TONEMAP_NEUTRAL";

		// Token: 0x04000616 RID: 1558
		public const string FilmGrain = "_FILM_GRAIN";

		// Token: 0x04000617 RID: 1559
		public const string Fxaa = "_FXAA";

		// Token: 0x04000618 RID: 1560
		public const string Dithering = "_DITHERING";

		// Token: 0x04000619 RID: 1561
		public const string ScreenSpaceOcclusion = "_SCREEN_SPACE_OCCLUSION";

		// Token: 0x0400061A RID: 1562
		public const string PointSampling = "_POINT_SAMPLING";

		// Token: 0x0400061B RID: 1563
		public const string Rcas = "_RCAS";

		// Token: 0x0400061C RID: 1564
		public const string Gamma20 = "_GAMMA_20";

		// Token: 0x0400061D RID: 1565
		public const string HighQualitySampling = "_HIGH_QUALITY_SAMPLING";

		// Token: 0x0400061E RID: 1566
		public const string DOWNSAMPLING_SIZE_2 = "DOWNSAMPLING_SIZE_2";

		// Token: 0x0400061F RID: 1567
		public const string DOWNSAMPLING_SIZE_4 = "DOWNSAMPLING_SIZE_4";

		// Token: 0x04000620 RID: 1568
		public const string DOWNSAMPLING_SIZE_8 = "DOWNSAMPLING_SIZE_8";

		// Token: 0x04000621 RID: 1569
		public const string DOWNSAMPLING_SIZE_16 = "DOWNSAMPLING_SIZE_16";

		// Token: 0x04000622 RID: 1570
		public const string _SPOT = "_SPOT";

		// Token: 0x04000623 RID: 1571
		public const string _DIRECTIONAL = "_DIRECTIONAL";

		// Token: 0x04000624 RID: 1572
		public const string _POINT = "_POINT";

		// Token: 0x04000625 RID: 1573
		public const string _DEFERRED_STENCIL = "_DEFERRED_STENCIL";

		// Token: 0x04000626 RID: 1574
		public const string _DEFERRED_FIRST_LIGHT = "_DEFERRED_FIRST_LIGHT";

		// Token: 0x04000627 RID: 1575
		public const string _DEFERRED_MAIN_LIGHT = "_DEFERRED_MAIN_LIGHT";

		// Token: 0x04000628 RID: 1576
		public const string _GBUFFER_NORMALS_OCT = "_GBUFFER_NORMALS_OCT";

		// Token: 0x04000629 RID: 1577
		public const string _DEFERRED_MIXED_LIGHTING = "_DEFERRED_MIXED_LIGHTING";

		// Token: 0x0400062A RID: 1578
		public const string LIGHTMAP_ON = "LIGHTMAP_ON";

		// Token: 0x0400062B RID: 1579
		public const string DYNAMICLIGHTMAP_ON = "DYNAMICLIGHTMAP_ON";

		// Token: 0x0400062C RID: 1580
		public const string _ALPHATEST_ON = "_ALPHATEST_ON";

		// Token: 0x0400062D RID: 1581
		public const string DIRLIGHTMAP_COMBINED = "DIRLIGHTMAP_COMBINED";

		// Token: 0x0400062E RID: 1582
		public const string _DETAIL_MULX2 = "_DETAIL_MULX2";

		// Token: 0x0400062F RID: 1583
		public const string _DETAIL_SCALED = "_DETAIL_SCALED";

		// Token: 0x04000630 RID: 1584
		public const string _CLEARCOAT = "_CLEARCOAT";

		// Token: 0x04000631 RID: 1585
		public const string _CLEARCOATMAP = "_CLEARCOATMAP";

		// Token: 0x04000632 RID: 1586
		public const string DEBUG_DISPLAY = "DEBUG_DISPLAY";

		// Token: 0x04000633 RID: 1587
		public const string _EMISSION = "_EMISSION";

		// Token: 0x04000634 RID: 1588
		public const string _RECEIVE_SHADOWS_OFF = "_RECEIVE_SHADOWS_OFF";

		// Token: 0x04000635 RID: 1589
		public const string _SURFACE_TYPE_TRANSPARENT = "_SURFACE_TYPE_TRANSPARENT";

		// Token: 0x04000636 RID: 1590
		public const string _ALPHAPREMULTIPLY_ON = "_ALPHAPREMULTIPLY_ON";

		// Token: 0x04000637 RID: 1591
		public const string _ALPHAMODULATE_ON = "_ALPHAMODULATE_ON";

		// Token: 0x04000638 RID: 1592
		public const string _NORMALMAP = "_NORMALMAP";

		// Token: 0x04000639 RID: 1593
		public const string EDITOR_VISUALIZATION = "EDITOR_VISUALIZATION";

		// Token: 0x0400063A RID: 1594
		public const string UseDrawProcedural = "_USE_DRAW_PROCEDURAL";
	}
}
