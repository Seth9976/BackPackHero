using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
	// Token: 0x02000087 RID: 135
	public class EventSubSubscription
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000477A File Offset: 0x0000297A
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00004782 File Offset: 0x00002982
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000478B File Offset: 0x0000298B
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00004793 File Offset: 0x00002993
		[JsonProperty(PropertyName = "status")]
		public string Status { get; protected set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000479C File Offset: 0x0000299C
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000047A4 File Offset: 0x000029A4
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000047AD File Offset: 0x000029AD
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000047B5 File Offset: 0x000029B5
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000047BE File Offset: 0x000029BE
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x000047C6 File Offset: 0x000029C6
		[JsonProperty(PropertyName = "condition")]
		public Dictionary<string, string> Condition { get; protected set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000047CF File Offset: 0x000029CF
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000047D7 File Offset: 0x000029D7
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000047E0 File Offset: 0x000029E0
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000047E8 File Offset: 0x000029E8
		[JsonProperty(PropertyName = "transport")]
		public EventSubTransport Transport { get; protected set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000047F1 File Offset: 0x000029F1
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000047F9 File Offset: 0x000029F9
		[JsonProperty(PropertyName = "cost")]
		public int Cost { get; protected set; }
	}
}
