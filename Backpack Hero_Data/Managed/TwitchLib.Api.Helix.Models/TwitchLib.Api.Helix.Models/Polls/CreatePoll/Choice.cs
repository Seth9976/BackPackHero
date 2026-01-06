using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
	// Token: 0x02000051 RID: 81
	public class Choice
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000369A File Offset: 0x0000189A
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000036A2 File Offset: 0x000018A2
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }
	}
}
