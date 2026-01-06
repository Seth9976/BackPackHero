using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CE RID: 206
	public class Images
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00005B89 File Offset: 0x00003D89
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00005B91 File Offset: 0x00003D91
		[JsonProperty(PropertyName = "dark")]
		public ImageList Dark { get; protected set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00005B9A File Offset: 0x00003D9A
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00005BA2 File Offset: 0x00003DA2
		[JsonProperty(PropertyName = "light")]
		public ImageList Light { get; protected set; }
	}
}
