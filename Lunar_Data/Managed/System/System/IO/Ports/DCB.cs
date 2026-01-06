using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x02000850 RID: 2128
	[StructLayout(LayoutKind.Sequential)]
	internal class DCB
	{
		// Token: 0x060043DD RID: 17373 RVA: 0x000EB104 File Offset: 0x000E9304
		public void SetValues(int baud_rate, Parity parity, int byte_size, StopBits sb, Handshake hs)
		{
			switch (sb)
			{
			case StopBits.One:
				this.stop_bits = 0;
				break;
			case StopBits.Two:
				this.stop_bits = 2;
				break;
			case StopBits.OnePointFive:
				this.stop_bits = 1;
				break;
			}
			this.baud_rate = baud_rate;
			this.parity = (byte)parity;
			this.byte_size = (byte)byte_size;
			this.flags &= -8965;
			switch (hs)
			{
			case Handshake.None:
				break;
			case Handshake.XOnXOff:
				this.flags |= 768;
				return;
			case Handshake.RequestToSend:
				this.flags |= 8196;
				return;
			case Handshake.RequestToSendXOnXOff:
				this.flags |= 8964;
				break;
			default:
				return;
			}
		}

		// Token: 0x040028D8 RID: 10456
		public int dcb_length;

		// Token: 0x040028D9 RID: 10457
		public int baud_rate;

		// Token: 0x040028DA RID: 10458
		public int flags;

		// Token: 0x040028DB RID: 10459
		public short w_reserved;

		// Token: 0x040028DC RID: 10460
		public short xon_lim;

		// Token: 0x040028DD RID: 10461
		public short xoff_lim;

		// Token: 0x040028DE RID: 10462
		public byte byte_size;

		// Token: 0x040028DF RID: 10463
		public byte parity;

		// Token: 0x040028E0 RID: 10464
		public byte stop_bits;

		// Token: 0x040028E1 RID: 10465
		public byte xon_char;

		// Token: 0x040028E2 RID: 10466
		public byte xoff_char;

		// Token: 0x040028E3 RID: 10467
		public byte error_char;

		// Token: 0x040028E4 RID: 10468
		public byte eof_char;

		// Token: 0x040028E5 RID: 10469
		public byte evt_char;

		// Token: 0x040028E6 RID: 10470
		public short w_reserved1;

		// Token: 0x040028E7 RID: 10471
		private const int fOutxCtsFlow = 4;

		// Token: 0x040028E8 RID: 10472
		private const int fOutX = 256;

		// Token: 0x040028E9 RID: 10473
		private const int fInX = 512;

		// Token: 0x040028EA RID: 10474
		private const int fRtsControl2 = 8192;
	}
}
