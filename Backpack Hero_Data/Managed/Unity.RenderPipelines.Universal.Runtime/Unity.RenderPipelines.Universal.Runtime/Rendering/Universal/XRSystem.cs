using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.XR;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EF RID: 239
	internal class XRSystem
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00026564 File Offset: 0x00024764
		internal XRSystem()
		{
			this.RefreshXrSdk();
			TextureXR.maxViews = Math.Max(TextureXR.slices, this.GetMaxViews());
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000265B4 File Offset: 0x000247B4
		internal void InitializeXRSystemData(XRSystemData data)
		{
			if (data)
			{
				if (this.occlusionMeshMaterial != null)
				{
					CoreUtils.Destroy(this.occlusionMeshMaterial);
				}
				if (this.mirrorViewMaterial != null)
				{
					CoreUtils.Destroy(this.mirrorViewMaterial);
				}
				this.occlusionMeshMaterial = CoreUtils.CreateEngineMaterial(data.shaders.xrOcclusionMeshPS);
				this.mirrorViewMaterial = CoreUtils.CreateEngineMaterial(data.shaders.xrMirrorViewPS);
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00026627 File Offset: 0x00024827
		private static void GetDisplaySubsystem()
		{
			SubsystemManager.GetInstances<XRDisplaySubsystem>(XRSystem.displayList);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00026634 File Offset: 0x00024834
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		internal static void XRSystemInit()
		{
			if (GraphicsSettings.currentRenderPipeline == null)
			{
				return;
			}
			XRSystem.GetDisplaySubsystem();
			for (int i = 0; i < XRSystem.displayList.Count; i++)
			{
				XRSystem.displayList[i].disableLegacyRenderer = true;
				XRSystem.displayList[i].textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
				XRSystem.displayList[i].sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000266A4 File Offset: 0x000248A4
		internal static void UpdateMSAALevel(int level)
		{
			if (XRSystem.msaaLevel == level)
			{
				return;
			}
			level = Mathf.NextPowerOfTwo(level);
			level = Mathf.Clamp(level, 1, 8);
			XRSystem.GetDisplaySubsystem();
			for (int i = 0; i < XRSystem.displayList.Count; i++)
			{
				XRSystem.displayList[i].SetMSAALevel(level);
			}
			XRSystem.msaaLevel = level;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000266FD File Offset: 0x000248FD
		internal static int GetMSAALevel()
		{
			return XRSystem.msaaLevel;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00026704 File Offset: 0x00024904
		internal static void UpdateRenderScale(float renderScale)
		{
			XRSystem.GetDisplaySubsystem();
			for (int i = 0; i < XRSystem.displayList.Count; i++)
			{
				XRSystem.displayList[i].scaleOfAllRenderTargets = renderScale;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0002673C File Offset: 0x0002493C
		internal int GetMaxViews()
		{
			int num = 1;
			if (this.display != null)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00026756 File Offset: 0x00024956
		internal void BeginLateLatching(Camera camera, XRPass xrPass)
		{
			if (this.display != null && xrPass.singlePassEnabled && xrPass.viewCount == 2)
			{
				this.display.BeginRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = true;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00026784 File Offset: 0x00024984
		internal void EndLateLatching(Camera camera, XRPass xrPass)
		{
			if (this.display != null && xrPass.isLateLatchEnabled)
			{
				this.display.EndRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = false;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000267AC File Offset: 0x000249AC
		internal List<XRPass> SetupFrame(Camera camera, bool enableXRRendering)
		{
			bool flag = this.RefreshXrSdk();
			if (this.display != null)
			{
				this.display.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
				this.display.zNear = camera.nearClipPlane;
				this.display.zFar = camera.farClipPlane;
				this.display.sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
			if (this.framePasses.Count > 0)
			{
				Debug.LogWarning("XRSystem.ReleaseFrame() was not called!");
				this.ReleaseFrame();
			}
			if (camera == null)
			{
				return this.framePasses;
			}
			bool flag2 = (camera.cameraType == CameraType.Game || camera.cameraType == CameraType.VR) && camera.targetTexture == null && enableXRRendering;
			if (flag && flag2)
			{
				this.CreateLayoutFromXrSdk(camera, true);
				this.OverrideForAutomatedTests(camera);
			}
			else
			{
				this.AddPassToFrame(this.emptyPass);
			}
			return this.framePasses;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00026888 File Offset: 0x00024A88
		internal void ReleaseFrame()
		{
			for (int i = 0; i < this.framePasses.Count; i++)
			{
				XRPass xrpass = this.framePasses[this.framePasses.Count - i - 1];
				if (xrpass != this.emptyPass)
				{
					XRPass.Release(xrpass);
				}
			}
			this.framePasses.Clear();
			if (this.testRenderTexture)
			{
				RenderTexture.ReleaseTemporary(this.testRenderTexture);
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000268F8 File Offset: 0x00024AF8
		internal bool RefreshXrSdk()
		{
			XRSystem.GetDisplaySubsystem();
			if (XRSystem.displayList.Count <= 0)
			{
				this.display = null;
				return false;
			}
			if (XRSystem.displayList.Count > 1)
			{
				throw new NotImplementedException("Only 1 XR display is supported.");
			}
			this.display = XRSystem.displayList[0];
			this.display.disableLegacyRenderer = true;
			TextureXR.maxViews = Math.Max(TextureXR.slices, this.GetMaxViews());
			return this.display.running;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00026978 File Offset: 0x00024B78
		internal void UpdateCameraData(ref CameraData baseCameraData, in XRPass xr)
		{
			Rect rect = baseCameraData.camera.rect;
			Rect viewport = xr.GetViewport(0);
			baseCameraData.pixelRect = new Rect(rect.x * viewport.width + viewport.x, rect.y * viewport.height + viewport.y, rect.width * viewport.width, rect.height * viewport.height);
			Rect pixelRect = baseCameraData.pixelRect;
			baseCameraData.pixelWidth = (int)Math.Round((double)(pixelRect.width + pixelRect.x)) - (int)Math.Round((double)pixelRect.x);
			baseCameraData.pixelHeight = (int)Math.Round((double)(pixelRect.height + pixelRect.y)) - (int)Math.Round((double)pixelRect.y);
			baseCameraData.aspectRatio = (float)baseCameraData.pixelWidth / (float)baseCameraData.pixelHeight;
			bool flag = Math.Abs(viewport.x) <= 0f && Math.Abs(viewport.y) <= 0f && Math.Abs(viewport.width) >= (float)xr.renderTargetDesc.width && Math.Abs(viewport.height) >= (float)xr.renderTargetDesc.height;
			baseCameraData.isDefaultViewport = baseCameraData.isDefaultViewport && flag;
			RenderTextureDescriptor cameraTargetDescriptor = baseCameraData.cameraTargetDescriptor;
			baseCameraData.cameraTargetDescriptor = xr.renderTargetDesc;
			if (baseCameraData.isHdrEnabled)
			{
				baseCameraData.cameraTargetDescriptor.graphicsFormat = cameraTargetDescriptor.graphicsFormat;
			}
			baseCameraData.cameraTargetDescriptor.msaaSamples = cameraTargetDescriptor.msaaSamples;
			baseCameraData.cameraTargetDescriptor.width = baseCameraData.pixelWidth;
			baseCameraData.cameraTargetDescriptor.height = baseCameraData.pixelHeight;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00026B40 File Offset: 0x00024D40
		internal void UpdateFromCamera(ref XRPass xrPass, CameraData cameraData)
		{
			if (xrPass.enabled && this.display != null)
			{
				XRDisplaySubsystem.XRRenderPass xrrenderPass;
				this.display.GetRenderPass(xrPass.multipassId, out xrrenderPass);
				ScriptableCullingParameters scriptableCullingParameters;
				this.display.GetCullingParameters(cameraData.camera, xrrenderPass.cullingPassIndex, out scriptableCullingParameters);
				scriptableCullingParameters.cullingOptions &= ~CullingOptions.Stereo;
				xrPass.UpdateCullingParams(xrrenderPass.cullingPassIndex, scriptableCullingParameters);
				if (xrPass.singlePassEnabled)
				{
					for (int i = 0; i < xrrenderPass.GetRenderParameterCount(); i++)
					{
						XRDisplaySubsystem.XRRenderParameter xrrenderParameter;
						xrrenderPass.GetRenderParameter(cameraData.camera, i, out xrrenderParameter);
						xrPass.UpdateView(i, xrrenderPass, xrrenderParameter);
					}
				}
				else
				{
					XRDisplaySubsystem.XRRenderParameter xrrenderParameter2;
					xrrenderPass.GetRenderParameter(cameraData.camera, 0, out xrrenderParameter2);
					xrPass.UpdateView(0, xrrenderPass, xrrenderParameter2);
				}
				this.OverrideForAutomatedTests(cameraData.camera);
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00026C10 File Offset: 0x00024E10
		private void CreateLayoutFromXrSdk(Camera camera, bool singlePassAllowed)
		{
			XRSystem.<>c__DisplayClass26_0 CS$<>8__locals1;
			CS$<>8__locals1.camera = camera;
			for (int i = 0; i < this.display.GetRenderPassCount(); i++)
			{
				XRDisplaySubsystem.XRRenderPass xrrenderPass;
				this.display.GetRenderPass(i, out xrrenderPass);
				ScriptableCullingParameters scriptableCullingParameters;
				this.display.GetCullingParameters(CS$<>8__locals1.camera, xrrenderPass.cullingPassIndex, out scriptableCullingParameters);
				scriptableCullingParameters.cullingOptions &= ~CullingOptions.Stereo;
				if (singlePassAllowed && XRSystem.<CreateLayoutFromXrSdk>g__CanUseSinglePass|26_0(xrrenderPass, ref CS$<>8__locals1))
				{
					XRPass xrpass = XRPass.Create(xrrenderPass, this.framePasses.Count, scriptableCullingParameters, this.occlusionMeshMaterial);
					for (int j = 0; j < xrrenderPass.GetRenderParameterCount(); j++)
					{
						XRDisplaySubsystem.XRRenderParameter xrrenderParameter;
						xrrenderPass.GetRenderParameter(CS$<>8__locals1.camera, j, out xrrenderParameter);
						xrpass.AddView(xrrenderPass, xrrenderParameter);
					}
					this.AddPassToFrame(xrpass);
				}
				else
				{
					for (int k = 0; k < xrrenderPass.GetRenderParameterCount(); k++)
					{
						XRDisplaySubsystem.XRRenderParameter xrrenderParameter2;
						xrrenderPass.GetRenderParameter(CS$<>8__locals1.camera, k, out xrrenderParameter2);
						XRPass xrpass2 = XRPass.Create(xrrenderPass, this.framePasses.Count, scriptableCullingParameters, this.occlusionMeshMaterial);
						xrpass2.AddView(xrrenderPass, xrrenderParameter2);
						this.AddPassToFrame(xrpass2);
					}
				}
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00026D2D File Offset: 0x00024F2D
		internal void Dispose()
		{
			CoreUtils.Destroy(this.occlusionMeshMaterial);
			CoreUtils.Destroy(this.mirrorViewMaterial);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00026D45 File Offset: 0x00024F45
		internal void AddPassToFrame(XRPass xrPass)
		{
			xrPass.UpdateOcclusionMesh();
			this.framePasses.Add(xrPass);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00026D5C File Offset: 0x00024F5C
		internal void RenderMirrorView(CommandBuffer cmd, Camera camera)
		{
			if (Application.platform == RuntimePlatform.Android && !XRGraphicsAutomatedTests.running)
			{
				return;
			}
			if (this.display == null || !this.display.running || !this.mirrorViewMaterial)
			{
				return;
			}
			using (new ProfilingScope(cmd, XRSystem._XRMirrorProfilingSampler))
			{
				cmd.SetRenderTarget((camera.targetTexture != null) ? camera.targetTexture : new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget));
				bool flag = camera.targetTexture != null || camera.cameraType == CameraType.SceneView || camera.cameraType == CameraType.Preview;
				int preferredMirrorBlitMode = this.display.GetPreferredMirrorBlitMode();
				XRDisplaySubsystem.XRMirrorViewBlitDesc xrmirrorViewBlitDesc;
				if (this.display.GetMirrorViewBlitDesc(null, out xrmirrorViewBlitDesc, preferredMirrorBlitMode))
				{
					if (xrmirrorViewBlitDesc.nativeBlitAvailable)
					{
						this.display.AddGraphicsThreadMirrorViewBlit(cmd, xrmirrorViewBlitDesc.nativeBlitInvalidStates, preferredMirrorBlitMode);
					}
					else
					{
						for (int i = 0; i < xrmirrorViewBlitDesc.blitParamsCount; i++)
						{
							XRDisplaySubsystem.XRBlitParams xrblitParams;
							xrmirrorViewBlitDesc.GetBlitParameter(i, out xrblitParams);
							Vector4 vector = (flag ? new Vector4(xrblitParams.srcRect.width, -xrblitParams.srcRect.height, xrblitParams.srcRect.x, xrblitParams.srcRect.height + xrblitParams.srcRect.y) : new Vector4(xrblitParams.srcRect.width, xrblitParams.srcRect.height, xrblitParams.srcRect.x, xrblitParams.srcRect.y));
							Vector4 vector2 = new Vector4(xrblitParams.destRect.width, xrblitParams.destRect.height, xrblitParams.destRect.x, xrblitParams.destRect.y);
							this.mirrorViewMaterialProperty.SetFloat(XRSystem.XRShaderIDs._SRGBRead, xrblitParams.srcTex.sRGB ? 0f : 1f);
							this.mirrorViewMaterialProperty.SetFloat(XRSystem.XRShaderIDs._SRGBWrite, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 0f : 1f);
							this.mirrorViewMaterialProperty.SetTexture(ShaderPropertyId.sourceTex, xrblitParams.srcTex);
							this.mirrorViewMaterialProperty.SetVector(ShaderPropertyId.scaleBias, vector);
							this.mirrorViewMaterialProperty.SetVector(ShaderPropertyId.scaleBiasRt, vector2);
							this.mirrorViewMaterialProperty.SetFloat(XRSystem.XRShaderIDs._SourceTexArraySlice, (float)xrblitParams.srcTexArraySlice);
							int num = ((xrblitParams.srcTex.dimension == TextureDimension.Tex2DArray) ? 1 : 0);
							cmd.DrawProcedural(Matrix4x4.identity, this.mirrorViewMaterial, num, MeshTopology.Quads, 4, 1, this.mirrorViewMaterialProperty);
						}
					}
				}
				else
				{
					cmd.ClearRenderTarget(true, true, Color.black);
				}
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00027020 File Offset: 0x00025220
		private void OverrideForAutomatedTests(Camera camera)
		{
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00027044 File Offset: 0x00025244
		[CompilerGenerated]
		internal static bool <CreateLayoutFromXrSdk>g__CanUseSinglePass|26_0(XRDisplaySubsystem.XRRenderPass renderPass, ref XRSystem.<>c__DisplayClass26_0 A_1)
		{
			if (renderPass.renderTargetDesc.dimension != TextureDimension.Tex2DArray)
			{
				return false;
			}
			if (renderPass.GetRenderParameterCount() != 2 || renderPass.renderTargetDesc.volumeDepth != 2)
			{
				return false;
			}
			XRDisplaySubsystem.XRRenderParameter xrrenderParameter;
			renderPass.GetRenderParameter(A_1.camera, 0, out xrrenderParameter);
			XRDisplaySubsystem.XRRenderParameter xrrenderParameter2;
			renderPass.GetRenderParameter(A_1.camera, 1, out xrrenderParameter2);
			return xrrenderParameter.textureArraySlice == 0 && xrrenderParameter2.textureArraySlice == 1 && !(xrrenderParameter.viewport != xrrenderParameter2.viewport);
		}

		// Token: 0x04000698 RID: 1688
		internal readonly XRPass emptyPass = new XRPass();

		// Token: 0x04000699 RID: 1689
		private List<XRPass> framePasses = new List<XRPass>();

		// Token: 0x0400069A RID: 1690
		private static List<XRDisplaySubsystem> displayList = new List<XRDisplaySubsystem>();

		// Token: 0x0400069B RID: 1691
		private XRDisplaySubsystem display;

		// Token: 0x0400069C RID: 1692
		private static int msaaLevel = 1;

		// Token: 0x0400069D RID: 1693
		private Material occlusionMeshMaterial;

		// Token: 0x0400069E RID: 1694
		private Material mirrorViewMaterial;

		// Token: 0x0400069F RID: 1695
		private MaterialPropertyBlock mirrorViewMaterialProperty = new MaterialPropertyBlock();

		// Token: 0x040006A0 RID: 1696
		private RenderTexture testRenderTexture;

		// Token: 0x040006A1 RID: 1697
		private const string k_XRMirrorTag = "XR Mirror View";

		// Token: 0x040006A2 RID: 1698
		private static ProfilingSampler _XRMirrorProfilingSampler = new ProfilingSampler("XR Mirror View");

		// Token: 0x0200018F RID: 399
		internal static class XRShaderIDs
		{
			// Token: 0x04000A09 RID: 2569
			public static readonly int _SourceTexArraySlice = Shader.PropertyToID("_SourceTexArraySlice");

			// Token: 0x04000A0A RID: 2570
			public static readonly int _SRGBRead = Shader.PropertyToID("_SRGBRead");

			// Token: 0x04000A0B RID: 2571
			public static readonly int _SRGBWrite = Shader.PropertyToID("_SRGBWrite");
		}
	}
}
