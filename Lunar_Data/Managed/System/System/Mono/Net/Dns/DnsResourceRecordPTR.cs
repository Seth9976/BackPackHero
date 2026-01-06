using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C2 RID: 194
	internal class DnsResourceRecordPTR : DnsResourceRecord
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000B57C File Offset: 0x0000977C
		internal DnsResourceRecordPTR(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.dname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public string DName
		{
			get
			{
				return this.dname;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B5C8 File Offset: 0x000097C8
		public override string ToString()
		{
			return base.ToString() + " DNAME: " + this.dname.ToString();
		}

		// Token: 0x04000320 RID: 800
		private string dname;
	}
}
