using System;

namespace System.IO
{
	// Token: 0x0200082B RID: 2091
	internal interface IFileWatcher
	{
		// Token: 0x060042C8 RID: 17096
		void StartDispatching(object fsw);

		// Token: 0x060042C9 RID: 17097
		void StopDispatching(object fsw);

		// Token: 0x060042CA RID: 17098
		void Dispose(object fsw);
	}
}
