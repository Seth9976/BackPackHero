using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A type for HTTP handlers that delegate the processing of HTTP response messages to another handler, called the inner handler.</summary>
	// Token: 0x02000013 RID: 19
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class.</summary>
		// Token: 0x060000BC RID: 188 RVA: 0x00003AC0 File Offset: 0x00001CC0
		protected DelegatingHandler()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		// Token: 0x060000BD RID: 189 RVA: 0x00003AC8 File Offset: 0x00001CC8
		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
			if (innerHandler == null)
			{
				throw new ArgumentNullException("innerHandler");
			}
			this.InnerHandler = innerHandler;
		}

		/// <summary>Gets or sets the inner handler which processes the HTTP response messages.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMessageHandler" />.The inner handler for HTTP response messages.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003AE5 File Offset: 0x00001CE5
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003AED File Offset: 0x00001CED
		public HttpMessageHandler InnerHandler
		{
			get
			{
				return this.handler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("InnerHandler");
				}
				this.handler = value;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.DelegatingHandler" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x060000C0 RID: 192 RVA: 0x00003B04 File Offset: 0x00001D04
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.InnerHandler != null)
				{
					this.InnerHandler.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token to cancel operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		// Token: 0x060000C1 RID: 193 RVA: 0x00003B32 File Offset: 0x00001D32
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (this.InnerHandler == null)
			{
				throw new InvalidOperationException("The inner handler has not been assigned.");
			}
			return this.InnerHandler.SendAsync(request, cancellationToken);
		}

		// Token: 0x04000051 RID: 81
		private bool disposed;

		// Token: 0x04000052 RID: 82
		private HttpMessageHandler handler;
	}
}
