using System;

namespace System.Net
{
	// Token: 0x020003EC RID: 1004
	internal static class IntPtrHelper
	{
		// Token: 0x060020AA RID: 8362 RVA: 0x000778A4 File Offset: 0x00075AA4
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000778B4 File Offset: 0x00075AB4
		internal static long Subtract(IntPtr a, IntPtr b)
		{
			return (long)a - (long)b;
		}
	}
}
