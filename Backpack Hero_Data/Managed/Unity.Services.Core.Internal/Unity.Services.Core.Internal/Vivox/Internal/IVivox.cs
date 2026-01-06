using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Vivox.Internal
{
	// Token: 0x02000007 RID: 7
	[RequireImplementors]
	public interface IVivox : IServiceComponent
	{
		// Token: 0x0600000F RID: 15
		void RegisterTokenProvider(IVivoxTokenProviderInternal tokenProvider);
	}
}
