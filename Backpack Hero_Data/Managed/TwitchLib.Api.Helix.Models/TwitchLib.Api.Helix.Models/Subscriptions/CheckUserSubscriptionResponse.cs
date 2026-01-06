using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
	// Token: 0x0200001B RID: 27
	public class CheckUserSubscriptionResponse
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000273C File Offset: 0x0000093C
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002744 File Offset: 0x00000944
		[JsonProperty(PropertyName = "data")]
		public Subscription[] Data { get; protected set; }
	}
}
