using System;
using System.Collections.Generic;

namespace TwitchLib.PubSub.Models
{
	// Token: 0x02000004 RID: 4
	public class Outcome
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004FF5 File Offset: 0x000031F5
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00004FFD File Offset: 0x000031FD
		public Guid Id { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005006 File Offset: 0x00003206
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000500E File Offset: 0x0000320E
		public string Color { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005017 File Offset: 0x00003217
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000501F File Offset: 0x0000321F
		public string Title { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005028 File Offset: 0x00003228
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00005030 File Offset: 0x00003230
		public long TotalPoints { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005039 File Offset: 0x00003239
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00005041 File Offset: 0x00003241
		public long TotalUsers { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000504A File Offset: 0x0000324A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00005052 File Offset: 0x00003252
		public ICollection<Outcome.Predictor> TopPredictors { get; set; } = new List<Outcome.Predictor>();

		// Token: 0x02000063 RID: 99
		public class Predictor
		{
			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600025B RID: 603 RVA: 0x000075A4 File Offset: 0x000057A4
			// (set) Token: 0x0600025C RID: 604 RVA: 0x000075AC File Offset: 0x000057AC
			public long Points { get; set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600025D RID: 605 RVA: 0x000075B5 File Offset: 0x000057B5
			// (set) Token: 0x0600025E RID: 606 RVA: 0x000075BD File Offset: 0x000057BD
			public string UserId { get; set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600025F RID: 607 RVA: 0x000075C6 File Offset: 0x000057C6
			// (set) Token: 0x06000260 RID: 608 RVA: 0x000075CE File Offset: 0x000057CE
			public string DisplayName { get; set; }
		}
	}
}
