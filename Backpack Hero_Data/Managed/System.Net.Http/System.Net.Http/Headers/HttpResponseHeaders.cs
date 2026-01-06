using System;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Response Headers as defined in RFC 2616.</summary>
	// Token: 0x02000049 RID: 73
	public sealed class HttpResponseHeaders : HttpHeaders
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000A47C File Offset: 0x0000867C
		internal HttpResponseHeaders()
			: base(HttpHeaderKind.Response)
		{
		}

		/// <summary>Gets the value of the Accept-Ranges header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Accept-Ranges header for an HTTP response.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000A485 File Offset: 0x00008685
		public HttpHeaderValueCollection<string> AcceptRanges
		{
			get
			{
				return base.GetValues<string>("Accept-Ranges");
			}
		}

		/// <summary>Gets or sets the value of the Age header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The value of the Age header for an HTTP response.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A492 File Offset: 0x00008692
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000A49F File Offset: 0x0000869F
		public TimeSpan? Age
		{
			get
			{
				return base.GetValue<TimeSpan?>("Age");
			}
			set
			{
				base.AddOrRemove<TimeSpan>("Age", value, (object l) => ((long)((TimeSpan)l).TotalSeconds).ToString());
			}
		}

		/// <summary>Gets or sets the value of the Cache-Control header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" />.The value of the Cache-Control header for an HTTP response.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00009ECC File Offset: 0x000080CC
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00009ED9 File Offset: 0x000080D9
		public CacheControlHeaderValue CacheControl
		{
			get
			{
				return base.GetValue<CacheControlHeaderValue>("Cache-Control");
			}
			set
			{
				base.AddOrRemove<CacheControlHeaderValue>("Cache-Control", value, null);
			}
		}

		/// <summary>Gets the value of the Connection header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Connection header for an HTTP response.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00009EE8 File Offset: 0x000080E8
		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
				return base.GetValues<string>("Connection");
			}
		}

		/// <summary>Gets or sets a value that indicates if the Connection header for an HTTP response contains Close.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the Connection header contains Close, otherwise false.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A4CC File Offset: 0x000086CC
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000A530 File Offset: 0x00008730
		public bool? ConnectionClose
		{
			get
			{
				bool? connectionclose = this.connectionclose;
				bool flag = true;
				if (!((connectionclose.GetValueOrDefault() == flag) & (connectionclose != null)))
				{
					if (this.Connection.Find((string l) => string.Equals(l, "close", StringComparison.OrdinalIgnoreCase)) == null)
					{
						return this.connectionclose;
					}
				}
				return new bool?(true);
			}
			set
			{
				bool? connectionclose = this.connectionclose;
				bool? flag = value;
				if ((connectionclose.GetValueOrDefault() == flag.GetValueOrDefault()) & (connectionclose != null == (flag != null)))
				{
					return;
				}
				this.Connection.Remove("close");
				flag = value;
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					this.Connection.Add("close");
				}
				this.connectionclose = value;
			}
		}

		/// <summary>Gets or sets the value of the Date header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the Date header for an HTTP response.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A005 File Offset: 0x00008205
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000A012 File Offset: 0x00008212
		public DateTimeOffset? Date
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Date");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Date", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the ETag header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.The value of the ETag header for an HTTP response.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A5AA File Offset: 0x000087AA
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A5B7 File Offset: 0x000087B7
		public EntityTagHeaderValue ETag
		{
			get
			{
				return base.GetValue<EntityTagHeaderValue>("ETag");
			}
			set
			{
				base.AddOrRemove<EntityTagHeaderValue>("ETag", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Location header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The value of the Location header for an HTTP response.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A5C6 File Offset: 0x000087C6
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000A5D3 File Offset: 0x000087D3
		public Uri Location
		{
			get
			{
				return base.GetValue<Uri>("Location");
			}
			set
			{
				base.AddOrRemove<Uri>("Location", value, null);
			}
		}

		/// <summary>Gets the value of the Pragma header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Pragma header for an HTTP response.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A20B File Offset: 0x0000840B
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
				return base.GetValues<NameValueHeaderValue>("Pragma");
			}
		}

		/// <summary>Gets the value of the Proxy-Authenticate header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Proxy-Authenticate header for an HTTP response.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A5E2 File Offset: 0x000087E2
		public HttpHeaderValueCollection<AuthenticationHeaderValue> ProxyAuthenticate
		{
			get
			{
				return base.GetValues<AuthenticationHeaderValue>("Proxy-Authenticate");
			}
		}

		/// <summary>Gets or sets the value of the Retry-After header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" />.The value of the Retry-After header for an HTTP response.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A5EF File Offset: 0x000087EF
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A5FC File Offset: 0x000087FC
		public RetryConditionHeaderValue RetryAfter
		{
			get
			{
				return base.GetValue<RetryConditionHeaderValue>("Retry-After");
			}
			set
			{
				base.AddOrRemove<RetryConditionHeaderValue>("Retry-After", value, null);
			}
		}

		/// <summary>Gets the value of the Server header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Server header for an HTTP response.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A60B File Offset: 0x0000880B
		public HttpHeaderValueCollection<ProductInfoHeaderValue> Server
		{
			get
			{
				return base.GetValues<ProductInfoHeaderValue>("Server");
			}
		}

		/// <summary>Gets the value of the Trailer header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Trailer header for an HTTP response.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000A279 File Offset: 0x00008479
		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
				return base.GetValues<string>("Trailer");
			}
		}

		/// <summary>Gets the value of the Transfer-Encoding header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Transfer-Encoding header for an HTTP response.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A286 File Offset: 0x00008486
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
				return base.GetValues<TransferCodingHeaderValue>("Transfer-Encoding");
			}
		}

		/// <summary>Gets or sets a value that indicates if the Transfer-Encoding header for an HTTP response contains chunked.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the Transfer-Encoding header contains chunked, otherwise false.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000A618 File Offset: 0x00008818
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000A678 File Offset: 0x00008878
		public bool? TransferEncodingChunked
		{
			get
			{
				if (this.transferEncodingChunked != null)
				{
					return this.transferEncodingChunked;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => StringComparer.OrdinalIgnoreCase.Equals(l.Value, "chunked")) == null)
				{
					return null;
				}
				return new bool?(true);
			}
			set
			{
				bool? flag = value;
				bool? flag2 = this.transferEncodingChunked;
				if ((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null)))
				{
					return;
				}
				this.TransferEncoding.Remove((TransferCodingHeaderValue l) => l.Value == "chunked");
				flag2 = value;
				bool flag3 = true;
				if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
				{
					this.TransferEncoding.Add(new TransferCodingHeaderValue("chunked"));
				}
				this.transferEncodingChunked = value;
			}
		}

		/// <summary>Gets the value of the Upgrade header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Upgrade header for an HTTP response.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000A38C File Offset: 0x0000858C
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
				return base.GetValues<ProductHeaderValue>("Upgrade");
			}
		}

		/// <summary>Gets the value of the Vary header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Vary header for an HTTP response.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A710 File Offset: 0x00008910
		public HttpHeaderValueCollection<string> Vary
		{
			get
			{
				return base.GetValues<string>("Vary");
			}
		}

		/// <summary>Gets the value of the Via header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Via header for an HTTP response.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A3A6 File Offset: 0x000085A6
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
				return base.GetValues<ViaHeaderValue>("Via");
			}
		}

		/// <summary>Gets the value of the Warning header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Warning header for an HTTP response.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A3B3 File Offset: 0x000085B3
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
				return base.GetValues<WarningHeaderValue>("Warning");
			}
		}

		/// <summary>Gets the value of the WWW-Authenticate header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the WWW-Authenticate header for an HTTP response.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000A71D File Offset: 0x0000891D
		public HttpHeaderValueCollection<AuthenticationHeaderValue> WwwAuthenticate
		{
			get
			{
				return base.GetValues<AuthenticationHeaderValue>("WWW-Authenticate");
			}
		}
	}
}
