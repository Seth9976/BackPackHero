using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000514 RID: 1300
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x060029FB RID: 10747 RVA: 0x0009A1A4 File Offset: 0x000983A4
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009A1AC File Offset: 0x000983AC
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x0009A1BB File Offset: 0x000983BB
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x0009A1C8 File Offset: 0x000983C8
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x0009A1D5 File Offset: 0x000983D5
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x00003062 File Offset: 0x00001262
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x00003062 File Offset: 0x00001262
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00003062 File Offset: 0x00001262
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0009A1E4 File Offset: 0x000983E4
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x04001899 RID: 6297
		private SystemIPAddressInformation innerInfo;
	}
}
