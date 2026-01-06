using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000026 RID: 38
	internal abstract class AsyncOperationBase : CustomYieldInstruction, IAsyncOperation, IEnumerator, INotifyCompletion
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002663 File Offset: 0x00000863
		public override bool keepWaiting
		{
			get
			{
				return !this.IsCompleted;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146
		public abstract bool IsCompleted { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000266E File Offset: 0x0000086E
		public bool IsDone
		{
			get
			{
				return this.IsCompleted;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000094 RID: 148
		public abstract AsyncOperationStatus Status { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000095 RID: 149
		public abstract Exception Exception { get; }

		// Token: 0x06000096 RID: 150
		public abstract void GetResult();

		// Token: 0x06000097 RID: 151
		public abstract AsyncOperationBase GetAwaiter();

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000098 RID: 152 RVA: 0x00002676 File Offset: 0x00000876
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x0000269F File Offset: 0x0000089F
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

		// Token: 0x0600009A RID: 154 RVA: 0x000026B8 File Offset: 0x000008B8
		protected void DidComplete()
		{
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback == null)
			{
				return;
			}
			completedCallback(this);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000026CC File Offset: 0x000008CC
		public virtual void OnCompleted(Action continuation)
		{
			this.Completed += delegate(IAsyncOperation op)
			{
				Action continuation2 = continuation;
				if (continuation2 == null)
				{
					return;
				}
				continuation2();
			};
		}

		// Token: 0x04000027 RID: 39
		private Action<IAsyncOperation> m_CompletedCallback;
	}
}
