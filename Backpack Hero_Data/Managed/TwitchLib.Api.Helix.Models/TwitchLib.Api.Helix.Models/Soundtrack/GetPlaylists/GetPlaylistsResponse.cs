using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists
{
	// Token: 0x02000033 RID: 51
	public class GetPlaylistsResponse
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002E4B File Offset: 0x0000104B
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002E53 File Offset: 0x00001053
		[JsonProperty(PropertyName = "data")]
		public PlaylistMetadata[] Data { get; protected set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002E5C File Offset: 0x0000105C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00002E64 File Offset: 0x00001064
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
