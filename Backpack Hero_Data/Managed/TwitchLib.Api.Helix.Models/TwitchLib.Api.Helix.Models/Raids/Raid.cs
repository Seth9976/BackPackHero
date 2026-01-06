using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Raids
{
	// Token: 0x02000043 RID: 67
	public class Raid
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00003272 File Offset: 0x00001472
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000327A File Offset: 0x0000147A
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00003283 File Offset: 0x00001483
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000328B File Offset: 0x0000148B
		[JsonProperty(PropertyName = "is_mature")]
		public bool IsMature { get; protected set; }
	}
}
