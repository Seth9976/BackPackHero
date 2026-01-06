using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
	// Token: 0x02000034 RID: 52
	public class PlaylistMetadata
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00002E75 File Offset: 0x00001075
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00002E7D File Offset: 0x0000107D
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002E86 File Offset: 0x00001086
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00002E8E File Offset: 0x0000108E
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00002E97 File Offset: 0x00001097
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00002E9F File Offset: 0x0000109F
		[JsonProperty(PropertyName = "image_url")]
		public string ImageUrl { get; protected set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00002EB0 File Offset: 0x000010B0
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }
	}
}
