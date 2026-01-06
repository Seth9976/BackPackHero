using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
	// Token: 0x0200002F RID: 47
	public class Artist
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00002D0A File Offset: 0x00000F0A
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00002D12 File Offset: 0x00000F12
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00002D1B File Offset: 0x00000F1B
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00002D23 File Offset: 0x00000F23
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00002D2C File Offset: 0x00000F2C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00002D34 File Offset: 0x00000F34
		[JsonProperty(PropertyName = "creator_channel_id")]
		public string CreatorChannelId { get; protected set; }
	}
}
