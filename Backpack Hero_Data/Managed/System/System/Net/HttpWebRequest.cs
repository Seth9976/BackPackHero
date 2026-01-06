using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;
using Unity;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020004A5 RID: 1189
	[Serializable]
	public class HttpWebRequest : WebRequest, ISerializable
	{
		// Token: 0x060025E3 RID: 9699 RVA: 0x0008C134 File Offset: 0x0008A334
		static HttpWebRequest()
		{
			NetConfig netConfig = ConfigurationSettings.GetConfig("system.net/settings") as NetConfig;
			if (netConfig != null)
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = netConfig.MaxResponseHeadersLength;
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0008C178 File Offset: 0x0008A378
		internal HttpWebRequest(Uri uri)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			this.requestUri = uri;
			this.actualUri = uri;
			this.proxy = WebRequest.InternalDefaultWebProxy;
			this.webHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.ThrowOnError = true;
			this.ResetAuthorization();
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0008C241 File Offset: 0x0008A441
		internal HttpWebRequest(Uri uri, MobileTlsProvider tlsProvider, MonoTlsSettings settings = null)
			: this(uri)
		{
			this.tlsProvider = tlsProvider;
			this.tlsSettings = settings;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		// Token: 0x060025E6 RID: 9702 RVA: 0x0008C258 File Offset: 0x0008A458
		[Obsolete("Serialization is obsoleted for this type.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected HttpWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			throw new SerializationException();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0008C2F4 File Offset: 0x0008A4F4
		private void ResetAuthorization()
		{
			this.auth_state = new HttpWebRequest.AuthorizationState(this, false);
			this.proxy_auth_state = new HttpWebRequest.AuthorizationState(this, true);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0008C310 File Offset: 0x0008A510
		private void SetSpecialHeaders(string HeaderName, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.webHeaders.RemoveInternal(HeaderName);
			if (value.Length != 0)
			{
				this.webHeaders.AddInternal(HeaderName, value);
			}
		}

		/// <summary>Gets or sets the value of the Accept HTTP header.</summary>
		/// <returns>The value of the Accept HTTP header. The default value is null.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x0008C33C File Offset: 0x0008A53C
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x0008C34E File Offset: 0x0008A54E
		public string Accept
		{
			get
			{
				return this.webHeaders["Accept"];
			}
			set
			{
				this.CheckRequestStarted();
				this.SetSpecialHeaders("Accept", value);
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the Internet resource that actually responds to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that identifies the Internet resource that actually responds to the request. The default is the URI used by the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method to initialize the request.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x0008C362 File Offset: 0x0008A562
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x0008C36A File Offset: 0x0008A56A
		public Uri Address
		{
			get
			{
				return this.actualUri;
			}
			internal set
			{
				this.actualUri = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the request should follow redirection responses.</summary>
		/// <returns>true if the request should automatically follow redirection responses from the Internet resource; otherwise, false. The default value is true.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x0008C373 File Offset: 0x0008A573
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x0008C37B File Offset: 0x0008A57B
		public virtual bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.allowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data sent to the Internet resource.</summary>
		/// <returns>true to enable buffering of the data sent to the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x0008C384 File Offset: 0x0008A584
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x0008C38C File Offset: 0x0008A58C
		public virtual bool AllowWriteStreamBuffering
		{
			get
			{
				return this.allowBuffering;
			}
			set
			{
				this.allowBuffering = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the received from the  Internet resource.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true to enable buffering of the data received from the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0008C395 File Offset: 0x0008A595
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x0008C39D File Offset: 0x0008A59D
		public virtual bool AllowReadStreamBuffering
		{
			get
			{
				return this.allowReadStreamBuffering;
			}
			set
			{
				this.allowReadStreamBuffering = value;
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0001FC8B File Offset: 0x0001DE8B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the type of decompression that is used.</summary>
		/// <returns>A T:System.Net.DecompressionMethods object that indicates the type of decompression that is used. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object's current state does not allow this property to be set.</exception>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x0008C3A6 File Offset: 0x0008A5A6
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x0008C3AE File Offset: 0x0008A5AE
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.auto_decomp;
			}
			set
			{
				this.CheckRequestStarted();
				this.auto_decomp = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x0008C3BD File Offset: 0x0008A5BD
		internal bool InternalAllowBuffering
		{
			get
			{
				return this.allowBuffering && this.MethodWithBuffer;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x0008C3D0 File Offset: 0x0008A5D0
		private bool MethodWithBuffer
		{
			get
			{
				return this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "TRACE";
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x0008C437 File Offset: 0x0008A637
		internal MobileTlsProvider TlsProvider
		{
			get
			{
				return this.tlsProvider;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0008C43F File Offset: 0x0008A63F
		internal MonoTlsSettings TlsSettings
		{
			get
			{
				return this.tlsSettings;
			}
		}

		/// <summary>Gets or sets the collection of security certificates that are associated with this request.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains the security certificates associated with this request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null. </exception>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x0008C447 File Offset: 0x0008A647
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x0008C462 File Offset: 0x0008A662
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.certificates == null)
				{
					this.certificates = new X509CertificateCollection();
				}
				return this.certificates;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.certificates = value;
			}
		}

		/// <summary>Gets or sets the value of the Connection HTTP header.</summary>
		/// <returns>The value of the Connection HTTP header. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The value of <see cref="P:System.Net.HttpWebRequest.Connection" /> is set to Keep-alive or Close. </exception>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x0008C479 File Offset: 0x0008A679
		// (set) Token: 0x060025FD RID: 9725 RVA: 0x0008C48C File Offset: 0x0008A68C
		public string Connection
		{
			get
			{
				return this.webHeaders["Connection"];
			}
			set
			{
				this.CheckRequestStarted();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.webHeaders.RemoveInternal("Connection");
					return;
				}
				string text = value.ToLowerInvariant();
				if (text.Contains("keep-alive") || text.Contains("close"))
				{
					throw new ArgumentException("Keep-Alive and Close may not be set using this property.", "value");
				}
				string text2 = HttpValidationHelpers.CheckBadHeaderValueChars(value);
				this.webHeaders.CheckUpdate("Connection", text2);
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for this request. The default value is null.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x0008C501 File Offset: 0x0008A701
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x0008C509 File Offset: 0x0008A709
		public override string ConnectionGroupName
		{
			get
			{
				return this.connectionGroup;
			}
			set
			{
				this.connectionGroup = value;
			}
		}

		/// <summary>Gets or sets the Content-length HTTP header.</summary>
		/// <returns>The number of bytes of data to send to the Internet resource. The default is -1, which indicates the property has not been set and that there is no request data to send.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new <see cref="P:System.Net.HttpWebRequest.ContentLength" /> value is less than 0. </exception>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x0008C512 File Offset: 0x0008A712
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x0008C51A File Offset: 0x0008A71A
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Content-Length must be >= 0");
				}
				this.contentLength = value;
				this.haveContentLength = true;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (set) Token: 0x06002602 RID: 9730 RVA: 0x0008C545 File Offset: 0x0008A745
		internal long InternalContentLength
		{
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x0008C54E File Offset: 0x0008A74E
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x0008C556 File Offset: 0x0008A756
		internal bool ThrowOnError { get; set; }

		/// <summary>Gets or sets the value of the Content-type HTTP header.</summary>
		/// <returns>The value of the Content-type HTTP header. The default value is null.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x0008C55F File Offset: 0x0008A75F
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x0008C571 File Offset: 0x0008A771
		public override string ContentType
		{
			get
			{
				return this.webHeaders["Content-Type"];
			}
			set
			{
				this.SetSpecialHeaders("Content-Type", value);
			}
		}

		/// <summary>Gets or sets the delegate method called when an HTTP 100-continue response is received from the Internet resource.</summary>
		/// <returns>A delegate that implements the callback method that executes when an HTTP Continue response is returned from the Internet resource. The default value is null.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x0008C57F File Offset: 0x0008A77F
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x0008C587 File Offset: 0x0008A787
		public HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.continueDelegate;
			}
			set
			{
				this.continueDelegate = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieContainer" /> that contains the cookies associated with this request.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x0008C590 File Offset: 0x0008A790
		// (set) Token: 0x0600260A RID: 9738 RVA: 0x0008C598 File Offset: 0x0008A798
		public virtual CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		/// <summary>Gets or sets authentication information for the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials associated with the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x0008C5A1 File Offset: 0x0008A7A1
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x0008C5A9 File Offset: 0x0008A7A9
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Get or set the Date HTTP header value to use in an HTTP request.</summary>
		/// <returns>The Date header value in the HTTP request.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x0008C5B4 File Offset: 0x0008A7B4
		// (set) Token: 0x0600260E RID: 9742 RVA: 0x0008C5F3 File Offset: 0x0008A7F3
		public DateTime Date
		{
			get
			{
				string text = this.webHeaders["Date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				return DateTime.ParseExact(text, "r", CultureInfo.InvariantCulture).ToLocalTime();
			}
			set
			{
				this.SetDateHeaderHelper("Date", value);
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0008C601 File Offset: 0x0008A801
		private void SetDateHeaderHelper(string headerName, DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				this.SetSpecialHeaders(headerName, null);
				return;
			}
			this.SetSpecialHeaders(headerName, HttpProtocolUtils.date2string(dateTime));
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x0008C626 File Offset: 0x0008A826
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x0008C62D File Offset: 0x0008A82D
		[MonoTODO]
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return HttpWebRequest.defaultCachePolicy;
			}
			set
			{
				HttpWebRequest.defaultCachePolicy = value;
			}
		}

		/// <summary>Gets or sets the default maximum length of an HTTP error response.</summary>
		/// <returns>An integer that represents the default maximum length of an HTTP error response.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1. </exception>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x0008C635 File Offset: 0x0008A835
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x0008C63C File Offset: 0x0008A83C
		[MonoTODO]
		public static int DefaultMaximumErrorResponseLength
		{
			get
			{
				return HttpWebRequest.defaultMaximumErrorResponseLength;
			}
			set
			{
				HttpWebRequest.defaultMaximumErrorResponseLength = value;
			}
		}

		/// <summary>Gets or sets the value of the Expect HTTP header.</summary>
		/// <returns>The contents of the Expect HTTP header. The default value is null.NoteThe value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		/// <exception cref="T:System.ArgumentException">Expect is set to a string that contains "100-continue" as a substring. </exception>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x0008C644 File Offset: 0x0008A844
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x0008C658 File Offset: 0x0008A858
		public string Expect
		{
			get
			{
				return this.webHeaders["Expect"];
			}
			set
			{
				this.CheckRequestStarted();
				string text = value;
				if (text != null)
				{
					text = text.Trim().ToLower();
				}
				if (text == null || text.Length == 0)
				{
					this.webHeaders.RemoveInternal("Expect");
					return;
				}
				if (text == "100-continue")
				{
					throw new ArgumentException("100-Continue cannot be set with this property.", "value");
				}
				this.webHeaders.CheckUpdate("Expect", value);
			}
		}

		/// <summary>Gets a value that indicates whether a response has been received from an Internet resource.</summary>
		/// <returns>true if a response has been received; otherwise, false.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x0008C6C6 File Offset: 0x0008A8C6
		public virtual bool HaveResponse
		{
			get
			{
				return this.haveResponse;
			}
		}

		/// <summary>Specifies a collection of the name/value pairs that make up the HTTP headers.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the name/value pairs that make up the headers for the HTTP request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x0008C6CE File Offset: 0x0008A8CE
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x0008C6D8 File Offset: 0x0008A8D8
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
			set
			{
				this.CheckRequestStarted();
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
				foreach (string text in value.AllKeys)
				{
					webHeaderCollection.Add(text, value[text]);
				}
				this.webHeaders = webHeaderCollection;
			}
		}

		/// <summary>Get or set the Host header value to use in an HTTP request independent from the request URI.</summary>
		/// <returns>The Host header value in the HTTP request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The Host header cannot be set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The Host header cannot be set to an invalid value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The Host header cannot be set after the <see cref="T:System.Net.HttpWebRequest" /> has already started to be sent. </exception>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x0008C728 File Offset: 0x0008A928
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x0008C790 File Offset: 0x0008A990
		public string Host
		{
			get
			{
				Uri uri = this.hostUri ?? this.Address;
				if ((!(this.hostUri == null) && this.hostHasPort) || !this.Address.IsDefaultPort)
				{
					return uri.Host + ":" + uri.Port.ToString();
				}
				return uri.Host;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Uri uri;
				if (value.IndexOf('/') != -1 || !this.TryGetHostUri(value, out uri))
				{
					throw new ArgumentException("The specified value is not a valid Host header string.", "value");
				}
				this.hostUri = uri;
				if (!this.hostUri.IsDefaultPort)
				{
					this.hostHasPort = true;
					return;
				}
				if (value.IndexOf(':') == -1)
				{
					this.hostHasPort = false;
					return;
				}
				int num = value.IndexOf(']');
				this.hostHasPort = num == -1 || value.LastIndexOf(':') > num;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0008C827 File Offset: 0x0008AA27
		private bool TryGetHostUri(string hostName, out Uri hostUri)
		{
			return Uri.TryCreate(this.Address.Scheme + "://" + hostName + this.Address.PathAndQuery, UriKind.Absolute, out hostUri);
		}

		/// <summary>Gets or sets the value of the If-Modified-Since HTTP header.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the contents of the If-Modified-Since HTTP header. The default value is the current date and time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x0008C854 File Offset: 0x0008AA54
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x0008C8A0 File Offset: 0x0008AAA0
		public DateTime IfModifiedSince
		{
			get
			{
				string text = this.webHeaders["If-Modified-Since"];
				if (text == null)
				{
					return DateTime.Now;
				}
				DateTime dateTime;
				try
				{
					dateTime = MonoHttpDate.Parse(text);
				}
				catch (Exception)
				{
					dateTime = DateTime.Now;
				}
				return dateTime;
			}
			set
			{
				this.CheckRequestStarted();
				this.webHeaders.SetInternal("If-Modified-Since", value.ToUniversalTime().ToString("r", null));
			}
		}

		/// <summary>Gets or sets a value that indicates whether to make a persistent connection to the Internet resource.</summary>
		/// <returns>true if the request to the Internet resource should contain a Connection HTTP header with the value Keep-alive; otherwise, false. The default is true.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x0008C8D8 File Offset: 0x0008AAD8
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x0008C8E0 File Offset: 0x0008AAE0
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the request follows.</summary>
		/// <returns>The maximum number of redirection responses that the request follows. The default value is 50.</returns>
		/// <exception cref="T:System.ArgumentException">The value is set to 0 or less. </exception>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x0008C8E9 File Offset: 0x0008AAE9
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x0008C8F1 File Offset: 0x0008AAF1
		public int MaximumAutomaticRedirections
		{
			get
			{
				return this.maxAutoRedirect;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Must be > 0", "value");
				}
				this.maxAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the response headers.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set after the request has already been submitted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1. </exception>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x0008C90E File Offset: 0x0008AB0E
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x0008C916 File Offset: 0x0008AB16
		[MonoTODO("Use this")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return this.maxResponseHeadersLength;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "The specified value must be greater than 0.");
				}
				this.maxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the default for the <see cref="P:System.Net.HttpWebRequest.MaximumResponseHeadersLength" /> property.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the default maximum for response headers received. The default configuration file sets this value to 64 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not equal to -1 and is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0008C93D File Offset: 0x0008AB3D
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x0008C944 File Offset: 0x0008AB44
		[MonoTODO("Use this")]
		public static int DefaultMaximumResponseHeadersLength
		{
			get
			{
				return HttpWebRequest.defaultMaxResponseHeadersLength;
			}
			set
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets a time-out in milliseconds when writing to or reading from a stream.</summary>
		/// <returns>The number of milliseconds before the writing or reading times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /></exception>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x0008C94C File Offset: 0x0008AB4C
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x0008C954 File Offset: 0x0008AB54
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.readWriteTimeout = value;
			}
		}

		/// <summary>Gets or sets a timeout, in seconds, to wait for the server status after 100-continue is received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The timeout, in seconds, to wait for the server status after 100-continue is received.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x0008C97B File Offset: 0x0008AB7B
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x0008C983 File Offset: 0x0008AB83
		[MonoTODO]
		public int ContinueTimeout
		{
			get
			{
				return this.continueTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.");
				}
				this.continueTimeout = value;
			}
		}

		/// <summary>Gets or sets the media type of the request.</summary>
		/// <returns>The media type of the request. The default value is null.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x0008C9AA File Offset: 0x0008ABAA
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x0008C9B2 File Offset: 0x0008ABB2
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.mediaType = value;
			}
		}

		/// <summary>Gets or sets the method for the request.</summary>
		/// <returns>The request method to use to contact the Internet resource. The default value is GET.</returns>
		/// <exception cref="T:System.ArgumentException">No method is supplied.-or- The method string contains invalid characters. </exception>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x0008C9BB File Offset: 0x0008ABBB
		// (set) Token: 0x0600262D RID: 9773 RVA: 0x0008C9C4 File Offset: 0x0008ABC4
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				if (HttpValidationHelpers.IsInvalidMethodOrHeaderString(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				this.method = value.ToUpperInvariant();
				if (this.method != "HEAD" && this.method != "GET" && this.method != "POST" && this.method != "PUT" && this.method != "DELETE" && this.method != "CONNECT" && this.method != "TRACE" && this.method != "MKCOL")
				{
					this.method = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether to pipeline the request to the Internet resource.</summary>
		/// <returns>true if the request should be pipelined; otherwise, false. The default is true.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x0008CAA7 File Offset: 0x0008ACA7
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x0008CAAF File Offset: 0x0008ACAF
		public bool Pipelined
		{
			get
			{
				return this.pipelined;
			}
			set
			{
				this.pipelined = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send an Authorization header with the request.</summary>
		/// <returns>true to send an  HTTP Authorization header with requests after authentication has taken place; otherwise, false. The default is false.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x0008CAB8 File Offset: 0x0008ACB8
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x0008CAC0 File Offset: 0x0008ACC0
		public override bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		/// <summary>Gets or sets the version of HTTP to use for the request.</summary>
		/// <returns>The HTTP version to use for the request. The default is <see cref="F:System.Net.HttpVersion.Version11" />.</returns>
		/// <exception cref="T:System.ArgumentException">The HTTP version is set to a value other than 1.0 or 1.1. </exception>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x0008CAC9 File Offset: 0x0008ACC9
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x0008CAD1 File Offset: 0x0008ACD1
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value != HttpVersion.Version10 && value != HttpVersion.Version11)
				{
					throw new ArgumentException("Only HTTP/1.0 and HTTP/1.1 version requests are currently supported.", "value");
				}
				this.force_version = true;
				this.version = value;
			}
		}

		/// <summary>Gets or sets proxy information for the request.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> object to use to proxy the request. The default value is set by calling the <see cref="P:System.Net.GlobalProxySelection.Select" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.HttpWebRequest.Proxy" /> is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x0008CB0B File Offset: 0x0008AD0B
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x0008CB13 File Offset: 0x0008AD13
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				this.proxy = value;
				this.servicePoint = null;
				this.GetServicePoint();
			}
		}

		/// <summary>Gets or sets the value of the Referer HTTP header.</summary>
		/// <returns>The value of the Referer HTTP header. The default value is null.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x0008CB30 File Offset: 0x0008AD30
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x0008CB42 File Offset: 0x0008AD42
		public string Referer
		{
			get
			{
				return this.webHeaders["Referer"];
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null || value.Trim().Length == 0)
				{
					this.webHeaders.RemoveInternal("Referer");
					return;
				}
				this.webHeaders.SetInternal("Referer", value);
			}
		}

		/// <summary>Gets the original Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource passed to the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x0008CB7C File Offset: 0x0008AD7C
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send data in segments to the Internet resource.</summary>
		/// <returns>true to send data to the Internet resource in segments; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x0008CB84 File Offset: 0x0008AD84
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x0008CB8C File Offset: 0x0008AD8C
		public bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.CheckRequestStarted();
				this.sendChunked = value;
			}
		}

		/// <summary>Gets the service point to use for the request.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that represents the network connection to the Internet resource.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x0008CB9B File Offset: 0x0008AD9B
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x0008CBA3 File Offset: 0x0008ADA3
		internal ServicePoint ServicePointNoLock
		{
			get
			{
				return this.servicePoint;
			}
		}

		/// <summary>Gets a value that indicates whether the request provides support for a <see cref="T:System.Net.CookieContainer" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if a <see cref="T:System.Net.CookieContainer" /> is supported; otherwise, false. </returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool SupportsCookieContainer
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the time-out value in milliseconds for the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> and <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> methods.</summary>
		/// <returns>The number of milliseconds to wait before the request times out. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x0008CBAB File Offset: 0x0008ADAB
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x0008CBB3 File Offset: 0x0008ADB3
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets the value of the Transfer-encoding HTTP header.</summary>
		/// <returns>The value of the Transfer-encoding HTTP header. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set when <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to the value "Chunked". </exception>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x0008CBCB File Offset: 0x0008ADCB
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x0008CBE0 File Offset: 0x0008ADE0
		public string TransferEncoding
		{
			get
			{
				return this.webHeaders["Transfer-Encoding"];
			}
			set
			{
				this.CheckRequestStarted();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.webHeaders.RemoveInternal("Transfer-Encoding");
					return;
				}
				if (value.ToLower().Contains("chunked"))
				{
					throw new ArgumentException("Chunked encoding must be set via the SendChunked property.", "value");
				}
				if (!this.SendChunked)
				{
					throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
				}
				string text = HttpValidationHelpers.CheckBadHeaderValueChars(value);
				this.webHeaders.CheckUpdate("Transfer-Encoding", text);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether default credentials are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x0008CC59 File Offset: 0x0008AE59
		// (set) Token: 0x06002643 RID: 9795 RVA: 0x0008CC68 File Offset: 0x0008AE68
		public override bool UseDefaultCredentials
		{
			get
			{
				return CredentialCache.DefaultCredentials == this.Credentials;
			}
			set
			{
				this.Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets the value of the User-agent HTTP header.</summary>
		/// <returns>The value of the User-agent HTTP header. The default value is null.NoteThe value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x0008CC7B File Offset: 0x0008AE7B
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x0008CC8D File Offset: 0x0008AE8D
		public string UserAgent
		{
			get
			{
				return this.webHeaders["User-Agent"];
			}
			set
			{
				this.webHeaders.SetInternal("User-Agent", value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether to allow high-speed NTLM-authenticated connection sharing.</summary>
		/// <returns>true to keep the authenticated connection open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
		// (set) Token: 0x06002647 RID: 9799 RVA: 0x0008CCA8 File Offset: 0x0008AEA8
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_auth_blah;
			}
			set
			{
				this.unsafe_auth_blah = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x0008CCB1 File Offset: 0x0008AEB1
		internal bool GotRequestStream
		{
			get
			{
				return this.gotRequestStream;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x0008CCB9 File Offset: 0x0008AEB9
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x0008CCC1 File Offset: 0x0008AEC1
		internal bool ExpectContinue
		{
			get
			{
				return this.expectContinue;
			}
			set
			{
				this.expectContinue = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0008C362 File Offset: 0x0008A562
		internal Uri AuthUri
		{
			get
			{
				return this.actualUri;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x0008CCCA File Offset: 0x0008AECA
		internal bool ProxyQuery
		{
			get
			{
				return this.servicePoint.UsesProxy && !this.servicePoint.UseConnect;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0008CCE9 File Offset: 0x0008AEE9
		internal ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return this.certValidationCallback;
			}
		}

		/// <summary>Gets or sets a callback function to validate the server certificate.</summary>
		/// <returns>Returns <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />.A callback function to validate the server certificate.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x0008CCF1 File Offset: 0x0008AEF1
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x0008CD08 File Offset: 0x0008AF08
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (this.certValidationCallback == null)
				{
					return null;
				}
				return this.certValidationCallback.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					this.certValidationCallback = null;
					return;
				}
				this.certValidationCallback = new ServerCertValidationCallback(value);
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0008CD24 File Offset: 0x0008AF24
		internal ServicePoint GetServicePoint()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.hostChanged || this.servicePoint == null)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.actualUri, this.proxy);
					this.hostChanged = false;
				}
			}
			return this.servicePoint;
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002651 RID: 9809 RVA: 0x0008CD94 File Offset: 0x0008AF94
		public void AddRange(int range)
		{
			this.AddRange("bytes", (long)range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data. </param>
		/// <param name="to">The position at which to stop sending data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002652 RID: 9810 RVA: 0x0008CDA3 File Offset: 0x0008AFA3
		public void AddRange(int from, int to)
		{
			this.AddRange("bytes", (long)from, (long)to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range. </param>
		/// <param name="range">The starting or ending point of the range. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002653 RID: 9811 RVA: 0x0008CDB4 File Offset: 0x0008AFB4
		public void AddRange(string rangeSpecifier, int range)
		{
			this.AddRange(rangeSpecifier, (long)range);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range. </param>
		/// <param name="from">The position at which to start sending data. </param>
		/// <param name="to">The position at which to stop sending data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002654 RID: 9812 RVA: 0x0008CDBF File Offset: 0x0008AFBF
		public void AddRange(string rangeSpecifier, int from, int to)
		{
			this.AddRange(rangeSpecifier, (long)from, (long)to);
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002655 RID: 9813 RVA: 0x0008CDCC File Offset: 0x0008AFCC
		public void AddRange(long range)
		{
			this.AddRange("bytes", range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002656 RID: 9814 RVA: 0x0008CDDA File Offset: 0x0008AFDA
		public void AddRange(long from, long to)
		{
			this.AddRange("bytes", from, to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002657 RID: 9815 RVA: 0x0008CDEC File Offset: 0x0008AFEC
		public void AddRange(string rangeSpecifier, long range)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				if (string.Compare(text.Substring(0, text.IndexOf('=')), rangeSpecifier, StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new InvalidOperationException("A different range specifier is already in use");
				}
				text += ",";
			}
			string text2 = range.ToString(CultureInfo.InvariantCulture);
			if (range < 0L)
			{
				text = text + "0" + text2;
			}
			else
			{
				text = text + text2 + "-";
			}
			this.webHeaders.ChangeInternal("Range", text);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x06002658 RID: 9816 RVA: 0x0008CEB0 File Offset: 0x0008B0B0
		public void AddRange(string rangeSpecifier, long from, long to)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			if (from > to || from < 0L)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (to < 0L)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				text += ",";
			}
			text = string.Format("{0}{1}-{2}", text, from, to);
			this.webHeaders.ChangeInternal("Range", text);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0008CF60 File Offset: 0x0008B160
		private WebOperation SendRequest(bool redirecting, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			object obj = this.locker;
			WebOperation webOperation2;
			lock (obj)
			{
				if (!redirecting && this.requestSent)
				{
					WebOperation webOperation = this.currentOperation;
					if (webOperation == null)
					{
						throw new InvalidOperationException("Should never happen!");
					}
					webOperation2 = webOperation;
				}
				else
				{
					WebOperation webOperation = new WebOperation(this, writeBuffer, false, cancellationToken);
					if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, webOperation, null) != null)
					{
						throw new InvalidOperationException("Invalid nested call.");
					}
					this.requestSent = true;
					if (!redirecting)
					{
						this.redirects = 0;
					}
					this.servicePoint = this.GetServicePoint();
					this.servicePoint.SendRequest(webOperation, this.connectionGroup);
					webOperation2 = webOperation;
				}
			}
			return webOperation2;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0008D014 File Offset: 0x0008B214
		private Task<Stream> MyGetRequestStreamAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			bool flag = !(this.method == "GET") && !(this.method == "CONNECT") && !(this.method == "HEAD") && !(this.method == "TRACE");
			if (this.method == null || !flag)
			{
				throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
			}
			if (this.contentLength == -1L && !this.sendChunked && !this.allowBuffering && this.KeepAlive)
			{
				throw new ProtocolViolationException("Content-Length not set");
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
			}
			object obj = this.locker;
			WebOperation webOperation;
			lock (obj)
			{
				if (this.getResponseCalled)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				webOperation = this.currentOperation;
				if (webOperation == null)
				{
					this.initialMethod = this.method;
					this.gotRequestStream = true;
					webOperation = this.SendRequest(false, null, cancellationToken);
				}
			}
			return webOperation.GetRequestStream();
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">The state object for this request. </param>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false.-or- The thread pool is running out of threads. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called. </exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600265B RID: 9819 RVA: 0x0008D160 File Offset: 0x0008B360
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.RunWithTimeout<Stream>(new Func<CancellationToken, Task<Stream>>(this.MyGetRequestStreamAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="asyncResult">The pending request for a stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600265C RID: 9820 RVA: 0x0008D17C File Offset: 0x0008B37C
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream stream;
			try
			{
				stream = TaskToApm.End<Stream>(asyncResult);
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return stream;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600265D RID: 9821 RVA: 0x0008D1BC File Offset: 0x0008B3BC
		public override Stream GetRequestStream()
		{
			Stream result;
			try
			{
				result = this.GetRequestStreamAsync().Result;
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return result;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method was unable to obtain the <see cref="T:System.IO.Stream" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		// Token: 0x0600265E RID: 9822 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public Stream GetRequestStream(out TransportContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
		public override Task<Stream> GetRequestStreamAsync()
		{
			return this.RunWithTimeout<Stream>(new Func<CancellationToken, Task<Stream>>(this.MyGetRequestStreamAsync));
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0008D208 File Offset: 0x0008B408
		internal static Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func, int timeout, Action abort, Func<bool> aborted, CancellationToken cancellationToken)
		{
			CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), timeout, abort, aborted, cancellationTokenSource);
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0008D234 File Offset: 0x0008B434
		private static async Task<T> RunWithTimeoutWorker<T>(Task<T> workerTask, int timeout, Action abort, Func<bool> aborted, CancellationTokenSource cts)
		{
			T result;
			try
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = ServicePointScheduler.WaitAsync(workerTask, timeout).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					try
					{
						cts.Cancel();
						abort();
					}
					catch
					{
					}
					workerTask.ContinueWith<int?>(delegate(Task<T> t)
					{
						AggregateException exception = t.Exception;
						if (exception == null)
						{
							return null;
						}
						return new int?(exception.GetHashCode());
					}, TaskContinuationOptions.OnlyOnFaulted);
					throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				}
				result = workerTask.Result;
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.GetWebException(ex, aborted());
			}
			finally
			{
				cts.Dispose();
			}
			return result;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0008D298 File Offset: 0x0008B498
		private Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), this.timeout, new Action(this.Abort), () => this.Aborted, cancellationTokenSource);
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0008D2DC File Offset: 0x0008B4DC
		private async Task<HttpWebResponse> MyGetResponseAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			WebCompletionSource completion = new WebCompletionSource();
			object obj = this.locker;
			WebOperation operation;
			lock (obj)
			{
				this.getResponseCalled = true;
				WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref this.responseTask, completion, null);
				if (webCompletionSource != null)
				{
					webCompletionSource.ThrowOnError();
					if (this.haveResponse && webCompletionSource.Task.IsCompleted)
					{
						return this.webResponse;
					}
					throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
				}
				else
				{
					operation = this.currentOperation;
					if (this.currentOperation != null)
					{
						this.writeStream = this.currentOperation.WriteStream;
					}
					this.initialMethod = this.method;
					operation = this.SendRequest(false, null, cancellationToken);
				}
			}
			HttpWebResponse httpWebResponse;
			for (;;)
			{
				WebException throwMe = null;
				HttpWebResponse response = null;
				WebResponseStream stream = null;
				bool redirect = false;
				bool mustReadAll = false;
				WebOperation ntlm = null;
				BufferOffsetSize writeBuffer = null;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebRequestStream webRequestStream = await operation.GetRequestStreamInternal().ConfigureAwait(false);
					this.writeStream = webRequestStream;
					await this.writeStream.WriteRequestAsync(cancellationToken).ConfigureAwait(false);
					stream = await operation.GetResponseStream();
					ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation> valueTuple = await this.GetResponseFromData(stream, cancellationToken).ConfigureAwait(false);
					response = valueTuple.Item1;
					redirect = valueTuple.Item2;
					mustReadAll = valueTuple.Item3;
					writeBuffer = valueTuple.Item4;
					ntlm = valueTuple.Item5;
				}
				catch (Exception ex)
				{
					throwMe = this.GetWebException(ex);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (!redirect)
					{
						this.haveResponse = true;
						this.webResponse = response;
						completion.TrySetCompleted();
						httpWebResponse = response;
						break;
					}
					this.finished_reading = false;
					this.haveResponse = false;
					this.webResponse = null;
					this.currentOperation = ntlm;
				}
				try
				{
					if (mustReadAll)
					{
						await stream.ReadAllAsync(redirect || ntlm != null, cancellationToken).ConfigureAwait(false);
					}
					operation.Finish(true, null);
					response.Close();
				}
				catch (Exception ex2)
				{
					throwMe = this.GetWebException(ex2);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						WebResponseStream webResponseStream = stream;
						if (webResponseStream != null)
						{
							webResponseStream.Close();
						}
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (ntlm == null)
					{
						operation = this.SendRequest(true, writeBuffer, cancellationToken);
					}
					else
					{
						operation = ntlm;
					}
				}
				throwMe = null;
				response = null;
				stream = null;
				ntlm = null;
				writeBuffer = null;
			}
			return httpWebResponse;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0008D328 File Offset: 0x0008B528
		[return: TupleElementNames(new string[] { "response", "redirect", "mustReadAll", "writeBuffer", "ntlm" })]
		private async Task<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>> GetResponseFromData(WebResponseStream stream, CancellationToken cancellationToken)
		{
			HttpWebResponse response = new HttpWebResponse(this.actualUri, this.method, stream, this.cookieContainer);
			WebException throwMe = null;
			bool redirect = false;
			bool mustReadAll = false;
			WebOperation webOperation = null;
			Task<BufferOffsetSize> task = null;
			BufferOffsetSize bufferOffsetSize = null;
			object obj = this.locker;
			lock (obj)
			{
				ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> valueTuple = this.CheckFinalStatus(response);
				redirect = valueTuple.Item1;
				mustReadAll = valueTuple.Item2;
				task = valueTuple.Item3;
				throwMe = valueTuple.Item4;
			}
			if (throwMe != null)
			{
				if (mustReadAll)
				{
					await stream.ReadAllAsync(false, cancellationToken).ConfigureAwait(false);
				}
				throw throwMe;
			}
			if (task != null)
			{
				bufferOffsetSize = await task.ConfigureAwait(false);
			}
			obj = this.locker;
			lock (obj)
			{
				bool flag2 = this.ProxyQuery && this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
				if (!redirect)
				{
					if ((flag2 ? this.proxy_auth_state : this.auth_state).IsNtlmAuthenticated && response.StatusCode < HttpStatusCode.BadRequest)
					{
						stream.Connection.NtlmAuthenticated = true;
					}
					if (this.writeStream != null)
					{
						this.writeStream.KillBuffer();
					}
					return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, false, false, bufferOffsetSize, null);
				}
				if (this.sendChunked)
				{
					this.sendChunked = false;
					this.webHeaders.RemoveInternal("Transfer-Encoding");
				}
				webOperation = this.HandleNtlmAuth(stream, response, bufferOffsetSize, cancellationToken).Item1;
			}
			return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, true, mustReadAll, bufferOffsetSize, webOperation);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0008D37C File Offset: 0x0008B57C
		internal static Exception FlattenException(Exception e)
		{
			AggregateException ex = e as AggregateException;
			if (ex != null)
			{
				ex = ex.Flatten();
				if (ex.InnerExceptions.Count == 1)
				{
					return ex.InnerException;
				}
			}
			return e;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0008D3B0 File Offset: 0x0008B5B0
		private WebException GetWebException(Exception e)
		{
			return HttpWebRequest.GetWebException(e, this.Aborted);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		private static WebException GetWebException(Exception e, bool aborted)
		{
			e = HttpWebRequest.FlattenException(e);
			WebException ex = e as WebException;
			if (ex != null && (!aborted || ex.Status == WebExceptionStatus.RequestCanceled || ex.Status == WebExceptionStatus.Timeout))
			{
				return ex;
			}
			if (aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(e.Message, e, WebExceptionStatus.UnknownError, null);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x0008D41F File Offset: 0x0008B61F
		internal static WebException CreateRequestAbortedException()
		{
			return new WebException(SR.Format("The request was aborted: The request was canceled.", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
		}

		/// <summary>Begins an asynchronous request to an Internet resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request for a response.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate </param>
		/// <param name="state">The state object for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false.-or- The thread pool is running out of threads. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.-or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002669 RID: 9833 RVA: 0x0008D438 File Offset: 0x0008B638
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
			}
			return TaskToApm.Begin(this.RunWithTimeout<HttpWebResponse>(new Func<CancellationToken, Task<HttpWebResponse>>(this.MyGetResponseAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request to an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <param name="asyncResult">The pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult." />-or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> property is greater than 0 but the data has not been written to the request stream. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600266A RID: 9834 RVA: 0x0008D49C File Offset: 0x0008B69C
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WebResponse webResponse;
			try
			{
				webResponse = TaskToApm.End<HttpWebResponse>(asyncResult);
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return webResponse;
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="asyncResult">The pending request for a stream.</param>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		// Token: 0x0600266B RID: 9835 RVA: 0x0008D4DC File Offset: 0x0008B6DC
		public Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			context = null;
			return this.EndGetRequestStream(asyncResult);
		}

		/// <summary>Returns a response from an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater or equal to zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. -or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, this request includes data to be sent to the server. Requests that send data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600266C RID: 9836 RVA: 0x0008D4F8 File Offset: 0x0008B6F8
		public override WebResponse GetResponse()
		{
			WebResponse result;
			try
			{
				result = this.GetResponseAsync().Result;
			}
			catch (Exception ex)
			{
				throw this.GetWebException(ex);
			}
			return result;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x0008D530 File Offset: 0x0008B730
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x0008D538 File Offset: 0x0008B738
		internal bool FinishedReading
		{
			get
			{
				return this.finished_reading;
			}
			set
			{
				this.finished_reading = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x0008D541 File Offset: 0x0008B741
		internal bool Aborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.aborted, 0, 0) == 1;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002670 RID: 9840 RVA: 0x0008D554 File Offset: 0x0008B754
		public override void Abort()
		{
			if (Interlocked.CompareExchange(ref this.aborted, 1, 0) == 1)
			{
				return;
			}
			this.haveResponse = true;
			WebOperation webOperation = this.currentOperation;
			if (webOperation != null)
			{
				webOperation.Abort();
			}
			WebCompletionSource webCompletionSource = this.responseTask;
			if (webCompletionSource != null)
			{
				webCompletionSource.TrySetCanceled();
			}
			if (this.webResponse != null)
			{
				try
				{
					this.webResponse.Close();
					this.webResponse = null;
				}
				catch
				{
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002671 RID: 9841 RVA: 0x0008D5CC File Offset: 0x0008B7CC
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002672 RID: 9842 RVA: 0x0008D5CC File Offset: 0x0008B7CC
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0008D5D3 File Offset: 0x0008B7D3
		private void CheckRequestStarted()
		{
			if (this.requestSent)
			{
				throw new InvalidOperationException("request started");
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
		internal void DoContinueDelegate(int statusCode, WebHeaderCollection headers)
		{
			if (this.continueDelegate != null)
			{
				this.continueDelegate(statusCode, headers);
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008D5FF File Offset: 0x0008B7FF
		private void RewriteRedirectToGet()
		{
			this.method = "GET";
			this.webHeaders.RemoveInternal("Transfer-Encoding");
			this.sendChunked = false;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008D624 File Offset: 0x0008B824
		private bool Redirect(HttpStatusCode code, WebResponse response)
		{
			this.redirects++;
			Exception ex = null;
			string text = null;
			switch (code)
			{
			case HttpStatusCode.MultipleChoices:
				ex = new WebException("Ambiguous redirect.");
				goto IL_0097;
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
				if (this.method == "POST")
				{
					this.RewriteRedirectToGet();
					goto IL_0097;
				}
				goto IL_0097;
			case HttpStatusCode.SeeOther:
				this.RewriteRedirectToGet();
				goto IL_0097;
			case HttpStatusCode.NotModified:
				return false;
			case HttpStatusCode.UseProxy:
				ex = new NotImplementedException("Proxy support not available.");
				goto IL_0097;
			case HttpStatusCode.TemporaryRedirect:
				goto IL_0097;
			}
			string text2 = "Invalid status code: ";
			int num = (int)code;
			ex = new ProtocolViolationException(text2 + num.ToString());
			IL_0097:
			if (this.method != "GET" && !this.InternalAllowBuffering && this.ResendContentFactory == null && (this.writeStream.WriteBufferLength > 0 || this.contentLength > 0L))
			{
				ex = new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.AllowWriteStreamBuffering || this.method == "GET")
			{
				this.contentLength = -1L;
			}
			text = response.Headers["Location"];
			if (text == null)
			{
				throw new WebException(string.Format("No Location header found for {0}", (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			Uri uri = this.actualUri;
			try
			{
				this.actualUri = new Uri(this.actualUri, text);
			}
			catch (Exception)
			{
				throw new WebException(string.Format("Invalid URL ({0}) for {1}", text, (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			this.hostChanged = this.actualUri.Scheme != uri.Scheme || this.Host != uri.Authority;
			return true;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0008D7E0 File Offset: 0x0008B9E0
		private string GetHeaders()
		{
			bool flag = false;
			if (this.sendChunked)
			{
				flag = true;
				this.webHeaders.ChangeInternal("Transfer-Encoding", "chunked");
				this.webHeaders.RemoveInternal("Content-Length");
			}
			else if (this.contentLength != -1L)
			{
				if (this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge)
				{
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", "0");
					}
					else
					{
						this.webHeaders.RemoveInternal("Content-Length");
					}
				}
				else
				{
					if (this.contentLength > 0L)
					{
						flag = true;
					}
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", this.contentLength.ToString());
					}
				}
				this.webHeaders.RemoveInternal("Transfer-Encoding");
			}
			else
			{
				this.webHeaders.RemoveInternal("Content-Length");
			}
			if (this.actualVersion == HttpVersion.Version11 && flag && this.servicePoint.SendContinue)
			{
				this.webHeaders.ChangeInternal("Expect", "100-continue");
				this.expectContinue = true;
			}
			else
			{
				this.webHeaders.RemoveInternal("Expect");
				this.expectContinue = false;
			}
			bool proxyQuery = this.ProxyQuery;
			string text = (proxyQuery ? "Proxy-Connection" : "Connection");
			this.webHeaders.RemoveInternal((!proxyQuery) ? "Proxy-Connection" : "Connection");
			Version protocolVersion = this.servicePoint.ProtocolVersion;
			bool flag2 = protocolVersion == null || protocolVersion == HttpVersion.Version10;
			if (this.keepAlive && (this.version == HttpVersion.Version10 || flag2))
			{
				if (this.webHeaders[text] == null || this.webHeaders[text].IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) == -1)
				{
					this.webHeaders.ChangeInternal(text, "keep-alive");
				}
			}
			else if (!this.keepAlive && this.version == HttpVersion.Version11)
			{
				this.webHeaders.ChangeInternal(text, "close");
			}
			string text2;
			if (this.hostUri != null)
			{
				if (this.hostHasPort)
				{
					text2 = this.hostUri.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
				}
				else
				{
					text2 = this.hostUri.GetComponents(UriComponents.Host, UriFormat.Unescaped);
				}
			}
			else if (this.Address.IsDefaultPort)
			{
				text2 = this.Address.GetComponents(UriComponents.Host, UriFormat.Unescaped);
			}
			else
			{
				text2 = this.Address.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
			}
			this.webHeaders.SetInternal("Host", text2);
			if (this.cookieContainer != null)
			{
				string cookieHeader = this.cookieContainer.GetCookieHeader(this.actualUri);
				if (cookieHeader != "")
				{
					this.webHeaders.ChangeInternal("Cookie", cookieHeader);
				}
				else
				{
					this.webHeaders.RemoveInternal("Cookie");
				}
			}
			string text3 = null;
			if ((this.auto_decomp & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				text3 = "gzip";
			}
			if ((this.auto_decomp & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				text3 = ((text3 != null) ? "gzip, deflate" : "deflate");
			}
			if (text3 != null)
			{
				this.webHeaders.ChangeInternal("Accept-Encoding", text3);
			}
			if (!this.usedPreAuth && this.preAuthenticate)
			{
				this.DoPreAuthenticate();
			}
			return this.webHeaders.ToString();
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0008DB54 File Offset: 0x0008BD54
		private void DoPreAuthenticate()
		{
			bool flag = this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
			ICredentials credentials = ((!flag || this.credentials != null) ? this.credentials : this.proxy.Credentials);
			Authorization authorization = AuthenticationManager.PreAuthenticate(this, credentials);
			if (authorization == null)
			{
				return;
			}
			this.webHeaders.RemoveInternal("Proxy-Authorization");
			this.webHeaders.RemoveInternal("Authorization");
			string text = ((flag && this.credentials == null) ? "Proxy-Authorization" : "Authorization");
			this.webHeaders[text] = authorization.Message;
			this.usedPreAuth = true;
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0008DC00 File Offset: 0x0008BE00
		internal byte[] GetRequestHeaders()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (!this.ProxyQuery)
			{
				text = this.actualUri.PathAndQuery;
			}
			else
			{
				text = string.Format("{0}://{1}{2}", this.actualUri.Scheme, this.Host, this.actualUri.PathAndQuery);
			}
			if (!this.force_version && this.servicePoint.ProtocolVersion != null && this.servicePoint.ProtocolVersion < this.version)
			{
				this.actualVersion = this.servicePoint.ProtocolVersion;
			}
			else
			{
				this.actualVersion = this.version;
			}
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", new object[]
			{
				this.method,
				text,
				this.actualVersion.Major,
				this.actualVersion.Minor
			});
			stringBuilder.Append(this.GetHeaders());
			string text2 = stringBuilder.ToString();
			return Encoding.UTF8.GetBytes(text2);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008DD08 File Offset: 0x0008BF08
		private ValueTuple<WebOperation, bool> HandleNtlmAuth(WebResponseStream stream, HttpWebResponse response, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			bool flag = response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired;
			if ((flag ? this.proxy_auth_state : this.auth_state).NtlmAuthState == HttpWebRequest.NtlmAuthState.None)
			{
				return new ValueTuple<WebOperation, bool>(null, false);
			}
			bool flag2 = this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge;
			WebOperation webOperation = new WebOperation(this, writeBuffer, flag2, cancellationToken);
			stream.Operation.SetPriorityRequest(webOperation);
			ICredentials credentials = ((!flag || this.proxy == null) ? this.credentials : this.proxy.Credentials);
			if (credentials != null)
			{
				stream.Connection.NtlmCredential = credentials.GetCredential(this.requestUri, "NTLM");
				stream.Connection.UnsafeAuthenticatedConnectionSharing = this.unsafe_auth_blah;
			}
			return new ValueTuple<WebOperation, bool>(webOperation, flag2);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0008DDD4 File Offset: 0x0008BFD4
		private bool CheckAuthorization(WebResponse response, HttpStatusCode code)
		{
			if (code != HttpStatusCode.ProxyAuthenticationRequired)
			{
				return this.auth_state.CheckAuthorization(response, code);
			}
			return this.proxy_auth_state.CheckAuthorization(response, code);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0008DDFC File Offset: 0x0008BFFC
		[return: TupleElementNames(new string[] { "task", "throwMe" })]
		private ValueTuple<Task<BufferOffsetSize>, WebException> GetRewriteHandler(HttpWebResponse response, bool redirect)
		{
			if (redirect)
			{
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
				if (this.writeStream.WriteBufferLength == 0 || this.contentLength == 0L)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
			}
			if (this.AllowWriteStreamBuffering)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(Task.FromResult<BufferOffsetSize>(this.writeStream.GetWriteBuffer()), null);
			}
			if (this.ResendContentFactory == null)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response));
			}
			return new ValueTuple<Task<BufferOffsetSize>, WebException>(async delegate
			{
				BufferOffsetSize bufferOffsetSize;
				using (MemoryStream ms = new MemoryStream())
				{
					await this.ResendContentFactory(ms).ConfigureAwait(false);
					byte[] array = ms.ToArray();
					bufferOffsetSize = new BufferOffsetSize(array, 0, array.Length, false);
				}
				return bufferOffsetSize;
			}(), null);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0008DE8C File Offset: 0x0008C08C
		[return: TupleElementNames(new string[] { "redirect", "mustReadAll", "writeBuffer", "throwMe" })]
		private ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> CheckFinalStatus(HttpWebResponse response)
		{
			WebException ex = null;
			bool flag = false;
			Task<BufferOffsetSize> task = null;
			HttpStatusCode statusCode = response.StatusCode;
			if (((!this.auth_state.IsCompleted && statusCode == HttpStatusCode.Unauthorized && this.credentials != null) || (this.ProxyQuery && !this.proxy_auth_state.IsCompleted && statusCode == HttpStatusCode.ProxyAuthenticationRequired)) && !this.usedPreAuth && this.CheckAuthorization(response, statusCode))
			{
				flag = true;
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, null, null);
				}
				ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler = this.GetRewriteHandler(response, false);
				task = rewriteHandler.Item1;
				ex = rewriteHandler.Item2;
				if (ex == null)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, task, null);
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				this.writeStream.InternalClose();
				this.writeStream = null;
				response.Close();
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
			else
			{
				if (statusCode >= HttpStatusCode.BadRequest)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				else if (statusCode == HttpStatusCode.NotModified && this.allowAutoRedirect)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
				}
				else if (statusCode >= HttpStatusCode.MultipleChoices && this.allowAutoRedirect && this.redirects >= this.maxAutoRedirect)
				{
					ex = new WebException("Max. redirections exceeded.", null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				if (ex == null)
				{
					int num = (int)statusCode;
					bool flag2 = false;
					if (this.allowAutoRedirect && num >= 300)
					{
						flag2 = this.Redirect(statusCode, response);
						ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler2 = this.GetRewriteHandler(response, true);
						task = rewriteHandler2.Item1;
						ex = rewriteHandler2.Item2;
						if (flag2 && !this.unsafe_auth_blah)
						{
							this.auth_state.Reset();
							this.proxy_auth_state.Reset();
						}
					}
					if (num >= 300 && num != 304)
					{
						flag = true;
					}
					if (ex == null)
					{
						return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(flag2, flag, task, null);
					}
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				if (this.writeStream != null)
				{
					this.writeStream.InternalClose();
					this.writeStream = null;
				}
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x0008E0A0 File Offset: 0x0008C2A0
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x0008E0A8 File Offset: 0x0008C2A8
		internal bool ReuseConnection { get; set; }

		// Token: 0x06002680 RID: 9856 RVA: 0x0008E0B4 File Offset: 0x0008C2B4
		internal static StringBuilder GenerateConnectionGroup(string connectionGroupName, bool unsafeConnectionGroup, bool isInternalGroup)
		{
			StringBuilder stringBuilder = new StringBuilder(connectionGroupName);
			stringBuilder.Append(unsafeConnectionGroup ? "U>" : "S>");
			if (isInternalGroup)
			{
				stringBuilder.Append("I>");
			}
			return stringBuilder;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class.</summary>
		// Token: 0x06002683 RID: 9859 RVA: 0x00013B26 File Offset: 0x00011D26
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HttpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015EA RID: 5610
		private Uri requestUri;

		// Token: 0x040015EB RID: 5611
		private Uri actualUri;

		// Token: 0x040015EC RID: 5612
		private bool hostChanged;

		// Token: 0x040015ED RID: 5613
		private bool allowAutoRedirect;

		// Token: 0x040015EE RID: 5614
		private bool allowBuffering;

		// Token: 0x040015EF RID: 5615
		private bool allowReadStreamBuffering;

		// Token: 0x040015F0 RID: 5616
		private X509CertificateCollection certificates;

		// Token: 0x040015F1 RID: 5617
		private string connectionGroup;

		// Token: 0x040015F2 RID: 5618
		private bool haveContentLength;

		// Token: 0x040015F3 RID: 5619
		private long contentLength;

		// Token: 0x040015F4 RID: 5620
		private HttpContinueDelegate continueDelegate;

		// Token: 0x040015F5 RID: 5621
		private CookieContainer cookieContainer;

		// Token: 0x040015F6 RID: 5622
		private ICredentials credentials;

		// Token: 0x040015F7 RID: 5623
		private bool haveResponse;

		// Token: 0x040015F8 RID: 5624
		private bool requestSent;

		// Token: 0x040015F9 RID: 5625
		private WebHeaderCollection webHeaders;

		// Token: 0x040015FA RID: 5626
		private bool keepAlive;

		// Token: 0x040015FB RID: 5627
		private int maxAutoRedirect;

		// Token: 0x040015FC RID: 5628
		private string mediaType;

		// Token: 0x040015FD RID: 5629
		private string method;

		// Token: 0x040015FE RID: 5630
		private string initialMethod;

		// Token: 0x040015FF RID: 5631
		private bool pipelined;

		// Token: 0x04001600 RID: 5632
		private bool preAuthenticate;

		// Token: 0x04001601 RID: 5633
		private bool usedPreAuth;

		// Token: 0x04001602 RID: 5634
		private Version version;

		// Token: 0x04001603 RID: 5635
		private bool force_version;

		// Token: 0x04001604 RID: 5636
		private Version actualVersion;

		// Token: 0x04001605 RID: 5637
		private IWebProxy proxy;

		// Token: 0x04001606 RID: 5638
		private bool sendChunked;

		// Token: 0x04001607 RID: 5639
		private ServicePoint servicePoint;

		// Token: 0x04001608 RID: 5640
		private int timeout;

		// Token: 0x04001609 RID: 5641
		private int continueTimeout;

		// Token: 0x0400160A RID: 5642
		private WebRequestStream writeStream;

		// Token: 0x0400160B RID: 5643
		private HttpWebResponse webResponse;

		// Token: 0x0400160C RID: 5644
		private WebCompletionSource responseTask;

		// Token: 0x0400160D RID: 5645
		private WebOperation currentOperation;

		// Token: 0x0400160E RID: 5646
		private int aborted;

		// Token: 0x0400160F RID: 5647
		private bool gotRequestStream;

		// Token: 0x04001610 RID: 5648
		private int redirects;

		// Token: 0x04001611 RID: 5649
		private bool expectContinue;

		// Token: 0x04001612 RID: 5650
		private bool getResponseCalled;

		// Token: 0x04001613 RID: 5651
		private object locker;

		// Token: 0x04001614 RID: 5652
		private bool finished_reading;

		// Token: 0x04001615 RID: 5653
		private DecompressionMethods auto_decomp;

		// Token: 0x04001616 RID: 5654
		private int maxResponseHeadersLength;

		// Token: 0x04001617 RID: 5655
		private static int defaultMaxResponseHeadersLength = 64;

		// Token: 0x04001618 RID: 5656
		private static int defaultMaximumErrorResponseLength = 64;

		// Token: 0x04001619 RID: 5657
		private static RequestCachePolicy defaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x0400161A RID: 5658
		private int readWriteTimeout;

		// Token: 0x0400161B RID: 5659
		private MobileTlsProvider tlsProvider;

		// Token: 0x0400161C RID: 5660
		private MonoTlsSettings tlsSettings;

		// Token: 0x0400161D RID: 5661
		private ServerCertValidationCallback certValidationCallback;

		// Token: 0x0400161E RID: 5662
		private bool hostHasPort;

		// Token: 0x0400161F RID: 5663
		private Uri hostUri;

		// Token: 0x04001620 RID: 5664
		private HttpWebRequest.AuthorizationState auth_state;

		// Token: 0x04001621 RID: 5665
		private HttpWebRequest.AuthorizationState proxy_auth_state;

		// Token: 0x04001622 RID: 5666
		[NonSerialized]
		internal Func<Stream, Task> ResendContentFactory;

		// Token: 0x04001623 RID: 5667
		internal readonly int ID;

		// Token: 0x04001625 RID: 5669
		private bool unsafe_auth_blah;

		// Token: 0x020004A6 RID: 1190
		private enum NtlmAuthState
		{
			// Token: 0x04001628 RID: 5672
			None,
			// Token: 0x04001629 RID: 5673
			Challenge,
			// Token: 0x0400162A RID: 5674
			Response
		}

		// Token: 0x020004A7 RID: 1191
		private struct AuthorizationState
		{
			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06002684 RID: 9860 RVA: 0x0008E13B File Offset: 0x0008C33B
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06002685 RID: 9861 RVA: 0x0008E143 File Offset: 0x0008C343
			public HttpWebRequest.NtlmAuthState NtlmAuthState
			{
				get
				{
					return this.ntlm_auth_state;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06002686 RID: 9862 RVA: 0x0008E14B File Offset: 0x0008C34B
			public bool IsNtlmAuthenticated
			{
				get
				{
					return this.isCompleted && this.ntlm_auth_state > HttpWebRequest.NtlmAuthState.None;
				}
			}

			// Token: 0x06002687 RID: 9863 RVA: 0x0008E160 File Offset: 0x0008C360
			public AuthorizationState(HttpWebRequest request, bool isProxy)
			{
				this.request = request;
				this.isProxy = isProxy;
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
			}

			// Token: 0x06002688 RID: 9864 RVA: 0x0008E180 File Offset: 0x0008C380
			public bool CheckAuthorization(WebResponse response, HttpStatusCode code)
			{
				this.isCompleted = false;
				if (code == HttpStatusCode.Unauthorized && this.request.credentials == null)
				{
					return false;
				}
				if (this.isProxy != (code == HttpStatusCode.ProxyAuthenticationRequired))
				{
					return false;
				}
				if (this.isProxy && (this.request.proxy == null || this.request.proxy.Credentials == null))
				{
					return false;
				}
				string[] values = response.Headers.GetValues(this.isProxy ? "Proxy-Authenticate" : "WWW-Authenticate");
				if (values == null || values.Length == 0)
				{
					return false;
				}
				ICredentials credentials = ((!this.isProxy) ? this.request.credentials : this.request.proxy.Credentials);
				Authorization authorization = null;
				string[] array = values;
				for (int i = 0; i < array.Length; i++)
				{
					authorization = AuthenticationManager.Authenticate(array[i], this.request, credentials);
					if (authorization != null)
					{
						break;
					}
				}
				if (authorization == null)
				{
					return false;
				}
				this.request.webHeaders[this.isProxy ? "Proxy-Authorization" : "Authorization"] = authorization.Message;
				this.isCompleted = authorization.Complete;
				if (authorization.ModuleAuthenticationType == "NTLM")
				{
					this.ntlm_auth_state++;
				}
				return true;
			}

			// Token: 0x06002689 RID: 9865 RVA: 0x0008E2BB File Offset: 0x0008C4BB
			public void Reset()
			{
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
				this.request.webHeaders.RemoveInternal(this.isProxy ? "Proxy-Authorization" : "Authorization");
			}

			// Token: 0x0600268A RID: 9866 RVA: 0x0008E2EF File Offset: 0x0008C4EF
			public override string ToString()
			{
				return string.Format("{0}AuthState [{1}:{2}]", this.isProxy ? "Proxy" : "", this.isCompleted, this.ntlm_auth_state);
			}

			// Token: 0x0400162B RID: 5675
			private readonly HttpWebRequest request;

			// Token: 0x0400162C RID: 5676
			private readonly bool isProxy;

			// Token: 0x0400162D RID: 5677
			private bool isCompleted;

			// Token: 0x0400162E RID: 5678
			private HttpWebRequest.NtlmAuthState ntlm_auth_state;
		}
	}
}
