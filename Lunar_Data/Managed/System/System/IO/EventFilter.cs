using System;

namespace System.IO
{
	// Token: 0x02000830 RID: 2096
	internal enum EventFilter : short
	{
		// Token: 0x040027F5 RID: 10229
		Read = -1,
		// Token: 0x040027F6 RID: 10230
		Write = -2,
		// Token: 0x040027F7 RID: 10231
		Aio = -3,
		// Token: 0x040027F8 RID: 10232
		Vnode = -4,
		// Token: 0x040027F9 RID: 10233
		Proc = -5,
		// Token: 0x040027FA RID: 10234
		Signal = -6,
		// Token: 0x040027FB RID: 10235
		Timer = -7,
		// Token: 0x040027FC RID: 10236
		MachPort = -8,
		// Token: 0x040027FD RID: 10237
		FS = -9,
		// Token: 0x040027FE RID: 10238
		User = -10,
		// Token: 0x040027FF RID: 10239
		VM = -11
	}
}
