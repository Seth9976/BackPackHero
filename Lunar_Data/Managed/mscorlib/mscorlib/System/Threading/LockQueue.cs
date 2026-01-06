using System;

namespace System.Threading
{
	// Token: 0x020002F2 RID: 754
	internal class LockQueue
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x00076B1D File Offset: 0x00074D1D
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00076B2C File Offset: 0x00074D2C
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool flag3;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					flag3 = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return flag3;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00076BB0 File Offset: 0x00074DB0
		public bool IsEmpty
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.lockCount == 0;
				}
				return flag2;
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00076BF0 File Offset: 0x00074DF0
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x04001B70 RID: 7024
		private ReaderWriterLock rwlock;

		// Token: 0x04001B71 RID: 7025
		private int lockCount;
	}
}
