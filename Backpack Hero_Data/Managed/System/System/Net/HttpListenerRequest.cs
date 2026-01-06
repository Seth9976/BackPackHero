using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200049E RID: 1182
	public sealed class HttpListenerRequest
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x0008A66E File Offset: 0x0008886E
		internal HttpListenerRequest(HttpListenerContext context)
		{
			this.context = context;
			this.headers = new WebHeaderCollection();
			this.version = HttpVersion.Version10;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0008A694 File Offset: 0x00088894
		internal void SetRequestLine(string req)
		{
			string[] array = req.Split(HttpListenerRequest.separators, 3);
			if (array.Length != 3)
			{
				this.context.ErrorMessage = "Invalid request line (parts).";
				return;
			}
			this.method = array[0];
			foreach (char c in this.method)
			{
				int num = (int)c;
				if ((num < 65 || num > 90) && (num <= 32 || c >= '\u007f' || c == '(' || c == ')' || c == '<' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}'))
				{
					this.context.ErrorMessage = "(Invalid verb)";
					return;
				}
			}
			this.raw_url = array[1];
			if (array[2].Length != 8 || !array[2].StartsWith("HTTP/"))
			{
				this.context.ErrorMessage = "Invalid request line (version).";
				return;
			}
			try
			{
				this.version = new Version(array[2].Substring(5));
				if (this.version.Major < 1)
				{
					throw new Exception();
				}
			}
			catch
			{
				this.context.ErrorMessage = "Invalid request line (version).";
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0008A7FC File Offset: 0x000889FC
		private void CreateQueryString(string query)
		{
			if (query == null || query.Length == 0)
			{
				this.query_string = new NameValueCollection(1);
				return;
			}
			this.query_string = new NameValueCollection();
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			foreach (string text in query.Split('&', StringSplitOptions.None))
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					this.query_string.Add(null, WebUtility.UrlDecode(text));
				}
				else
				{
					string text2 = WebUtility.UrlDecode(text.Substring(0, num));
					string text3 = WebUtility.UrlDecode(text.Substring(num + 1));
					this.query_string.Add(text2, text3);
				}
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0008A8AC File Offset: 0x00088AAC
		private static bool MaybeUri(string s)
		{
			int num = s.IndexOf(':');
			return num != -1 && num < 10 && HttpListenerRequest.IsPredefinedScheme(s.Substring(0, num));
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0008A8DC File Offset: 0x00088ADC
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme == null || scheme.Length < 3)
			{
				return false;
			}
			char c = scheme[0];
			if (c == 'h')
			{
				return scheme == "http" || scheme == "https";
			}
			if (c == 'f')
			{
				return scheme == "file" || scheme == "ftp";
			}
			if (c != 'n')
			{
				return (c == 'g' && scheme == "gopher") || (c == 'm' && scheme == "mailto");
			}
			c = scheme[1];
			if (c == 'e')
			{
				return scheme == "news" || scheme == "net.pipe" || scheme == "net.tcp";
			}
			return scheme == "nntp";
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0008A9B4 File Offset: 0x00088BB4
		internal bool FinishInitialization()
		{
			string text = this.UserHostName;
			if (this.version > HttpVersion.Version10 && (text == null || text.Length == 0))
			{
				this.context.ErrorMessage = "Invalid host name";
				return true;
			}
			Uri uri = null;
			string pathAndQuery;
			if (HttpListenerRequest.MaybeUri(this.raw_url.ToLowerInvariant()) && Uri.TryCreate(this.raw_url, UriKind.Absolute, out uri))
			{
				pathAndQuery = uri.PathAndQuery;
			}
			else
			{
				pathAndQuery = this.raw_url;
			}
			if (text == null || text.Length == 0)
			{
				text = this.UserHostAddress;
			}
			if (uri != null)
			{
				text = uri.Host;
			}
			int num = text.IndexOf(':');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = string.Format("{0}://{1}:{2}", this.IsSecureConnection ? "https" : "http", text, this.LocalEndPoint.Port);
			if (!Uri.TryCreate(text2 + pathAndQuery, UriKind.Absolute, out this.url))
			{
				this.context.ErrorMessage = WebUtility.HtmlEncode("Invalid url: " + text2 + pathAndQuery);
				return true;
			}
			this.CreateQueryString(this.url.Query);
			this.url = HttpListenerRequestUriBuilder.GetRequestUri(this.raw_url, this.url.Scheme, this.url.Authority, this.url.LocalPath, this.url.Query);
			if (this.version >= HttpVersion.Version11)
			{
				string text3 = this.Headers["Transfer-Encoding"];
				this.is_chunked = text3 != null && string.Compare(text3, "chunked", StringComparison.OrdinalIgnoreCase) == 0;
				if (text3 != null && !this.is_chunked)
				{
					this.context.Connection.SendError(null, 501);
					return false;
				}
			}
			if (!this.is_chunked && !this.cl_set && (string.Compare(this.method, "POST", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.method, "PUT", StringComparison.OrdinalIgnoreCase) == 0))
			{
				this.context.Connection.SendError(null, 411);
				return false;
			}
			if (string.Compare(this.Headers["Expect"], "100-continue", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.context.Connection.GetResponseStream().InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
			}
			return true;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0008AC0C File Offset: 0x00088E0C
		internal static string Unquote(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num >= 0 && num2 >= 0)
			{
				str = str.Substring(num + 1, num2 - 1);
			}
			return str.Trim();
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0008AC48 File Offset: 0x00088E48
		internal void AddHeader(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1 || num == 0)
			{
				this.context.ErrorMessage = "Bad Request";
				this.context.ErrorStatus = 400;
				return;
			}
			string text = header.Substring(0, num).Trim();
			string text2 = header.Substring(num + 1).Trim();
			string text3 = text.ToLower(CultureInfo.InvariantCulture);
			this.headers.SetInternal(text, text2);
			if (text3 == "accept-language")
			{
				this.user_languages = text2.Split(',', StringSplitOptions.None);
				return;
			}
			if (!(text3 == "accept"))
			{
				if (!(text3 == "content-length"))
				{
					if (!(text3 == "referer"))
					{
						if (!(text3 == "cookie"))
						{
							return;
						}
						goto IL_0142;
					}
				}
				else
				{
					try
					{
						this.content_length = long.Parse(text2.Trim());
						if (this.content_length < 0L)
						{
							this.context.ErrorMessage = "Invalid Content-Length.";
						}
						this.cl_set = true;
						return;
					}
					catch
					{
						this.context.ErrorMessage = "Invalid Content-Length.";
						return;
					}
				}
				try
				{
					this.referrer = new Uri(text2);
					return;
				}
				catch
				{
					this.referrer = new Uri("http://someone.is.screwing.with.the.headers.com/");
					return;
				}
				IL_0142:
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				string[] array = text2.Split(new char[] { ',', ';' });
				Cookie cookie = null;
				int num2 = 0;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text4 = array2[i].Trim();
					if (text4.Length != 0)
					{
						if (text4.StartsWith("$Version"))
						{
							num2 = int.Parse(HttpListenerRequest.Unquote(text4.Substring(text4.IndexOf('=') + 1)));
						}
						else if (text4.StartsWith("$Path"))
						{
							if (cookie != null)
							{
								cookie.Path = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Domain"))
						{
							if (cookie != null)
							{
								cookie.Domain = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Port"))
						{
							if (cookie != null)
							{
								cookie.Port = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else
						{
							if (cookie != null)
							{
								this.cookies.Add(cookie);
							}
							try
							{
								cookie = new Cookie();
								int num3 = text4.IndexOf('=');
								if (num3 > 0)
								{
									cookie.Name = text4.Substring(0, num3).Trim();
									cookie.Value = text4.Substring(num3 + 1).Trim();
								}
								else
								{
									cookie.Name = text4.Trim();
									cookie.Value = string.Empty;
								}
								cookie.Version = num2;
							}
							catch (CookieException)
							{
								cookie = null;
							}
						}
					}
				}
				if (cookie != null)
				{
					this.cookies.Add(cookie);
				}
				return;
			}
			this.accept_types = text2.Split(',', StringSplitOptions.None);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0008AF90 File Offset: 0x00089190
		internal bool FlushInput()
		{
			if (!this.HasEntityBody)
			{
				return true;
			}
			int num = 2048;
			if (this.content_length > 0L)
			{
				num = (int)Math.Min(this.content_length, (long)num);
			}
			byte[] array = new byte[num];
			bool flag;
			for (;;)
			{
				try
				{
					IAsyncResult asyncResult = this.InputStream.BeginRead(array, 0, num, null, null);
					if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(1000))
					{
						flag = false;
					}
					else
					{
						if (this.InputStream.EndRead(asyncResult) > 0)
						{
							continue;
						}
						flag = true;
					}
				}
				catch (ObjectDisposedException)
				{
					this.input_stream = null;
					flag = true;
				}
				catch
				{
					flag = false;
				}
				break;
			}
			return flag;
		}

		/// <summary>Gets the MIME types accepted by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the type names specified in the request's Accept header or null if the client request did not include an Accept header.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x0008B040 File Offset: 0x00089240
		public string[] AcceptTypes
		{
			get
			{
				return this.accept_types;
			}
		}

		/// <summary>Gets an error code that identifies a problem with the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> provided by the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a Windows error code.</returns>
		/// <exception cref="T:System.InvalidOperationException">The client certificate has not been initialized yet by a call to the <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /> or <see cref="M:System.Net.HttpListenerRequest.GetClientCertificate" /> methods-or - The operation is still in progress.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x0008B048 File Offset: 0x00089248
		public int ClientCertificateError
		{
			get
			{
				HttpConnection connection = this.context.Connection;
				if (connection.ClientCertificate == null)
				{
					throw new InvalidOperationException("No client certificate");
				}
				int[] clientCertificateErrors = connection.ClientCertificateErrors;
				if (clientCertificateErrors != null && clientCertificateErrors.Length != 0)
				{
					return clientCertificateErrors[0];
				}
				return 0;
			}
		}

		/// <summary>Gets the content encoding that can be used with data sent with the request</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerRequest.InputStream" /> property.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x0008B085 File Offset: 0x00089285
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
		}

		/// <summary>Gets the length of the body data included in the request.</summary>
		/// <returns>The value from the request's Content-Length header. This value is -1 if the content length is not known.</returns>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x0008B0A0 File Offset: 0x000892A0
		public long ContentLength64
		{
			get
			{
				if (!this.is_chunked)
				{
					return this.content_length;
				}
				return -1L;
			}
		}

		/// <summary>Gets the MIME type of the body data included in the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the request's Content-Type header.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x0008B0B3 File Offset: 0x000892B3
		public string ContentType
		{
			get
			{
				return this.headers["content-type"];
			}
		}

		/// <summary>Gets the cookies sent with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies that accompany the request. This property returns an empty collection if the request does not contain cookies.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0008B0C5 File Offset: 0x000892C5
		public CookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				return this.cookies;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>true if the request has associated body data; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0008B0E0 File Offset: 0x000892E0
		public bool HasEntityBody
		{
			get
			{
				return this.content_length > 0L || this.is_chunked;
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0008B0F4 File Offset: 0x000892F4
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets the HTTP method specified by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the method used in the request.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x0008B0FC File Offset: 0x000892FC
		public string HttpMethod
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x0008B104 File Offset: 0x00089304
		public Stream InputStream
		{
			get
			{
				if (this.input_stream == null)
				{
					if (this.is_chunked || this.content_length > 0L)
					{
						this.input_stream = this.context.Connection.GetRequestStream(this.is_chunked, this.content_length);
					}
					else
					{
						this.input_stream = Stream.Null;
					}
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client sending this request is authenticated.</summary>
		/// <returns>true if the client was authenticated; otherwise, false.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO("Always returns false")]
		public bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request is sent from the local computer.</summary>
		/// <returns>true if the request originated on the same computer as the <see cref="T:System.Net.HttpListener" /> object that provided the request; otherwise, false.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0008B160 File Offset: 0x00089360
		public bool IsLocal
		{
			get
			{
				return this.LocalEndPoint.Address.Equals(this.RemoteEndPoint.Address);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>true if the TCP connection is using SSL; otherwise, false.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0008B17D File Offset: 0x0008937D
		public bool IsSecureConnection
		{
			get
			{
				return this.context.Connection.IsSecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>true if the connection should be kept open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x0008B190 File Offset: 0x00089390
		public bool KeepAlive
		{
			get
			{
				if (this.ka_set)
				{
					return this.keep_alive;
				}
				this.ka_set = true;
				string text = this.headers["Connection"];
				if (!string.IsNullOrEmpty(text))
				{
					this.keep_alive = string.Compare(text, "keep-alive", StringComparison.OrdinalIgnoreCase) == 0;
				}
				else if (this.version == HttpVersion.Version11)
				{
					this.keep_alive = true;
				}
				else
				{
					text = this.headers["keep-alive"];
					if (!string.IsNullOrEmpty(text))
					{
						this.keep_alive = string.Compare(text, "closed", StringComparison.OrdinalIgnoreCase) != 0;
					}
				}
				return this.keep_alive;
			}
		}

		/// <summary>Get the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0008B232 File Offset: 0x00089432
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.context.Connection.LocalEndPoint;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x0008B244 File Offset: 0x00089444
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets the query string included in the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the query data included in the request <see cref="P:System.Net.HttpListenerRequest.Url" />.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x0008B24C File Offset: 0x0008944C
		public NameValueCollection QueryString
		{
			get
			{
				return this.query_string;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x0008B254 File Offset: 0x00089454
		public string RawUrl
		{
			get
			{
				return this.raw_url;
			}
		}

		/// <summary>Gets the client IP address and port number from which the request originated.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address and port number from which the request originated.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x0008B25C File Offset: 0x0008945C
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.context.Connection.RemoteEndPoint;
			}
		}

		/// <summary>Gets the request identifier of the incoming HTTP request.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the identifier of the HTTP request.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x0008B26E File Offset: 0x0008946E
		[MonoTODO("Always returns Guid.Empty")]
		public Guid RequestTraceIdentifier
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0008B275 File Offset: 0x00089475
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the resource that referred the client to the server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that contains the text of the request's <see cref="F:System.Net.HttpRequestHeader.Referer" /> header, or null if the header was not included in the request.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x0008B27D File Offset: 0x0008947D
		public Uri UrlReferrer
		{
			get
			{
				return this.referrer;
			}
		}

		/// <summary>Gets the user agent presented by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the text of the request's User-Agent header.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0008B285 File Offset: 0x00089485
		public string UserAgent
		{
			get
			{
				return this.headers["user-agent"];
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x0008B297 File Offset: 0x00089497
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's Host header.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x0008B2A4 File Offset: 0x000894A4
		public string UserHostName
		{
			get
			{
				return this.headers["host"];
			}
		}

		/// <summary>Gets the natural languages that are preferred for the response.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the languages specified in the request's <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header or null if the client request did not include an <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x0008B2B6 File Offset: 0x000894B6
		public string[] UserLanguages
		{
			get
			{
				return this.user_languages;
			}
		}

		/// <summary>Begins an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that indicates the status of the operation.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the callback delegate when the operation completes.</param>
		// Token: 0x06002592 RID: 9618 RVA: 0x0008B2BE File Offset: 0x000894BE
		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			if (this.gcc_delegate == null)
			{
				this.gcc_delegate = new HttpListenerRequest.GCCDelegate(this.GetClientCertificate);
			}
			return this.gcc_delegate.BeginInvoke(requestCallback, state);
		}

		/// <summary>Ends an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>The <see cref="T:System.IAsyncResult" /> object that is returned when the operation started.</returns>
		/// <param name="asyncResult">The pending request for the certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /><paramref name="e." /></exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		// Token: 0x06002593 RID: 9619 RVA: 0x0008B2E7 File Offset: 0x000894E7
		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.gcc_delegate == null)
			{
				throw new InvalidOperationException();
			}
			return this.gcc_delegate.EndInvoke(asyncResult);
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate.</summary>
		/// <returns>A <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		/// <exception cref="T:System.InvalidOperationException">A call to this method to retrieve the client's X.509 v.3 certificate is in progress and therefore another call to this method cannot be made.</exception>
		// Token: 0x06002594 RID: 9620 RVA: 0x0008B311 File Offset: 0x00089511
		public X509Certificate2 GetClientCertificate()
		{
			return this.context.Connection.ClientCertificate;
		}

		/// <summary>Gets the Service Provider Name (SPN) that the client sent on the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SPN the client sent on the request. </returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00002F6A File Offset: 0x0000116A
		[MonoTODO]
		public string ServiceName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.TransportContext" /> for the client request.</summary>
		/// <returns>A <see cref="T:System.Net.TransportContext" /> object for the client request.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x0008B323 File Offset: 0x00089523
		public TransportContext TransportContext
		{
			get
			{
				return new HttpListenerRequest.Context();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection was  a WebSocket request.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the TCP connection is a WebSocket request; otherwise, false.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO]
		public bool IsWebSocketRequest
		{
			get
			{
				return false;
			}
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		// Token: 0x06002598 RID: 9624 RVA: 0x0008B32A File Offset: 0x0008952A
		public Task<X509Certificate2> GetClientCertificateAsync()
		{
			return Task<X509Certificate2>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetClientCertificate), new Func<IAsyncResult, X509Certificate2>(this.EndGetClientCertificate), null);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00013B26 File Offset: 0x00011D26
		internal HttpListenerRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015B8 RID: 5560
		private string[] accept_types;

		// Token: 0x040015B9 RID: 5561
		private Encoding content_encoding;

		// Token: 0x040015BA RID: 5562
		private long content_length;

		// Token: 0x040015BB RID: 5563
		private bool cl_set;

		// Token: 0x040015BC RID: 5564
		private CookieCollection cookies;

		// Token: 0x040015BD RID: 5565
		private WebHeaderCollection headers;

		// Token: 0x040015BE RID: 5566
		private string method;

		// Token: 0x040015BF RID: 5567
		private Stream input_stream;

		// Token: 0x040015C0 RID: 5568
		private Version version;

		// Token: 0x040015C1 RID: 5569
		private NameValueCollection query_string;

		// Token: 0x040015C2 RID: 5570
		private string raw_url;

		// Token: 0x040015C3 RID: 5571
		private Uri url;

		// Token: 0x040015C4 RID: 5572
		private Uri referrer;

		// Token: 0x040015C5 RID: 5573
		private string[] user_languages;

		// Token: 0x040015C6 RID: 5574
		private HttpListenerContext context;

		// Token: 0x040015C7 RID: 5575
		private bool is_chunked;

		// Token: 0x040015C8 RID: 5576
		private bool ka_set;

		// Token: 0x040015C9 RID: 5577
		private bool keep_alive;

		// Token: 0x040015CA RID: 5578
		private HttpListenerRequest.GCCDelegate gcc_delegate;

		// Token: 0x040015CB RID: 5579
		private static byte[] _100continue = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");

		// Token: 0x040015CC RID: 5580
		private static char[] separators = new char[] { ' ' };

		// Token: 0x0200049F RID: 1183
		private class Context : TransportContext
		{
			// Token: 0x0600259B RID: 9627 RVA: 0x0000822E File Offset: 0x0000642E
			public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x020004A0 RID: 1184
		// (Invoke) Token: 0x0600259E RID: 9630
		private delegate X509Certificate2 GCCDelegate();
	}
}
