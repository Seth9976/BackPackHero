using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x020005F0 RID: 1520
	internal sealed class WebSocketHandle
	{
		// Token: 0x060030E1 RID: 12513 RVA: 0x000AE838 File Offset: 0x000ACA38
		public static WebSocketHandle Create()
		{
			return new WebSocketHandle();
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000AE83F File Offset: 0x000ACA3F
		public static bool IsValid(WebSocketHandle handle)
		{
			return handle != null;
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000AE848 File Offset: 0x000ACA48
		public WebSocketCloseStatus? CloseStatus
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.CloseStatus;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000AE86E File Offset: 0x000ACA6E
		public string CloseStatusDescription
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.CloseStatusDescription;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000AE881 File Offset: 0x000ACA81
		public WebSocketState State
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return this._state;
				}
				return webSocket.State;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000AE899 File Offset: 0x000ACA99
		public string SubProtocol
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.SubProtocol;
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x00003917 File Offset: 0x00001B17
		public static void CheckPlatformSupport()
		{
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000AE8AC File Offset: 0x000ACAAC
		public void Dispose()
		{
			this._state = WebSocketState.Closed;
			WebSocket webSocket = this._webSocket;
			if (webSocket == null)
			{
				return;
			}
			webSocket.Dispose();
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000AE8C5 File Offset: 0x000ACAC5
		public void Abort()
		{
			this._abortSource.Cancel();
			WebSocket webSocket = this._webSocket;
			if (webSocket == null)
			{
				return;
			}
			webSocket.Abort();
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000AE8E2 File Offset: 0x000ACAE2
		public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			return this._webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000AE8F4 File Offset: 0x000ACAF4
		public ValueTask SendAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			return this._webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000AE906 File Offset: 0x000ACB06
		public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			return this._webSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000AE915 File Offset: 0x000ACB15
		public ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			return this._webSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000AE924 File Offset: 0x000ACB24
		public Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this._webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000AE934 File Offset: 0x000ACB34
		public Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this._webSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000AE944 File Offset: 0x000ACB44
		public Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken, ClientWebSocketOptions options)
		{
			WebSocketHandle.<ConnectAsyncCore>d__26 <ConnectAsyncCore>d__;
			<ConnectAsyncCore>d__.<>4__this = this;
			<ConnectAsyncCore>d__.uri = uri;
			<ConnectAsyncCore>d__.cancellationToken = cancellationToken;
			<ConnectAsyncCore>d__.options = options;
			<ConnectAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ConnectAsyncCore>d__.<>1__state = -1;
			<ConnectAsyncCore>d__.<>t__builder.Start<WebSocketHandle.<ConnectAsyncCore>d__26>(ref <ConnectAsyncCore>d__);
			return <ConnectAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
		private async Task<Socket> ConnectSocketAsync(string host, int port, CancellationToken cancellationToken)
		{
			IPAddress[] array = await Dns.GetHostAddressesAsync(host).ConfigureAwait(false);
			ExceptionDispatchInfo exceptionDispatchInfo = null;
			foreach (IPAddress ipaddress in array)
			{
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					CancellationToken cancellationToken2;
					using (cancellationToken.Register(delegate(object s)
					{
						((Socket)s).Dispose();
					}, socket))
					{
						cancellationToken2 = this._abortSource.Token;
						using (cancellationToken2.Register(delegate(object s)
						{
							((Socket)s).Dispose();
						}, socket))
						{
							try
							{
								await socket.ConnectAsync(ipaddress, port).ConfigureAwait(false);
							}
							catch (ObjectDisposedException ex)
							{
								CancellationToken cancellationToken3 = (cancellationToken.IsCancellationRequested ? cancellationToken : this._abortSource.Token);
								if (cancellationToken3.IsCancellationRequested)
								{
									throw new OperationCanceledException(new OperationCanceledException().Message, ex, cancellationToken3);
								}
							}
						}
						CancellationTokenRegistration cancellationTokenRegistration2 = default(CancellationTokenRegistration);
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
					cancellationToken.ThrowIfCancellationRequested();
					cancellationToken2 = this._abortSource.Token;
					cancellationToken2.ThrowIfCancellationRequested();
					return socket;
				}
				catch (Exception ex2)
				{
					socket.Dispose();
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex2);
				}
				socket = null;
			}
			IPAddress[] array2 = null;
			if (exceptionDispatchInfo != null)
			{
				exceptionDispatchInfo.Throw();
			}
			throw new WebSocketException("Unable to connect to the remote server");
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000AE9FC File Offset: 0x000ACBFC
		private static byte[] BuildRequestHeader(Uri uri, ClientWebSocketOptions options, string secKey)
		{
			StringBuilder stringBuilder;
			if ((stringBuilder = WebSocketHandle.t_cachedStringBuilder) == null)
			{
				stringBuilder = (WebSocketHandle.t_cachedStringBuilder = new StringBuilder());
			}
			StringBuilder stringBuilder2 = stringBuilder;
			byte[] bytes;
			try
			{
				stringBuilder2.Append("GET ").Append(uri.PathAndQuery).Append(" HTTP/1.1\r\n");
				string text = options.RequestHeaders["Host"];
				stringBuilder2.Append("Host: ");
				if (string.IsNullOrEmpty(text))
				{
					stringBuilder2.Append(uri.IdnHost).Append(':').Append(uri.Port)
						.Append("\r\n");
				}
				else
				{
					stringBuilder2.Append(text).Append("\r\n");
				}
				stringBuilder2.Append("Connection: Upgrade\r\n");
				stringBuilder2.Append("Upgrade: websocket\r\n");
				stringBuilder2.Append("Sec-WebSocket-Version: 13\r\n");
				stringBuilder2.Append("Sec-WebSocket-Key: ").Append(secKey).Append("\r\n");
				foreach (string text2 in options.RequestHeaders.AllKeys)
				{
					if (!string.Equals(text2, "Host", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder2.Append(text2).Append(": ").Append(options.RequestHeaders[text2])
							.Append("\r\n");
					}
				}
				if (options.RequestedSubProtocols.Count > 0)
				{
					stringBuilder2.Append("Sec-WebSocket-Protocol").Append(": ");
					stringBuilder2.Append(options.RequestedSubProtocols[0]);
					for (int j = 1; j < options.RequestedSubProtocols.Count; j++)
					{
						stringBuilder2.Append(", ").Append(options.RequestedSubProtocols[j]);
					}
					stringBuilder2.Append("\r\n");
				}
				if (options.Cookies != null)
				{
					string cookieHeader = options.Cookies.GetCookieHeader(uri);
					if (!string.IsNullOrWhiteSpace(cookieHeader))
					{
						stringBuilder2.Append("Cookie").Append(": ").Append(cookieHeader)
							.Append("\r\n");
					}
				}
				stringBuilder2.Append("\r\n");
				bytes = WebSocketHandle.s_defaultHttpEncoding.GetBytes(stringBuilder2.ToString());
			}
			finally
			{
				stringBuilder2.Clear();
			}
			return bytes;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000AEC48 File Offset: 0x000ACE48
		private static KeyValuePair<string, string> CreateSecKeyAndSecWebSocketAccept()
		{
			string text = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			KeyValuePair<string, string> keyValuePair;
			using (SHA1 sha = SHA1.Create())
			{
				keyValuePair = new KeyValuePair<string, string>(text, Convert.ToBase64String(sha.ComputeHash(Encoding.ASCII.GetBytes(text + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))));
			}
			return keyValuePair;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000AECB4 File Offset: 0x000ACEB4
		private async Task<string> ParseAndValidateConnectResponseAsync(Stream stream, ClientWebSocketOptions options, string expectedSecWebSocketAccept, CancellationToken cancellationToken)
		{
			string text = await WebSocketHandle.ReadResponseHeaderLineAsync(stream, cancellationToken).ConfigureAwait(false);
			if (string.IsNullOrEmpty(text))
			{
				throw new WebSocketException(SR.Format("Unable to connect to the remote server", Array.Empty<object>()));
			}
			if (!text.StartsWith("HTTP/1.1 ", StringComparison.Ordinal) || text.Length < "HTTP/1.1 101".Length)
			{
				throw new WebSocketException(WebSocketError.HeaderError);
			}
			if (!text.StartsWith("HTTP/1.1 101", StringComparison.Ordinal) || (text.Length > "HTTP/1.1 101".Length && !char.IsWhiteSpace(text["HTTP/1.1 101".Length])))
			{
				throw new WebSocketException("Unable to connect to the remote server");
			}
			bool foundUpgrade = false;
			bool foundConnection = false;
			bool foundSecWebSocketAccept = false;
			string subprotocol = null;
			string text2;
			while (!string.IsNullOrEmpty(text2 = await WebSocketHandle.ReadResponseHeaderLineAsync(stream, cancellationToken).ConfigureAwait(false)))
			{
				int num = text2.IndexOf(':');
				if (num == -1)
				{
					throw new WebSocketException(WebSocketError.HeaderError);
				}
				string text3 = text2.SubstringTrim(0, num);
				string headerValue = text2.SubstringTrim(num + 1);
				WebSocketHandle.ValidateAndTrackHeader("Connection", "Upgrade", text3, headerValue, ref foundConnection);
				WebSocketHandle.ValidateAndTrackHeader("Upgrade", "websocket", text3, headerValue, ref foundUpgrade);
				WebSocketHandle.ValidateAndTrackHeader("Sec-WebSocket-Accept", expectedSecWebSocketAccept, text3, headerValue, ref foundSecWebSocketAccept);
				if (string.Equals("Sec-WebSocket-Protocol", text3, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(headerValue))
				{
					string text4 = options.RequestedSubProtocols.Find((string requested) => string.Equals(requested, headerValue, StringComparison.OrdinalIgnoreCase));
					if (text4 == null || subprotocol != null)
					{
						throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.Format("The WebSocket client request requested '{0}' protocol(s), but server is only accepting '{1}' protocol(s).", string.Join(", ", options.RequestedSubProtocols), subprotocol));
					}
					subprotocol = text4;
				}
			}
			if (!foundUpgrade || !foundConnection || !foundSecWebSocketAccept)
			{
				throw new WebSocketException("Unable to connect to the remote server");
			}
			return subprotocol;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000AED10 File Offset: 0x000ACF10
		private static void ValidateAndTrackHeader(string targetHeaderName, string targetHeaderValue, string foundHeaderName, string foundHeaderValue, ref bool foundHeader)
		{
			bool flag = string.Equals(targetHeaderName, foundHeaderName, StringComparison.OrdinalIgnoreCase);
			if (!foundHeader)
			{
				if (flag)
				{
					if (!string.Equals(targetHeaderValue, foundHeaderValue, StringComparison.OrdinalIgnoreCase))
					{
						throw new WebSocketException(SR.Format("The '{0}' header value '{1}' is invalid.", targetHeaderName, foundHeaderValue));
					}
					foundHeader = true;
					return;
				}
			}
			else if (flag)
			{
				throw new WebSocketException(SR.Format("Unable to connect to the remote server", Array.Empty<object>()));
			}
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000AED68 File Offset: 0x000ACF68
		private static async Task<string> ReadResponseHeaderLineAsync(Stream stream, CancellationToken cancellationToken)
		{
			StringBuilder sb = WebSocketHandle.t_cachedStringBuilder;
			if (sb != null)
			{
				WebSocketHandle.t_cachedStringBuilder = null;
			}
			else
			{
				sb = new StringBuilder();
			}
			byte[] arr = new byte[1];
			char prevChar = '\0';
			string text;
			try
			{
				for (;;)
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = stream.ReadAsync(arr, 0, 1, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() != 1)
					{
						break;
					}
					char c = (char)arr[0];
					if (prevChar == '\r' && c == '\n')
					{
						break;
					}
					sb.Append(c);
					prevChar = c;
				}
				if (sb.Length > 0 && sb[sb.Length - 1] == '\r')
				{
					sb.Length--;
				}
				text = sb.ToString();
			}
			finally
			{
				sb.Clear();
				WebSocketHandle.t_cachedStringBuilder = sb;
			}
			return text;
		}

		// Token: 0x04001DAB RID: 7595
		[ThreadStatic]
		private static StringBuilder t_cachedStringBuilder;

		// Token: 0x04001DAC RID: 7596
		private static readonly Encoding s_defaultHttpEncoding = Encoding.GetEncoding(28591);

		// Token: 0x04001DAD RID: 7597
		private const int DefaultReceiveBufferSize = 4096;

		// Token: 0x04001DAE RID: 7598
		private const string WSServerGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x04001DAF RID: 7599
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x04001DB0 RID: 7600
		private WebSocketState _state = WebSocketState.Connecting;

		// Token: 0x04001DB1 RID: 7601
		private WebSocket _webSocket;
	}
}
