using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI. </summary>
	// Token: 0x02000015 RID: 21
	public class HttpClient : HttpMessageInvoker
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class.</summary>
		// Token: 0x060000C5 RID: 197 RVA: 0x00003C45 File Offset: 0x00001E45
		public HttpClient()
			: this(new HttpClientHandler(), true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class with a specific handler.</summary>
		/// <param name="handler">The HTTP handler stack to use for sending requests. </param>
		// Token: 0x060000C6 RID: 198 RVA: 0x00003C53 File Offset: 0x00001E53
		public HttpClient(HttpMessageHandler handler)
			: this(handler, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class with a specific handler.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler">true if the inner handler should be disposed of by Dispose(),false if you intend to reuse the inner handler.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00003C5D File Offset: 0x00001E5D
		public HttpClient(HttpMessageHandler handler, bool disposeHandler)
			: base(handler, disposeHandler)
		{
			this.buffer_size = 2147483647L;
			this.timeout = HttpClient.TimeoutDefault;
			this.cts = new CancellationTokenSource();
		}

		/// <summary>Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003C89 File Offset: 0x00001E89
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003C91 File Offset: 0x00001E91
		public Uri BaseAddress
		{
			get
			{
				return this.base_address;
			}
			set
			{
				this.base_address = value;
			}
		}

		/// <summary>Gets the headers which should be sent with each request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpRequestHeaders" />.The headers which should be sent with each request.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003C9C File Offset: 0x00001E9C
		public HttpRequestHeaders DefaultRequestHeaders
		{
			get
			{
				HttpRequestHeaders httpRequestHeaders;
				if ((httpRequestHeaders = this.headers) == null)
				{
					httpRequestHeaders = (this.headers = new HttpRequestHeaders());
				}
				return httpRequestHeaders;
			}
		}

		/// <summary>Gets or sets the maximum number of bytes to buffer when reading the response content.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The maximum number of bytes to buffer when reading the response content. The default value for this property is 2 gigabytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The size specified is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An operation has already been started on the current instance. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003CC1 File Offset: 0x00001EC1
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public long MaxResponseContentBufferSize
		{
			get
			{
				return this.buffer_size;
			}
			set
			{
				if (value <= 0L)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.buffer_size = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds to wait before the request times out.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The number of milliseconds to wait before the request times out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An operation has already been started on the current instance. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003CDD File Offset: 0x00001EDD
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003CE5 File Offset: 0x00001EE5
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value != global::System.Threading.Timeout.InfiniteTimeSpan && (value <= TimeSpan.Zero || value.TotalMilliseconds > 2147483647.0))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.timeout = value;
			}
		}

		/// <summary>Cancel all pending requests on this instance.</summary>
		// Token: 0x060000CF RID: 207 RVA: 0x00003D20 File Offset: 0x00001F20
		public void CancelPendingRequests()
		{
			using (CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.cts, new CancellationTokenSource()))
			{
				cancellationTokenSource.Cancel();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpClient" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x060000D0 RID: 208 RVA: 0x00003D60 File Offset: 0x00001F60
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				this.cts.Cancel();
				this.cts.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Send a DELETE request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000D1 RID: 209 RVA: 0x00003D91 File Offset: 0x00001F91
		public Task<HttpResponseMessage> DeleteAsync(string requestUri)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri));
		}

		/// <summary>Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000D2 RID: 210 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
		}

		/// <summary>Send a DELETE request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000D3 RID: 211 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri));
		}

		/// <summary>Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000D4 RID: 212 RVA: 0x00003DCB File Offset: 0x00001FCB
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000D5 RID: 213 RVA: 0x00003DDF File Offset: 0x00001FDF
		public Task<HttpResponseMessage> GetAsync(string requestUri)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
		}

		/// <summary>Send a GET request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000D6 RID: 214 RVA: 0x00003DF2 File Offset: 0x00001FF2
		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000D7 RID: 215 RVA: 0x00003E06 File Offset: 0x00002006
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP  completion option value that indicates when the operation should be considered completed.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x00003E1A File Offset: 0x0000201A
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption, cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000D9 RID: 217 RVA: 0x00003E2F File Offset: 0x0000202F
		public Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
		}

		/// <summary>Send a GET request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DA RID: 218 RVA: 0x00003E42 File Offset: 0x00002042
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP  completion option value that indicates when the operation should be considered completed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DB RID: 219 RVA: 0x00003E56 File Offset: 0x00002056
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP  completion option value that indicates when the operation should be considered completed.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DC RID: 220 RVA: 0x00003E6A File Offset: 0x0000206A
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption, cancellationToken);
		}

		/// <summary>Send a POST request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DD RID: 221 RVA: 0x00003E7F File Offset: 0x0000207F
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a POST request with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DE RID: 222 RVA: 0x00003E99 File Offset: 0x00002099
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a POST request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000DF RID: 223 RVA: 0x00003EB4 File Offset: 0x000020B4
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a POST request with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00003ECE File Offset: 0x000020CE
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a PUT request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000E1 RID: 225 RVA: 0x00003EE9 File Offset: 0x000020E9
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a PUT request with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000E2 RID: 226 RVA: 0x00003F03 File Offset: 0x00002103
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a PUT request to the specified Uri as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000E3 RID: 227 RVA: 0x00003F1E File Offset: 0x0000211E
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a PUT request with a cancellation token as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000E4 RID: 228 RVA: 0x00003F38 File Offset: 0x00002138
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000E5 RID: 229 RVA: 0x00003F53 File Offset: 0x00002153
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
		}

		/// <summary>Send an HTTP request as an asynchronous operation. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000E6 RID: 230 RVA: 0x00003F62 File Offset: 0x00002162
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
		{
			return this.SendAsync(request, completionOption, CancellationToken.None);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000E7 RID: 231 RVA: 0x00003F71 File Offset: 0x00002171
		public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		// Token: 0x060000E8 RID: 232 RVA: 0x00003F7C File Offset: 0x0000217C
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (request.SetIsUsed())
			{
				throw new InvalidOperationException("Cannot send the same request message multiple times");
			}
			Uri requestUri = request.RequestUri;
			if (requestUri == null)
			{
				if (this.base_address == null)
				{
					throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
				}
				request.RequestUri = this.base_address;
			}
			else if (!requestUri.IsAbsoluteUri || (requestUri.Scheme == Uri.UriSchemeFile && requestUri.OriginalString.StartsWith("/", StringComparison.Ordinal)))
			{
				if (this.base_address == null)
				{
					throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
				}
				request.RequestUri = new Uri(this.base_address, requestUri);
			}
			if (this.headers != null)
			{
				request.Headers.AddHeaders(this.headers);
			}
			return this.SendAsyncWorker(request, completionOption, cancellationToken);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000405C File Offset: 0x0000225C
		private async Task<HttpResponseMessage> SendAsyncWorker(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage2;
			using (CancellationTokenSource lcts = CancellationTokenSource.CreateLinkedTokenSource(this.cts.Token, cancellationToken))
			{
				HttpClientHandler httpClientHandler = this.handler as HttpClientHandler;
				if (httpClientHandler != null)
				{
					httpClientHandler.SetWebRequestTimeout(this.timeout);
				}
				lcts.CancelAfter(this.timeout);
				Task<HttpResponseMessage> task = base.SendAsync(request, lcts.Token);
				if (task == null)
				{
					throw new InvalidOperationException("Handler failed to return a value");
				}
				HttpResponseMessage httpResponseMessage = await task.ConfigureAwait(false);
				HttpResponseMessage response = httpResponseMessage;
				if (response == null)
				{
					throw new InvalidOperationException("Handler failed to return a response");
				}
				if (response.Content != null && (completionOption & HttpCompletionOption.ResponseHeadersRead) == HttpCompletionOption.ResponseContentRead)
				{
					await response.Content.LoadIntoBufferAsync(this.MaxResponseContentBufferSize).ConfigureAwait(false);
				}
				httpResponseMessage2 = response;
			}
			return httpResponseMessage2;
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000EA RID: 234 RVA: 0x000040B8 File Offset: 0x000022B8
		public async Task<byte[]> GetByteArrayAsync(string requestUri)
		{
			HttpResponseMessage httpResponseMessage = await this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			byte[] array;
			using (HttpResponseMessage resp = httpResponseMessage)
			{
				resp.EnsureSuccessStatusCode();
				array = await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
			}
			return array;
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000EB RID: 235 RVA: 0x00004104 File Offset: 0x00002304
		public async Task<byte[]> GetByteArrayAsync(Uri requestUri)
		{
			HttpResponseMessage httpResponseMessage = await this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			byte[] array;
			using (HttpResponseMessage resp = httpResponseMessage)
			{
				resp.EnsureSuccessStatusCode();
				array = await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
			}
			return array;
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000EC RID: 236 RVA: 0x00004150 File Offset: 0x00002350
		public async Task<Stream> GetStreamAsync(string requestUri)
		{
			object obj = await this.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
			obj.EnsureSuccessStatusCode();
			return await obj.Content.ReadAsStreamAsync().ConfigureAwait(false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000ED RID: 237 RVA: 0x0000419C File Offset: 0x0000239C
		public async Task<Stream> GetStreamAsync(Uri requestUri)
		{
			object obj = await this.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
			obj.EnsureSuccessStatusCode();
			return await obj.Content.ReadAsStreamAsync().ConfigureAwait(false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000EE RID: 238 RVA: 0x000041E8 File Offset: 0x000023E8
		public async Task<string> GetStringAsync(string requestUri)
		{
			HttpResponseMessage httpResponseMessage = await this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			string text;
			using (HttpResponseMessage resp = httpResponseMessage)
			{
				resp.EnsureSuccessStatusCode();
				text = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
			return text;
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> was null.</exception>
		// Token: 0x060000EF RID: 239 RVA: 0x00004234 File Offset: 0x00002434
		public async Task<string> GetStringAsync(Uri requestUri)
		{
			HttpResponseMessage httpResponseMessage = await this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			string text;
			using (HttpResponseMessage resp = httpResponseMessage)
			{
				resp.EnsureSuccessStatusCode();
				text = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
			return text;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000027DD File Offset: 0x000009DD
		public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000027DD File Offset: 0x000009DD
		public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000027DD File Offset: 0x000009DD
		public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000027DD File Offset: 0x000009DD
		public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x04000053 RID: 83
		private static readonly TimeSpan TimeoutDefault = TimeSpan.FromSeconds(100.0);

		// Token: 0x04000054 RID: 84
		private Uri base_address;

		// Token: 0x04000055 RID: 85
		private CancellationTokenSource cts;

		// Token: 0x04000056 RID: 86
		private bool disposed;

		// Token: 0x04000057 RID: 87
		private HttpRequestHeaders headers;

		// Token: 0x04000058 RID: 88
		private long buffer_size;

		// Token: 0x04000059 RID: 89
		private TimeSpan timeout;
	}
}
