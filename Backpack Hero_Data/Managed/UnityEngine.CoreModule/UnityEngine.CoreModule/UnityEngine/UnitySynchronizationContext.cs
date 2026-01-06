using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000227 RID: 551
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x000268D9 File Offset: 0x00024AD9
		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0002690B File Offset: 0x00024B0B
		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00026938 File Offset: 0x00024B38
		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback.Invoke(state);
			}
			else
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, manualResetEvent));
					}
					manualResetEvent.WaitOne();
				}
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000269D4 File Offset: 0x00024BD4
		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000269E3 File Offset: 0x00024BE3
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000269F4 File Offset: 0x00024BF4
		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00026A40 File Offset: 0x00024C40
		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00026A64 File Offset: 0x00024C64
		private void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest workRequest = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.RemoveAt(0);
				workRequest.Invoke();
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00026AF4 File Offset: 0x00024CF4
		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00026B1F File Offset: 0x00024D1F
		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			SynchronizationContext.SetSynchronizationContext(new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00026B38 File Offset: 0x00024D38
		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext != null;
			if (flag)
			{
				unitySynchronizationContext.Exec();
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00026B60 File Offset: 0x00024D60
		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (unitySynchronizationContext.HasPendingTasks())
				{
					bool flag3 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag3)
					{
						break;
					}
					unitySynchronizationContext.Exec();
					Thread.Sleep(1);
				}
				flag2 = !unitySynchronizationContext.HasPendingTasks();
			}
			return flag2;
		}

		// Token: 0x04000826 RID: 2086
		private const int kAwqInitialCapacity = 20;

		// Token: 0x04000827 RID: 2087
		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		// Token: 0x04000828 RID: 2088
		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		// Token: 0x04000829 RID: 2089
		private readonly int m_MainThreadID;

		// Token: 0x0400082A RID: 2090
		private int m_TrackedCount = 0;

		// Token: 0x02000228 RID: 552
		private struct WorkRequest
		{
			// Token: 0x060017E1 RID: 6113 RVA: 0x00026BD0 File Offset: 0x00024DD0
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			// Token: 0x060017E2 RID: 6114 RVA: 0x00026BE8 File Offset: 0x00024DE8
			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback.Invoke(this.m_DelagateState);
				}
				finally
				{
					ManualResetEvent waitHandle = this.m_WaitHandle;
					if (waitHandle != null)
					{
						waitHandle.Set();
					}
				}
			}

			// Token: 0x0400082B RID: 2091
			private readonly SendOrPostCallback m_DelagateCallback;

			// Token: 0x0400082C RID: 2092
			private readonly object m_DelagateState;

			// Token: 0x0400082D RID: 2093
			private readonly ManualResetEvent m_WaitHandle;
		}
	}
}
