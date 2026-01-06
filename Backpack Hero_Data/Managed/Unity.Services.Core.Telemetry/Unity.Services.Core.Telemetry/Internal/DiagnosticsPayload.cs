using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	internal struct DiagnosticsPayload : ITelemetryPayload
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002EC9 File Offset: 0x000010C9
		Dictionary<string, string> ITelemetryPayload.CommonTags
		{
			get
			{
				return this.CommonTags;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002ED1 File Offset: 0x000010D1
		int ITelemetryPayload.Count
		{
			get
			{
				List<Diagnostic> diagnostics = this.Diagnostics;
				if (diagnostics == null)
				{
					return 0;
				}
				return diagnostics.Count;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EE4 File Offset: 0x000010E4
		void ITelemetryPayload.Add(ITelemetryEvent telemetryEvent)
		{
			if (telemetryEvent is Diagnostic)
			{
				Diagnostic diagnostic = (Diagnostic)telemetryEvent;
				if (this.Diagnostics == null)
				{
					this.Diagnostics = new List<Diagnostic>(1);
				}
				this.Diagnostics.Add(diagnostic);
				return;
			}
			throw new ArgumentException("This payload accepts only Diagnostic events.");
		}

		// Token: 0x04000021 RID: 33
		[JsonProperty("diagnostics")]
		public List<Diagnostic> Diagnostics;

		// Token: 0x04000022 RID: 34
		[JsonProperty("commonTags")]
		public Dictionary<string, string> CommonTags;

		// Token: 0x04000023 RID: 35
		[JsonProperty("diagnosticsCommonTags")]
		public Dictionary<string, string> DiagnosticsCommonTags;
	}
}
