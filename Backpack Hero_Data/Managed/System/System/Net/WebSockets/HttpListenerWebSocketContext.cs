using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using Unity;

namespace System.Net.WebSockets
{
	/// <summary>Provides access to information received by the <see cref="T:System.Net.HttpListener" /> class when accepting WebSocket connections.</summary>
	// Token: 0x020005EA RID: 1514
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x0600309E RID: 12446 RVA: 0x000ADF5C File Offset: 0x000AC15C
		internal HttpListenerWebSocketContext(Uri requestUri, NameValueCollection headers, CookieCollection cookieCollection, IPrincipal user, bool isAuthenticated, bool isLocal, bool isSecureConnection, string origin, IEnumerable<string> secWebSocketProtocols, string secWebSocketVersion, string secWebSocketKey, WebSocket webSocket)
		{
			this._cookieCollection = new CookieCollection();
			this._cookieCollection.Add(cookieCollection);
			this._headers = new NameValueCollection(headers);
			this._user = HttpListenerWebSocketContext.CopyPrincipal(user);
			this._requestUri = requestUri;
			this._isAuthenticated = isAuthenticated;
			this._isLocal = isLocal;
			this._isSecureConnection = isSecureConnection;
			this._origin = origin;
			this._secWebSocketProtocols = secWebSocketProtocols;
			this._secWebSocketVersion = secWebSocketVersion;
			this._secWebSocketKey = secWebSocketKey;
			this._webSocket = webSocket;
		}

		/// <summary>Gets the URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The URI requested by the WebSocket client.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000ADFE6 File Offset: 0x000AC1E6
		public override Uri RequestUri
		{
			get
			{
				return this._requestUri;
			}
		}

		/// <summary>Gets the HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.The HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000ADFEE File Offset: 0x000AC1EE
		public override NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		/// <summary>Gets the value of the Origin HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the Origin HTTP header.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000ADFF6 File Offset: 0x000AC1F6
		public override string Origin
		{
			get
			{
				return this._origin;
			}
		}

		/// <summary>Gets the list of the Secure WebSocket protocols included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.The list of the Secure WebSocket protocols.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000ADFFE File Offset: 0x000AC1FE
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this._secWebSocketProtocols;
			}
		}

		/// <summary>Gets the list of sub-protocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The list of sub-protocols requested by the WebSocket client.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000AE006 File Offset: 0x000AC206
		public override string SecWebSocketVersion
		{
			get
			{
				return this._secWebSocketVersion;
			}
		}

		/// <summary>Gets the value of the SecWebSocketKey HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the SecWebSocketKey HTTP header.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000AE00E File Offset: 0x000AC20E
		public override string SecWebSocketKey
		{
			get
			{
				return this._secWebSocketKey;
			}
		}

		/// <summary>Gets the cookies received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.The cookies received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000AE016 File Offset: 0x000AC216
		public override CookieCollection CookieCollection
		{
			get
			{
				return this._cookieCollection;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.The identity, authentication information, and security roles for the WebSocket client.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000AE01E File Offset: 0x000AC21E
		public override IPrincipal User
		{
			get
			{
				return this._user;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client is authenticated; otherwise false.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000AE026 File Offset: 0x000AC226
		public override bool IsAuthenticated
		{
			get
			{
				return this._isAuthenticated;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client connected from the local machine; otherwise false.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000AE02E File Offset: 0x000AC22E
		public override bool IsLocal
		{
			get
			{
				return this._isLocal;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket connection is secured using SSL; otherwise false.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000AE036 File Offset: 0x000AC236
		public override bool IsSecureConnection
		{
			get
			{
				return this._isSecureConnection;
			}
		}

		/// <summary>Gets the WebSocket instance used to send and receive data over the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.The WebSocket instance.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000AE03E File Offset: 0x000AC23E
		public override WebSocket WebSocket
		{
			get
			{
				return this._webSocket;
			}
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000AE048 File Offset: 0x000AC248
		private static IPrincipal CopyPrincipal(IPrincipal user)
		{
			if (user != null)
			{
				if (user is WindowsPrincipal)
				{
					throw new PlatformNotSupportedException();
				}
				HttpListenerBasicIdentity httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity;
				if (httpListenerBasicIdentity != null)
				{
					return new GenericPrincipal(new HttpListenerBasicIdentity(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password), null);
				}
			}
			return null;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x00013B26 File Offset: 0x00011D26
		internal HttpListenerWebSocketContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D83 RID: 7555
		private readonly Uri _requestUri;

		// Token: 0x04001D84 RID: 7556
		private readonly NameValueCollection _headers;

		// Token: 0x04001D85 RID: 7557
		private readonly CookieCollection _cookieCollection;

		// Token: 0x04001D86 RID: 7558
		private readonly IPrincipal _user;

		// Token: 0x04001D87 RID: 7559
		private readonly bool _isAuthenticated;

		// Token: 0x04001D88 RID: 7560
		private readonly bool _isLocal;

		// Token: 0x04001D89 RID: 7561
		private readonly bool _isSecureConnection;

		// Token: 0x04001D8A RID: 7562
		private readonly string _origin;

		// Token: 0x04001D8B RID: 7563
		private readonly IEnumerable<string> _secWebSocketProtocols;

		// Token: 0x04001D8C RID: 7564
		private readonly string _secWebSocketVersion;

		// Token: 0x04001D8D RID: 7565
		private readonly string _secWebSocketKey;

		// Token: 0x04001D8E RID: 7566
		private readonly WebSocket _webSocket;
	}
}
