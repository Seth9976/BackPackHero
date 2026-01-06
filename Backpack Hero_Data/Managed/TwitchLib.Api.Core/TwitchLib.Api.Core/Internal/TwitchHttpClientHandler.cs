using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Internal
{
	// Token: 0x0200000D RID: 13
	public class TwitchHttpClientHandler : DelegatingHandler
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003136 File Offset: 0x00001336
		public TwitchHttpClientHandler(ILogger<IHttpCallHandler> logger)
			: base(new HttpClientHandler())
		{
			this._logger = logger;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000314C File Offset: 0x0000134C
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			TwitchHttpClientHandler.<SendAsync>d__2 <SendAsync>d__;
			<SendAsync>d__.<>4__this = this;
			<SendAsync>d__.request = request;
			<SendAsync>d__.cancellationToken = cancellationToken;
			<SendAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
			<SendAsync>d__.<>1__state = -1;
			<SendAsync>d__.<>t__builder.Start<TwitchHttpClientHandler.<SendAsync>d__2>(ref <SendAsync>d__);
			return <SendAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400001E RID: 30
		private readonly ILogger<IHttpCallHandler> _logger;
	}
}
