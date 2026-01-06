using System;

namespace Unity.Burst
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum NativeDumpFlags
	{
		// Token: 0x0400009E RID: 158
		None = 0,
		// Token: 0x0400009F RID: 159
		IL = 1,
		// Token: 0x040000A0 RID: 160
		Unused = 2,
		// Token: 0x040000A1 RID: 161
		IR = 4,
		// Token: 0x040000A2 RID: 162
		IROptimized = 8,
		// Token: 0x040000A3 RID: 163
		Asm = 16,
		// Token: 0x040000A4 RID: 164
		Function = 32,
		// Token: 0x040000A5 RID: 165
		Analysis = 64,
		// Token: 0x040000A6 RID: 166
		IRPassAnalysis = 128,
		// Token: 0x040000A7 RID: 167
		ILPre = 256,
		// Token: 0x040000A8 RID: 168
		IRPerEntryPoint = 512,
		// Token: 0x040000A9 RID: 169
		All = 1021
	}
}
