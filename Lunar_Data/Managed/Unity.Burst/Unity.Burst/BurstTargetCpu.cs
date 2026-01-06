using System;

namespace Unity.Burst
{
	// Token: 0x0200000C RID: 12
	internal enum BurstTargetCpu
	{
		// Token: 0x04000093 RID: 147
		Auto,
		// Token: 0x04000094 RID: 148
		X86_SSE2,
		// Token: 0x04000095 RID: 149
		X86_SSE4,
		// Token: 0x04000096 RID: 150
		X64_SSE2,
		// Token: 0x04000097 RID: 151
		X64_SSE4,
		// Token: 0x04000098 RID: 152
		AVX,
		// Token: 0x04000099 RID: 153
		AVX2,
		// Token: 0x0400009A RID: 154
		WASM32,
		// Token: 0x0400009B RID: 155
		ARMV7A_NEON32,
		// Token: 0x0400009C RID: 156
		ARMV8A_AARCH64,
		// Token: 0x0400009D RID: 157
		THUMB2_NEON32,
		// Token: 0x0400009E RID: 158
		ARMV8A_AARCH64_HALFFP,
		// Token: 0x0400009F RID: 159
		ARMV9A
	}
}
