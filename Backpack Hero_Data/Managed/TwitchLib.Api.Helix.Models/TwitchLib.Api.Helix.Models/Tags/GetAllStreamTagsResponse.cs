using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Tags
{
	// Token: 0x0200001A RID: 26
	public class GetAllStreamTagsResponse
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000271A File Offset: 0x0000091A
		[JsonProperty(PropertyName = "data")]
		public Tag[] Data { get; protected set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002723 File Offset: 0x00000923
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000272B File Offset: 0x0000092B
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
