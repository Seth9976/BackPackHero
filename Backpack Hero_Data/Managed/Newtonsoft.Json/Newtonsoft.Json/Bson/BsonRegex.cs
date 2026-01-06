using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010D RID: 269
	internal class BsonRegex : BsonToken
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00035E0B File Offset: 0x0003400B
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00035E13 File Offset: 0x00034013
		public BsonString Pattern { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00035E1C File Offset: 0x0003401C
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00035E24 File Offset: 0x00034024
		public BsonString Options { get; set; }

		// Token: 0x06000D9B RID: 3483 RVA: 0x00035E2D File Offset: 0x0003402D
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00035E4F File Offset: 0x0003404F
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
