using System;

namespace Unity.Services.Core
{
	// Token: 0x02000004 RID: 4
	internal class ExternalUserIdProperty
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		public event Action<string> UserIdChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020F1 File Offset: 0x000002F1
		public string UserId
		{
			get
			{
				return this.m_UserId;
			}
			set
			{
				this.m_UserId = value;
				Action<string> userIdChanged = this.UserIdChanged;
				if (userIdChanged == null)
				{
					return;
				}
				userIdChanged(this.m_UserId);
			}
		}

		// Token: 0x0400000F RID: 15
		private string m_UserId;
	}
}
