using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
	// Token: 0x02000042 RID: 66
	public class CreateChannelStreamSegmentResponse
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00003259 File Offset: 0x00001459
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00003261 File Offset: 0x00001461
		[JsonProperty("data")]
		public ChannelStreamSchedule Schedule { get; protected set; }
	}
}
