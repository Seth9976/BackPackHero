using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000035 RID: 53
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	public abstract class ShadowCasterGroup2D : MonoBehaviour
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000110A4 File Offset: 0x0000F2A4
		internal virtual void CacheValues()
		{
			for (int i = 0; i < this.m_ShadowCasters.Count; i++)
			{
				this.m_ShadowCasters[i].CacheValues();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public List<ShadowCaster2D> GetShadowCasters()
		{
			return this.m_ShadowCasters;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public int GetShadowGroup()
		{
			return this.m_ShadowGroup;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000110E8 File Offset: 0x0000F2E8
		public void RegisterShadowCaster2D(ShadowCaster2D shadowCaster2D)
		{
			if (this.m_ShadowCasters == null)
			{
				this.m_ShadowCasters = new List<ShadowCaster2D>();
			}
			this.m_ShadowCasters.Add(shadowCaster2D);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00011109 File Offset: 0x0000F309
		public void UnregisterShadowCaster2D(ShadowCaster2D shadowCaster2D)
		{
			if (this.m_ShadowCasters != null)
			{
				this.m_ShadowCasters.Remove(shadowCaster2D);
			}
		}

		// Token: 0x04000172 RID: 370
		[SerializeField]
		internal int m_ShadowGroup;

		// Token: 0x04000173 RID: 371
		private List<ShadowCaster2D> m_ShadowCasters;
	}
}
