using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
	// Token: 0x02000007 RID: 7
	public class ModLookupResponse
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000234D File Offset: 0x0000054D
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002355 File Offset: 0x00000555
		[JsonProperty(PropertyName = "status")]
		public int Status { get; protected set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000235E File Offset: 0x0000055E
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002366 File Offset: 0x00000566
		[JsonProperty(PropertyName = "user")]
		public string User { get; protected set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000236F File Offset: 0x0000056F
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002377 File Offset: 0x00000577
		[JsonProperty(PropertyName = "count")]
		public int Count { get; protected set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002380 File Offset: 0x00000580
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002388 File Offset: 0x00000588
		[JsonProperty(PropertyName = "channels")]
		public ModLookupListing[] Channels { get; protected set; }
	}
}
