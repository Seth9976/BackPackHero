using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule
{
	// Token: 0x02000040 RID: 64
	public class GetChannelStreamScheduleResponse
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000031C1 File Offset: 0x000013C1
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000031C9 File Offset: 0x000013C9
		[JsonProperty("data")]
		public ChannelStreamSchedule Schedule { get; protected set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000031D2 File Offset: 0x000013D2
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000031DA File Offset: 0x000013DA
		[JsonProperty("pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
