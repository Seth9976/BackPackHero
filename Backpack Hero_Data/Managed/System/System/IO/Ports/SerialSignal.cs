using System;

namespace System.IO.Ports
{
	// Token: 0x0200084D RID: 2125
	internal enum SerialSignal
	{
		// Token: 0x040028AC RID: 10412
		None,
		// Token: 0x040028AD RID: 10413
		Cd,
		// Token: 0x040028AE RID: 10414
		Cts,
		// Token: 0x040028AF RID: 10415
		Dsr = 4,
		// Token: 0x040028B0 RID: 10416
		Dtr = 8,
		// Token: 0x040028B1 RID: 10417
		Rts = 16
	}
}
