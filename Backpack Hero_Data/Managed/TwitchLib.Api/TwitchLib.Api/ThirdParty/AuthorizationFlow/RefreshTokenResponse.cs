using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
	// Token: 0x0200000E RID: 14
	public class RefreshTokenResponse
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D16 File Offset: 0x00000F16
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002D1E File Offset: 0x00000F1E
		[JsonProperty(PropertyName = "token")]
		public string Token { get; protected set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D27 File Offset: 0x00000F27
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002D2F File Offset: 0x00000F2F
		[JsonProperty(PropertyName = "refresh")]
		public string Refresh { get; protected set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D38 File Offset: 0x00000F38
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002D40 File Offset: 0x00000F40
		[JsonProperty(PropertyName = "client_id")]
		public string ClientId { get; protected set; }
	}
}
