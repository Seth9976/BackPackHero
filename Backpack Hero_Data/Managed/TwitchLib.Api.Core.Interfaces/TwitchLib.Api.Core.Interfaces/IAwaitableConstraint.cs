using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000003 RID: 3
	public interface IAwaitableConstraint
	{
		// Token: 0x0600000F RID: 15
		Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken);
	}
}
