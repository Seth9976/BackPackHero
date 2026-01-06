using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000009 RID: 9
	internal class DisabledDiagnosticsFactory : IDiagnosticsFactory, IServiceComponent
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000244F File Offset: 0x0000064F
		IReadOnlyDictionary<string, string> IDiagnosticsFactory.CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x0600001E RID: 30 RVA: 0x00002457 File Offset: 0x00000657
		IDiagnostics IDiagnosticsFactory.Create(string packageName)
		{
			return new DisabledDiagnostics();
		}
	}
}
