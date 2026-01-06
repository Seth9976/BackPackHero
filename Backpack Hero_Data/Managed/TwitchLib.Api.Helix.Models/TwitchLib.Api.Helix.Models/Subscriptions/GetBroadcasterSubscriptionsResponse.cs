using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
	// Token: 0x0200001C RID: 28
	public class GetBroadcasterSubscriptionsResponse
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002755 File Offset: 0x00000955
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000275D File Offset: 0x0000095D
		[JsonProperty(PropertyName = "data")]
		public Subscription[] Data { get; protected set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002766 File Offset: 0x00000966
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000276E File Offset: 0x0000096E
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002777 File Offset: 0x00000977
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000277F File Offset: 0x0000097F
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002788 File Offset: 0x00000988
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002790 File Offset: 0x00000990
		[JsonProperty(PropertyName = "points")]
		public int Points { get; protected set; }
	}
}
