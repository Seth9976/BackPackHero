using System;

namespace System.Net.Cache
{
	// Token: 0x02000591 RID: 1425
	internal sealed class RequestCacheManager
	{
		// Token: 0x06002D28 RID: 11560 RVA: 0x0000219B File Offset: 0x0000039B
		private RequestCacheManager()
		{
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0009FD2C File Offset: 0x0009DF2C
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x0009FDA6 File Offset: 0x0009DFA6
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0009FDC8 File Offset: 0x0009DFC8
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0009FE4C File Offset: 0x0009E04C
		private static void LoadConfigSettings()
		{
			RequestCacheBinding requestCacheBinding = RequestCacheManager.s_BypassCacheBinding;
			lock (requestCacheBinding)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.s_CacheConfigSettings = new RequestCachingSectionInternal();
				}
			}
		}

		// Token: 0x04001AA4 RID: 6820
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04001AA5 RID: 6821
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04001AA6 RID: 6822
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04001AA7 RID: 6823
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04001AA8 RID: 6824
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
