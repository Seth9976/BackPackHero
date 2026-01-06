using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModerators
{
	// Token: 0x02000054 RID: 84
	public class GetModeratorsResponse
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00003764 File Offset: 0x00001964
		[JsonProperty(PropertyName = "data")]
		public Moderator[] Data { get; protected set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000376D File Offset: 0x0000196D
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00003775 File Offset: 0x00001975
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
