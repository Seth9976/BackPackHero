using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000069 RID: 105
	internal class TimerEventScheduler : IScheduler
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000AC40 File Offset: 0x00008E40
		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000AD18 File Offset: 0x00008F18
		public ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000AD44 File Offset: 0x00008F44
		public ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = stopCondition
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000AD80 File Offset: 0x00008F80
		public ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = null
			};
			timerEventSchedulerItem.SetDuration(durationMs);
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool flag2;
			if (flag)
			{
				this.m_ScheduledItems.RemoveAt(index);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + ((item != null) ? item.ToString() : null));
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
					}
				}
				item.OnItemUnscheduled();
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AEE0 File Offset: 0x000090E0
		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AF18 File Offset: 0x00009118
		public void UpdateScheduledEvents()
		{
			try
			{
				this.m_TransactionMode = true;
				long num = Panel.TimeSinceStartupMs();
				int count = this.m_ScheduledItems.Count;
				int num2 = this.m_LastUpdatedIndex + 1;
				bool flag = num2 >= count;
				if (flag)
				{
					num2 = 0;
				}
				for (int i = 0; i < count; i++)
				{
					int num3 = num2 + i;
					bool flag2 = num3 >= count;
					if (flag2)
					{
						num3 -= count;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[num3];
					bool flag3 = false;
					bool flag4 = num - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag4)
					{
						TimerState timerState = new TimerState
						{
							start = scheduledItem.startMs,
							now = num
						};
						bool flag5 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag5)
						{
							scheduledItem.PerformTimerUpdate(timerState);
						}
						scheduledItem.startMs = num;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag6 = scheduledItem.ShouldUnschedule();
						if (flag6)
						{
							flag3 = true;
						}
					}
					bool flag7 = flag3 || (scheduledItem.endTimeMs > 0L && num > scheduledItem.endTimeMs);
					if (flag7)
					{
						bool flag8 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag8)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = num3;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem scheduledItem2 in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(scheduledItem2);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem scheduledItem3 in this.m_ScheduleTransactions)
				{
					this.Schedule(scheduledItem3);
				}
				this.m_ScheduleTransactions.Clear();
			}
		}

		// Token: 0x04000156 RID: 342
		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		// Token: 0x04000157 RID: 343
		private bool m_TransactionMode;

		// Token: 0x04000158 RID: 344
		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		// Token: 0x04000159 RID: 345
		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		// Token: 0x0400015A RID: 346
		internal bool disableThrottling = false;

		// Token: 0x0400015B RID: 347
		private int m_LastUpdatedIndex = -1;

		// Token: 0x0200006A RID: 106
		private class TimerEventSchedulerItem : ScheduledItem
		{
			// Token: 0x06000305 RID: 773 RVA: 0x0000B1A8 File Offset: 0x000093A8
			public TimerEventSchedulerItem(Action<TimerState> updateEvent)
			{
				this.m_TimerUpdateEvent = updateEvent;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000B1B9 File Offset: 0x000093B9
			public override void PerformTimerUpdate(TimerState state)
			{
				Action<TimerState> timerUpdateEvent = this.m_TimerUpdateEvent;
				if (timerUpdateEvent != null)
				{
					timerUpdateEvent.Invoke(state);
				}
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000B1D0 File Offset: 0x000093D0
			public override string ToString()
			{
				return this.m_TimerUpdateEvent.ToString();
			}

			// Token: 0x0400015C RID: 348
			private readonly Action<TimerState> m_TimerUpdateEvent;
		}
	}
}
