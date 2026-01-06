using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000541 RID: 1345
	internal class Win32IPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06002B2A RID: 11050 RVA: 0x0009C8A8 File Offset: 0x0009AAA8
		public Win32IPv4InterfaceStatistics(Win32_MIB_IFROW info)
		{
			this.info = info;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x0009C8B7 File Offset: 0x0009AAB7
		public override long BytesReceived
		{
			get
			{
				return (long)this.info.InOctets;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x0009C8C5 File Offset: 0x0009AAC5
		public override long BytesSent
		{
			get
			{
				return (long)this.info.OutOctets;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x0009C8D3 File Offset: 0x0009AAD3
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.info.InDiscards;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x0009C8E1 File Offset: 0x0009AAE1
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.info.InErrors;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0009C8EF File Offset: 0x0009AAEF
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.info.InUnknownProtos;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x0009C8FD File Offset: 0x0009AAFD
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InNUcastPkts;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x0009C90B File Offset: 0x0009AB0B
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutNUcastPkts;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0009C919 File Offset: 0x0009AB19
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.info.OutDiscards;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0009C927 File Offset: 0x0009AB27
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.info.OutErrors;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x0009C935 File Offset: 0x0009AB35
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.info.OutQLen;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x0009C943 File Offset: 0x0009AB43
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InUcastPkts;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x0009C951 File Offset: 0x0009AB51
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutUcastPkts;
			}
		}

		// Token: 0x0400194C RID: 6476
		private Win32_MIB_IFROW info;
	}
}
