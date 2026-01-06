using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.EndPrediction
{
	// Token: 0x02000049 RID: 73
	public class EndPredictionResponse
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000347E File Offset: 0x0000167E
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00003486 File Offset: 0x00001686
		[JsonProperty(PropertyName = "data")]
		public Prediction[] Data { get; protected set; }
	}
}
