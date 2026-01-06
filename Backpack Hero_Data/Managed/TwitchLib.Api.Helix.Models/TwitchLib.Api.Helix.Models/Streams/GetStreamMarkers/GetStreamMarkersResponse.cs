using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
	// Token: 0x02000023 RID: 35
	public class GetStreamMarkersResponse
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00002A02 File Offset: 0x00000C02
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00002A0A File Offset: 0x00000C0A
		[JsonProperty(PropertyName = "data")]
		public UserMarkerListing[] Data { get; protected set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00002A13 File Offset: 0x00000C13
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00002A1B File Offset: 0x00000C1B
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
