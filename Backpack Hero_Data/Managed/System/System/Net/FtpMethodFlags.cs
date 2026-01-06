using System;

namespace System.Net
{
	// Token: 0x0200039D RID: 925
	[Flags]
	internal enum FtpMethodFlags
	{
		// Token: 0x04001028 RID: 4136
		None = 0,
		// Token: 0x04001029 RID: 4137
		IsDownload = 1,
		// Token: 0x0400102A RID: 4138
		IsUpload = 2,
		// Token: 0x0400102B RID: 4139
		TakesParameter = 4,
		// Token: 0x0400102C RID: 4140
		MayTakeParameter = 8,
		// Token: 0x0400102D RID: 4141
		DoesNotTakeParameter = 16,
		// Token: 0x0400102E RID: 4142
		ParameterIsDirectory = 32,
		// Token: 0x0400102F RID: 4143
		ShouldParseForResponseUri = 64,
		// Token: 0x04001030 RID: 4144
		HasHttpCommand = 128,
		// Token: 0x04001031 RID: 4145
		MustChangeWorkingDirectoryToPath = 256
	}
}
