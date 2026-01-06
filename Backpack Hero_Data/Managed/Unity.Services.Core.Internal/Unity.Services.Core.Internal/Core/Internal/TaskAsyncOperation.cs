using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002E RID: 46
	internal class TaskAsyncOperation : AsyncOperationBase, INotifyCompletion
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002854 File Offset: 0x00000A54
		public override bool IsCompleted
		{
			get
			{
				return this.m_Task.IsCompleted;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002861 File Offset: 0x00000A61
		public override AsyncOperationStatus Status
		{
			get
			{
				if (this.m_Task == null)
				{
					return AsyncOperationStatus.None;
				}
				if (!this.m_Task.IsCompleted)
				{
					return AsyncOperationStatus.InProgress;
				}
				if (this.m_Task.IsCanceled)
				{
					return AsyncOperationStatus.Cancelled;
				}
				if (this.m_Task.IsFaulted)
				{
					return AsyncOperationStatus.Failed;
				}
				return AsyncOperationStatus.Succeeded;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000289B File Offset: 0x00000A9B
		public override Exception Exception
		{
			get
			{
				Task task = this.m_Task;
				if (task == null)
				{
					return null;
				}
				return task.Exception;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000028AE File Offset: 0x00000AAE
		public override void GetResult()
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000028B0 File Offset: 0x00000AB0
		public override AsyncOperationBase GetAwaiter()
		{
			return this;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000028B4 File Offset: 0x00000AB4
		public TaskAsyncOperation(Task task)
		{
			if (TaskAsyncOperation.Scheduler == null)
			{
				TaskAsyncOperation.SetScheduler();
			}
			this.m_Task = task;
			task.ContinueWith(delegate(Task t, object state)
			{
				((TaskAsyncOperation)state).DidComplete();
			}, this, CancellationToken.None, TaskContinuationOptions.None, TaskAsyncOperation.Scheduler);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000290C File Offset: 0x00000B0C
		public static TaskAsyncOperation Run(Action action)
		{
			Task task = new Task(action);
			TaskAsyncOperation taskAsyncOperation = new TaskAsyncOperation(task);
			task.Start();
			return taskAsyncOperation;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000292C File Offset: 0x00000B2C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void SetScheduler()
		{
			TaskAsyncOperation.Scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		// Token: 0x0400002F RID: 47
		internal static TaskScheduler Scheduler;

		// Token: 0x04000030 RID: 48
		private Task m_Task;
	}
}
