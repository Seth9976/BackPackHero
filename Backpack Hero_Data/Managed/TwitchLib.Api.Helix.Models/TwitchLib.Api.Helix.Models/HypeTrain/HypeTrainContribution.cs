using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
	// Token: 0x02000070 RID: 112
	public class HypeTrainContribution
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00003D9D File Offset: 0x00001F9D
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00003DA5 File Offset: 0x00001FA5
		[JsonProperty(PropertyName = "total")]
		public int Total { get; protected set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00003DAE File Offset: 0x00001FAE
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00003DB6 File Offset: 0x00001FB6
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00003DBF File Offset: 0x00001FBF
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00003DC7 File Offset: 0x00001FC7
		[JsonProperty(PropertyName = "user")]
		public string UserId { get; protected set; }
	}
}
