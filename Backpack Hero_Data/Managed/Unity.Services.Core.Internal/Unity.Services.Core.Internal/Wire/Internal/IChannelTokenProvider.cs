using System;
using System.Threading.Tasks;

namespace Unity.Services.Wire.Internal
{
	// Token: 0x02000004 RID: 4
	public interface IChannelTokenProvider
	{
		// Token: 0x0600000D RID: 13
		Task<ChannelToken> GetTokenAsync();
	}
}
