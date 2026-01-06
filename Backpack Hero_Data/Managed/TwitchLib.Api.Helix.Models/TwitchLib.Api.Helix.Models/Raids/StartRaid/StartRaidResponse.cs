using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Raids.StartRaid
{
	// Token: 0x02000044 RID: 68
	public class StartRaidResponse
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000329C File Offset: 0x0000149C
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000032A4 File Offset: 0x000014A4
		[JsonProperty(PropertyName = "data")]
		public Raid[] Data { get; protected set; }
	}
}
