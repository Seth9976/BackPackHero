using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
	// Token: 0x02000053 RID: 83
	public class CreatePollResponse
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003743 File Offset: 0x00001943
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000374B File Offset: 0x0000194B
		[JsonProperty(PropertyName = "data")]
		public Poll[] Data { get; protected set; }
	}
}
