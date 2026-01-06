using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000066 RID: 102
	internal interface IScheduler
	{
		// Token: 0x060002E3 RID: 739
		ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs);

		// Token: 0x060002E4 RID: 740
		ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition = null);

		// Token: 0x060002E5 RID: 741
		ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs);

		// Token: 0x060002E6 RID: 742
		void Unschedule(ScheduledItem item);

		// Token: 0x060002E7 RID: 743
		void Schedule(ScheduledItem item);

		// Token: 0x060002E8 RID: 744
		void UpdateScheduledEvents();
	}
}
