using System;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum AnalyticsEventPriority
	{
		// Token: 0x04000027 RID: 39
		FlushQueueFlag = 1,
		// Token: 0x04000028 RID: 40
		CacheImmediatelyFlag = 2,
		// Token: 0x04000029 RID: 41
		AllowInStopModeFlag = 4,
		// Token: 0x0400002A RID: 42
		SendImmediateFlag = 8,
		// Token: 0x0400002B RID: 43
		NoCachingFlag = 16,
		// Token: 0x0400002C RID: 44
		NoRetryFlag = 32,
		// Token: 0x0400002D RID: 45
		NormalPriorityEvent = 0,
		// Token: 0x0400002E RID: 46
		NormalPriorityEvent_WithCaching = 2,
		// Token: 0x0400002F RID: 47
		NormalPriorityEvent_NoRetryNoCaching = 48,
		// Token: 0x04000030 RID: 48
		HighPriorityEvent = 1,
		// Token: 0x04000031 RID: 49
		HighPriorityEvent_InStopMode = 5,
		// Token: 0x04000032 RID: 50
		HighestPriorityEvent = 9,
		// Token: 0x04000033 RID: 51
		HighestPriorityEvent_NoRetryNoCaching = 49
	}
}
