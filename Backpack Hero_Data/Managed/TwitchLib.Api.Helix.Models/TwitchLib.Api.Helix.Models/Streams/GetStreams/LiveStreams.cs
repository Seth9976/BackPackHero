using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
	// Token: 0x02000021 RID: 33
	public class LiveStreams
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000028D1 File Offset: 0x00000AD1
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000028D9 File Offset: 0x00000AD9
		[JsonProperty(PropertyName = "_total")]
		public int Total { get; protected set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000028E2 File Offset: 0x00000AE2
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000028EA File Offset: 0x00000AEA
		[JsonProperty(PropertyName = "streams")]
		public Stream[] Streams { get; protected set; }
	}
}
