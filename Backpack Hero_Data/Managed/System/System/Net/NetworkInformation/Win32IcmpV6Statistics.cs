using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000547 RID: 1351
	internal class Win32IcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x0009CB14 File Offset: 0x0009AD14
		public Win32IcmpV6Statistics(Win32_MIB_ICMP_EX info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x0009CB34 File Offset: 0x0009AD34
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[1]);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x0009CB44 File Offset: 0x0009AD44
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[1]);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x0009CB54 File Offset: 0x0009AD54
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[129]);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0009CB68 File Offset: 0x0009AD68
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[129]);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x0009CB7C File Offset: 0x0009AD7C
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[128]);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0009CB90 File Offset: 0x0009AD90
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[128]);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0009CBA4 File Offset: 0x0009ADA4
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x0009CBB2 File Offset: 0x0009ADB2
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x0009CBC0 File Offset: 0x0009ADC0
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[130]);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0009CBD4 File Offset: 0x0009ADD4
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[130]);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x0009CBE8 File Offset: 0x0009ADE8
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[132]);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0009CBFC File Offset: 0x0009ADFC
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[132]);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0009CC10 File Offset: 0x0009AE10
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[131]);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x0009CC24 File Offset: 0x0009AE24
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[131]);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x0009CC38 File Offset: 0x0009AE38
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x0009CC46 File Offset: 0x0009AE46
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0009CC54 File Offset: 0x0009AE54
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[136]);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x0009CC68 File Offset: 0x0009AE68
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[136]);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x0009CC7C File Offset: 0x0009AE7C
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[135]);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x0009CC90 File Offset: 0x0009AE90
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[135]);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x0009CCA4 File Offset: 0x0009AEA4
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[2]);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x0009CCB4 File Offset: 0x0009AEB4
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[2]);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x0009CCC4 File Offset: 0x0009AEC4
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[4]);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x0009CCD4 File Offset: 0x0009AED4
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[4]);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x0009CCE4 File Offset: 0x0009AEE4
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[137]);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[137]);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x0009CD0C File Offset: 0x0009AF0C
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[134]);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x0009CD20 File Offset: 0x0009AF20
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[134]);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x0009CD34 File Offset: 0x0009AF34
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[133]);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x0009CD48 File Offset: 0x0009AF48
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[133]);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x0009CD5C File Offset: 0x0009AF5C
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[3]);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x0009CD6C File Offset: 0x0009AF6C
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[3]);
			}
		}

		// Token: 0x0400196E RID: 6510
		private Win32_MIBICMPSTATS_EX iin;

		// Token: 0x0400196F RID: 6511
		private Win32_MIBICMPSTATS_EX iout;
	}
}
