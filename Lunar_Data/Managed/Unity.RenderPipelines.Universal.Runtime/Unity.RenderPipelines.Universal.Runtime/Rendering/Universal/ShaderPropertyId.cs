using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E5 RID: 229
	internal static class ShaderPropertyId
	{
		// Token: 0x040005C0 RID: 1472
		public static readonly int glossyEnvironmentColor = Shader.PropertyToID("_GlossyEnvironmentColor");

		// Token: 0x040005C1 RID: 1473
		public static readonly int subtractiveShadowColor = Shader.PropertyToID("_SubtractiveShadowColor");

		// Token: 0x040005C2 RID: 1474
		public static readonly int glossyEnvironmentCubeMap = Shader.PropertyToID("_GlossyEnvironmentCubeMap");

		// Token: 0x040005C3 RID: 1475
		public static readonly int glossyEnvironmentCubeMapHDR = Shader.PropertyToID("_GlossyEnvironmentCubeMap_HDR");

		// Token: 0x040005C4 RID: 1476
		public static readonly int ambientSkyColor = Shader.PropertyToID("unity_AmbientSky");

		// Token: 0x040005C5 RID: 1477
		public static readonly int ambientEquatorColor = Shader.PropertyToID("unity_AmbientEquator");

		// Token: 0x040005C6 RID: 1478
		public static readonly int ambientGroundColor = Shader.PropertyToID("unity_AmbientGround");

		// Token: 0x040005C7 RID: 1479
		public static readonly int time = Shader.PropertyToID("_Time");

		// Token: 0x040005C8 RID: 1480
		public static readonly int sinTime = Shader.PropertyToID("_SinTime");

		// Token: 0x040005C9 RID: 1481
		public static readonly int cosTime = Shader.PropertyToID("_CosTime");

		// Token: 0x040005CA RID: 1482
		public static readonly int deltaTime = Shader.PropertyToID("unity_DeltaTime");

		// Token: 0x040005CB RID: 1483
		public static readonly int timeParameters = Shader.PropertyToID("_TimeParameters");

		// Token: 0x040005CC RID: 1484
		public static readonly int scaledScreenParams = Shader.PropertyToID("_ScaledScreenParams");

		// Token: 0x040005CD RID: 1485
		public static readonly int worldSpaceCameraPos = Shader.PropertyToID("_WorldSpaceCameraPos");

		// Token: 0x040005CE RID: 1486
		public static readonly int screenParams = Shader.PropertyToID("_ScreenParams");

		// Token: 0x040005CF RID: 1487
		public static readonly int projectionParams = Shader.PropertyToID("_ProjectionParams");

		// Token: 0x040005D0 RID: 1488
		public static readonly int zBufferParams = Shader.PropertyToID("_ZBufferParams");

		// Token: 0x040005D1 RID: 1489
		public static readonly int orthoParams = Shader.PropertyToID("unity_OrthoParams");

		// Token: 0x040005D2 RID: 1490
		public static readonly int globalMipBias = Shader.PropertyToID("_GlobalMipBias");

		// Token: 0x040005D3 RID: 1491
		public static readonly int screenSize = Shader.PropertyToID("_ScreenSize");

		// Token: 0x040005D4 RID: 1492
		public static readonly int viewMatrix = Shader.PropertyToID("unity_MatrixV");

		// Token: 0x040005D5 RID: 1493
		public static readonly int projectionMatrix = Shader.PropertyToID("glstate_matrix_projection");

		// Token: 0x040005D6 RID: 1494
		public static readonly int viewAndProjectionMatrix = Shader.PropertyToID("unity_MatrixVP");

		// Token: 0x040005D7 RID: 1495
		public static readonly int inverseViewMatrix = Shader.PropertyToID("unity_MatrixInvV");

		// Token: 0x040005D8 RID: 1496
		public static readonly int inverseProjectionMatrix = Shader.PropertyToID("unity_MatrixInvP");

		// Token: 0x040005D9 RID: 1497
		public static readonly int inverseViewAndProjectionMatrix = Shader.PropertyToID("unity_MatrixInvVP");

		// Token: 0x040005DA RID: 1498
		public static readonly int cameraProjectionMatrix = Shader.PropertyToID("unity_CameraProjection");

		// Token: 0x040005DB RID: 1499
		public static readonly int inverseCameraProjectionMatrix = Shader.PropertyToID("unity_CameraInvProjection");

		// Token: 0x040005DC RID: 1500
		public static readonly int worldToCameraMatrix = Shader.PropertyToID("unity_WorldToCamera");

		// Token: 0x040005DD RID: 1501
		public static readonly int cameraToWorldMatrix = Shader.PropertyToID("unity_CameraToWorld");

		// Token: 0x040005DE RID: 1502
		public static readonly int cameraWorldClipPlanes = Shader.PropertyToID("unity_CameraWorldClipPlanes");

		// Token: 0x040005DF RID: 1503
		public static readonly int billboardNormal = Shader.PropertyToID("unity_BillboardNormal");

		// Token: 0x040005E0 RID: 1504
		public static readonly int billboardTangent = Shader.PropertyToID("unity_BillboardTangent");

		// Token: 0x040005E1 RID: 1505
		public static readonly int billboardCameraParams = Shader.PropertyToID("unity_BillboardCameraParams");

		// Token: 0x040005E2 RID: 1506
		public static readonly int sourceTex = Shader.PropertyToID("_SourceTex");

		// Token: 0x040005E3 RID: 1507
		public static readonly int scaleBias = Shader.PropertyToID("_ScaleBias");

		// Token: 0x040005E4 RID: 1508
		public static readonly int scaleBiasRt = Shader.PropertyToID("_ScaleBiasRt");

		// Token: 0x040005E5 RID: 1509
		public static readonly int rendererColor = Shader.PropertyToID("_RendererColor");
	}
}
