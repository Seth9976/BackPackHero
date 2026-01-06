using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack
{
	// Token: 0x02000035 RID: 53
	public class CurrentTrack
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00002EC1 File Offset: 0x000010C1
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00002EC9 File Offset: 0x000010C9
		[JsonProperty(PropertyName = "track")]
		public Track Track { get; protected set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00002ED2 File Offset: 0x000010D2
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00002EDA File Offset: 0x000010DA
		[JsonProperty(PropertyName = "source")]
		public Source Source { get; protected set; }
	}
}
