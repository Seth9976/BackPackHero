using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000016 RID: 22
	public class GetTeamsResponse
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002604 File Offset: 0x00000804
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000260C File Offset: 0x0000080C
		[JsonProperty(PropertyName = "data")]
		public Team[] Teams { get; protected set; }
	}
}
