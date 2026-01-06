using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
	// Token: 0x02000025 RID: 37
	public class UserMarkerListing
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00002A89 File Offset: 0x00000C89
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00002A91 File Offset: 0x00000C91
		[JsonProperty(PropertyName = "user_id")]
		public string UserId { get; protected set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00002A9A File Offset: 0x00000C9A
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00002AA2 File Offset: 0x00000CA2
		[JsonProperty(PropertyName = "username")]
		public string UserName { get; protected set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00002AAB File Offset: 0x00000CAB
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00002AB3 File Offset: 0x00000CB3
		[JsonProperty(PropertyName = "user_login")]
		public string UserLogin { get; protected set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00002AC4 File Offset: 0x00000CC4
		[JsonProperty(PropertyName = "videos")]
		public Video[] Videos { get; protected set; }
	}
}
