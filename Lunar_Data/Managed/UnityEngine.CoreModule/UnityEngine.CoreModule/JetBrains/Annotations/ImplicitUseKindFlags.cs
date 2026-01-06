using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C3 RID: 195
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x0400024C RID: 588
		Default = 7,
		// Token: 0x0400024D RID: 589
		Access = 1,
		// Token: 0x0400024E RID: 590
		Assign = 2,
		// Token: 0x0400024F RID: 591
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000250 RID: 592
		InstantiatedNoFixedConstructorSignature = 8
	}
}
