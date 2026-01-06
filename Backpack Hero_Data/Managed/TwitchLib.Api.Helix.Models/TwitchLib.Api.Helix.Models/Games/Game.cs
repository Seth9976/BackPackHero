using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Games
{
	// Token: 0x02000075 RID: 117
	public class Game
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000413C File Offset: 0x0000233C
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00004145 File Offset: 0x00002345
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000414D File Offset: 0x0000234D
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00004156 File Offset: 0x00002356
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000415E File Offset: 0x0000235E
		[JsonProperty(PropertyName = "box_art_url")]
		public string BoxArtUrl { get; protected set; }
	}
}
