using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000023 RID: 35
	internal class AsyncOperation<T> : IAsyncOperation<T>, IEnumerator
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000023B2 File Offset: 0x000005B2
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000023BA File Offset: 0x000005BA
		public bool IsDone { get; protected set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000023C3 File Offset: 0x000005C3
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000023CB File Offset: 0x000005CB
		public AsyncOperationStatus Status { get; protected set; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000079 RID: 121 RVA: 0x000023D4 File Offset: 0x000005D4
		// (remove) Token: 0x0600007A RID: 122 RVA: 0x000023FD File Offset: 0x000005FD
		public event Action<IAsyncOperation<T>> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002416 File Offset: 0x00000616
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000241E File Offset: 0x0000061E
		public Exception Exception { get; protected set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002427 File Offset: 0x00000627
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000242F File Offset: 0x0000062F
		public T Result { get; protected set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002438 File Offset: 0x00000638
		public void SetInProgress()
		{
			this.Status = AsyncOperationStatus.InProgress;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002441 File Offset: 0x00000641
		public void Succeed(T result)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Result = result;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Succeeded;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000247A File Offset: 0x0000067A
		public void Fail(Exception reason)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = reason;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Failed;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000024B3 File Offset: 0x000006B3
		public void Cancel()
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = new OperationCanceledException();
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Cancelled;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000024F0 File Offset: 0x000006F0
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000024FB File Offset: 0x000006FB
		void IEnumerator.Reset()
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000024FD File Offset: 0x000006FD
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000024 RID: 36
		protected Action<IAsyncOperation<T>> m_CompletedCallback;
	}
}
