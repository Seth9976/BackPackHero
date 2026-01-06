using System;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x02000191 RID: 401
	internal class SqlConnectionTimeoutPhaseDuration
	{
		// Token: 0x0600137D RID: 4989 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		internal void StartCapture()
		{
			this._swDuration.Start();
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0005E609 File Offset: 0x0005C809
		internal void StopCapture()
		{
			if (this._swDuration.IsRunning)
			{
				this._swDuration.Stop();
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0005E623 File Offset: 0x0005C823
		internal long GetMilliSecondDuration()
		{
			return this._swDuration.ElapsedMilliseconds;
		}

		// Token: 0x04000D15 RID: 3349
		private Stopwatch _swDuration = new Stopwatch();
	}
}
