using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
	// Token: 0x02000026 RID: 38
	public class Video
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002AD5 File Offset: 0x00000CD5
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002ADD File Offset: 0x00000CDD
		[JsonProperty(PropertyName = "video_id")]
		public string VideoId { get; protected set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002AE6 File Offset: 0x00000CE6
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00002AEE File Offset: 0x00000CEE
		[JsonProperty(PropertyName = "markers")]
		public Marker[] Markers { get; protected set; }
	}
}
