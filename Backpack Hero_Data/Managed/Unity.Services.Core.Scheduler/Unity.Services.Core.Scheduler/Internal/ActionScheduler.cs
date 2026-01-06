using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Services.Core.Internal;
using UnityEngine.LowLevel;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000002 RID: 2
	internal class ActionScheduler : IActionScheduler, IServiceComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ActionScheduler()
			: this(new UtcTimeProvider())
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public ActionScheduler(ITimeProvider timeProvider)
		{
			this.m_TimeProvider = timeProvider;
			this.SchedulerLoopSystem = new PlayerLoopSystem
			{
				type = typeof(ActionScheduler),
				updateDelegate = new PlayerLoopSystem.UpdateFunction(this.ExecuteExpiredActions)
			};
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		public int ScheduledActionsCount
		{
			get
			{
				return this.m_ScheduledActions.Count;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public long ScheduleAction([NotNull] Action action, double delaySeconds = 0.0)
		{
			if (delaySeconds < 0.0)
			{
				throw new ArgumentException("delaySeconds can not be negative");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			object @lock = this.m_Lock;
			long num;
			lock (@lock)
			{
				ScheduledInvocation scheduledInvocation = new ScheduledInvocation();
				scheduledInvocation.Action = action;
				scheduledInvocation.InvocationTime = this.m_TimeProvider.Now.AddSeconds(delaySeconds);
				num = this.m_NextId;
				this.m_NextId = num + 1L;
				scheduledInvocation.ActionId = num;
				ScheduledInvocation scheduledInvocation2 = scheduledInvocation;
				if (this.m_NextId < 1L)
				{
					this.m_NextId = 1L;
				}
				this.m_ScheduledActions.Insert(scheduledInvocation2);
				this.m_IdScheduledInvocationMap.Add(scheduledInvocation2.ActionId, scheduledInvocation2);
				num = scheduledInvocation2.ActionId;
			}
			return num;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D4 File Offset: 0x000003D4
		public void CancelAction(long actionId)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				ScheduledInvocation scheduledInvocation;
				if (this.m_IdScheduledInvocationMap.TryGetValue(actionId, out scheduledInvocation))
				{
					this.m_ScheduledActions.Remove(scheduledInvocation);
					this.m_IdScheduledInvocationMap.Remove(scheduledInvocation.ActionId);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002240 File Offset: 0x00000440
		internal void ExecuteExpiredActions()
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				this.m_ExpiredActions.Clear();
				while (this.m_ScheduledActions.Count > 0)
				{
					ScheduledInvocation min = this.m_ScheduledActions.Min;
					if (!(((min != null) ? new DateTime?(min.InvocationTime) : null) <= this.m_TimeProvider.Now))
					{
						break;
					}
					ScheduledInvocation scheduledInvocation = this.m_ScheduledActions.ExtractMin();
					this.m_ExpiredActions.Add(scheduledInvocation);
					this.m_ScheduledActions.Remove(scheduledInvocation);
					this.m_IdScheduledInvocationMap.Remove(scheduledInvocation.ActionId);
				}
				foreach (ScheduledInvocation scheduledInvocation2 in this.m_ExpiredActions)
				{
					try
					{
						scheduledInvocation2.Action();
					}
					catch (Exception ex)
					{
						CoreLogger.LogException(ex);
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000237C File Offset: 0x0000057C
		internal static void UpdateCurrentPlayerLoopWith(List<PlayerLoopSystem> subSystemList, PlayerLoopSystem currentPlayerLoop)
		{
			currentPlayerLoop.subSystemList = subSystemList.ToArray();
			PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002394 File Offset: 0x00000594
		public void JoinPlayerLoopSystem()
		{
			PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			List<PlayerLoopSystem> list = new List<PlayerLoopSystem>(currentPlayerLoop.subSystemList);
			if (!list.Contains(this.SchedulerLoopSystem))
			{
				list.Add(this.SchedulerLoopSystem);
				ActionScheduler.UpdateCurrentPlayerLoopWith(list, currentPlayerLoop);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023D4 File Offset: 0x000005D4
		public void QuitPlayerLoopSystem()
		{
			PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			List<PlayerLoopSystem> list = new List<PlayerLoopSystem>(currentPlayerLoop.subSystemList);
			if (list.Remove(this.SchedulerLoopSystem))
			{
				ActionScheduler.UpdateCurrentPlayerLoopWith(list, currentPlayerLoop);
			}
		}

		// Token: 0x04000001 RID: 1
		private const long k_MinimumIdValue = 1L;

		// Token: 0x04000002 RID: 2
		internal readonly PlayerLoopSystem SchedulerLoopSystem;

		// Token: 0x04000003 RID: 3
		private readonly ITimeProvider m_TimeProvider;

		// Token: 0x04000004 RID: 4
		private readonly object m_Lock = new object();

		// Token: 0x04000005 RID: 5
		private readonly MinimumBinaryHeap<ScheduledInvocation> m_ScheduledActions = new MinimumBinaryHeap<ScheduledInvocation>(new ScheduledInvocationComparer(), 10);

		// Token: 0x04000006 RID: 6
		private readonly Dictionary<long, ScheduledInvocation> m_IdScheduledInvocationMap = new Dictionary<long, ScheduledInvocation>();

		// Token: 0x04000007 RID: 7
		private readonly List<ScheduledInvocation> m_ExpiredActions = new List<ScheduledInvocation>();

		// Token: 0x04000008 RID: 8
		private long m_NextId = 1L;
	}
}
