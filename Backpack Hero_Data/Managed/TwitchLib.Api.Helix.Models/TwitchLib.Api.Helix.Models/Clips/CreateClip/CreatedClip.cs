using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip
{
	// Token: 0x02000096 RID: 150
	public class CreatedClip
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00004BCC File Offset: 0x00002DCC
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00004BD4 File Offset: 0x00002DD4
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00004BDD File Offset: 0x00002DDD
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00004BE5 File Offset: 0x00002DE5
		[JsonProperty(PropertyName = "edit_url")]
		public string EditUrl { get; protected set; }
	}
}
