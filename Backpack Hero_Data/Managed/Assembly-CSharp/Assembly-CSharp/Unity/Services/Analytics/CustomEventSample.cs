using System;
using System.Collections.Generic;

namespace Unity.Services.Analytics
{
	// Token: 0x020001AF RID: 431
	public static class CustomEventSample
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x000A1217 File Offset: 0x0009F417
		public static void RecordCustomEventWithNoParameters()
		{
			AnalyticsService.Instance.CustomData("myEvent", new Dictionary<string, object>());
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000A1230 File Offset: 0x0009F430
		public static void RecordCustomEventWithParameters()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "fabulousString", "hello there" },
				{ "sparklingInt", 1337 },
				{ "tremendousLong", long.MaxValue },
				{ "spectacularFloat", 0.451f },
				{ "incredibleDouble", 3.1337E-17 },
				{ "peculiarBool", true },
				{
					"ultimateTimestamp",
					DateTime.UtcNow
				}
			};
			AnalyticsService.Instance.CustomData("myEvent", dictionary);
		}
	}
}
