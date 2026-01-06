using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010F RID: 271
	internal enum BsonType : sbyte
	{
		// Token: 0x04000445 RID: 1093
		Number = 1,
		// Token: 0x04000446 RID: 1094
		String,
		// Token: 0x04000447 RID: 1095
		Object,
		// Token: 0x04000448 RID: 1096
		Array,
		// Token: 0x04000449 RID: 1097
		Binary,
		// Token: 0x0400044A RID: 1098
		Undefined,
		// Token: 0x0400044B RID: 1099
		Oid,
		// Token: 0x0400044C RID: 1100
		Boolean,
		// Token: 0x0400044D RID: 1101
		Date,
		// Token: 0x0400044E RID: 1102
		Null,
		// Token: 0x0400044F RID: 1103
		Regex,
		// Token: 0x04000450 RID: 1104
		Reference,
		// Token: 0x04000451 RID: 1105
		Code,
		// Token: 0x04000452 RID: 1106
		Symbol,
		// Token: 0x04000453 RID: 1107
		CodeWScope,
		// Token: 0x04000454 RID: 1108
		Integer,
		// Token: 0x04000455 RID: 1109
		TimeStamp,
		// Token: 0x04000456 RID: 1110
		Long,
		// Token: 0x04000457 RID: 1111
		MinKey = -1,
		// Token: 0x04000458 RID: 1112
		MaxKey = 127
	}
}
