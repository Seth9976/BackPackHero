using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
	// Token: 0x020000D4 RID: 212
	public class UpdateExtensionBitsProductResponse
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00005D2F File Offset: 0x00003F2F
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00005D37 File Offset: 0x00003F37
		[JsonProperty(PropertyName = "data")]
		public ExtensionBitsProduct[] Data { get; protected set; }
	}
}
