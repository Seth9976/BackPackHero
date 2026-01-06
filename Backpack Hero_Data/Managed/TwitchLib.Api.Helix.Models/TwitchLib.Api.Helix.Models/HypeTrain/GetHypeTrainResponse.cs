using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
	// Token: 0x0200006E RID: 110
	public class GetHypeTrainResponse
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00003D16 File Offset: 0x00001F16
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00003D1E File Offset: 0x00001F1E
		[JsonProperty(PropertyName = "data")]
		public HypeTrain[] HypeTrain { get; protected set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00003D27 File Offset: 0x00001F27
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00003D2F File Offset: 0x00001F2F
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
