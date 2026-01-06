using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024D RID: 589
	internal class SNITCPHandle : SNIHandle
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00085DF8 File Offset: 0x00083FF8
		public override void Dispose()
		{
			lock (this)
			{
				if (this._sslOverTdsStream != null)
				{
					this._sslOverTdsStream.Dispose();
					this._sslOverTdsStream = null;
				}
				if (this._sslStream != null)
				{
					this._sslStream.Dispose();
					this._sslStream = null;
				}
				if (this._tcpStream != null)
				{
					this._tcpStream.Dispose();
					this._tcpStream = null;
				}
				this._stream = null;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00085E84 File Offset: 0x00084084
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00085E8C File Offset: 0x0008408C
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00085E94 File Offset: 0x00084094
		public SNITCPHandle(string serverName, int port, long timerExpire, object callbackObject, bool parallel)
		{
			this._callbackObject = callbackObject;
			this._targetServer = serverName;
			try
			{
				TimeSpan timeSpan = default(TimeSpan);
				bool flag = long.MaxValue == timerExpire;
				if (!flag)
				{
					timeSpan = DateTime.FromFileTime(timerExpire) - DateTime.Now;
					timeSpan = ((timeSpan.Ticks < 0L) ? TimeSpan.FromTicks(0L) : timeSpan);
				}
				if (parallel)
				{
					Task<IPAddress[]> hostAddressesAsync = Dns.GetHostAddressesAsync(serverName);
					hostAddressesAsync.Wait(timeSpan);
					IPAddress[] result = hostAddressesAsync.Result;
					if (result.Length > 64)
					{
						this.ReportTcpSNIError(0U, 47U, string.Empty);
						return;
					}
					Task<Socket> task = SNITCPHandle.ParallelConnectAsync(result, port);
					if (!(flag ? task.Wait(-1) : task.Wait(timeSpan)))
					{
						this.ReportTcpSNIError(0U, 40U, string.Empty);
						return;
					}
					this._socket = task.Result;
				}
				else
				{
					this._socket = SNITCPHandle.Connect(serverName, port, flag ? TimeSpan.FromMilliseconds(2147483647.0) : timeSpan);
				}
				if (this._socket == null || !this._socket.Connected)
				{
					if (this._socket != null)
					{
						this._socket.Dispose();
						this._socket = null;
					}
					this.ReportTcpSNIError(0U, 40U, string.Empty);
					return;
				}
				this._socket.NoDelay = true;
				this._tcpStream = new NetworkStream(this._socket, true);
				this._sslOverTdsStream = new SslOverTdsStream(this._tcpStream);
				this._sslStream = new SslStream(this._sslOverTdsStream, true, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
			}
			catch (SocketException ex)
			{
				this.ReportTcpSNIError(ex);
				return;
			}
			catch (Exception ex2)
			{
				this.ReportTcpSNIError(ex2);
				return;
			}
			this._stream = this._tcpStream;
			this._status = 0U;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000860A0 File Offset: 0x000842A0
		private static Socket Connect(string serverName, int port, TimeSpan timeout)
		{
			SNITCPHandle.<>c__DisplayClass20_0 CS$<>8__locals1 = new SNITCPHandle.<>c__DisplayClass20_0();
			IPAddress[] array = Dns.GetHostAddresses(serverName);
			IPAddress ipaddress = null;
			IPAddress ipaddress2 = null;
			foreach (IPAddress ipaddress3 in array)
			{
				if (ipaddress3.AddressFamily == AddressFamily.InterNetwork)
				{
					ipaddress = ipaddress3;
				}
				else if (ipaddress3.AddressFamily == AddressFamily.InterNetworkV6)
				{
					ipaddress2 = ipaddress3;
				}
			}
			array = new IPAddress[] { ipaddress, ipaddress2 };
			CS$<>8__locals1.sockets = new Socket[2];
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.CancelAfter(timeout);
			cancellationTokenSource.Token.Register(new Action(CS$<>8__locals1.<Connect>g__Cancel|0));
			Socket socket = null;
			for (int j = 0; j < CS$<>8__locals1.sockets.Length; j++)
			{
				try
				{
					if (array[j] != null)
					{
						CS$<>8__locals1.sockets[j] = new Socket(array[j].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
						CS$<>8__locals1.sockets[j].Connect(array[j], port);
						if (CS$<>8__locals1.sockets[j] != null)
						{
							if (CS$<>8__locals1.sockets[j].Connected)
							{
								socket = CS$<>8__locals1.sockets[j];
								break;
							}
							CS$<>8__locals1.sockets[j].Dispose();
							CS$<>8__locals1.sockets[j] = null;
						}
					}
				}
				catch
				{
				}
			}
			return socket;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000861E4 File Offset: 0x000843E4
		private static Task<Socket> ParallelConnectAsync(IPAddress[] serverAddresses, int port)
		{
			if (serverAddresses == null)
			{
				throw new ArgumentNullException("serverAddresses");
			}
			if (serverAddresses.Length == 0)
			{
				throw new ArgumentOutOfRangeException("serverAddresses");
			}
			List<Socket> list = new List<Socket>(serverAddresses.Length);
			List<Task> list2 = new List<Task>(serverAddresses.Length);
			TaskCompletionSource<Socket> taskCompletionSource = new TaskCompletionSource<Socket>();
			StrongBox<Exception> strongBox = new StrongBox<Exception>();
			StrongBox<int> strongBox2 = new StrongBox<int>(serverAddresses.Length);
			foreach (IPAddress ipaddress in serverAddresses)
			{
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				list.Add(socket);
				try
				{
					list2.Add(socket.ConnectAsync(ipaddress, port));
				}
				catch (Exception ex)
				{
					list2.Add(Task.FromException(ex));
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				SNITCPHandle.ParallelConnectHelper(list[j], list2[j], taskCompletionSource, strongBox2, strongBox, list);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000862D4 File Offset: 0x000844D4
		private static async void ParallelConnectHelper(Socket socket, Task connectTask, TaskCompletionSource<Socket> tcs, StrongBox<int> pendingCompleteCount, StrongBox<Exception> lastError, List<Socket> sockets)
		{
			bool success = false;
			try
			{
				await connectTask.ConfigureAwait(false);
				success = tcs.TrySetResult(socket);
				if (success)
				{
					foreach (Socket socket2 in sockets)
					{
						if (socket2 != socket)
						{
							socket2.Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Interlocked.Exchange<Exception>(ref lastError.Value, ex);
			}
			finally
			{
				if (!success && Interlocked.Decrement(ref pendingCompleteCount.Value) == 0)
				{
					if (lastError.Value != null)
					{
						tcs.TrySetException(lastError.Value);
					}
					else
					{
						tcs.TrySetCanceled();
					}
					List<Socket>.Enumerator enumerator = sockets.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Socket socket3 = enumerator.Current;
							socket3.Dispose();
						}
					}
					finally
					{
						int num;
						if (num < 0)
						{
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00086338 File Offset: 0x00084538
		public override uint EnableSsl(uint options)
		{
			this._validateCert = (options & 1U) > 0U;
			try
			{
				this._sslStream.AuthenticateAsClient(this._targetServer);
				this._sslOverTdsStream.FinishHandshake();
			}
			catch (AuthenticationException ex)
			{
				return this.ReportTcpSNIError(ex);
			}
			catch (InvalidOperationException ex2)
			{
				return this.ReportTcpSNIError(ex2);
			}
			this._stream = this._sslStream;
			return 0U;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000863B0 File Offset: 0x000845B0
		public override void DisableSsl()
		{
			this._sslStream.Dispose();
			this._sslStream = null;
			this._sslOverTdsStream.Dispose();
			this._sslOverTdsStream = null;
			this._stream = this._tcpStream;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000863E2 File Offset: 0x000845E2
		private bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return !this._validateCert || SNICommon.ValidateSslServerCertificate(this._targetServer, sender, cert, chain, policyErrors);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000863FE File Offset: 0x000845FE
		public override void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00086408 File Offset: 0x00084608
		public override uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				try
				{
					packet.WriteToStream(this._stream);
					num = 0U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportTcpSNIError(ex);
				}
				catch (SocketException ex2)
				{
					num = this.ReportTcpSNIError(ex2);
				}
				catch (IOException ex3)
				{
					num = this.ReportTcpSNIError(ex3);
				}
			}
			return num;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00086498 File Offset: 0x00084698
		public override uint Receive(out SNIPacket packet, int timeoutInMilliseconds)
		{
			uint num;
			lock (this)
			{
				packet = null;
				try
				{
					if (timeoutInMilliseconds > 0)
					{
						this._socket.ReceiveTimeout = timeoutInMilliseconds;
					}
					else
					{
						if (timeoutInMilliseconds != -1)
						{
							this.ReportTcpSNIError(0U, 11U, string.Empty);
							return 258U;
						}
						this._socket.ReceiveTimeout = 0;
					}
					packet = new SNIPacket(this._bufferSize);
					packet.ReadFromStream(this._stream);
					if (packet.Length == 0)
					{
						Win32Exception ex = new Win32Exception();
						num = this.ReportErrorAndReleasePacket(packet, (uint)ex.NativeErrorCode, 0U, ex.Message);
					}
					else
					{
						num = 0U;
					}
				}
				catch (ObjectDisposedException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
				catch (SocketException ex3)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex3);
				}
				catch (IOException ex4)
				{
					uint num2 = this.ReportErrorAndReleasePacket(packet, ex4);
					if (ex4.InnerException is SocketException && ((SocketException)ex4.InnerException).SocketErrorCode == SocketError.TimedOut)
					{
						num2 = 258U;
					}
					num = num2;
				}
				finally
				{
					this._socket.ReceiveTimeout = 0;
				}
			}
			return num;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000865F0 File Offset: 0x000847F0
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
			this._receiveCallback = receiveCallback;
			this._sendCallback = sendCallback;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00086600 File Offset: 0x00084800
		public override uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null)
		{
			SNIAsyncCallback sniasyncCallback = callback ?? this._sendCallback;
			lock (this)
			{
				packet.WriteToStreamAsync(this._stream, sniasyncCallback, SNIProviders.TCP_PROV, disposePacketAfterSendAsync);
			}
			return 997U;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00086658 File Offset: 0x00084858
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			packet = new SNIPacket(this._bufferSize);
			uint num;
			try
			{
				packet.ReadFromStreamAsync(this._stream, this._receiveCallback);
				num = 997U;
			}
			catch (Exception ex) when (ex is ObjectDisposedException || ex is SocketException || ex is IOException)
			{
				num = this.ReportErrorAndReleasePacket(packet, ex);
			}
			return num;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000866DC File Offset: 0x000848DC
		public override uint CheckConnection()
		{
			try
			{
				if (!this._socket.Connected || this._socket.Poll(0, SelectMode.SelectError))
				{
					return 1U;
				}
			}
			catch (SocketException ex)
			{
				return this.ReportTcpSNIError(ex);
			}
			catch (ObjectDisposedException ex2)
			{
				return this.ReportTcpSNIError(ex2);
			}
			return 0U;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00086740 File Offset: 0x00084940
		private uint ReportTcpSNIError(Exception sniException)
		{
			this._status = 1U;
			return SNICommon.ReportSNIError(SNIProviders.TCP_PROV, 35U, sniException);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00086752 File Offset: 0x00084952
		private uint ReportTcpSNIError(uint nativeError, uint sniError, string errorMessage)
		{
			this._status = 1U;
			return SNICommon.ReportSNIError(SNIProviders.TCP_PROV, nativeError, sniError, errorMessage);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00086764 File Offset: 0x00084964
		private uint ReportErrorAndReleasePacket(SNIPacket packet, Exception sniException)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return this.ReportTcpSNIError(sniException);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00086776 File Offset: 0x00084976
		private uint ReportErrorAndReleasePacket(SNIPacket packet, uint nativeError, uint sniError, string errorMessage)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return this.ReportTcpSNIError(nativeError, sniError, errorMessage);
		}

		// Token: 0x04001354 RID: 4948
		private readonly string _targetServer;

		// Token: 0x04001355 RID: 4949
		private readonly object _callbackObject;

		// Token: 0x04001356 RID: 4950
		private readonly Socket _socket;

		// Token: 0x04001357 RID: 4951
		private NetworkStream _tcpStream;

		// Token: 0x04001358 RID: 4952
		private Stream _stream;

		// Token: 0x04001359 RID: 4953
		private SslStream _sslStream;

		// Token: 0x0400135A RID: 4954
		private SslOverTdsStream _sslOverTdsStream;

		// Token: 0x0400135B RID: 4955
		private SNIAsyncCallback _receiveCallback;

		// Token: 0x0400135C RID: 4956
		private SNIAsyncCallback _sendCallback;

		// Token: 0x0400135D RID: 4957
		private bool _validateCert = true;

		// Token: 0x0400135E RID: 4958
		private int _bufferSize = 4096;

		// Token: 0x0400135F RID: 4959
		private uint _status = uint.MaxValue;

		// Token: 0x04001360 RID: 4960
		private Guid _connectionId = Guid.NewGuid();

		// Token: 0x04001361 RID: 4961
		private const int MaxParallelIpAddresses = 64;
	}
}
