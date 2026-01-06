using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x0200007E RID: 126
	public class IconUrls
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00004424 File Offset: 0x00002624
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000442C File Offset: 0x0000262C
		[JsonProperty(PropertyName = "100x100")]
		public string Size100x100 { get; protected set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00004435 File Offset: 0x00002635
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000443D File Offset: 0x0000263D
		[JsonProperty(PropertyName = "24x24")]
		public string Size24x24 { get; protected set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00004446 File Offset: 0x00002646
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000444E File Offset: 0x0000264E
		[JsonProperty(PropertyName = "300x200")]
		public string Size300x200 { get; protected set; }
	}
}
