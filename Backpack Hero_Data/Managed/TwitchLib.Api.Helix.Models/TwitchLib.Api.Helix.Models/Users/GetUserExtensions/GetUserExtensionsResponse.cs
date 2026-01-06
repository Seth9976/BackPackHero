using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserExtensions
{
	// Token: 0x02000010 RID: 16
	public class GetUserExtensionsResponse
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000252A File Offset: 0x0000072A
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002532 File Offset: 0x00000732
		[JsonProperty(PropertyName = "data")]
		public UserExtension[] Users { get; protected set; }
	}
}
