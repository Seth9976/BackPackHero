using System;

namespace System.Net
{
	// Token: 0x020003F4 RID: 1012
	internal enum SecurityStatus
	{
		// Token: 0x040011E7 RID: 4583
		OK,
		// Token: 0x040011E8 RID: 4584
		ContinueNeeded = 590610,
		// Token: 0x040011E9 RID: 4585
		CompleteNeeded,
		// Token: 0x040011EA RID: 4586
		CompAndContinue,
		// Token: 0x040011EB RID: 4587
		ContextExpired = 590615,
		// Token: 0x040011EC RID: 4588
		CredentialsNeeded = 590624,
		// Token: 0x040011ED RID: 4589
		Renegotiate,
		// Token: 0x040011EE RID: 4590
		OutOfMemory = -2146893056,
		// Token: 0x040011EF RID: 4591
		InvalidHandle,
		// Token: 0x040011F0 RID: 4592
		Unsupported,
		// Token: 0x040011F1 RID: 4593
		TargetUnknown,
		// Token: 0x040011F2 RID: 4594
		InternalError,
		// Token: 0x040011F3 RID: 4595
		PackageNotFound,
		// Token: 0x040011F4 RID: 4596
		NotOwner,
		// Token: 0x040011F5 RID: 4597
		CannotInstall,
		// Token: 0x040011F6 RID: 4598
		InvalidToken,
		// Token: 0x040011F7 RID: 4599
		CannotPack,
		// Token: 0x040011F8 RID: 4600
		QopNotSupported,
		// Token: 0x040011F9 RID: 4601
		NoImpersonation,
		// Token: 0x040011FA RID: 4602
		LogonDenied,
		// Token: 0x040011FB RID: 4603
		UnknownCredentials,
		// Token: 0x040011FC RID: 4604
		NoCredentials,
		// Token: 0x040011FD RID: 4605
		MessageAltered,
		// Token: 0x040011FE RID: 4606
		OutOfSequence,
		// Token: 0x040011FF RID: 4607
		NoAuthenticatingAuthority,
		// Token: 0x04001200 RID: 4608
		IncompleteMessage = -2146893032,
		// Token: 0x04001201 RID: 4609
		IncompleteCredentials = -2146893024,
		// Token: 0x04001202 RID: 4610
		BufferNotEnough,
		// Token: 0x04001203 RID: 4611
		WrongPrincipal,
		// Token: 0x04001204 RID: 4612
		TimeSkew = -2146893020,
		// Token: 0x04001205 RID: 4613
		UntrustedRoot,
		// Token: 0x04001206 RID: 4614
		IllegalMessage,
		// Token: 0x04001207 RID: 4615
		CertUnknown,
		// Token: 0x04001208 RID: 4616
		CertExpired,
		// Token: 0x04001209 RID: 4617
		AlgorithmMismatch = -2146893007,
		// Token: 0x0400120A RID: 4618
		SecurityQosFailed,
		// Token: 0x0400120B RID: 4619
		SmartcardLogonRequired = -2146892994,
		// Token: 0x0400120C RID: 4620
		UnsupportedPreauth = -2146892989,
		// Token: 0x0400120D RID: 4621
		BadBinding = -2146892986
	}
}
