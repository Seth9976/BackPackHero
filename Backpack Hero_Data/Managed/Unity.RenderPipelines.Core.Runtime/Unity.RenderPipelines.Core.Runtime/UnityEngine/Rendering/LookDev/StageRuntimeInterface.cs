using System;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x020000EA RID: 234
	public class StageRuntimeInterface
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0001EC24 File Offset: 0x0001CE24
		public StageRuntimeInterface(Func<bool, GameObject> AddGameObject, Func<Camera> GetCamera, Func<Light> GetSunLight)
		{
			this.m_AddGameObject = AddGameObject;
			this.m_GetCamera = GetCamera;
			this.m_GetSunLight = GetSunLight;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001EC41 File Offset: 0x0001CE41
		public GameObject AddGameObject(bool persistent = false)
		{
			Func<bool, GameObject> addGameObject = this.m_AddGameObject;
			if (addGameObject == null)
			{
				return null;
			}
			return addGameObject(persistent);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001EC55 File Offset: 0x0001CE55
		public Camera camera
		{
			get
			{
				Func<Camera> getCamera = this.m_GetCamera;
				if (getCamera == null)
				{
					return null;
				}
				return getCamera();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001EC68 File Offset: 0x0001CE68
		public Light sunLight
		{
			get
			{
				Func<Light> getSunLight = this.m_GetSunLight;
				if (getSunLight == null)
				{
					return null;
				}
				return getSunLight();
			}
		}

		// Token: 0x040003CA RID: 970
		private Func<bool, GameObject> m_AddGameObject;

		// Token: 0x040003CB RID: 971
		private Func<Camera> m_GetCamera;

		// Token: 0x040003CC RID: 972
		private Func<Light> m_GetSunLight;

		// Token: 0x040003CD RID: 973
		public object SRPData;
	}
}
