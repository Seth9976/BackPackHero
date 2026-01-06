using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x02000083 RID: 131
	public class Views
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000466C File Offset: 0x0000286C
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00004674 File Offset: 0x00002874
		[JsonProperty(PropertyName = "mobile")]
		public Mobile Mobile { get; protected set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000467D File Offset: 0x0000287D
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00004685 File Offset: 0x00002885
		[JsonProperty(PropertyName = "panel")]
		public Panel Panel { get; protected set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000468E File Offset: 0x0000288E
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00004696 File Offset: 0x00002896
		[JsonProperty(PropertyName = "video_overlay")]
		public VideoOverlay VideoOverlay { get; protected set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000469F File Offset: 0x0000289F
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x000046A7 File Offset: 0x000028A7
		[JsonProperty(PropertyName = "component")]
		public Component Component { get; protected set; }
	}
}
