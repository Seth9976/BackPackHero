using System;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	/// <summary>Represents a HTTP response message including the status code and data.</summary>
	// Token: 0x0200002A RID: 42
	public class HttpResponseMessage : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpResponseMessage" /> class.</summary>
		// Token: 0x06000155 RID: 341 RVA: 0x00005B60 File Offset: 0x00003D60
		public HttpResponseMessage()
			: this(HttpStatusCode.OK)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpResponseMessage" /> class with a specific <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" />.</summary>
		/// <param name="statusCode">The status code of the HTTP response.</param>
		// Token: 0x06000156 RID: 342 RVA: 0x00005B6D File Offset: 0x00003D6D
		public HttpResponseMessage(HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		/// <summary>Gets or sets the content of a HTTP response message. </summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpContent" />.The content of the HTTP response message.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005B7C File Offset: 0x00003D7C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005B84 File Offset: 0x00003D84
		public HttpContent Content { get; set; }

		/// <summary>Gets the collection of HTTP response headers. </summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpResponseHeaders" />.The collection of HTTP response headers.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005B90 File Offset: 0x00003D90
		public HttpResponseHeaders Headers
		{
			get
			{
				HttpResponseHeaders httpResponseHeaders;
				if ((httpResponseHeaders = this.headers) == null)
				{
					httpResponseHeaders = (this.headers = new HttpResponseHeaders());
				}
				return httpResponseHeaders;
			}
		}

		/// <summary>Gets a value that indicates if the HTTP response was successful.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.A value that indicates if the HTTP response was successful. true if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 200-299; otherwise false.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005BB5 File Offset: 0x00003DB5
		public bool IsSuccessStatusCode
		{
			get
			{
				return this.statusCode >= HttpStatusCode.OK && this.statusCode < HttpStatusCode.MultipleChoices;
			}
		}

		/// <summary>Gets or sets the reason phrase which typically is sent by servers together with the status code. </summary>
		/// <returns>Returns <see cref="T:System.String" />.The reason phrase sent by the server.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005BD3 File Offset: 0x00003DD3
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005BEA File Offset: 0x00003DEA
		public string ReasonPhrase
		{
			get
			{
				return this.reasonPhrase ?? HttpStatusDescription.Get(this.statusCode);
			}
			set
			{
				this.reasonPhrase = value;
			}
		}

		/// <summary>Gets or sets the request message which led to this response message.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpRequestMessage" />.The request message which led to this response message.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005BF3 File Offset: 0x00003DF3
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005BFB File Offset: 0x00003DFB
		public HttpRequestMessage RequestMessage { get; set; }

		/// <summary>Gets or sets the status code of the HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.HttpStatusCode" />.The status code of the HTTP response.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005C04 File Offset: 0x00003E04
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005C0C File Offset: 0x00003E0C
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				if (value < (HttpStatusCode)0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.statusCode = value;
			}
		}

		/// <summary>Gets or sets the HTTP message version. </summary>
		/// <returns>Returns <see cref="T:System.Version" />.The HTTP message version. The default is 1.1. </returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005C1F File Offset: 0x00003E1F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005C30 File Offset: 0x00003E30
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

		/// <summary>Releases the unmanaged resources and disposes of unmanaged resources used by the <see cref="T:System.Net.Http.HttpResponseMessage" />.</summary>
		// Token: 0x06000163 RID: 355 RVA: 0x00005C4D File Offset: 0x00003E4D
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpResponseMessage" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06000164 RID: 356 RVA: 0x00005C56 File Offset: 0x00003E56
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

		/// <summary>Throws an exception if the <see cref="P:System.Net.Http.HttpResponseMessage.IsSuccessStatusCode" /> property for the HTTP response is false.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpResponseMessage" />.The HTTP response message if the call is successful.</returns>
		// Token: 0x06000165 RID: 357 RVA: 0x00005C7D File Offset: 0x00003E7D
		public HttpResponseMessage EnsureSuccessStatusCode()
		{
			if (this.IsSuccessStatusCode)
			{
				return this;
			}
			throw new HttpRequestException(string.Format("{0} ({1})", (int)this.statusCode, this.ReasonPhrase));
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string representation of the current object.</returns>
		// Token: 0x06000166 RID: 358 RVA: 0x00005CAC File Offset: 0x00003EAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StatusCode: ").Append((int)this.StatusCode);
			stringBuilder.Append(", ReasonPhrase: '").Append(this.ReasonPhrase ?? "<null>");
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005D7B File Offset: 0x00003F7B
		public HttpResponseHeaders TrailingHeaders
		{
			get
			{
				if (this.trailingHeaders == null)
				{
					this.trailingHeaders = new HttpResponseHeaders();
				}
				return this.trailingHeaders;
			}
		}

		// Token: 0x040000BB RID: 187
		private HttpResponseHeaders headers;

		// Token: 0x040000BC RID: 188
		private HttpResponseHeaders trailingHeaders;

		// Token: 0x040000BD RID: 189
		private string reasonPhrase;

		// Token: 0x040000BE RID: 190
		private HttpStatusCode statusCode;

		// Token: 0x040000BF RID: 191
		private Version version;

		// Token: 0x040000C0 RID: 192
		private bool disposed;
	}
}
