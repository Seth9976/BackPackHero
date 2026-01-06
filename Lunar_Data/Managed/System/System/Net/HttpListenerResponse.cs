using System;
using System.Globalization;
using System.IO;
using System.Text;
using Unity;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x020004A1 RID: 1185
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x0008B380 File Offset: 0x00089580
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this.headers = new WebHeaderCollection();
			this.keep_alive = true;
			this.version = HttpVersion.Version11;
			this.status_code = 200;
			this.status_description = "OK";
			this.headers_lock = new object();
			base..ctor();
			this.context = context;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x0008B3D8 File Offset: 0x000895D8
		internal bool ForceCloseChunked
		{
			get
			{
				return this.force_close_chunked;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or null if no encoding is specified.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0008B3E0 File Offset: 0x000895E0
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x0008B3FB File Offset: 0x000895FB
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
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_encoding = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's Content-Length header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0008B430 File Offset: 0x00089630
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x0008B438 File Offset: 0x00089638
		public long ContentLength64
		{
			get
			{
				return this.content_length;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Must be >= 0", "value");
				}
				this.cl_set = true;
				this.content_length = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's Content-Type header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0008B494 File Offset: 0x00089694
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x0008B49C File Offset: 0x0008969C
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the collection of cookies returned with the response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies to accompany the response. The collection is empty if no cookies have been added to the response.</returns>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x0008B4D1 File Offset: 0x000896D1
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x0008B4EC File Offset: 0x000896EC
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
			set
			{
				this.cookies = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x0008B4F5 File Offset: 0x000896F5
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x0008B4FD File Offset: 0x000896FD
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server requests a persistent connection.</summary>
		/// <returns>true if the server requests a persistent connection; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x0008B506 File Offset: 0x00089706
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x0008B50E File Offset: 0x0008970E
		public bool KeepAlive
		{
			get
			{
				return this.keep_alive;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.keep_alive = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0008B543 File Offset: 0x00089743
		public Stream OutputStream
		{
			get
			{
				if (this.output_stream == null)
				{
					this.output_stream = this.context.Connection.GetResponseStream();
				}
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the HTTP version used for the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> object indicating the version of HTTP used when responding to the client. Note that this property is now obsolete.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation does not have its <see cref="P:System.Version.Major" /> property set to 1 or does not have its <see cref="P:System.Version.Minor" /> property set to either 0 or 1.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0008B569 File Offset: 0x00089769
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x0008B574 File Offset: 0x00089774
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException("Must be 1.0 or 1.1", "value");
				}
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.version = value;
			}
		}

		/// <summary>Gets or sets the value of the HTTP Location header in this response.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the absolute URL to be sent to the client in the Location header. </returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0008B60B File Offset: 0x0008980B
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x0008B613 File Offset: 0x00089813
		public string RedirectLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.location = value;
			}
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>true if the response is set to use chunked transfer encoding; otherwise, false. The default is false.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0008B648 File Offset: 0x00089848
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x0008B650 File Offset: 0x00089850
		public bool SendChunked
		{
			get
			{
				return this.chunked;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.chunked = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x0008B685 File Offset: 0x00089885
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x0008B690 File Offset: 0x00089890
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("StatusCode must be between 100 and 999.");
				}
				this.status_code = value;
				this.status_description = HttpStatusDescription.Get(value);
			}
		}

		/// <summary>Gets or sets a text description of the HTTP status code returned to the client.</summary>
		/// <returns>The text description of the HTTP status code returned to the client. The default is the RFC 2616 description for the <see cref="P:System.Net.HttpListenerResponse.StatusCode" /> property value, or an empty string ("") if an RFC 2616 description does not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation contains non-printable characters.</exception>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0008B6F4 File Offset: 0x000898F4
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x0008B6FC File Offset: 0x000898FC
		public string StatusDescription
		{
			get
			{
				return this.status_description;
			}
			set
			{
				this.status_description = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x060025BA RID: 9658 RVA: 0x0008B705 File Offset: 0x00089905
		void IDisposable.Dispose()
		{
			this.Close(true);
		}

		/// <summary>Closes the connection to the client without sending a response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025BB RID: 9659 RVA: 0x0008B70E File Offset: 0x0008990E
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Adds the specified header and value to the HTTP headers for this response.</summary>
		/// <param name="name">The name of the HTTP header to set.</param>
		/// <param name="value">The value for the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x060025BC RID: 9660 RVA: 0x0008B720 File Offset: 0x00089920
		public void AddHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Set(name, value);
		}

		/// <summary>Adds the specified <see cref="T:System.Net.Cookie" /> to the collection of cookies for this response.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to add to the collection to be sent with this response</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		// Token: 0x060025BD RID: 9661 RVA: 0x0008B77D File Offset: 0x0008997D
		public void AppendCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.Cookies.Add(cookie);
		}

		/// <summary>Appends a value to the specified HTTP header to be sent with this response.</summary>
		/// <param name="name">The name of the HTTP header to append <paramref name="value" /> to.</param>
		/// <param name="value">The value to append to the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").-or-You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x060025BE RID: 9662 RVA: 0x0008B79C File Offset: 0x0008999C
		public void AppendHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Add(name, value);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0008B7F9 File Offset: 0x000899F9
		private void Close(bool force)
		{
			this.disposed = true;
			this.context.Connection.Close(force);
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025C0 RID: 9664 RVA: 0x0008B813 File Offset: 0x00089A13
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(false);
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">true to block execution while flushing the stream to the client; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025C1 RID: 9665 RVA: 0x0008B825 File Offset: 0x00089A25
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this.disposed)
			{
				return;
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			this.ContentLength64 = (long)responseEntity.Length;
			this.OutputStream.Write(responseEntity, 0, (int)this.content_length);
			this.Close(false);
		}

		/// <summary>Copies properties from the specified <see cref="T:System.Net.HttpListenerResponse" /> to this response.</summary>
		/// <param name="templateResponse">The <see cref="T:System.Net.HttpListenerResponse" /> instance to copy.</param>
		// Token: 0x060025C2 RID: 9666 RVA: 0x0008B864 File Offset: 0x00089A64
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			this.headers.Clear();
			this.headers.Add(templateResponse.headers);
			this.content_length = templateResponse.content_length;
			this.status_code = templateResponse.status_code;
			this.status_description = templateResponse.status_description;
			this.keep_alive = templateResponse.keep_alive;
			this.version = templateResponse.version;
		}

		/// <summary>Configures the response to redirect the client to the specified URL.</summary>
		/// <param name="url">The URL that the client should use to locate the requested resource.</param>
		// Token: 0x060025C3 RID: 9667 RVA: 0x0008B8C9 File Offset: 0x00089AC9
		public void Redirect(string url)
		{
			this.StatusCode = 302;
			this.location = url;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0008B8E0 File Offset: 0x00089AE0
		private bool FindCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			foreach (object obj in this.cookies)
			{
				Cookie cookie2 = (Cookie)obj;
				if (!(name != cookie2.Name) && !(domain != cookie2.Domain) && path == cookie2.Path)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0008B984 File Offset: 0x00089B84
		internal void SendHeaders(bool closing, MemoryStream ms)
		{
			Encoding @default = this.content_encoding;
			if (@default == null)
			{
				@default = Encoding.Default;
			}
			if (this.content_type != null)
			{
				if (this.content_encoding != null && this.content_type.IndexOf("charset=", StringComparison.Ordinal) == -1)
				{
					string webName = this.content_encoding.WebName;
					this.headers.SetInternal("Content-Type", this.content_type + "; charset=" + webName);
				}
				else
				{
					this.headers.SetInternal("Content-Type", this.content_type);
				}
			}
			if (this.headers["Server"] == null)
			{
				this.headers.SetInternal("Server", "Mono-HTTPAPI/1.0");
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (this.headers["Date"] == null)
			{
				this.headers.SetInternal("Date", DateTime.UtcNow.ToString("r", invariantCulture));
			}
			if (!this.chunked)
			{
				if (!this.cl_set && closing)
				{
					this.cl_set = true;
					this.content_length = 0L;
				}
				if (this.cl_set)
				{
					this.headers.SetInternal("Content-Length", this.content_length.ToString(invariantCulture));
				}
			}
			Version protocolVersion = this.context.Request.ProtocolVersion;
			if (!this.cl_set && !this.chunked && protocolVersion >= HttpVersion.Version11)
			{
				this.chunked = true;
			}
			bool flag = this.status_code == 400 || this.status_code == 408 || this.status_code == 411 || this.status_code == 413 || this.status_code == 414 || this.status_code == 500 || this.status_code == 503;
			if (!flag)
			{
				flag = !this.context.Request.KeepAlive;
			}
			if (!this.keep_alive || flag)
			{
				this.headers.SetInternal("Connection", "close");
				flag = true;
			}
			if (this.chunked)
			{
				this.headers.SetInternal("Transfer-Encoding", "chunked");
			}
			int reuses = this.context.Connection.Reuses;
			if (reuses >= 100)
			{
				this.force_close_chunked = true;
				if (!flag)
				{
					this.headers.SetInternal("Connection", "close");
					flag = true;
				}
			}
			if (!flag)
			{
				this.headers.SetInternal("Keep-Alive", string.Format("timeout=15,max={0}", 100 - reuses));
				if (this.context.Request.ProtocolVersion <= HttpVersion.Version10)
				{
					this.headers.SetInternal("Connection", "keep-alive");
				}
			}
			if (this.location != null)
			{
				this.headers.SetInternal("Location", this.location);
			}
			if (this.cookies != null)
			{
				foreach (object obj in this.cookies)
				{
					Cookie cookie = (Cookie)obj;
					this.headers.SetInternal("Set-Cookie", HttpListenerResponse.CookieToClientString(cookie));
				}
			}
			StreamWriter streamWriter = new StreamWriter(ms, @default, 256);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", this.version, this.status_code, this.status_description);
			string text = HttpListenerResponse.FormatHeaders(this.headers);
			streamWriter.Write(text);
			streamWriter.Flush();
			int num = @default.GetPreamble().Length;
			if (this.output_stream == null)
			{
				this.output_stream = this.context.Connection.GetResponseStream();
			}
			ms.Position = (long)num;
			this.HeadersSent = true;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0008BD40 File Offset: 0x00089F40
		private static string FormatHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (WebHeaderCollection.AllowMultiValues(key))
				{
					foreach (string text in headers.GetValues(i))
					{
						stringBuilder.Append(key).Append(": ").Append(text)
							.Append("\r\n");
					}
				}
				else
				{
					stringBuilder.Append(key).Append(": ").Append(headers.Get(i))
						.Append("\r\n");
				}
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0008BDF8 File Offset: 0x00089FF8
		private static string CookieToClientString(Cookie cookie)
		{
			if (cookie.Name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (cookie.Version > 0)
			{
				stringBuilder.Append("Version=").Append(cookie.Version).Append(";");
			}
			stringBuilder.Append(cookie.Name).Append("=").Append(cookie.Value);
			if (cookie.Path != null && cookie.Path.Length != 0)
			{
				stringBuilder.Append(";Path=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Path));
			}
			if (cookie.Domain != null && cookie.Domain.Length != 0)
			{
				stringBuilder.Append(";Domain=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Domain));
			}
			if (cookie.Port != null && cookie.Port.Length != 0)
			{
				stringBuilder.Append(";Port=").Append(cookie.Port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0008BF02 File Offset: 0x0008A102
		private static string QuotedString(Cookie cookie, string value)
		{
			if (cookie.Version == 0 || HttpListenerResponse.IsToken(value))
			{
				return value;
			}
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0008BF38 File Offset: 0x0008A138
		private static bool IsToken(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ' || c >= '\u007f' || HttpListenerResponse.tspecials.IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Adds or updates a <see cref="T:System.Net.Cookie" /> in the collection of cookies sent with this response. </summary>
		/// <param name="cookie">A <see cref="T:System.Net.Cookie" /> for this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cookie already exists in the collection and could not be replaced.</exception>
		// Token: 0x060025CA RID: 9674 RVA: 0x0008BF7C File Offset: 0x0008A17C
		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (this.cookies != null)
			{
				if (this.FindCookie(cookie))
				{
					throw new ArgumentException("The cookie already exists.");
				}
			}
			else
			{
				this.cookies = new CookieCollection();
			}
			this.cookies.Add(cookie);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00013B26 File Offset: 0x00011D26
		internal HttpListenerResponse()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015CD RID: 5581
		private bool disposed;

		// Token: 0x040015CE RID: 5582
		private Encoding content_encoding;

		// Token: 0x040015CF RID: 5583
		private long content_length;

		// Token: 0x040015D0 RID: 5584
		private bool cl_set;

		// Token: 0x040015D1 RID: 5585
		private string content_type;

		// Token: 0x040015D2 RID: 5586
		private CookieCollection cookies;

		// Token: 0x040015D3 RID: 5587
		private WebHeaderCollection headers;

		// Token: 0x040015D4 RID: 5588
		private bool keep_alive;

		// Token: 0x040015D5 RID: 5589
		private ResponseStream output_stream;

		// Token: 0x040015D6 RID: 5590
		private Version version;

		// Token: 0x040015D7 RID: 5591
		private string location;

		// Token: 0x040015D8 RID: 5592
		private int status_code;

		// Token: 0x040015D9 RID: 5593
		private string status_description;

		// Token: 0x040015DA RID: 5594
		private bool chunked;

		// Token: 0x040015DB RID: 5595
		private HttpListenerContext context;

		// Token: 0x040015DC RID: 5596
		internal bool HeadersSent;

		// Token: 0x040015DD RID: 5597
		internal object headers_lock;

		// Token: 0x040015DE RID: 5598
		private bool force_close_chunked;

		// Token: 0x040015DF RID: 5599
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";
	}
}
