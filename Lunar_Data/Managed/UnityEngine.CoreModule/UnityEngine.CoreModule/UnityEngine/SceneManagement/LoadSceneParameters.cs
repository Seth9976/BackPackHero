using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E6 RID: 742
	[Serializable]
	public struct LoadSceneParameters
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00031528 File Offset: 0x0002F728
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x00031540 File Offset: 0x0002F740
		public LoadSceneMode loadSceneMode
		{
			get
			{
				return this.m_LoadSceneMode;
			}
			set
			{
				this.m_LoadSceneMode = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0003154C File Offset: 0x0002F74C
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x00031564 File Offset: 0x0002F764
		public LocalPhysicsMode localPhysicsMode
		{
			get
			{
				return this.m_LocalPhysicsMode;
			}
			set
			{
				this.m_LocalPhysicsMode = value;
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0003156E File Offset: 0x0002F76E
		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0003157F File Offset: 0x0002F77F
		public LoadSceneParameters(LoadSceneMode mode, LocalPhysicsMode physicsMode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x040009EB RID: 2539
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		// Token: 0x040009EC RID: 2540
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
