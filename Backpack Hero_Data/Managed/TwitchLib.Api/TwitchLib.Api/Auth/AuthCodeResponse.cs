using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth
{
	// Token: 0x02000026 RID: 38
	public class AuthCodeResponse
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003DFF File Offset: 0x00001FFF
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003E07 File Offset: 0x00002007
		[JsonProperty(PropertyName = "access_token")]
		public string AccessToken { get; protected set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003E10 File Offset: 0x00002010
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003E18 File Offset: 0x00002018
		[JsonProperty(PropertyName = "refresh_token")]
		public string RefreshToken { get; protected set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003E21 File Offset: 0x00002021
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003E29 File Offset: 0x00002029
		[JsonProperty(PropertyName = "expires_in")]
		public int ExpiresIn { get; protected set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003E32 File Offset: 0x00002032
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003E3A File Offset: 0x0000203A
		[JsonProperty(PropertyName = "scope")]
		public string[] Scopes { get; protected set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003E43 File Offset: 0x00002043
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003E4B File Offset: 0x0000204B
		[JsonProperty(PropertyName = "token_type")]
		public string TokenType { get; set; }
	}
}
