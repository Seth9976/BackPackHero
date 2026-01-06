using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BC RID: 188
	internal enum DnsRCode : ushort
	{
		// Token: 0x04000305 RID: 773
		NoError,
		// Token: 0x04000306 RID: 774
		FormErr,
		// Token: 0x04000307 RID: 775
		ServFail,
		// Token: 0x04000308 RID: 776
		NXDomain,
		// Token: 0x04000309 RID: 777
		NotImp,
		// Token: 0x0400030A RID: 778
		Refused,
		// Token: 0x0400030B RID: 779
		YXDomain,
		// Token: 0x0400030C RID: 780
		YXRRSet,
		// Token: 0x0400030D RID: 781
		NXRRSet,
		// Token: 0x0400030E RID: 782
		NotAuth,
		// Token: 0x0400030F RID: 783
		NotZone,
		// Token: 0x04000310 RID: 784
		BadVers = 16,
		// Token: 0x04000311 RID: 785
		BadSig = 16,
		// Token: 0x04000312 RID: 786
		BadKey,
		// Token: 0x04000313 RID: 787
		BadTime,
		// Token: 0x04000314 RID: 788
		BadMode,
		// Token: 0x04000315 RID: 789
		BadName,
		// Token: 0x04000316 RID: 790
		BadAlg,
		// Token: 0x04000317 RID: 791
		BadTrunc
	}
}
