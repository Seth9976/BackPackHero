using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
	// Token: 0x020000BC RID: 188
	public class Image
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00005642 File Offset: 0x00003842
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000564A File Offset: 0x0000384A
		[JsonProperty(PropertyName = "url_1x")]
		public string Url1x { get; protected set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00005653 File Offset: 0x00003853
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000565B File Offset: 0x0000385B
		[JsonProperty(PropertyName = "url_2x")]
		public string Url2x { get; protected set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00005664 File Offset: 0x00003864
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0000566C File Offset: 0x0000386C
		[JsonProperty(PropertyName = "url_4x")]
		public string Url4x { get; protected set; }
	}
}
