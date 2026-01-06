using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamTags
{
	// Token: 0x0200001F RID: 31
	public class GetStreamTagsResponse
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002896 File Offset: 0x00000A96
		[JsonProperty(PropertyName = "data")]
		public Tag[] Data { get; protected set; }
	}
}
