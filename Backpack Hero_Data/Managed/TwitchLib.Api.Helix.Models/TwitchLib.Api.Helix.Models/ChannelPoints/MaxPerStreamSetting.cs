using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BD RID: 189
	public class MaxPerStreamSetting
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000567D File Offset: 0x0000387D
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00005685 File Offset: 0x00003885
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000568E File Offset: 0x0000388E
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00005696 File Offset: 0x00003896
		[JsonProperty(PropertyName = "max_per_stream")]
		public int MaxPerStream { get; protected set; }
	}
}
