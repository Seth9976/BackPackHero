using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetFollowedStreams
{
	// Token: 0x02000029 RID: 41
	public class GetFollowedStreamsResponse
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002B31 File Offset: 0x00000D31
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002B39 File Offset: 0x00000D39
		[JsonProperty(PropertyName = "data")]
		public Stream[] Data { get; protected set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002B42 File Offset: 0x00000D42
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00002B4A File Offset: 0x00000D4A
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
