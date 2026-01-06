using System;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000008 RID: 8
	internal class UtcTimeProvider : ITimeProvider
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029D9 File Offset: 0x00000BD9
		public DateTime Now
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
