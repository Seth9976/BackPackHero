using System;
using System.Collections.Generic;
using Unity;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Content Headers as defined in RFC 2616.</summary>
	// Token: 0x02000041 RID: 65
	public sealed class HttpContentHeaders : HttpHeaders
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00008C48 File Offset: 0x00006E48
		internal HttpContentHeaders(HttpContent content)
			: base(HttpHeaderKind.Content)
		{
			this.content = content;
		}

		/// <summary>Gets the value of the Allow content header on an HTTP response. </summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The value of the Allow header on an HTTP response.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008C58 File Offset: 0x00006E58
		public ICollection<string> Allow
		{
			get
			{
				return base.GetValues<string>("Allow");
			}
		}

		/// <summary>Gets the value of the Content-Encoding content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The value of the Content-Encoding content header on an HTTP response.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008C65 File Offset: 0x00006E65
		public ICollection<string> ContentEncoding
		{
			get
			{
				return base.GetValues<string>("Content-Encoding");
			}
		}

		/// <summary>Gets the value of the Content-Disposition content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.The value of the Content-Disposition content header on an HTTP response.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008C72 File Offset: 0x00006E72
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00008C7F File Offset: 0x00006E7F
		public ContentDispositionHeaderValue ContentDisposition
		{
			get
			{
				return base.GetValue<ContentDispositionHeaderValue>("Content-Disposition");
			}
			set
			{
				base.AddOrRemove<ContentDispositionHeaderValue>("Content-Disposition", value, null);
			}
		}

		/// <summary>Gets the value of the Content-Language content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The value of the Content-Language content header on an HTTP response.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008C8E File Offset: 0x00006E8E
		public ICollection<string> ContentLanguage
		{
			get
			{
				return base.GetValues<string>("Content-Language");
			}
		}

		/// <summary>Gets or sets the value of the Content-Length content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The value of the Content-Length content header on an HTTP response.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008C9C File Offset: 0x00006E9C
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00008D03 File Offset: 0x00006F03
		public long? ContentLength
		{
			get
			{
				long? num = base.GetValue<long?>("Content-Length");
				if (num != null)
				{
					return num;
				}
				num = this.content.LoadedBufferLength;
				if (num != null)
				{
					return num;
				}
				long num2;
				if (this.content.TryComputeLength(out num2))
				{
					base.SetValue<long>("Content-Length", num2, null);
					return new long?(num2);
				}
				return null;
			}
			set
			{
				base.AddOrRemove<long>("Content-Length", value);
			}
		}

		/// <summary>Gets or sets the value of the Content-Location content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The value of the Content-Location content header on an HTTP response.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008D11 File Offset: 0x00006F11
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00008D1E File Offset: 0x00006F1E
		public Uri ContentLocation
		{
			get
			{
				return base.GetValue<Uri>("Content-Location");
			}
			set
			{
				base.AddOrRemove<Uri>("Content-Location", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Content-MD5 content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Byte" />.The value of the Content-MD5 content header on an HTTP response.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008D2D File Offset: 0x00006F2D
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00008D3A File Offset: 0x00006F3A
		public byte[] ContentMD5
		{
			get
			{
				return base.GetValue<byte[]>("Content-MD5");
			}
			set
			{
				base.AddOrRemove<byte[]>("Content-MD5", value, Parser.MD5.ToString);
			}
		}

		/// <summary>Gets or sets the value of the Content-Range content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" />.The value of the Content-Range content header on an HTTP response.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008D4D File Offset: 0x00006F4D
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00008D5A File Offset: 0x00006F5A
		public ContentRangeHeaderValue ContentRange
		{
			get
			{
				return base.GetValue<ContentRangeHeaderValue>("Content-Range");
			}
			set
			{
				base.AddOrRemove<ContentRangeHeaderValue>("Content-Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Content-Type content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" />.The value of the Content-Type content header on an HTTP response.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00008D69 File Offset: 0x00006F69
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00008D76 File Offset: 0x00006F76
		public MediaTypeHeaderValue ContentType
		{
			get
			{
				return base.GetValue<MediaTypeHeaderValue>("Content-Type");
			}
			set
			{
				base.AddOrRemove<MediaTypeHeaderValue>("Content-Type", value, null);
			}
		}

		/// <summary>Gets or sets the value of the Expires content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the Expires content header on an HTTP response.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00008D85 File Offset: 0x00006F85
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00008D92 File Offset: 0x00006F92
		public DateTimeOffset? Expires
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Expires");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Expires", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the Last-Modified content header on an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The value of the Last-Modified content header on an HTTP response.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008DA5 File Offset: 0x00006FA5
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00008DB2 File Offset: 0x00006FB2
		public DateTimeOffset? LastModified
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Last-Modified");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Last-Modified", value, Parser.DateTime.ToString);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008DC5 File Offset: 0x00006FC5
		internal HttpContentHeaders()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000FC RID: 252
		private readonly HttpContent content;
	}
}
