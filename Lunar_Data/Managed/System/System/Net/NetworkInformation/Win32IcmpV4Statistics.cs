using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000543 RID: 1347
	internal class Win32IcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06002B3A RID: 11066 RVA: 0x0009C988 File Offset: 0x0009AB88
		public Win32IcmpV4Statistics(Win32_MIBICMPINFO info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x0009C9A8 File Offset: 0x0009ABA8
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMaskReps);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x0009C9B6 File Offset: 0x0009ABB6
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMaskReps);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x0009C9C4 File Offset: 0x0009ABC4
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMasks);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0009C9D2 File Offset: 0x0009ABD2
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMasks);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x0009C9E0 File Offset: 0x0009ABE0
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.DestUnreachs);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0009C9EE File Offset: 0x0009ABEE
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.DestUnreachs);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.EchoReps);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x0009CA0A File Offset: 0x0009AC0A
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.EchoReps);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x0009CA18 File Offset: 0x0009AC18
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Echos);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x0009CA26 File Offset: 0x0009AC26
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Echos);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x0009CA34 File Offset: 0x0009AC34
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x0009CA42 File Offset: 0x0009AC42
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0009CA50 File Offset: 0x0009AC50
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0009CA5E File Offset: 0x0009AC5E
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x0009CA6C File Offset: 0x0009AC6C
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.ParmProbs);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x0009CA7A File Offset: 0x0009AC7A
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.ParmProbs);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x0009CA88 File Offset: 0x0009AC88
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Redirects);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x0009CA96 File Offset: 0x0009AC96
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Redirects);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.iin.SrcQuenchs);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x0009CAB2 File Offset: 0x0009ACB2
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.iout.SrcQuenchs);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x0009CAC0 File Offset: 0x0009ACC0
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimeExcds);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x0009CACE File Offset: 0x0009ACCE
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimeExcds);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x0009CADC File Offset: 0x0009ACDC
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimestampReps);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x0009CAEA File Offset: 0x0009ACEA
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimestampReps);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0009CAF8 File Offset: 0x0009ACF8
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Timestamps);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x0009CB06 File Offset: 0x0009AD06
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Timestamps);
			}
		}

		// Token: 0x0400194E RID: 6478
		private Win32_MIBICMPSTATS iin;

		// Token: 0x0400194F RID: 6479
		private Win32_MIBICMPSTATS iout;
	}
}
