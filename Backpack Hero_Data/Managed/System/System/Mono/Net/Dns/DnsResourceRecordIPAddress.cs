using System;
using System.Net;

namespace Mono.Net.Dns
{
	// Token: 0x020000C1 RID: 193
	internal abstract class DnsResourceRecordIPAddress : DnsResourceRecord
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000B504 File Offset: 0x00009704
		internal DnsResourceRecordIPAddress(DnsResourceRecord rr, int address_size)
		{
			base.CopyFrom(rr);
			ArraySegment<byte> data = rr.Data;
			byte[] array = new byte[address_size];
			Buffer.BlockCopy(data.Array, data.Offset, array, 0, address_size);
			this.address = new IPAddress(array);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B54E File Offset: 0x0000974E
		public override string ToString()
		{
			string text = base.ToString();
			string text2 = " Address: ";
			IPAddress ipaddress = this.address;
			return text + text2 + ((ipaddress != null) ? ipaddress.ToString() : null);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B572 File Offset: 0x00009772
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x0400031F RID: 799
		private IPAddress address;
	}
}
