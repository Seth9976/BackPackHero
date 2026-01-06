using System;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200018C RID: 396
	public class RWLock
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x000033F6 File Offset: 0x000015F6
		private void AddPendingSync()
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000033F6 File Offset: 0x000015F6
		private void RemovePendingSync()
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000033F6 File Offset: 0x000015F6
		private void AddPendingAsync()
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000033F6 File Offset: 0x000015F6
		private void RemovePendingAsync()
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003D077 File Offset: 0x0003B277
		public RWLock.LockSync ReadSync()
		{
			this.AddPendingSync();
			this.lastWrite.Complete();
			this.lastWrite = default(JobHandle);
			return new RWLock.LockSync(this);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0003D09C File Offset: 0x0003B29C
		public RWLock.ReadLockAsync Read()
		{
			this.AddPendingAsync();
			return new RWLock.ReadLockAsync(this, this.lastWrite);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
		public RWLock.LockSync WriteSync()
		{
			this.AddPendingSync();
			this.lastWrite.Complete();
			this.lastWrite = default(JobHandle);
			this.lastRead.Complete();
			return new RWLock.LockSync(this);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
		public RWLock.WriteLockAsync Write()
		{
			this.AddPendingAsync();
			return new RWLock.WriteLockAsync(this, JobHandle.CombineDependencies(this.lastRead, this.lastWrite));
		}

		// Token: 0x0400075E RID: 1886
		private JobHandle lastWrite;

		// Token: 0x0400075F RID: 1887
		private JobHandle lastRead;

		// Token: 0x0200018D RID: 397
		public readonly struct CombinedReadLockAsync
		{
			// Token: 0x06000AD7 RID: 2775 RVA: 0x0003D0FF File Offset: 0x0003B2FF
			public CombinedReadLockAsync(RWLock.ReadLockAsync lock1, RWLock.ReadLockAsync lock2)
			{
				this.lock1 = lock1.inner;
				this.lock2 = lock2.inner;
				this.dependency = JobHandle.CombineDependencies(lock1.dependency, lock2.dependency);
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0003D130 File Offset: 0x0003B330
			public void UnlockAfter(JobHandle handle)
			{
				if (this.lock1 != null)
				{
					this.lock1.RemovePendingAsync();
					this.lock1.lastRead = JobHandle.CombineDependencies(this.lock1.lastRead, handle);
				}
				if (this.lock2 != null)
				{
					this.lock2.RemovePendingAsync();
					this.lock2.lastRead = JobHandle.CombineDependencies(this.lock2.lastRead, handle);
				}
			}

			// Token: 0x04000760 RID: 1888
			private readonly RWLock lock1;

			// Token: 0x04000761 RID: 1889
			private readonly RWLock lock2;

			// Token: 0x04000762 RID: 1890
			public readonly JobHandle dependency;
		}

		// Token: 0x0200018E RID: 398
		public readonly struct ReadLockAsync
		{
			// Token: 0x06000AD9 RID: 2777 RVA: 0x0003D19B File Offset: 0x0003B39B
			public ReadLockAsync(RWLock inner, JobHandle dependency)
			{
				this.inner = inner;
				this.dependency = dependency;
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x0003D1AB File Offset: 0x0003B3AB
			public void UnlockAfter(JobHandle handle)
			{
				if (this.inner != null)
				{
					this.inner.RemovePendingAsync();
					this.inner.lastRead = JobHandle.CombineDependencies(this.inner.lastRead, handle);
				}
			}

			// Token: 0x04000763 RID: 1891
			internal readonly RWLock inner;

			// Token: 0x04000764 RID: 1892
			public readonly JobHandle dependency;
		}

		// Token: 0x0200018F RID: 399
		public readonly struct WriteLockAsync
		{
			// Token: 0x06000ADB RID: 2779 RVA: 0x0003D1DC File Offset: 0x0003B3DC
			public WriteLockAsync(RWLock inner, JobHandle dependency)
			{
				this.inner = inner;
				this.dependency = dependency;
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0003D1EC File Offset: 0x0003B3EC
			public void UnlockAfter(JobHandle handle)
			{
				if (this.inner != null)
				{
					this.inner.RemovePendingAsync();
					this.inner.lastWrite = handle;
				}
			}

			// Token: 0x04000765 RID: 1893
			private readonly RWLock inner;

			// Token: 0x04000766 RID: 1894
			public readonly JobHandle dependency;
		}

		// Token: 0x02000190 RID: 400
		public readonly struct LockSync
		{
			// Token: 0x06000ADD RID: 2781 RVA: 0x0003D20D File Offset: 0x0003B40D
			public LockSync(RWLock inner)
			{
				this.inner = inner;
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x0003D216 File Offset: 0x0003B416
			public void Unlock()
			{
				if (this.inner != null)
				{
					this.inner.RemovePendingSync();
				}
			}

			// Token: 0x04000767 RID: 1895
			private readonly RWLock inner;
		}
	}
}
