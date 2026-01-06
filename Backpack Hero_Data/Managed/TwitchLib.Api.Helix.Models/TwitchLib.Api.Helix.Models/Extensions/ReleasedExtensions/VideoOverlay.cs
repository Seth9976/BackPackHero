using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x02000082 RID: 130
	public class VideoOverlay
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00004642 File Offset: 0x00002842
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000464A File Offset: 0x0000284A
		[JsonProperty(PropertyName = "viewer_url")]
		public string ViewerUrl { get; protected set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00004653 File Offset: 0x00002853
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000465B File Offset: 0x0000285B
		[JsonProperty(PropertyName = "can_link_external_content")]
		public bool CanLinkExternalContent { get; protected set; }
	}
}
