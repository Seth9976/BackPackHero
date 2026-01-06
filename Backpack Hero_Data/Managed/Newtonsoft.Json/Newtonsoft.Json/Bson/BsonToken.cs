using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000105 RID: 261
	internal abstract class BsonToken
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D78 RID: 3448
		public abstract BsonType Type { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00035C76 File Offset: 0x00033E76
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00035C7E File Offset: 0x00033E7E
		public BsonToken Parent { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00035C87 File Offset: 0x00033E87
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x00035C8F File Offset: 0x00033E8F
		public int CalculatedSize { get; set; }
	}
}
