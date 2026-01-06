using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonString : BsonValue
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00035DBF File Offset: 0x00033FBF
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00035DC7 File Offset: 0x00033FC7
		public int ByteCount { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00035DD0 File Offset: 0x00033FD0
		public bool IncludeLength { get; }

		// Token: 0x06000D93 RID: 3475 RVA: 0x00035DD8 File Offset: 0x00033FD8
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
