using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents
{
	// Token: 0x02000056 RID: 86
	public class EventData
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000037C1 File Offset: 0x000019C1
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000037C9 File Offset: 0x000019C9
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; protected set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000037D2 File Offset: 0x000019D2
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000037DA File Offset: 0x000019DA
		[JsonProperty(PropertyName = "broadcaster_login")]
		public string BroadcasterLogin { get; protected set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000037E3 File Offset: 0x000019E3
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x000037EB File Offset: 0x000019EB
		[JsonProperty(PropertyName = "broadcaster_name")]
		public string BroadcasterName { get; protected set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000037F4 File Offset: 0x000019F4
		// (set) Token: 0x060002CA RID: 714 RVA: 0x000037FC File Offset: 0x000019FC
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00003805 File Offset: 0x00001A05
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000380D File Offset: 0x00001A0D
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00003816 File Offset: 0x00001A16
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000381E File Offset: 0x00001A1E
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }
	}
}
