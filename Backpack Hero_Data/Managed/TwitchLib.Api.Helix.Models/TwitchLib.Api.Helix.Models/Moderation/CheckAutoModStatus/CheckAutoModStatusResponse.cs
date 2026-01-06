using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
	// Token: 0x0200005F RID: 95
	public class CheckAutoModStatusResponse
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00003AF5 File Offset: 0x00001CF5
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00003AFD File Offset: 0x00001CFD
		[JsonProperty(PropertyName = "data")]
		public AutoModResult[] Data { get; protected set; }
	}
}
