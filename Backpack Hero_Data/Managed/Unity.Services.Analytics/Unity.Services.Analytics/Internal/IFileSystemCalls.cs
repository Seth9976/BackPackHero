using System;
using System.IO;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000032 RID: 50
	internal interface IFileSystemCalls
	{
		// Token: 0x06000105 RID: 261
		bool CanAccessFileSystem();

		// Token: 0x06000106 RID: 262
		bool FileExists(string path);

		// Token: 0x06000107 RID: 263
		void DeleteFile(string path);

		// Token: 0x06000108 RID: 264
		Stream OpenFileForWriting(string path);

		// Token: 0x06000109 RID: 265
		Stream OpenFileForReading(string path);
	}
}
