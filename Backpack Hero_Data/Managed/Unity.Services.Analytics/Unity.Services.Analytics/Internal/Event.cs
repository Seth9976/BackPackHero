using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000037 RID: 55
	[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
	public class Event
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00004D32 File Offset: 0x00002F32
		public Event(string name, int? version)
		{
			this.Name = name;
			this.Version = version;
			this.Parameters = new EventData();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004D53 File Offset: 0x00002F53
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004D5B File Offset: 0x00002F5B
		public EventData Parameters { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004D64 File Offset: 0x00002F64
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00004D6C File Offset: 0x00002F6C
		public string Name { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004D75 File Offset: 0x00002F75
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004D7D File Offset: 0x00002F7D
		public int? Version { get; private set; }
	}
}
