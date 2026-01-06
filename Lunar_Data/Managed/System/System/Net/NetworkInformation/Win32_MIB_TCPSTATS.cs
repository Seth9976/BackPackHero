using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055E RID: 1374
	internal struct Win32_MIB_TCPSTATS
	{
		// Token: 0x04001A0F RID: 6671
		public uint RtoAlgorithm;

		// Token: 0x04001A10 RID: 6672
		public uint RtoMin;

		// Token: 0x04001A11 RID: 6673
		public uint RtoMax;

		// Token: 0x04001A12 RID: 6674
		public uint MaxConn;

		// Token: 0x04001A13 RID: 6675
		public uint ActiveOpens;

		// Token: 0x04001A14 RID: 6676
		public uint PassiveOpens;

		// Token: 0x04001A15 RID: 6677
		public uint AttemptFails;

		// Token: 0x04001A16 RID: 6678
		public uint EstabResets;

		// Token: 0x04001A17 RID: 6679
		public uint CurrEstab;

		// Token: 0x04001A18 RID: 6680
		public uint InSegs;

		// Token: 0x04001A19 RID: 6681
		public uint OutSegs;

		// Token: 0x04001A1A RID: 6682
		public uint RetransSegs;

		// Token: 0x04001A1B RID: 6683
		public uint InErrs;

		// Token: 0x04001A1C RID: 6684
		public uint OutRsts;

		// Token: 0x04001A1D RID: 6685
		public uint NumConns;
	}
}
