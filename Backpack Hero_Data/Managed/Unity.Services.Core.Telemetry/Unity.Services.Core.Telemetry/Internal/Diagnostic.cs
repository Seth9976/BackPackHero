using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal struct Diagnostic : ITelemetryEvent
	{
		// Token: 0x04000020 RID: 32
		[JsonProperty("content")]
		public IDictionary<string, string> Content;
	}
}
