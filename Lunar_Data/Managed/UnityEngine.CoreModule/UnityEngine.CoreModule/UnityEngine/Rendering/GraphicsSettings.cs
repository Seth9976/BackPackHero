using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DF RID: 991
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : Object
	{
		// Token: 0x06001F96 RID: 8086 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private GraphicsSettings()
		{
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001F97 RID: 8087
		// (set) Token: 0x06001F98 RID: 8088
		public static extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x00033B38 File Offset: 0x00031D38
		// (set) Token: 0x06001F9A RID: 8090 RVA: 0x00033B4D File Offset: 0x00031D4D
		public static Vector3 transparencySortAxis
		{
			get
			{
				Vector3 vector;
				GraphicsSettings.get_transparencySortAxis_Injected(out vector);
				return vector;
			}
			set
			{
				GraphicsSettings.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001F9B RID: 8091
		// (set) Token: 0x06001F9C RID: 8092
		public static extern bool realtimeDirectRectangularAreaLights
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001F9D RID: 8093
		// (set) Token: 0x06001F9E RID: 8094
		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001F9F RID: 8095
		// (set) Token: 0x06001FA0 RID: 8096
		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001FA1 RID: 8097
		// (set) Token: 0x06001FA2 RID: 8098
		public static extern uint defaultRenderingLayerMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001FA3 RID: 8099
		// (set) Token: 0x06001FA4 RID: 8100
		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001FA5 RID: 8101
		// (set) Token: 0x06001FA6 RID: 8102
		public static extern bool logWhenShaderIsCompiled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001FA7 RID: 8103
		// (set) Token: 0x06001FA8 RID: 8104
		public static extern bool disableBuiltinCustomRenderTextureUpdate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001FA9 RID: 8105
		public static extern VideoShadersIncludeMode videoShadersIncludeMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001FAA RID: 8106
		[MethodImpl(4096)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		// Token: 0x06001FAB RID: 8107 RVA: 0x00033B58 File Offset: 0x00031D58
		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001FAC RID: 8108
		[NativeName("CurrentRenderPipeline")]
		private static extern ScriptableObject INTERNAL_currentRenderPipeline
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00033B78 File Offset: 0x00031D78
		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00033B94 File Offset: 0x00031D94
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00033BAB File Offset: 0x00031DAB
		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001FB0 RID: 8112
		// (set) Token: 0x06001FB1 RID: 8113
		[NativeName("DefaultRenderPipeline")]
		private static extern ScriptableObject INTERNAL_defaultRenderPipeline
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x00033BB8 File Offset: 0x00031DB8
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x00033BD4 File Offset: 0x00031DD4
		public static RenderPipelineAsset defaultRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_defaultRenderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.INTERNAL_defaultRenderPipeline = value;
			}
		}

		// Token: 0x06001FB4 RID: 8116
		[NativeName("GetAllConfiguredRenderPipelinesForScript")]
		[MethodImpl(4096)]
		private static extern ScriptableObject[] GetAllConfiguredRenderPipelines();

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public static RenderPipelineAsset[] allConfiguredRenderPipelines
		{
			get
			{
				return Enumerable.ToArray<RenderPipelineAsset>(Enumerable.Cast<RenderPipelineAsset>(GraphicsSettings.GetAllConfiguredRenderPipelines()));
			}
		}

		// Token: 0x06001FB6 RID: 8118
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern Object GetGraphicsSettings();

		// Token: 0x06001FB7 RID: 8119
		[NativeName("SetShaderModeScript")]
		[MethodImpl(4096)]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		// Token: 0x06001FB8 RID: 8120
		[NativeName("GetShaderModeScript")]
		[MethodImpl(4096)]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		// Token: 0x06001FB9 RID: 8121
		[NativeName("SetCustomShaderScript")]
		[MethodImpl(4096)]
		public static extern void SetCustomShader(BuiltinShaderType type, Shader shader);

		// Token: 0x06001FBA RID: 8122
		[NativeName("GetCustomShaderScript")]
		[MethodImpl(4096)]
		public static extern Shader GetCustomShader(BuiltinShaderType type);

		// Token: 0x06001FBB RID: 8123 RVA: 0x00033C01 File Offset: 0x00031E01
		public static void RegisterRenderPipelineSettings<T>(RenderPipelineGlobalSettings settings) where T : RenderPipeline
		{
			GraphicsSettings.RegisterRenderPipeline(typeof(T).FullName, settings);
		}

		// Token: 0x06001FBC RID: 8124
		[NativeName("RegisterRenderPipelineSettings")]
		[MethodImpl(4096)]
		private static extern void RegisterRenderPipeline(string renderpipelineName, Object settings);

		// Token: 0x06001FBD RID: 8125 RVA: 0x00033C1A File Offset: 0x00031E1A
		public static void UnregisterRenderPipelineSettings<T>() where T : RenderPipeline
		{
			GraphicsSettings.UnregisterRenderPipeline(typeof(T).FullName);
		}

		// Token: 0x06001FBE RID: 8126
		[NativeName("UnregisterRenderPipelineSettings")]
		[MethodImpl(4096)]
		private static extern void UnregisterRenderPipeline(string renderpipelineName);

		// Token: 0x06001FBF RID: 8127 RVA: 0x00033C34 File Offset: 0x00031E34
		public static RenderPipelineGlobalSettings GetSettingsForRenderPipeline<T>() where T : RenderPipeline
		{
			return GraphicsSettings.GetSettingsForRenderPipeline(typeof(T).FullName) as RenderPipelineGlobalSettings;
		}

		// Token: 0x06001FC0 RID: 8128
		[NativeName("GetSettingsForRenderPipeline")]
		[MethodImpl(4096)]
		private static extern Object GetSettingsForRenderPipeline(string renderpipelineName);

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001FC1 RID: 8129
		// (set) Token: 0x06001FC2 RID: 8130
		public static extern bool cameraRelativeLightCulling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001FC3 RID: 8131
		// (set) Token: 0x06001FC4 RID: 8132
		public static extern bool cameraRelativeShadowCulling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001FC5 RID: 8133
		[MethodImpl(4096)]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x06001FC6 RID: 8134
		[MethodImpl(4096)]
		private static extern void set_transparencySortAxis_Injected(ref Vector3 value);
	}
}
