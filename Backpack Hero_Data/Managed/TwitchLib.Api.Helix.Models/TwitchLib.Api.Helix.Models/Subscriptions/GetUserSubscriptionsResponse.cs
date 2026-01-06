using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
	// Token: 0x0200001D RID: 29
	public class GetUserSubscriptionsResponse
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000027A1 File Offset: 0x000009A1
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000027A9 File Offset: 0x000009A9
		[JsonProperty(PropertyName = "data")]
		public Subscription[] Data { get; protected set; }
	}
}
