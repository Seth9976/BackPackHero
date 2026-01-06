using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C7 RID: 199
	internal enum ResolverError
	{
		// Token: 0x04000373 RID: 883
		NoError,
		// Token: 0x04000374 RID: 884
		FormatError,
		// Token: 0x04000375 RID: 885
		ServerFailure,
		// Token: 0x04000376 RID: 886
		NameError,
		// Token: 0x04000377 RID: 887
		NotImplemented,
		// Token: 0x04000378 RID: 888
		Refused,
		// Token: 0x04000379 RID: 889
		ResponseHeaderError,
		// Token: 0x0400037A RID: 890
		ResponseFormatError,
		// Token: 0x0400037B RID: 891
		Timeout
	}
}
