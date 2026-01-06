using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
	// Token: 0x02000041 RID: 65
	public class CreateChannelStreamSegmentRequest
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000031EB File Offset: 0x000013EB
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000031F3 File Offset: 0x000013F3
		[JsonProperty("start_time")]
		public DateTime StartTime { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000031FC File Offset: 0x000013FC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00003204 File Offset: 0x00001404
		[JsonProperty("timezone")]
		public string Timezone { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000320D File Offset: 0x0000140D
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00003215 File Offset: 0x00001415
		[JsonProperty("is_recurring")]
		public bool IsRecurring { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000321E File Offset: 0x0000141E
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00003226 File Offset: 0x00001426
		[JsonProperty("duration")]
		public string Duration { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000322F File Offset: 0x0000142F
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00003237 File Offset: 0x00001437
		[JsonProperty("category_id")]
		public string CategoryId { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00003240 File Offset: 0x00001440
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003248 File Offset: 0x00001448
		[JsonProperty("title")]
		public string Title { get; set; }
	}
}
