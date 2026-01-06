using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000055 RID: 85
	internal class DebugHandler : IDebugDisplaySettingsQuery
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00013CCE File Offset: 0x00011ECE
		private DebugDisplaySettingsLighting LightingSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.LightingSettings;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00013CDB File Offset: 0x00011EDB
		private DebugDisplaySettingsMaterial MaterialSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.MaterialSettings;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00013CE8 File Offset: 0x00011EE8
		private DebugDisplaySettingsRendering RenderingSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.RenderingSettings;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00013CF5 File Offset: 0x00011EF5
		public bool AreAnySettingsActive
		{
			get
			{
				return this.m_DebugDisplaySettings.AreAnySettingsActive;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00013D02 File Offset: 0x00011F02
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.m_DebugDisplaySettings.IsPostProcessingAllowed;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00013D0F File Offset: 0x00011F0F
		public bool IsLightingActive
		{
			get
			{
				return this.m_DebugDisplaySettings.IsLightingActive;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00013D1C File Offset: 0x00011F1C
		internal bool IsActiveModeUnsupportedForDeferred
		{
			get
			{
				return this.m_DebugDisplaySettings.LightingSettings.DebugLightingMode != DebugLightingMode.None || this.m_DebugDisplaySettings.LightingSettings.DebugLightingFeatureFlagsMask != DebugLightingFeatureFlags.None || this.m_DebugDisplaySettings.RenderingSettings.debugSceneOverrideMode != DebugSceneOverrideMode.None || this.m_DebugDisplaySettings.MaterialSettings.DebugMaterialModeData != DebugMaterialMode.None || this.m_DebugDisplaySettings.MaterialSettings.DebugVertexAttributeIndexData != DebugVertexAttributeMode.None || this.m_DebugDisplaySettings.MaterialSettings.MaterialValidationMode > DebugMaterialValidationMode.None;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00013D98 File Offset: 0x00011F98
		public bool TryGetScreenClearColor(ref Color color)
		{
			return this.m_DebugDisplaySettings.TryGetScreenClearColor(ref color);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00013DA6 File Offset: 0x00011FA6
		internal Material ReplacementMaterial
		{
			get
			{
				return this.m_ReplacementMaterial;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00013DAE File Offset: 0x00011FAE
		internal DebugDisplaySettings DebugDisplaySettings
		{
			get
			{
				return this.m_DebugDisplaySettings;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00013DB8 File Offset: 0x00011FB8
		internal bool IsScreenClearNeeded
		{
			get
			{
				Color black = Color.black;
				return this.TryGetScreenClearColor(ref black);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00013DD3 File Offset: 0x00011FD3
		internal bool IsRenderPassSupported
		{
			get
			{
				return this.RenderingSettings.debugSceneOverrideMode == DebugSceneOverrideMode.None || this.RenderingSettings.debugSceneOverrideMode == DebugSceneOverrideMode.Overdraw;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00013DF4 File Offset: 0x00011FF4
		internal DebugHandler(ScriptableRendererData scriptableRendererData)
		{
			Shader debugReplacementPS = scriptableRendererData.debugShaders.debugReplacementPS;
			this.m_DebugDisplaySettings = DebugDisplaySettings.Instance;
			this.m_ReplacementMaterial = ((debugReplacementPS == null) ? null : CoreUtils.CreateEngineMaterial(debugReplacementPS));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013E36 File Offset: 0x00012036
		internal bool IsActiveForCamera(ref CameraData cameraData)
		{
			return !cameraData.isPreviewCamera && this.AreAnySettingsActive;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00013E48 File Offset: 0x00012048
		internal bool TryGetFullscreenDebugMode(out DebugFullScreenMode debugFullScreenMode)
		{
			int num;
			return this.TryGetFullscreenDebugMode(out debugFullScreenMode, out num);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00013E5E File Offset: 0x0001205E
		internal bool TryGetFullscreenDebugMode(out DebugFullScreenMode debugFullScreenMode, out int textureHeightPercent)
		{
			debugFullScreenMode = this.RenderingSettings.debugFullScreenMode;
			textureHeightPercent = this.RenderingSettings.debugFullScreenModeOutputSizeScreenPercent;
			return debugFullScreenMode > DebugFullScreenMode.None;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00013E80 File Offset: 0x00012080
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void SetupShaderProperties(CommandBuffer cmd, int passIndex = 0)
		{
			if (this.LightingSettings.DebugLightingMode == DebugLightingMode.ShadowCascades)
			{
				cmd.EnableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			else
			{
				cmd.DisableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			switch (this.RenderingSettings.debugSceneOverrideMode)
			{
			case DebugSceneOverrideMode.Overdraw:
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, new Color(0.1f, 0.01f, 0.01f, 1f));
				break;
			case DebugSceneOverrideMode.Wireframe:
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, Color.black);
				break;
			case DebugSceneOverrideMode.SolidWireframe:
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, (passIndex == 0) ? Color.white : Color.black);
				break;
			case DebugSceneOverrideMode.ShadedWireframe:
				if (passIndex == 0)
				{
					cmd.DisableShaderKeyword("DEBUG_DISPLAY");
				}
				else if (passIndex == 1)
				{
					cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, Color.black);
					cmd.EnableShaderKeyword("DEBUG_DISPLAY");
				}
				break;
			}
			DebugMaterialValidationMode materialValidationMode = this.MaterialSettings.MaterialValidationMode;
			if (materialValidationMode == DebugMaterialValidationMode.Albedo)
			{
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoMinLuminanceId, this.MaterialSettings.AlbedoMinLuminance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoMaxLuminanceId, this.MaterialSettings.AlbedoMaxLuminance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoSaturationToleranceId, this.MaterialSettings.AlbedoSaturationTolerance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoHueToleranceId, this.MaterialSettings.AlbedoHueTolerance);
				cmd.SetGlobalColor(DebugHandler.k_DebugValidateAlbedoCompareColorId, this.MaterialSettings.AlbedoCompareColor.linear);
				return;
			}
			if (materialValidationMode != DebugMaterialValidationMode.Metallic)
			{
				return;
			}
			cmd.SetGlobalFloat(DebugHandler.k_DebugValidateMetallicMinValueId, this.MaterialSettings.MetallicMinValue);
			cmd.SetGlobalFloat(DebugHandler.k_DebugValidateMetallicMaxValueId, this.MaterialSettings.MetallicMaxValue);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00014014 File Offset: 0x00012214
		internal void SetDebugRenderTarget(RenderTargetIdentifier renderTargetIdentifier, Rect displayRect, bool supportsStereo)
		{
			this.m_HasDebugRenderTarget = true;
			this.m_DebugRenderTargetSupportsStereo = supportsStereo;
			this.m_DebugRenderTargetIdentifier = renderTargetIdentifier;
			this.m_DebugRenderTargetPixelRect = new Vector4(displayRect.x, displayRect.y, displayRect.width, displayRect.height);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00014052 File Offset: 0x00012252
		internal void ResetDebugRenderTarget()
		{
			this.m_HasDebugRenderTarget = false;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001405C File Offset: 0x0001225C
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void UpdateShaderGlobalPropertiesForFinalValidationPass(CommandBuffer cmd, ref CameraData cameraData, bool isFinalPass)
		{
			if (!isFinalPass || !cameraData.resolveFinalTarget)
			{
				cmd.DisableShaderKeyword("DEBUG_DISPLAY");
				return;
			}
			if (this.IsActiveForCamera(ref cameraData))
			{
				cmd.EnableShaderKeyword("DEBUG_DISPLAY");
			}
			else
			{
				cmd.DisableShaderKeyword("DEBUG_DISPLAY");
			}
			if (this.m_HasDebugRenderTarget)
			{
				cmd.SetGlobalTexture(this.m_DebugRenderTargetSupportsStereo ? DebugHandler.k_DebugTexturePropertyId : DebugHandler.k_DebugTextureNoStereoPropertyId, this.m_DebugRenderTargetIdentifier);
				cmd.SetGlobalVector(DebugHandler.k_DebugTextureDisplayRect, this.m_DebugRenderTargetPixelRect);
				cmd.SetGlobalInteger(DebugHandler.k_DebugRenderTargetSupportsStereo, this.m_DebugRenderTargetSupportsStereo ? 1 : 0);
			}
			DebugDisplaySettingsRendering renderingSettings = this.m_DebugDisplaySettings.RenderingSettings;
			if (renderingSettings.validationMode == DebugValidationMode.HighlightOutsideOfRange)
			{
				cmd.SetGlobalInteger(DebugHandler.k_ValidationChannelsId, (int)renderingSettings.validationChannels);
				cmd.SetGlobalFloat(DebugHandler.k_RangeMinimumId, renderingSettings.ValidationRangeMin);
				cmd.SetGlobalFloat(DebugHandler.k_RangeMaximumId, renderingSettings.ValidationRangeMax);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001413C File Offset: 0x0001233C
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void Setup(ScriptableRenderContext context, ref CameraData cameraData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get("");
			if (this.IsActiveForCamera(ref cameraData))
			{
				commandBuffer.EnableShaderKeyword("DEBUG_DISPLAY");
				commandBuffer.SetGlobalFloat(DebugHandler.k_DebugMaterialModeId, (float)this.MaterialSettings.DebugMaterialModeData);
				commandBuffer.SetGlobalFloat(DebugHandler.k_DebugVertexAttributeModeId, (float)this.MaterialSettings.DebugVertexAttributeIndexData);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugMaterialValidationModeId, (int)this.MaterialSettings.MaterialValidationMode);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugMipInfoModeId, (int)this.RenderingSettings.debugMipInfoMode);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugSceneOverrideModeId, (int)this.RenderingSettings.debugSceneOverrideMode);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugFullScreenModeId, (int)this.RenderingSettings.debugFullScreenMode);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugValidationModeId, (int)this.RenderingSettings.validationMode);
				commandBuffer.SetGlobalColor(DebugHandler.k_DebugValidateBelowMinThresholdColorPropertyId, Color.red);
				commandBuffer.SetGlobalColor(DebugHandler.k_DebugValidateAboveMaxThresholdColorPropertyId, Color.blue);
				commandBuffer.SetGlobalFloat(DebugHandler.k_DebugLightingModeId, (float)this.LightingSettings.DebugLightingMode);
				commandBuffer.SetGlobalInteger(DebugHandler.k_DebugLightingFeatureFlagsId, (int)this.LightingSettings.DebugLightingFeatureFlagsMask);
				commandBuffer.SetGlobalColor(DebugHandler.k_DebugColorInvalidModePropertyId, Color.red);
			}
			else
			{
				commandBuffer.DisableShaderKeyword("DEBUG_DISPLAY");
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001427F File Offset: 0x0001247F
		internal IEnumerable<DebugRenderSetup> CreateDebugRenderSetupEnumerable(ScriptableRenderContext context, CommandBuffer commandBuffer)
		{
			return new DebugHandler.DebugRenderPassEnumerable(this, context, commandBuffer);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001428C File Offset: 0x0001248C
		internal void DrawWithDebugRenderState(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock, DebugHandler.DrawFunction func)
		{
			foreach (DebugRenderSetup debugRenderSetup in this.CreateDebugRenderSetupEnumerable(context, cmd))
			{
				DrawingSettings drawingSettings2 = debugRenderSetup.CreateDrawingSettings(drawingSettings);
				RenderStateBlock renderStateBlock2 = debugRenderSetup.GetRenderStateBlock(renderStateBlock);
				func(context, ref renderingData, ref drawingSettings2, ref filteringSettings, ref renderStateBlock2);
			}
		}

		// Token: 0x04000245 RID: 581
		private static readonly int k_DebugColorInvalidModePropertyId = Shader.PropertyToID("_DebugColorInvalidMode");

		// Token: 0x04000246 RID: 582
		private static readonly int k_DebugColorPropertyId = Shader.PropertyToID("_DebugColor");

		// Token: 0x04000247 RID: 583
		private static readonly int k_DebugTexturePropertyId = Shader.PropertyToID("_DebugTexture");

		// Token: 0x04000248 RID: 584
		private static readonly int k_DebugTextureNoStereoPropertyId = Shader.PropertyToID("_DebugTextureNoStereo");

		// Token: 0x04000249 RID: 585
		private static readonly int k_DebugTextureDisplayRect = Shader.PropertyToID("_DebugTextureDisplayRect");

		// Token: 0x0400024A RID: 586
		private static readonly int k_DebugRenderTargetSupportsStereo = Shader.PropertyToID("_DebugRenderTargetSupportsStereo");

		// Token: 0x0400024B RID: 587
		private static readonly int k_DebugMaterialModeId = Shader.PropertyToID("_DebugMaterialMode");

		// Token: 0x0400024C RID: 588
		private static readonly int k_DebugVertexAttributeModeId = Shader.PropertyToID("_DebugVertexAttributeMode");

		// Token: 0x0400024D RID: 589
		private static readonly int k_DebugMaterialValidationModeId = Shader.PropertyToID("_DebugMaterialValidationMode");

		// Token: 0x0400024E RID: 590
		private static readonly int k_DebugMipInfoModeId = Shader.PropertyToID("_DebugMipInfoMode");

		// Token: 0x0400024F RID: 591
		private static readonly int k_DebugSceneOverrideModeId = Shader.PropertyToID("_DebugSceneOverrideMode");

		// Token: 0x04000250 RID: 592
		private static readonly int k_DebugFullScreenModeId = Shader.PropertyToID("_DebugFullScreenMode");

		// Token: 0x04000251 RID: 593
		private static readonly int k_DebugValidationModeId = Shader.PropertyToID("_DebugValidationMode");

		// Token: 0x04000252 RID: 594
		private static readonly int k_DebugValidateBelowMinThresholdColorPropertyId = Shader.PropertyToID("_DebugValidateBelowMinThresholdColor");

		// Token: 0x04000253 RID: 595
		private static readonly int k_DebugValidateAboveMaxThresholdColorPropertyId = Shader.PropertyToID("_DebugValidateAboveMaxThresholdColor");

		// Token: 0x04000254 RID: 596
		private static readonly int k_DebugLightingModeId = Shader.PropertyToID("_DebugLightingMode");

		// Token: 0x04000255 RID: 597
		private static readonly int k_DebugLightingFeatureFlagsId = Shader.PropertyToID("_DebugLightingFeatureFlags");

		// Token: 0x04000256 RID: 598
		private static readonly int k_DebugValidateAlbedoMinLuminanceId = Shader.PropertyToID("_DebugValidateAlbedoMinLuminance");

		// Token: 0x04000257 RID: 599
		private static readonly int k_DebugValidateAlbedoMaxLuminanceId = Shader.PropertyToID("_DebugValidateAlbedoMaxLuminance");

		// Token: 0x04000258 RID: 600
		private static readonly int k_DebugValidateAlbedoSaturationToleranceId = Shader.PropertyToID("_DebugValidateAlbedoSaturationTolerance");

		// Token: 0x04000259 RID: 601
		private static readonly int k_DebugValidateAlbedoHueToleranceId = Shader.PropertyToID("_DebugValidateAlbedoHueTolerance");

		// Token: 0x0400025A RID: 602
		private static readonly int k_DebugValidateAlbedoCompareColorId = Shader.PropertyToID("_DebugValidateAlbedoCompareColor");

		// Token: 0x0400025B RID: 603
		private static readonly int k_DebugValidateMetallicMinValueId = Shader.PropertyToID("_DebugValidateMetallicMinValue");

		// Token: 0x0400025C RID: 604
		private static readonly int k_DebugValidateMetallicMaxValueId = Shader.PropertyToID("_DebugValidateMetallicMaxValue");

		// Token: 0x0400025D RID: 605
		private static readonly int k_ValidationChannelsId = Shader.PropertyToID("_ValidationChannels");

		// Token: 0x0400025E RID: 606
		private static readonly int k_RangeMinimumId = Shader.PropertyToID("_RangeMinimum");

		// Token: 0x0400025F RID: 607
		private static readonly int k_RangeMaximumId = Shader.PropertyToID("_RangeMaximum");

		// Token: 0x04000260 RID: 608
		private readonly Material m_ReplacementMaterial;

		// Token: 0x04000261 RID: 609
		private bool m_HasDebugRenderTarget;

		// Token: 0x04000262 RID: 610
		private bool m_DebugRenderTargetSupportsStereo;

		// Token: 0x04000263 RID: 611
		private Vector4 m_DebugRenderTargetPixelRect;

		// Token: 0x04000264 RID: 612
		private RenderTargetIdentifier m_DebugRenderTargetIdentifier;

		// Token: 0x04000265 RID: 613
		private readonly DebugDisplaySettings m_DebugDisplaySettings;

		// Token: 0x02000163 RID: 355
		private class DebugRenderPassEnumerable : IEnumerable<DebugRenderSetup>, IEnumerable
		{
			// Token: 0x06000984 RID: 2436 RVA: 0x0003FBA0 File Offset: 0x0003DDA0
			public DebugRenderPassEnumerable(DebugHandler debugHandler, ScriptableRenderContext context, CommandBuffer commandBuffer)
			{
				this.m_DebugHandler = debugHandler;
				this.m_Context = context;
				this.m_CommandBuffer = commandBuffer;
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x0003FBBD File Offset: 0x0003DDBD
			public IEnumerator<DebugRenderSetup> GetEnumerator()
			{
				return new DebugHandler.DebugRenderPassEnumerable.Enumerator(this.m_DebugHandler, this.m_Context, this.m_CommandBuffer);
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x0003FBD6 File Offset: 0x0003DDD6
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000924 RID: 2340
			private readonly DebugHandler m_DebugHandler;

			// Token: 0x04000925 RID: 2341
			private readonly ScriptableRenderContext m_Context;

			// Token: 0x04000926 RID: 2342
			private readonly CommandBuffer m_CommandBuffer;

			// Token: 0x020001DB RID: 475
			private class Enumerator : IEnumerator<DebugRenderSetup>, IEnumerator, IDisposable
			{
				// Token: 0x1700023E RID: 574
				// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00043331 File Offset: 0x00041531
				// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x00043339 File Offset: 0x00041539
				public DebugRenderSetup Current { get; private set; }

				// Token: 0x1700023F RID: 575
				// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00043342 File Offset: 0x00041542
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06000AB2 RID: 2738 RVA: 0x0004334C File Offset: 0x0004154C
				public Enumerator(DebugHandler debugHandler, ScriptableRenderContext context, CommandBuffer commandBuffer)
				{
					DebugSceneOverrideMode debugSceneOverrideMode = debugHandler.DebugDisplaySettings.RenderingSettings.debugSceneOverrideMode;
					this.m_DebugHandler = debugHandler;
					this.m_Context = context;
					this.m_CommandBuffer = commandBuffer;
					this.m_NumIterations = ((debugSceneOverrideMode == DebugSceneOverrideMode.SolidWireframe || debugSceneOverrideMode == DebugSceneOverrideMode.ShadedWireframe) ? 2 : 1);
					this.m_Index = -1;
				}

				// Token: 0x06000AB3 RID: 2739 RVA: 0x000433A0 File Offset: 0x000415A0
				public bool MoveNext()
				{
					DebugRenderSetup debugRenderSetup = this.Current;
					if (debugRenderSetup != null)
					{
						debugRenderSetup.Dispose();
					}
					int num = this.m_Index + 1;
					this.m_Index = num;
					if (num >= this.m_NumIterations)
					{
						return false;
					}
					this.Current = new DebugRenderSetup(this.m_DebugHandler, this.m_Context, this.m_CommandBuffer, this.m_Index);
					return true;
				}

				// Token: 0x06000AB4 RID: 2740 RVA: 0x000433FD File Offset: 0x000415FD
				public void Reset()
				{
					if (this.Current != null)
					{
						this.Current.Dispose();
						this.Current = null;
					}
					this.m_Index = -1;
				}

				// Token: 0x06000AB5 RID: 2741 RVA: 0x00043420 File Offset: 0x00041620
				public void Dispose()
				{
					DebugRenderSetup debugRenderSetup = this.Current;
					if (debugRenderSetup == null)
					{
						return;
					}
					debugRenderSetup.Dispose();
				}

				// Token: 0x04000B64 RID: 2916
				private readonly DebugHandler m_DebugHandler;

				// Token: 0x04000B65 RID: 2917
				private readonly ScriptableRenderContext m_Context;

				// Token: 0x04000B66 RID: 2918
				private readonly CommandBuffer m_CommandBuffer;

				// Token: 0x04000B67 RID: 2919
				private readonly int m_NumIterations;

				// Token: 0x04000B68 RID: 2920
				private int m_Index;
			}
		}

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x06000988 RID: 2440
		internal delegate void DrawFunction(ScriptableRenderContext context, ref RenderingData renderingData, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock);
	}
}
