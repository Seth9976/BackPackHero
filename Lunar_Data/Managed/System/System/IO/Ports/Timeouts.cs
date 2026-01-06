using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x02000851 RID: 2129
	[StructLayout(LayoutKind.Sequential)]
	internal class Timeouts
	{
		// Token: 0x060043DF RID: 17375 RVA: 0x000EB1BA File Offset: 0x000E93BA
		public Timeouts(int read_timeout, int write_timeout)
		{
			this.SetValues(read_timeout, write_timeout);
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000EB1CA File Offset: 0x000E93CA
		public void SetValues(int read_timeout, int write_timeout)
		{
			this.ReadIntervalTimeout = uint.MaxValue;
			this.ReadTotalTimeoutMultiplier = uint.MaxValue;
			this.ReadTotalTimeoutConstant = (uint)((read_timeout == -1) ? (-2) : read_timeout);
			this.WriteTotalTimeoutMultiplier = 0U;
			this.WriteTotalTimeoutConstant = (uint)((write_timeout == -1) ? (-1) : write_timeout);
		}

		// Token: 0x040028EB RID: 10475
		public uint ReadIntervalTimeout;

		// Token: 0x040028EC RID: 10476
		public uint ReadTotalTimeoutMultiplier;

		// Token: 0x040028ED RID: 10477
		public uint ReadTotalTimeoutConstant;

		// Token: 0x040028EE RID: 10478
		public uint WriteTotalTimeoutMultiplier;

		// Token: 0x040028EF RID: 10479
		public uint WriteTotalTimeoutConstant;

		// Token: 0x040028F0 RID: 10480
		public const uint MaxDWord = 4294967295U;
	}
}
