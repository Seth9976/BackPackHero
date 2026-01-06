using System;

namespace System.Net
{
	// Token: 0x0200039C RID: 924
	internal enum FtpOperation
	{
		// Token: 0x04001019 RID: 4121
		DownloadFile,
		// Token: 0x0400101A RID: 4122
		ListDirectory,
		// Token: 0x0400101B RID: 4123
		ListDirectoryDetails,
		// Token: 0x0400101C RID: 4124
		UploadFile,
		// Token: 0x0400101D RID: 4125
		UploadFileUnique,
		// Token: 0x0400101E RID: 4126
		AppendFile,
		// Token: 0x0400101F RID: 4127
		DeleteFile,
		// Token: 0x04001020 RID: 4128
		GetDateTimestamp,
		// Token: 0x04001021 RID: 4129
		GetFileSize,
		// Token: 0x04001022 RID: 4130
		Rename,
		// Token: 0x04001023 RID: 4131
		MakeDirectory,
		// Token: 0x04001024 RID: 4132
		RemoveDirectory,
		// Token: 0x04001025 RID: 4133
		PrintWorkingDirectory,
		// Token: 0x04001026 RID: 4134
		Other
	}
}
