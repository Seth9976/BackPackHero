using System;
using System.Collections.Generic;

namespace Unity.Services.Analytics
{
	// Token: 0x02000007 RID: 7
	internal interface IUnstructuredEventRecorder
	{
		// Token: 0x0600002E RID: 46
		void CustomData(string eventName, IDictionary<string, object> eventParams, int? eventVersion, bool includeCommonParams, bool includePlayerIds, string callingMethodIdentifier);
	}
}
