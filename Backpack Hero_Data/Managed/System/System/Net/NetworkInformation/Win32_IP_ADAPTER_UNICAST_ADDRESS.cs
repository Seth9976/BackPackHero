using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055A RID: 1370
	internal struct Win32_IP_ADAPTER_UNICAST_ADDRESS
	{
		// Token: 0x040019FF RID: 6655
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04001A00 RID: 6656
		public IntPtr Next;

		// Token: 0x04001A01 RID: 6657
		public Win32_SOCKET_ADDRESS Address;

		// Token: 0x04001A02 RID: 6658
		public PrefixOrigin PrefixOrigin;

		// Token: 0x04001A03 RID: 6659
		public SuffixOrigin SuffixOrigin;

		// Token: 0x04001A04 RID: 6660
		public DuplicateAddressDetectionState DadState;

		// Token: 0x04001A05 RID: 6661
		public uint ValidLifetime;

		// Token: 0x04001A06 RID: 6662
		public uint PreferredLifetime;

		// Token: 0x04001A07 RID: 6663
		public uint LeaseLifetime;

		// Token: 0x04001A08 RID: 6664
		public byte OnLinkPrefixLength;
	}
}
