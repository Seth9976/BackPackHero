using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A base type for handlers which only do some small processing of request and/or response messages.</summary>
	// Token: 0x0200002B RID: 43
	public abstract class MessageProcessingHandler : DelegatingHandler
	{
		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class.</summary>
		// Token: 0x06000168 RID: 360 RVA: 0x00005D96 File Offset: 0x00003F96
		protected MessageProcessingHandler()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		// Token: 0x06000169 RID: 361 RVA: 0x00005D9E File Offset: 0x00003F9E
		protected MessageProcessingHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}

		/// <summary>Performs processing on each request sent to the server.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpRequestMessage" />.The HTTP request message that was processed.</returns>
		/// <param name="request">The HTTP request message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		// Token: 0x0600016A RID: 362
		protected abstract HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken);

		/// <summary>Perform processing on each response from the server.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpResponseMessage" />.The HTTP response message that was processed.</returns>
		/// <param name="response">The HTTP response message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		// Token: 0x0600016B RID: 363
		protected abstract HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken);

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		// Token: 0x0600016C RID: 364 RVA: 0x00005DA8 File Offset: 0x00003FA8
		protected internal sealed override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request = this.ProcessRequest(request, cancellationToken);
			HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
			return this.ProcessResponse(httpResponseMessage, cancellationToken);
		}
	}
}
