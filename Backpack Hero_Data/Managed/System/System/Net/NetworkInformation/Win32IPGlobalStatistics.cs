using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200053C RID: 1340
	internal class Win32IPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009C386 File Offset: 0x0009A586
		public Win32IPGlobalStatistics(Win32_MIB_IPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0009C395 File Offset: 0x0009A595
		public override int DefaultTtl
		{
			get
			{
				return this.info.DefaultTTL;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0009C3A2 File Offset: 0x0009A5A2
		public override bool ForwardingEnabled
		{
			get
			{
				return this.info.Forwarding != 0;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0009C3B2 File Offset: 0x0009A5B2
		public override int NumberOfInterfaces
		{
			get
			{
				return this.info.NumIf;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x0009C3BF File Offset: 0x0009A5BF
		public override int NumberOfIPAddresses
		{
			get
			{
				return this.info.NumAddr;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0009C3CC File Offset: 0x0009A5CC
		public override int NumberOfRoutes
		{
			get
			{
				return this.info.NumRoutes;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x0009C3D9 File Offset: 0x0009A5D9
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.info.OutRequests);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0009C3E7 File Offset: 0x0009A5E7
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.info.RoutingDiscards);
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x0009C3F5 File Offset: 0x0009A5F5
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.OutDiscards);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x0009C403 File Offset: 0x0009A603
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.info.OutNoRoutes);
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x0009C411 File Offset: 0x0009A611
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.info.FragFails);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0009C41F File Offset: 0x0009A61F
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.info.ReasmReqds);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x0009C42D File Offset: 0x0009A62D
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.info.ReasmFails);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x0009C43B File Offset: 0x0009A63B
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.info.ReasmTimeout);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x0009C449 File Offset: 0x0009A649
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.info.FragOks);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0009C457 File Offset: 0x0009A657
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.info.ReasmOks);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x0009C465 File Offset: 0x0009A665
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.info.InReceives);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0009C473 File Offset: 0x0009A673
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.info.InDelivers);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x0009C481 File Offset: 0x0009A681
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.InDiscards);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0009C48F File Offset: 0x0009A68F
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.info.ForwDatagrams);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x0009C49D File Offset: 0x0009A69D
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.info.InAddrErrors);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0009C4AB File Offset: 0x0009A6AB
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.info.InHdrErrors);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x0009C4B9 File Offset: 0x0009A6B9
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.info.InUnknownProtos);
			}
		}

		// Token: 0x0400192A RID: 6442
		private Win32_MIB_IPSTATS info;
	}
}
