using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	/// <summary>Used for accessing the information in the WebSocket handshake.</summary>
	// Token: 0x020005FC RID: 1532
	public abstract class WebSocketContext
	{
		/// <summary>The URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.</returns>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003129 RID: 12585
		public abstract Uri RequestUri { get; }

		/// <summary>The HTTP headers that were sent to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600312A RID: 12586
		public abstract NameValueCollection Headers { get; }

		/// <summary>The value of the Origin HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600312B RID: 12587
		public abstract string Origin { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600312C RID: 12588
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		/// <summary>The list of subprotocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600312D RID: 12589
		public abstract string SecWebSocketVersion { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600312E RID: 12590
		public abstract string SecWebSocketKey { get; }

		/// <summary>The cookies that were passed to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600312F RID: 12591
		public abstract CookieCollection CookieCollection { get; }

		/// <summary>An object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003130 RID: 12592
		public abstract IPrincipal User { get; }

		/// <summary>Whether the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003131 RID: 12593
		public abstract bool IsAuthenticated { get; }

		/// <summary>Whether the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003132 RID: 12594
		public abstract bool IsLocal { get; }

		/// <summary>Whether the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003133 RID: 12595
		public abstract bool IsSecureConnection { get; }

		/// <summary>The WebSocket instance used to interact (send/receive/close/etc) with the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003134 RID: 12596
		public abstract WebSocket WebSocket { get; }
	}
}
