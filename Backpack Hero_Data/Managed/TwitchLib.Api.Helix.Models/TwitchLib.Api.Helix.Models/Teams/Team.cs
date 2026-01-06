using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000017 RID: 23
	public class Team : TeamBase
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002625 File Offset: 0x00000825
		[JsonProperty(PropertyName = "users")]
		public TeamMember[] Users { get; protected set; }
	}
}
