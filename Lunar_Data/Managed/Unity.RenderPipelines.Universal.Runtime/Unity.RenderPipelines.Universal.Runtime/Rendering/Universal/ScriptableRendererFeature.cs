using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B9 RID: 185
	[ExcludeFromPreset]
	public abstract class ScriptableRendererFeature : ScriptableObject, IDisposable
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001F6A2 File Offset: 0x0001D8A2
		public bool isActive
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x0600058E RID: 1422
		public abstract void Create();

		// Token: 0x0600058F RID: 1423 RVA: 0x0001F6AA File Offset: 0x0001D8AA
		public virtual void OnCameraPreCull(ScriptableRenderer renderer, in CameraData cameraData)
		{
		}

		// Token: 0x06000590 RID: 1424
		public abstract void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData);

		// Token: 0x06000591 RID: 1425 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		private void OnEnable()
		{
			this.Create();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		private void OnValidate()
		{
			this.Create();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001F6BC File Offset: 0x0001D8BC
		internal virtual bool SupportsNativeRenderPass()
		{
			return false;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001F6BF File Offset: 0x0001D8BF
		public void SetActive(bool active)
		{
			this.m_Active = active;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001F6D7 File Offset: 0x0001D8D7
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04000468 RID: 1128
		[SerializeField]
		[HideInInspector]
		private bool m_Active = true;
	}
}
