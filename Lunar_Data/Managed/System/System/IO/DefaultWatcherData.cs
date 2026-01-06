using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200081C RID: 2076
	internal class DefaultWatcherData
	{
		// Token: 0x04002790 RID: 10128
		public FileSystemWatcher FSW;

		// Token: 0x04002791 RID: 10129
		public string Directory;

		// Token: 0x04002792 RID: 10130
		public string FileMask;

		// Token: 0x04002793 RID: 10131
		public bool IncludeSubdirs;

		// Token: 0x04002794 RID: 10132
		public bool Enabled;

		// Token: 0x04002795 RID: 10133
		public bool NoWildcards;

		// Token: 0x04002796 RID: 10134
		public DateTime DisabledTime;

		// Token: 0x04002797 RID: 10135
		public object FilesLock = new object();

		// Token: 0x04002798 RID: 10136
		public Dictionary<string, FileData> Files;
	}
}
