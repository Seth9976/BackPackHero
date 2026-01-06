using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
	// Token: 0x0200005E RID: 94
	public class AutoModResult
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00003ACB File Offset: 0x00001CCB
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00003AD3 File Offset: 0x00001CD3
		[JsonProperty(PropertyName = "msg_id")]
		public string MsgId { get; protected set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00003ADC File Offset: 0x00001CDC
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[JsonProperty(PropertyName = "is_permitted")]
		public bool IsPermitted { get; protected set; }
	}
}
