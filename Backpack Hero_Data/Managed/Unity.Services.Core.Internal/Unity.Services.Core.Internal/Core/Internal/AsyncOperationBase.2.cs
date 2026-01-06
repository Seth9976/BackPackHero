using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000027 RID: 39
	internal abstract class AsyncOperationBase<T> : CustomYieldInstruction, IAsyncOperation<T>, IEnumerator, INotifyCompletion
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002700 File Offset: 0x00000900
		public override bool keepWaiting
		{
			get
			{
				return !this.IsCompleted;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158
		public abstract bool IsCompleted { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000270B File Offset: 0x0000090B
		public bool IsDone
		{
			get
			{
				return this.IsCompleted;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160
		public abstract AsyncOperationStatus Status { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A1 RID: 161
		public abstract Exception Exception { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162
		public abstract T Result { get; }

		// Token: 0x060000A3 RID: 163
		public abstract T GetResult();

		// Token: 0x060000A4 RID: 164
		public abstract AsyncOperationBase<T> GetAwaiter();

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000A5 RID: 165 RVA: 0x00002713 File Offset: 0x00000913
		// (remove) Token: 0x060000A6 RID: 166 RVA: 0x0000273C File Offset: 0x0000093C
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

		// Token: 0x060000A7 RID: 167 RVA: 0x00002755 File Offset: 0x00000955
		protected void DidComplete()
		{
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback == null)
			{
				return;
			}
			completedCallback(this);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002768 File Offset: 0x00000968
		public virtual void OnCompleted(Action continuation)
		{
			this.Completed += delegate(IAsyncOperation<T> op)
			{
				Action continuation2 = continuation;
				if (continuation2 == null)
				{
					return;
				}
				continuation2();
			};
		}

		// Token: 0x04000028 RID: 40
		private Action<IAsyncOperation<T>> m_CompletedCallback;
	}
}
