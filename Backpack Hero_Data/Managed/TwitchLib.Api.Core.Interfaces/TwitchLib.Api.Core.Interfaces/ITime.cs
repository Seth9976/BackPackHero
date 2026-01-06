using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000008 RID: 8
	public interface ITime
	{
		// Token: 0x06000021 RID: 33
		DateTime GetTimeNow();

		// Token: 0x06000022 RID: 34
		Task GetDelay(TimeSpan timespan, CancellationToken cancellationToken);
	}
}
