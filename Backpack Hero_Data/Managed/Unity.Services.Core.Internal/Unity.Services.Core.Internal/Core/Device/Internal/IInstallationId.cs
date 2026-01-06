using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Device.Internal
{
	// Token: 0x0200001D RID: 29
	public interface IInstallationId : IServiceComponent
	{
		// Token: 0x0600005B RID: 91
		string GetOrCreateIdentifier();
	}
}
