using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
	// Token: 0x0200000C RID: 12
	public class CreatedFlow
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002485 File Offset: 0x00000685
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000248D File Offset: 0x0000068D
		[JsonProperty(PropertyName = "message")]
		public string Url { get; protected set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002496 File Offset: 0x00000696
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000249E File Offset: 0x0000069E
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }
	}
}
