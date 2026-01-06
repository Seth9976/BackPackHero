using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000067 RID: 103
	internal abstract class ScheduledItem
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000AB50 File Offset: 0x00008D50
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000AB58 File Offset: 0x00008D58
		public long startMs { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000AB61 File Offset: 0x00008D61
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000AB69 File Offset: 0x00008D69
		public long delayMs { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000AB72 File Offset: 0x00008D72
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000AB7A File Offset: 0x00008D7A
		public long intervalMs { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000AB83 File Offset: 0x00008D83
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000AB8B File Offset: 0x00008D8B
		public long endTimeMs { get; private set; }

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AB94 File Offset: 0x00008D94
		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000ABBF File Offset: 0x00008DBF
		public void SetDuration(long durationMs)
		{
			this.endTimeMs = this.startMs + durationMs;
		}

		// Token: 0x060002F4 RID: 756
		public abstract void PerformTimerUpdate(TimerState state);

		// Token: 0x060002F5 RID: 757 RVA: 0x000020E6 File Offset: 0x000002E6
		internal virtual void OnItemUnscheduled()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition.Invoke();
		}

		// Token: 0x0400014E RID: 334
		public Func<bool> timerUpdateStopCondition;

		// Token: 0x0400014F RID: 335
		public static readonly Func<bool> OnceCondition = () => true;

		// Token: 0x04000150 RID: 336
		public static readonly Func<bool> ForeverCondition = () => false;
	}
}
