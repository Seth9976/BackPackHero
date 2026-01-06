using System;
using Unity.Profiling;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("")]
	[DefaultExecutionOrder(-1)]
	[ExecuteInEditMode]
	internal class SpriteSkinUpdateHelper : MonoBehaviour
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006114 File Offset: 0x00004314
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000611C File Offset: 0x0000431C
		public Action<GameObject> onDestroyingComponent { get; set; }

		// Token: 0x06000115 RID: 277 RVA: 0x00006125 File Offset: 0x00004325
		private void OnDestroy()
		{
			Action<GameObject> onDestroyingComponent = this.onDestroyingComponent;
			if (onDestroyingComponent == null)
			{
				return;
			}
			onDestroyingComponent(base.gameObject);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000613D File Offset: 0x0000433D
		private void LateUpdate()
		{
			if (SpriteSkinComposite.instance.helperGameObject != base.gameObject)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			SpriteSkinComposite.instance.LateUpdate();
		}

		// Token: 0x040000B9 RID: 185
		private ProfilerMarker m_ProfilerMarker = new ProfilerMarker("SpriteSkinUpdateHelper.LateUpdate");
	}
}
