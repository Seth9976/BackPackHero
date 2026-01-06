using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000402 RID: 1026
	public abstract class RenderPipeline
	{
		// Token: 0x060022E7 RID: 8935
		protected abstract void Render(ScriptableRenderContext context, Camera[] cameras);

		// Token: 0x060022E8 RID: 8936 RVA: 0x00004557 File Offset: 0x00002757
		protected virtual void ProcessRenderRequests(ScriptableRenderContext context, Camera camera, List<Camera.RenderRequest> renderRequests)
		{
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0003ABFD File Offset: 0x00038DFD
		protected static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0003AC0D File Offset: 0x00038E0D
		protected static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, cameras);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0003AC18 File Offset: 0x00038E18
		protected static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.BeginCameraRendering(context, camera);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0003AC23 File Offset: 0x00038E23
		protected static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.EndContextRendering(context, cameras);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0003AC2E File Offset: 0x00038E2E
		protected static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.EndContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0003AC3E File Offset: 0x00038E3E
		protected static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.EndCameraRendering(context, camera);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0003AC49 File Offset: 0x00038E49
		protected virtual void Render(ScriptableRenderContext context, List<Camera> cameras)
		{
			this.Render(context, cameras.ToArray());
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0003AC5C File Offset: 0x00038E5C
		internal void InternalRender(ScriptableRenderContext context, List<Camera> cameras)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.Render(context, cameras);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0003AC90 File Offset: 0x00038E90
		internal void InternalRenderWithRequests(ScriptableRenderContext context, List<Camera> cameras, List<Camera.RenderRequest> renderRequests)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.ProcessRenderRequests(context, (cameras == null || cameras.Count == 0) ? null : cameras[0], renderRequests);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0003ACD7 File Offset: 0x00038ED7
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x0003ACDF File Offset: 0x00038EDF
		public bool disposed { get; private set; }

		// Token: 0x060022F4 RID: 8948 RVA: 0x0003ACE8 File Offset: 0x00038EE8
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00004557 File Offset: 0x00002757
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0003AD04 File Offset: 0x00038F04
		public virtual RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return null;
			}
		}
	}
}
