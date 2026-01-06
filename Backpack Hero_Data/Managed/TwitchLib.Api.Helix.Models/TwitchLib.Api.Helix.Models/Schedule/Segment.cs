using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
	// Token: 0x0200003C RID: 60
	public class Segment
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000030A2 File Offset: 0x000012A2
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000030AA File Offset: 0x000012AA
		[JsonProperty("id")]
		public string Id { get; protected set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000030B3 File Offset: 0x000012B3
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000030BB File Offset: 0x000012BB
		[JsonProperty("start_time")]
		public DateTime StartTime { get; protected set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000030C4 File Offset: 0x000012C4
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000030CC File Offset: 0x000012CC
		[JsonProperty("end_time")]
		public DateTime EndTime { get; protected set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000030D5 File Offset: 0x000012D5
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000030DD File Offset: 0x000012DD
		[JsonProperty("title")]
		public string Title { get; protected set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000030E6 File Offset: 0x000012E6
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000030EE File Offset: 0x000012EE
		[JsonProperty("canceled_until")]
		public DateTime? CanceledUntil { get; protected set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000030F7 File Offset: 0x000012F7
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000030FF File Offset: 0x000012FF
		[JsonProperty("category")]
		public Category Category { get; protected set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003108 File Offset: 0x00001308
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00003110 File Offset: 0x00001310
		[JsonProperty("is_recurring")]
		public bool IsRecurring { get; protected set; }
	}
}
