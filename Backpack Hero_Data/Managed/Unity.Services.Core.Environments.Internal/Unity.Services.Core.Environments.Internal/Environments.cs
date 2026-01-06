using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Environments.Internal
{
	// Token: 0x02000002 RID: 2
	internal class Environments : IEnvironments, IServiceComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string Current { get; internal set; }
	}
}
