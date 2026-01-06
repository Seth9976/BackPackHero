using System;

namespace System.IO
{
	// Token: 0x0200082F RID: 2095
	[Flags]
	internal enum EventFlags : ushort
	{
		// Token: 0x040027E7 RID: 10215
		Add = 1,
		// Token: 0x040027E8 RID: 10216
		Delete = 2,
		// Token: 0x040027E9 RID: 10217
		Enable = 4,
		// Token: 0x040027EA RID: 10218
		Disable = 8,
		// Token: 0x040027EB RID: 10219
		OneShot = 16,
		// Token: 0x040027EC RID: 10220
		Clear = 32,
		// Token: 0x040027ED RID: 10221
		Receipt = 64,
		// Token: 0x040027EE RID: 10222
		Dispatch = 128,
		// Token: 0x040027EF RID: 10223
		Flag0 = 4096,
		// Token: 0x040027F0 RID: 10224
		Flag1 = 8192,
		// Token: 0x040027F1 RID: 10225
		SystemFlags = 61440,
		// Token: 0x040027F2 RID: 10226
		EOF = 32768,
		// Token: 0x040027F3 RID: 10227
		Error = 16384
	}
}
