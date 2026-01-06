using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Analytics
{
	// Token: 0x020000D5 RID: 213
	public class ExtensionAnalytics
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00005D48 File Offset: 0x00003F48
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00005D50 File Offset: 0x00003F50
		[JsonProperty(PropertyName = "extension_id")]
		public string ExtensionId { get; protected set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00005D59 File Offset: 0x00003F59
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00005D61 File Offset: 0x00003F61
		[JsonProperty(PropertyName = "URL")]
		public string Url { get; protected set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00005D6A File Offset: 0x00003F6A
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00005D72 File Offset: 0x00003F72
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00005D7B File Offset: 0x00003F7B
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00005D83 File Offset: 0x00003F83
		[JsonProperty(PropertyName = "date_range")]
		public DateRange DateRange { get; protected set; }
	}
}
