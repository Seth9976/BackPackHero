using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
	// Token: 0x02000007 RID: 7
	public class ActiveExtensions
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002221 File Offset: 0x00000421
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002229 File Offset: 0x00000429
		[JsonProperty(PropertyName = "panel")]
		public Dictionary<string, UserActiveExtension> Panel { get; protected set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002232 File Offset: 0x00000432
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000223A File Offset: 0x0000043A
		[JsonProperty(PropertyName = "overlay")]
		public Dictionary<string, UserActiveExtension> Overlay { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000224B File Offset: 0x0000044B
		[JsonProperty(PropertyName = "component")]
		public Dictionary<string, UserActiveExtension> Component { get; protected set; }
	}
}
