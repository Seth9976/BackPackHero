using System;

namespace Mono.Btls
{
	// Token: 0x0200010D RID: 269
	internal enum MonoBtlsX509StoreType
	{
		// Token: 0x04000458 RID: 1112
		Custom,
		// Token: 0x04000459 RID: 1113
		MachineTrustedRoots,
		// Token: 0x0400045A RID: 1114
		MachineIntermediateCA,
		// Token: 0x0400045B RID: 1115
		MachineUntrusted,
		// Token: 0x0400045C RID: 1116
		UserTrustedRoots,
		// Token: 0x0400045D RID: 1117
		UserIntermediateCA,
		// Token: 0x0400045E RID: 1118
		UserUntrusted
	}
}
