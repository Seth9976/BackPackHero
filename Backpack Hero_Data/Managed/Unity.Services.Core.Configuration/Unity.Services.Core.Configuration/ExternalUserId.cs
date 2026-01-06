using System;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000006 RID: 6
	internal class ExternalUserId : IExternalUserId, IServiceComponent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021C3 File Offset: 0x000003C3
		public string UserId
		{
			get
			{
				return UnityServices.ExternalUserIdProperty.UserId;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x000021CF File Offset: 0x000003CF
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x000021DC File Offset: 0x000003DC
		public event Action<string> UserIdChanged
		{
			add
			{
				UnityServices.ExternalUserIdProperty.UserIdChanged += value;
			}
			remove
			{
				UnityServices.ExternalUserIdProperty.UserIdChanged -= value;
			}
		}
	}
}
