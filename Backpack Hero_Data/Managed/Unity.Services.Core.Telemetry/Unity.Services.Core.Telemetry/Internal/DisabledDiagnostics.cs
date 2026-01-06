using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000008 RID: 8
	internal class DisabledDiagnostics : IDiagnostics
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002445 File Offset: 0x00000645
		void IDiagnostics.SendDiagnostic(string name, string message, IDictionary<string, string> tags)
		{
		}
	}
}
