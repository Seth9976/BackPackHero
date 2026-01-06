using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
	// Token: 0x0200004B RID: 75
	public class CreatePredictionResponse
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000034E3 File Offset: 0x000016E3
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000034EB File Offset: 0x000016EB
		[JsonProperty(PropertyName = "data")]
		public Prediction[] Data { get; protected set; }
	}
}
