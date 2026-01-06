using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Predictions.GetPredictions
{
	// Token: 0x02000048 RID: 72
	public class GetPredictionsResponse
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00003454 File Offset: 0x00001654
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000345C File Offset: 0x0000165C
		[JsonProperty(PropertyName = "data")]
		public Prediction[] Data { get; protected set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00003465 File Offset: 0x00001665
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000346D File Offset: 0x0000166D
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
