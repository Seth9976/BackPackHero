using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000F RID: 15
	public interface IDiagnostics
	{
		// Token: 0x06000020 RID: 32
		void SendDiagnostic(string name, string message, IDictionary<string, string> tags = null);
	}
}
