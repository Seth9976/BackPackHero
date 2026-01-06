using System;

namespace System.IO
{
	// Token: 0x02000831 RID: 2097
	[Flags]
	internal enum FilterFlags : uint
	{
		// Token: 0x04002801 RID: 10241
		ReadPoll = 4096U,
		// Token: 0x04002802 RID: 10242
		ReadOutOfBand = 8192U,
		// Token: 0x04002803 RID: 10243
		ReadLowWaterMark = 1U,
		// Token: 0x04002804 RID: 10244
		WriteLowWaterMark = 1U,
		// Token: 0x04002805 RID: 10245
		NoteTrigger = 16777216U,
		// Token: 0x04002806 RID: 10246
		NoteFFNop = 0U,
		// Token: 0x04002807 RID: 10247
		NoteFFAnd = 1073741824U,
		// Token: 0x04002808 RID: 10248
		NoteFFOr = 2147483648U,
		// Token: 0x04002809 RID: 10249
		NoteFFCopy = 3221225472U,
		// Token: 0x0400280A RID: 10250
		NoteFFCtrlMask = 3221225472U,
		// Token: 0x0400280B RID: 10251
		NoteFFlagsMask = 16777215U,
		// Token: 0x0400280C RID: 10252
		VNodeDelete = 1U,
		// Token: 0x0400280D RID: 10253
		VNodeWrite = 2U,
		// Token: 0x0400280E RID: 10254
		VNodeExtend = 4U,
		// Token: 0x0400280F RID: 10255
		VNodeAttrib = 8U,
		// Token: 0x04002810 RID: 10256
		VNodeLink = 16U,
		// Token: 0x04002811 RID: 10257
		VNodeRename = 32U,
		// Token: 0x04002812 RID: 10258
		VNodeRevoke = 64U,
		// Token: 0x04002813 RID: 10259
		VNodeNone = 128U,
		// Token: 0x04002814 RID: 10260
		ProcExit = 2147483648U,
		// Token: 0x04002815 RID: 10261
		ProcFork = 1073741824U,
		// Token: 0x04002816 RID: 10262
		ProcExec = 536870912U,
		// Token: 0x04002817 RID: 10263
		ProcReap = 268435456U,
		// Token: 0x04002818 RID: 10264
		ProcSignal = 134217728U,
		// Token: 0x04002819 RID: 10265
		ProcExitStatus = 67108864U,
		// Token: 0x0400281A RID: 10266
		ProcResourceEnd = 33554432U,
		// Token: 0x0400281B RID: 10267
		ProcAppactive = 8388608U,
		// Token: 0x0400281C RID: 10268
		ProcAppBackground = 4194304U,
		// Token: 0x0400281D RID: 10269
		ProcAppNonUI = 2097152U,
		// Token: 0x0400281E RID: 10270
		ProcAppInactive = 1048576U,
		// Token: 0x0400281F RID: 10271
		ProcAppAllStates = 15728640U,
		// Token: 0x04002820 RID: 10272
		ProcPDataMask = 1048575U,
		// Token: 0x04002821 RID: 10273
		ProcControlMask = 4293918720U,
		// Token: 0x04002822 RID: 10274
		VMPressure = 2147483648U,
		// Token: 0x04002823 RID: 10275
		VMPressureTerminate = 1073741824U,
		// Token: 0x04002824 RID: 10276
		VMPressureSuddenTerminate = 536870912U,
		// Token: 0x04002825 RID: 10277
		VMError = 268435456U,
		// Token: 0x04002826 RID: 10278
		TimerSeconds = 1U,
		// Token: 0x04002827 RID: 10279
		TimerMicroSeconds = 2U,
		// Token: 0x04002828 RID: 10280
		TimerNanoSeconds = 4U,
		// Token: 0x04002829 RID: 10281
		TimerAbsolute = 8U
	}
}
