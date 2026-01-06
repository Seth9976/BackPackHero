using System;
using System.Threading.Tasks;

namespace Unity.Services.Core
{
	// Token: 0x02000009 RID: 9
	internal interface IUnityServices
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29
		ServicesInitializationState State { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30
		InitializationOptions Options { get; }

		// Token: 0x0600001F RID: 31
		Task InitializeAsync(InitializationOptions options);
	}
}
