using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054E RID: 1358
	internal struct Win32_FIXED_INFO
	{
		// Token: 0x0400197D RID: 6525
		private const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x0400197E RID: 6526
		private const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x0400197F RID: 6527
		private const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x04001980 RID: 6528
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string HostName;

		// Token: 0x04001981 RID: 6529
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string DomainName;

		// Token: 0x04001982 RID: 6530
		public IntPtr CurrentDnsServer;

		// Token: 0x04001983 RID: 6531
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x04001984 RID: 6532
		public NetBiosNodeType NodeType;

		// Token: 0x04001985 RID: 6533
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string ScopeId;

		// Token: 0x04001986 RID: 6534
		public uint EnableRouting;

		// Token: 0x04001987 RID: 6535
		public uint EnableProxy;

		// Token: 0x04001988 RID: 6536
		public uint EnableDns;
	}
}
