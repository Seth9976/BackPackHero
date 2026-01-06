using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
	// Token: 0x0200001E RID: 30
	public class Subscription
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000027BA File Offset: 0x000009BA
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000027C2 File Offset: 0x000009C2
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000027CB File Offset: 0x000009CB
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000027D3 File Offset: 0x000009D3
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000027DC File Offset: 0x000009DC
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000027E4 File Offset: 0x000009E4
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000027ED File Offset: 0x000009ED
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000027F5 File Offset: 0x000009F5
		[JsonProperty(PropertyName = "is_gift")]
		public bool IsGift { get; protected set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000027FE File Offset: 0x000009FE
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002806 File Offset: 0x00000A06
		[JsonProperty(PropertyName = "tier")]
		public string Tier { get; protected set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002817 File Offset: 0x00000A17
		[JsonProperty(PropertyName = "plan_name")]
		public string PlanName { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002820 File Offset: 0x00000A20
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002828 File Offset: 0x00000A28
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002831 File Offset: 0x00000A31
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002839 File Offset: 0x00000A39
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002842 File Offset: 0x00000A42
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000284A File Offset: 0x00000A4A
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002853 File Offset: 0x00000A53
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000285B File Offset: 0x00000A5B
		[JsonProperty(PropertyName = "gifter_id")]
		public string GiftertId { get; protected set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002864 File Offset: 0x00000A64
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000286C File Offset: 0x00000A6C
		[JsonProperty(PropertyName = "gifter_name")]
		public string GifterName { get; protected set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002875 File Offset: 0x00000A75
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000287D File Offset: 0x00000A7D
		[JsonProperty(PropertyName = "gifter_login")]
		public string GifterLogin { get; protected set; }
	}
}
