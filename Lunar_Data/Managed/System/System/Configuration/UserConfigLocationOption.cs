using System;

namespace System.Configuration
{
	// Token: 0x020001AB RID: 427
	internal enum UserConfigLocationOption : uint
	{
		// Token: 0x04000758 RID: 1880
		Product = 32U,
		// Token: 0x04000759 RID: 1881
		Product_VersionMajor,
		// Token: 0x0400075A RID: 1882
		Product_VersionMinor,
		// Token: 0x0400075B RID: 1883
		Product_VersionBuild = 36U,
		// Token: 0x0400075C RID: 1884
		Product_VersionRevision = 40U,
		// Token: 0x0400075D RID: 1885
		Company_Product = 48U,
		// Token: 0x0400075E RID: 1886
		Company_Product_VersionMajor,
		// Token: 0x0400075F RID: 1887
		Company_Product_VersionMinor,
		// Token: 0x04000760 RID: 1888
		Company_Product_VersionBuild = 52U,
		// Token: 0x04000761 RID: 1889
		Company_Product_VersionRevision = 56U,
		// Token: 0x04000762 RID: 1890
		Evidence = 64U,
		// Token: 0x04000763 RID: 1891
		Other = 32768U
	}
}
