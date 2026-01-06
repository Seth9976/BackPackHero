using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001B RID: 27
	internal struct ReadOnlyHttpResponse
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002210 File Offset: 0x00000410
		public ReadOnlyHttpResponse(HttpResponse response)
		{
			this.m_Response = response;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002219 File Offset: 0x00000419
		public ReadOnlyHttpRequest Request
		{
			get
			{
				return this.m_Response.Request;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002226 File Offset: 0x00000426
		public IReadOnlyDictionary<string, string> Headers
		{
			get
			{
				return this.m_Response.Headers;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002233 File Offset: 0x00000433
		public byte[] Data
		{
			get
			{
				return this.m_Response.Data;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002240 File Offset: 0x00000440
		public long StatusCode
		{
			get
			{
				return this.m_Response.StatusCode;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000224D File Offset: 0x0000044D
		public string ErrorMessage
		{
			get
			{
				return this.m_Response.ErrorMessage;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000225A File Offset: 0x0000045A
		public bool IsHttpError
		{
			get
			{
				return this.m_Response.IsHttpError;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002267 File Offset: 0x00000467
		public bool IsNetworkError
		{
			get
			{
				return this.m_Response.IsNetworkError;
			}
		}

		// Token: 0x0400001B RID: 27
		private HttpResponse m_Response;
	}
}
