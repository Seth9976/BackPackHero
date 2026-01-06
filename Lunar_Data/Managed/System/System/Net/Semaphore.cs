using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200044B RID: 1099
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x060022B4 RID: 8884 RVA: 0x0007F180 File Offset: 0x0007D380
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				int num;
				this.Handle = Semaphore.CreateSemaphore_internal(initialCount, maxCount, null, out num);
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007F1CC File Offset: 0x0007D3CC
		internal bool ReleaseSemaphore()
		{
			int num;
			return Semaphore.ReleaseSemaphore_internal(this.Handle, 1, out num);
		}
	}
}
