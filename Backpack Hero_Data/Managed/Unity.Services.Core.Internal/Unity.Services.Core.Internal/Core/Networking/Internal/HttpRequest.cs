using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x02000016 RID: 22
	internal class HttpRequest
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002050 File Offset: 0x00000250
		public HttpRequest()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002058 File Offset: 0x00000258
		public HttpRequest(string method, string url, Dictionary<string, string> headers, byte[] body)
		{
			this.Method = method;
			this.Url = url;
			this.Headers = headers;
			this.Body = body;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000207D File Offset: 0x0000027D
		public HttpRequest SetMethod(string method)
		{
			this.Method = method;
			return this;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002087 File Offset: 0x00000287
		public HttpRequest SetUrl(string url)
		{
			this.Url = url;
			return this;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002091 File Offset: 0x00000291
		public HttpRequest SetHeader(string key, string value)
		{
			if (this.Headers == null)
			{
				this.Headers = new Dictionary<string, string>(1);
			}
			this.Headers[key] = value;
			return this;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000020B5 File Offset: 0x000002B5
		public HttpRequest SetHeaders(Dictionary<string, string> headers)
		{
			this.Headers = headers;
			return this;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000020BF File Offset: 0x000002BF
		public HttpRequest SetBody(byte[] body)
		{
			this.Body = body;
			return this;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000020C9 File Offset: 0x000002C9
		public HttpRequest SetOptions(HttpOptions options)
		{
			this.Options = options;
			return this;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000020D3 File Offset: 0x000002D3
		public HttpRequest SetRedirectLimit(int redirectLimit)
		{
			this.Options.RedirectLimit = redirectLimit;
			return this;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000020E2 File Offset: 0x000002E2
		public HttpRequest SetTimeOutInSeconds(int timeout)
		{
			this.Options.RequestTimeoutInSeconds = timeout;
			return this;
		}

		// Token: 0x0400000E RID: 14
		public string Method;

		// Token: 0x0400000F RID: 15
		public string Url;

		// Token: 0x04000010 RID: 16
		public Dictionary<string, string> Headers;

		// Token: 0x04000011 RID: 17
		public byte[] Body;

		// Token: 0x04000012 RID: 18
		public HttpOptions Options;
	}
}
