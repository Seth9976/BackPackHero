using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
	// Token: 0x020000B7 RID: 183
	public class ChannelEditor
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00005448 File Offset: 0x00003648
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00005450 File Offset: 0x00003650
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00005459 File Offset: 0x00003659
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00005461 File Offset: 0x00003661
		[JsonProperty(PropertyName = "user_name")]
		public string UserName { get; protected set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000546A File Offset: 0x0000366A
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00005472 File Offset: 0x00003672
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }
	}
}
