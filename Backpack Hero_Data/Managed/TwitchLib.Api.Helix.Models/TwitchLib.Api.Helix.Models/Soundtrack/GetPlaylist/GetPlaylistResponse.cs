using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylist
{
	// Token: 0x02000032 RID: 50
	public class GetPlaylistResponse
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002E21 File Offset: 0x00001021
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00002E29 File Offset: 0x00001029
		[JsonProperty(PropertyName = "data")]
		public Track[] Data { get; protected set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002E32 File Offset: 0x00001032
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00002E3A File Offset: 0x0000103A
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
