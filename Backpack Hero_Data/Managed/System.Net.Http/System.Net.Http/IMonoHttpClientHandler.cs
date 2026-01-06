using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000C RID: 12
	internal interface IMonoHttpClientHandler : IDisposable
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83
		bool SupportsAutomaticDecompression { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		bool UseCookies { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		CookieContainer CookieContainer { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		SslClientAuthenticationOptions SslOptions { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		DecompressionMethods AutomaticDecompression { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		bool UseProxy { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		IWebProxy Proxy { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		ICredentials DefaultProxyCredentials { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		bool PreAuthenticate { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		ICredentials Credentials { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		bool AllowAutoRedirect { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		int MaxAutomaticRedirections { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		int MaxConnectionsPerServer { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		int MaxResponseHeadersLength { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		long MaxRequestContentBufferSize { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000070 RID: 112
		IDictionary<string, object> Properties { get; }

		// Token: 0x06000071 RID: 113
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x06000072 RID: 114
		void SetWebRequestTimeout(TimeSpan timeout);
	}
}
