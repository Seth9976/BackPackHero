using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004D RID: 77
	public abstract class AbstractEventData
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017D01 File Offset: 0x00015F01
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00017D0A File Offset: 0x00015F0A
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x040001AD RID: 429
		protected bool m_Used;
	}
}
