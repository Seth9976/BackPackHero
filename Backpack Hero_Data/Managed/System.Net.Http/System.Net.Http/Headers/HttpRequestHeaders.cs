using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Request Headers as defined in RFC 2616.</summary>
	// Token: 0x02000047 RID: 71
	public sealed class HttpRequestHeaders : HttpHeaders
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00009E73 File Offset: 0x00008073
		internal HttpRequestHeaders()
			: base(HttpHeaderKind.Request)
		{
		}

		/// <summary>Gets the value of the Accept header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Accept header for an HTTP request.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009E7C File Offset: 0x0000807C
		public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept
		{
			get
			{
				return base.GetValues<MediaTypeWithQualityHeaderValue>("Accept");
			}
		}

		/// <summary>Gets the value of the Accept-Charset header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Accept-Charset header for an HTTP request.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009E89 File Offset: 0x00008089
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Charset");
			}
		}

		/// <summary>Gets the value of the Accept-Encoding header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Accept-Encoding header for an HTTP request.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009E96 File Offset: 0x00008096
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Encoding");
			}
		}

		/// <summary>Gets the value of the Accept-Language header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Accept-Language header for an HTTP request.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009EA3 File Offset: 0x000080A3
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Language");
			}
		}

		/// <summary>Gets or sets the value of the Authorization header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" />.The value of the Authorization header for an HTTP request.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009EB0 File Offset: 0x000080B0
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00009EBD File Offset: 0x000080BD
		public AuthenticationHeaderValue Authorization
		{
			get
			{
				return base.GetValue<AuthenticationHeaderValue>("Authorization");
			}
			set
			{
				base.AddOrRemove<AuthenticationHeaderValue>("Authorization", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Cache-Control header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" />.The value of the Cache-Control header for an HTTP request.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009ECC File Offset: 0x000080CC
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00009ED9 File Offset: 0x000080D9
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

		/// <summary>Gets the value of the Connection header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Connection header for an HTTP request.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009EE8 File Offset: 0x000080E8
		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
				return base.GetValues<string>("Connection");
			}
		}

		/// <summary>Gets or sets a value that indicates if the Connection header for an HTTP request contains Close.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the Connection header contains Close, otherwise false.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009EF8 File Offset: 0x000080F8
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00009F5C File Offset: 0x0000815C
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009FD6 File Offset: 0x000081D6
		internal bool ConnectionKeepAlive
		{
			get
			{
				return this.Connection.Find((string l) => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase)) != null;
			}
		}

		/// <summary>Gets or sets the value of the Date header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the Date header for an HTTP request.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000A005 File Offset: 0x00008205
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000A012 File Offset: 0x00008212
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

		/// <summary>Gets the value of the Expect header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Expect header for an HTTP request.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000A025 File Offset: 0x00008225
		public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect
		{
			get
			{
				return base.GetValues<NameValueWithParametersHeaderValue>("Expect");
			}
		}

		/// <summary>Gets or sets a value that indicates if the Expect header for an HTTP request contains Continue.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the Expect header contains Continue, otherwise false.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A034 File Offset: 0x00008234
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000A094 File Offset: 0x00008294
		public bool? ExpectContinue
		{
			get
			{
				if (this.expectContinue != null)
				{
					return this.expectContinue;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "100-continue", StringComparison.OrdinalIgnoreCase)) == null)
				{
					return null;
				}
				return new bool?(true);
			}
			set
			{
				bool? flag = this.expectContinue;
				bool? flag2 = value;
				if ((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null)))
				{
					return;
				}
				this.Expect.Remove((NameValueWithParametersHeaderValue l) => l.Name == "100-continue");
				flag2 = value;
				bool flag3 = true;
				if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
				{
					this.Expect.Add(new NameValueWithParametersHeaderValue("100-continue"));
				}
				this.expectContinue = value;
			}
		}

		/// <summary>Gets or sets the value of the From header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the From header for an HTTP request.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A12C File Offset: 0x0000832C
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000A139 File Offset: 0x00008339
		public string From
		{
			get
			{
				return base.GetValue<string>("From");
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !Parser.EmailAddress.TryParse(value, out value))
				{
					throw new FormatException();
				}
				base.AddOrRemove("From", value);
			}
		}

		/// <summary>Gets or sets the value of the Host header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the Host header for an HTTP request.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A15F File Offset: 0x0000835F
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000A16C File Offset: 0x0000836C
		public string Host
		{
			get
			{
				return base.GetValue<string>("Host");
			}
			set
			{
				base.AddOrRemove("Host", value);
			}
		}

		/// <summary>Gets the value of the If-Match header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the If-Match header for an HTTP request.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A17A File Offset: 0x0000837A
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch
		{
			get
			{
				return base.GetValues<EntityTagHeaderValue>("If-Match");
			}
		}

		/// <summary>Gets or sets the value of the If-Modified-Since header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the If-Modified-Since header for an HTTP request.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A187 File Offset: 0x00008387
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000A194 File Offset: 0x00008394
		public DateTimeOffset? IfModifiedSince
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("If-Modified-Since");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("If-Modified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets the value of the If-None-Match header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.Gets the value of the If-None-Match header for an HTTP request.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch
		{
			get
			{
				return base.GetValues<EntityTagHeaderValue>("If-None-Match");
			}
		}

		/// <summary>Gets or sets the value of the If-Range header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" />.The value of the If-Range header for an HTTP request.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A1B4 File Offset: 0x000083B4
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A1C1 File Offset: 0x000083C1
		public RangeConditionHeaderValue IfRange
		{
			get
			{
				return base.GetValue<RangeConditionHeaderValue>("If-Range");
			}
			set
			{
				base.AddOrRemove<RangeConditionHeaderValue>("If-Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the If-Unmodified-Since header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the If-Unmodified-Since header for an HTTP request.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A1D0 File Offset: 0x000083D0
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000A1DD File Offset: 0x000083DD
		public DateTimeOffset? IfUnmodifiedSince
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("If-Unmodified-Since");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("If-Unmodified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the Max-Forwards header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The value of the Max-Forwards header for an HTTP request.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000A1FD File Offset: 0x000083FD
		public int? MaxForwards
		{
			get
			{
				return base.GetValue<int?>("Max-Forwards");
			}
			set
			{
				base.AddOrRemove<int>("Max-Forwards", value);
			}
		}

		/// <summary>Gets the value of the Pragma header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Pragma header for an HTTP request.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A20B File Offset: 0x0000840B
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
				return base.GetValues<NameValueHeaderValue>("Pragma");
			}
		}

		/// <summary>Gets or sets the value of the Proxy-Authorization header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" />.The value of the Proxy-Authorization header for an HTTP request.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A218 File Offset: 0x00008418
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000A225 File Offset: 0x00008425
		public AuthenticationHeaderValue ProxyAuthorization
		{
			get
			{
				return base.GetValue<AuthenticationHeaderValue>("Proxy-Authorization");
			}
			set
			{
				base.AddOrRemove<AuthenticationHeaderValue>("Proxy-Authorization", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Range header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RangeHeaderValue" />.The value of the Range header for an HTTP request.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A234 File Offset: 0x00008434
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000A241 File Offset: 0x00008441
		public RangeHeaderValue Range
		{
			get
			{
				return base.GetValue<RangeHeaderValue>("Range");
			}
			set
			{
				base.AddOrRemove<RangeHeaderValue>("Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Referer header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The value of the Referer header for an HTTP request.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000A250 File Offset: 0x00008450
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000A25D File Offset: 0x0000845D
		public Uri Referrer
		{
			get
			{
				return base.GetValue<Uri>("Referer");
			}
			set
			{
				base.AddOrRemove<Uri>("Referer", value, null);
			}
		}

		/// <summary>Gets the value of the TE header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the TE header for an HTTP request.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A26C File Offset: 0x0000846C
		public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE
		{
			get
			{
				return base.GetValues<TransferCodingWithQualityHeaderValue>("TE");
			}
		}

		/// <summary>Gets the value of the Trailer header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Trailer header for an HTTP request.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A279 File Offset: 0x00008479
		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
				return base.GetValues<string>("Trailer");
			}
		}

		/// <summary>Gets the value of the Transfer-Encoding header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Transfer-Encoding header for an HTTP request.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A286 File Offset: 0x00008486
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
				return base.GetValues<TransferCodingHeaderValue>("Transfer-Encoding");
			}
		}

		/// <summary>Gets or sets a value that indicates if the Transfer-Encoding header for an HTTP request contains chunked.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the Transfer-Encoding header contains chunked, otherwise false.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A294 File Offset: 0x00008494
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public bool? TransferEncodingChunked
		{
			get
			{
				if (this.transferEncodingChunked != null)
				{
					return this.transferEncodingChunked;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "chunked", StringComparison.OrdinalIgnoreCase)) == null)
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

		/// <summary>Gets the value of the Upgrade header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Upgrade header for an HTTP request.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A38C File Offset: 0x0000858C
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
				return base.GetValues<ProductHeaderValue>("Upgrade");
			}
		}

		/// <summary>Gets the value of the User-Agent header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the User-Agent header for an HTTP request.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A399 File Offset: 0x00008599
		public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent
		{
			get
			{
				return base.GetValues<ProductInfoHeaderValue>("User-Agent");
			}
		}

		/// <summary>Gets the value of the Via header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Via header for an HTTP request.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000A3A6 File Offset: 0x000085A6
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
				return base.GetValues<ViaHeaderValue>("Via");
			}
		}

		/// <summary>Gets the value of the Warning header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.The value of the Warning header for an HTTP request.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A3B3 File Offset: 0x000085B3
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
				return base.GetValues<WarningHeaderValue>("Warning");
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A3C0 File Offset: 0x000085C0
		internal void AddHeaders(HttpRequestHeaders headers)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
			{
				base.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x04000112 RID: 274
		private bool? expectContinue;
	}
}
