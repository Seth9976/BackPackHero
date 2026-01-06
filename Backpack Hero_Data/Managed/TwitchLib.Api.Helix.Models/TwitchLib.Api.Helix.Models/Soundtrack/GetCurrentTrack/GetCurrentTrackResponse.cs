using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
	// Token: 0x02000036 RID: 54
	public class GetCurrentTrackResponse
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00002EEB File Offset: 0x000010EB
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00002EF3 File Offset: 0x000010F3
		[JsonProperty(PropertyName = "data")]
		public CurrentTrack[] Data { get; protected set; }
	}
}
