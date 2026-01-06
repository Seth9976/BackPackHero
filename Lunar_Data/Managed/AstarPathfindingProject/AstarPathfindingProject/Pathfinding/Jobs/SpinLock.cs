using System;
using System.Threading;
using Unity.Burst.Intrinsics;

namespace Pathfinding.Jobs
{
	// Token: 0x02000191 RID: 401
	public struct SpinLock
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x0003D22B File Offset: 0x0003B42B
		public void Lock()
		{
			while (Interlocked.CompareExchange(ref this.locked, 1, 0) != 0)
			{
				Common.Pause();
			}
			Thread.MemoryBarrier();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003D248 File Offset: 0x0003B448
		public void Unlock()
		{
			Thread.MemoryBarrier();
			if (Interlocked.Exchange(ref this.locked, 0) == 0)
			{
				throw new InvalidOperationException("Trying to unlock a lock which is not locked");
			}
		}

		// Token: 0x04000768 RID: 1896
		private volatile int locked;
	}
}
