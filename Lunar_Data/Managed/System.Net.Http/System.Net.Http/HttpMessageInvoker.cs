using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A specialty class that allows applications to call the <see cref="M:System.Net.Http.HttpMessageInvoker.SendAsync(System.Net.Http.HttpRequestMessage,System.Threading.CancellationToken)" /> method on an Http handler chain. </summary>
	// Token: 0x02000026 RID: 38
	public class HttpMessageInvoker : IDisposable
	{
		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		// Token: 0x0600012A RID: 298 RVA: 0x000056ED File Offset: 0x000038ED
		public HttpMessageInvoker(HttpMessageHandler handler)
			: this(handler, true)
		{
		}

		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler">true if the inner handler should be disposed of by Dispose(),false if you intend to reuse the inner handler.</param>
		// Token: 0x0600012B RID: 299 RVA: 0x000056F7 File Offset: 0x000038F7
		public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.handler = handler;
			this.disposeHandler = disposeHandler;
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" />.</summary>
		// Token: 0x0600012C RID: 300 RVA: 0x0000571B File Offset: 0x0000391B
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x0600012D RID: 301 RVA: 0x00005724 File Offset: 0x00003924
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeHandler && this.handler != null)
			{
				this.handler.Dispose();
				this.handler = null;
			}
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		// Token: 0x0600012E RID: 302 RVA: 0x0000574B File Offset: 0x0000394B
		public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this.handler.SendAsync(request, cancellationToken);
		}

		// Token: 0x040000A9 RID: 169
		private protected HttpMessageHandler handler;

		// Token: 0x040000AA RID: 170
		private readonly bool disposeHandler;
	}
}
