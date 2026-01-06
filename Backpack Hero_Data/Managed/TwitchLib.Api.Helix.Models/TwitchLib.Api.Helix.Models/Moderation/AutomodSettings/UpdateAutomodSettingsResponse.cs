using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
	// Token: 0x0200006D RID: 109
	public class UpdateAutomodSettingsResponse
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00003CFD File Offset: 0x00001EFD
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00003D05 File Offset: 0x00001F05
		[JsonProperty(PropertyName = "data")]
		public AutomodSettings[] Data { get; protected set; }
	}
}
