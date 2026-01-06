using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E0 RID: 224
	public struct CameraData
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0002520F File Offset: 0x0002340F
		internal void SetViewAndProjectionMatrix(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
		{
			this.m_ViewMatrix = viewMatrix;
			this.m_ProjectionMatrix = projectionMatrix;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002521F File Offset: 0x0002341F
		public Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			if (this.xr.enabled)
			{
				return this.xr.GetViewMatrix(viewIndex);
			}
			return this.m_ViewMatrix;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00025241 File Offset: 0x00023441
		public Matrix4x4 GetProjectionMatrix(int viewIndex = 0)
		{
			if (this.xr.enabled)
			{
				return this.xr.GetProjMatrix(viewIndex);
			}
			return this.m_ProjectionMatrix;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00025263 File Offset: 0x00023463
		public Matrix4x4 GetGPUProjectionMatrix(int viewIndex = 0)
		{
			return GL.GetGPUProjectionMatrix(this.GetProjectionMatrix(viewIndex), this.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00025278 File Offset: 0x00023478
		internal bool requireSrgbConversion
		{
			get
			{
				if (this.xr.enabled)
				{
					return !this.xr.renderTargetDesc.sRGB && QualitySettings.activeColorSpace == ColorSpace.Linear;
				}
				return this.targetTexture == null && Display.main.requiresSrgbBlitToBackbuffer;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000252CC File Offset: 0x000234CC
		public bool isSceneViewCamera
		{
			get
			{
				return this.cameraType == CameraType.SceneView;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000252D7 File Offset: 0x000234D7
		public bool isPreviewCamera
		{
			get
			{
				return this.cameraType == CameraType.Preview;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x000252E2 File Offset: 0x000234E2
		internal bool isRenderPassSupportedCamera
		{
			get
			{
				return this.cameraType == CameraType.Game || this.cameraType == CameraType.Reflection;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000252FC File Offset: 0x000234FC
		public bool IsCameraProjectionMatrixFlipped()
		{
			ScriptableRenderer current = ScriptableRenderer.current;
			if (current != null)
			{
				bool flag = current.cameraColorTarget == BuiltinRenderTextureType.CameraTarget;
				if (this.xr.enabled)
				{
					flag |= current.cameraColorTarget == this.xr.renderTarget && !this.xr.renderTargetIsRenderTexture;
				}
				bool flag2 = !flag || this.targetTexture != null;
				return SystemInfo.graphicsUVStartsAtTop && flag2;
			}
			return true;
		}

		// Token: 0x0400057A RID: 1402
		private Matrix4x4 m_ViewMatrix;

		// Token: 0x0400057B RID: 1403
		private Matrix4x4 m_ProjectionMatrix;

		// Token: 0x0400057C RID: 1404
		public Camera camera;

		// Token: 0x0400057D RID: 1405
		public CameraRenderType renderType;

		// Token: 0x0400057E RID: 1406
		public RenderTexture targetTexture;

		// Token: 0x0400057F RID: 1407
		public RenderTextureDescriptor cameraTargetDescriptor;

		// Token: 0x04000580 RID: 1408
		internal Rect pixelRect;

		// Token: 0x04000581 RID: 1409
		internal int pixelWidth;

		// Token: 0x04000582 RID: 1410
		internal int pixelHeight;

		// Token: 0x04000583 RID: 1411
		internal float aspectRatio;

		// Token: 0x04000584 RID: 1412
		public float renderScale;

		// Token: 0x04000585 RID: 1413
		internal ImageScalingMode imageScalingMode;

		// Token: 0x04000586 RID: 1414
		internal ImageUpscalingFilter upscalingFilter;

		// Token: 0x04000587 RID: 1415
		internal bool fsrOverrideSharpness;

		// Token: 0x04000588 RID: 1416
		internal float fsrSharpness;

		// Token: 0x04000589 RID: 1417
		public bool clearDepth;

		// Token: 0x0400058A RID: 1418
		public CameraType cameraType;

		// Token: 0x0400058B RID: 1419
		public bool isDefaultViewport;

		// Token: 0x0400058C RID: 1420
		public bool isHdrEnabled;

		// Token: 0x0400058D RID: 1421
		public bool requiresDepthTexture;

		// Token: 0x0400058E RID: 1422
		public bool requiresOpaqueTexture;

		// Token: 0x0400058F RID: 1423
		public bool postProcessingRequiresDepthTexture;

		// Token: 0x04000590 RID: 1424
		public bool xrRendering;

		// Token: 0x04000591 RID: 1425
		public SortingCriteria defaultOpaqueSortFlags;

		// Token: 0x04000592 RID: 1426
		internal XRPass xr;

		// Token: 0x04000593 RID: 1427
		[Obsolete("Please use xr.enabled instead.")]
		public bool isStereoEnabled;

		// Token: 0x04000594 RID: 1428
		public float maxShadowDistance;

		// Token: 0x04000595 RID: 1429
		public bool postProcessEnabled;

		// Token: 0x04000596 RID: 1430
		public IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions;

		// Token: 0x04000597 RID: 1431
		public LayerMask volumeLayerMask;

		// Token: 0x04000598 RID: 1432
		public Transform volumeTrigger;

		// Token: 0x04000599 RID: 1433
		public bool isStopNaNEnabled;

		// Token: 0x0400059A RID: 1434
		public bool isDitheringEnabled;

		// Token: 0x0400059B RID: 1435
		public AntialiasingMode antialiasing;

		// Token: 0x0400059C RID: 1436
		public AntialiasingQuality antialiasingQuality;

		// Token: 0x0400059D RID: 1437
		public ScriptableRenderer renderer;

		// Token: 0x0400059E RID: 1438
		public bool resolveFinalTarget;

		// Token: 0x0400059F RID: 1439
		public Vector3 worldSpaceCameraPos;
	}
}
