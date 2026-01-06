using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>The default message handler used by <see cref="T:System.Net.Http.HttpClient" />.  </summary>
	// Token: 0x0200000A RID: 10
	public class HttpClientHandler : HttpMessageHandler
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000027D6 File Offset: 0x000009D6
		private static IMonoHttpClientHandler CreateDefaultHandler()
		{
			return new MonoWebRequestHandler();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000027DD File Offset: 0x000009DD
		public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> DangerousAcceptAnyServerCertificateValidator
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.HttpClientHandler" /> class.</summary>
		// Token: 0x06000020 RID: 32 RVA: 0x000027E4 File Offset: 0x000009E4
		public HttpClientHandler()
			: this(HttpClientHandler.CreateDefaultHandler())
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027F1 File Offset: 0x000009F1
		internal HttpClientHandler(IMonoHttpClientHandler handler)
		{
			this._delegatingHandler = handler;
			this.ClientCertificateOptions = ClientCertificateOption.Manual;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpClientHandler" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06000022 RID: 34 RVA: 0x00002807 File Offset: 0x00000A07
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._delegatingHandler.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets a value that indicates whether the handler supports automatic response content decompression.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the if the handler supports automatic response content decompression; otherwise false. The default value is true.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000281E File Offset: 0x00000A1E
		public virtual bool SupportsAutomaticDecompression
		{
			get
			{
				return this._delegatingHandler.SupportsAutomaticDecompression;
			}
		}

		/// <summary>Gets a value that indicates whether the handler supports proxy settings.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the if the handler supports proxy settings; otherwise false. The default value is true.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000282B File Offset: 0x00000A2B
		public virtual bool SupportsProxy
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the if the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties; otherwise false. The default value is true.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000282B File Offset: 0x00000A2B
		public virtual bool SupportsRedirectConfiguration
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the if the handler supports uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests; otherwise false. The default value is true.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000282E File Offset: 0x00000A2E
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000283B File Offset: 0x00000A3B
		public bool UseCookies
		{
			get
			{
				return this._delegatingHandler.UseCookies;
			}
			set
			{
				this._delegatingHandler.UseCookies = value;
			}
		}

		/// <summary>Gets or sets the cookie container used to store server cookies by the handler.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieContainer" />.The cookie container used to store server cookies by the handler.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002849 File Offset: 0x00000A49
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002856 File Offset: 0x00000A56
		public CookieContainer CookieContainer
		{
			get
			{
				return this._delegatingHandler.CookieContainer;
			}
			set
			{
				this._delegatingHandler.CookieContainer = value;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002864 File Offset: 0x00000A64
		private void ThrowForModifiedManagedSslOptionsIfStarted()
		{
			this._delegatingHandler.SslOptions = this._delegatingHandler.SslOptions;
		}

		/// <summary>Gets or sets the collection of security certificates that are associated with this handler.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.ClientCertificateOption" />.The collection of security certificates associated with this handler.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002884 File Offset: 0x00000A84
		public ClientCertificateOption ClientCertificateOptions
		{
			get
			{
				return this._clientCertificateOptions;
			}
			set
			{
				if (value == ClientCertificateOption.Manual)
				{
					this.ThrowForModifiedManagedSslOptionsIfStarted();
					this._clientCertificateOptions = value;
					this._delegatingHandler.SslOptions.LocalCertificateSelectionCallback = (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate(this.ClientCertificates);
					return;
				}
				if (value != ClientCertificateOption.Automatic)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._clientCertificateOptions = value;
				this._delegatingHandler.SslOptions.LocalCertificateSelectionCallback = (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000290C File Offset: 0x00000B0C
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.ClientCertificateOptions != ClientCertificateOption.Manual)
				{
					throw new InvalidOperationException(SR.Format("The {0} property must be set to '{1}' to use this property.", "ClientCertificateOptions", "Manual"));
				}
				X509CertificateCollection x509CertificateCollection;
				if ((x509CertificateCollection = this._delegatingHandler.SslOptions.ClientCertificates) == null)
				{
					x509CertificateCollection = (this._delegatingHandler.SslOptions.ClientCertificates = new X509CertificateCollection());
				}
				return x509CertificateCollection;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002967 File Offset: 0x00000B67
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002995 File Offset: 0x00000B95
		public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> ServerCertificateCustomValidationCallback
		{
			get
			{
				RemoteCertificateValidationCallback remoteCertificateValidationCallback = this._delegatingHandler.SslOptions.RemoteCertificateValidationCallback;
				ConnectHelper.CertificateCallbackMapper certificateCallbackMapper = ((remoteCertificateValidationCallback != null) ? remoteCertificateValidationCallback.Target : null) as ConnectHelper.CertificateCallbackMapper;
				if (certificateCallbackMapper == null)
				{
					return null;
				}
				return certificateCallbackMapper.FromHttpClientHandler;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.RemoteCertificateValidationCallback = ((value != null) ? new ConnectHelper.CertificateCallbackMapper(value).ForSocketsHttpHandler : null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029BE File Offset: 0x00000BBE
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000029D3 File Offset: 0x00000BD3
		public bool CheckCertificateRevocationList
		{
			get
			{
				return this._delegatingHandler.SslOptions.CertificateRevocationCheckMode == X509RevocationMode.Online;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.CertificateRevocationCheckMode = (value ? X509RevocationMode.Online : X509RevocationMode.NoCheck);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029F2 File Offset: 0x00000BF2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002A04 File Offset: 0x00000C04
		public SslProtocols SslProtocols
		{
			get
			{
				return this._delegatingHandler.SslOptions.EnabledSslProtocols;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.EnabledSslProtocols = value;
			}
		}

		/// <summary>Gets or sets the type of decompression method used by the handler for automatic decompression of the HTTP content response.</summary>
		/// <returns>Returns <see cref="T:System.Net.DecompressionMethods" />.The automatic decompression method used by the handler. The default value is <see cref="F:System.Net.DecompressionMethods.None" />.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A1D File Offset: 0x00000C1D
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002A2A File Offset: 0x00000C2A
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this._delegatingHandler.AutomaticDecompression;
			}
			set
			{
				this._delegatingHandler.AutomaticDecompression = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler uses a proxy for requests. </summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the handler should use a proxy for requests; otherwise false. The default value is true.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A38 File Offset: 0x00000C38
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002A45 File Offset: 0x00000C45
		public bool UseProxy
		{
			get
			{
				return this._delegatingHandler.UseProxy;
			}
			set
			{
				this._delegatingHandler.UseProxy = value;
			}
		}

		/// <summary>Gets or sets proxy information used by the handler.</summary>
		/// <returns>Returns <see cref="T:System.Net.IWebProxy" />.The proxy information used by the handler. The default value is null.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A53 File Offset: 0x00000C53
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A60 File Offset: 0x00000C60
		public IWebProxy Proxy
		{
			get
			{
				return this._delegatingHandler.Proxy;
			}
			set
			{
				this._delegatingHandler.Proxy = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002A7B File Offset: 0x00000C7B
		public ICredentials DefaultProxyCredentials
		{
			get
			{
				return this._delegatingHandler.DefaultProxyCredentials;
			}
			set
			{
				this._delegatingHandler.DefaultProxyCredentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler sends an Authorization header with the request.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true for the handler to send an HTTP Authorization header with requests after authentication has taken place; otherwise, false. The default is false.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A89 File Offset: 0x00000C89
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002A96 File Offset: 0x00000C96
		public bool PreAuthenticate
		{
			get
			{
				return this._delegatingHandler.PreAuthenticate;
			}
			set
			{
				this._delegatingHandler.PreAuthenticate = value;
			}
		}

		/// <summary>Gets or sets a value that controls whether default credentials are sent with requests by the handler.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the default credentials are used; otherwise false. The default value is false.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AA4 File Offset: 0x00000CA4
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public bool UseDefaultCredentials
		{
			get
			{
				return this._delegatingHandler.Credentials == CredentialCache.DefaultCredentials;
			}
			set
			{
				if (value)
				{
					this._delegatingHandler.Credentials = CredentialCache.DefaultCredentials;
					return;
				}
				if (this._delegatingHandler.Credentials == CredentialCache.DefaultCredentials)
				{
					this._delegatingHandler.Credentials = null;
				}
			}
		}

		/// <summary>Gets or sets authentication information used by this handler.</summary>
		/// <returns>Returns <see cref="T:System.Net.ICredentials" />.The authentication credentials associated with the handler. The default is null.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public ICredentials Credentials
		{
			get
			{
				return this._delegatingHandler.Credentials;
			}
			set
			{
				this._delegatingHandler.Credentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler should follow redirection responses.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the if the handler should follow redirection responses; otherwise false. The default value is true.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002B07 File Offset: 0x00000D07
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002B14 File Offset: 0x00000D14
		public bool AllowAutoRedirect
		{
			get
			{
				return this._delegatingHandler.AllowAutoRedirect;
			}
			set
			{
				this._delegatingHandler.AllowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the handler follows.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The maximum number of redirection responses that the handler follows. The default value is 50.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B22 File Offset: 0x00000D22
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002B2F File Offset: 0x00000D2F
		public int MaxAutomaticRedirections
		{
			get
			{
				return this._delegatingHandler.MaxAutomaticRedirections;
			}
			set
			{
				this._delegatingHandler.MaxAutomaticRedirections = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B3D File Offset: 0x00000D3D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002B4A File Offset: 0x00000D4A
		public int MaxConnectionsPerServer
		{
			get
			{
				return this._delegatingHandler.MaxConnectionsPerServer;
			}
			set
			{
				this._delegatingHandler.MaxConnectionsPerServer = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002B65 File Offset: 0x00000D65
		public int MaxResponseHeadersLength
		{
			get
			{
				return this._delegatingHandler.MaxResponseHeadersLength;
			}
			set
			{
				this._delegatingHandler.MaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the maximum request content buffer size used by the handler.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The maximum request content buffer size in bytes. The default value is 2 gigabytes.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B73 File Offset: 0x00000D73
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B80 File Offset: 0x00000D80
		public long MaxRequestContentBufferSize
		{
			get
			{
				return this._delegatingHandler.MaxRequestContentBufferSize;
			}
			set
			{
				this._delegatingHandler.MaxRequestContentBufferSize = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B8E File Offset: 0x00000D8E
		public IDictionary<string, object> Properties
		{
			get
			{
				return this._delegatingHandler.Properties;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B9B File Offset: 0x00000D9B
		internal void SetWebRequestTimeout(TimeSpan timeout)
		{
			this._delegatingHandler.SetWebRequestTimeout(timeout);
		}

		/// <summary>Creates an instance of  <see cref="T:System.Net.Http.HttpResponseMessage" /> based on the information provided in the <see cref="T:System.Net.Http.HttpRequestMessage" /> as an operation that will not block.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="request">The HTTP request message.</param>
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
		// Token: 0x0600004E RID: 78 RVA: 0x00002BA9 File Offset: 0x00000DA9
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._delegatingHandler.SendAsync(request, cancellationToken);
		}

		// Token: 0x0400001C RID: 28
		private readonly IMonoHttpClientHandler _delegatingHandler;

		// Token: 0x0400001D RID: 29
		private ClientCertificateOption _clientCertificateOptions;
	}
}
