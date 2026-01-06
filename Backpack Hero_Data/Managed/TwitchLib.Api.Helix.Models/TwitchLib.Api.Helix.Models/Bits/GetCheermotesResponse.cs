using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
	// Token: 0x020000CC RID: 204
	public class GetCheermotesResponse
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00005B46 File Offset: 0x00003D46
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00005B4E File Offset: 0x00003D4E
		[JsonProperty(PropertyName = "data")]
		public Cheermote[] Listings { get; protected set; }
	}
}
