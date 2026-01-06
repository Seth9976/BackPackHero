using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CD RID: 205
	public class ImageList
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00005B5F File Offset: 0x00003D5F
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00005B67 File Offset: 0x00003D67
		[JsonProperty(PropertyName = "animated")]
		public Dictionary<string, string> Animated { get; protected set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00005B70 File Offset: 0x00003D70
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00005B78 File Offset: 0x00003D78
		[JsonProperty(PropertyName = "static")]
		public Dictionary<string, string> Static { get; protected set; }
	}
}
