using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000014 RID: 20
	public interface IActionScheduler : IServiceComponent
	{
		// Token: 0x0600002A RID: 42
		long ScheduleAction(Action action, double delaySeconds = 0.0);

		// Token: 0x0600002B RID: 43
		void CancelAction(long actionId);
	}
}
