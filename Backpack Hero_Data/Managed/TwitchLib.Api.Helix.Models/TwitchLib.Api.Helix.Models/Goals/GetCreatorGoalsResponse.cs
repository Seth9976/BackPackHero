using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Goals
{
	// Token: 0x02000074 RID: 116
	public class GetCreatorGoalsResponse
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000411B File Offset: 0x0000231B
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00004123 File Offset: 0x00002323
		[JsonProperty(PropertyName = "data")]
		public CreatorGoal[] Data { get; protected set; }
	}
}
