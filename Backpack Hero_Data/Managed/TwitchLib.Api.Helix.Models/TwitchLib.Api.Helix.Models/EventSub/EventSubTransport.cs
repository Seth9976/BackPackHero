using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
	// Token: 0x02000088 RID: 136
	public class EventSubTransport
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000480A File Offset: 0x00002A0A
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00004812 File Offset: 0x00002A12
		[JsonProperty(PropertyName = "method")]
		public string Method { get; protected set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000481B File Offset: 0x00002A1B
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00004823 File Offset: 0x00002A23
		[JsonProperty(PropertyName = "callback")]
		public string Callback { get; protected set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000482C File Offset: 0x00002A2C
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00004834 File Offset: 0x00002A34
		[JsonProperty(PropertyName = "created_at")]
		public DateTime? CreatedAt { get; protected set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000483D File Offset: 0x00002A3D
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00004845 File Offset: 0x00002A45
		[JsonProperty(PropertyName = "disconnected_at")]
		public DateTime? DisconnectedAt { get; protected set; }
	}
}
