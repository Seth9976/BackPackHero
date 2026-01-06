using System;
using System.Threading.Tasks;

namespace Unity.Services.Vivox.Internal
{
	// Token: 0x02000008 RID: 8
	public interface IVivoxTokenProviderInternal
	{
		// Token: 0x06000010 RID: 16
		Task<string> GetTokenAsync(string issuer = null, TimeSpan? expiration = null, string userUri = null, string action = null, string conferenceUri = null, string fromUserUri = null, string realm = null);
	}
}
