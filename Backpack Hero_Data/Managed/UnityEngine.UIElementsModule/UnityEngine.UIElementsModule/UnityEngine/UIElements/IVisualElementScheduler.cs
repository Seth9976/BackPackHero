using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000ED RID: 237
	public interface IVisualElementScheduler
	{
		// Token: 0x06000768 RID: 1896
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		// Token: 0x06000769 RID: 1897
		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}
