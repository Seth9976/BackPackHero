using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x02000018 RID: 24
	internal class HttpResponse
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002166 File Offset: 0x00000366
		public HttpResponse SetRequest(HttpRequest request)
		{
			this.Request = new ReadOnlyHttpRequest(request);
			return this;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002175 File Offset: 0x00000375
		public HttpResponse SetRequest(ReadOnlyHttpRequest request)
		{
			this.Request = request;
			return this;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000217F File Offset: 0x0000037F
		public HttpResponse SetHeader(string key, string value)
		{
			this.Headers[key] = value;
			return this;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000218F File Offset: 0x0000038F
		public HttpResponse SetHeaders(Dictionary<string, string> headers)
		{
			this.Headers = headers;
			return this;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002199 File Offset: 0x00000399
		public HttpResponse SetData(byte[] data)
		{
			this.Data = data;
			return this;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000021A3 File Offset: 0x000003A3
		public HttpResponse SetStatusCode(long statusCode)
		{
			this.StatusCode = statusCode;
			return this;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000021AD File Offset: 0x000003AD
		public HttpResponse SetErrorMessage(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
			return this;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000021B7 File Offset: 0x000003B7
		public HttpResponse SetIsHttpError(bool isHttpError)
		{
			this.IsHttpError = isHttpError;
			return this;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000021C1 File Offset: 0x000003C1
		public HttpResponse SetIsNetworkError(bool isNetworkError)
		{
			this.IsNetworkError = isNetworkError;
			return this;
		}

		// Token: 0x04000013 RID: 19
		public ReadOnlyHttpRequest Request;

		// Token: 0x04000014 RID: 20
		public Dictionary<string, string> Headers;

		// Token: 0x04000015 RID: 21
		public byte[] Data;

		// Token: 0x04000016 RID: 22
		public long StatusCode;

		// Token: 0x04000017 RID: 23
		public string ErrorMessage;

		// Token: 0x04000018 RID: 24
		public bool IsHttpError;

		// Token: 0x04000019 RID: 25
		public bool IsNetworkError;
	}
}
