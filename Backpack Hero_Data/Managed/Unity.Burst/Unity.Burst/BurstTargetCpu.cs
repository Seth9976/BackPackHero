using System;

namespace Unity.Burst
{
	// Token: 0x0200000C RID: 12
	internal enum BurstTargetCpu
	{
		// Token: 0x04000090 RID: 144
		Auto,
		// Token: 0x04000091 RID: 145
		X86_SSE2,
		// Token: 0x04000092 RID: 146
		X86_SSE4,
		// Token: 0x04000093 RID: 147
		X64_SSE2,
		// Token: 0x04000094 RID: 148
		X64_SSE4,
		// Token: 0x04000095 RID: 149
		AVX,
		// Token: 0x04000096 RID: 150
		AVX2,
		// Token: 0x04000097 RID: 151
		WASM32,
		// Token: 0x04000098 RID: 152
		ARMV7A_NEON32,
		// Token: 0x04000099 RID: 153
		ARMV8A_AARCH64,
		// Token: 0x0400009A RID: 154
		THUMB2_NEON32,
		// Token: 0x0400009B RID: 155
		ARMV8A_AARCH64_HALFFP,
		// Token: 0x0400009C RID: 156
		ARMV9A
	}
}
