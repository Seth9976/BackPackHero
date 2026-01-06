using System;

namespace Unity.Burst
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum NativeDumpFlags
	{
		// Token: 0x040000A1 RID: 161
		None = 0,
		// Token: 0x040000A2 RID: 162
		IL = 1,
		// Token: 0x040000A3 RID: 163
		Unused = 2,
		// Token: 0x040000A4 RID: 164
		IR = 4,
		// Token: 0x040000A5 RID: 165
		IROptimized = 8,
		// Token: 0x040000A6 RID: 166
		Asm = 16,
		// Token: 0x040000A7 RID: 167
		Function = 32,
		// Token: 0x040000A8 RID: 168
		Analysis = 64,
		// Token: 0x040000A9 RID: 169
		IRPassAnalysis = 128,
		// Token: 0x040000AA RID: 170
		ILPre = 256,
		// Token: 0x040000AB RID: 171
		IRPerEntryPoint = 512,
		// Token: 0x040000AC RID: 172
		All = 1021
	}
}
