using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth
{
	// Token: 0x02000027 RID: 39
	public class RefreshResponse
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003E5C File Offset: 0x0000205C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003E64 File Offset: 0x00002064
		[JsonProperty(PropertyName = "access_token")]
		public string AccessToken { get; protected set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003E6D File Offset: 0x0000206D
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003E75 File Offset: 0x00002075
		[JsonProperty(PropertyName = "refresh_token")]
		public string RefreshToken { get; protected set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003E7E File Offset: 0x0000207E
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003E86 File Offset: 0x00002086
		[JsonProperty(PropertyName = "expires_in")]
		public int ExpiresIn { get; protected set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003E8F File Offset: 0x0000208F
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003E97 File Offset: 0x00002097
		[JsonProperty(PropertyName = "scope")]
		public string[] Scopes { get; protected set; }
	}
}
