using System;

namespace System.Net
{
	// Token: 0x020003F6 RID: 1014
	internal enum ContextAttribute
	{
		// Token: 0x04001215 RID: 4629
		Sizes,
		// Token: 0x04001216 RID: 4630
		Names,
		// Token: 0x04001217 RID: 4631
		Lifespan,
		// Token: 0x04001218 RID: 4632
		DceInfo,
		// Token: 0x04001219 RID: 4633
		StreamSizes,
		// Token: 0x0400121A RID: 4634
		Authority = 6,
		// Token: 0x0400121B RID: 4635
		PackageInfo = 10,
		// Token: 0x0400121C RID: 4636
		NegotiationInfo = 12,
		// Token: 0x0400121D RID: 4637
		UniqueBindings = 25,
		// Token: 0x0400121E RID: 4638
		EndpointBindings,
		// Token: 0x0400121F RID: 4639
		ClientSpecifiedSpn,
		// Token: 0x04001220 RID: 4640
		RemoteCertificate = 83,
		// Token: 0x04001221 RID: 4641
		LocalCertificate,
		// Token: 0x04001222 RID: 4642
		RootStore,
		// Token: 0x04001223 RID: 4643
		IssuerListInfoEx = 89,
		// Token: 0x04001224 RID: 4644
		ConnectionInfo,
		// Token: 0x04001225 RID: 4645
		UiInfo = 104
	}
}
