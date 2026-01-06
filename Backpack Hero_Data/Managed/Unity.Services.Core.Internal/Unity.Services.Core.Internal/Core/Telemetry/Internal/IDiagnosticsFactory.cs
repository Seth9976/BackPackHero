using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000011 RID: 17
	public interface IDiagnosticsFactory : IServiceComponent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35
		IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x06000024 RID: 36
		IDiagnostics Create(string packageName);
	}
}
