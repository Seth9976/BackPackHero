using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Core.Configuration.Internal
{
	// Token: 0x0200001F RID: 31
	[RequireImplementors]
	public interface IExternalUserId : IServiceComponent
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93
		string UserId { get; }

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600005E RID: 94
		// (remove) Token: 0x0600005F RID: 95
		event Action<string> UserIdChanged;
	}
}
