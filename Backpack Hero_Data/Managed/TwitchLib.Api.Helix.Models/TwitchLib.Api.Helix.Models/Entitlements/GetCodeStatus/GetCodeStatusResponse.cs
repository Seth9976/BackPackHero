using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetCodeStatus
{
	// Token: 0x02000090 RID: 144
	public class GetCodeStatusResponse
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000049E2 File Offset: 0x00002BE2
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000049EA File Offset: 0x00002BEA
		[JsonProperty(PropertyName = "data")]
		public Status[] Data { get; protected set; }
	}
}
