using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
	// Token: 0x020000B1 RID: 177
	public class GetCharityCampaignResponse
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000052D5 File Offset: 0x000034D5
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000052DD File Offset: 0x000034DD
		[JsonProperty(PropertyName = "data")]
		public CharityCampaignResponseModel[] Data { get; protected set; }
	}
}
