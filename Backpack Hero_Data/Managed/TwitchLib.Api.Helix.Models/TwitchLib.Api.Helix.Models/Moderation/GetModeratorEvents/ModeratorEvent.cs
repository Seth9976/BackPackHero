using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents
{
	// Token: 0x02000058 RID: 88
	public class ModeratorEvent
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003859 File Offset: 0x00001A59
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00003861 File Offset: 0x00001A61
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000386A File Offset: 0x00001A6A
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003872 File Offset: 0x00001A72
		[JsonProperty(PropertyName = "event_type")]
		public string EventType { get; protected set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000387B File Offset: 0x00001A7B
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00003883 File Offset: 0x00001A83
		[JsonProperty(PropertyName = "event_timestamp")]
		public DateTime EventTimestamp { get; protected set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000388C File Offset: 0x00001A8C
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00003894 File Offset: 0x00001A94
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000389D File Offset: 0x00001A9D
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000038A5 File Offset: 0x00001AA5
		[JsonProperty(PropertyName = "event_data")]
		public EventData EventData { get; protected set; }
	}
}
