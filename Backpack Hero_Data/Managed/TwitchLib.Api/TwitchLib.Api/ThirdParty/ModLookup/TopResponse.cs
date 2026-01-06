using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x0200000B RID: 11
	public class TopResponse
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000245B File Offset: 0x0000065B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002463 File Offset: 0x00000663
		[JsonProperty(PropertyName = "status")]
		public int Status { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000246C File Offset: 0x0000066C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002474 File Offset: 0x00000674
		[JsonProperty(PropertyName = "top")]
		public Top Top { get; protected set; }
	}
}
