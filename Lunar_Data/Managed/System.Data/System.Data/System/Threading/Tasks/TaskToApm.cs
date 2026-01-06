using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000034 RID: 52
	internal static class TaskToApm
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000C920 File Offset: 0x0000AB20
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C970 File Offset: 0x0000AB70
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000035 RID: 53
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06000242 RID: 578 RVA: 0x0000CA38 File Offset: 0x0000AC38
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000243 RID: 579 RVA: 0x0000CA55 File Offset: 0x0000AC55
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000244 RID: 580 RVA: 0x0000CA5D File Offset: 0x0000AC5D
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000245 RID: 581 RVA: 0x0000CA65 File Offset: 0x0000AC65
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CA72 File Offset: 0x0000AC72
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04000466 RID: 1126
			internal readonly Task Task;

			// Token: 0x04000467 RID: 1127
			private readonly object _state;

			// Token: 0x04000468 RID: 1128
			private readonly bool _completedSynchronously;
		}
	}
}
