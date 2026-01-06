using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200024D RID: 589
	[VisibleToOtherModules]
	internal class SystemClock
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00029614 File Offset: 0x00027814
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0002962C File Offset: 0x0002782C
		public static long ToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0002965C File Offset: 0x0002785C
		public static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalSeconds);
		}

		// Token: 0x04000870 RID: 2160
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);
	}
}
