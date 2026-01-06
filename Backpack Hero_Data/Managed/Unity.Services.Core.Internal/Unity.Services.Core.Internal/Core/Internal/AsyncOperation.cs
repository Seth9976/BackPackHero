using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000022 RID: 34
	internal class AsyncOperation : IAsyncOperation, IEnumerator
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002274 File Offset: 0x00000474
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000227C File Offset: 0x0000047C
		public bool IsDone { get; protected set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002285 File Offset: 0x00000485
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000228D File Offset: 0x0000048D
		public AsyncOperationStatus Status { get; protected set; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000069 RID: 105 RVA: 0x00002296 File Offset: 0x00000496
		// (remove) Token: 0x0600006A RID: 106 RVA: 0x000022BF File Offset: 0x000004BF
		public event Action<IAsyncOperation> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000022E0 File Offset: 0x000004E0
		public Exception Exception { get; protected set; }

		// Token: 0x0600006D RID: 109 RVA: 0x000022E9 File Offset: 0x000004E9
		public void SetInProgress()
		{
			this.Status = AsyncOperationStatus.InProgress;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000022F2 File Offset: 0x000004F2
		public void Succeed()
		{
			if (this.IsDone)
			{
				return;
			}
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Succeeded;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002324 File Offset: 0x00000524
		public void Fail(Exception reason)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = reason;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Failed;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000235D File Offset: 0x0000055D
		public void Cancel()
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = new OperationCanceledException();
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Cancelled;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000239A File Offset: 0x0000059A
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000023A5 File Offset: 0x000005A5
		void IEnumerator.Reset()
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000023A7 File Offset: 0x000005A7
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400001F RID: 31
		protected Action<IAsyncOperation> m_CompletedCallback;
	}
}
