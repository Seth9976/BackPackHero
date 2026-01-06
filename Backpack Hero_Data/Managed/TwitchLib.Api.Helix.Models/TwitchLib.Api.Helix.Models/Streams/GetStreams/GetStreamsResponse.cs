using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
	// Token: 0x02000020 RID: 32
	public class GetStreamsResponse
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000028A7 File Offset: 0x00000AA7
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000028AF File Offset: 0x00000AAF
		[JsonProperty(PropertyName = "data")]
		public Stream[] Streams { get; protected set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000028B8 File Offset: 0x00000AB8
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000028C0 File Offset: 0x00000AC0
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
