using System;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000005 RID: 5
	internal class ScheduledInvocation
	{
		// Token: 0x04000010 RID: 16
		public Action Action;

		// Token: 0x04000011 RID: 17
		public DateTime InvocationTime;

		// Token: 0x04000012 RID: 18
		public long ActionId;
	}
}
