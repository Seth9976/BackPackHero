using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EE RID: 238
	internal class XRPass
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00025B29 File Offset: 0x00023D29
		internal bool enabled
		{
			get
			{
				return this.views.Count > 0;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00025B39 File Offset: 0x00023D39
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00025B41 File Offset: 0x00023D41
		internal bool xrSdkEnabled { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00025B4A File Offset: 0x00023D4A
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00025B52 File Offset: 0x00023D52
		internal bool copyDepth { get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00025B5B File Offset: 0x00023D5B
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00025B63 File Offset: 0x00023D63
		internal int multipassId { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00025B6C File Offset: 0x00023D6C
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00025B74 File Offset: 0x00023D74
		internal int cullingPassId { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00025B7D File Offset: 0x00023D7D
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00025B85 File Offset: 0x00023D85
		internal RenderTargetIdentifier renderTarget { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00025B8E File Offset: 0x00023D8E
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00025B96 File Offset: 0x00023D96
		internal RenderTextureDescriptor renderTargetDesc { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00025B9F File Offset: 0x00023D9F
		internal bool renderTargetValid
		{
			get
			{
				return this.renderTarget != XRPass.invalidRT;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00025BB1 File Offset: 0x00023DB1
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00025BB9 File Offset: 0x00023DB9
		internal bool renderTargetIsRenderTexture { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00025BC2 File Offset: 0x00023DC2
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00025BCA File Offset: 0x00023DCA
		internal bool isLateLatchEnabled { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00025BD3 File Offset: 0x00023DD3
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00025BDB File Offset: 0x00023DDB
		internal bool canMarkLateLatch { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00025BE4 File Offset: 0x00023DE4
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00025BEC File Offset: 0x00023DEC
		internal bool hasMarkedLateLatch { get; set; }

		// Token: 0x060006AC RID: 1708 RVA: 0x00025BF5 File Offset: 0x00023DF5
		internal Matrix4x4 GetProjMatrix(int viewIndex = 0)
		{
			return this.views[viewIndex].projMatrix;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00025C08 File Offset: 0x00023E08
		internal Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return this.views[viewIndex].viewMatrix;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00025C1B File Offset: 0x00023E1B
		internal int GetTextureArraySlice(int viewIndex = 0)
		{
			return this.views[viewIndex].textureArraySlice;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00025C2E File Offset: 0x00023E2E
		internal Rect GetViewport(int viewIndex = 0)
		{
			return this.views[viewIndex].viewport;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00025C41 File Offset: 0x00023E41
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00025C49 File Offset: 0x00023E49
		internal ScriptableCullingParameters cullingParams { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00025C52 File Offset: 0x00023E52
		internal int viewCount
		{
			get
			{
				return this.views.Count;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00025C5F File Offset: 0x00023E5F
		internal bool singlePassEnabled
		{
			get
			{
				return this.viewCount > 1;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00025C6A File Offset: 0x00023E6A
		internal bool isOcclusionMeshSupported
		{
			get
			{
				return this.enabled && this.xrSdkEnabled && this.occlusionMeshMaterial != null;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00025C8A File Offset: 0x00023E8A
		internal bool hasValidOcclusionMesh
		{
			get
			{
				if (!this.isOcclusionMeshSupported)
				{
					return false;
				}
				if (this.singlePassEnabled)
				{
					return this.occlusionMeshCombined != null;
				}
				return this.views[0].occlusionMesh != null;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00025CC2 File Offset: 0x00023EC2
		internal void SetCustomMirrorView(XRPass.CustomMirrorView callback)
		{
			this.customMirrorView = callback;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00025CCC File Offset: 0x00023ECC
		internal static XRPass Create(XRPassCreateInfo createInfo)
		{
			XRPass xrpass = GenericPool<XRPass>.Get();
			xrpass.multipassId = createInfo.multipassId;
			xrpass.cullingPassId = createInfo.cullingPassId;
			xrpass.cullingParams = createInfo.cullingParameters;
			xrpass.customMirrorView = createInfo.customMirrorView;
			xrpass.views.Clear();
			if (createInfo.renderTarget != null)
			{
				xrpass.renderTarget = new RenderTargetIdentifier(createInfo.renderTarget, 0, CubemapFace.Unknown, -1);
				xrpass.renderTargetDesc = createInfo.renderTarget.descriptor;
				xrpass.renderTargetIsRenderTexture = createInfo.renderTargetIsRenderTexture;
			}
			else
			{
				xrpass.renderTarget = XRPass.invalidRT;
				xrpass.renderTargetDesc = createInfo.renderTargetDesc;
				xrpass.renderTargetIsRenderTexture = createInfo.renderTargetIsRenderTexture;
			}
			xrpass.occlusionMeshMaterial = null;
			xrpass.xrSdkEnabled = false;
			xrpass.copyDepth = false;
			return xrpass;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00025D94 File Offset: 0x00023F94
		internal void UpdateView(int viewId, XRDisplaySubsystem.XRRenderPass xrSdkRenderPass, XRDisplaySubsystem.XRRenderParameter xrSdkRenderParameter)
		{
			if (viewId >= this.views.Count)
			{
				throw new NotImplementedException("Invalid XR setup to update, trying to update non-existing xr view.");
			}
			this.views[viewId] = new XRView(xrSdkRenderPass, xrSdkRenderParameter);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00025DC2 File Offset: 0x00023FC2
		internal void UpdateView(int viewId, Matrix4x4 proj, Matrix4x4 view, Rect vp, int textureArraySlice = -1)
		{
			if (viewId >= this.views.Count)
			{
				throw new NotImplementedException("Invalid XR setup to update, trying to update non-existing xr view.");
			}
			this.views[viewId] = new XRView(proj, view, vp, textureArraySlice);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00025DF4 File Offset: 0x00023FF4
		internal void UpdateCullingParams(int cullingPassId, ScriptableCullingParameters cullingParams)
		{
			this.cullingPassId = cullingPassId;
			this.cullingParams = cullingParams;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00025E04 File Offset: 0x00024004
		internal void AddView(Matrix4x4 proj, Matrix4x4 view, Rect vp, int textureArraySlice = -1)
		{
			this.AddViewInternal(new XRView(proj, view, vp, textureArraySlice));
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00025E18 File Offset: 0x00024018
		internal static XRPass Create(XRDisplaySubsystem.XRRenderPass xrRenderPass, int multipassId, ScriptableCullingParameters cullingParameters, Material occlusionMeshMaterial)
		{
			XRPass xrpass = GenericPool<XRPass>.Get();
			xrpass.multipassId = multipassId;
			xrpass.cullingPassId = xrRenderPass.cullingPassIndex;
			xrpass.cullingParams = cullingParameters;
			xrpass.views.Clear();
			xrpass.renderTarget = new RenderTargetIdentifier(xrRenderPass.renderTarget, 0, CubemapFace.Unknown, -1);
			RenderTextureDescriptor renderTargetDesc = xrRenderPass.renderTargetDesc;
			xrpass.renderTargetDesc = new RenderTextureDescriptor(renderTargetDesc.width, renderTargetDesc.height, renderTargetDesc.colorFormat, renderTargetDesc.depthBufferBits, renderTargetDesc.mipCount)
			{
				dimension = xrRenderPass.renderTargetDesc.dimension,
				volumeDepth = xrRenderPass.renderTargetDesc.volumeDepth,
				vrUsage = xrRenderPass.renderTargetDesc.vrUsage,
				sRGB = xrRenderPass.renderTargetDesc.sRGB
			};
			xrpass.renderTargetIsRenderTexture = false;
			xrpass.occlusionMeshMaterial = occlusionMeshMaterial;
			xrpass.xrSdkEnabled = true;
			xrpass.copyDepth = xrRenderPass.shouldFillOutDepth;
			xrpass.customMirrorView = null;
			return xrpass;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00025F0F File Offset: 0x0002410F
		internal void AddView(XRDisplaySubsystem.XRRenderPass xrSdkRenderPass, XRDisplaySubsystem.XRRenderParameter xrSdkRenderParameter)
		{
			this.AddViewInternal(new XRView(xrSdkRenderPass, xrSdkRenderParameter));
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00025F1E File Offset: 0x0002411E
		internal static void Release(XRPass xrPass)
		{
			GenericPool<XRPass>.Release(xrPass);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00025F28 File Offset: 0x00024128
		internal void AddViewInternal(XRView xrView)
		{
			int num = Math.Min(TextureXR.slices, 2);
			if (this.views.Count < num)
			{
				this.views.Add(xrView);
				return;
			}
			throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to add too many views! Max supported: {0}", num));
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00025F74 File Offset: 0x00024174
		internal void UpdateOcclusionMesh()
		{
			int num;
			if (this.isOcclusionMeshSupported && this.singlePassEnabled && this.TryGetOcclusionMeshCombinedHashCode(out num))
			{
				if (this.occlusionMeshCombined == null || num != this.occlusionMeshCombinedHashCode)
				{
					this.CreateOcclusionMeshCombined();
					this.occlusionMeshCombinedHashCode = num;
					return;
				}
			}
			else
			{
				this.occlusionMeshCombined = null;
				this.occlusionMeshCombinedHashCode = 0;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00025FD0 File Offset: 0x000241D0
		private bool TryGetOcclusionMeshCombinedHashCode(out int hashCode)
		{
			hashCode = 17;
			for (int i = 0; i < this.viewCount; i++)
			{
				if (!(this.views[i].occlusionMesh != null))
				{
					hashCode = 0;
					return false;
				}
				hashCode = hashCode * 23 + this.views[i].occlusionMesh.GetHashCode();
			}
			return true;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00026034 File Offset: 0x00024234
		private void CreateOcclusionMeshCombined()
		{
			this.occlusionMeshCombined = new Mesh();
			this.occlusionMeshCombined.indexFormat = IndexFormat.UInt16;
			int num = 0;
			uint num2 = 0U;
			for (int i = 0; i < this.viewCount; i++)
			{
				Mesh occlusionMesh = this.views[i].occlusionMesh;
				num += occlusionMesh.vertexCount;
				num2 += occlusionMesh.GetIndexCount(0);
			}
			Vector3[] array = new Vector3[num];
			ushort[] array2 = new ushort[num2];
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < this.viewCount; j++)
			{
				Mesh occlusionMesh2 = this.views[j].occlusionMesh;
				int[] indices = occlusionMesh2.GetIndices(0);
				occlusionMesh2.vertices.CopyTo(array, num3);
				for (int k = 0; k < occlusionMesh2.vertices.Length; k++)
				{
					array[num3 + k].z = (float)j;
				}
				for (int l = 0; l < indices.Length; l++)
				{
					int num5 = num3 + indices[l];
					array2[num4 + l] = (ushort)num5;
				}
				num3 += occlusionMesh2.vertexCount;
				num4 += indices.Length;
			}
			this.occlusionMeshCombined.vertices = array;
			this.occlusionMeshCombined.SetIndices(array2, MeshTopology.Triangles, 0, true, 0);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0002617C File Offset: 0x0002437C
		internal void StartSinglePass(CommandBuffer cmd)
		{
			if (!this.enabled || !this.singlePassEnabled)
			{
				return;
			}
			if (this.viewCount > TextureXR.slices)
			{
				throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to render too many views! Max supported: {0}", TextureXR.slices));
			}
			if (SystemInfo.supportsMultiview)
			{
				cmd.EnableShaderKeyword("STEREO_MULTIVIEW_ON");
				return;
			}
			cmd.EnableShaderKeyword("STEREO_INSTANCING_ON");
			cmd.SetInstanceMultiplier((uint)this.viewCount);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000261EB File Offset: 0x000243EB
		internal void StopSinglePass(CommandBuffer cmd)
		{
			if (this.enabled && this.singlePassEnabled)
			{
				if (SystemInfo.supportsMultiview)
				{
					cmd.DisableShaderKeyword("STEREO_MULTIVIEW_ON");
					return;
				}
				cmd.DisableShaderKeyword("STEREO_INSTANCING_ON");
				cmd.SetInstanceMultiplier(1U);
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00026224 File Offset: 0x00024424
		internal void EndCamera(CommandBuffer cmd, CameraData cameraData)
		{
			if (!this.enabled)
			{
				return;
			}
			this.StopSinglePass(cmd);
			if (this.customMirrorView != null)
			{
				using (new ProfilingScope(cmd, XRPass._XRCustomMirrorProfilingSampler))
				{
					this.customMirrorView(this, cmd, cameraData.targetTexture, cameraData.pixelRect);
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00026290 File Offset: 0x00024490
		internal void RenderOcclusionMesh(CommandBuffer cmd)
		{
			if (this.isOcclusionMeshSupported)
			{
				using (new ProfilingScope(cmd, XRPass._XROcclusionProfilingSampler))
				{
					if (this.singlePassEnabled)
					{
						if (this.occlusionMeshCombined != null && SystemInfo.supportsRenderTargetArrayIndexFromVertexShader)
						{
							this.StopSinglePass(cmd);
							cmd.EnableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
							cmd.DrawMesh(this.occlusionMeshCombined, Matrix4x4.identity, this.occlusionMeshMaterial);
							cmd.DisableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
							this.StartSinglePass(cmd);
						}
					}
					else if (this.views[0].occlusionMesh != null)
					{
						cmd.DrawMesh(this.views[0].occlusionMesh, Matrix4x4.identity, this.occlusionMeshMaterial);
					}
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0002636C File Offset: 0x0002456C
		internal void UpdateGPUViewAndProjectionMatrices(CommandBuffer cmd, ref CameraData cameraData, bool isRenderToTexture)
		{
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(cameraData.xr.GetProjMatrix(0), isRenderToTexture);
			RenderingUtils.SetViewAndProjectionMatrices(cmd, cameraData.xr.GetViewMatrix(0), gpuprojectionMatrix, true);
			if (cameraData.xr.singlePassEnabled)
			{
				for (int i = 0; i < 2; i++)
				{
					this.stereoCameraProjectionMatrix[i] = cameraData.xr.GetProjMatrix(i);
					this.stereoViewMatrix[i] = cameraData.xr.GetViewMatrix(i);
					this.stereoProjectionMatrix[i] = GL.GetGPUProjectionMatrix(this.stereoCameraProjectionMatrix[i], isRenderToTexture);
				}
				RenderingUtils.SetStereoViewAndProjectionMatrices(cmd, this.stereoViewMatrix, this.stereoProjectionMatrix, this.stereoCameraProjectionMatrix, true);
				if (cameraData.xr.canMarkLateLatch)
				{
					this.MarkLateLatchShaderProperties(cmd, ref cameraData);
				}
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00026438 File Offset: 0x00024638
		internal void MarkLateLatchShaderProperties(CommandBuffer cmd, ref CameraData cameraData)
		{
			cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.View, XRPass.UNITY_STEREO_MATRIX_V);
			cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.InverseView, XRPass.UNITY_STEREO_MATRIX_IV);
			cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.ViewProjection, XRPass.UNITY_STEREO_MATRIX_VP);
			cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.InverseViewProjection, XRPass.UNITY_STEREO_MATRIX_IVP);
			cmd.SetLateLatchProjectionMatrices(this.stereoProjectionMatrix);
			cameraData.xr.hasMarkedLateLatch = true;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002648D File Offset: 0x0002468D
		internal void UnmarkLateLatchShaderProperties(CommandBuffer cmd, ref CameraData cameraData)
		{
			cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.View);
			cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.InverseView);
			cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.ViewProjection);
			cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.InverseViewProjection);
			cameraData.xr.hasMarkedLateLatch = false;
		}

		// Token: 0x0400067C RID: 1660
		internal List<XRView> views = new List<XRView>(2);

		// Token: 0x04000683 RID: 1667
		private static RenderTargetIdentifier invalidRT = -1;

		// Token: 0x04000689 RID: 1673
		private Material occlusionMeshMaterial;

		// Token: 0x0400068A RID: 1674
		private Mesh occlusionMeshCombined;

		// Token: 0x0400068B RID: 1675
		private int occlusionMeshCombinedHashCode;

		// Token: 0x0400068C RID: 1676
		private XRPass.CustomMirrorView customMirrorView;

		// Token: 0x0400068D RID: 1677
		private const string k_XRCustomMirrorTag = "XR Custom Mirror View";

		// Token: 0x0400068E RID: 1678
		private static ProfilingSampler _XRCustomMirrorProfilingSampler = new ProfilingSampler("XR Custom Mirror View");

		// Token: 0x0400068F RID: 1679
		private const string k_XROcclusionTag = "XR Occlusion Mesh";

		// Token: 0x04000690 RID: 1680
		private static ProfilingSampler _XROcclusionProfilingSampler = new ProfilingSampler("XR Occlusion Mesh");

		// Token: 0x04000691 RID: 1681
		private Matrix4x4[] stereoProjectionMatrix = new Matrix4x4[2];

		// Token: 0x04000692 RID: 1682
		private Matrix4x4[] stereoViewMatrix = new Matrix4x4[2];

		// Token: 0x04000693 RID: 1683
		private Matrix4x4[] stereoCameraProjectionMatrix = new Matrix4x4[2];

		// Token: 0x04000694 RID: 1684
		internal static readonly int UNITY_STEREO_MATRIX_V = Shader.PropertyToID("unity_StereoMatrixV");

		// Token: 0x04000695 RID: 1685
		internal static readonly int UNITY_STEREO_MATRIX_IV = Shader.PropertyToID("unity_StereoMatrixInvV");

		// Token: 0x04000696 RID: 1686
		internal static readonly int UNITY_STEREO_MATRIX_VP = Shader.PropertyToID("unity_StereoMatrixVP");

		// Token: 0x04000697 RID: 1687
		internal static readonly int UNITY_STEREO_MATRIX_IVP = Shader.PropertyToID("unity_StereoMatrixIVP");

		// Token: 0x0200018E RID: 398
		// (Invoke) Token: 0x060009F2 RID: 2546
		internal delegate void CustomMirrorView(XRPass pass, CommandBuffer cmd, RenderTexture rt, Rect viewport);
	}
}
