using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000003 RID: 3
	internal static class ConnectHelper
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020A0 File Offset: 0x000002A0
		public static async ValueTask<ValueTuple<Socket, Stream>> ConnectAsync(string host, int port, CancellationToken cancellationToken)
		{
			ConnectHelper.ConnectEventArgs saea;
			if (!ConnectHelper.s_connectEventArgs.TryDequeue(out saea))
			{
				saea = new ConnectHelper.ConnectEventArgs();
			}
			ValueTuple<Socket, Stream> valueTuple;
			try
			{
				saea.Initialize(cancellationToken);
				saea.RemoteEndPoint = new DnsEndPoint(host, port);
				if (Socket.ConnectAsync(SocketType.Stream, ProtocolType.Tcp, saea))
				{
					using (cancellationToken.Register(delegate(object s)
					{
						Socket.CancelConnectAsync((SocketAsyncEventArgs)s);
					}, saea))
					{
						await saea.Builder.Task.ConfigureAwait(false);
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
				}
				else if (saea.SocketError != SocketError.Success)
				{
					throw new SocketException((int)saea.SocketError);
				}
				Socket connectSocket = saea.ConnectSocket;
				connectSocket.NoDelay = true;
				valueTuple = new ValueTuple<Socket, Stream>(connectSocket, new NetworkStream(connectSocket, true));
			}
			catch (Exception ex)
			{
				throw CancellationHelper.ShouldWrapInOperationCanceledException(ex, cancellationToken) ? CancellationHelper.CreateOperationCanceledException(ex, cancellationToken) : new HttpRequestException(ex.Message, ex);
			}
			finally
			{
				saea.Clear();
				if (!ConnectHelper.s_connectEventArgs.TryEnqueue(saea))
				{
					saea.Dispose();
				}
			}
			return valueTuple;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public static ValueTask<SslStream> EstablishSslConnectionAsync(SslClientAuthenticationOptions sslOptions, HttpRequestMessage request, Stream stream, CancellationToken cancellationToken)
		{
			RemoteCertificateValidationCallback remoteCertificateValidationCallback = sslOptions.RemoteCertificateValidationCallback;
			if (remoteCertificateValidationCallback != null)
			{
				ConnectHelper.CertificateCallbackMapper certificateCallbackMapper = remoteCertificateValidationCallback.Target as ConnectHelper.CertificateCallbackMapper;
				if (certificateCallbackMapper != null)
				{
					sslOptions = sslOptions.ShallowClone();
					Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> localFromHttpClientHandler = certificateCallbackMapper.FromHttpClientHandler;
					HttpRequestMessage localRequest = request;
					sslOptions.RemoteCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => localFromHttpClientHandler(localRequest, certificate as X509Certificate2, chain, sslPolicyErrors);
				}
			}
			return ConnectHelper.EstablishSslConnectionAsyncCore(stream, sslOptions, cancellationToken);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		private static ValueTask<SslStream> EstablishSslConnectionAsyncCore(Stream stream, SslClientAuthenticationOptions sslOptions, CancellationToken cancellationToken)
		{
			ConnectHelper.<EstablishSslConnectionAsyncCore>d__5 <EstablishSslConnectionAsyncCore>d__;
			<EstablishSslConnectionAsyncCore>d__.stream = stream;
			<EstablishSslConnectionAsyncCore>d__.sslOptions = sslOptions;
			<EstablishSslConnectionAsyncCore>d__.cancellationToken = cancellationToken;
			<EstablishSslConnectionAsyncCore>d__.<>t__builder = AsyncValueTaskMethodBuilder<SslStream>.Create();
			<EstablishSslConnectionAsyncCore>d__.<>1__state = -1;
			<EstablishSslConnectionAsyncCore>d__.<>t__builder.Start<ConnectHelper.<EstablishSslConnectionAsyncCore>d__5>(ref <EstablishSslConnectionAsyncCore>d__);
			return <EstablishSslConnectionAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x04000002 RID: 2
		private static readonly ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment s_connectEventArgs = new ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment(ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment.RoundUpToPowerOf2(Math.Max(2, Environment.ProcessorCount)));

		// Token: 0x02000004 RID: 4
		internal sealed class CertificateCallbackMapper
		{
			// Token: 0x0600000A RID: 10 RVA: 0x000021C7 File Offset: 0x000003C7
			public CertificateCallbackMapper(Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> fromHttpClientHandler)
			{
				this.FromHttpClientHandler = fromHttpClientHandler;
				this.ForSocketsHttpHandler = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => this.FromHttpClientHandler(sender as HttpRequestMessage, certificate as X509Certificate2, chain, sslPolicyErrors);
			}

			// Token: 0x04000003 RID: 3
			public readonly Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> FromHttpClientHandler;

			// Token: 0x04000004 RID: 4
			public readonly RemoteCertificateValidationCallback ForSocketsHttpHandler;
		}

		// Token: 0x02000005 RID: 5
		private sealed class ConnectEventArgs : SocketAsyncEventArgs
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000C RID: 12 RVA: 0x00002204 File Offset: 0x00000404
			// (set) Token: 0x0600000D RID: 13 RVA: 0x0000220C File Offset: 0x0000040C
			public AsyncTaskMethodBuilder Builder { get; private set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000E RID: 14 RVA: 0x00002215 File Offset: 0x00000415
			// (set) Token: 0x0600000F RID: 15 RVA: 0x0000221D File Offset: 0x0000041D
			public CancellationToken CancellationToken { get; private set; }

			// Token: 0x06000010 RID: 16 RVA: 0x00002228 File Offset: 0x00000428
			public void Initialize(CancellationToken cancellationToken)
			{
				this.CancellationToken = cancellationToken;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
				Task task = asyncTaskMethodBuilder.Task;
				this.Builder = asyncTaskMethodBuilder;
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002254 File Offset: 0x00000454
			public void Clear()
			{
				this.CancellationToken = default(CancellationToken);
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002270 File Offset: 0x00000470
			protected override void OnCompleted(SocketAsyncEventArgs _)
			{
				SocketError socketError = base.SocketError;
				if (socketError != SocketError.Success)
				{
					if (socketError == SocketError.OperationAborted || socketError == SocketError.ConnectionAborted)
					{
						if (this.CancellationToken.IsCancellationRequested)
						{
							this.Builder.SetException(CancellationHelper.CreateOperationCanceledException(null, this.CancellationToken));
							return;
						}
					}
					this.Builder.SetException(new SocketException((int)base.SocketError));
					return;
				}
				this.Builder.SetResult();
			}
		}
	}
}
