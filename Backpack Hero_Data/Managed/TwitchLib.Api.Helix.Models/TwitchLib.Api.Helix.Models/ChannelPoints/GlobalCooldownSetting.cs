using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BB RID: 187
	public class GlobalCooldownSetting
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00005618 File Offset: 0x00003818
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00005620 File Offset: 0x00003820
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00005629 File Offset: 0x00003829
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00005631 File Offset: 0x00003831
		[JsonProperty(PropertyName = "global_cooldown_seconds")]
		public int GlobalCooldownSeconds { get; protected set; }
	}
}
