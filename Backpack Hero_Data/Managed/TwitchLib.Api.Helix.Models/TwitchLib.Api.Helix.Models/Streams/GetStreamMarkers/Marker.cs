using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
	// Token: 0x02000024 RID: 36
	public class Marker
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002A34 File Offset: 0x00000C34
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00002A3D File Offset: 0x00000C3D
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00002A45 File Offset: 0x00000C45
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; protected set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002A4E File Offset: 0x00000C4E
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00002A56 File Offset: 0x00000C56
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00002A5F File Offset: 0x00000C5F
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00002A67 File Offset: 0x00000C67
		[JsonProperty(PropertyName = "position_seconds")]
		public int PositionSeconds { get; protected set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00002A70 File Offset: 0x00000C70
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00002A78 File Offset: 0x00000C78
		[JsonProperty(PropertyName = "URL")]
		public string Url { get; protected set; }
	}
}
