using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x02000006 RID: 6
	public class ComposedAwaitableConstraint : IAwaitableConstraint
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002CB3 File Offset: 0x00000EB3
		internal ComposedAwaitableConstraint(IAwaitableConstraint ac1, IAwaitableConstraint ac2)
		{
			this._ac1 = ac1;
			this._ac2 = ac2;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
		{
			ComposedAwaitableConstraint.<WaitForReadiness>d__4 <WaitForReadiness>d__;
			<WaitForReadiness>d__.<>4__this = this;
			<WaitForReadiness>d__.cancellationToken = cancellationToken;
			<WaitForReadiness>d__.<>t__builder = AsyncTaskMethodBuilder<IDisposable>.Create();
			<WaitForReadiness>d__.<>1__state = -1;
			<WaitForReadiness>d__.<>t__builder.Start<ComposedAwaitableConstraint.<WaitForReadiness>d__4>(ref <WaitForReadiness>d__);
			return <WaitForReadiness>d__.<>t__builder.Task;
		}

		// Token: 0x04000011 RID: 17
		private IAwaitableConstraint _ac1;

		// Token: 0x04000012 RID: 18
		private IAwaitableConstraint _ac2;

		// Token: 0x04000013 RID: 19
		private readonly SemaphoreSlim _semafore = new SemaphoreSlim(1, 1);
	}
}
