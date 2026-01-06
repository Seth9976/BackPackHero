using System;

namespace System.Net
{
	// Token: 0x0200037E RID: 894
	internal enum SecurityStatusPalErrorCode
	{
		// Token: 0x04000F14 RID: 3860
		NotSet,
		// Token: 0x04000F15 RID: 3861
		OK,
		// Token: 0x04000F16 RID: 3862
		ContinueNeeded,
		// Token: 0x04000F17 RID: 3863
		CompleteNeeded,
		// Token: 0x04000F18 RID: 3864
		CompAndContinue,
		// Token: 0x04000F19 RID: 3865
		ContextExpired,
		// Token: 0x04000F1A RID: 3866
		CredentialsNeeded,
		// Token: 0x04000F1B RID: 3867
		Renegotiate,
		// Token: 0x04000F1C RID: 3868
		OutOfMemory,
		// Token: 0x04000F1D RID: 3869
		InvalidHandle,
		// Token: 0x04000F1E RID: 3870
		Unsupported,
		// Token: 0x04000F1F RID: 3871
		TargetUnknown,
		// Token: 0x04000F20 RID: 3872
		InternalError,
		// Token: 0x04000F21 RID: 3873
		PackageNotFound,
		// Token: 0x04000F22 RID: 3874
		NotOwner,
		// Token: 0x04000F23 RID: 3875
		CannotInstall,
		// Token: 0x04000F24 RID: 3876
		InvalidToken,
		// Token: 0x04000F25 RID: 3877
		CannotPack,
		// Token: 0x04000F26 RID: 3878
		QopNotSupported,
		// Token: 0x04000F27 RID: 3879
		NoImpersonation,
		// Token: 0x04000F28 RID: 3880
		LogonDenied,
		// Token: 0x04000F29 RID: 3881
		UnknownCredentials,
		// Token: 0x04000F2A RID: 3882
		NoCredentials,
		// Token: 0x04000F2B RID: 3883
		MessageAltered,
		// Token: 0x04000F2C RID: 3884
		OutOfSequence,
		// Token: 0x04000F2D RID: 3885
		NoAuthenticatingAuthority,
		// Token: 0x04000F2E RID: 3886
		IncompleteMessage,
		// Token: 0x04000F2F RID: 3887
		IncompleteCredentials,
		// Token: 0x04000F30 RID: 3888
		BufferNotEnough,
		// Token: 0x04000F31 RID: 3889
		WrongPrincipal,
		// Token: 0x04000F32 RID: 3890
		TimeSkew,
		// Token: 0x04000F33 RID: 3891
		UntrustedRoot,
		// Token: 0x04000F34 RID: 3892
		IllegalMessage,
		// Token: 0x04000F35 RID: 3893
		CertUnknown,
		// Token: 0x04000F36 RID: 3894
		CertExpired,
		// Token: 0x04000F37 RID: 3895
		AlgorithmMismatch,
		// Token: 0x04000F38 RID: 3896
		SecurityQosFailed,
		// Token: 0x04000F39 RID: 3897
		SmartcardLogonRequired,
		// Token: 0x04000F3A RID: 3898
		UnsupportedPreauth,
		// Token: 0x04000F3B RID: 3899
		BadBinding,
		// Token: 0x04000F3C RID: 3900
		DowngradeDetected,
		// Token: 0x04000F3D RID: 3901
		ApplicationProtocolMismatch
	}
}
