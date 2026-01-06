using System;
using System.Collections.Generic;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x0200037E RID: 894
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06002536 RID: 9526 RVA: 0x000844A7 File Offset: 0x000826A7
		internal ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000844B0 File Offset: 0x000826B0
		protected internal override void QueueTask(Task task)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				Task parent = task.m_parent;
				TaskTrace.TaskScheduled(base.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, task.Id, (parent == null) ? 0 : parent.Id, (int)task.Options);
			}
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				RuntimeThread runtimeThread = RuntimeThread.Create(ThreadPoolTaskScheduler.s_longRunningThreadWork, 0);
				runtimeThread.IsBackground = true;
				runtimeThread.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00084538 File Offset: 0x00082738
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0008457C File Offset: 0x0008277C
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00084584 File Offset: 0x00082784
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00084591 File Offset: 0x00082791
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000845A1 File Offset: 0x000827A1
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001D59 RID: 7513
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = delegate(object s)
		{
			((Task)s).ExecuteEntry(false);
		};
	}
}
