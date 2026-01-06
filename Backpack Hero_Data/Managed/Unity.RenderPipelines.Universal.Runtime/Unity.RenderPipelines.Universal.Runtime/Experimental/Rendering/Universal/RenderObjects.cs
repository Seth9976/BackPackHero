using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experimental.Rendering.Universal
{
	// Token: 0x02000005 RID: 5
	[ExcludeFromPreset]
	[Tooltip("Render Objects simplifies the injection of additional render passes by exposing a selection of commonly used settings.")]
	public class RenderObjects : ScriptableRendererFeature
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000029A0 File Offset: 0x00000BA0
		public override void Create()
		{
			RenderObjects.FilterSettings filterSettings = this.settings.filterSettings;
			if (this.settings.Event < RenderPassEvent.BeforeRenderingPrePasses)
			{
				this.settings.Event = RenderPassEvent.BeforeRenderingPrePasses;
			}
			this.renderObjectsPass = new RenderObjectsPass(this.settings.passTag, this.settings.Event, filterSettings.PassNames, filterSettings.RenderQueueType, filterSettings.LayerMask, this.settings.cameraSettings);
			this.renderObjectsPass.overrideMaterial = this.settings.overrideMaterial;
			this.renderObjectsPass.overrideMaterialPassIndex = this.settings.overrideMaterialPassIndex;
			if (this.settings.overrideDepthState)
			{
				this.renderObjectsPass.SetDetphState(this.settings.enableWrite, this.settings.depthCompareFunction);
			}
			if (this.settings.stencilSettings.overrideStencilState)
			{
				this.renderObjectsPass.SetStencilState(this.settings.stencilSettings.stencilReference, this.settings.stencilSettings.stencilCompareFunction, this.settings.stencilSettings.passOperation, this.settings.stencilSettings.failOperation, this.settings.stencilSettings.zFailOperation);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			renderer.EnqueuePass(this.renderObjectsPass);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AF3 File Offset: 0x00000CF3
		internal override bool SupportsNativeRenderPass()
		{
			return this.settings.Event <= RenderPassEvent.BeforeRenderingPostProcessing;
		}

		// Token: 0x04000015 RID: 21
		public RenderObjects.RenderObjectsSettings settings = new RenderObjects.RenderObjectsSettings();

		// Token: 0x04000016 RID: 22
		private RenderObjectsPass renderObjectsPass;

		// Token: 0x02000136 RID: 310
		[Serializable]
		public class RenderObjectsSettings
		{
			// Token: 0x04000873 RID: 2163
			public string passTag = "RenderObjectsFeature";

			// Token: 0x04000874 RID: 2164
			public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

			// Token: 0x04000875 RID: 2165
			public RenderObjects.FilterSettings filterSettings = new RenderObjects.FilterSettings();

			// Token: 0x04000876 RID: 2166
			public Material overrideMaterial;

			// Token: 0x04000877 RID: 2167
			public int overrideMaterialPassIndex;

			// Token: 0x04000878 RID: 2168
			public bool overrideDepthState;

			// Token: 0x04000879 RID: 2169
			public CompareFunction depthCompareFunction = CompareFunction.LessEqual;

			// Token: 0x0400087A RID: 2170
			public bool enableWrite = true;

			// Token: 0x0400087B RID: 2171
			public StencilStateData stencilSettings = new StencilStateData();

			// Token: 0x0400087C RID: 2172
			public RenderObjects.CustomCameraSettings cameraSettings = new RenderObjects.CustomCameraSettings();
		}

		// Token: 0x02000137 RID: 311
		[Serializable]
		public class FilterSettings
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x0003E328 File Offset: 0x0003C528
			public FilterSettings()
			{
				this.RenderQueueType = RenderQueueType.Opaque;
				this.LayerMask = 0;
			}

			// Token: 0x0400087D RID: 2173
			public RenderQueueType RenderQueueType;

			// Token: 0x0400087E RID: 2174
			public LayerMask LayerMask;

			// Token: 0x0400087F RID: 2175
			public string[] PassNames;
		}

		// Token: 0x02000138 RID: 312
		[Serializable]
		public class CustomCameraSettings
		{
			// Token: 0x04000880 RID: 2176
			public bool overrideCamera;

			// Token: 0x04000881 RID: 2177
			public bool restoreCamera = true;

			// Token: 0x04000882 RID: 2178
			public Vector4 offset;

			// Token: 0x04000883 RID: 2179
			public float cameraFieldOfView = 60f;
		}
	}
}
