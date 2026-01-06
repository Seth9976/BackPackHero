using System;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000002 RID: 2
	internal class CloudProjectId : ICloudProjectId, IServiceComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string GetCloudProjectId()
		{
			return Application.cloudProjectId;
		}
	}
}
