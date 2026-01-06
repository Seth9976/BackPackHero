using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
	// Token: 0x0200006C RID: 108
	public class GetAutomodSettingsResponse
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00003CE4 File Offset: 0x00001EE4
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00003CEC File Offset: 0x00001EEC
		[JsonProperty(PropertyName = "data")]
		public AutomodSettingsResponseModel[] Data { get; protected set; }
	}
}
