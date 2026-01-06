using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions
{
	// Token: 0x02000006 RID: 6
	public class UpdateUserExtensionsRequest
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000021E6 File Offset: 0x000003E6
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000021EE File Offset: 0x000003EE
		[JsonProperty(PropertyName = "panel")]
		public Dictionary<string, UserExtensionState> Panel { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000021F7 File Offset: 0x000003F7
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000021FF File Offset: 0x000003FF
		[JsonProperty(PropertyName = "component")]
		public Dictionary<string, UserExtensionState> Component { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002210 File Offset: 0x00000410
		[JsonProperty(PropertyName = "overlay")]
		public Dictionary<string, UserExtensionState> Overlay { get; set; }
	}
}
