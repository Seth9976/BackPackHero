using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents
{
	// Token: 0x0200005B RID: 91
	public class BannedEvent
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003981 File Offset: 0x00001B81
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00003989 File Offset: 0x00001B89
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00003992 File Offset: 0x00001B92
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000399A File Offset: 0x00001B9A
		[JsonProperty(PropertyName = "event_type")]
		public string EventType { get; protected set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000039A3 File Offset: 0x00001BA3
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000039AB File Offset: 0x00001BAB
		[JsonProperty(PropertyName = "event_timestamp")]
		public DateTime EventTimestamp { get; protected set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000039B4 File Offset: 0x00001BB4
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000039BC File Offset: 0x00001BBC
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000039C5 File Offset: 0x00001BC5
		// (set) Token: 0x06000301 RID: 769 RVA: 0x000039CD File Offset: 0x00001BCD
		[JsonProperty(PropertyName = "event_data")]
		public EventData EventData { get; protected set; }
	}
}
