using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CA RID: 202
	public class DateRange
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00005AE1 File Offset: 0x00003CE1
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00005AE9 File Offset: 0x00003CE9
		[JsonProperty(PropertyName = "started_at")]
		public DateTime StartedAt { get; protected set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00005AF2 File Offset: 0x00003CF2
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x00005AFA File Offset: 0x00003CFA
		[JsonProperty(PropertyName = "ended_at")]
		public DateTime EndedAt { get; protected set; }
	}
}
