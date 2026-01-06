using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000108 RID: 264
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00035D42 File Offset: 0x00033F42
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00035D51 File Offset: 0x00033F51
		public override BsonType Type { get; }

		// Token: 0x04000436 RID: 1078
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x04000437 RID: 1079
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
