using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x0200001F RID: 31
	public class MaxPerStream
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006F82 File Offset: 0x00005182
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006F8A File Offset: 0x0000518A
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006F93 File Offset: 0x00005193
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006F9B File Offset: 0x0000519B
		[JsonProperty(PropertyName = "max_per_stream")]
		public int MaxPerStreamValue { get; protected set; }
	}
}
