using System;

namespace ES3Internal
{
	// Token: 0x020000DA RID: 218
	internal enum ES3SpecialByte : byte
	{
		// Token: 0x04000140 RID: 320
		Null,
		// Token: 0x04000141 RID: 321
		Bool,
		// Token: 0x04000142 RID: 322
		Byte,
		// Token: 0x04000143 RID: 323
		Sbyte,
		// Token: 0x04000144 RID: 324
		Char,
		// Token: 0x04000145 RID: 325
		Decimal,
		// Token: 0x04000146 RID: 326
		Double,
		// Token: 0x04000147 RID: 327
		Float,
		// Token: 0x04000148 RID: 328
		Int,
		// Token: 0x04000149 RID: 329
		Uint,
		// Token: 0x0400014A RID: 330
		Long,
		// Token: 0x0400014B RID: 331
		Ulong,
		// Token: 0x0400014C RID: 332
		Short,
		// Token: 0x0400014D RID: 333
		Ushort,
		// Token: 0x0400014E RID: 334
		String,
		// Token: 0x0400014F RID: 335
		ByteArray,
		// Token: 0x04000150 RID: 336
		Collection = 128,
		// Token: 0x04000151 RID: 337
		Dictionary,
		// Token: 0x04000152 RID: 338
		CollectionItem,
		// Token: 0x04000153 RID: 339
		Object = 254,
		// Token: 0x04000154 RID: 340
		Terminator
	}
}
