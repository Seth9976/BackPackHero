using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
	// Token: 0x020000D3 RID: 211
	public class GetExtensionBitsProductsResponse
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00005D16 File Offset: 0x00003F16
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00005D1E File Offset: 0x00003F1E
		[JsonProperty(PropertyName = "data")]
		public ExtensionBitsProduct[] Data { get; protected set; }
	}
}
