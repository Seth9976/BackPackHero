using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x02000824 RID: 2084
	internal class FAMData
	{
		// Token: 0x040027B0 RID: 10160
		public FileSystemWatcher FSW;

		// Token: 0x040027B1 RID: 10161
		public string Directory;

		// Token: 0x040027B2 RID: 10162
		public string FileMask;

		// Token: 0x040027B3 RID: 10163
		public bool IncludeSubdirs;

		// Token: 0x040027B4 RID: 10164
		public bool Enabled;

		// Token: 0x040027B5 RID: 10165
		public FAMRequest Request;

		// Token: 0x040027B6 RID: 10166
		public Hashtable SubDirs;
	}
}
