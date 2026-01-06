using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents
{
	// Token: 0x02000057 RID: 87
	public class GetModeratorEventsResponse
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000382F File Offset: 0x00001A2F
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00003837 File Offset: 0x00001A37
		[JsonProperty(PropertyName = "data")]
		public ModeratorEvent[] Data { get; protected set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00003840 File Offset: 0x00001A40
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00003848 File Offset: 0x00001A48
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
