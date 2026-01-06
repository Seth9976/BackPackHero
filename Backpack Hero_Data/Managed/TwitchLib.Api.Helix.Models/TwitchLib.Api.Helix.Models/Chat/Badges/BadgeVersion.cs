using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges
{
	// Token: 0x020000AC RID: 172
	public class BadgeVersion
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000516A File Offset: 0x0000336A
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00005172 File Offset: 0x00003372
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000517B File Offset: 0x0000337B
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00005183 File Offset: 0x00003383
		[JsonProperty(PropertyName = "image_url_1x")]
		public string ImageUrl1x { get; protected set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000518C File Offset: 0x0000338C
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00005194 File Offset: 0x00003394
		[JsonProperty(PropertyName = "image_url_2x")]
		public string ImageUrl2x { get; protected set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000519D File Offset: 0x0000339D
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x000051A5 File Offset: 0x000033A5
		[JsonProperty(PropertyName = "image_url_4x")]
		public string ImageUrl4x { get; protected set; }
	}
}
