using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x0200001E RID: 30
	public class GlobalCooldown
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006F58 File Offset: 0x00005158
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00006F60 File Offset: 0x00005160
		[JsonProperty(PropertyName = "is_enabled")]
		public string IsEnabled { get; protected set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006F69 File Offset: 0x00005169
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00006F71 File Offset: 0x00005171
		[JsonProperty(PropertyName = "global_cooldown_seconds")]
		public int GlobalCooldownSeconds { get; protected set; }
	}
}
