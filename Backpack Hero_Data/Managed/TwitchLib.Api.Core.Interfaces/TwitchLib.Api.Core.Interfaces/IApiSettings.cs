using System;
using System.Collections.Generic;
using System.ComponentModel;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000002 RID: 2
	public interface IApiSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		string AccessToken { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		string Secret { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		string ClientId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		bool SkipDynamicScopeValidation { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		bool SkipAutoServerTokenGeneration { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		List<AuthScopes> Scopes { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000D RID: 13
		// (remove) Token: 0x0600000E RID: 14
		event PropertyChangedEventHandler PropertyChanged;
	}
}
