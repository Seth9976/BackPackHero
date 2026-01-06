using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x0200007F RID: 127
	public class Mobile
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000445F File Offset: 0x0000265F
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00004467 File Offset: 0x00002667
		[JsonProperty(PropertyName = "viewer_url")]
		public string ViewerUrl { get; protected set; }
	}
}
