using System;

namespace System.Net
{
	// Token: 0x02000372 RID: 882
	[Flags]
	internal enum ContextFlagsPal
	{
		// Token: 0x04000ECC RID: 3788
		None = 0,
		// Token: 0x04000ECD RID: 3789
		Delegate = 1,
		// Token: 0x04000ECE RID: 3790
		MutualAuth = 2,
		// Token: 0x04000ECF RID: 3791
		ReplayDetect = 4,
		// Token: 0x04000ED0 RID: 3792
		SequenceDetect = 8,
		// Token: 0x04000ED1 RID: 3793
		Confidentiality = 16,
		// Token: 0x04000ED2 RID: 3794
		UseSessionKey = 32,
		// Token: 0x04000ED3 RID: 3795
		AllocateMemory = 256,
		// Token: 0x04000ED4 RID: 3796
		Connection = 2048,
		// Token: 0x04000ED5 RID: 3797
		InitExtendedError = 16384,
		// Token: 0x04000ED6 RID: 3798
		AcceptExtendedError = 32768,
		// Token: 0x04000ED7 RID: 3799
		InitStream = 32768,
		// Token: 0x04000ED8 RID: 3800
		AcceptStream = 65536,
		// Token: 0x04000ED9 RID: 3801
		InitIntegrity = 65536,
		// Token: 0x04000EDA RID: 3802
		AcceptIntegrity = 131072,
		// Token: 0x04000EDB RID: 3803
		InitManualCredValidation = 524288,
		// Token: 0x04000EDC RID: 3804
		InitUseSuppliedCreds = 128,
		// Token: 0x04000EDD RID: 3805
		InitIdentify = 131072,
		// Token: 0x04000EDE RID: 3806
		AcceptIdentify = 524288,
		// Token: 0x04000EDF RID: 3807
		ProxyBindings = 67108864,
		// Token: 0x04000EE0 RID: 3808
		AllowMissingBindings = 268435456,
		// Token: 0x04000EE1 RID: 3809
		UnverifiedTargetName = 536870912
	}
}
