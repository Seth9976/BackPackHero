using System;

namespace System.Net
{
	// Token: 0x020003FB RID: 1019
	internal enum IgnoreCertProblem
	{
		// Token: 0x04001244 RID: 4676
		not_time_valid = 1,
		// Token: 0x04001245 RID: 4677
		ctl_not_time_valid,
		// Token: 0x04001246 RID: 4678
		not_time_nested = 4,
		// Token: 0x04001247 RID: 4679
		invalid_basic_constraints = 8,
		// Token: 0x04001248 RID: 4680
		all_not_time_valid = 7,
		// Token: 0x04001249 RID: 4681
		allow_unknown_ca = 16,
		// Token: 0x0400124A RID: 4682
		wrong_usage = 32,
		// Token: 0x0400124B RID: 4683
		invalid_name = 64,
		// Token: 0x0400124C RID: 4684
		invalid_policy = 128,
		// Token: 0x0400124D RID: 4685
		end_rev_unknown = 256,
		// Token: 0x0400124E RID: 4686
		ctl_signer_rev_unknown = 512,
		// Token: 0x0400124F RID: 4687
		ca_rev_unknown = 1024,
		// Token: 0x04001250 RID: 4688
		root_rev_unknown = 2048,
		// Token: 0x04001251 RID: 4689
		all_rev_unknown = 3840,
		// Token: 0x04001252 RID: 4690
		none = 4095
	}
}
