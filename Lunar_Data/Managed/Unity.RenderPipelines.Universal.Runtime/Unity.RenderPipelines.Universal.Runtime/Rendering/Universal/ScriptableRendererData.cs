using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B8 RID: 184
	public abstract class ScriptableRendererData : ScriptableObject
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001F5A3 File Offset: 0x0001D7A3
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0001F5AB File Offset: 0x0001D7AB
		internal bool isInvalidated { get; set; }

		// Token: 0x06000583 RID: 1411
		protected abstract ScriptableRenderer Create();

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
		public List<ScriptableRendererFeature> rendererFeatures
		{
			get
			{
				return this.m_RendererFeatures;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001F5BC File Offset: 0x0001D7BC
		public new void SetDirty()
		{
			this.isInvalidated = true;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001F5C5 File Offset: 0x0001D7C5
		internal ScriptableRenderer InternalCreateRenderer()
		{
			this.isInvalidated = false;
			return this.Create();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		protected virtual void OnValidate()
		{
			this.SetDirty();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		protected virtual void OnEnable()
		{
			this.SetDirty();
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		public bool useNativeRenderPass
		{
			get
			{
				return this.m_UseNativeRenderPass;
			}
			set
			{
				this.SetDirty();
				this.m_UseNativeRenderPass = value;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		internal bool TryGetRendererFeature<T>(out T rendererFeature) where T : ScriptableRendererFeature
		{
			foreach (ScriptableRendererFeature scriptableRendererFeature in this.rendererFeatures)
			{
				if (scriptableRendererFeature.GetType() == typeof(T))
				{
					rendererFeature = scriptableRendererFeature as T;
					return true;
				}
			}
			rendererFeature = default(T);
			return false;
		}

		// Token: 0x04000464 RID: 1124
		public ScriptableRendererData.DebugShaderResources debugShaders;

		// Token: 0x04000465 RID: 1125
		[SerializeField]
		internal List<ScriptableRendererFeature> m_RendererFeatures = new List<ScriptableRendererFeature>(10);

		// Token: 0x04000466 RID: 1126
		[SerializeField]
		internal List<long> m_RendererFeatureMap = new List<long>(10);

		// Token: 0x04000467 RID: 1127
		[SerializeField]
		private bool m_UseNativeRenderPass;

		// Token: 0x02000182 RID: 386
		[ReloadGroup]
		[Serializable]
		public sealed class DebugShaderResources
		{
			// Token: 0x040009E2 RID: 2530
			[Reload("Shaders/Debug/DebugReplacement.shader", ReloadAttribute.Package.Root)]
			public Shader debugReplacementPS;
		}
	}
}
