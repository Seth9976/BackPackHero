using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Core.Analytics.Internal
{
	// Token: 0x02000021 RID: 33
	[RequireImplementors]
	public interface IAnalyticsStandardEventComponent : IServiceComponent
	{
		// Token: 0x06000064 RID: 100
		void Record(string eventName, IDictionary<string, object> eventParameters, int eventVersion, string packageName);
	}
}
