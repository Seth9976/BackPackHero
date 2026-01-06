using System;
using ES3Types;

namespace ES3Internal
{
	// Token: 0x020000C7 RID: 199
	public struct ES3Data
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0001EC75 File Offset: 0x0001CE75
		public ES3Data(Type type, byte[] bytes)
		{
			this.type = ((type == null) ? null : ES3TypeMgr.GetOrCreateES3Type(type, true));
			this.bytes = bytes;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001EC97 File Offset: 0x0001CE97
		public ES3Data(ES3Type type, byte[] bytes)
		{
			this.type = type;
			this.bytes = bytes;
		}

		// Token: 0x04000105 RID: 261
		public ES3Type type;

		// Token: 0x04000106 RID: 262
		public byte[] bytes;
	}
}
