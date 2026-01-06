using System;

namespace System.Threading
{
	// Token: 0x020002B9 RID: 697
	public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x000175B9 File Offset: 0x000157B9
		static PreAllocatedOverlapped()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00070CAF File Offset: 0x0006EEAF
		[CLSCompliant(false)]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._overlapped = Win32ThreadPoolNativeOverlapped.Allocate(callback, state, pinData, this);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00070CD4 File Offset: 0x0006EED4
		internal bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00070CE2 File Offset: 0x0006EEE2
		internal void Release()
		{
			this._lifetime.Release(this);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00070CF0 File Offset: 0x0006EEF0
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00070D04 File Offset: 0x0006EF04
		~PreAllocatedOverlapped()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00070D38 File Offset: 0x0006EF38
		unsafe void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (this._overlapped != null)
			{
				if (disposed)
				{
					Win32ThreadPoolNativeOverlapped.Free(this._overlapped);
					return;
				}
				*Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(this._overlapped) = default(NativeOverlapped);
			}
		}

		// Token: 0x04001AB6 RID: 6838
		internal unsafe readonly Win32ThreadPoolNativeOverlapped* _overlapped;

		// Token: 0x04001AB7 RID: 6839
		private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;
	}
}
