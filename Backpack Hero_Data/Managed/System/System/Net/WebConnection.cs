using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;

namespace System.Net
{
	// Token: 0x020004CF RID: 1231
	internal class WebConnection : IDisposable
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x00093CEF File Offset: 0x00091EEF
		public ServicePoint ServicePoint { get; }

		// Token: 0x060027E3 RID: 10211 RVA: 0x00093CF7 File Offset: 0x00091EF7
		public WebConnection(ServicePoint sPoint)
		{
			this.ServicePoint = sPoint;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message)
		{
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x00093D06 File Offset: 0x00091F06
		private bool CanReuse()
		{
			return !this.socket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x00093D18 File Offset: 0x00091F18
		private bool CheckReusable()
		{
			if (this.socket != null && this.socket.Connected)
			{
				try
				{
					if (this.CanReuse())
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x00093D60 File Offset: 0x00091F60
		private async Task Connect(WebOperation operation, CancellationToken cancellationToken)
		{
			IPHostEntry hostEntry = this.ServicePoint.HostEntry;
			if (hostEntry == null || hostEntry.AddressList.Length == 0)
			{
				throw WebConnection.GetException(this.ServicePoint.UsesProxy ? WebExceptionStatus.ProxyNameResolutionFailure : WebExceptionStatus.NameResolutionFailure, null);
			}
			Exception connectException = null;
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				operation.ThrowIfDisposed(cancellationToken);
				try
				{
					this.socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				}
				catch (Exception ex)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.ServicePoint.Address.Port);
				this.socket.NoDelay = !this.ServicePoint.UseNagleAlgorithm;
				try
				{
					this.ServicePoint.KeepAliveSetup(this.socket);
				}
				catch
				{
				}
				if (!this.ServicePoint.CallEndPointDelegate(this.socket, ipendPoint))
				{
					Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
					if (socket != null)
					{
						socket.Close();
					}
				}
				else
				{
					try
					{
						operation.ThrowIfDisposed(cancellationToken);
						await Task.Factory.FromAsync<IPEndPoint>((IPEndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
						{
							((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
						}, ipendPoint, this.socket).ConfigureAwait(false);
					}
					catch (ObjectDisposedException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						Socket socket2 = Interlocked.Exchange<Socket>(ref this.socket, null);
						if (socket2 != null)
						{
							socket2.Close();
						}
						connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex2);
						goto IL_0220;
					}
					if (this.socket != null)
					{
						return;
					}
				}
				IL_0220:;
			}
			IPAddress[] array = null;
			if (connectException == null)
			{
				connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, null);
			}
			throw connectException;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00093DB4 File Offset: 0x00091FB4
		private async Task<bool> CreateStream(WebOperation operation, bool reused, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				NetworkStream stream = new NetworkStream(this.socket, false);
				if (operation.Request.Address.Scheme == Uri.UriSchemeHttps)
				{
					if (!reused || this.monoTlsStream == null)
					{
						if (this.ServicePoint.UseConnect)
						{
							if (this.tunnel == null)
							{
								this.tunnel = new WebConnectionTunnel(operation.Request, this.ServicePoint.Address);
							}
							await this.tunnel.Initialize(stream, cancellationToken).ConfigureAwait(false);
							if (!this.tunnel.Success)
							{
								return false;
							}
						}
						this.monoTlsStream = new MonoTlsStream(operation.Request, stream);
						this.networkStream = await this.monoTlsStream.CreateStream(this.tunnel, cancellationToken).ConfigureAwait(false);
					}
					flag = true;
				}
				else
				{
					this.networkStream = stream;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				ex = HttpWebRequest.FlattenException(ex);
				if (operation.Aborted || this.monoTlsStream == null)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				throw WebConnection.GetException(this.monoTlsStream.ExceptionStatus, ex);
			}
			finally
			{
			}
			return flag;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00093E10 File Offset: 0x00092010
		internal async Task<WebRequestStream> InitConnection(WebOperation operation, CancellationToken cancellationToken)
		{
			bool flag = true;
			for (;;)
			{
				operation.ThrowIfClosedOrDisposed(cancellationToken);
				bool reused = this.CheckReusable();
				if (!reused)
				{
					this.CloseSocket();
					if (flag)
					{
						this.Reset();
					}
					try
					{
						await this.Connect(operation, cancellationToken).ConfigureAwait(false);
					}
					catch (Exception)
					{
						throw;
					}
				}
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.CreateStream(operation, reused, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					goto IL_0180;
				}
				WebConnectionTunnel webConnectionTunnel = this.tunnel;
				if (((webConnectionTunnel != null) ? webConnectionTunnel.Challenge : null) == null)
				{
					break;
				}
				if (this.tunnel.CloseConnection)
				{
					this.CloseSocket();
				}
				flag = false;
			}
			throw WebConnection.GetException(WebExceptionStatus.ProtocolError, null);
			IL_0180:
			this.networkStream.ReadTimeout = operation.Request.ReadWriteTimeout;
			return new WebRequestStream(this, operation, this.networkStream, this.tunnel);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00093E64 File Offset: 0x00092064
		internal static WebException GetException(WebExceptionStatus status, Exception error)
		{
			if (error == null)
			{
				return new WebException(string.Format("Error: {0}", status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			return new WebException(string.Format("Error: {0} ({1})", status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00093EB8 File Offset: 0x000920B8
		internal static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				int num2 = start;
				start = num2 + 1;
				num = (int)buffer[num2];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						StringBuilder stringBuilder2 = stringBuilder;
						num2 = stringBuilder2.Length;
						stringBuilder2.Length = num2 - 1;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				StringBuilder stringBuilder4 = stringBuilder;
				int num2 = stringBuilder4.Length;
				stringBuilder4.Length = num2 - 1;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00093F7C File Offset: 0x0009217C
		internal bool CanReuseConnection(WebOperation operation)
		{
			bool flag2;
			lock (this)
			{
				if (this.Closed || this.currentOperation != null)
				{
					flag2 = false;
				}
				else if (!this.NtlmAuthenticated)
				{
					flag2 = true;
				}
				else
				{
					NetworkCredential ntlmCredential = this.NtlmCredential;
					HttpWebRequest request = operation.Request;
					ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
					NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
					if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
					{
						flag2 = false;
					}
					else
					{
						bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
						bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
						flag2 = unsafeAuthenticatedConnectionSharing && unsafeAuthenticatedConnectionSharing == unsafeAuthenticatedConnectionSharing2;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000940A4 File Offset: 0x000922A4
		private bool PrepareSharingNtlm(WebOperation operation)
		{
			if (operation == null || !this.NtlmAuthenticated)
			{
				return true;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = this.NtlmCredential;
			HttpWebRequest request = operation.Request;
			ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
			NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
			if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
				flag = !unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2;
			}
			return flag;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00094188 File Offset: 0x00092388
		private void Reset()
		{
			lock (this)
			{
				this.tunnel = null;
				this.ResetNtlm();
			}
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000941CC File Offset: 0x000923CC
		private void Close(bool reset)
		{
			lock (this)
			{
				this.CloseSocket();
				if (reset)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00094210 File Offset: 0x00092410
		private void CloseSocket()
		{
			lock (this)
			{
				if (this.networkStream != null)
				{
					try
					{
						this.networkStream.Dispose();
					}
					catch
					{
					}
					this.networkStream = null;
				}
				if (this.monoTlsStream != null)
				{
					try
					{
						this.monoTlsStream.Dispose();
					}
					catch
					{
					}
					this.monoTlsStream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Dispose();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.monoTlsStream = null;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000942D0 File Offset: 0x000924D0
		public bool Closed
		{
			get
			{
				return this.disposed != 0;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000942DB File Offset: 0x000924DB
		public bool Busy
		{
			get
			{
				return this.currentOperation != null;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x000942E6 File Offset: 0x000924E6
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000942F0 File Offset: 0x000924F0
		public bool StartOperation(WebOperation operation, bool reused)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, operation, null) != null)
				{
					return false;
				}
				this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
				if (reused && !this.PrepareSharingNtlm(operation))
				{
					this.Close(true);
				}
				operation.RegisterRequest(this.ServicePoint, this);
			}
			operation.Run();
			return true;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0009438C File Offset: 0x0009258C
		public bool Continue(WebOperation next)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (this.socket == null || !this.socket.Connected || !this.PrepareSharingNtlm(next))
				{
					this.Close(true);
					return false;
				}
				this.currentOperation = next;
				if (next == null)
				{
					return true;
				}
				next.RegisterRequest(this.ServicePoint, this);
			}
			next.Run();
			return true;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0009441C File Offset: 0x0009261C
		private void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) != 0)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00094435 File Offset: 0x00092635
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0009443E File Offset: 0x0009263E
		private void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x00094455 File Offset: 0x00092655
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x0009445D File Offset: 0x0009265D
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x00094466 File Offset: 0x00092666
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x0009446E File Offset: 0x0009266E
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00094477 File Offset: 0x00092677
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x0009447F File Offset: 0x0009267F
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x04001720 RID: 5920
		private NetworkCredential ntlm_credentials;

		// Token: 0x04001721 RID: 5921
		private bool ntlm_authenticated;

		// Token: 0x04001722 RID: 5922
		private bool unsafe_sharing;

		// Token: 0x04001723 RID: 5923
		private Stream networkStream;

		// Token: 0x04001724 RID: 5924
		private Socket socket;

		// Token: 0x04001725 RID: 5925
		private MonoTlsStream monoTlsStream;

		// Token: 0x04001726 RID: 5926
		private WebConnectionTunnel tunnel;

		// Token: 0x04001727 RID: 5927
		private int disposed;

		// Token: 0x04001729 RID: 5929
		internal readonly int ID;

		// Token: 0x0400172A RID: 5930
		private DateTime idleSince;

		// Token: 0x0400172B RID: 5931
		private WebOperation currentOperation;
	}
}
