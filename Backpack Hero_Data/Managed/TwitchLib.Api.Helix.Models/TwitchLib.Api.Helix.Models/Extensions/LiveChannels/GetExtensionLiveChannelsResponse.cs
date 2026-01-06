using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels
{
	// Token: 0x02000084 RID: 132
	public class GetExtensionLiveChannelsResponse
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000046C0 File Offset: 0x000028C0
		[JsonProperty(PropertyName = "data")]
		public LiveChannel[] Data { get; protected set; }
	}
}
