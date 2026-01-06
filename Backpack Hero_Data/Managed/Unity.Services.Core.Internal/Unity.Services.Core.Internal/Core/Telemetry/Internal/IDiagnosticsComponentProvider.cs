using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000010 RID: 16
	internal interface IDiagnosticsComponentProvider
	{
		// Token: 0x06000021 RID: 33
		Task<IDiagnosticsFactory> CreateDiagnosticsComponents();

		// Token: 0x06000022 RID: 34
		Task<string> GetSerializedProjectConfigurationAsync();
	}
}
