using System;

namespace System.Net
{
	// Token: 0x020003F9 RID: 1017
	internal enum BufferType
	{
		// Token: 0x0400122E RID: 4654
		Empty,
		// Token: 0x0400122F RID: 4655
		Data,
		// Token: 0x04001230 RID: 4656
		Token,
		// Token: 0x04001231 RID: 4657
		Parameters,
		// Token: 0x04001232 RID: 4658
		Missing,
		// Token: 0x04001233 RID: 4659
		Extra,
		// Token: 0x04001234 RID: 4660
		Trailer,
		// Token: 0x04001235 RID: 4661
		Header,
		// Token: 0x04001236 RID: 4662
		Padding = 9,
		// Token: 0x04001237 RID: 4663
		Stream,
		// Token: 0x04001238 RID: 4664
		ChannelBindings = 14,
		// Token: 0x04001239 RID: 4665
		TargetHost = 16,
		// Token: 0x0400123A RID: 4666
		ReadOnlyFlag = -2147483648,
		// Token: 0x0400123B RID: 4667
		ReadOnlyWithChecksum = 268435456
	}
}
