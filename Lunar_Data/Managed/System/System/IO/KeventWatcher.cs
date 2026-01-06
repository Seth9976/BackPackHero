using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000837 RID: 2103
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x060042F3 RID: 17139 RVA: 0x0000219B File Offset: 0x0000039B
		private KeventWatcher()
		{
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000E94D0 File Offset: 0x000E76D0
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			int num = KeventWatcher.kqueue();
			if (num == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.close(num);
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000E9538 File Offset: 0x000E7738
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor;
			if (KeventWatcher.watches.ContainsKey(fileSystemWatcher))
			{
				kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			}
			else
			{
				kqueueMonitor = new KqueueMonitor(fileSystemWatcher);
				KeventWatcher.watches.Add(fileSystemWatcher, kqueueMonitor);
			}
			kqueueMonitor.Start();
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000E9588 File Offset: 0x000E7788
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			if (kqueueMonitor == null)
			{
				return;
			}
			kqueueMonitor.Stop();
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x060042F8 RID: 17144
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x060042F9 RID: 17145
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x0400284D RID: 10317
		private static bool failed;

		// Token: 0x0400284E RID: 10318
		private static KeventWatcher instance;

		// Token: 0x0400284F RID: 10319
		private static Hashtable watches;
	}
}
