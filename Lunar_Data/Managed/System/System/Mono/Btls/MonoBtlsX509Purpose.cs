using System;

namespace Mono.Btls
{
	// Token: 0x02000105 RID: 261
	internal enum MonoBtlsX509Purpose
	{
		// Token: 0x04000444 RID: 1092
		SSL_CLIENT = 1,
		// Token: 0x04000445 RID: 1093
		SSL_SERVER,
		// Token: 0x04000446 RID: 1094
		NS_SSL_SERVER,
		// Token: 0x04000447 RID: 1095
		SMIME_SIGN,
		// Token: 0x04000448 RID: 1096
		SMIME_ENCRYPT,
		// Token: 0x04000449 RID: 1097
		CRL_SIGN,
		// Token: 0x0400044A RID: 1098
		ANY,
		// Token: 0x0400044B RID: 1099
		OCSP_HELPER,
		// Token: 0x0400044C RID: 1100
		TIMESTAMP_SIGN
	}
}
