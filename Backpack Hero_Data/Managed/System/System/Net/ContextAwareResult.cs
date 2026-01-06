using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200036C RID: 876
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x06001CF5 RID: 7413 RVA: 0x00068F0E File Offset: 0x0006710E
		private void SafeCaptureIdentity()
		{
			this._windowsIdentity = WindowsIdentity.GetCurrent();
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00068F1C File Offset: 0x0006711C
		internal WindowsIdentity Identity
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				if (this._windowsIdentity != null)
				{
					return this._windowsIdentity;
				}
				if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No identity captured - specify captureIdentity.", "Identity");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling Identity (unless it's only called after FinishPostingAsyncOp).", "Identity");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._windowsIdentity;
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00069004 File Offset: 0x00067204
		private void CleanupInternal()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
				this._windowsIdentity = null;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00069020 File Offset: 0x00067220
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack)
			: this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006902D File Offset: 0x0006722D
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack)
			: this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006903D File Offset: 0x0006723D
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack)
			: base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00069078 File Offset: 0x00067278
		internal ExecutionContext ContextCopy
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				ExecutionContext context = this._context;
				if (context != null)
				{
					return context;
				}
				if (base.AsyncCallback == null && (this._flags & ContextAwareResult.StateFlags.CaptureContext) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No context captured - specify a callback or forceCaptureContext.", "ContextCopy");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling ContextCopy (unless it's only called after FinishPostingAsyncOp).", "ContextCopy");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._context;
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00069168 File Offset: 0x00067368
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00069171 File Offset: 0x00067371
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			if (base.InternalPeekCompleted)
			{
				NetEventSource.Fail(this, "Called on completed result.", "StartPostingAsyncOp");
			}
			this._lock = (lockCapture ? new object() : null);
			this._flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._lock;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000691B0 File Offset: 0x000673B0
		internal bool FinishPostingAsyncOp()
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000691E8 File Offset: 0x000673E8
		internal bool FinishPostingAsyncOp(ref CallbackClosure closure)
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			CallbackClosure callbackClosure = closure;
			ExecutionContext executionContext;
			if (callbackClosure == null)
			{
				executionContext = null;
			}
			else if (!callbackClosure.IsCompatible(base.AsyncCallback))
			{
				closure = null;
				executionContext = null;
			}
			else
			{
				base.AsyncCallback = callbackClosure.AsyncCallback;
				executionContext = callbackClosure.Context;
			}
			bool flag = this.CaptureOrComplete(ref executionContext, true);
			if (closure == null && base.AsyncCallback != null && executionContext != null)
			{
				closure = new CallbackClosure(executionContext, base.AsyncCallback);
			}
			return flag;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006926A File Offset: 0x0006746A
		protected override void Cleanup()
		{
			base.Cleanup();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Cleanup");
			}
			this.CleanupInternal();
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0006928C File Offset: 0x0006748C
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				NetEventSource.Fail(this, "Called without calling StartPostingAsyncOp.", "CaptureOrComplete");
			}
			bool flag = base.AsyncCallback != null || (this._flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && !flag)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting identity capture", "CaptureOrComplete");
				}
				this.SafeCaptureIdentity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting capture", "CaptureOrComplete");
				}
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._context = cachedContext;
					}
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("_context:{0}", new object[] { this._context }), "CaptureOrComplete");
				}
			}
			else
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Skipping capture", "CaptureOrComplete");
				}
				cachedContext = null;
				if (base.AsyncCallback != null && !base.CompletedSynchronously)
				{
					NetEventSource.Fail(this, "Didn't capture context, but didn't complete synchronously!", "CaptureOrComplete");
				}
			}
			if (base.CompletedSynchronously)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Completing synchronously", "CaptureOrComplete");
				}
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000693E0 File Offset: 0x000675E0
		protected override void Complete(IntPtr userToken)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("_context(set):{0} userToken:{1}", new object[]
				{
					this._context != null,
					userToken
				}), "Complete");
			}
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(context, delegate(object s)
			{
				((ContextAwareResult)s).CompleteCallback();
			}, this);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0006948D File Offset: 0x0006768D
		private void CompleteCallback()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "Context set, calling callback.", "CompleteCallback");
			}
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00002F6A File Offset: 0x0000116A
		internal virtual EndPoint RemoteEndPoint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000EB9 RID: 3769
		private WindowsIdentity _windowsIdentity;

		// Token: 0x04000EBA RID: 3770
		private volatile ExecutionContext _context;

		// Token: 0x04000EBB RID: 3771
		private object _lock;

		// Token: 0x04000EBC RID: 3772
		private ContextAwareResult.StateFlags _flags;

		// Token: 0x0200036D RID: 877
		[Flags]
		private enum StateFlags : byte
		{
			// Token: 0x04000EBE RID: 3774
			None = 0,
			// Token: 0x04000EBF RID: 3775
			CaptureIdentity = 1,
			// Token: 0x04000EC0 RID: 3776
			CaptureContext = 2,
			// Token: 0x04000EC1 RID: 3777
			ThreadSafeContextCopy = 4,
			// Token: 0x04000EC2 RID: 3778
			PostBlockStarted = 8,
			// Token: 0x04000EC3 RID: 3779
			PostBlockFinished = 16
		}
	}
}
