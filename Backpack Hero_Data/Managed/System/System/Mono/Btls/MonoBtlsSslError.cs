using System;

namespace Mono.Btls
{
	// Token: 0x020000ED RID: 237
	internal enum MonoBtlsSslError
	{
		// Token: 0x040003C2 RID: 962
		None,
		// Token: 0x040003C3 RID: 963
		Ssl,
		// Token: 0x040003C4 RID: 964
		WantRead,
		// Token: 0x040003C5 RID: 965
		WantWrite,
		// Token: 0x040003C6 RID: 966
		WantX509Lookup,
		// Token: 0x040003C7 RID: 967
		Syscall,
		// Token: 0x040003C8 RID: 968
		ZeroReturn,
		// Token: 0x040003C9 RID: 969
		WantConnect,
		// Token: 0x040003CA RID: 970
		WantAccept,
		// Token: 0x040003CB RID: 971
		WantChannelIdLookup,
		// Token: 0x040003CC RID: 972
		PendingSession = 11,
		// Token: 0x040003CD RID: 973
		PendingCertificate,
		// Token: 0x040003CE RID: 974
		WantPrivateKeyOperation
	}
}
