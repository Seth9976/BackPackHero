using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000006 RID: 6
	internal class ScheduledInvocationComparer : IComparer<ScheduledInvocation>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000298C File Offset: 0x00000B8C
		public int Compare(ScheduledInvocation x, ScheduledInvocation y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x == null)
			{
				return -1;
			}
			int num = x.InvocationTime.CompareTo(y.InvocationTime);
			if (num == 0)
			{
				num = x.ActionId.CompareTo(y.ActionId);
			}
			return num;
		}
	}
}
