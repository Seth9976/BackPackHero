using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BF RID: 191
	public class Reward
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000056D1 File Offset: 0x000038D1
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000056D9 File Offset: 0x000038D9
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000056E2 File Offset: 0x000038E2
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x000056EA File Offset: 0x000038EA
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x000056F3 File Offset: 0x000038F3
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x000056FB File Offset: 0x000038FB
		[JsonProperty(PropertyName = "prompt")]
		public string Prompt { get; protected set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00005704 File Offset: 0x00003904
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0000570C File Offset: 0x0000390C
		[JsonProperty(PropertyName = "cost")]
		public int Cost { get; protected set; }
	}
}
