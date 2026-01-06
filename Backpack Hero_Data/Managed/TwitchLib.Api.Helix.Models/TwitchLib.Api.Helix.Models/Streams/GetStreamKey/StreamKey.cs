using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
	// Token: 0x02000028 RID: 40
	public class StreamKey
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002B20 File Offset: 0x00000D20
		[JsonProperty(PropertyName = "stream_key")]
		public string Key { get; protected set; }
	}
}
