using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common
{
	// Token: 0x02000091 RID: 145
	public class DateRange
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000049FB File Offset: 0x00002BFB
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00004A03 File Offset: 0x00002C03
		[JsonProperty(PropertyName = "started_at")]
		public DateTime StartedAt { get; protected set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00004A0C File Offset: 0x00002C0C
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00004A14 File Offset: 0x00002C14
		[JsonProperty(PropertyName = "ended_at")]
		public DateTime EndedAt { get; protected set; }
	}
}
