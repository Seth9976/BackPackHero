using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000496 RID: 1174
	internal sealed class HttpConnection
	{
		// Token: 0x06002505 RID: 9477 RVA: 0x00088F7C File Offset: 0x0008717C
		public HttpConnection(Socket sock, EndPointListener epl, bool secure, X509Certificate cert)
		{
			this.sock = sock;
			this.epl = epl;
			this.secure = secure;
			this.cert = cert;
			if (!secure)
			{
				this.stream = new NetworkStream(sock, false);
			}
			else
			{
				this.ssl_stream = epl.Listener.CreateSslStream(new NetworkStream(sock, false), false, delegate(object t, X509Certificate c, X509Chain ch, SslPolicyErrors e)
				{
					if (c == null)
					{
						return true;
					}
					X509Certificate2 x509Certificate = c as X509Certificate2;
					if (x509Certificate == null)
					{
						x509Certificate = new X509Certificate2(c.GetRawCertData());
					}
					this.client_cert = x509Certificate;
					this.client_cert_errors = new int[] { (int)e };
					return true;
				});
				this.stream = this.ssl_stream;
			}
			this.timer = new Timer(new TimerCallback(this.OnTimeout), null, -1, -1);
			if (this.ssl_stream != null)
			{
				this.ssl_stream.AuthenticateAsServer(cert, true, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			}
			this.Init();
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x00089036 File Offset: 0x00087236
		internal SslStream SslStream
		{
			get
			{
				return this.ssl_stream;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x0008903E File Offset: 0x0008723E
		internal int[] ClientCertificateErrors
		{
			get
			{
				return this.client_cert_errors;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x00089046 File Offset: 0x00087246
		internal X509Certificate2 ClientCertificate
		{
			get
			{
				return this.client_cert;
			}
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00089050 File Offset: 0x00087250
		private void Init()
		{
			this.context_bound = false;
			this.i_stream = null;
			this.o_stream = null;
			this.prefix = null;
			this.chunked = false;
			this.ms = new MemoryStream();
			this.position = 0;
			this.input_state = HttpConnection.InputState.RequestLine;
			this.line_state = HttpConnection.LineState.None;
			this.context = new HttpListenerContext(this);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000890AC File Offset: 0x000872AC
		public bool IsClosed
		{
			get
			{
				return this.sock == null;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000890B7 File Offset: 0x000872B7
		public int Reuses
		{
			get
			{
				return this.reuses;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000890BF File Offset: 0x000872BF
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.local_ep != null)
				{
					return this.local_ep;
				}
				this.local_ep = (IPEndPoint)this.sock.LocalEndPoint;
				return this.local_ep;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000890EC File Offset: 0x000872EC
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return (IPEndPoint)this.sock.RemoteEndPoint;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x000890FE File Offset: 0x000872FE
		public bool IsSecure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x00089106 File Offset: 0x00087306
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x0008910E File Offset: 0x0008730E
		public ListenerPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00089117 File Offset: 0x00087317
		private void OnTimeout(object unused)
		{
			this.CloseSocket();
			this.Unbind();
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00089128 File Offset: 0x00087328
		public void BeginReadRequest()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[8192];
			}
			try
			{
				if (this.reuses == 1)
				{
					this.s_timeout = 15000;
				}
				this.timer.Change(this.s_timeout, -1);
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
			catch
			{
				this.timer.Change(-1, -1);
				this.CloseSocket();
				this.Unbind();
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000891C4 File Offset: 0x000873C4
		public RequestStream GetRequestStream(bool chunked, long contentlength)
		{
			if (this.i_stream == null)
			{
				byte[] array = this.ms.GetBuffer();
				int num = (int)this.ms.Length;
				this.ms = null;
				if (chunked)
				{
					this.chunked = true;
					this.context.Response.SendChunked = true;
					this.i_stream = new ChunkedInputStream(this.context, this.stream, array, this.position, num - this.position);
				}
				else
				{
					this.i_stream = new RequestStream(this.stream, array, this.position, num - this.position, contentlength);
				}
			}
			return this.i_stream;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00089268 File Offset: 0x00087468
		public ResponseStream GetResponseStream()
		{
			if (this.o_stream == null)
			{
				HttpListener listener = this.context.Listener;
				if (listener == null)
				{
					return new ResponseStream(this.stream, this.context.Response, true);
				}
				this.o_stream = new ResponseStream(this.stream, this.context.Response, listener.IgnoreWriteExceptions);
			}
			return this.o_stream;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000892CC File Offset: 0x000874CC
		private static void OnRead(IAsyncResult ares)
		{
			((HttpConnection)ares.AsyncState).OnReadInternal(ares);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000892E0 File Offset: 0x000874E0
		private void OnReadInternal(IAsyncResult ares)
		{
			this.timer.Change(-1, -1);
			int num = -1;
			try
			{
				num = this.stream.EndRead(ares);
				this.ms.Write(this.buffer, 0, num);
				if (this.ms.Length > 32768L)
				{
					this.SendError("Bad request", 400);
					this.Close(true);
					return;
				}
			}
			catch
			{
				if (this.ms != null && this.ms.Length > 0L)
				{
					this.SendError();
				}
				if (this.sock != null)
				{
					this.CloseSocket();
					this.Unbind();
				}
				return;
			}
			if (num == 0)
			{
				this.CloseSocket();
				this.Unbind();
				return;
			}
			if (this.ProcessInput(this.ms))
			{
				if (!this.context.HaveError && !this.context.Request.FinishInitialization())
				{
					this.Close(true);
					return;
				}
				if (this.context.HaveError)
				{
					this.SendError();
					this.Close(true);
					return;
				}
				if (!this.epl.BindContext(this.context))
				{
					this.SendError("Invalid host", 400);
					this.Close(true);
					return;
				}
				HttpListener listener = this.context.Listener;
				if (this.last_listener != listener)
				{
					this.RemoveConnection();
					listener.AddConnection(this);
					this.last_listener = listener;
				}
				this.context_bound = true;
				listener.RegisterContext(this.context);
				return;
			}
			else
			{
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00089480 File Offset: 0x00087680
		private void RemoveConnection()
		{
			if (this.last_listener == null)
			{
				this.epl.RemoveConnection(this);
				return;
			}
			this.last_listener.RemoveConnection(this);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000894A4 File Offset: 0x000876A4
		private bool ProcessInput(MemoryStream ms)
		{
			byte[] array = ms.GetBuffer();
			int num = (int)ms.Length;
			int num2 = 0;
			while (!this.context.HaveError)
			{
				if (this.position < num)
				{
					string text;
					try
					{
						text = this.ReadLine(array, this.position, num - this.position, ref num2);
						this.position += num2;
					}
					catch
					{
						this.context.ErrorMessage = "Bad request";
						this.context.ErrorStatus = 400;
						return true;
					}
					if (text == null)
					{
						goto IL_010D;
					}
					if (text == "")
					{
						if (this.input_state != HttpConnection.InputState.RequestLine)
						{
							this.current_line = null;
							ms = null;
							return true;
						}
						continue;
					}
					else
					{
						if (this.input_state == HttpConnection.InputState.RequestLine)
						{
							this.context.Request.SetRequestLine(text);
							this.input_state = HttpConnection.InputState.Headers;
							continue;
						}
						try
						{
							this.context.Request.AddHeader(text);
							continue;
						}
						catch (Exception ex)
						{
							this.context.ErrorMessage = ex.Message;
							this.context.ErrorStatus = 400;
							return true;
						}
						goto IL_010D;
					}
					bool flag;
					return flag;
				}
				IL_010D:
				if (num2 == num)
				{
					ms.SetLength(0L);
					this.position = 0;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000895F4 File Offset: 0x000877F4
		private string ReadLine(byte[] buffer, int offset, int len, ref int used)
		{
			if (this.current_line == null)
			{
				this.current_line = new StringBuilder(128);
			}
			int num = offset + len;
			used = 0;
			int num2 = offset;
			while (num2 < num && this.line_state != HttpConnection.LineState.LF)
			{
				used++;
				byte b = buffer[num2];
				if (b == 13)
				{
					this.line_state = HttpConnection.LineState.CR;
				}
				else if (b == 10)
				{
					this.line_state = HttpConnection.LineState.LF;
				}
				else
				{
					this.current_line.Append((char)b);
				}
				num2++;
			}
			string text = null;
			if (this.line_state == HttpConnection.LineState.LF)
			{
				this.line_state = HttpConnection.LineState.None;
				text = this.current_line.ToString();
				this.current_line.Length = 0;
			}
			return text;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00089698 File Offset: 0x00087898
		public void SendError(string msg, int status)
		{
			try
			{
				HttpListenerResponse response = this.context.Response;
				response.StatusCode = status;
				response.ContentType = "text/html";
				string text = HttpStatusDescription.Get(status);
				string text2;
				if (msg != null)
				{
					text2 = string.Format("<h1>{0} ({1})</h1>", text, msg);
				}
				else
				{
					text2 = string.Format("<h1>{0}</h1>", text);
				}
				byte[] bytes = this.context.Response.ContentEncoding.GetBytes(text2);
				response.Close(bytes, false);
			}
			catch
			{
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0008971C File Offset: 0x0008791C
		public void SendError()
		{
			this.SendError(this.context.ErrorMessage, this.context.ErrorStatus);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0008973A File Offset: 0x0008793A
		private void Unbind()
		{
			if (this.context_bound)
			{
				this.epl.UnbindContext(this.context);
				this.context_bound = false;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0008975C File Offset: 0x0008795C
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00089768 File Offset: 0x00087968
		private void CloseSocket()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			finally
			{
				this.sock = null;
			}
			this.RemoveConnection();
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000897BC File Offset: 0x000879BC
		internal void Close(bool force_close)
		{
			if (this.sock != null)
			{
				Stream responseStream = this.GetResponseStream();
				if (responseStream != null)
				{
					responseStream.Close();
				}
				this.o_stream = null;
			}
			if (this.sock == null)
			{
				return;
			}
			force_close |= !this.context.Request.KeepAlive;
			if (!force_close)
			{
				force_close = this.context.Response.Headers["connection"] == "close";
			}
			if (force_close || !this.context.Request.FlushInput())
			{
				Socket socket = this.sock;
				this.sock = null;
				try
				{
					if (socket != null)
					{
						socket.Shutdown(SocketShutdown.Both);
					}
				}
				catch
				{
				}
				finally
				{
					if (socket != null)
					{
						socket.Close();
					}
				}
				this.Unbind();
				this.RemoveConnection();
				return;
			}
			if (this.chunked && !this.context.Response.ForceCloseChunked)
			{
				this.reuses++;
				this.Unbind();
				this.Init();
				this.BeginReadRequest();
				return;
			}
			this.reuses++;
			this.Unbind();
			this.Init();
			this.BeginReadRequest();
		}

		// Token: 0x04001579 RID: 5497
		private static AsyncCallback onread_cb = new AsyncCallback(HttpConnection.OnRead);

		// Token: 0x0400157A RID: 5498
		private const int BufferSize = 8192;

		// Token: 0x0400157B RID: 5499
		private Socket sock;

		// Token: 0x0400157C RID: 5500
		private Stream stream;

		// Token: 0x0400157D RID: 5501
		private EndPointListener epl;

		// Token: 0x0400157E RID: 5502
		private MemoryStream ms;

		// Token: 0x0400157F RID: 5503
		private byte[] buffer;

		// Token: 0x04001580 RID: 5504
		private HttpListenerContext context;

		// Token: 0x04001581 RID: 5505
		private StringBuilder current_line;

		// Token: 0x04001582 RID: 5506
		private ListenerPrefix prefix;

		// Token: 0x04001583 RID: 5507
		private RequestStream i_stream;

		// Token: 0x04001584 RID: 5508
		private ResponseStream o_stream;

		// Token: 0x04001585 RID: 5509
		private bool chunked;

		// Token: 0x04001586 RID: 5510
		private int reuses;

		// Token: 0x04001587 RID: 5511
		private bool context_bound;

		// Token: 0x04001588 RID: 5512
		private bool secure;

		// Token: 0x04001589 RID: 5513
		private X509Certificate cert;

		// Token: 0x0400158A RID: 5514
		private int s_timeout = 90000;

		// Token: 0x0400158B RID: 5515
		private Timer timer;

		// Token: 0x0400158C RID: 5516
		private IPEndPoint local_ep;

		// Token: 0x0400158D RID: 5517
		private HttpListener last_listener;

		// Token: 0x0400158E RID: 5518
		private int[] client_cert_errors;

		// Token: 0x0400158F RID: 5519
		private X509Certificate2 client_cert;

		// Token: 0x04001590 RID: 5520
		private SslStream ssl_stream;

		// Token: 0x04001591 RID: 5521
		private HttpConnection.InputState input_state;

		// Token: 0x04001592 RID: 5522
		private HttpConnection.LineState line_state;

		// Token: 0x04001593 RID: 5523
		private int position;

		// Token: 0x02000497 RID: 1175
		private enum InputState
		{
			// Token: 0x04001595 RID: 5525
			RequestLine,
			// Token: 0x04001596 RID: 5526
			Headers
		}

		// Token: 0x02000498 RID: 1176
		private enum LineState
		{
			// Token: 0x04001598 RID: 5528
			None,
			// Token: 0x04001599 RID: 5529
			CR,
			// Token: 0x0400159A RID: 5530
			LF
		}
	}
}
