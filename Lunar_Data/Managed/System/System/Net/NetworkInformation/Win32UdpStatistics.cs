using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055F RID: 1375
	internal class Win32UdpStatistics : UdpStatistics
	{
		// Token: 0x06002BA9 RID: 11177 RVA: 0x0009D2BF File Offset: 0x0009B4BF
		public Win32UdpStatistics(Win32_MIB_UDPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x0009D2CE File Offset: 0x0009B4CE
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.info.InDatagrams);
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x0009D2DC File Offset: 0x0009B4DC
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.info.OutDatagrams);
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x0009D2EA File Offset: 0x0009B4EA
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.NoPorts);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x0009D2F8 File Offset: 0x0009B4F8
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.info.InErrors);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x0009D306 File Offset: 0x0009B506
		public override int UdpListeners
		{
			get
			{
				return this.info.NumAddrs;
			}
		}

		// Token: 0x04001A1E RID: 6686
		private Win32_MIB_UDPSTATS info;
	}
}
