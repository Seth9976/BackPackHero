using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModerators
{
	// Token: 0x02000055 RID: 85
	public class Moderator
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00003786 File Offset: 0x00001986
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000378E File Offset: 0x0000198E
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003797 File Offset: 0x00001997
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000379F File Offset: 0x0000199F
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000037A8 File Offset: 0x000019A8
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000037B0 File Offset: 0x000019B0
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }
	}
}
