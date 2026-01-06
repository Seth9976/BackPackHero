using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BA RID: 186
	public class DefaultImage
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000055F8 File Offset: 0x000037F8
		[JsonProperty(PropertyName = "url_1x")]
		public string Url1x { get; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00005600 File Offset: 0x00003800
		[JsonProperty(PropertyName = "url_2x")]
		public string Url2x { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00005608 File Offset: 0x00003808
		[JsonProperty(PropertyName = "url_4x")]
		public string Url4x { get; }
	}
}
