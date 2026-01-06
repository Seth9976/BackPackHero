using System;
using System.Runtime.ConstrainedExecution;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200000D RID: 13
	public abstract class GenericSafeHandle<HandleType> : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002518 File Offset: 0x00000718
		public GenericSafeHandle(HandleType handle)
		{
			this.handleObject = handle;
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000253D File Offset: 0x0000073D
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			this.Dispose(false);
		}

		// Token: 0x06000037 RID: 55
		protected abstract void ReleaseHandle();

		// Token: 0x06000038 RID: 56
		public abstract bool IsValid();

		// Token: 0x06000039 RID: 57 RVA: 0x00002546 File Offset: 0x00000746
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.ReleaseHandle();
				this.disposedValue = true;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002560 File Offset: 0x00000760
		~GenericSafeHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002590 File Offset: 0x00000790
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400002C RID: 44
		protected HandleType handleObject;

		// Token: 0x0400002D RID: 45
		private bool disposedValue;
	}
}
