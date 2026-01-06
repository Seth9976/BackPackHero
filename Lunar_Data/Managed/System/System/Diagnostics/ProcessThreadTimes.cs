using System;

namespace System.Diagnostics
{
	// Token: 0x02000243 RID: 579
	internal class ProcessThreadTimes
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004DC66 File Offset: 0x0004BE66
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004DC73 File Offset: 0x0004BE73
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004DC80 File Offset: 0x0004BE80
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004DC8D File Offset: 0x0004BE8D
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004DC9A File Offset: 0x0004BE9A
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x04000A6D RID: 2669
		internal long create;

		// Token: 0x04000A6E RID: 2670
		internal long exit;

		// Token: 0x04000A6F RID: 2671
		internal long kernel;

		// Token: 0x04000A70 RID: 2672
		internal long user;
	}
}
