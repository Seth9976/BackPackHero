using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.EndPoll
{
	// Token: 0x02000050 RID: 80
	public class EndPollResponse
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00003681 File Offset: 0x00001881
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00003689 File Offset: 0x00001889
		[JsonProperty(PropertyName = "data")]
		public Poll[] Data { get; protected set; }
	}
}
