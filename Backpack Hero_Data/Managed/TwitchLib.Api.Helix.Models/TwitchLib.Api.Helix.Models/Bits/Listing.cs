using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CF RID: 207
	public class Listing
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00005BB3 File Offset: 0x00003DB3
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00005BBB File Offset: 0x00003DBB
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00005BCC File Offset: 0x00003DCC
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00005BD5 File Offset: 0x00003DD5
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00005BDD File Offset: 0x00003DDD
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00005BEE File Offset: 0x00003DEE
		[JsonProperty(PropertyName = "rank")]
		public int Rank { get; protected set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00005BF7 File Offset: 0x00003DF7
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00005BFF File Offset: 0x00003DFF
		[JsonProperty(PropertyName = "score")]
		public int Score { get; protected set; }
	}
}
