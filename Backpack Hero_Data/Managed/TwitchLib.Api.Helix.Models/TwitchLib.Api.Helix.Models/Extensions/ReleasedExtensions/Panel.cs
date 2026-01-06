using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x02000080 RID: 128
	public class Panel
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00004478 File Offset: 0x00002678
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00004480 File Offset: 0x00002680
		[JsonProperty(PropertyName = "viewer_url")]
		public string ViewerUrl { get; protected set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00004489 File Offset: 0x00002689
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00004491 File Offset: 0x00002691
		[JsonProperty(PropertyName = "height")]
		public int Height { get; protected set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000449A File Offset: 0x0000269A
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000044A2 File Offset: 0x000026A2
		[JsonProperty(PropertyName = "can_link_external_content")]
		public bool CanLinkExternalContent { get; protected set; }
	}
}
