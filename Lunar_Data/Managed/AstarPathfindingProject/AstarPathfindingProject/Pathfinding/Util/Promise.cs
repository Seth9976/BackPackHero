using System;
using Unity.Jobs;

namespace Pathfinding.Util
{
	// Token: 0x0200027C RID: 636
	public struct Promise<T> : IProgress, IDisposable where T : IProgress, IDisposable
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0005CFD7 File Offset: 0x0005B1D7
		public Promise(JobHandle handle, T result)
		{
			this.handle = handle;
			this.result = result;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0005CFE7 File Offset: 0x0005B1E7
		public bool IsCompleted
		{
			get
			{
				return this.handle.IsCompleted;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0005CFF4 File Offset: 0x0005B1F4
		public float Progress
		{
			get
			{
				return this.result.Progress;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0005D007 File Offset: 0x0005B207
		public T GetValue()
		{
			return this.result;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0005D00F File Offset: 0x0005B20F
		public T Complete()
		{
			this.handle.Complete();
			return this.result;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0005D022 File Offset: 0x0005B222
		public void Dispose()
		{
			this.Complete();
			this.result.Dispose();
		}

		// Token: 0x04000B4A RID: 2890
		public JobHandle handle;

		// Token: 0x04000B4B RID: 2891
		private T result;
	}
}
