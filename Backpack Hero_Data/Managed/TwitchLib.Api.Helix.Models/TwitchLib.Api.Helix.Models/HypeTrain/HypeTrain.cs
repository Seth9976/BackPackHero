using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
	// Token: 0x0200006F RID: 111
	public class HypeTrain
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00003D40 File Offset: 0x00001F40
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00003D48 File Offset: 0x00001F48
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00003D51 File Offset: 0x00001F51
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00003D59 File Offset: 0x00001F59
		[JsonProperty(PropertyName = "event_type")]
		public string EventType { get; protected set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00003D62 File Offset: 0x00001F62
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00003D6A File Offset: 0x00001F6A
		[JsonProperty(PropertyName = "event_timestamp")]
		public string EventTimeStamp { get; protected set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00003D73 File Offset: 0x00001F73
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00003D7B File Offset: 0x00001F7B
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00003D84 File Offset: 0x00001F84
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00003D8C File Offset: 0x00001F8C
		[JsonProperty(PropertyName = "event_data")]
		public HypeTrainEventData EventData { get; protected set; }
	}
}
