using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	/// <summary>Represents a HTTP request message.</summary>
	// Token: 0x02000029 RID: 41
	public class HttpRequestMessage : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class.</summary>
		// Token: 0x06000143 RID: 323 RVA: 0x000058B1 File Offset: 0x00003AB1
		public HttpRequestMessage()
		{
			this.method = HttpMethod.Get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">A string that represents the request  <see cref="T:System.Uri" />.</param>
		// Token: 0x06000144 RID: 324 RVA: 0x000058C4 File Offset: 0x00003AC4
		public HttpRequestMessage(HttpMethod method, string requestUri)
			: this(method, string.IsNullOrEmpty(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">The <see cref="T:System.Uri" /> to request.</param>
		// Token: 0x06000145 RID: 325 RVA: 0x000058DF File Offset: 0x00003ADF
		public HttpRequestMessage(HttpMethod method, Uri requestUri)
		{
			this.Method = method;
			this.RequestUri = requestUri;
		}

		/// <summary>Gets or sets the contents of the HTTP message. </summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpContent" />.The content of a message</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000058F5 File Offset: 0x00003AF5
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000058FD File Offset: 0x00003AFD
		public HttpContent Content { get; set; }

		/// <summary>Gets the collection of HTTP request headers.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpRequestHeaders" />.The collection of HTTP request headers.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005908 File Offset: 0x00003B08
		public HttpRequestHeaders Headers
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

		/// <summary>Gets or sets the HTTP method used by the HTTP request message.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.The HTTP method used by the request message. The default is the GET method.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000592D File Offset: 0x00003B2D
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00005935 File Offset: 0x00003B35
		public HttpMethod Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("method");
				}
				this.method = value;
			}
		}

		/// <summary>Gets a set of properties for the HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005954 File Offset: 0x00003B54
		public IDictionary<string, object> Properties
		{
			get
			{
				Dictionary<string, object> dictionary;
				if ((dictionary = this.properties) == null)
				{
					dictionary = (this.properties = new Dictionary<string, object>());
				}
				return dictionary;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> used for the HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The <see cref="T:System.Uri" /> used for the HTTP request.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005979 File Offset: 0x00003B79
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005981 File Offset: 0x00003B81
		public Uri RequestUri
		{
			get
			{
				return this.uri;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && !HttpRequestMessage.IsAllowedAbsoluteUri(value))
				{
					throw new ArgumentException("Only http or https scheme is allowed");
				}
				this.uri = value;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000059B0 File Offset: 0x00003BB0
		private static bool IsAllowedAbsoluteUri(Uri uri)
		{
			return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || (uri.Scheme == Uri.UriSchemeFile && uri.OriginalString.StartsWith("/", StringComparison.Ordinal));
		}

		/// <summary>Gets or sets the HTTP message version.</summary>
		/// <returns>Returns <see cref="T:System.Version" />.The HTTP message version. The default is 1.1.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005A0B File Offset: 0x00003C0B
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005A1C File Offset: 0x00003C1C
		public Version Version
		{
			get
			{
				return this.version ?? HttpVersion.Version11;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Version");
				}
				this.version = value;
			}
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" />.</summary>
		// Token: 0x06000151 RID: 337 RVA: 0x00005A39 File Offset: 0x00003C39
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06000152 RID: 338 RVA: 0x00005A42 File Offset: 0x00003C42
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.Content != null)
				{
					this.Content.Dispose();
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005A69 File Offset: 0x00003C69
		internal bool SetIsUsed()
		{
			if (this.is_used)
			{
				return true;
			}
			this.is_used = true;
			return false;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string representation of the current object.</returns>
		// Token: 0x06000154 RID: 340 RVA: 0x00005A80 File Offset: 0x00003C80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Method: ").Append(this.method);
			stringBuilder.Append(", RequestUri: '").Append((this.RequestUri != null) ? this.RequestUri.ToString() : "<null>");
			stringBuilder.Append("', Version: ").Append(this.Version);
			stringBuilder.Append(", Content: ").Append((this.Content != null) ? this.Content.ToString() : "<null>");
			stringBuilder.Append(", Headers:\r\n{\r\n").Append(this.Headers);
			if (this.Content != null)
			{
				stringBuilder.Append(this.Content.Headers);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x040000B3 RID: 179
		private HttpRequestHeaders headers;

		// Token: 0x040000B4 RID: 180
		private HttpMethod method;

		// Token: 0x040000B5 RID: 181
		private Version version;

		// Token: 0x040000B6 RID: 182
		private Dictionary<string, object> properties;

		// Token: 0x040000B7 RID: 183
		private Uri uri;

		// Token: 0x040000B8 RID: 184
		private bool is_used;

		// Token: 0x040000B9 RID: 185
		private bool disposed;
	}
}
