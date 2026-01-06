using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200038B RID: 907
	[StructLayout(LayoutKind.Auto)]
	public struct ManualResetValueTaskSourceCore<TResult>
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x0008478F File Offset: 0x0008298F
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x00084797 File Offset: 0x00082997
		public bool RunContinuationsAsynchronously { readonly get; set; }

		// Token: 0x0600255E RID: 9566 RVA: 0x000847A0 File Offset: 0x000829A0
		public void Reset()
		{
			this._version += 1;
			this._completed = false;
			this._result = default(TResult);
			this._error = null;
			this._executionContext = null;
			this._capturedContext = null;
			this._continuation = null;
			this._continuationState = null;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000847F2 File Offset: 0x000829F2
		public void SetResult(TResult result)
		{
			this._result = result;
			this.SignalCompletion();
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00084801 File Offset: 0x00082A01
		public void SetException(Exception error)
		{
			this._error = ExceptionDispatchInfo.Capture(error);
			this.SignalCompletion();
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x00084815 File Offset: 0x00082A15
		public short Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0008481D File Offset: 0x00082A1D
		public ValueTaskSourceStatus GetStatus(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				return ValueTaskSourceStatus.Pending;
			}
			if (this._error == null)
			{
				return ValueTaskSourceStatus.Succeeded;
			}
			if (!(this._error.SourceException is OperationCanceledException))
			{
				return ValueTaskSourceStatus.Faulted;
			}
			return ValueTaskSourceStatus.Canceled;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0008484F File Offset: 0x00082A4F
		[StackTraceHidden]
		public TResult GetResult(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
			ExceptionDispatchInfo error = this._error;
			if (error != null)
			{
				error.Throw();
			}
			return this._result;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0008487C File Offset: 0x00082A7C
		public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			this.ValidateToken(token);
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				this._executionContext = ExecutionContext.Capture();
			}
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					this._capturedContext = synchronizationContext;
				}
				else
				{
					TaskScheduler taskScheduler = TaskScheduler.Current;
					if (taskScheduler != TaskScheduler.Default)
					{
						this._capturedContext = taskScheduler;
					}
				}
			}
			object obj = this._continuation;
			if (obj == null)
			{
				this._continuationState = state;
				obj = Interlocked.CompareExchange<Action<object>>(ref this._continuation, continuation, null);
			}
			if (obj != null)
			{
				if (obj != ManualResetValueTaskSourceCoreShared.s_sentinel)
				{
					ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
				}
				object capturedContext = this._capturedContext;
				if (capturedContext != null)
				{
					SynchronizationContext synchronizationContext2 = capturedContext as SynchronizationContext;
					if (synchronizationContext2 != null)
					{
						synchronizationContext2.Post(delegate(object s)
						{
							Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
							tuple.Item1(tuple.Item2);
						}, Tuple.Create<Action<object>, object>(continuation, state));
						return;
					}
					TaskScheduler taskScheduler2 = capturedContext as TaskScheduler;
					if (taskScheduler2 == null)
					{
						return;
					}
					Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler2);
				}
				else
				{
					if (this._executionContext != null)
					{
						ThreadPool.QueueUserWorkItem<object>(continuation, state, true);
						return;
					}
					ThreadPool.UnsafeQueueUserWorkItem<object>(continuation, state, true);
					return;
				}
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000849A8 File Offset: 0x00082BA8
		private void ValidateToken(short token)
		{
			if (token != this._version)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000849B8 File Offset: 0x00082BB8
		private void SignalCompletion()
		{
			if (this._completed)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
			this._completed = true;
			if (this._continuation != null || Interlocked.CompareExchange<Action<object>>(ref this._continuation, ManualResetValueTaskSourceCoreShared.s_sentinel, null) != null)
			{
				if (this._executionContext != null)
				{
					ExecutionContext.RunInternal<ManualResetValueTaskSourceCore<TResult>>(this._executionContext, delegate(ref ManualResetValueTaskSourceCore<TResult> s)
					{
						s.InvokeContinuation();
					}, ref this);
					return;
				}
				this.InvokeContinuation();
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00084A30 File Offset: 0x00082C30
		private void InvokeContinuation()
		{
			object capturedContext = this._capturedContext;
			if (capturedContext != null)
			{
				SynchronizationContext synchronizationContext = capturedContext as SynchronizationContext;
				if (synchronizationContext != null)
				{
					synchronizationContext.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create<Action<object>, object>(this._continuation, this._continuationState));
					return;
				}
				TaskScheduler taskScheduler = capturedContext as TaskScheduler;
				if (taskScheduler == null)
				{
					return;
				}
				Task.Factory.StartNew(this._continuation, this._continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
				return;
			}
			else
			{
				if (!this.RunContinuationsAsynchronously)
				{
					this._continuation(this._continuationState);
					return;
				}
				if (this._executionContext != null)
				{
					ThreadPool.QueueUserWorkItem<object>(this._continuation, this._continuationState, true);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem<object>(this._continuation, this._continuationState, true);
				return;
			}
		}

		// Token: 0x04001D7D RID: 7549
		private Action<object> _continuation;

		// Token: 0x04001D7E RID: 7550
		private object _continuationState;

		// Token: 0x04001D7F RID: 7551
		private ExecutionContext _executionContext;

		// Token: 0x04001D80 RID: 7552
		private object _capturedContext;

		// Token: 0x04001D81 RID: 7553
		private bool _completed;

		// Token: 0x04001D82 RID: 7554
		private TResult _result;

		// Token: 0x04001D83 RID: 7555
		private ExceptionDispatchInfo _error;

		// Token: 0x04001D84 RID: 7556
		private short _version;
	}
}
