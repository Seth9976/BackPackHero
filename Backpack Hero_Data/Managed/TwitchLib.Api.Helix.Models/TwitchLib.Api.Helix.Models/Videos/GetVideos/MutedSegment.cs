using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
	// Token: 0x02000003 RID: 3
	public class MutedSegment
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002082 File Offset: 0x00000282
		[JsonProperty(PropertyName = "duration")]
		public int Duration { get; protected set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002093 File Offset: 0x00000293
		[JsonProperty(PropertyName = "offset")]
		public int Offset { get; protected set; }
	}
}
