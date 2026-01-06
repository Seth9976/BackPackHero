using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
	// Token: 0x02000068 RID: 104
	public class BanUserResponse
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00003CB3 File Offset: 0x00001EB3
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00003CBB File Offset: 0x00001EBB
		[JsonProperty(PropertyName = "data")]
		public BannedUser[] Data { get; protected set; }
	}
}
