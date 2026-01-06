using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023B RID: 571
	internal class SNISMUXHeader
	{
		// Token: 0x040012C8 RID: 4808
		public const int HEADER_LENGTH = 16;

		// Token: 0x040012C9 RID: 4809
		public byte SMID;

		// Token: 0x040012CA RID: 4810
		public byte flags;

		// Token: 0x040012CB RID: 4811
		public ushort sessionId;

		// Token: 0x040012CC RID: 4812
		public uint length;

		// Token: 0x040012CD RID: 4813
		public uint sequenceNumber;

		// Token: 0x040012CE RID: 4814
		public uint highwater;
	}
}
