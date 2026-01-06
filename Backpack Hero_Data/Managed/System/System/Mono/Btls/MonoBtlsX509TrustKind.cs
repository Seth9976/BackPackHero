using System;

namespace Mono.Btls
{
	// Token: 0x0200010E RID: 270
	[Flags]
	internal enum MonoBtlsX509TrustKind
	{
		// Token: 0x04000460 RID: 1120
		DEFAULT = 0,
		// Token: 0x04000461 RID: 1121
		TRUST_CLIENT = 1,
		// Token: 0x04000462 RID: 1122
		TRUST_SERVER = 2,
		// Token: 0x04000463 RID: 1123
		TRUST_ALL = 4,
		// Token: 0x04000464 RID: 1124
		REJECT_CLIENT = 32,
		// Token: 0x04000465 RID: 1125
		REJECT_SERVER = 64,
		// Token: 0x04000466 RID: 1126
		REJECT_ALL = 128
	}
}
