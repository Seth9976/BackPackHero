using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B2 RID: 178
	[DisallowMultipleRendererFeature(null)]
	[Tooltip("The Ambient Occlusion effect darkens creases, holes, intersections and surfaces that are close to each other.")]
	internal class ScreenSpaceAmbientOcclusion : ScriptableRendererFeature
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001E88C File Offset: 0x0001CA8C
		internal bool afterOpaque
		{
			get
			{
				return this.m_Settings.AfterOpaque;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001E899 File Offset: 0x0001CA99
		public override void Create()
		{
			if (this.m_SSAOPass == null)
			{
				this.m_SSAOPass = new ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass();
			}
			this.GetMaterial();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (!this.GetMaterial())
			{
				Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added. Check for missing reference in the renderer resources.", new object[]
				{
					base.GetType().Name,
					base.name
				});
				return;
			}
			if (this.m_SSAOPass.Setup(this.m_Settings, renderer, this.m_Material))
			{
				renderer.EnqueuePass(this.m_SSAOPass);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001E91B File Offset: 0x0001CB1B
		protected override void Dispose(bool disposing)
		{
			CoreUtils.Destroy(this.m_Material);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001E928 File Offset: 0x0001CB28
		private bool GetMaterial()
		{
			if (this.m_Material != null)
			{
				return true;
			}
			if (this.m_Shader == null)
			{
				this.m_Shader = Shader.Find("Hidden/Universal Render Pipeline/ScreenSpaceAmbientOcclusion");
				if (this.m_Shader == null)
				{
					return false;
				}
			}
			this.m_Material = CoreUtils.CreateEngineMaterial(this.m_Shader);
			return this.m_Material != null;
		}

		// Token: 0x0400043B RID: 1083
		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		// Token: 0x0400043C RID: 1084
		[SerializeField]
		private ScreenSpaceAmbientOcclusionSettings m_Settings = new ScreenSpaceAmbientOcclusionSettings();

		// Token: 0x0400043D RID: 1085
		private Material m_Material;

		// Token: 0x0400043E RID: 1086
		private ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass m_SSAOPass;

		// Token: 0x0400043F RID: 1087
		private const string k_ShaderName = "Hidden/Universal Render Pipeline/ScreenSpaceAmbientOcclusion";

		// Token: 0x04000440 RID: 1088
		private const string k_OrthographicCameraKeyword = "_ORTHOGRAPHIC";

		// Token: 0x04000441 RID: 1089
		private const string k_NormalReconstructionLowKeyword = "_RECONSTRUCT_NORMAL_LOW";

		// Token: 0x04000442 RID: 1090
		private const string k_NormalReconstructionMediumKeyword = "_RECONSTRUCT_NORMAL_MEDIUM";

		// Token: 0x04000443 RID: 1091
		private const string k_NormalReconstructionHighKeyword = "_RECONSTRUCT_NORMAL_HIGH";

		// Token: 0x04000444 RID: 1092
		private const string k_SourceDepthKeyword = "_SOURCE_DEPTH";

		// Token: 0x04000445 RID: 1093
		private const string k_SourceDepthNormalsKeyword = "_SOURCE_DEPTH_NORMALS";

		// Token: 0x0200017E RID: 382
		private class ScreenSpaceAmbientOcclusionPass : ScriptableRenderPass
		{
			// Token: 0x17000231 RID: 561
			// (get) Token: 0x060009CC RID: 2508 RVA: 0x00040D83 File Offset: 0x0003EF83
			private bool isRendererDeferred
			{
				get
				{
					return this.m_Renderer != null && this.m_Renderer is UniversalRenderer && ((UniversalRenderer)this.m_Renderer).renderingMode == RenderingMode.Deferred;
				}
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x00040DB0 File Offset: 0x0003EFB0
			internal ScreenSpaceAmbientOcclusionPass()
			{
				this.m_CurrentSettings = new ScreenSpaceAmbientOcclusionSettings();
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x00040E70 File Offset: 0x0003F070
			internal bool Setup(ScreenSpaceAmbientOcclusionSettings featureSettings, ScriptableRenderer renderer, Material material)
			{
				this.m_Material = material;
				this.m_Renderer = renderer;
				this.m_CurrentSettings = featureSettings;
				ScreenSpaceAmbientOcclusionSettings.DepthSource depthSource;
				if (this.isRendererDeferred)
				{
					base.renderPassEvent = (featureSettings.AfterOpaque ? RenderPassEvent.AfterRenderingOpaques : RenderPassEvent.AfterRenderingGbuffer);
					depthSource = ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
				}
				else
				{
					base.renderPassEvent = (featureSettings.AfterOpaque ? RenderPassEvent.AfterRenderingOpaques : ((RenderPassEvent)201));
					depthSource = this.m_CurrentSettings.Source;
				}
				if (depthSource != ScreenSpaceAmbientOcclusionSettings.DepthSource.Depth)
				{
					if (depthSource != ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals)
					{
						throw new ArgumentOutOfRangeException();
					}
					base.ConfigureInput(ScriptableRenderPassInput.Normal);
				}
				else
				{
					base.ConfigureInput(ScriptableRenderPassInput.Depth);
				}
				return this.m_Material != null && this.m_CurrentSettings.Intensity > 0f && this.m_CurrentSettings.Radius > 0f && this.m_CurrentSettings.SampleCount > 0;
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x00040F44 File Offset: 0x0003F144
			public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
			{
				RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				int num = (this.m_CurrentSettings.Downsample ? 2 : 1);
				Vector4 vector = new Vector4(this.m_CurrentSettings.Intensity, this.m_CurrentSettings.Radius, 1f / (float)num, (float)this.m_CurrentSettings.SampleCount);
				this.m_Material.SetVector(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOParamsID, vector);
				int num2 = ((renderingData.cameraData.xr.enabled && renderingData.cameraData.xr.singlePassEnabled) ? 2 : 1);
				for (int i = 0; i < num2; i++)
				{
					Matrix4x4 viewMatrix = renderingData.cameraData.GetViewMatrix(i);
					Matrix4x4 projectionMatrix = renderingData.cameraData.GetProjectionMatrix(i);
					this.m_CameraViewProjections[i] = projectionMatrix * viewMatrix;
					Matrix4x4 matrix4x = viewMatrix;
					matrix4x.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
					Matrix4x4 inverse = (projectionMatrix * matrix4x).inverse;
					Vector4 vector2 = inverse.MultiplyPoint(new Vector4(-1f, 1f, -1f, 1f));
					Vector4 vector3 = inverse.MultiplyPoint(new Vector4(1f, 1f, -1f, 1f));
					Vector4 vector4 = inverse.MultiplyPoint(new Vector4(-1f, -1f, -1f, 1f));
					Vector4 vector5 = inverse.MultiplyPoint(new Vector4(0f, 0f, 1f, 1f));
					this.m_CameraTopLeftCorner[i] = vector2;
					this.m_CameraXExtent[i] = vector3 - vector2;
					this.m_CameraYExtent[i] = vector4 - vector2;
					this.m_CameraZExtent[i] = vector5;
				}
				this.m_Material.SetVector(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_ProjectionParams2ID, new Vector4(1f / renderingData.cameraData.camera.nearClipPlane, 0f, 0f, 0f));
				this.m_Material.SetMatrixArray(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_CameraViewProjectionsID, this.m_CameraViewProjections);
				this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_CameraViewTopLeftCornerID, this.m_CameraTopLeftCorner);
				this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_CameraViewXExtentID, this.m_CameraXExtent);
				this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_CameraViewYExtentID, this.m_CameraYExtent);
				this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_CameraViewZExtentID, this.m_CameraZExtent);
				CoreUtils.SetKeyword(this.m_Material, "_ORTHOGRAPHIC", renderingData.cameraData.camera.orthographic);
				ScreenSpaceAmbientOcclusionSettings.DepthSource depthSource = (this.isRendererDeferred ? ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals : this.m_CurrentSettings.Source);
				if (depthSource == ScreenSpaceAmbientOcclusionSettings.DepthSource.Depth)
				{
					switch (this.m_CurrentSettings.NormalSamples)
					{
					case ScreenSpaceAmbientOcclusionSettings.NormalQuality.Low:
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_LOW", true);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_MEDIUM", false);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_HIGH", false);
						break;
					case ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium:
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_LOW", false);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_MEDIUM", true);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_HIGH", false);
						break;
					case ScreenSpaceAmbientOcclusionSettings.NormalQuality.High:
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_LOW", false);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_MEDIUM", false);
						CoreUtils.SetKeyword(this.m_Material, "_RECONSTRUCT_NORMAL_HIGH", true);
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				if (depthSource == ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals)
				{
					CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH", false);
					CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_NORMALS", true);
				}
				else
				{
					CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH", true);
					CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_NORMALS", false);
				}
				RenderTextureDescriptor renderTextureDescriptor = cameraTargetDescriptor;
				renderTextureDescriptor.msaaSamples = 1;
				renderTextureDescriptor.depthBufferBits = 0;
				this.m_AOPassDescriptor = renderTextureDescriptor;
				this.m_AOPassDescriptor.width = this.m_AOPassDescriptor.width / num;
				this.m_AOPassDescriptor.height = this.m_AOPassDescriptor.height / num;
				this.m_AOPassDescriptor.colorFormat = RenderTextureFormat.ARGB32;
				this.m_BlurPassesDescriptor = renderTextureDescriptor;
				this.m_BlurPassesDescriptor.colorFormat = RenderTextureFormat.ARGB32;
				this.m_FinalDescriptor = renderTextureDescriptor;
				this.m_FinalDescriptor.colorFormat = (this.m_SupportsR8RenderTextureFormat ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32);
				cmd.GetTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture1ID, this.m_AOPassDescriptor, FilterMode.Bilinear);
				cmd.GetTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture2ID, this.m_BlurPassesDescriptor, FilterMode.Bilinear);
				cmd.GetTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture3ID, this.m_BlurPassesDescriptor, FilterMode.Bilinear);
				cmd.GetTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTextureFinalID, this.m_FinalDescriptor, FilterMode.Bilinear);
				base.ConfigureTarget(this.m_CurrentSettings.AfterOpaque ? this.m_Renderer.cameraColorTarget : ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture2ID);
				base.ConfigureClear(ClearFlag.None, Color.white);
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x00041454 File Offset: 0x0003F654
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				if (this.m_Material == null)
				{
					Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceAmbientOcclusion pass will not execute. Check for missing reference in the renderer resources.", new object[] { base.GetType().Name });
					return;
				}
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
				{
					if (!this.m_CurrentSettings.AfterOpaque)
					{
						CoreUtils.SetKeyword(commandBuffer, "_SCREEN_SPACE_OCCLUSION", true);
					}
					PostProcessUtils.SetSourceSize(commandBuffer, this.m_AOPassDescriptor);
					Vector4 vector = new Vector4(-1f, 1f, -1f, 1f);
					commandBuffer.SetGlobalVector(Shader.PropertyToID("_ScaleBiasRt"), vector);
					this.Render(commandBuffer, this.m_SSAOTexture1Target, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses.AO);
					this.RenderAndSetBaseMap(commandBuffer, this.m_SSAOTexture1Target, this.m_SSAOTexture2Target, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses.BlurHorizontal);
					PostProcessUtils.SetSourceSize(commandBuffer, this.m_BlurPassesDescriptor);
					this.RenderAndSetBaseMap(commandBuffer, this.m_SSAOTexture2Target, this.m_SSAOTexture3Target, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses.BlurVertical);
					this.RenderAndSetBaseMap(commandBuffer, this.m_SSAOTexture3Target, this.m_SSAOTextureFinalTarget, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses.BlurFinal);
					commandBuffer.SetGlobalTexture("_ScreenSpaceOcclusionTexture", this.m_SSAOTextureFinalTarget);
					commandBuffer.SetGlobalVector("_AmbientOcclusionParam", new Vector4(0f, 0f, 0f, this.m_CurrentSettings.DirectLightingStrength));
					if (this.m_CurrentSettings.AfterOpaque)
					{
						CameraData cameraData = renderingData.cameraData;
						float num = ((cameraData.cameraType != CameraType.Game || !(this.m_Renderer.cameraColorTarget == BuiltinRenderTextureType.CameraTarget) || !(cameraData.camera.targetTexture == null)) ? (-1f) : 1f);
						vector = ((num < 0f) ? new Vector4(num, 1f, -1f, 1f) : new Vector4(num, 0f, 1f, 1f));
						commandBuffer.SetGlobalVector(Shader.PropertyToID("_ScaleBiasRt"), vector);
						commandBuffer.SetRenderTarget(this.m_Renderer.cameraColorTarget, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_Material, 0, 4);
					}
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x00041694 File Offset: 0x0003F894
			private void Render(CommandBuffer cmd, RenderTargetIdentifier target, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses pass)
			{
				cmd.SetRenderTarget(target, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, target, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_Material, 0, (int)pass);
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x000416BA File Offset: 0x0003F8BA
			private void RenderAndSetBaseMap(CommandBuffer cmd, RenderTargetIdentifier baseMap, RenderTargetIdentifier target, ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.ShaderPasses pass)
			{
				cmd.SetGlobalTexture(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_BaseMapID, baseMap);
				this.Render(cmd, target, pass);
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x000416D4 File Offset: 0x0003F8D4
			public override void OnCameraCleanup(CommandBuffer cmd)
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				if (!this.m_CurrentSettings.AfterOpaque)
				{
					CoreUtils.SetKeyword(cmd, "_SCREEN_SPACE_OCCLUSION", false);
				}
				cmd.ReleaseTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture1ID);
				cmd.ReleaseTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture2ID);
				cmd.ReleaseTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture3ID);
				cmd.ReleaseTemporaryRT(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTextureFinalID);
			}

			// Token: 0x040009B4 RID: 2484
			private bool m_SupportsR8RenderTextureFormat = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8);

			// Token: 0x040009B5 RID: 2485
			private Material m_Material;

			// Token: 0x040009B6 RID: 2486
			private Vector4[] m_CameraTopLeftCorner = new Vector4[2];

			// Token: 0x040009B7 RID: 2487
			private Vector4[] m_CameraXExtent = new Vector4[2];

			// Token: 0x040009B8 RID: 2488
			private Vector4[] m_CameraYExtent = new Vector4[2];

			// Token: 0x040009B9 RID: 2489
			private Vector4[] m_CameraZExtent = new Vector4[2];

			// Token: 0x040009BA RID: 2490
			private Matrix4x4[] m_CameraViewProjections = new Matrix4x4[2];

			// Token: 0x040009BB RID: 2491
			private ProfilingSampler m_ProfilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.SSAO);

			// Token: 0x040009BC RID: 2492
			private ScriptableRenderer m_Renderer;

			// Token: 0x040009BD RID: 2493
			private RenderTargetIdentifier m_SSAOTexture1Target = new RenderTargetIdentifier(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture1ID, 0, CubemapFace.Unknown, -1);

			// Token: 0x040009BE RID: 2494
			private RenderTargetIdentifier m_SSAOTexture2Target = new RenderTargetIdentifier(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture2ID, 0, CubemapFace.Unknown, -1);

			// Token: 0x040009BF RID: 2495
			private RenderTargetIdentifier m_SSAOTexture3Target = new RenderTargetIdentifier(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTexture3ID, 0, CubemapFace.Unknown, -1);

			// Token: 0x040009C0 RID: 2496
			private RenderTargetIdentifier m_SSAOTextureFinalTarget = new RenderTargetIdentifier(ScreenSpaceAmbientOcclusion.ScreenSpaceAmbientOcclusionPass.s_SSAOTextureFinalID, 0, CubemapFace.Unknown, -1);

			// Token: 0x040009C1 RID: 2497
			private RenderTextureDescriptor m_AOPassDescriptor;

			// Token: 0x040009C2 RID: 2498
			private RenderTextureDescriptor m_BlurPassesDescriptor;

			// Token: 0x040009C3 RID: 2499
			private RenderTextureDescriptor m_FinalDescriptor;

			// Token: 0x040009C4 RID: 2500
			private ScreenSpaceAmbientOcclusionSettings m_CurrentSettings;

			// Token: 0x040009C5 RID: 2501
			private const string k_SSAOTextureName = "_ScreenSpaceOcclusionTexture";

			// Token: 0x040009C6 RID: 2502
			private const string k_SSAOAmbientOcclusionParamName = "_AmbientOcclusionParam";

			// Token: 0x040009C7 RID: 2503
			private static readonly int s_BaseMapID = Shader.PropertyToID("_BaseMap");

			// Token: 0x040009C8 RID: 2504
			private static readonly int s_SSAOParamsID = Shader.PropertyToID("_SSAOParams");

			// Token: 0x040009C9 RID: 2505
			private static readonly int s_SSAOTexture1ID = Shader.PropertyToID("_SSAO_OcclusionTexture1");

			// Token: 0x040009CA RID: 2506
			private static readonly int s_SSAOTexture2ID = Shader.PropertyToID("_SSAO_OcclusionTexture2");

			// Token: 0x040009CB RID: 2507
			private static readonly int s_SSAOTexture3ID = Shader.PropertyToID("_SSAO_OcclusionTexture3");

			// Token: 0x040009CC RID: 2508
			private static readonly int s_SSAOTextureFinalID = Shader.PropertyToID("_SSAO_OcclusionTexture");

			// Token: 0x040009CD RID: 2509
			private static readonly int s_CameraViewXExtentID = Shader.PropertyToID("_CameraViewXExtent");

			// Token: 0x040009CE RID: 2510
			private static readonly int s_CameraViewYExtentID = Shader.PropertyToID("_CameraViewYExtent");

			// Token: 0x040009CF RID: 2511
			private static readonly int s_CameraViewZExtentID = Shader.PropertyToID("_CameraViewZExtent");

			// Token: 0x040009D0 RID: 2512
			private static readonly int s_ProjectionParams2ID = Shader.PropertyToID("_ProjectionParams2");

			// Token: 0x040009D1 RID: 2513
			private static readonly int s_CameraViewProjectionsID = Shader.PropertyToID("_CameraViewProjections");

			// Token: 0x040009D2 RID: 2514
			private static readonly int s_CameraViewTopLeftCornerID = Shader.PropertyToID("_CameraViewTopLeftCorner");

			// Token: 0x020001E2 RID: 482
			private enum ShaderPasses
			{
				// Token: 0x04000B79 RID: 2937
				AO,
				// Token: 0x04000B7A RID: 2938
				BlurHorizontal,
				// Token: 0x04000B7B RID: 2939
				BlurVertical,
				// Token: 0x04000B7C RID: 2940
				BlurFinal,
				// Token: 0x04000B7D RID: 2941
				AfterOpaque
			}
		}
	}
}
