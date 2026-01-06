using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
	// Token: 0x0200003F RID: 63
	public class UpdateChannelStreamSegmentResponse
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000031A8 File Offset: 0x000013A8
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000031B0 File Offset: 0x000013B0
		[JsonProperty("data")]
		public ChannelStreamSchedule Schedule { get; protected set; }
	}
}
