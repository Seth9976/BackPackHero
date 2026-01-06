using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000561 RID: 1377
	internal class Win32UnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x0009D314 File Offset: 0x0009B514
		public Win32UnicastIPAddressInformation(Win32_IP_ADAPTER_UNICAST_ADDRESS info)
		{
			this.info = info;
			IPAddress ipaddress = info.Address.GetIPAddress();
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = Win32UnicastIPAddressInformation.PrefixLengthToSubnetMask(info.OnLinkPrefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x0009D35B File Offset: 0x0009B55B
		public override IPAddress Address
		{
			get
			{
				return this.info.Address.GetIPAddress();
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x0009D36D File Offset: 0x0009B56D
		public override bool IsDnsEligible
		{
			get
			{
				return this.info.LengthFlags.IsDnsEligible;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x0009D37F File Offset: 0x0009B57F
		public override bool IsTransient
		{
			get
			{
				return this.info.LengthFlags.IsTransient;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x0009D391 File Offset: 0x0009B591
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.info.PreferredLifetime);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x0009D39F File Offset: 0x0009B59F
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.info.ValidLifetime);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x0009D3AD File Offset: 0x0009B5AD
		public override long DhcpLeaseLifetime
		{
			get
			{
				return (long)((ulong)this.info.LeaseLifetime);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x0009D3BB File Offset: 0x0009B5BB
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.info.DadState;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x0009D3C8 File Offset: 0x0009B5C8
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x0009D3E4 File Offset: 0x0009B5E4
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.info.PrefixOrigin;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x0009D3F1 File Offset: 0x0009B5F1
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.info.SuffixOrigin;
			}
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0009D400 File Offset: 0x0009B600
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04001A24 RID: 6692
		private Win32_IP_ADAPTER_UNICAST_ADDRESS info;

		// Token: 0x04001A25 RID: 6693
		private IPAddress ipv4Mask;
	}
}
