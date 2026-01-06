using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
	// Token: 0x0200008A RID: 138
	public class Status
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000048B3 File Offset: 0x00002AB3
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x000048BB File Offset: 0x00002ABB
		[JsonProperty(PropertyName = "code")]
		public string Code { get; protected set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000048CC File Offset: 0x00002ACC
		[JsonProperty(PropertyName = "status")]
		public CodeStatusEnum StatusEnum { get; protected set; }
	}
}
