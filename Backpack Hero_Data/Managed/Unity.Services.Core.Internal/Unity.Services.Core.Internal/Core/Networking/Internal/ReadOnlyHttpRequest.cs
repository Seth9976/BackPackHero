using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001A RID: 26
	internal struct ReadOnlyHttpRequest
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000021D3 File Offset: 0x000003D3
		public ReadOnlyHttpRequest(HttpRequest request)
		{
			this.m_Request = request;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000021DC File Offset: 0x000003DC
		public string Method
		{
			get
			{
				return this.m_Request.Method;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000021E9 File Offset: 0x000003E9
		public string Url
		{
			get
			{
				return this.m_Request.Url;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000021F6 File Offset: 0x000003F6
		public IReadOnlyDictionary<string, string> Headers
		{
			get
			{
				return this.m_Request.Headers;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002203 File Offset: 0x00000403
		public byte[] Body
		{
			get
			{
				return this.m_Request.Body;
			}
		}

		// Token: 0x0400001A RID: 26
		private HttpRequest m_Request;
	}
}
