using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Internal
{
	// Token: 0x02000005 RID: 5
	[RequireImplementors]
	public interface IWire : IServiceComponent
	{
		// Token: 0x0600000E RID: 14
		IChannel CreateChannel(IChannelTokenProvider tokenProvider);
	}
}
