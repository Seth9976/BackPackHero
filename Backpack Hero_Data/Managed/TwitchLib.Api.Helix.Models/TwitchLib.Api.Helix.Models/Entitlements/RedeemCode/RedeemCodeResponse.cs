using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Entitlements.RedeemCode
{
	// Token: 0x0200008D RID: 141
	public class RedeemCodeResponse
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00004920 File Offset: 0x00002B20
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00004928 File Offset: 0x00002B28
		[JsonProperty(PropertyName = "data")]
		public Status[] Data { get; protected set; }
	}
}
