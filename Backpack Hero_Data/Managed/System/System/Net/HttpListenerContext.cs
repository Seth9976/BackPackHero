using System;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200049C RID: 1180
	public sealed class HttpListenerContext
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x0008A39E File Offset: 0x0008859E
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.err_status = 400;
			base..ctor();
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x0008A3D0 File Offset: 0x000885D0
		// (set) Token: 0x06002553 RID: 9555 RVA: 0x0008A3D8 File Offset: 0x000885D8
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0008A3E1 File Offset: 0x000885E1
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x0008A3E9 File Offset: 0x000885E9
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x0008A3F2 File Offset: 0x000885F2
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0008A3FD File Offset: 0x000885FD
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x0008A405 File Offset: 0x00088605
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request. </summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x0008A40D File Offset: 0x0008860D
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object. </summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or null if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x0008A415 File Offset: 0x00088615
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0008A420 File Offset: 0x00088620
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[] { ' ' }, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0008A48C File Offset: 0x0008868C
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal principal;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string text3;
				if (num > 0)
				{
					text3 = text.Substring(num);
				}
				else
				{
					text3 = text;
				}
				principal = new GenericPrincipal(new HttpListenerBasicIdentity(text3, text2), new string[0]);
			}
			catch (Exception)
			{
				principal = null;
			}
			return principal;
		}

		/// <summary>Accept a WebSocket connection as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x0600255D RID: 9565 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol  and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x0600255E RID: 9566 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x0600255F RID: 9567 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, WebSocket keep-alive interval, and the internal buffer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <param name="internalBuffer">An internal buffer to use for this operation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x06002560 RID: 9568 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00013B26 File Offset: 0x00011D26
		internal HttpListenerContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015AF RID: 5551
		private HttpListenerRequest request;

		// Token: 0x040015B0 RID: 5552
		private HttpListenerResponse response;

		// Token: 0x040015B1 RID: 5553
		private IPrincipal user;

		// Token: 0x040015B2 RID: 5554
		private HttpConnection cnc;

		// Token: 0x040015B3 RID: 5555
		private string error;

		// Token: 0x040015B4 RID: 5556
		private int err_status;

		// Token: 0x040015B5 RID: 5557
		internal HttpListener Listener;
	}
}
