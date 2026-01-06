using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x0200007D RID: 125
	public class GetReleasedExtensionsResponse
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000440B File Offset: 0x0000260B
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00004413 File Offset: 0x00002613
		[JsonProperty(PropertyName = "data")]
		public ReleasedExtension[] Data { get; protected set; }
	}
}
