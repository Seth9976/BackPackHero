using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000078 RID: 120
	public abstract class ScriptableRenderPass
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x00017F96 File Offset: 0x00016196
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void FrameCleanup(CommandBuffer cmd)
		{
			this.OnCameraCleanup(cmd);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00017F9F File Offset: 0x0001619F
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00017FA7 File Offset: 0x000161A7
		public RenderPassEvent renderPassEvent { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00017FB0 File Offset: 0x000161B0
		public RenderTargetIdentifier[] colorAttachments
		{
			get
			{
				return this.m_ColorAttachments;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00017FB8 File Offset: 0x000161B8
		public RenderTargetIdentifier colorAttachment
		{
			get
			{
				return this.m_ColorAttachments[0];
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00017FC6 File Offset: 0x000161C6
		public RenderTargetIdentifier depthAttachment
		{
			get
			{
				return this.m_DepthAttachment;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00017FCE File Offset: 0x000161CE
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00017FD6 File Offset: 0x000161D6
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00017FDE File Offset: 0x000161DE
		internal bool[] overriddenColorStoreActions
		{
			get
			{
				return this.m_OverriddenColorStoreActions;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00017FE6 File Offset: 0x000161E6
		internal bool overriddenDepthStoreAction
		{
			get
			{
				return this.m_OverriddenDepthStoreAction;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00017FEE File Offset: 0x000161EE
		public ScriptableRenderPassInput input
		{
			get
			{
				return this.m_Input;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00017FF6 File Offset: 0x000161F6
		public ClearFlag clearFlag
		{
			get
			{
				return this.m_ClearFlag;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00017FFE File Offset: 0x000161FE
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00018006 File Offset: 0x00016206
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0001800E File Offset: 0x0001620E
		protected internal ProfilingSampler profilingSampler { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00018017 File Offset: 0x00016217
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0001801F File Offset: 0x0001621F
		internal bool overrideCameraTarget { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00018028 File Offset: 0x00016228
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00018030 File Offset: 0x00016230
		internal bool isBlitRenderPass { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00018039 File Offset: 0x00016239
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00018041 File Offset: 0x00016241
		internal bool useNativeRenderPass { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001804A File Offset: 0x0001624A
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00018052 File Offset: 0x00016252
		internal int renderTargetWidth { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001805B File Offset: 0x0001625B
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00018063 File Offset: 0x00016263
		internal int renderTargetHeight { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001806C File Offset: 0x0001626C
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00018074 File Offset: 0x00016274
		internal int renderTargetSampleCount { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001807D File Offset: 0x0001627D
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00018085 File Offset: 0x00016285
		internal bool depthOnly { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001808E File Offset: 0x0001628E
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00018096 File Offset: 0x00016296
		internal bool isLastPass { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001809F File Offset: 0x0001629F
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x000180A7 File Offset: 0x000162A7
		internal int renderPassQueueIndex { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000180B0 File Offset: 0x000162B0
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000180B8 File Offset: 0x000162B8
		internal GraphicsFormat[] renderTargetFormat { get; set; }

		// Token: 0x0600042A RID: 1066 RVA: 0x000180C4 File Offset: 0x000162C4
		internal DebugHandler GetActiveDebugHandler(RenderingData renderingData)
		{
			DebugHandler debugHandler = renderingData.cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(ref renderingData.cameraData))
			{
				return debugHandler;
			}
			return null;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000180F8 File Offset: 0x000162F8
		public ScriptableRenderPass()
		{
			this.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			this.m_ColorAttachments = new RenderTargetIdentifier[]
			{
				BuiltinRenderTextureType.CameraTarget,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			this.m_InputAttachments = new RenderTargetIdentifier[] { -1, -1, -1, -1, -1, -1, -1, -1 };
			this.m_InputAttachmentIsTransient = new bool[8];
			this.m_DepthAttachment = BuiltinRenderTextureType.CameraTarget;
			this.m_ColorStoreActions = new RenderBufferStoreAction[8];
			this.m_DepthStoreAction = RenderBufferStoreAction.Store;
			this.m_OverriddenColorStoreActions = new bool[8];
			this.m_OverriddenDepthStoreAction = false;
			this.m_ClearFlag = ClearFlag.None;
			this.m_ClearColor = Color.black;
			this.overrideCameraTarget = false;
			this.isBlitRenderPass = false;
			this.profilingSampler = new ProfilingSampler("Unnamed_ScriptableRenderPass");
			this.useNativeRenderPass = true;
			this.renderTargetWidth = -1;
			this.renderTargetHeight = -1;
			this.renderTargetSampleCount = -1;
			this.renderPassQueueIndex = -1;
			this.renderTargetFormat = new GraphicsFormat[8];
			this.depthOnly = false;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00018302 File Offset: 0x00016502
		public void ConfigureInput(ScriptableRenderPassInput passInput)
		{
			this.m_Input = passInput;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001830B File Offset: 0x0001650B
		public void ConfigureColorStoreAction(RenderBufferStoreAction storeAction, uint attachmentIndex = 0U)
		{
			this.m_ColorStoreActions[(int)attachmentIndex] = storeAction;
			this.m_OverriddenColorStoreActions[(int)attachmentIndex] = true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00018320 File Offset: 0x00016520
		public void ConfigureColorStoreActions(RenderBufferStoreAction[] storeActions)
		{
			int num = Math.Min(storeActions.Length, this.m_ColorStoreActions.Length);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				this.m_ColorStoreActions[(int)num2] = storeActions[(int)num2];
				this.m_OverriddenColorStoreActions[(int)num2] = true;
				num2 += 1U;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00018360 File Offset: 0x00016560
		public void ConfigureDepthStoreAction(RenderBufferStoreAction storeAction)
		{
			this.m_DepthStoreAction = storeAction;
			this.m_OverriddenDepthStoreAction = true;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00018370 File Offset: 0x00016570
		internal void ConfigureInputAttachments(RenderTargetIdentifier input, bool isTransient = false)
		{
			this.m_InputAttachments[0] = input;
			this.m_InputAttachmentIsTransient[0] = isTransient;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00018388 File Offset: 0x00016588
		internal void ConfigureInputAttachments(RenderTargetIdentifier[] inputs)
		{
			this.m_InputAttachments = inputs;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00018391 File Offset: 0x00016591
		internal void ConfigureInputAttachments(RenderTargetIdentifier[] inputs, bool[] isTransient)
		{
			this.ConfigureInputAttachments(inputs);
			this.m_InputAttachmentIsTransient = isTransient;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000183A1 File Offset: 0x000165A1
		internal void SetInputAttachmentTransient(int idx, bool isTransient)
		{
			this.m_InputAttachmentIsTransient[idx] = isTransient;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000183AC File Offset: 0x000165AC
		internal bool IsInputAttachmentTransient(int idx)
		{
			return this.m_InputAttachmentIsTransient[idx];
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000183B6 File Offset: 0x000165B6
		public void ConfigureTarget(RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment)
		{
			this.m_DepthAttachment = depthAttachment;
			this.ConfigureTarget(colorAttachment);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000183C6 File Offset: 0x000165C6
		internal void ConfigureTarget(RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment, GraphicsFormat format)
		{
			this.m_DepthAttachment = depthAttachment;
			this.ConfigureTarget(colorAttachment, format, -1, -1, -1, false);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000183DC File Offset: 0x000165DC
		public void ConfigureTarget(RenderTargetIdentifier[] colorAttachments, RenderTargetIdentifier depthAttachment)
		{
			this.overrideCameraTarget = true;
			uint validColorBufferCount = RenderingUtils.GetValidColorBufferCount(colorAttachments);
			if ((ulong)validColorBufferCount > (ulong)((long)SystemInfo.supportedRenderTargetCount))
			{
				Debug.LogError("Trying to set " + validColorBufferCount.ToString() + " renderTargets, which is more than the maximum supported:" + SystemInfo.supportedRenderTargetCount.ToString());
			}
			this.m_ColorAttachments = colorAttachments;
			this.m_DepthAttachment = depthAttachment;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00018438 File Offset: 0x00016638
		internal void ConfigureTarget(RenderTargetIdentifier[] colorAttachments, RenderTargetIdentifier depthAttachment, GraphicsFormat[] formats)
		{
			this.ConfigureTarget(colorAttachments, depthAttachment);
			for (int i = 0; i < formats.Length; i++)
			{
				this.renderTargetFormat[i] = formats[i];
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00018468 File Offset: 0x00016668
		public void ConfigureTarget(RenderTargetIdentifier colorAttachment)
		{
			this.overrideCameraTarget = true;
			this.m_ColorAttachments[0] = colorAttachment;
			for (int i = 1; i < this.m_ColorAttachments.Length; i++)
			{
				this.m_ColorAttachments[i] = 0;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000184B0 File Offset: 0x000166B0
		internal void ConfigureTarget(RenderTargetIdentifier colorAttachment, GraphicsFormat format, int width = -1, int height = -1, int sampleCount = -1, bool depth = false)
		{
			this.ConfigureTarget(colorAttachment);
			for (int i = 1; i < this.m_ColorAttachments.Length; i++)
			{
				this.renderTargetFormat[i] = GraphicsFormat.None;
			}
			if (depth && !GraphicsFormatUtility.IsDepthFormat(format))
			{
				throw new ArgumentException("When configuring a depth only target the passed in format must be a depth format.");
			}
			this.renderTargetWidth = width;
			this.renderTargetHeight = height;
			this.renderTargetSampleCount = sampleCount;
			this.depthOnly = depth;
			this.renderTargetFormat[0] = format;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001851F File Offset: 0x0001671F
		public void ConfigureTarget(RenderTargetIdentifier[] colorAttachments)
		{
			this.ConfigureTarget(colorAttachments, BuiltinRenderTextureType.CameraTarget);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001852E File Offset: 0x0001672E
		public void ConfigureClear(ClearFlag clearFlag, Color clearColor)
		{
			this.m_ClearFlag = clearFlag;
			this.m_ClearColor = clearColor;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001853E File Offset: 0x0001673E
		public virtual void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00018540 File Offset: 0x00016740
		public virtual void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00018542 File Offset: 0x00016742
		public virtual void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00018544 File Offset: 0x00016744
		public virtual void OnFinishCameraStackRendering(CommandBuffer cmd)
		{
		}

		// Token: 0x06000441 RID: 1089
		public abstract void Execute(ScriptableRenderContext context, ref RenderingData renderingData);

		// Token: 0x06000442 RID: 1090 RVA: 0x00018546 File Offset: 0x00016746
		public void Blit(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material = null, int passIndex = 0)
		{
			ScriptableRenderer.SetRenderTarget(cmd, destination, BuiltinRenderTextureType.CameraTarget, this.clearFlag, this.clearColor);
			cmd.Blit(source, destination, material, passIndex);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00018570 File Offset: 0x00016770
		public void Blit(CommandBuffer cmd, ref RenderingData data, Material material, int passIndex = 0)
		{
			ScriptableRenderer renderer = data.cameraData.renderer;
			this.Blit(cmd, renderer.cameraColorTarget, renderer.GetCameraColorFrontBuffer(cmd), material, passIndex);
			renderer.SwapColorBuffer(cmd);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000185A8 File Offset: 0x000167A8
		public DrawingSettings CreateDrawingSettings(ShaderTagId shaderTagId, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			Camera camera = renderingData.cameraData.camera;
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = sortingCriteria
			};
			return new DrawingSettings(shaderTagId, sortingSettings)
			{
				perObjectData = renderingData.perObjectData,
				mainLightIndex = renderingData.lightData.mainLightIndex,
				enableDynamicBatching = renderingData.supportsDynamicBatching,
				enableInstancing = (camera.cameraType != CameraType.Preview)
			};
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00018620 File Offset: 0x00016820
		public DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTagIdList, ref RenderingData renderingData, SortingCriteria sortingCriteria)
		{
			if (shaderTagIdList == null || shaderTagIdList.Count == 0)
			{
				Debug.LogWarning("ShaderTagId list is invalid. DrawingSettings is created with default pipeline ShaderTagId");
				return this.CreateDrawingSettings(new ShaderTagId("UniversalPipeline"), ref renderingData, sortingCriteria);
			}
			DrawingSettings drawingSettings = this.CreateDrawingSettings(shaderTagIdList[0], ref renderingData, sortingCriteria);
			for (int i = 1; i < shaderTagIdList.Count; i++)
			{
				drawingSettings.SetShaderPassName(i, shaderTagIdList[i]);
			}
			return drawingSettings;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018686 File Offset: 0x00016886
		public static bool operator <(ScriptableRenderPass lhs, ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent < rhs.renderPassEvent;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018696 File Offset: 0x00016896
		public static bool operator >(ScriptableRenderPass lhs, ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent > rhs.renderPassEvent;
		}

		// Token: 0x04000303 RID: 771
		private RenderBufferStoreAction[] m_ColorStoreActions = new RenderBufferStoreAction[1];

		// Token: 0x04000304 RID: 772
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x04000305 RID: 773
		private bool[] m_OverriddenColorStoreActions = new bool[1];

		// Token: 0x04000306 RID: 774
		private bool m_OverriddenDepthStoreAction;

		// Token: 0x04000311 RID: 785
		internal NativeArray<int> m_ColorAttachmentIndices;

		// Token: 0x04000312 RID: 786
		internal NativeArray<int> m_InputAttachmentIndices;

		// Token: 0x04000314 RID: 788
		private RenderTargetIdentifier[] m_ColorAttachments = new RenderTargetIdentifier[] { BuiltinRenderTextureType.CameraTarget };

		// Token: 0x04000315 RID: 789
		internal RenderTargetIdentifier[] m_InputAttachments = new RenderTargetIdentifier[8];

		// Token: 0x04000316 RID: 790
		internal bool[] m_InputAttachmentIsTransient = new bool[8];

		// Token: 0x04000317 RID: 791
		private RenderTargetIdentifier m_DepthAttachment = BuiltinRenderTextureType.CameraTarget;

		// Token: 0x04000318 RID: 792
		private ScriptableRenderPassInput m_Input;

		// Token: 0x04000319 RID: 793
		private ClearFlag m_ClearFlag;

		// Token: 0x0400031A RID: 794
		private Color m_ClearColor = Color.black;
	}
}
