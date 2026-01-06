using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Qos.Internal
{
	// Token: 0x02000009 RID: 9
	[RequireImplementors]
	public interface IQosResults : IServiceComponent
	{
		// Token: 0x06000011 RID: 17
		Task<IList<QosResult>> GetSortedQosResultsAsync(string service, IList<string> regions);
	}
}
