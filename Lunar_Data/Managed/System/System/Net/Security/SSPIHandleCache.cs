using System;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x0200065A RID: 1626
	internal static class SSPIHandleCache
	{
		// Token: 0x06003406 RID: 13318 RVA: 0x000BD044 File Offset: 0x000BB244
		internal static void CacheCredential(SafeFreeCredentials newHandle)
		{
			try
			{
				SafeCredentialReference safeCredentialReference = SafeCredentialReference.CreateReference(newHandle);
				if (safeCredentialReference != null)
				{
					int num = Interlocked.Increment(ref SSPIHandleCache.s_current) & 31;
					safeCredentialReference = Interlocked.Exchange<SafeCredentialReference>(ref SSPIHandleCache.s_cacheSlots[num], safeCredentialReference);
					if (safeCredentialReference != null)
					{
						safeCredentialReference.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionCheck.IsFatal(ex))
				{
					NetEventSource.Fail(null, "Attempted to throw: {e}", "CacheCredential");
				}
			}
		}

		// Token: 0x04001F91 RID: 8081
		private const int c_MaxCacheSize = 31;

		// Token: 0x04001F92 RID: 8082
		private static SafeCredentialReference[] s_cacheSlots = new SafeCredentialReference[32];

		// Token: 0x04001F93 RID: 8083
		private static int s_current = -1;
	}
}
