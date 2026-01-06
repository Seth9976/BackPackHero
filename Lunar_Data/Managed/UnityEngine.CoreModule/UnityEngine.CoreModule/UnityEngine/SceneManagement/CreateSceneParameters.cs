using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E7 RID: 743
	[Serializable]
	public struct CreateSceneParameters
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00031590 File Offset: 0x0002F790
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x000315A8 File Offset: 0x0002F7A8
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

		// Token: 0x06001E72 RID: 7794 RVA: 0x000315A8 File Offset: 0x0002F7A8
		public CreateSceneParameters(LocalPhysicsMode physicsMode)
		{
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x040009ED RID: 2541
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
