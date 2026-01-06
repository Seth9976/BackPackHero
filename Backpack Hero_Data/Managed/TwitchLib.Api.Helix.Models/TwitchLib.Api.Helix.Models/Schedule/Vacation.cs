using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
	// Token: 0x0200003D RID: 61
	public class Vacation
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00003121 File Offset: 0x00001321
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00003129 File Offset: 0x00001329
		[JsonProperty("start_time")]
		public DateTime StartTime { get; protected set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000313A File Offset: 0x0000133A
		[JsonProperty("end_time")]
		public DateTime EndTime { get; protected set; }
	}
}
