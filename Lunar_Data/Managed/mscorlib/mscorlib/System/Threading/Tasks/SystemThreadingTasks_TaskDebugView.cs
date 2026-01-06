using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000360 RID: 864
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x000824BF File Offset: 0x000806BF
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x000824CE File Offset: 0x000806CE
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x000824DB File Offset: 0x000806DB
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000824E8 File Offset: 0x000806E8
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000824F5 File Offset: 0x000806F5
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x00082504 File Offset: 0x00080704
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x00082534 File Offset: 0x00080734
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001D04 RID: 7428
		private Task m_task;
	}
}
