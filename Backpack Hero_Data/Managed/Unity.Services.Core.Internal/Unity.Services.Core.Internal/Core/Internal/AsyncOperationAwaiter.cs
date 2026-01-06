using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000024 RID: 36
	internal struct AsyncOperationAwaiter : IAsyncOperationAwaiter, ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002508 File Offset: 0x00000708
		public AsyncOperationAwaiter(IAsyncOperation asyncOperation)
		{
			this.m_Operation = asyncOperation;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002514 File Offset: 0x00000714
		public void OnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation operation)
			{
				continuation();
			};
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002548 File Offset: 0x00000748
		public void UnsafeOnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation operation)
			{
				continuation();
			};
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002579 File Offset: 0x00000779
		public bool IsCompleted
		{
			get
			{
				return this.m_Operation.IsDone;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002586 File Offset: 0x00000786
		public void GetResult()
		{
			if (this.m_Operation.Status == AsyncOperationStatus.Failed || this.m_Operation.Status == AsyncOperationStatus.Cancelled)
			{
				throw this.m_Operation.Exception;
			}
		}

		// Token: 0x04000025 RID: 37
		private IAsyncOperation m_Operation;
	}
}
