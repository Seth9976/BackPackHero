using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000056 RID: 86
	internal class DebugRenderSetup : IDisposable
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000144A2 File Offset: 0x000126A2
		private DebugDisplaySettingsMaterial MaterialSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.MaterialSettings;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000144B4 File Offset: 0x000126B4
		private DebugDisplaySettingsRendering RenderingSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.RenderingSettings;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000144C6 File Offset: 0x000126C6
		private DebugDisplaySettingsLighting LightingSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.LightingSettings;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000144D8 File Offset: 0x000126D8
		private void Begin()
		{
			DebugSceneOverrideMode debugSceneOverrideMode = this.RenderingSettings.debugSceneOverrideMode;
			if (debugSceneOverrideMode != DebugSceneOverrideMode.Wireframe)
			{
				if (debugSceneOverrideMode - DebugSceneOverrideMode.SolidWireframe <= 1)
				{
					if (this.m_Index == 1)
					{
						this.m_Context.Submit();
						GL.wireframe = true;
					}
				}
			}
			else
			{
				this.m_Context.Submit();
				GL.wireframe = true;
			}
			this.m_Context.ExecuteCommandBuffer(this.m_CommandBuffer);
			this.m_CommandBuffer.Clear();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00014550 File Offset: 0x00012750
		private void End()
		{
			DebugSceneOverrideMode debugSceneOverrideMode = this.RenderingSettings.debugSceneOverrideMode;
			if (debugSceneOverrideMode == DebugSceneOverrideMode.Wireframe)
			{
				this.m_Context.Submit();
				GL.wireframe = false;
				return;
			}
			if (debugSceneOverrideMode - DebugSceneOverrideMode.SolidWireframe > 1)
			{
				return;
			}
			if (this.m_Index == 1)
			{
				this.m_Context.Submit();
				GL.wireframe = false;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000145A6 File Offset: 0x000127A6
		internal DebugRenderSetup(DebugHandler debugHandler, ScriptableRenderContext context, CommandBuffer commandBuffer, int index)
		{
			this.m_DebugHandler = debugHandler;
			this.m_Context = context;
			this.m_CommandBuffer = commandBuffer;
			this.m_Index = index;
			this.Begin();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000145D4 File Offset: 0x000127D4
		internal DrawingSettings CreateDrawingSettings(DrawingSettings drawingSettings)
		{
			if (this.MaterialSettings.DebugVertexAttributeIndexData > DebugVertexAttributeMode.None)
			{
				Material replacementMaterial = this.m_DebugHandler.ReplacementMaterial;
				DrawingSettings drawingSettings2 = drawingSettings;
				drawingSettings2.overrideMaterial = replacementMaterial;
				drawingSettings2.overrideMaterialPassIndex = 0;
				return drawingSettings2;
			}
			return drawingSettings;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00014614 File Offset: 0x00012814
		internal RenderStateBlock GetRenderStateBlock(RenderStateBlock renderStateBlock)
		{
			DebugSceneOverrideMode debugSceneOverrideMode = this.RenderingSettings.debugSceneOverrideMode;
			if (debugSceneOverrideMode != DebugSceneOverrideMode.Overdraw)
			{
				if (debugSceneOverrideMode - DebugSceneOverrideMode.SolidWireframe <= 1)
				{
					if (this.m_Index == 1)
					{
						renderStateBlock.rasterState = new RasterState(CullMode.Back, -1, -1f, true);
						renderStateBlock.mask = RenderStateMask.Raster;
					}
				}
			}
			else
			{
				RenderTargetBlendState renderTargetBlendState = new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.One, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
				renderStateBlock.blendState = new BlendState
				{
					blendState0 = renderTargetBlendState
				};
				renderStateBlock.mask = RenderStateMask.Blend;
			}
			return renderStateBlock;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00014690 File Offset: 0x00012890
		public void Dispose()
		{
			this.End();
		}

		// Token: 0x04000266 RID: 614
		private readonly DebugHandler m_DebugHandler;

		// Token: 0x04000267 RID: 615
		private readonly ScriptableRenderContext m_Context;

		// Token: 0x04000268 RID: 616
		private readonly CommandBuffer m_CommandBuffer;

		// Token: 0x04000269 RID: 617
		private readonly int m_Index;
	}
}
