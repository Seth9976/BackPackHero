using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack
{
	// Token: 0x0200002E RID: 46
	public class Album
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00002CCF File Offset: 0x00000ECF
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00002CD7 File Offset: 0x00000ED7
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00002CE8 File Offset: 0x00000EE8
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00002CF9 File Offset: 0x00000EF9
		[JsonProperty(PropertyName = "image_url")]
		public string ImageUrl { get; protected set; }
	}
}
