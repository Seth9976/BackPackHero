using System;

namespace System.IO
{
	// Token: 0x02000839 RID: 2105
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x060042FA RID: 17146 RVA: 0x00003917 File Offset: 0x00001B17
		public void StartDispatching(object handle)
		{
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00003917 File Offset: 0x00001B17
		public void StopDispatching(object handle)
		{
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000E95B8 File Offset: 0x000E77B8
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x04002859 RID: 10329
		private static IFileWatcher instance;
	}
}
