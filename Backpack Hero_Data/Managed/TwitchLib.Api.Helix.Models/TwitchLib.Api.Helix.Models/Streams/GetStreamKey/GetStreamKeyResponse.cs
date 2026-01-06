using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
	// Token: 0x02000027 RID: 39
	public class GetStreamKeyResponse
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00002AFF File Offset: 0x00000CFF
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00002B07 File Offset: 0x00000D07
		[JsonProperty(PropertyName = "data")]
		public StreamKey[] Streams { get; protected set; }
	}
}
