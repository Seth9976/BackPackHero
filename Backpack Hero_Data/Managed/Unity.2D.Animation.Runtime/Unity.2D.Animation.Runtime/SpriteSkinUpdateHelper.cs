using System;
using Unity.Profiling;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000028 RID: 40
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-1)]
	[AddComponentMenu("")]
	internal class SpriteSkinUpdateHelper : MonoBehaviour
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004599 File Offset: 0x00002799
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000045A1 File Offset: 0x000027A1
		public Action<GameObject> onDestroyingComponent { get; set; }

		// Token: 0x060000EF RID: 239 RVA: 0x000045AA File Offset: 0x000027AA
		private void OnDestroy()
		{
			Action<GameObject> onDestroyingComponent = this.onDestroyingComponent;
			if (onDestroyingComponent == null)
			{
				return;
			}
			onDestroyingComponent(base.gameObject);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000026F3 File Offset: 0x000008F3
		private void LateUpdate()
		{
		}

		// Token: 0x04000060 RID: 96
		private ProfilerMarker m_ProfilerMarker = new ProfilerMarker("SpriteSkinUpdateHelper.LateUpdate");
	}
}
