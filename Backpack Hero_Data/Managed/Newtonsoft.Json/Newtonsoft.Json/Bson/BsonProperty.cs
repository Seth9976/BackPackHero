using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010E RID: 270
	internal class BsonProperty
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00035E53 File Offset: 0x00034053
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00035E5B File Offset: 0x0003405B
		public BsonString Name { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00035E64 File Offset: 0x00034064
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00035E6C File Offset: 0x0003406C
		public BsonToken Value { get; set; }
	}
}
