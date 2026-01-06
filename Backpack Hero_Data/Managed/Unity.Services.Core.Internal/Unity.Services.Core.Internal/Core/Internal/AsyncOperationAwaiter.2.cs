using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000025 RID: 37
	internal struct AsyncOperationAwaiter<T> : IAsyncOperationAwaiter<T>, ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000025B0 File Offset: 0x000007B0
		public AsyncOperationAwaiter(IAsyncOperation<T> asyncOperation)
		{
			this.m_Operation = asyncOperation;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000025BC File Offset: 0x000007BC
		public void OnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation<T> obj)
			{
				continuation();
			};
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000025F0 File Offset: 0x000007F0
		public void UnsafeOnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation<T> obj)
			{
				continuation();
			};
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002621 File Offset: 0x00000821
		public bool IsCompleted
		{
			get
			{
				return this.m_Operation.IsDone;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000262E File Offset: 0x0000082E
		public T GetResult()
		{
			if (this.m_Operation.Status == AsyncOperationStatus.Failed || this.m_Operation.Status == AsyncOperationStatus.Cancelled)
			{
				throw this.m_Operation.Exception;
			}
			return this.m_Operation.Result;
		}

		// Token: 0x04000026 RID: 38
		private IAsyncOperation<T> m_Operation;
	}
}
