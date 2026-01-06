using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x020005D4 RID: 1492
	public static class SocketTaskExtensions
	{
		// Token: 0x0600301B RID: 12315 RVA: 0x000AA62C File Offset: 0x000A882C
		public static Task<Socket> AcceptAsync(this Socket socket)
		{
			return Task<Socket>.Factory.FromAsync((AsyncCallback callback, object state) => ((Socket)state).BeginAccept(callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), socket);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000AA684 File Offset: 0x000A8884
		public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
		{
			return Task<Socket>.Factory.FromAsync<Socket, int>((Socket socketForAccept, int receiveSize, AsyncCallback callback, object state) => ((Socket)state).BeginAccept(socketForAccept, receiveSize, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), acceptSocket, 0, socket);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000AA6DC File Offset: 0x000A88DC
		public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
		{
			return Task.Factory.FromAsync<EndPoint>((EndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, remoteEP, socket);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000AA734 File Offset: 0x000A8934
		public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
		{
			return Task.Factory.FromAsync<IPAddress, int>((IPAddress targetAddress, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddress, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, address, port, socket);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000AA78C File Offset: 0x000A898C
		public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
		{
			return Task.Factory.FromAsync<IPAddress[], int>((IPAddress[] targetAddresses, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddresses, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, addresses, port, socket);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000AA7E4 File Offset: 0x000A89E4
		public static Task ConnectAsync(this Socket socket, string host, int port)
		{
			return Task.Factory.FromAsync<string, int>((string targetHost, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetHost, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, host, port, socket);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000AA83C File Offset: 0x000A8A3C
		public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffer, socketFlags, socket);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000AA894 File Offset: 0x000A8A94
		public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffers, socketFlags, socket);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000AA8EC File Offset: 0x000A8AEC
		public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] array = new object[] { socket, remoteEndPoint };
			return Task<SocketReceiveFromResult>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>(delegate(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				object[] array2 = (object[])state;
				Socket socket2 = (Socket)array2[0];
				EndPoint endPoint = (EndPoint)array2[1];
				IAsyncResult asyncResult2 = socket2.BeginReceiveFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, ref endPoint, callback, state);
				array2[1] = endPoint;
				return asyncResult2;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array3 = (object[])asyncResult.AsyncState;
				Socket socket3 = (Socket)array3[0];
				EndPoint endPoint2 = (EndPoint)array3[1];
				int num = socket3.EndReceiveFrom(asyncResult, ref endPoint2);
				return new SocketReceiveFromResult
				{
					ReceivedBytes = num,
					RemoteEndPoint = endPoint2
				};
			}, buffer, socketFlags, array);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000AA954 File Offset: 0x000A8B54
		public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] array = new object[] { socket, socketFlags, remoteEndPoint };
			return Task<SocketReceiveMessageFromResult>.Factory.FromAsync<ArraySegment<byte>>(delegate(ArraySegment<byte> targetBuffer, AsyncCallback callback, object state)
			{
				object[] array2 = (object[])state;
				Socket socket2 = (Socket)array2[0];
				SocketFlags socketFlags2 = (SocketFlags)array2[1];
				EndPoint endPoint = (EndPoint)array2[2];
				IAsyncResult asyncResult2 = socket2.BeginReceiveMessageFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, socketFlags2, ref endPoint, callback, state);
				array2[2] = endPoint;
				return asyncResult2;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array3 = (object[])asyncResult.AsyncState;
				Socket socket3 = (Socket)array3[0];
				SocketFlags socketFlags3 = (SocketFlags)array3[1];
				EndPoint endPoint2 = (EndPoint)array3[2];
				IPPacketInformation ippacketInformation;
				int num = socket3.EndReceiveMessageFrom(asyncResult, ref socketFlags3, ref endPoint2, out ippacketInformation);
				return new SocketReceiveMessageFromResult
				{
					PacketInformation = ippacketInformation,
					ReceivedBytes = num,
					RemoteEndPoint = endPoint2,
					SocketFlags = socketFlags3
				};
			}, buffer, array);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000AA9C4 File Offset: 0x000A8BC4
		public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffer, socketFlags, socket);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000AAA1C File Offset: 0x000A8C1C
		public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffers, socketFlags, socket);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000AAA74 File Offset: 0x000A8C74
		public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags, EndPoint>((ArraySegment<byte> targetBuffer, SocketFlags flags, EndPoint endPoint, AsyncCallback callback, object state) => ((Socket)state).BeginSendTo(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, endPoint, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSendTo(asyncResult), buffer, socketFlags, remoteEP, socket);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000AAACD File Offset: 0x000A8CCD
		public static ValueTask<int> SendAsync(this Socket socket, ReadOnlyMemory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken = default(CancellationToken))
		{
			return socket.SendAsync(buffer, socketFlags, cancellationToken);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000AAAD8 File Offset: 0x000A8CD8
		public static ValueTask<int> ReceiveAsync(this Socket socket, Memory<byte> memory, SocketFlags socketFlags, CancellationToken cancellationToken = default(CancellationToken))
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(socket);
			socket.BeginReceive(memory.ToArray(), 0, memory.Length, socketFlags, delegate(IAsyncResult iar)
			{
				cancellationToken.ThrowIfCancellationRequested();
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				Socket socket2 = (Socket)taskCompletionSource2.Task.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(socket2.EndReceive(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<int>(taskCompletionSource.Task);
		}
	}
}
