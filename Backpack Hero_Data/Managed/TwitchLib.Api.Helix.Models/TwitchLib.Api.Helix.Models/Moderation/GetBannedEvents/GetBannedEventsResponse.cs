using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents
{
	// Token: 0x0200005D RID: 93
	public class GetBannedEventsResponse
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00003AA1 File Offset: 0x00001CA1
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00003AA9 File Offset: 0x00001CA9
		[JsonProperty(PropertyName = "data")]
		public BannedEvent[] Data { get; protected set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00003AB2 File Offset: 0x00001CB2
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00003ABA File Offset: 0x00001CBA
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
