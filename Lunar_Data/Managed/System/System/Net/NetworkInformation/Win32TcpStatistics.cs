using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055D RID: 1373
	internal class Win32TcpStatistics : TcpStatistics
	{
		// Token: 0x06002B9A RID: 11162 RVA: 0x0009D1EC File Offset: 0x0009B3EC
		public Win32TcpStatistics(Win32_MIB_TCPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0009D1FB File Offset: 0x0009B3FB
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.info.PassiveOpens);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x0009D209 File Offset: 0x0009B409
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.info.ActiveOpens);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x0009D217 File Offset: 0x0009B417
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.info.NumConns);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x0009D225 File Offset: 0x0009B425
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.info.CurrEstab);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x0009D233 File Offset: 0x0009B433
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.info.InErrs);
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x0009D241 File Offset: 0x0009B441
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.info.AttemptFails);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x0009D24F File Offset: 0x0009B44F
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.info.MaxConn);
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x0009D25D File Offset: 0x0009B45D
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMax);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x0009D26B File Offset: 0x0009B46B
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMin);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x0009D279 File Offset: 0x0009B479
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.info.EstabResets);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x0009D287 File Offset: 0x0009B487
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.info.OutRsts);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x0009D295 File Offset: 0x0009B495
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.info.InSegs);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x0009D2A3 File Offset: 0x0009B4A3
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.info.RetransSegs);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x0009D2B1 File Offset: 0x0009B4B1
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.info.OutSegs);
			}
		}

		// Token: 0x04001A0E RID: 6670
		private Win32_MIB_TCPSTATS info;
	}
}
