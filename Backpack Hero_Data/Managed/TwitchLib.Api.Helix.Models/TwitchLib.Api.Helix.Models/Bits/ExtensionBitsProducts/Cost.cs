using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
	// Token: 0x020000D1 RID: 209
	public class Cost
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00005C7E File Offset: 0x00003E7E
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00005C86 File Offset: 0x00003E86
		[JsonProperty(PropertyName = "amount")]
		public int Amount { get; protected set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00005C8F File Offset: 0x00003E8F
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00005C97 File Offset: 0x00003E97
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }
	}
}
