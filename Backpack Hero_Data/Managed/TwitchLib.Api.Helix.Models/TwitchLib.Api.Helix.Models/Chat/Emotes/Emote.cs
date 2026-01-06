using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
	// Token: 0x0200009F RID: 159
	public abstract class Emote
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00004E27 File Offset: 0x00003027
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00004E2F File Offset: 0x0000302F
		[JsonProperty("id")]
		public string Id { get; protected set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00004E38 File Offset: 0x00003038
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00004E40 File Offset: 0x00003040
		[JsonProperty("name")]
		public string Name { get; protected set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00004E49 File Offset: 0x00003049
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00004E51 File Offset: 0x00003051
		[JsonProperty("images")]
		public EmoteImages Images { get; protected set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00004E5A File Offset: 0x0000305A
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00004E62 File Offset: 0x00003062
		[JsonProperty("format")]
		public string[] Format { get; protected set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00004E6B File Offset: 0x0000306B
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00004E73 File Offset: 0x00003073
		[JsonProperty("scale")]
		public string[] Scale { get; protected set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00004E7C File Offset: 0x0000307C
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00004E84 File Offset: 0x00003084
		[JsonProperty("theme_mode")]
		public string[] ThemeMode { get; protected set; }
	}
}
