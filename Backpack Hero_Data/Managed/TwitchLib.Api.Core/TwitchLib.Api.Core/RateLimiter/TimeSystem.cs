using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x0200000C RID: 12
	public class TimeSystem : ITime
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000310B File Offset: 0x0000130B
		public static ITime StandardTime { get; } = new TimeSystem();

		// Token: 0x06000058 RID: 88 RVA: 0x0000311E File Offset: 0x0000131E
		private TimeSystem()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003126 File Offset: 0x00001326
		DateTime ITime.GetTimeNow()
		{
			return DateTime.Now;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000312D File Offset: 0x0000132D
		Task ITime.GetDelay(TimeSpan timespan, CancellationToken cancellationToken)
		{
			return Task.Delay(timespan, cancellationToken);
		}
	}
}
