using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth
{
	// Token: 0x02000028 RID: 40
	public class ValidateAccessTokenResponse
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003EA8 File Offset: 0x000020A8
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003EB0 File Offset: 0x000020B0
		[JsonProperty(PropertyName = "client_id")]
		public string ClientId { get; protected set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003EB9 File Offset: 0x000020B9
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003EC1 File Offset: 0x000020C1
		[JsonProperty(PropertyName = "login")]
		public string Login { get; protected set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003ECA File Offset: 0x000020CA
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003ED2 File Offset: 0x000020D2
		[JsonProperty(PropertyName = "scopes")]
		public List<string> Scopes { get; protected set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003EDB File Offset: 0x000020DB
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003EE3 File Offset: 0x000020E3
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003EEC File Offset: 0x000020EC
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003EF4 File Offset: 0x000020F4
		[JsonProperty(PropertyName = "expires_in")]
		public int ExpiresIn { get; protected set; }
	}
}
