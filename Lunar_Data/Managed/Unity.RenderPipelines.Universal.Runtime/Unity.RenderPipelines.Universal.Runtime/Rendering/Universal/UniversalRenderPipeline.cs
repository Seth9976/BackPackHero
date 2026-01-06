using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DA RID: 218
	public sealed class UniversalRenderPipeline : RenderPipeline
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00022EEF File Offset: 0x000210EF
		public static float maxShadowBias
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00022EF6 File Offset: 0x000210F6
		public static float minRenderScale
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00022EFD File Offset: 0x000210FD
		public static float maxRenderScale
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00022F04 File Offset: 0x00021104
		public static int maxPerObjectLights
		{
			get
			{
				if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2)
				{
					return 8;
				}
				return 4;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00022F14 File Offset: 0x00021114
		public static int maxVisibleAdditionalLights
		{
			get
			{
				bool flag = GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.SHADER_API_MOBILE);
				if (flag && (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 || (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 && Graphics.minOpenGLESVersion <= OpenGLESVersion.OpenGLES30)))
				{
					return 16;
				}
				if (!flag && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3)
				{
					return 256;
				}
				return 32;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00022F6D File Offset: 0x0002116D
		internal static int lightsPerTile
		{
			get
			{
				return (UniversalRenderPipeline.maxVisibleAdditionalLights + 31) / 32 * 32;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00022F7D File Offset: 0x0002117D
		internal static int maxZBins
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00022F84 File Offset: 0x00021184
		internal static int maxTileVec4s
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00022F8B File Offset: 0x0002118B
		public override RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return this.m_GlobalSettings;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00022F93 File Offset: 0x00021193
		public override string ToString()
		{
			UniversalRenderPipelineAsset universalRenderPipelineAsset = this.pipelineAsset;
			if (universalRenderPipelineAsset == null)
			{
				return null;
			}
			return universalRenderPipelineAsset.ToString();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00022FA8 File Offset: 0x000211A8
		public UniversalRenderPipeline(UniversalRenderPipelineAsset asset)
		{
			this.pipelineAsset = asset;
			this.m_GlobalSettings = UniversalRenderPipelineGlobalSettings.instance;
			UniversalRenderPipeline.SetSupportedRenderingFeatures();
			if (((QualitySettings.antiAliasing > 0) ? QualitySettings.antiAliasing : 1) != asset.msaaSampleCount)
			{
				QualitySettings.antiAliasing = asset.msaaSampleCount;
				XRSystem.UpdateMSAALevel(asset.msaaSampleCount);
			}
			XRSystem.UpdateRenderScale(asset.renderScale);
			Shader.globalRenderPipeline = "UniversalPipeline";
			Lightmapping.SetDelegate(UniversalRenderPipeline.lightsDelegate);
			CameraCaptureBridge.enabled = true;
			RenderingUtils.ClearSystemInfoCache();
			DecalProjector.defaultMaterial = asset.decalMaterial;
			DebugManager.instance.RefreshEditor();
			this.m_DebugDisplaySettingsUI.RegisterDebug(DebugDisplaySettings.Instance);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00023084 File Offset: 0x00021284
		protected override void Dispose(bool disposing)
		{
			this.m_DebugDisplaySettingsUI.UnregisterDebug();
			base.Dispose(disposing);
			this.pipelineAsset.DestroyRenderers();
			Shader.globalRenderPipeline = "";
			SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			ShaderData.instance.Dispose();
			DeferredShaderData.instance.Dispose();
			XRSystem xrsystem = UniversalRenderPipeline.m_XRSystem;
			if (xrsystem != null)
			{
				xrsystem.Dispose();
			}
			Lightmapping.ResetDelegate();
			CameraCaptureBridge.enabled = false;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000230F1 File Offset: 0x000212F1
		protected override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
		{
			this.Render(renderContext, new List<Camera>(cameras));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00023100 File Offset: 0x00021300
		protected override void Render(ScriptableRenderContext renderContext, List<Camera> cameras)
		{
			using (new ProfilingScope(null, ProfilingSampler.Get<URPProfileId>(URPProfileId.UniversalRenderTotal)))
			{
				using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.beginContextRendering))
				{
					RenderPipeline.BeginContextRendering(renderContext, cameras);
				}
				GraphicsSettings.lightsUseLinearIntensity = QualitySettings.activeColorSpace == ColorSpace.Linear;
				GraphicsSettings.lightsUseColorTemperature = true;
				GraphicsSettings.useScriptableRenderPipelineBatching = UniversalRenderPipeline.asset.useSRPBatcher;
				GraphicsSettings.defaultRenderingLayerMask = 1U;
				UniversalRenderPipeline.SetupPerFrameShaderConstants();
				XRSystem.UpdateMSAALevel(UniversalRenderPipeline.asset.msaaSampleCount);
				this.SortCameras(cameras);
				for (int i = 0; i < cameras.Count; i++)
				{
					Camera camera = cameras[i];
					if (UniversalRenderPipeline.IsGameCamera(camera))
					{
						UniversalRenderPipeline.RenderCameraStack(renderContext, camera);
					}
					else
					{
						using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.beginCameraRendering))
						{
							RenderPipeline.BeginCameraRendering(renderContext, camera);
						}
						UniversalRenderPipeline.UpdateVolumeFramework(camera, null);
						UniversalRenderPipeline.RenderSingleCamera(renderContext, camera);
						using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.endCameraRendering))
						{
							RenderPipeline.EndCameraRendering(renderContext, camera);
						}
					}
				}
				using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.endContextRendering))
				{
					RenderPipeline.EndContextRendering(renderContext, cameras);
				}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000232AC File Offset: 0x000214AC
		public static void RenderSingleCamera(ScriptableRenderContext context, Camera camera)
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = null;
			if (UniversalRenderPipeline.IsGameCamera(camera))
			{
				camera.gameObject.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData);
			}
			if (universalAdditionalCameraData != null && universalAdditionalCameraData.renderType != CameraRenderType.Base)
			{
				Debug.LogWarning("Only Base cameras can be rendered with standalone RenderSingleCamera. Camera will be skipped.");
				return;
			}
			CameraData cameraData;
			UniversalRenderPipeline.InitializeCameraData(camera, universalAdditionalCameraData, true, out cameraData);
			UniversalRenderPipeline.RenderSingleCamera(context, cameraData, cameraData.postProcessEnabled);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00023304 File Offset: 0x00021504
		private static bool TryGetCullingParameters(CameraData cameraData, out ScriptableCullingParameters cullingParams)
		{
			if (cameraData.xr.enabled)
			{
				cullingParams = cameraData.xr.cullingParams;
				if (!cameraData.camera.usePhysicalProperties && !XRGraphicsAutomatedTests.enabled)
				{
					cameraData.camera.fieldOfView = 57.29578f * Mathf.Atan(1f / cullingParams.stereoProjectionMatrix.m11) * 2f;
				}
				return true;
			}
			return cameraData.camera.TryGetCullingParameters(false, out cullingParams);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00023380 File Offset: 0x00021580
		private static void RenderSingleCamera(ScriptableRenderContext context, CameraData cameraData, bool anyPostProcessingEnabled)
		{
			Camera camera = cameraData.camera;
			ScriptableRenderer renderer = cameraData.renderer;
			if (renderer == null)
			{
				Debug.LogWarning(string.Format("Trying to render {0} with an invalid renderer. Camera rendering will be skipped.", camera.name));
				return;
			}
			ScriptableCullingParameters scriptableCullingParameters;
			if (!UniversalRenderPipeline.TryGetCullingParameters(cameraData, out scriptableCullingParameters))
			{
				return;
			}
			ScriptableRenderer.current = renderer;
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			CommandBuffer commandBuffer2 = (cameraData.xr.enabled ? null : commandBuffer);
			ProfilingSampler profilingSampler = UniversalRenderPipeline.Profiling.TryGetOrAddCameraSampler(camera);
			using (new ProfilingScope(commandBuffer2, profilingSampler))
			{
				renderer.Clear(cameraData.renderType);
				using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.Renderer.setupCullingParameters))
				{
					renderer.OnPreCullRenderPasses(in cameraData);
					renderer.SetupCullingParameters(ref scriptableCullingParameters, ref cameraData);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				CullingResults cullingResults = context.Cull(ref scriptableCullingParameters);
				RenderingData renderingData;
				UniversalRenderPipeline.InitializeRenderingData(UniversalRenderPipeline.asset, ref cameraData, ref cullingResults, anyPostProcessingEnabled, out renderingData);
				using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.Renderer.setup))
				{
					renderer.Setup(context, ref renderingData);
				}
				renderer.Execute(context, ref renderingData);
				UniversalRenderPipeline.CleanupLightData(ref renderingData.lightData);
			}
			cameraData.xr.EndCamera(commandBuffer, cameraData);
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.Context.submit))
			{
				if (renderer.useRenderPassEnabled && !context.SubmitForRenderPassValidation())
				{
					renderer.useRenderPassEnabled = false;
					CoreUtils.SetKeyword(commandBuffer, "_RENDER_PASS_ENABLED", false);
					Debug.LogWarning("Rendering command not supported inside a native RenderPass found. Falling back to non-RenderPass rendering path");
				}
				context.Submit();
			}
			ScriptableRenderer.current = null;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00023550 File Offset: 0x00021750
		private static void RenderCameraStack(ScriptableRenderContext context, Camera baseCamera)
		{
			using (new ProfilingScope(null, ProfilingSampler.Get<URPProfileId>(URPProfileId.RenderCameraStack)))
			{
				UniversalAdditionalCameraData universalAdditionalCameraData;
				baseCamera.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData);
				if (!(universalAdditionalCameraData != null) || universalAdditionalCameraData.renderType != CameraRenderType.Overlay)
				{
					ScriptableRenderer scriptableRenderer = ((universalAdditionalCameraData != null) ? universalAdditionalCameraData.scriptableRenderer : null);
					List<Camera> list = ((scriptableRenderer != null && scriptableRenderer.SupportsCameraStackingType(CameraRenderType.Base)) ? ((universalAdditionalCameraData != null) ? universalAdditionalCameraData.cameraStack : null) : null);
					bool flag = universalAdditionalCameraData != null && universalAdditionalCameraData.renderPostProcessing;
					int num = -1;
					if (list != null)
					{
						Type type = ((universalAdditionalCameraData != null) ? universalAdditionalCameraData.scriptableRenderer.GetType() : null);
						bool flag2 = false;
						for (int i = 0; i < list.Count; i++)
						{
							Camera camera = list[i];
							if (camera == null)
							{
								flag2 = true;
							}
							else if (camera.isActiveAndEnabled)
							{
								UniversalAdditionalCameraData universalAdditionalCameraData2;
								camera.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData2);
								Type type2 = ((universalAdditionalCameraData2 != null) ? universalAdditionalCameraData2.scriptableRenderer.GetType() : null);
								if (type2 != type)
								{
									Debug.LogWarning(string.Concat(new string[] { "Only cameras with compatible renderer types can be stacked. The camera: ", camera.name, " are using the renderer ", type2.Name, ", but the base camera: ", baseCamera.name, " are using ", type.Name, ". Will skip rendering" }));
								}
								else if ((universalAdditionalCameraData2.scriptableRenderer.SupportedCameraStackingTypes() & 2) == 0)
								{
									Debug.LogWarning(string.Concat(new string[]
									{
										"The camera: ",
										camera.name,
										" is using a renderer of type ",
										scriptableRenderer.GetType().Name,
										" which does not support Overlay cameras in it's current state."
									}));
								}
								else if (universalAdditionalCameraData2 == null || universalAdditionalCameraData2.renderType != CameraRenderType.Overlay)
								{
									Debug.LogWarning("Stack can only contain Overlay cameras. The camera: " + camera.name + " " + string.Format("has a type {0} that is not supported. Will skip rendering.", universalAdditionalCameraData2.renderType));
								}
								else
								{
									flag |= universalAdditionalCameraData2.renderPostProcessing;
									num = i;
								}
							}
						}
						if (flag2)
						{
							universalAdditionalCameraData.UpdateCameraStack();
						}
					}
					flag &= SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
					bool flag3 = num != -1;
					bool flag4 = false;
					bool flag5 = true;
					if (universalAdditionalCameraData != null)
					{
						flag5 = universalAdditionalCameraData.allowXRRendering;
					}
					foreach (XRPass xrpass in UniversalRenderPipeline.m_XRSystem.SetupFrame(baseCamera, flag5))
					{
						if (xrpass.enabled)
						{
							flag4 = true;
							UniversalRenderPipeline.UpdateCameraStereoMatrices(baseCamera, xrpass);
						}
						using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.beginCameraRendering))
						{
							RenderPipeline.BeginCameraRendering(context, baseCamera);
						}
						UniversalRenderPipeline.UpdateVolumeFramework(baseCamera, universalAdditionalCameraData);
						CameraData cameraData;
						UniversalRenderPipeline.InitializeCameraData(baseCamera, universalAdditionalCameraData, !flag3, out cameraData);
						RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
						if (xrpass.enabled)
						{
							cameraData.xr = xrpass;
							cameraData.isStereoEnabled = xrpass.enabled;
							UniversalRenderPipeline.m_XRSystem.UpdateCameraData(ref cameraData, in cameraData.xr);
							UniversalRenderPipeline.m_XRSystem.UpdateFromCamera(ref cameraData.xr, cameraData);
							UniversalRenderPipeline.m_XRSystem.BeginLateLatching(baseCamera, xrpass);
						}
						UniversalRenderPipeline.RenderSingleCamera(context, cameraData, flag);
						using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.endCameraRendering))
						{
							RenderPipeline.EndCameraRendering(context, baseCamera);
						}
						UniversalRenderPipeline.m_XRSystem.EndLateLatching(baseCamera, xrpass);
						if (flag3)
						{
							for (int j = 0; j < list.Count; j++)
							{
								Camera camera2 = list[j];
								if (camera2.isActiveAndEnabled)
								{
									UniversalAdditionalCameraData universalAdditionalCameraData3;
									camera2.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData3);
									if (universalAdditionalCameraData3 != null)
									{
										CameraData cameraData2 = cameraData;
										bool flag6 = j == num;
										UniversalRenderPipeline.UpdateCameraStereoMatrices(universalAdditionalCameraData3.camera, xrpass);
										using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.beginCameraRendering))
										{
											RenderPipeline.BeginCameraRendering(context, camera2);
										}
										UniversalRenderPipeline.UpdateVolumeFramework(camera2, universalAdditionalCameraData3);
										UniversalRenderPipeline.InitializeAdditionalCameraData(camera2, universalAdditionalCameraData3, flag6, ref cameraData2);
										if (cameraData.xr.enabled)
										{
											UniversalRenderPipeline.m_XRSystem.UpdateFromCamera(ref cameraData2.xr, cameraData2);
										}
										UniversalRenderPipeline.RenderSingleCamera(context, cameraData2, flag);
										using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.endCameraRendering))
										{
											RenderPipeline.EndCameraRendering(context, camera2);
										}
									}
								}
							}
						}
						if (cameraData.xr.enabled)
						{
							cameraData.cameraTargetDescriptor = cameraTargetDescriptor;
						}
					}
					if (flag4)
					{
						CommandBuffer commandBuffer = CommandBufferPool.Get();
						using (new ProfilingScope(commandBuffer, UniversalRenderPipeline.Profiling.Pipeline.XR.mirrorView))
						{
							UniversalRenderPipeline.m_XRSystem.RenderMirrorView(commandBuffer, baseCamera);
						}
						context.ExecuteCommandBuffer(commandBuffer);
						context.Submit();
						CommandBufferPool.Release(commandBuffer);
					}
					UniversalRenderPipeline.m_XRSystem.ReleaseFrame();
				}
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00023AE0 File Offset: 0x00021CE0
		private static void UpdateVolumeFramework(Camera camera, UniversalAdditionalCameraData additionalCameraData)
		{
			using (new ProfilingScope(null, ProfilingSampler.Get<URPProfileId>(URPProfileId.UpdateVolumeFramework)))
			{
				if (!((camera.cameraType == CameraType.SceneView) | (additionalCameraData != null && additionalCameraData.requiresVolumeFrameworkUpdate)) && additionalCameraData)
				{
					if (additionalCameraData.volumeStack == null)
					{
						camera.UpdateVolumeStack(additionalCameraData);
					}
					VolumeManager.instance.stack = additionalCameraData.volumeStack;
				}
				else
				{
					if (additionalCameraData && additionalCameraData.volumeStack != null)
					{
						camera.DestroyVolumeStack(additionalCameraData);
					}
					LayerMask layerMask;
					Transform transform;
					camera.GetVolumeLayerMaskAndTrigger(additionalCameraData, out layerMask, out transform);
					VolumeManager.instance.ResetMainStack();
					VolumeManager.instance.Update(transform, layerMask);
				}
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00023B9C File Offset: 0x00021D9C
		private static bool CheckPostProcessForDepth(in CameraData cameraData)
		{
			if (!cameraData.postProcessEnabled)
			{
				return false;
			}
			if (cameraData.antialiasing == AntialiasingMode.SubpixelMorphologicalAntiAliasing)
			{
				return true;
			}
			VolumeStack stack = VolumeManager.instance.stack;
			return stack.GetComponent<DepthOfField>().IsActive() || stack.GetComponent<MotionBlur>().IsActive();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00023BE8 File Offset: 0x00021DE8
		private static void SetSupportedRenderingFeatures()
		{
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00023BEC File Offset: 0x00021DEC
		private static void InitializeCameraData(Camera camera, UniversalAdditionalCameraData additionalCameraData, bool resolveFinalTarget, out CameraData cameraData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeCameraData))
			{
				cameraData = default(CameraData);
				UniversalRenderPipeline.InitializeStackedCameraData(camera, additionalCameraData, ref cameraData);
				UniversalRenderPipeline.InitializeAdditionalCameraData(camera, additionalCameraData, resolveFinalTarget, ref cameraData);
				ScriptableRenderer scriptableRenderer = ((additionalCameraData != null) ? additionalCameraData.scriptableRenderer : null);
				bool flag = scriptableRenderer != null && scriptableRenderer.supportedRenderingFeatures.msaa;
				int num = 1;
				if (camera.allowMSAA && UniversalRenderPipeline.asset.msaaSampleCount > 1 && flag)
				{
					num = ((camera.targetTexture != null) ? camera.targetTexture.antiAliasing : UniversalRenderPipeline.asset.msaaSampleCount);
				}
				if (cameraData.xrRendering && flag)
				{
					num = XRSystem.GetMSAALevel();
				}
				bool preserveFramebufferAlpha = Graphics.preserveFramebufferAlpha;
				cameraData.cameraTargetDescriptor = UniversalRenderPipeline.CreateRenderTextureDescriptor(camera, cameraData.renderScale, cameraData.isHdrEnabled, num, preserveFramebufferAlpha, cameraData.requiresOpaqueTexture);
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00023CDC File Offset: 0x00021EDC
		private static void InitializeStackedCameraData(Camera baseCamera, UniversalAdditionalCameraData baseAdditionalCameraData, ref CameraData cameraData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeStackedCameraData))
			{
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				cameraData.targetTexture = baseCamera.targetTexture;
				cameraData.cameraType = baseCamera.cameraType;
				if (cameraData.isSceneViewCamera)
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = AntialiasingMode.None;
					cameraData.antialiasingQuality = AntialiasingQuality.High;
					cameraData.xrRendering = false;
				}
				else if (baseAdditionalCameraData != null)
				{
					cameraData.volumeLayerMask = baseAdditionalCameraData.volumeLayerMask;
					cameraData.volumeTrigger = ((baseAdditionalCameraData.volumeTrigger == null) ? baseCamera.transform : baseAdditionalCameraData.volumeTrigger);
					cameraData.isStopNaNEnabled = baseAdditionalCameraData.stopNaN && SystemInfo.graphicsShaderLevel >= 35;
					cameraData.isDitheringEnabled = baseAdditionalCameraData.dithering;
					cameraData.antialiasing = baseAdditionalCameraData.antialiasing;
					cameraData.antialiasingQuality = baseAdditionalCameraData.antialiasingQuality;
					cameraData.xrRendering = baseAdditionalCameraData.allowXRRendering && UniversalRenderPipeline.m_XRSystem.RefreshXrSdk();
				}
				else
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = AntialiasingMode.None;
					cameraData.antialiasingQuality = AntialiasingQuality.High;
					cameraData.xrRendering = UniversalRenderPipeline.m_XRSystem.RefreshXrSdk();
				}
				cameraData.isHdrEnabled = baseCamera.allowHDR && asset.supportsHDR;
				Rect rect = baseCamera.rect;
				cameraData.pixelRect = baseCamera.pixelRect;
				cameraData.pixelWidth = baseCamera.pixelWidth;
				cameraData.pixelHeight = baseCamera.pixelHeight;
				cameraData.aspectRatio = (float)cameraData.pixelWidth / (float)cameraData.pixelHeight;
				cameraData.isDefaultViewport = Math.Abs(rect.x) <= 0f && Math.Abs(rect.y) <= 0f && Math.Abs(rect.width) >= 1f && Math.Abs(rect.height) >= 1f;
				cameraData.renderScale = ((Mathf.Abs(1f - asset.renderScale) < 0.05f) ? 1f : asset.renderScale);
				cameraData.upscalingFilter = UniversalRenderPipeline.ResolveUpscalingFilterSelection(new Vector2((float)cameraData.pixelWidth, (float)cameraData.pixelHeight), cameraData.renderScale, asset.upscalingFilter);
				if (cameraData.renderScale > 1f)
				{
					cameraData.imageScalingMode = ImageScalingMode.Downscaling;
				}
				else if (cameraData.renderScale < 1f || cameraData.upscalingFilter == ImageUpscalingFilter.FSR)
				{
					cameraData.imageScalingMode = ImageScalingMode.Upscaling;
				}
				else
				{
					cameraData.imageScalingMode = ImageScalingMode.None;
				}
				cameraData.fsrOverrideSharpness = asset.fsrOverrideSharpness;
				cameraData.fsrSharpness = asset.fsrSharpness;
				cameraData.xr = UniversalRenderPipeline.m_XRSystem.emptyPass;
				XRSystem.UpdateRenderScale(cameraData.renderScale);
				SortingCriteria sortingCriteria = SortingCriteria.CommonOpaque;
				SortingCriteria sortingCriteria2 = SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.OptimizeStateChanges | SortingCriteria.CanvasOrder;
				bool hasHiddenSurfaceRemovalOnGPU = SystemInfo.hasHiddenSurfaceRemovalOnGPU;
				cameraData.defaultOpaqueSortFlags = (((baseCamera.opaqueSortMode == OpaqueSortMode.Default && hasHiddenSurfaceRemovalOnGPU) || baseCamera.opaqueSortMode == OpaqueSortMode.NoDistanceSort) ? sortingCriteria2 : sortingCriteria);
				cameraData.captureActions = CameraCaptureBridge.GetCaptureActions(baseCamera);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00024014 File Offset: 0x00022214
		private static void InitializeAdditionalCameraData(Camera camera, UniversalAdditionalCameraData additionalCameraData, bool resolveFinalTarget, ref CameraData cameraData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeAdditionalCameraData))
			{
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				cameraData.camera = camera;
				bool flag = asset.supportsMainLightShadows || asset.supportsAdditionalLightShadows;
				cameraData.maxShadowDistance = Mathf.Min(asset.shadowDistance, camera.farClipPlane);
				cameraData.maxShadowDistance = ((flag && cameraData.maxShadowDistance >= camera.nearClipPlane) ? cameraData.maxShadowDistance : 0f);
				bool isSceneViewCamera = cameraData.isSceneViewCamera;
				if (isSceneViewCamera)
				{
					cameraData.renderType = CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = CoreUtils.ArePostProcessesEnabled(camera);
					cameraData.requiresDepthTexture = asset.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = asset.supportsCameraOpaqueTexture;
					cameraData.renderer = UniversalRenderPipeline.asset.scriptableRenderer;
				}
				else if (additionalCameraData != null)
				{
					cameraData.renderType = additionalCameraData.renderType;
					cameraData.clearDepth = additionalCameraData.renderType == CameraRenderType.Base || additionalCameraData.clearDepth;
					cameraData.postProcessEnabled = additionalCameraData.renderPostProcessing;
					cameraData.maxShadowDistance = (additionalCameraData.renderShadows ? cameraData.maxShadowDistance : 0f);
					cameraData.requiresDepthTexture = additionalCameraData.requiresDepthTexture;
					cameraData.requiresOpaqueTexture = additionalCameraData.requiresColorTexture;
					cameraData.renderer = additionalCameraData.scriptableRenderer;
				}
				else
				{
					cameraData.renderType = CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = false;
					cameraData.requiresDepthTexture = asset.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = asset.supportsCameraOpaqueTexture;
					cameraData.renderer = UniversalRenderPipeline.asset.scriptableRenderer;
				}
				cameraData.postProcessEnabled &= SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
				cameraData.requiresDepthTexture = cameraData.requiresDepthTexture || isSceneViewCamera;
				cameraData.postProcessingRequiresDepthTexture |= UniversalRenderPipeline.CheckPostProcessForDepth(in cameraData);
				cameraData.resolveFinalTarget = resolveFinalTarget;
				bool flag2 = cameraData.renderType == CameraRenderType.Overlay;
				if (flag2)
				{
					cameraData.requiresOpaqueTexture = false;
					cameraData.postProcessingRequiresDepthTexture = false;
				}
				Matrix4x4 projectionMatrix = camera.projectionMatrix;
				if (flag2 && !camera.orthographic && cameraData.pixelRect != camera.pixelRect)
				{
					float num = camera.projectionMatrix.m00 * camera.aspect / cameraData.aspectRatio;
					projectionMatrix.m00 = num;
				}
				cameraData.SetViewAndProjectionMatrix(camera.worldToCameraMatrix, projectionMatrix);
				cameraData.worldSpaceCameraPos = camera.transform.position;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00024274 File Offset: 0x00022474
		private static void InitializeRenderingData(UniversalRenderPipelineAsset settings, ref CameraData cameraData, ref CullingResults cullResults, bool anyPostProcessingEnabled, out RenderingData renderingData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeRenderingData))
			{
				NativeArray<VisibleLight> visibleLights = cullResults.visibleLights;
				int mainLightIndex = UniversalRenderPipeline.GetMainLightIndex(settings, visibleLights);
				bool flag = false;
				bool flag2 = false;
				if (cameraData.maxShadowDistance > 0f)
				{
					flag = mainLightIndex != -1 && visibleLights[mainLightIndex].light != null && visibleLights[mainLightIndex].light.shadows > LightShadows.None;
					if (settings.additionalLightsRenderingMode == LightRenderingMode.PerPixel)
					{
						for (int i = 0; i < visibleLights.Length; i++)
						{
							if (i != mainLightIndex)
							{
								Light light = visibleLights[i].light;
								if ((visibleLights[i].lightType == LightType.Spot || visibleLights[i].lightType == LightType.Point) && light != null && light.shadows != LightShadows.None)
								{
									flag2 = true;
									break;
								}
							}
						}
					}
				}
				renderingData.cullResults = cullResults;
				renderingData.cameraData = cameraData;
				UniversalRenderPipeline.InitializeLightData(settings, visibleLights, mainLightIndex, out renderingData.lightData);
				UniversalRenderPipeline.InitializeShadowData(settings, visibleLights, flag, flag2 && !renderingData.lightData.shadeAdditionalLightsPerVertex, out renderingData.shadowData);
				UniversalRenderPipeline.InitializePostProcessingData(settings, out renderingData.postProcessingData);
				renderingData.supportsDynamicBatching = settings.supportsDynamicBatching;
				renderingData.perObjectData = UniversalRenderPipeline.GetPerObjectLightFlags(renderingData.lightData.additionalLightsCount);
				renderingData.postProcessingEnabled = anyPostProcessingEnabled;
				UniversalRenderPipeline.CheckAndApplyDebugSettings(ref renderingData);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00024420 File Offset: 0x00022620
		private static void InitializeShadowData(UniversalRenderPipelineAsset settings, NativeArray<VisibleLight> visibleLights, bool mainLightCastShadows, bool additionalLightsCastShadows, out ShadowData shadowData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeShadowData))
			{
				UniversalRenderPipeline.m_ShadowBiasData.Clear();
				UniversalRenderPipeline.m_ShadowResolutionData.Clear();
				for (int i = 0; i < visibleLights.Length; i++)
				{
					Light light = visibleLights[i].light;
					UniversalAdditionalLightData universalAdditionalLightData = null;
					if (light != null)
					{
						light.gameObject.TryGetComponent<UniversalAdditionalLightData>(out universalAdditionalLightData);
					}
					if (universalAdditionalLightData && !universalAdditionalLightData.usePipelineSettings)
					{
						UniversalRenderPipeline.m_ShadowBiasData.Add(new Vector4(light.shadowBias, light.shadowNormalBias, 0f, 0f));
					}
					else
					{
						UniversalRenderPipeline.m_ShadowBiasData.Add(new Vector4(settings.shadowDepthBias, settings.shadowNormalBias, 0f, 0f));
					}
					if (universalAdditionalLightData && universalAdditionalLightData.additionalLightsShadowResolutionTier == UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
					{
						UniversalRenderPipeline.m_ShadowResolutionData.Add((int)light.shadowResolution);
					}
					else if (universalAdditionalLightData && universalAdditionalLightData.additionalLightsShadowResolutionTier != UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
					{
						int num = Mathf.Clamp(universalAdditionalLightData.additionalLightsShadowResolutionTier, UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow, UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh);
						UniversalRenderPipeline.m_ShadowResolutionData.Add(settings.GetAdditionalLightsShadowResolution(num));
					}
					else
					{
						UniversalRenderPipeline.m_ShadowResolutionData.Add(settings.GetAdditionalLightsShadowResolution(UniversalAdditionalLightData.AdditionalLightsShadowDefaultResolutionTier));
					}
				}
				shadowData.bias = UniversalRenderPipeline.m_ShadowBiasData;
				shadowData.resolution = UniversalRenderPipeline.m_ShadowResolutionData;
				shadowData.supportsMainLightShadows = SystemInfo.supportsShadows && settings.supportsMainLightShadows && mainLightCastShadows;
				shadowData.requiresScreenSpaceShadowResolve = false;
				shadowData.mainLightShadowCascadesCount = settings.shadowCascadeCount;
				shadowData.mainLightShadowmapWidth = settings.mainLightShadowmapResolution;
				shadowData.mainLightShadowmapHeight = settings.mainLightShadowmapResolution;
				switch (shadowData.mainLightShadowCascadesCount)
				{
				case 1:
					shadowData.mainLightShadowCascadesSplit = new Vector3(1f, 0f, 0f);
					break;
				case 2:
					shadowData.mainLightShadowCascadesSplit = new Vector3(settings.cascade2Split, 1f, 0f);
					break;
				case 3:
					shadowData.mainLightShadowCascadesSplit = new Vector3(settings.cascade3Split.x, settings.cascade3Split.y, 0f);
					break;
				default:
					shadowData.mainLightShadowCascadesSplit = settings.cascade4Split;
					break;
				}
				shadowData.mainLightShadowCascadeBorder = settings.cascadeBorder;
				shadowData.supportsAdditionalLightShadows = SystemInfo.supportsShadows && settings.supportsAdditionalLightShadows && additionalLightsCastShadows;
				shadowData.additionalLightsShadowmapWidth = (shadowData.additionalLightsShadowmapHeight = settings.additionalLightsShadowmapResolution);
				shadowData.supportsSoftShadows = settings.supportsSoftShadows && (shadowData.supportsMainLightShadows || shadowData.supportsAdditionalLightShadows);
				shadowData.shadowmapDepthBufferBits = 16;
				shadowData.isKeywordAdditionalLightShadowsEnabled = false;
				shadowData.isKeywordSoftShadowsEnabled = false;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00024704 File Offset: 0x00022904
		private static void InitializePostProcessingData(UniversalRenderPipelineAsset settings, out PostProcessingData postProcessingData)
		{
			postProcessingData.gradingMode = (settings.supportsHDR ? settings.colorGradingMode : ColorGradingMode.LowDynamicRange);
			postProcessingData.lutSize = settings.colorGradingLutSize;
			postProcessingData.useFastSRGBLinearConversion = settings.useFastSRGBLinearConversion;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00024738 File Offset: 0x00022938
		private static void InitializeLightData(UniversalRenderPipelineAsset settings, NativeArray<VisibleLight> visibleLights, int mainLightIndex, out LightData lightData)
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.initializeLightData))
			{
				int maxPerObjectLights = UniversalRenderPipeline.maxPerObjectLights;
				int maxVisibleAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				lightData.mainLightIndex = mainLightIndex;
				if (settings.additionalLightsRenderingMode != LightRenderingMode.Disabled)
				{
					lightData.additionalLightsCount = Math.Min((mainLightIndex != -1) ? (visibleLights.Length - 1) : visibleLights.Length, maxVisibleAdditionalLights);
					lightData.maxPerObjectAdditionalLightsCount = Math.Min(settings.maxAdditionalLightsCount, maxPerObjectLights);
				}
				else
				{
					lightData.additionalLightsCount = 0;
					lightData.maxPerObjectAdditionalLightsCount = 0;
				}
				lightData.supportsAdditionalLights = settings.additionalLightsRenderingMode > LightRenderingMode.Disabled;
				lightData.shadeAdditionalLightsPerVertex = settings.additionalLightsRenderingMode == LightRenderingMode.PerVertex;
				lightData.visibleLights = visibleLights;
				lightData.supportsMixedLighting = settings.supportsMixedLighting;
				lightData.reflectionProbeBlending = settings.reflectionProbeBlending;
				lightData.reflectionProbeBoxProjection = settings.reflectionProbeBoxProjection;
				lightData.supportsLightLayers = RenderingUtils.SupportsLightLayers(SystemInfo.graphicsDeviceType) && settings.supportsLightLayers;
				lightData.originalIndices = new NativeArray<int>(visibleLights.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < lightData.originalIndices.Length; i++)
				{
					lightData.originalIndices[i] = i;
				}
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002486C File Offset: 0x00022A6C
		private static void CleanupLightData(ref LightData lightData)
		{
			lightData.originalIndices.Dispose();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0002487C File Offset: 0x00022A7C
		private static void UpdateCameraStereoMatrices(Camera camera, XRPass xr)
		{
			if (xr.enabled)
			{
				if (xr.singlePassEnabled)
				{
					for (int i = 0; i < Mathf.Min(2, xr.viewCount); i++)
					{
						camera.SetStereoProjectionMatrix((Camera.StereoscopicEye)i, xr.GetProjMatrix(i));
						camera.SetStereoViewMatrix((Camera.StereoscopicEye)i, xr.GetViewMatrix(i));
					}
					return;
				}
				camera.SetStereoProjectionMatrix((Camera.StereoscopicEye)xr.multipassId, xr.GetProjMatrix(0));
				camera.SetStereoViewMatrix((Camera.StereoscopicEye)xr.multipassId, xr.GetViewMatrix(0));
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000248F4 File Offset: 0x00022AF4
		private static PerObjectData GetPerObjectLightFlags(int additionalLightsCount)
		{
			PerObjectData perObjectData2;
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.getPerObjectLightFlags))
			{
				PerObjectData perObjectData = PerObjectData.LightProbe | PerObjectData.ReflectionProbes | PerObjectData.Lightmaps | PerObjectData.LightData | PerObjectData.OcclusionProbe | PerObjectData.ShadowMask;
				if (additionalLightsCount > 0)
				{
					perObjectData |= PerObjectData.LightData;
					if (!RenderingUtils.useStructuredBuffer)
					{
						perObjectData |= PerObjectData.LightIndices;
					}
				}
				perObjectData2 = perObjectData;
			}
			return perObjectData2;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0002494C File Offset: 0x00022B4C
		private static int GetMainLightIndex(UniversalRenderPipelineAsset settings, NativeArray<VisibleLight> visibleLights)
		{
			int num;
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.getMainLightIndex))
			{
				int length = visibleLights.Length;
				if (length == 0 || settings.mainLightRenderingMode != LightRenderingMode.PerPixel)
				{
					num = -1;
				}
				else
				{
					Light sun = RenderSettings.sun;
					int num2 = -1;
					float num3 = 0f;
					for (int i = 0; i < length; i++)
					{
						VisibleLight visibleLight = visibleLights[i];
						Light light = visibleLight.light;
						if (light == null)
						{
							break;
						}
						if (visibleLight.lightType == LightType.Directional)
						{
							if (light == sun)
							{
								return i;
							}
							if (light.intensity > num3)
							{
								num3 = light.intensity;
								num2 = i;
							}
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00024A18 File Offset: 0x00022C18
		private static void SetupPerFrameShaderConstants()
		{
			using (new ProfilingScope(null, UniversalRenderPipeline.Profiling.Pipeline.setupPerFrameShaderConstants))
			{
				SphericalHarmonicsL2 ambientProbe = RenderSettings.ambientProbe;
				Color color = CoreUtils.ConvertLinearToActiveColorSpace(new Color(ambientProbe[0, 0], ambientProbe[1, 0], ambientProbe[2, 0]) * RenderSettings.reflectionIntensity);
				Shader.SetGlobalVector(ShaderPropertyId.glossyEnvironmentColor, color);
				Shader.SetGlobalVector(ShaderPropertyId.glossyEnvironmentCubeMapHDR, ReflectionProbe.defaultTextureHDRDecodeValues);
				Shader.SetGlobalTexture(ShaderPropertyId.glossyEnvironmentCubeMap, ReflectionProbe.defaultTexture);
				Shader.SetGlobalVector(ShaderPropertyId.ambientSkyColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientSkyColor));
				Shader.SetGlobalVector(ShaderPropertyId.ambientEquatorColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientEquatorColor));
				Shader.SetGlobalVector(ShaderPropertyId.ambientGroundColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientGroundColor));
				Shader.SetGlobalVector(ShaderPropertyId.subtractiveShadowColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.subtractiveShadowColor));
				Shader.SetGlobalColor(ShaderPropertyId.rendererColor, Color.white);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00024B2C File Offset: 0x00022D2C
		private static void CheckAndApplyDebugSettings(ref RenderingData renderingData)
		{
			DebugDisplaySettings instance = DebugDisplaySettings.Instance;
			ref CameraData ptr = ref renderingData.cameraData;
			if (instance.AreAnySettingsActive && !ptr.isPreviewCamera)
			{
				DebugDisplaySettingsRendering renderingSettings = instance.RenderingSettings;
				int num = ptr.cameraTargetDescriptor.msaaSamples;
				if (!renderingSettings.enableMsaa)
				{
					num = 1;
				}
				if (!renderingSettings.enableHDR)
				{
					ptr.isHdrEnabled = false;
				}
				if (!instance.IsPostProcessingAllowed)
				{
					ptr.postProcessEnabled = false;
				}
				ptr.cameraTargetDescriptor.graphicsFormat = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(ptr.isHdrEnabled, true);
				ptr.cameraTargetDescriptor.msaaSamples = num;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00024BB4 File Offset: 0x00022DB4
		private static ImageUpscalingFilter ResolveUpscalingFilterSelection(Vector2 imageSize, float renderScale, UpscalingFilterSelection selection)
		{
			ImageUpscalingFilter imageUpscalingFilter = ImageUpscalingFilter.Linear;
			if (selection == UpscalingFilterSelection.FSR && !FSRUtils.IsSupported())
			{
				selection = UpscalingFilterSelection.Auto;
			}
			switch (selection)
			{
			case UpscalingFilterSelection.Auto:
			{
				float num = 1f / renderScale;
				if (Mathf.Approximately(num - Mathf.Floor(num), 0f))
				{
					float num2 = imageSize.x / num;
					float num3 = imageSize.y / num;
					if (Mathf.Approximately(num2 - Mathf.Floor(num2), 0f) && Mathf.Approximately(num3 - Mathf.Floor(num3), 0f))
					{
						imageUpscalingFilter = ImageUpscalingFilter.Point;
					}
				}
				break;
			}
			case UpscalingFilterSelection.Point:
				imageUpscalingFilter = ImageUpscalingFilter.Point;
				break;
			case UpscalingFilterSelection.FSR:
				imageUpscalingFilter = ImageUpscalingFilter.FSR;
				break;
			}
			return imageUpscalingFilter;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00024C4B File Offset: 0x00022E4B
		public static bool IsGameCamera(Camera camera)
		{
			if (camera == null)
			{
				throw new ArgumentNullException("camera");
			}
			return camera.cameraType == CameraType.Game || camera.cameraType == CameraType.VR;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00024C75 File Offset: 0x00022E75
		[Obsolete("Please use CameraData.xr.enabled instead.")]
		public static bool IsStereoEnabled(Camera camera)
		{
			if (camera == null)
			{
				throw new ArgumentNullException("camera");
			}
			return UniversalRenderPipeline.IsGameCamera(camera) && camera.stereoTargetEye == StereoTargetEyeMask.Both;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00024C9E File Offset: 0x00022E9E
		public static UniversalRenderPipelineAsset asset
		{
			get
			{
				return GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00024CAA File Offset: 0x00022EAA
		[Obsolete("Please use CameraData.xr.singlePassEnabled instead.")]
		private static bool IsMultiPassStereoEnabled(Camera camera)
		{
			if (camera == null)
			{
				throw new ArgumentNullException("camera");
			}
			return false;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00024CC1 File Offset: 0x00022EC1
		private void SortCameras(List<Camera> cameras)
		{
			if (cameras.Count > 1)
			{
				cameras.Sort(this.cameraComparison);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00024CD8 File Offset: 0x00022ED8
		private static GraphicsFormat MakeRenderTextureGraphicsFormat(bool isHdrEnabled, bool needsAlpha)
		{
			if (!isHdrEnabled)
			{
				return SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
			}
			if (!needsAlpha && RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Blend))
			{
				return GraphicsFormat.B10G11R11_UFloatPack32;
			}
			if (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R16G16B16A16_SFloat, FormatUsage.Blend))
			{
				return GraphicsFormat.R16G16B16A16_SFloat;
			}
			return SystemInfo.GetGraphicsFormat(DefaultFormat.HDR);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00024D07 File Offset: 0x00022F07
		internal static GraphicsFormat MakeUnormRenderTextureGraphicsFormat()
		{
			if (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.A2B10G10R10_UNormPack32, FormatUsage.Blend))
			{
				return GraphicsFormat.A2B10G10R10_UNormPack32;
			}
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00024D18 File Offset: 0x00022F18
		private static RenderTextureDescriptor CreateRenderTextureDescriptor(Camera camera, float renderScale, bool isHdrEnabled, int msaaSamples, bool needsAlpha, bool requiresOpaqueTexture)
		{
			RenderTextureDescriptor descriptor;
			if (camera.targetTexture == null)
			{
				descriptor = new RenderTextureDescriptor(camera.pixelWidth, camera.pixelHeight);
				descriptor.width = (int)((float)descriptor.width * renderScale);
				descriptor.height = (int)((float)descriptor.height * renderScale);
				descriptor.graphicsFormat = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(isHdrEnabled, needsAlpha);
				descriptor.depthBufferBits = 32;
				descriptor.msaaSamples = msaaSamples;
				descriptor.sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
			else
			{
				descriptor = camera.targetTexture.descriptor;
				descriptor.width = camera.pixelWidth;
				descriptor.height = camera.pixelHeight;
				if (camera.cameraType == CameraType.SceneView && !isHdrEnabled)
				{
					descriptor.graphicsFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
				}
			}
			descriptor.width = Mathf.Max(1, descriptor.width);
			descriptor.height = Mathf.Max(1, descriptor.height);
			descriptor.enableRandomWrite = false;
			descriptor.bindMS = false;
			descriptor.useDynamicScale = camera.allowDynamicResolution;
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Vulkan && descriptor.msaaSamples == 2 && SystemInfo.GetRenderTextureSupportedMSAASampleCount(descriptor) == 1)
			{
				descriptor.msaaSamples = 4;
			}
			descriptor.msaaSamples = SystemInfo.GetRenderTextureSupportedMSAASampleCount(descriptor);
			if (!SystemInfo.supportsStoreAndResolveAction && requiresOpaqueTexture)
			{
				descriptor.msaaSamples = 1;
			}
			return descriptor;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00024E68 File Offset: 0x00023068
		public static void GetLightAttenuationAndSpotDirection(LightType lightType, float lightRange, Matrix4x4 lightLocalToWorldMatrix, float spotAngle, float? innerSpotAngle, out Vector4 lightAttenuation, out Vector4 lightSpotDir)
		{
			lightAttenuation = UniversalRenderPipeline.k_DefaultLightAttenuation;
			lightSpotDir = UniversalRenderPipeline.k_DefaultLightSpotDirection;
			if (lightType != LightType.Directional)
			{
				float num = lightRange * lightRange;
				float num2 = 0.64000005f * num - num;
				float num3 = 1f / num2;
				float num4 = -num / num2;
				float num5 = 1f / Mathf.Max(0.0001f, lightRange * lightRange);
				lightAttenuation.x = ((GraphicsSettings.HasShaderDefine(Graphics.activeTier, BuiltinShaderDefine.SHADER_API_MOBILE) || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Switch) ? num3 : num5);
				lightAttenuation.y = num4;
			}
			if (lightType == LightType.Spot)
			{
				Vector4 column = lightLocalToWorldMatrix.GetColumn(2);
				lightSpotDir = new Vector4(-column.x, -column.y, -column.z, 0f);
				float num6 = Mathf.Cos(0.017453292f * spotAngle * 0.5f);
				float num7;
				if (innerSpotAngle != null)
				{
					num7 = Mathf.Cos(innerSpotAngle.Value * 0.017453292f * 0.5f);
				}
				else
				{
					num7 = Mathf.Cos(2f * Mathf.Atan(Mathf.Tan(spotAngle * 0.5f * 0.017453292f) * 46f / 64f) * 0.5f);
				}
				float num8 = Mathf.Max(0.001f, num7 - num6);
				float num9 = 1f / num8;
				float num10 = -num6 * num9;
				lightAttenuation.z = num9;
				lightAttenuation.w = num10;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00024FCC File Offset: 0x000231CC
		public static void InitializeLightConstants_Common(NativeArray<VisibleLight> lights, int lightIndex, out Vector4 lightPos, out Vector4 lightColor, out Vector4 lightAttenuation, out Vector4 lightSpotDir, out Vector4 lightOcclusionProbeChannel)
		{
			lightPos = UniversalRenderPipeline.k_DefaultLightPosition;
			lightColor = UniversalRenderPipeline.k_DefaultLightColor;
			lightOcclusionProbeChannel = UniversalRenderPipeline.k_DefaultLightsProbeChannel;
			lightAttenuation = UniversalRenderPipeline.k_DefaultLightAttenuation;
			lightSpotDir = UniversalRenderPipeline.k_DefaultLightSpotDirection;
			if (lightIndex < 0)
			{
				return;
			}
			VisibleLight visibleLight = lights[lightIndex];
			if (visibleLight.lightType == LightType.Directional)
			{
				Vector4 vector = -visibleLight.localToWorldMatrix.GetColumn(2);
				lightPos = new Vector4(vector.x, vector.y, vector.z, 0f);
			}
			else
			{
				Vector4 column = visibleLight.localToWorldMatrix.GetColumn(3);
				lightPos = new Vector4(column.x, column.y, column.z, 1f);
			}
			lightColor = visibleLight.finalColor;
			LightType lightType = visibleLight.lightType;
			float range = visibleLight.range;
			Matrix4x4 localToWorldMatrix = visibleLight.localToWorldMatrix;
			float spotAngle = visibleLight.spotAngle;
			Light light = visibleLight.light;
			UniversalRenderPipeline.GetLightAttenuationAndSpotDirection(lightType, range, localToWorldMatrix, spotAngle, (light != null) ? new float?(light.innerSpotAngle) : null, out lightAttenuation, out lightSpotDir);
			Light light2 = visibleLight.light;
			if (light2 != null && light2.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && 0 <= light2.bakingOutput.occlusionMaskChannel && light2.bakingOutput.occlusionMaskChannel < 4)
			{
				lightOcclusionProbeChannel[light2.bakingOutput.occlusionMaskChannel] = 1f;
			}
		}

		// Token: 0x04000549 RID: 1353
		public const string k_ShaderTagName = "UniversalPipeline";

		// Token: 0x0400054A RID: 1354
		internal static XRSystem m_XRSystem = new XRSystem();

		// Token: 0x0400054B RID: 1355
		internal const int k_MaxVisibleAdditionalLightsMobileShaderLevelLessThan45 = 16;

		// Token: 0x0400054C RID: 1356
		internal const int k_MaxVisibleAdditionalLightsMobile = 32;

		// Token: 0x0400054D RID: 1357
		internal const int k_MaxVisibleAdditionalLightsNonMobile = 256;

		// Token: 0x0400054E RID: 1358
		internal const int k_DefaultRenderingLayerMask = 1;

		// Token: 0x0400054F RID: 1359
		private readonly DebugDisplaySettingsUI m_DebugDisplaySettingsUI = new DebugDisplaySettingsUI();

		// Token: 0x04000550 RID: 1360
		private UniversalRenderPipelineGlobalSettings m_GlobalSettings;

		// Token: 0x04000551 RID: 1361
		private readonly UniversalRenderPipelineAsset pipelineAsset;

		// Token: 0x04000552 RID: 1362
		private static Vector4 k_DefaultLightPosition = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x04000553 RID: 1363
		private static Vector4 k_DefaultLightColor = Color.black;

		// Token: 0x04000554 RID: 1364
		private static Vector4 k_DefaultLightAttenuation = new Vector4(0f, 1f, 0f, 1f);

		// Token: 0x04000555 RID: 1365
		private static Vector4 k_DefaultLightSpotDirection = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x04000556 RID: 1366
		private static Vector4 k_DefaultLightsProbeChannel = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000557 RID: 1367
		private static List<Vector4> m_ShadowBiasData = new List<Vector4>();

		// Token: 0x04000558 RID: 1368
		private static List<int> m_ShadowResolutionData = new List<int>();

		// Token: 0x04000559 RID: 1369
		private Comparison<Camera> cameraComparison = (Camera camera1, Camera camera2) => (int)camera1.depth - (int)camera2.depth;

		// Token: 0x0400055A RID: 1370
		private static Lightmapping.RequestLightsDelegate lightsDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			LightDataGI lightDataGI = default(LightDataGI);
			if (!SupportedRenderingFeatures.active.enlighten || (SupportedRenderingFeatures.active.lightmapBakeTypes | LightmapBakeType.Realtime) == (LightmapBakeType)0)
			{
				for (int i = 0; i < requests.Length; i++)
				{
					Light light = requests[i];
					lightDataGI.InitNoBake(light.GetInstanceID());
					lightsOutput[i] = lightDataGI;
				}
				return;
			}
			for (int j = 0; j < requests.Length; j++)
			{
				Light light2 = requests[j];
				switch (light2.type)
				{
				case LightType.Spot:
				{
					SpotLight spotLight = default(SpotLight);
					LightmapperUtils.Extract(light2, ref spotLight);
					spotLight.innerConeAngle = light2.innerSpotAngle * 0.017453292f;
					spotLight.angularFalloff = AngularFalloffType.AnalyticAndInnerAngle;
					lightDataGI.Init(ref spotLight);
					break;
				}
				case LightType.Directional:
				{
					DirectionalLight directionalLight = default(DirectionalLight);
					LightmapperUtils.Extract(light2, ref directionalLight);
					lightDataGI.Init(ref directionalLight);
					break;
				}
				case LightType.Point:
				{
					PointLight pointLight = default(PointLight);
					LightmapperUtils.Extract(light2, ref pointLight);
					lightDataGI.Init(ref pointLight);
					break;
				}
				case LightType.Area:
					lightDataGI.InitNoBake(light2.GetInstanceID());
					break;
				case LightType.Disc:
					lightDataGI.InitNoBake(light2.GetInstanceID());
					break;
				default:
					lightDataGI.InitNoBake(light2.GetInstanceID());
					break;
				}
				lightDataGI.falloff = FalloffType.InverseSquared;
				lightsOutput[j] = lightDataGI;
			}
		};

		// Token: 0x0200018C RID: 396
		private static class Profiling
		{
			// Token: 0x060009EB RID: 2539 RVA: 0x00041C28 File Offset: 0x0003FE28
			public static ProfilingSampler TryGetOrAddCameraSampler(Camera camera)
			{
				ProfilingSampler profilingSampler = null;
				int hashCode = camera.GetHashCode();
				if (!UniversalRenderPipeline.Profiling.s_HashSamplerCache.TryGetValue(hashCode, out profilingSampler))
				{
					profilingSampler = new ProfilingSampler("UniversalRenderPipeline.RenderSingleCamera: " + camera.name);
					UniversalRenderPipeline.Profiling.s_HashSamplerCache.Add(hashCode, profilingSampler);
				}
				return profilingSampler;
			}

			// Token: 0x04000A05 RID: 2565
			private static Dictionary<int, ProfilingSampler> s_HashSamplerCache = new Dictionary<int, ProfilingSampler>();

			// Token: 0x04000A06 RID: 2566
			public static readonly ProfilingSampler unknownSampler = new ProfilingSampler("Unknown");

			// Token: 0x020001E3 RID: 483
			public static class Pipeline
			{
				// Token: 0x04000B7E RID: 2942
				public static readonly ProfilingSampler beginContextRendering = new ProfilingSampler("RenderPipeline.BeginContextRendering");

				// Token: 0x04000B7F RID: 2943
				public static readonly ProfilingSampler endContextRendering = new ProfilingSampler("RenderPipeline.EndContextRendering");

				// Token: 0x04000B80 RID: 2944
				public static readonly ProfilingSampler beginCameraRendering = new ProfilingSampler("RenderPipeline.BeginCameraRendering");

				// Token: 0x04000B81 RID: 2945
				public static readonly ProfilingSampler endCameraRendering = new ProfilingSampler("RenderPipeline.EndCameraRendering");

				// Token: 0x04000B82 RID: 2946
				private const string k_Name = "UniversalRenderPipeline";

				// Token: 0x04000B83 RID: 2947
				public static readonly ProfilingSampler initializeCameraData = new ProfilingSampler("UniversalRenderPipeline.InitializeCameraData");

				// Token: 0x04000B84 RID: 2948
				public static readonly ProfilingSampler initializeStackedCameraData = new ProfilingSampler("UniversalRenderPipeline.InitializeStackedCameraData");

				// Token: 0x04000B85 RID: 2949
				public static readonly ProfilingSampler initializeAdditionalCameraData = new ProfilingSampler("UniversalRenderPipeline.InitializeAdditionalCameraData");

				// Token: 0x04000B86 RID: 2950
				public static readonly ProfilingSampler initializeRenderingData = new ProfilingSampler("UniversalRenderPipeline.InitializeRenderingData");

				// Token: 0x04000B87 RID: 2951
				public static readonly ProfilingSampler initializeShadowData = new ProfilingSampler("UniversalRenderPipeline.InitializeShadowData");

				// Token: 0x04000B88 RID: 2952
				public static readonly ProfilingSampler initializeLightData = new ProfilingSampler("UniversalRenderPipeline.InitializeLightData");

				// Token: 0x04000B89 RID: 2953
				public static readonly ProfilingSampler getPerObjectLightFlags = new ProfilingSampler("UniversalRenderPipeline.GetPerObjectLightFlags");

				// Token: 0x04000B8A RID: 2954
				public static readonly ProfilingSampler getMainLightIndex = new ProfilingSampler("UniversalRenderPipeline.GetMainLightIndex");

				// Token: 0x04000B8B RID: 2955
				public static readonly ProfilingSampler setupPerFrameShaderConstants = new ProfilingSampler("UniversalRenderPipeline.SetupPerFrameShaderConstants");

				// Token: 0x020001E4 RID: 484
				public static class Renderer
				{
					// Token: 0x04000B8C RID: 2956
					private const string k_Name = "ScriptableRenderer";

					// Token: 0x04000B8D RID: 2957
					public static readonly ProfilingSampler setupCullingParameters = new ProfilingSampler("ScriptableRenderer.SetupCullingParameters");

					// Token: 0x04000B8E RID: 2958
					public static readonly ProfilingSampler setup = new ProfilingSampler("ScriptableRenderer.Setup");
				}

				// Token: 0x020001E5 RID: 485
				public static class Context
				{
					// Token: 0x04000B8F RID: 2959
					private const string k_Name = "ScriptableRenderContext";

					// Token: 0x04000B90 RID: 2960
					public static readonly ProfilingSampler submit = new ProfilingSampler("ScriptableRenderContext.Submit");
				}

				// Token: 0x020001E6 RID: 486
				public static class XR
				{
					// Token: 0x04000B91 RID: 2961
					public static readonly ProfilingSampler mirrorView = new ProfilingSampler("XR Mirror View");
				}
			}
		}
	}
}
