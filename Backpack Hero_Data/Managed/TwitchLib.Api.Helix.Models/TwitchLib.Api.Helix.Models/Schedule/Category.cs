using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
	// Token: 0x0200003A RID: 58
	public class Category
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000301B File Offset: 0x0000121B
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00003023 File Offset: 0x00001223
		[JsonProperty("id")]
		public string Id { get; protected set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00003034 File Offset: 0x00001234
		[JsonProperty("name")]
		public string Name { get; protected set; }
	}
}
