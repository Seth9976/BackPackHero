using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips
{
	// Token: 0x02000095 RID: 149
	public class GetClipsResponse
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00004BA2 File Offset: 0x00002DA2
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00004BAA File Offset: 0x00002DAA
		[JsonProperty(PropertyName = "data")]
		public Clip[] Clips { get; protected set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00004BB3 File Offset: 0x00002DB3
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00004BBB File Offset: 0x00002DBB
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
