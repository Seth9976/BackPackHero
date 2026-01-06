using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BE RID: 190
	public class MaxPerUserPerStreamSetting
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000056A7 File Offset: 0x000038A7
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x000056AF File Offset: 0x000038AF
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000056B8 File Offset: 0x000038B8
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000056C0 File Offset: 0x000038C0
		[JsonProperty(PropertyName = "max_per_user_per_stream")]
		public int MaxPerUserPerStream { get; protected set; }
	}
}
