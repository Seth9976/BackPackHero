using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000554 RID: 1364
	internal struct Win32LengthFlagsUnion
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x0009D168 File Offset: 0x0009B368
		public bool IsDnsEligible
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0009D175 File Offset: 0x0009B375
		public bool IsTransient
		{
			get
			{
				return (this.Flags & 2U) > 0U;
			}
		}

		// Token: 0x040019EC RID: 6636
		private const int IP_ADAPTER_ADDRESS_DNS_ELIGIBLE = 1;

		// Token: 0x040019ED RID: 6637
		private const int IP_ADAPTER_ADDRESS_TRANSIENT = 2;

		// Token: 0x040019EE RID: 6638
		public uint Length;

		// Token: 0x040019EF RID: 6639
		public uint Flags;
	}
}
