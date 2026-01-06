using System;

namespace UnityEngine
{
	// Token: 0x02000185 RID: 389
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		// Token: 0x0400056D RID: 1389
		None = 0,
		// Token: 0x0400056E RID: 1390
		RealtimeEmissive = 1,
		// Token: 0x0400056F RID: 1391
		BakedEmissive = 2,
		// Token: 0x04000570 RID: 1392
		EmissiveIsBlack = 4,
		// Token: 0x04000571 RID: 1393
		AnyEmissive = 3
	}
}
