using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
	// Token: 0x0200004C RID: 76
	public class Outcome
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000034FC File Offset: 0x000016FC
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00003504 File Offset: 0x00001704
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }
	}
}
