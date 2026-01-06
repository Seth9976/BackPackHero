using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
	// Token: 0x0200003E RID: 62
	public class UpdateChannelStreamSegmentRequest
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000314B File Offset: 0x0000134B
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00003153 File Offset: 0x00001353
		[JsonProperty("start_time")]
		public DateTime StartTime { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00003164 File Offset: 0x00001364
		[JsonProperty("duration")]
		public string Duration { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000316D File Offset: 0x0000136D
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00003175 File Offset: 0x00001375
		[JsonProperty("category_id")]
		public string CategoryId { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000317E File Offset: 0x0000137E
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00003186 File Offset: 0x00001386
		[JsonProperty("is_canceled")]
		public bool IsCanceled { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000318F File Offset: 0x0000138F
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00003197 File Offset: 0x00001397
		[JsonProperty("timezone")]
		public string Timezone { get; set; }
	}
}
