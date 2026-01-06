using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000108 RID: 264
	public class ColorGradingLutPass : ScriptableRenderPass
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00033324 File Offset: 0x00031524
		public ColorGradingLutPass(RenderPassEvent evt, PostProcessData data)
		{
			base.profilingSampler = new ProfilingSampler("ColorGradingLutPass");
			base.renderPassEvent = evt;
			base.overrideCameraTarget = true;
			this.m_LutBuilderLdr = this.<.ctor>g__Load|6_0(data.shaders.lutBuilderLdrPS);
			this.m_LutBuilderHdr = this.<.ctor>g__Load|6_0(data.shaders.lutBuilderHdrPS);
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, FormatUsage.Blend))
			{
				this.m_HdrLutFormat = GraphicsFormat.R16G16B16A16_SFloat;
			}
			else if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Blend))
			{
				this.m_HdrLutFormat = GraphicsFormat.B10G11R11_UFloatPack32;
			}
			else
			{
				this.m_HdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;
			}
			this.m_LdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;
			base.useNativeRenderPass = false;
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 && Graphics.minOpenGLESVersion <= OpenGLESVersion.OpenGLES30 && SystemInfo.graphicsDeviceName.StartsWith("Adreno (TM) 3"))
			{
				this.m_AllowColorGradingACESHDR = false;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000333F0 File Offset: 0x000315F0
		public void Setup(in RenderTargetHandle internalLut)
		{
			this.m_InternalLut = internalLut;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00033400 File Offset: 0x00031600
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.ColorGradingLUT)))
			{
				VolumeStack stack = VolumeManager.instance.stack;
				ChannelMixer component = stack.GetComponent<ChannelMixer>();
				ColorAdjustments component2 = stack.GetComponent<ColorAdjustments>();
				ColorCurves component3 = stack.GetComponent<ColorCurves>();
				LiftGammaGain component4 = stack.GetComponent<LiftGammaGain>();
				ShadowsMidtonesHighlights component5 = stack.GetComponent<ShadowsMidtonesHighlights>();
				SplitToning component6 = stack.GetComponent<SplitToning>();
				Tonemapping component7 = stack.GetComponent<Tonemapping>();
				WhiteBalance component8 = stack.GetComponent<WhiteBalance>();
				bool flag = renderingData.postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
				int lutSize = renderingData.postProcessingData.lutSize;
				int num = lutSize * lutSize;
				GraphicsFormat graphicsFormat = (flag ? this.m_HdrLutFormat : this.m_LdrLutFormat);
				Material material = (flag ? this.m_LutBuilderHdr : this.m_LutBuilderLdr);
				RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(num, lutSize, graphicsFormat, 0);
				renderTextureDescriptor.vrUsage = VRTextureUsage.None;
				commandBuffer.GetTemporaryRT(this.m_InternalLut.id, renderTextureDescriptor, FilterMode.Bilinear);
				Vector3 vector = ColorUtils.ColorBalanceToLMSCoeffs(component8.temperature.value, component8.tint.value);
				Vector4 vector2 = new Vector4(component2.hueShift.value / 360f, component2.saturation.value / 100f + 1f, component2.contrast.value / 100f + 1f, 0f);
				Vector4 vector3 = new Vector4(component.redOutRedIn.value / 100f, component.redOutGreenIn.value / 100f, component.redOutBlueIn.value / 100f, 0f);
				Vector4 vector4 = new Vector4(component.greenOutRedIn.value / 100f, component.greenOutGreenIn.value / 100f, component.greenOutBlueIn.value / 100f, 0f);
				Vector4 vector5 = new Vector4(component.blueOutRedIn.value / 100f, component.blueOutGreenIn.value / 100f, component.blueOutBlueIn.value / 100f, 0f);
				Vector4 vector6 = new Vector4(component5.shadowsStart.value, component5.shadowsEnd.value, component5.highlightsStart.value, component5.highlightsEnd.value);
				Vector4 vector7 = component5.shadows.value;
				Vector4 vector8 = component5.midtones.value;
				Vector4 vector9 = component5.highlights.value;
				ValueTuple<Vector4, Vector4, Vector4> valueTuple = ColorUtils.PrepareShadowsMidtonesHighlights(in vector7, in vector8, in vector9);
				Vector4 item = valueTuple.Item1;
				Vector4 item2 = valueTuple.Item2;
				Vector4 item3 = valueTuple.Item3;
				vector7 = component4.lift.value;
				vector8 = component4.gamma.value;
				vector9 = component4.gain.value;
				ValueTuple<Vector4, Vector4, Vector4> valueTuple2 = ColorUtils.PrepareLiftGammaGain(in vector7, in vector8, in vector9);
				Vector4 item4 = valueTuple2.Item1;
				Vector4 item5 = valueTuple2.Item2;
				Vector4 item6 = valueTuple2.Item3;
				vector7 = component6.shadows.value;
				vector8 = component6.highlights.value;
				ValueTuple<Vector4, Vector4> valueTuple3 = ColorUtils.PrepareSplitToning(in vector7, in vector8, component6.balance.value);
				Vector4 item7 = valueTuple3.Item1;
				Vector4 item8 = valueTuple3.Item2;
				Vector4 vector10 = new Vector4((float)lutSize, 0.5f / (float)num, 0.5f / (float)lutSize, (float)lutSize / ((float)lutSize - 1f));
				material.SetVector(ColorGradingLutPass.ShaderConstants._Lut_Params, vector10);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ColorBalance, vector);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ColorFilter, component2.colorFilter.value.linear);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerRed, vector3);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerGreen, vector4);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerBlue, vector5);
				material.SetVector(ColorGradingLutPass.ShaderConstants._HueSatCon, vector2);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Lift, item4);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Gamma, item5);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Gain, item6);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Shadows, item);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Midtones, item2);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Highlights, item3);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ShaHiLimits, vector6);
				material.SetVector(ColorGradingLutPass.ShaderConstants._SplitShadows, item7);
				material.SetVector(ColorGradingLutPass.ShaderConstants._SplitHighlights, item8);
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveMaster, component3.master.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveRed, component3.red.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveGreen, component3.green.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveBlue, component3.blue.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveHueVsHue, component3.hueVsHue.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveHueVsSat, component3.hueVsSat.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveLumVsSat, component3.lumVsSat.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveSatVsSat, component3.satVsSat.value.GetTexture());
				if (flag)
				{
					material.shaderKeywords = null;
					TonemappingMode value = component7.mode.value;
					if (value != TonemappingMode.Neutral)
					{
						if (value == TonemappingMode.ACES)
						{
							material.EnableKeyword(this.m_AllowColorGradingACESHDR ? "_TONEMAP_ACES" : "_TONEMAP_NEUTRAL");
						}
					}
					else
					{
						material.EnableKeyword("_TONEMAP_NEUTRAL");
					}
				}
				renderingData.cameraData.xr.StopSinglePass(commandBuffer);
				commandBuffer.Blit(null, this.m_InternalLut.id, material);
				renderingData.cameraData.xr.StartSinglePass(commandBuffer);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000339EC File Offset: 0x00031BEC
		public override void OnFinishCameraStackRendering(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(this.m_InternalLut.id);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000339FF File Offset: 0x00031BFF
		public void Cleanup()
		{
			CoreUtils.Destroy(this.m_LutBuilderLdr);
			CoreUtils.Destroy(this.m_LutBuilderHdr);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00033A17 File Offset: 0x00031C17
		[CompilerGenerated]
		private Material <.ctor>g__Load|6_0(Shader shader)
		{
			if (shader == null)
			{
				Debug.LogError("Missing shader. " + base.GetType().DeclaringType.Name + " render pass will not execute. Check for missing reference in the renderer resources.");
				return null;
			}
			return CoreUtils.CreateEngineMaterial(shader);
		}

		// Token: 0x04000782 RID: 1922
		private readonly Material m_LutBuilderLdr;

		// Token: 0x04000783 RID: 1923
		private readonly Material m_LutBuilderHdr;

		// Token: 0x04000784 RID: 1924
		private readonly GraphicsFormat m_HdrLutFormat;

		// Token: 0x04000785 RID: 1925
		private readonly GraphicsFormat m_LdrLutFormat;

		// Token: 0x04000786 RID: 1926
		private RenderTargetHandle m_InternalLut;

		// Token: 0x04000787 RID: 1927
		private bool m_AllowColorGradingACESHDR = true;

		// Token: 0x020001A8 RID: 424
		private static class ShaderConstants
		{
			// Token: 0x04000ABF RID: 2751
			public static readonly int _Lut_Params = Shader.PropertyToID("_Lut_Params");

			// Token: 0x04000AC0 RID: 2752
			public static readonly int _ColorBalance = Shader.PropertyToID("_ColorBalance");

			// Token: 0x04000AC1 RID: 2753
			public static readonly int _ColorFilter = Shader.PropertyToID("_ColorFilter");

			// Token: 0x04000AC2 RID: 2754
			public static readonly int _ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

			// Token: 0x04000AC3 RID: 2755
			public static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

			// Token: 0x04000AC4 RID: 2756
			public static readonly int _ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

			// Token: 0x04000AC5 RID: 2757
			public static readonly int _HueSatCon = Shader.PropertyToID("_HueSatCon");

			// Token: 0x04000AC6 RID: 2758
			public static readonly int _Lift = Shader.PropertyToID("_Lift");

			// Token: 0x04000AC7 RID: 2759
			public static readonly int _Gamma = Shader.PropertyToID("_Gamma");

			// Token: 0x04000AC8 RID: 2760
			public static readonly int _Gain = Shader.PropertyToID("_Gain");

			// Token: 0x04000AC9 RID: 2761
			public static readonly int _Shadows = Shader.PropertyToID("_Shadows");

			// Token: 0x04000ACA RID: 2762
			public static readonly int _Midtones = Shader.PropertyToID("_Midtones");

			// Token: 0x04000ACB RID: 2763
			public static readonly int _Highlights = Shader.PropertyToID("_Highlights");

			// Token: 0x04000ACC RID: 2764
			public static readonly int _ShaHiLimits = Shader.PropertyToID("_ShaHiLimits");

			// Token: 0x04000ACD RID: 2765
			public static readonly int _SplitShadows = Shader.PropertyToID("_SplitShadows");

			// Token: 0x04000ACE RID: 2766
			public static readonly int _SplitHighlights = Shader.PropertyToID("_SplitHighlights");

			// Token: 0x04000ACF RID: 2767
			public static readonly int _CurveMaster = Shader.PropertyToID("_CurveMaster");

			// Token: 0x04000AD0 RID: 2768
			public static readonly int _CurveRed = Shader.PropertyToID("_CurveRed");

			// Token: 0x04000AD1 RID: 2769
			public static readonly int _CurveGreen = Shader.PropertyToID("_CurveGreen");

			// Token: 0x04000AD2 RID: 2770
			public static readonly int _CurveBlue = Shader.PropertyToID("_CurveBlue");

			// Token: 0x04000AD3 RID: 2771
			public static readonly int _CurveHueVsHue = Shader.PropertyToID("_CurveHueVsHue");

			// Token: 0x04000AD4 RID: 2772
			public static readonly int _CurveHueVsSat = Shader.PropertyToID("_CurveHueVsSat");

			// Token: 0x04000AD5 RID: 2773
			public static readonly int _CurveLumVsSat = Shader.PropertyToID("_CurveLumVsSat");

			// Token: 0x04000AD6 RID: 2774
			public static readonly int _CurveSatVsSat = Shader.PropertyToID("_CurveSatVsSat");
		}
	}
}
