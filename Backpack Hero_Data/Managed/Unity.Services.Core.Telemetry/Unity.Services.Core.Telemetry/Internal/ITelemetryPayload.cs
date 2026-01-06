using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000016 RID: 22
	internal interface ITelemetryPayload
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85
		Dictionary<string, string> CommonTags { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86
		int Count { get; }

		// Token: 0x06000057 RID: 87
		void Add(ITelemetryEvent telemetryEvent);
	}
}
