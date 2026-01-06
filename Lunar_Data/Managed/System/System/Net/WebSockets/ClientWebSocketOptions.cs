using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x020005EF RID: 1519
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x000AE4F6 File Offset: 0x000AC6F6
		internal ClientWebSocketOptions()
		{
			this._requestedSubProtocols = new List<string>();
			this._requestHeaders = new WebHeaderCollection();
		}

		/// <summary>Creates a HTTP request header and its value.</summary>
		/// <param name="headerName">The name of the HTTP header.</param>
		/// <param name="headerValue">The value of the HTTP header.</param>
		// Token: 0x060030C8 RID: 12488 RVA: 0x000AE535 File Offset: 0x000AC735
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this._requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000AE54A File Offset: 0x000AC74A
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this._requestHeaders;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000AE552 File Offset: 0x000AC752
		internal List<string> RequestedSubProtocols
		{
			get
			{
				return this._requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates if default credentials should be used during WebSocket handshake.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if default credentials should be used during WebSocket handshake; otherwise false. The default is true.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000AE55A File Offset: 0x000AC75A
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x000AE562 File Offset: 0x000AC762
		public bool UseDefaultCredentials
		{
			get
			{
				return this._useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._useDefaultCredentials = value;
			}
		}

		/// <summary>Gets or sets the credential information for the client.</summary>
		/// <returns>Returns <see cref="T:System.Net.ICredentials" />.The credential information for the client.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000AE571 File Offset: 0x000AC771
		// (set) Token: 0x060030CE RID: 12494 RVA: 0x000AE579 File Offset: 0x000AC779
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>Returns <see cref="T:System.Net.IWebProxy" />.The proxy for WebSocket requests.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000AE588 File Offset: 0x000AC788
		// (set) Token: 0x060030D0 RID: 12496 RVA: 0x000AE590 File Offset: 0x000AC790
		public IWebProxy Proxy
		{
			get
			{
				return this._proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>Returns <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.A collection of client side certificates.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000AE59F File Offset: 0x000AC79F
		// (set) Token: 0x060030D2 RID: 12498 RVA: 0x000AE5BA File Offset: 0x000AC7BA
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509CertificateCollection();
				}
				return this._clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000AE5D7 File Offset: 0x000AC7D7
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x000AE5DF File Offset: 0x000AC7DF
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			get
			{
				return this._remoteCertificateValidationCallback;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._remoteCertificateValidationCallback = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieContainer" />.The cookies associated with the request.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000AE5EE File Offset: 0x000AC7EE
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x000AE5F6 File Offset: 0x000AC7F6
		public CookieContainer Cookies
		{
			get
			{
				return this._cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._cookies = value;
			}
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x060030D7 RID: 12503 RVA: 0x000AE608 File Offset: 0x000AC808
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketValidate.ValidateSubprotocol(subProtocol);
			using (List<string>.Enumerator enumerator = this._requestedSubProtocols.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current, subProtocol, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(SR.Format("Duplicate protocols are not allowed: '{0}'.", subProtocol), "subProtocol");
					}
				}
			}
			this._requestedSubProtocols.Add(subProtocol);
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The WebSocket protocol keep-alive interval in milliseconds.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000AE68C File Offset: 0x000AC88C
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x000AE694 File Offset: 0x000AC894
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this._keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value != Timeout.InfiniteTimeSpan && value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format("The argument must be a value greater than {0}.", Timeout.InfiniteTimeSpan.ToString()));
				}
				this._keepAliveInterval = value;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000AE6F3 File Offset: 0x000AC8F3
		internal int ReceiveBufferSize
		{
			get
			{
				return this._receiveBufferSize;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x000AE6FB File Offset: 0x000AC8FB
		internal int SendBufferSize
		{
			get
			{
				return this._sendBufferSize;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000AE703 File Offset: 0x000AC903
		internal ArraySegment<byte>? Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Sets the client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		// Token: 0x060030DD RID: 12509 RVA: 0x000AE70C File Offset: 0x000AC90C
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = null;
		}

		/// <summary>Sets client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		/// <param name="buffer">The receive buffer to use.</param>
		// Token: 0x060030DE RID: 12510 RVA: 0x000AE784 File Offset: 0x000AC984
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			if (buffer.Count == 0)
			{
				throw new ArgumentOutOfRangeException("buffer");
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = new ArraySegment<byte>?(buffer);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000AE81A File Offset: 0x000ACA1A
		internal void SetToReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000AE823 File Offset: 0x000ACA23
		private void ThrowIfReadOnly()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
		}

		// Token: 0x04001D9E RID: 7582
		private bool _isReadOnly;

		// Token: 0x04001D9F RID: 7583
		private readonly List<string> _requestedSubProtocols;

		// Token: 0x04001DA0 RID: 7584
		private readonly WebHeaderCollection _requestHeaders;

		// Token: 0x04001DA1 RID: 7585
		private TimeSpan _keepAliveInterval = WebSocket.DefaultKeepAliveInterval;

		// Token: 0x04001DA2 RID: 7586
		private bool _useDefaultCredentials;

		// Token: 0x04001DA3 RID: 7587
		private ICredentials _credentials;

		// Token: 0x04001DA4 RID: 7588
		private IWebProxy _proxy;

		// Token: 0x04001DA5 RID: 7589
		private X509CertificateCollection _clientCertificates;

		// Token: 0x04001DA6 RID: 7590
		private CookieContainer _cookies;

		// Token: 0x04001DA7 RID: 7591
		private int _receiveBufferSize = 4096;

		// Token: 0x04001DA8 RID: 7592
		private int _sendBufferSize = 4096;

		// Token: 0x04001DA9 RID: 7593
		private ArraySegment<byte>? _buffer;

		// Token: 0x04001DAA RID: 7594
		private RemoteCertificateValidationCallback _remoteCertificateValidationCallback;
	}
}
