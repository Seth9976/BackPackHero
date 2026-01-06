using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonBinary : BsonValue
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00035DE9 File Offset: 0x00033FE9
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00035DF1 File Offset: 0x00033FF1
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000D96 RID: 3478 RVA: 0x00035DFA File Offset: 0x00033FFA
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
