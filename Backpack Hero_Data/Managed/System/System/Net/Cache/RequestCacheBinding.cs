using System;

namespace System.Net.Cache
{
	// Token: 0x02000594 RID: 1428
	internal class RequestCacheBinding
	{
		// Token: 0x06002D31 RID: 11569 RVA: 0x0009FEBF File Offset: 0x0009E0BF
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x0009FEDC File Offset: 0x0009E0DC
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x0009FEE4 File Offset: 0x0009E0E4
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x0009FEEC File Offset: 0x0009E0EC
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04001AAA RID: 6826
		private RequestCache m_RequestCache;

		// Token: 0x04001AAB RID: 6827
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x04001AAC RID: 6828
		private RequestCachePolicy m_Policy;
	}
}
