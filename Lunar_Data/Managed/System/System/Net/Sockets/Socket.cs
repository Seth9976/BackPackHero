using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Configuration;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Mono;

namespace System.Net.Sockets
{
	/// <summary>Implements the Berkeley sockets interface.</summary>
	// Token: 0x020005A0 RID: 1440
	public class Socket : IDisposable
	{
		// Token: 0x06002D8E RID: 11662 RVA: 0x000A1564 File Offset: 0x0009F764
		internal Task<Socket> AcceptAsync(Socket acceptSocket)
		{
			Socket.TaskSocketAsyncEventArgs<Socket> taskSocketAsyncEventArgs = Interlocked.Exchange<Socket.TaskSocketAsyncEventArgs<Socket>>(ref LazyInitializer.EnsureInitialized<Socket.CachedEventArgs>(ref this._cachedTaskEventArgs, () => new Socket.CachedEventArgs()).TaskAccept, Socket.s_rentedSocketSentinel);
			if (taskSocketAsyncEventArgs == Socket.s_rentedSocketSentinel)
			{
				return this.AcceptAsyncApm(acceptSocket);
			}
			if (taskSocketAsyncEventArgs == null)
			{
				taskSocketAsyncEventArgs = new Socket.TaskSocketAsyncEventArgs<Socket>();
				taskSocketAsyncEventArgs.Completed += Socket.AcceptCompletedHandler;
			}
			taskSocketAsyncEventArgs.AcceptSocket = acceptSocket;
			Task<Socket> task;
			if (this.AcceptAsync(taskSocketAsyncEventArgs))
			{
				bool flag;
				task = taskSocketAsyncEventArgs.GetCompletionResponsibility(out flag).Task;
				if (flag)
				{
					this.ReturnSocketAsyncEventArgs(taskSocketAsyncEventArgs);
				}
			}
			else
			{
				task = ((taskSocketAsyncEventArgs.SocketError == SocketError.Success) ? Task.FromResult<Socket>(taskSocketAsyncEventArgs.AcceptSocket) : Task.FromException<Socket>(Socket.GetException(taskSocketAsyncEventArgs.SocketError, false)));
				this.ReturnSocketAsyncEventArgs(taskSocketAsyncEventArgs);
			}
			return task;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000A162C File Offset: 0x0009F82C
		private Task<Socket> AcceptAsyncApm(Socket acceptSocket)
		{
			TaskCompletionSource<Socket> taskCompletionSource = new TaskCompletionSource<Socket>(this);
			this.BeginAccept(acceptSocket, 0, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<Socket> taskCompletionSource2 = (TaskCompletionSource<Socket>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndAccept(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000A1670 File Offset: 0x0009F870
		internal Task ConnectAsync(EndPoint remoteEP)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(remoteEP, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000A16B4 File Offset: 0x0009F8B4
		internal Task ConnectAsync(IPAddress address, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(address, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000A16F8 File Offset: 0x0009F8F8
		internal Task ConnectAsync(IPAddress[] addresses, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(addresses, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000A173C File Offset: 0x0009F93C
		internal Task ConnectAsync(string host, int port)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(this);
			this.BeginConnect(host, port, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<bool> taskCompletionSource2 = (TaskCompletionSource<bool>)iar.AsyncState;
				try
				{
					((Socket)taskCompletionSource2.Task.AsyncState).EndConnect(iar);
					taskCompletionSource2.TrySetResult(true);
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000A1780 File Offset: 0x0009F980
		internal Task<int> ReceiveAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, bool fromNetworkStream)
		{
			Socket.ValidateBuffer(buffer);
			return this.ReceiveAsync(buffer, socketFlags, fromNetworkStream, default(CancellationToken)).AsTask();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000A17B4 File Offset: 0x0009F9B4
		internal ValueTask<int> ReceiveAsync(Memory<byte> buffer, SocketFlags socketFlags, bool fromNetworkStream, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			Socket.AwaitableSocketAsyncEventArgs awaitableSocketAsyncEventArgs = LazyInitializer.EnsureInitialized<Socket.AwaitableSocketAsyncEventArgs>(ref LazyInitializer.EnsureInitialized<Socket.CachedEventArgs>(ref this._cachedTaskEventArgs, () => new Socket.CachedEventArgs()).ValueTaskReceive, () => new Socket.AwaitableSocketAsyncEventArgs());
			if (awaitableSocketAsyncEventArgs.Reserve())
			{
				awaitableSocketAsyncEventArgs.SetBuffer(buffer);
				awaitableSocketAsyncEventArgs.SocketFlags = socketFlags;
				awaitableSocketAsyncEventArgs.WrapExceptionsInIOExceptions = fromNetworkStream;
				return awaitableSocketAsyncEventArgs.ReceiveAsync(this);
			}
			return new ValueTask<int>(this.ReceiveAsyncApm(buffer, socketFlags));
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000A1860 File Offset: 0x0009FA60
		private Task<int> ReceiveAsyncApm(Memory<byte> buffer, SocketFlags socketFlags)
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
				this.BeginReceive(arraySegment.Array, arraySegment.Offset, arraySegment.Count, socketFlags, delegate(IAsyncResult iar)
				{
					TaskCompletionSource<int> taskCompletionSource3 = (TaskCompletionSource<int>)iar.AsyncState;
					try
					{
						taskCompletionSource3.TrySetResult(((Socket)taskCompletionSource3.Task.AsyncState).EndReceive(iar));
					}
					catch (Exception ex)
					{
						taskCompletionSource3.TrySetException(ex);
					}
				}, taskCompletionSource);
				return taskCompletionSource.Task;
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			TaskCompletionSource<int> taskCompletionSource2 = new TaskCompletionSource<int>(this);
			this.BeginReceive(array, 0, buffer.Length, socketFlags, delegate(IAsyncResult iar)
			{
				Tuple<TaskCompletionSource<int>, Memory<byte>, byte[]> tuple = (Tuple<TaskCompletionSource<int>, Memory<byte>, byte[]>)iar.AsyncState;
				try
				{
					int num = ((Socket)tuple.Item1.Task.AsyncState).EndReceive(iar);
					new ReadOnlyMemory<byte>(tuple.Item3, 0, num).Span.CopyTo(tuple.Item2.Span);
					tuple.Item1.TrySetResult(num);
				}
				catch (Exception ex2)
				{
					tuple.Item1.TrySetException(ex2);
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(tuple.Item3, false);
				}
			}, Tuple.Create<TaskCompletionSource<int>, Memory<byte>, byte[]>(taskCompletionSource2, buffer, array));
			return taskCompletionSource2.Task;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000A1920 File Offset: 0x0009FB20
		internal Task<int> ReceiveAsync(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			Socket.ValidateBuffersList(buffers);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(true);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBufferList(int32TaskSocketAsyncEventArgs, buffers, socketFlags);
				return this.GetTaskForSendReceive(this.ReceiveAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, false, true);
			}
			return this.ReceiveAsyncApm(buffers, socketFlags);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000A1960 File Offset: 0x0009FB60
		private Task<int> ReceiveAsyncApm(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginReceive(buffers, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndReceive(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000A19A4 File Offset: 0x0009FBA4
		internal Task<SocketReceiveFromResult> ReceiveFromAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult> stateTaskCompletionSource = new Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult>(this)
			{
				_field1 = remoteEndPoint
			};
			this.BeginReceiveFrom(buffer.Array, buffer.Offset, buffer.Count, socketFlags, ref stateTaskCompletionSource._field1, delegate(IAsyncResult iar)
			{
				Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult> stateTaskCompletionSource2 = (Socket.StateTaskCompletionSource<EndPoint, SocketReceiveFromResult>)iar.AsyncState;
				try
				{
					int num = ((Socket)stateTaskCompletionSource2.Task.AsyncState).EndReceiveFrom(iar, ref stateTaskCompletionSource2._field1);
					stateTaskCompletionSource2.TrySetResult(new SocketReceiveFromResult
					{
						ReceivedBytes = num,
						RemoteEndPoint = stateTaskCompletionSource2._field1
					});
				}
				catch (Exception ex)
				{
					stateTaskCompletionSource2.TrySetException(ex);
				}
			}, stateTaskCompletionSource);
			return stateTaskCompletionSource.Task;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000A1A08 File Offset: 0x0009FC08
		internal Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult> stateTaskCompletionSource = new Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>(this)
			{
				_field1 = socketFlags,
				_field2 = remoteEndPoint
			};
			this.BeginReceiveMessageFrom(buffer.Array, buffer.Offset, buffer.Count, socketFlags, ref stateTaskCompletionSource._field2, delegate(IAsyncResult iar)
			{
				Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult> stateTaskCompletionSource2 = (Socket.StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>)iar.AsyncState;
				try
				{
					IPPacketInformation ippacketInformation;
					int num = ((Socket)stateTaskCompletionSource2.Task.AsyncState).EndReceiveMessageFrom(iar, ref stateTaskCompletionSource2._field1, ref stateTaskCompletionSource2._field2, out ippacketInformation);
					stateTaskCompletionSource2.TrySetResult(new SocketReceiveMessageFromResult
					{
						ReceivedBytes = num,
						RemoteEndPoint = stateTaskCompletionSource2._field2,
						SocketFlags = stateTaskCompletionSource2._field1,
						PacketInformation = ippacketInformation
					});
				}
				catch (Exception ex)
				{
					stateTaskCompletionSource2.TrySetException(ex);
				}
			}, stateTaskCompletionSource);
			return stateTaskCompletionSource.Task;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000A1A74 File Offset: 0x0009FC74
		internal Task<int> SendAsync(ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			Socket.ValidateBuffer(buffer);
			return this.SendAsync(buffer, socketFlags, default(CancellationToken)).AsTask();
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000A1AA8 File Offset: 0x0009FCA8
		internal ValueTask<int> SendAsync(ReadOnlyMemory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			Socket.AwaitableSocketAsyncEventArgs awaitableSocketAsyncEventArgs = LazyInitializer.EnsureInitialized<Socket.AwaitableSocketAsyncEventArgs>(ref LazyInitializer.EnsureInitialized<Socket.CachedEventArgs>(ref this._cachedTaskEventArgs, () => new Socket.CachedEventArgs()).ValueTaskSend, () => new Socket.AwaitableSocketAsyncEventArgs());
			if (awaitableSocketAsyncEventArgs.Reserve())
			{
				awaitableSocketAsyncEventArgs.SetBuffer(MemoryMarshal.AsMemory<byte>(buffer));
				awaitableSocketAsyncEventArgs.SocketFlags = socketFlags;
				awaitableSocketAsyncEventArgs.WrapExceptionsInIOExceptions = false;
				return awaitableSocketAsyncEventArgs.SendAsync(this);
			}
			return new ValueTask<int>(this.SendAsyncApm(buffer, socketFlags));
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000A1B58 File Offset: 0x0009FD58
		internal ValueTask SendAsyncForNetworkStream(ReadOnlyMemory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}
			Socket.AwaitableSocketAsyncEventArgs awaitableSocketAsyncEventArgs = LazyInitializer.EnsureInitialized<Socket.AwaitableSocketAsyncEventArgs>(ref LazyInitializer.EnsureInitialized<Socket.CachedEventArgs>(ref this._cachedTaskEventArgs, () => new Socket.CachedEventArgs()).ValueTaskSend, () => new Socket.AwaitableSocketAsyncEventArgs());
			if (awaitableSocketAsyncEventArgs.Reserve())
			{
				awaitableSocketAsyncEventArgs.SetBuffer(MemoryMarshal.AsMemory<byte>(buffer));
				awaitableSocketAsyncEventArgs.SocketFlags = socketFlags;
				awaitableSocketAsyncEventArgs.WrapExceptionsInIOExceptions = true;
				return awaitableSocketAsyncEventArgs.SendAsyncForNetworkStream(this);
			}
			return new ValueTask(this.SendAsyncApm(buffer, socketFlags));
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000A1C08 File Offset: 0x0009FE08
		private Task<int> SendAsyncApm(ReadOnlyMemory<byte> buffer, SocketFlags socketFlags)
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
				this.BeginSend(arraySegment.Array, arraySegment.Offset, arraySegment.Count, socketFlags, delegate(IAsyncResult iar)
				{
					TaskCompletionSource<int> taskCompletionSource3 = (TaskCompletionSource<int>)iar.AsyncState;
					try
					{
						taskCompletionSource3.TrySetResult(((Socket)taskCompletionSource3.Task.AsyncState).EndSend(iar));
					}
					catch (Exception ex)
					{
						taskCompletionSource3.TrySetException(ex);
					}
				}, taskCompletionSource);
				return taskCompletionSource.Task;
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			buffer.Span.CopyTo(array);
			TaskCompletionSource<int> taskCompletionSource2 = new TaskCompletionSource<int>(this);
			this.BeginSend(array, 0, buffer.Length, socketFlags, delegate(IAsyncResult iar)
			{
				Tuple<TaskCompletionSource<int>, byte[]> tuple = (Tuple<TaskCompletionSource<int>, byte[]>)iar.AsyncState;
				try
				{
					tuple.Item1.TrySetResult(((Socket)tuple.Item1.Task.AsyncState).EndSend(iar));
				}
				catch (Exception ex2)
				{
					tuple.Item1.TrySetException(ex2);
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(tuple.Item2, false);
				}
			}, Tuple.Create<TaskCompletionSource<int>, byte[]>(taskCompletionSource2, array));
			return taskCompletionSource2.Task;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000A1CD8 File Offset: 0x0009FED8
		internal Task<int> SendAsync(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			Socket.ValidateBuffersList(buffers);
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = this.RentSocketAsyncEventArgs(false);
			if (int32TaskSocketAsyncEventArgs != null)
			{
				Socket.ConfigureBufferList(int32TaskSocketAsyncEventArgs, buffers, socketFlags);
				return this.GetTaskForSendReceive(this.SendAsync(int32TaskSocketAsyncEventArgs), int32TaskSocketAsyncEventArgs, false, false);
			}
			return this.SendAsyncApm(buffers, socketFlags);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000A1D18 File Offset: 0x0009FF18
		private Task<int> SendAsyncApm(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginSend(buffers, socketFlags, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndSend(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		internal Task<int> SendToAsync(ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(this);
			this.BeginSendTo(buffer.Array, buffer.Offset, buffer.Count, socketFlags, remoteEP, delegate(IAsyncResult iar)
			{
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(((Socket)taskCompletionSource2.Task.AsyncState).EndSendTo(iar));
				}
				catch (Exception ex)
				{
					taskCompletionSource2.TrySetException(ex);
				}
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000A1DB4 File Offset: 0x0009FFB4
		private static void ValidateBuffer(ArraySegment<byte> buffer)
		{
			if (buffer.Array == null)
			{
				throw new ArgumentNullException("Array");
			}
			if (buffer.Offset < 0 || buffer.Offset > buffer.Array.Length)
			{
				throw new ArgumentOutOfRangeException("Offset");
			}
			if (buffer.Count < 0 || buffer.Count > buffer.Array.Length - buffer.Offset)
			{
				throw new ArgumentOutOfRangeException("Count");
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000A1E2B File Offset: 0x000A002B
		private static void ValidateBuffersList(IList<ArraySegment<byte>> buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException(SR.Format("The parameter {0} must contain one or more elements.", "buffers"), "buffers");
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000A1E60 File Offset: 0x000A0060
		private static void ConfigureBufferList(Socket.Int32TaskSocketAsyncEventArgs saea, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			if (!saea.MemoryBuffer.Equals(default(Memory<byte>)))
			{
				saea.SetBuffer(default(Memory<byte>));
			}
			saea.BufferList = buffers;
			saea.SocketFlags = socketFlags;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000A1EA4 File Offset: 0x000A00A4
		private Task<int> GetTaskForSendReceive(bool pending, Socket.Int32TaskSocketAsyncEventArgs saea, bool fromNetworkStream, bool isReceive)
		{
			Task<int> task;
			if (pending)
			{
				bool flag;
				task = saea.GetCompletionResponsibility(out flag).Task;
				if (flag)
				{
					this.ReturnSocketAsyncEventArgs(saea, isReceive);
				}
			}
			else
			{
				if (saea.SocketError == SocketError.Success)
				{
					int bytesTransferred = saea.BytesTransferred;
					if (bytesTransferred == 0 || (fromNetworkStream & !isReceive))
					{
						task = Socket.s_zeroTask;
					}
					else
					{
						task = Task.FromResult<int>(bytesTransferred);
					}
				}
				else
				{
					task = Task.FromException<int>(Socket.GetException(saea.SocketError, fromNetworkStream));
				}
				this.ReturnSocketAsyncEventArgs(saea, isReceive);
			}
			return task;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000A1F1C File Offset: 0x000A011C
		private static void CompleteAccept(Socket s, Socket.TaskSocketAsyncEventArgs<Socket> saea)
		{
			SocketError socketError = saea.SocketError;
			Socket acceptSocket = saea.AcceptSocket;
			bool flag;
			AsyncTaskMethodBuilder<Socket> completionResponsibility = saea.GetCompletionResponsibility(out flag);
			if (flag)
			{
				s.ReturnSocketAsyncEventArgs(saea);
			}
			if (socketError == SocketError.Success)
			{
				completionResponsibility.SetResult(acceptSocket);
				return;
			}
			completionResponsibility.SetException(Socket.GetException(socketError, false));
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000A1F64 File Offset: 0x000A0164
		private static void CompleteSendReceive(Socket s, Socket.Int32TaskSocketAsyncEventArgs saea, bool isReceive)
		{
			SocketError socketError = saea.SocketError;
			int bytesTransferred = saea.BytesTransferred;
			bool wrapExceptionsInIOExceptions = saea._wrapExceptionsInIOExceptions;
			bool flag;
			AsyncTaskMethodBuilder<int> completionResponsibility = saea.GetCompletionResponsibility(out flag);
			if (flag)
			{
				s.ReturnSocketAsyncEventArgs(saea, isReceive);
			}
			if (socketError == SocketError.Success)
			{
				completionResponsibility.SetResult(bytesTransferred);
				return;
			}
			completionResponsibility.SetException(Socket.GetException(socketError, wrapExceptionsInIOExceptions));
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000A1FB8 File Offset: 0x000A01B8
		private static Exception GetException(SocketError error, bool wrapExceptionsInIOExceptions = false)
		{
			Exception ex = new SocketException((int)error);
			if (!wrapExceptionsInIOExceptions)
			{
				return ex;
			}
			return new IOException(SR.Format("Unable to transfer data on the transport connection: {0}.", ex.Message), ex);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000A1FE8 File Offset: 0x000A01E8
		private Socket.Int32TaskSocketAsyncEventArgs RentSocketAsyncEventArgs(bool isReceive)
		{
			Socket.CachedEventArgs cachedEventArgs = LazyInitializer.EnsureInitialized<Socket.CachedEventArgs>(ref this._cachedTaskEventArgs, () => new Socket.CachedEventArgs());
			Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = (isReceive ? Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedEventArgs.TaskReceive, Socket.s_rentedInt32Sentinel) : Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedEventArgs.TaskSend, Socket.s_rentedInt32Sentinel));
			if (int32TaskSocketAsyncEventArgs == Socket.s_rentedInt32Sentinel)
			{
				return null;
			}
			if (int32TaskSocketAsyncEventArgs == null)
			{
				int32TaskSocketAsyncEventArgs = new Socket.Int32TaskSocketAsyncEventArgs();
				int32TaskSocketAsyncEventArgs.Completed += (isReceive ? Socket.ReceiveCompletedHandler : Socket.SendCompletedHandler);
			}
			return int32TaskSocketAsyncEventArgs;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000A2070 File Offset: 0x000A0270
		private void ReturnSocketAsyncEventArgs(Socket.Int32TaskSocketAsyncEventArgs saea, bool isReceive)
		{
			saea._accessed = false;
			saea._builder = default(AsyncTaskMethodBuilder<int>);
			saea._wrapExceptionsInIOExceptions = false;
			if (isReceive)
			{
				Volatile.Write<Socket.Int32TaskSocketAsyncEventArgs>(ref this._cachedTaskEventArgs.TaskReceive, saea);
				return;
			}
			Volatile.Write<Socket.Int32TaskSocketAsyncEventArgs>(ref this._cachedTaskEventArgs.TaskSend, saea);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000A20BD File Offset: 0x000A02BD
		private void ReturnSocketAsyncEventArgs(Socket.TaskSocketAsyncEventArgs<Socket> saea)
		{
			saea.AcceptSocket = null;
			saea._accessed = false;
			saea._builder = default(AsyncTaskMethodBuilder<Socket>);
			Volatile.Write<Socket.TaskSocketAsyncEventArgs<Socket>>(ref this._cachedTaskEventArgs.TaskAccept, saea);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000A20EC File Offset: 0x000A02EC
		private void DisposeCachedTaskSocketAsyncEventArgs()
		{
			Socket.CachedEventArgs cachedTaskEventArgs = this._cachedTaskEventArgs;
			if (cachedTaskEventArgs != null)
			{
				Socket.TaskSocketAsyncEventArgs<Socket> taskSocketAsyncEventArgs = Interlocked.Exchange<Socket.TaskSocketAsyncEventArgs<Socket>>(ref cachedTaskEventArgs.TaskAccept, Socket.s_rentedSocketSentinel);
				if (taskSocketAsyncEventArgs != null)
				{
					taskSocketAsyncEventArgs.Dispose();
				}
				Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs = Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.TaskReceive, Socket.s_rentedInt32Sentinel);
				if (int32TaskSocketAsyncEventArgs != null)
				{
					int32TaskSocketAsyncEventArgs.Dispose();
				}
				Socket.Int32TaskSocketAsyncEventArgs int32TaskSocketAsyncEventArgs2 = Interlocked.Exchange<Socket.Int32TaskSocketAsyncEventArgs>(ref cachedTaskEventArgs.TaskSend, Socket.s_rentedInt32Sentinel);
				if (int32TaskSocketAsyncEventArgs2 != null)
				{
					int32TaskSocketAsyncEventArgs2.Dispose();
				}
				Socket.AwaitableSocketAsyncEventArgs awaitableSocketAsyncEventArgs = Interlocked.Exchange<Socket.AwaitableSocketAsyncEventArgs>(ref cachedTaskEventArgs.ValueTaskReceive, Socket.AwaitableSocketAsyncEventArgs.Reserved);
				if (awaitableSocketAsyncEventArgs != null)
				{
					awaitableSocketAsyncEventArgs.Dispose();
				}
				Socket.AwaitableSocketAsyncEventArgs awaitableSocketAsyncEventArgs2 = Interlocked.Exchange<Socket.AwaitableSocketAsyncEventArgs>(ref cachedTaskEventArgs.ValueTaskSend, Socket.AwaitableSocketAsyncEventArgs.Reserved);
				if (awaitableSocketAsyncEventArgs2 == null)
				{
					return;
				}
				awaitableSocketAsyncEventArgs2.Dispose();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified socket type and protocol.</summary>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">The combination of  <paramref name="socketType" /> and <paramref name="protocolType" /> results in an invalid socket. </exception>
		// Token: 0x06002DAD RID: 11693 RVA: 0x000A218C File Offset: 0x000A038C
		public Socket(SocketType socketType, ProtocolType protocolType)
			: this(AddressFamily.InterNetworkV6, socketType, protocolType)
		{
			this.DualMode = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified address family, socket type and protocol.</summary>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values. </param>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values. </param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">The combination of <paramref name="addressFamily" />, <paramref name="socketType" />, and <paramref name="protocolType" /> results in an invalid socket. </exception>
		// Token: 0x06002DAE RID: 11694 RVA: 0x000A21A0 File Offset: 0x000A03A0
		public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.ReadSem = new SemaphoreSlim(1, 1);
			this.WriteSem = new SemaphoreSlim(1, 1);
			this.is_blocking = true;
			base..ctor();
			Socket.s_LoggingEnabled = Logging.On;
			bool flag = Socket.s_LoggingEnabled;
			Socket.InitializeSockets();
			int num;
			this.m_Handle = new SafeSocketHandle(Socket.Socket_icall(addressFamily, socketType, protocolType, out num), true);
			if (this.m_Handle.IsInvalid)
			{
				throw new SocketException();
			}
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
			IPProtectionLevel ipprotectionLevel = SettingsSectionInternal.Section.IPProtectionLevel;
			if (ipprotectionLevel != IPProtectionLevel.Unspecified)
			{
				this.SetIPProtectionLevel(ipprotectionLevel);
			}
			this.SocketDefaults();
			bool flag2 = Socket.s_LoggingEnabled;
		}

		/// <summary>Gets a value indicating whether IPv4 support is available and enabled on the current host.</summary>
		/// <returns>true if the current host supports the IPv4 protocol; otherwise, false.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000A224F File Offset: 0x000A044F
		[Obsolete("SupportsIPv4 is obsoleted for this type, please use OSSupportsIPv4 instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static bool SupportsIPv4
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv4;
			}
		}

		/// <summary>Indicates whether the underlying operating system and network adaptors support Internet Protocol version 4 (IPv4).</summary>
		/// <returns>true if the operating system and network adaptors support the IPv4 protocol; otherwise, false.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000A224F File Offset: 0x000A044F
		public static bool OSSupportsIPv4
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv4;
			}
		}

		/// <summary>Gets a value that indicates whether the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> members.</summary>
		/// <returns>true if the Framework supports IPv6 for certain obsolete <see cref="T:System.Net.Dns" /> methods; otherwise, false.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000A225D File Offset: 0x000A045D
		[Obsolete("SupportsIPv6 is obsoleted for this type, please use OSSupportsIPv6 instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static bool SupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv6;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000A225D File Offset: 0x000A045D
		internal static bool LegacySupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_SupportsIPv6;
			}
		}

		/// <summary>Indicates whether the underlying operating system and network adaptors support Internet Protocol version 6 (IPv6).</summary>
		/// <returns>true if the operating system and network adaptors support the IPv6 protocol; otherwise, false.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000A226B File Offset: 0x000A046B
		public static bool OSSupportsIPv6
		{
			get
			{
				Socket.InitializeSockets();
				return Socket.s_OSSupportsIPv6;
			}
		}

		/// <summary>Gets the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the operating system handle for the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000A2279 File Offset: 0x000A0479
		public IntPtr Handle
		{
			get
			{
				return this.m_Handle.DangerousGetHandle();
			}
		}

		/// <summary>Specifies whether the socket should only use Overlapped I/O mode.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> uses only overlapped I/O; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The socket has been bound to a completion port.</exception>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000A2286 File Offset: 0x000A0486
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000A228E File Offset: 0x000A048E
		public bool UseOnlyOverlappedIO
		{
			get
			{
				return this.useOverlappedIO;
			}
			set
			{
				this.useOverlappedIO = value;
			}
		}

		/// <summary>Gets the address family of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000A2297 File Offset: 0x000A0497
		public AddressFamily AddressFamily
		{
			get
			{
				return this.addressFamily;
			}
		}

		/// <summary>Gets the type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000A229F File Offset: 0x000A049F
		public SocketType SocketType
		{
			get
			{
				return this.socketType;
			}
		}

		/// <summary>Gets the protocol type of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000A22A7 File Offset: 0x000A04A7
		public ProtocolType ProtocolType
		{
			get
			{
				return this.protocolType;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows only one process to bind to a port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows only one socket to bind to a specific port; otherwise, false. The default is true for Windows Server 2003 and Windows XP Service Pack 2, and false for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> has been called for this <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000A22AF File Offset: 0x000A04AF
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000A22C8 File Offset: 0x000A04C8
		public bool ExclusiveAddressUse
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse) != 0;
			}
			set
			{
				if (this.IsBound)
				{
					throw new InvalidOperationException(SR.GetString("The socket must not be bound or connected."));
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the receive buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000A22F6 File Offset: 0x000A04F6
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000A230D File Offset: 0x000A050D
		public int ReceiveBufferSize
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the size of the send buffer of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the send buffer. The default is 8192.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000A232F File Offset: 0x000A052F
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000A2346 File Offset: 0x000A0546
		public int SendBufferSize
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Receive" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000A2368 File Offset: 0x000A0568
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x000A237F File Offset: 0x000A057F
		public int ReceiveTimeout
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Sockets.Socket.Send" /> call will time out.</summary>
		/// <returns>The time-out value, in milliseconds. If you set the property with a value between 1 and 499, the value will be changed to 500. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than -1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000A23A8 File Offset: 0x000A05A8
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x000A23BF File Offset: 0x000A05BF
		public int SendTimeout
		{
			get
			{
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == -1)
				{
					value = 0;
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> will delay closing a socket in an attempt to send all pending data.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.LingerOption" /> that specifies how to linger while closing a socket.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000A23E8 File Offset: 0x000A05E8
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x000A23FF File Offset: 0x000A05FF
		public LingerOption LingerState
		{
			get
			{
				return (LingerOption)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		/// <summary>Gets or sets a value that specifies the Time To Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The TTL value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL value can't be set to a negative number.</exception>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. This error is also returned when an attempt was made to set TTL to a value higher than 255.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000A2414 File Offset: 0x000A0614
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000A2464 File Offset: 0x000A0664
		public short Ttl
		{
			get
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					return (short)((int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress));
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					return (short)((int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.ReuseAddress));
				}
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, (int)value);
					return;
				}
				if (this.addressFamily == AddressFamily.InterNetworkV6)
				{
					this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.ReuseAddress, (int)value);
					return;
				}
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows datagram fragmentation; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000A24BF File Offset: 0x000A06BF
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000A24ED File Offset: 0x000A06ED
		public bool DontFragment
		{
			get
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					return (int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment) != 0;
				}
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
			set
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment, value ? 1 : 0);
					return;
				}
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> is a dual-mode socket used for both IPv4 and IPv6.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.Sockets.Socket" /> is a  dual-mode socket; otherwise, false. The default is false.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000A2518 File Offset: 0x000A0718
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000A2546 File Offset: 0x000A0746
		public bool DualMode
		{
			get
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
				}
				return (int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only) == 0;
			}
			set
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
				}
				this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, value ? 0 : 1);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000A2573 File Offset: 0x000A0773
		private bool IsDualMode
		{
			get
			{
				return this.AddressFamily == AddressFamily.InterNetworkV6 && this.DualMode;
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000A2587 File Offset: 0x000A0787
		internal bool CanTryAddressFamily(AddressFamily family)
		{
			return family == this.addressFamily || (family == AddressFamily.InterNetwork && this.IsDualMode);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
		/// <param name="addresses">The IP addresses of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DCE RID: 11726 RVA: 0x000A25A0 File Offset: 0x000A07A0
		public void Connect(IPAddress[] addresses, int port)
		{
			bool flag = Socket.s_LoggingEnabled;
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The number of specified IP addresses has to be greater than 0."), "addresses");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
			Exception ex = null;
			foreach (IPAddress ipaddress in addresses)
			{
				if (this.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					try
					{
						this.Connect(new IPEndPoint(ipaddress, port));
						ex = null;
						break;
					}
					catch (Exception ex2)
					{
						if (NclUtilities.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			if (!this.Connected)
			{
				throw new ArgumentException(SR.GetString("None of the discovered or specified addresses match the socket address family."), "addresses");
			}
			bool flag2 = Socket.s_LoggingEnabled;
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0 or exceeds the size of the buffer. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DCF RID: 11727 RVA: 0x000A26A8 File Offset: 0x000A08A8
		public int Send(byte[] buffer, int size, SocketFlags socketFlags)
		{
			return this.Send(buffer, 0, size, socketFlags);
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" /> using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD0 RID: 11728 RVA: 0x000A26B4 File Offset: 0x000A08B4
		public int Send(byte[] buffer, SocketFlags socketFlags)
		{
			return this.Send(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags);
		}

		/// <summary>Sends data to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000A26C8 File Offset: 0x000A08C8
		public int Send(byte[] buffer)
		{
			return this.Send(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None);
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000A26DC File Offset: 0x000A08DC
		public int Send(IList<ArraySegment<byte>> buffers)
		{
			return this.Send(buffers, SocketFlags.None);
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000A26E8 File Offset: 0x000A08E8
		public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Send(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object with the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> transmit flag.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000A2709 File Offset: 0x000A0909
		public void SendFile(string fileName)
		{
			this.SendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread);
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000A2718 File Offset: 0x000A0918
		public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Send(buffer, offset, size, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Sends the specified number of bytes of data to the specified endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="size" /> exceeds the size of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD6 RID: 11734 RVA: 0x000A273C File Offset: 0x000A093C
		public int SendTo(byte[] buffer, int size, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, size, socketFlags, remoteEP);
		}

		/// <summary>Sends data to a specific endpoint using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000A274A File Offset: 0x000A094A
		public int SendTo(byte[] buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags, remoteEP);
		}

		/// <summary>Sends data to the specified endpoint.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000A275F File Offset: 0x000A095F
		public int SendTo(byte[] buffer, EndPoint remoteEP)
		{
			return this.SendTo(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None, remoteEP);
		}

		/// <summary>Receives the specified number of bytes of data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> exceeds the size of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000A2774 File Offset: 0x000A0974
		public int Receive(byte[] buffer, int size, SocketFlags socketFlags)
		{
			return this.Receive(buffer, 0, size, socketFlags);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DDA RID: 11738 RVA: 0x000A2780 File Offset: 0x000A0980
		public int Receive(byte[] buffer, SocketFlags socketFlags)
		{
			return this.Receive(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DDB RID: 11739 RVA: 0x000A2794 File Offset: 0x000A0994
		public int Receive(byte[] buffer)
		{
			return this.Receive(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None);
		}

		/// <summary>Receives the specified number of bytes from a bound <see cref="T:System.Net.Sockets.Socket" /> into the specified offset position of the receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DDC RID: 11740 RVA: 0x000A27A8 File Offset: 0x000A09A8
		public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Receive(buffer, offset, size, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000A27CC File Offset: 0x000A09CC
		public int Receive(IList<ArraySegment<byte>> buffers)
		{
			return this.Receive(buffers, SocketFlags.None);
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.-or-<paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DDE RID: 11742 RVA: 0x000A27D8 File Offset: 0x000A09D8
		public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			SocketError socketError;
			int num = this.Receive(buffers, socketFlags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Receives the specified number of bytes into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DDF RID: 11743 RVA: 0x000A27F9 File Offset: 0x000A09F9
		public int ReceiveFrom(byte[] buffer, int size, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, size, socketFlags, ref remoteEP);
		}

		/// <summary>Receives a datagram into the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE0 RID: 11744 RVA: 0x000A2807 File Offset: 0x000A0A07
		public int ReceiveFrom(byte[] buffer, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, (buffer != null) ? buffer.Length : 0, socketFlags, ref remoteEP);
		}

		/// <summary>Receives a datagram into the data buffer and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE1 RID: 11745 RVA: 0x000A281C File Offset: 0x000A0A1C
		public int ReceiveFrom(byte[] buffer, ref EndPoint remoteEP)
		{
			return this.ReceiveFrom(buffer, 0, (buffer != null) ? buffer.Length : 0, SocketFlags.None, ref remoteEP);
		}

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using the <see cref="T:System.Net.Sockets.IOControlCode" /> enumeration to specify control codes.</summary>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <param name="ioControlCode">A <see cref="T:System.Net.Sockets.IOControlCode" /> value that specifies the control code of the operation to perform. </param>
		/// <param name="optionInValue">An array of type <see cref="T:System.Byte" /> that contains the input data required by the operation. </param>
		/// <param name="optionOutValue">An array of type <see cref="T:System.Byte" /> that contains the output data returned by the operation. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE2 RID: 11746 RVA: 0x000A2831 File Offset: 0x000A0A31
		public int IOControl(IOControlCode ioControlCode, byte[] optionInValue, byte[] optionOutValue)
		{
			return this.IOControl((int)ioControlCode, optionInValue, optionOutValue);
		}

		/// <summary>Set the IP protection level on a socket.</summary>
		/// <param name="level">The IP protection level to set on this socket. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="level" /> parameter cannot be <see cref="F:System.Net.Sockets.IPProtectionLevel.Unspecified" />. The IP protection level cannot be set to unspecified.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.Sockets.AddressFamily" /> of the socket must be either <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</exception>
		// Token: 0x06002DE3 RID: 11747 RVA: 0x000A2840 File Offset: 0x000A0A40
		public void SetIPProtectionLevel(IPProtectionLevel level)
		{
			if (level == IPProtectionLevel.Unspecified)
			{
				throw new ArgumentException(SR.GetString("The specified value is not valid."), "level");
			}
			if (this.addressFamily == AddressFamily.InterNetworkV6)
			{
				this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPProtectionLevel, (int)level);
				return;
			}
			if (this.addressFamily == AddressFamily.InterNetwork)
			{
				this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IPProtectionLevel, (int)level);
				return;
			}
			throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
		}

		/// <summary>Sends the file <paramref name="fileName" /> to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the <see cref="F:System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread" /> flag.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous send.</returns>
		/// <param name="fileName">A string that contains the path and name of the file to send. This parameter can be null. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000A289F File Offset: 0x000A0A9F
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSendFile(string fileName, AsyncCallback callback, object state)
		{
			return this.BeginSendFile(fileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, callback, state);
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.Sockets.Socket" /> is not in the socket family.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000A28B0 File Offset: 0x000A0AB0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			bool flag = Socket.s_LoggingEnabled;
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (!this.CanTryAddressFamily(address.AddressFamily))
			{
				throw new NotSupportedException(SR.GetString("This protocol version is not supported."));
			}
			IAsyncResult asyncResult = this.BeginConnect(new IPEndPoint(address, port), requestCallback, state);
			bool flag2 = Socket.s_LoggingEnabled;
			return asyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is less than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002DE6 RID: 11750 RVA: 0x000A2938 File Offset: 0x000A0B38
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginSend(buffer, offset, size, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000A2968 File Offset: 0x000A0B68
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginSend(buffers, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DE8 RID: 11752 RVA: 0x000A2994 File Offset: 0x000A0B94
		public int EndSend(IAsyncResult asyncResult)
		{
			SocketError socketError;
			int num = this.EndSend(asyncResult, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000A29B4 File Offset: 0x000A0BB4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginReceive(buffer, offset, size, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002DEA RID: 11754 RVA: 0x000A29E4 File Offset: 0x000A0BE4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			SocketError socketError;
			IAsyncResult asyncResult = this.BeginReceive(buffers, socketFlags, out socketError, callback, state);
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				throw new SocketException(socketError);
			}
			return asyncResult;
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DEB RID: 11755 RVA: 0x000A2A10 File Offset: 0x000A0C10
		public int EndReceive(IAsyncResult asyncResult)
		{
			SocketError socketError;
			int num = this.EndReceive(asyncResult, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt and receives the first block of data sent by the client application.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <param name="receiveSize">The number of bytes to accept from the sender. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002DEC RID: 11756 RVA: 0x000A2A30 File Offset: 0x000A0C30
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAccept(int receiveSize, AsyncCallback callback, object state)
		{
			return this.BeginAccept(null, receiveSize, callback, state);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data transferred.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred. </param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" /> See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DED RID: 11757 RVA: 0x000A2A3C File Offset: 0x000A0C3C
		public Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult)
		{
			byte[] array;
			int num;
			Socket socket = this.EndAccept(out array, out num, asyncResult);
			buffer = new byte[num];
			Array.Copy(array, buffer, num);
			return socket;
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000A2A68 File Offset: 0x000A0C68
		private static object InternalSyncObject
		{
			get
			{
				if (Socket.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Socket.s_InternalSyncObject, obj, null);
				}
				return Socket.s_InternalSyncObject;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000A2A94 File Offset: 0x000A0C94
		internal bool CleanedUp
		{
			get
			{
				return this.m_IntCleanedUp == 1;
			}
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000A2AA0 File Offset: 0x000A0CA0
		internal static void InitializeSockets()
		{
			if (!Socket.s_Initialized)
			{
				object internalSyncObject = Socket.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!Socket.s_Initialized)
					{
						bool flag2 = Socket.IsProtocolSupported(NetworkInterfaceComponent.IPv4);
						bool flag3 = Socket.IsProtocolSupported(NetworkInterfaceComponent.IPv6);
						if (flag3)
						{
							Socket.s_OSSupportsIPv6 = true;
							flag3 = SettingsSectionInternal.Section.Ipv6Enabled;
						}
						Socket.s_SupportsIPv4 = flag2;
						Socket.s_SupportsIPv6 = flag3;
						Socket.s_Initialized = true;
					}
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
		// Token: 0x06002DF1 RID: 11761 RVA: 0x000A2B28 File Offset: 0x000A0D28
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000A2B38 File Offset: 0x000A0D38
		~Socket()
		{
			this.Dispose(false);
		}

		/// <summary>Begins an asynchronous request for a connection to a remote host.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation. </returns>
		/// <param name="socketType">One of the <see cref="T:System.Net.Sockets.SocketType" /> values.</param>
		/// <param name="protocolType">One of the <see cref="T:System.Net.Sockets.ProtocolType" /> values.</param>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is listening or a socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the local endpoint and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> are not the same address family.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x06002DF3 RID: 11763 RVA: 0x000A2B68 File Offset: 0x000A0D68
		public static bool ConnectAsync(SocketType socketType, ProtocolType protocolType, SocketAsyncEventArgs e)
		{
			bool flag = Socket.s_LoggingEnabled;
			if (e.BufferList != null)
			{
				throw new ArgumentException(SR.GetString("Multiple buffers cannot be used with this method."), "BufferList");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			EndPoint remoteEndPoint = e.RemoteEndPoint;
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			bool flag2;
			if (dnsEndPoint != null)
			{
				Socket socket = null;
				MultipleConnectAsync multipleConnectAsync;
				if (dnsEndPoint.AddressFamily == AddressFamily.Unspecified)
				{
					multipleConnectAsync = new DualSocketMultipleConnectAsync(socketType, protocolType);
				}
				else
				{
					socket = new Socket(dnsEndPoint.AddressFamily, socketType, protocolType);
					multipleConnectAsync = new SingleSocketMultipleConnectAsync(socket, false);
				}
				e.StartOperationCommon(socket);
				e.StartOperationWrapperConnect(multipleConnectAsync);
				try
				{
					flag2 = multipleConnectAsync.StartConnectAsync(e, dnsEndPoint);
					goto IL_00B7;
				}
				catch
				{
					Interlocked.Exchange(ref e.in_progress, 0);
					throw;
				}
			}
			flag2 = new Socket(remoteEndPoint.AddressFamily, socketType, protocolType).ConnectAsync(e);
			IL_00B7:
			bool flag3 = Socket.s_LoggingEnabled;
			return flag2;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000A2C48 File Offset: 0x000A0E48
		internal void InternalShutdown(SocketShutdown how)
		{
			if (!this.is_connected || this.CleanedUp)
			{
				return;
			}
			int num;
			Socket.Shutdown_internal(this.m_Handle, how, out num);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000A2C74 File Offset: 0x000A0E74
		internal IAsyncResult UnsafeBeginConnect(EndPoint remoteEP, AsyncCallback callback, object state)
		{
			return this.BeginConnect(remoteEP, callback, state);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000A2C7F File Offset: 0x000A0E7F
		internal IAsyncResult UnsafeBeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			return this.BeginSend(buffer, offset, size, socketFlags, callback, state);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000A2C90 File Offset: 0x000A0E90
		internal IAsyncResult UnsafeBeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			return this.BeginReceive(buffer, offset, size, socketFlags, callback, state);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000A2CA4 File Offset: 0x000A0EA4
		internal IAsyncResult BeginMultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			ArraySegment<byte>[] array = new ArraySegment<byte>[buffers.Length];
			for (int i = 0; i < buffers.Length; i++)
			{
				array[i] = new ArraySegment<byte>(buffers[i].Buffer, buffers[i].Offset, buffers[i].Size);
			}
			return this.BeginSend(array, socketFlags, callback, state);
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000A2CF7 File Offset: 0x000A0EF7
		internal IAsyncResult UnsafeBeginMultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags, AsyncCallback callback, object state)
		{
			return this.BeginMultipleSend(buffers, socketFlags, callback, state);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000A2D04 File Offset: 0x000A0F04
		internal int EndMultipleSend(IAsyncResult asyncResult)
		{
			return this.EndSend(asyncResult);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000A2D10 File Offset: 0x000A0F10
		internal void MultipleSend(BufferOffsetSize[] buffers, SocketFlags socketFlags)
		{
			ArraySegment<byte>[] array = new ArraySegment<byte>[buffers.Length];
			for (int i = 0; i < buffers.Length; i++)
			{
				array[i] = new ArraySegment<byte>(buffers[i].Buffer, buffers[i].Offset, buffers[i].Size);
			}
			this.Send(array, socketFlags);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000A2D64 File Offset: 0x000A0F64
		internal void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue, bool silent)
		{
			if (this.CleanedUp && this.is_closed)
			{
				if (silent)
				{
					return;
				}
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			else
			{
				int num;
				Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, null, null, optionValue, out num);
				if (!silent && num != 0)
				{
					throw new SocketException(num);
				}
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.Socket" /> class using the specified value returned from <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</summary>
		/// <param name="socketInformation">The socket information returned by <see cref="M:System.Net.Sockets.Socket.DuplicateAndClose(System.Int32)" />.</param>
		// Token: 0x06002DFD RID: 11773 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		public Socket(SocketInformation socketInformation)
		{
			this.ReadSem = new SemaphoreSlim(1, 1);
			this.WriteSem = new SemaphoreSlim(1, 1);
			this.is_blocking = true;
			base..ctor();
			this.is_listening = (socketInformation.Options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			this.is_connected = (socketInformation.Options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			this.is_blocking = (socketInformation.Options & SocketInformationOptions.NonBlocking) == (SocketInformationOptions)0;
			this.useOverlappedIO = (socketInformation.Options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			IList list = DataConverter.Unpack("iiiil", socketInformation.ProtocolInformation, 0);
			this.addressFamily = (AddressFamily)((int)list[0]);
			this.socketType = (SocketType)((int)list[1]);
			this.protocolType = (ProtocolType)((int)list[2]);
			this.is_bound = (int)list[3] != 0;
			this.m_Handle = new SafeSocketHandle((IntPtr)((long)list[4]), true);
			Socket.InitializeSockets();
			this.SocketDefaults();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000A2EBC File Offset: 0x000A10BC
		internal Socket(AddressFamily family, SocketType type, ProtocolType proto, SafeSocketHandle safe_handle)
		{
			this.ReadSem = new SemaphoreSlim(1, 1);
			this.WriteSem = new SemaphoreSlim(1, 1);
			this.is_blocking = true;
			base..ctor();
			this.addressFamily = family;
			this.socketType = type;
			this.protocolType = proto;
			this.m_Handle = safe_handle;
			this.is_connected = true;
			Socket.InitializeSockets();
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000A2F1C File Offset: 0x000A111C
		private void SocketDefaults()
		{
			try
			{
				if (this.addressFamily == AddressFamily.InterNetwork)
				{
					this.DontFragment = false;
					if (this.protocolType == ProtocolType.Tcp)
					{
						this.NoDelay = false;
					}
				}
				else if (this.addressFamily == AddressFamily.InterNetworkV6 && this.socketType != SocketType.Raw)
				{
					this.DualMode = true;
				}
			}
			catch (SocketException)
			{
			}
		}

		// Token: 0x06002E00 RID: 11776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Socket_icall(AddressFamily family, SocketType type, ProtocolType proto, out int error);

		/// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
		/// <returns>The number of bytes of data received from the network and available to be read.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000A2F7C File Offset: 0x000A117C
		public int Available
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				int num2;
				int num = Socket.Available_internal(this.m_Handle, out num2);
				if (num2 != 0)
				{
					throw new SocketException(num2);
				}
				return num;
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000A2FA8 File Offset: 0x000A11A8
		private static int Available_internal(SafeSocketHandle safeHandle, out int error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = Socket.Available_icall(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002E03 RID: 11779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Available_icall(IntPtr socket, out int error);

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.Socket" /> can send or receive broadcast packets.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> allows broadcast packets; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">This option is valid for a datagram socket only. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000A2FEC File Offset: 0x000A11EC
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x000A301E File Offset: 0x000A121E
		public bool EnableBroadcast
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				if (this.protocolType != ProtocolType.Udp)
				{
					throw new SocketException(10042);
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast) != 0;
			}
			set
			{
				this.ThrowIfDisposedAndClosed();
				if (this.protocolType != ProtocolType.Udp)
				{
					throw new SocketException(10042);
				}
				this.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, value ? 1 : 0);
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is bound to a specific local port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> is bound to a local port; otherwise, false.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000A304F File Offset: 0x000A124F
		public bool IsBound
		{
			get
			{
				return this.is_bound;
			}
		}

		/// <summary>Gets or sets a value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> receives outgoing multicast packets; otherwise, false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000A3058 File Offset: 0x000A1258
		// (set) Token: 0x06002E08 RID: 11784 RVA: 0x000A30C0 File Offset: 0x000A12C0
		public bool MulticastLoopback
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				if (this.protocolType == ProtocolType.Tcp)
				{
					throw new SocketException(10042);
				}
				AddressFamily addressFamily = this.addressFamily;
				if (addressFamily == AddressFamily.InterNetwork)
				{
					return (int)this.GetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback) != 0;
				}
				if (addressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
				}
				return (int)this.GetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback) != 0;
			}
			set
			{
				this.ThrowIfDisposedAndClosed();
				if (this.protocolType == ProtocolType.Tcp)
				{
					throw new SocketException(10042);
				}
				AddressFamily addressFamily = this.addressFamily;
				if (addressFamily == AddressFamily.InterNetwork)
				{
					this.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, value ? 1 : 0);
					return;
				}
				if (addressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException("This property is only valid for InterNetwork and InterNetworkV6 sockets");
				}
				this.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastLoopback, value ? 1 : 0);
			}
		}

		/// <summary>Gets the local endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> that the <see cref="T:System.Net.Sockets.Socket" /> is using for communications.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000A3128 File Offset: 0x000A1328
		public EndPoint LocalEndPoint
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				if (this.seed_endpoint == null)
				{
					return null;
				}
				int num;
				SocketAddress socketAddress = Socket.LocalEndPoint_internal(this.m_Handle, (int)this.addressFamily, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return this.seed_endpoint.Create(socketAddress);
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000A3170 File Offset: 0x000A1370
		private static SocketAddress LocalEndPoint_internal(SafeSocketHandle safeHandle, int family, out int error)
		{
			bool flag = false;
			SocketAddress socketAddress;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				socketAddress = Socket.LocalEndPoint_icall(safeHandle.DangerousGetHandle(), family, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return socketAddress;
		}

		// Token: 0x06002E0B RID: 11787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SocketAddress LocalEndPoint_icall(IntPtr socket, int family, out int error);

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.Socket" /> is in blocking mode.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> will block; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000A31B4 File Offset: 0x000A13B4
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x000A31BC File Offset: 0x000A13BC
		public bool Blocking
		{
			get
			{
				return this.is_blocking;
			}
			set
			{
				this.ThrowIfDisposedAndClosed();
				int num;
				Socket.Blocking_internal(this.m_Handle, value, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				this.is_blocking = value;
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000A31F0 File Offset: 0x000A13F0
		private static void Blocking_internal(SafeSocketHandle safeHandle, bool block, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.Blocking_icall(safeHandle.DangerousGetHandle(), block, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E0F RID: 11791
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Blocking_icall(IntPtr socket, bool block, out int error);

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.Sockets.Socket" /> is connected to a remote host as of the last <see cref="Overload:System.Net.Sockets.Socket.Send" /> or <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operation.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> was connected to a remote resource as of the most recent operation; otherwise, false.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000A3230 File Offset: 0x000A1430
		// (set) Token: 0x06002E11 RID: 11793 RVA: 0x000A3238 File Offset: 0x000A1438
		public bool Connected
		{
			get
			{
				return this.is_connected;
			}
			internal set
			{
				this.is_connected = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the stream <see cref="T:System.Net.Sockets.Socket" /> is using the Nagle algorithm.</summary>
		/// <returns>false if the <see cref="T:System.Net.Sockets.Socket" /> uses the Nagle algorithm; otherwise, true. The default is false.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000A3241 File Offset: 0x000A1441
		// (set) Token: 0x06002E13 RID: 11795 RVA: 0x000A325F File Offset: 0x000A145F
		public bool NoDelay
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				this.ThrowIfUdp();
				return (int)this.GetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug) != 0;
			}
			set
			{
				this.ThrowIfDisposedAndClosed();
				this.ThrowIfUdp();
				this.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, value ? 1 : 0);
			}
		}

		/// <summary>Gets the remote endpoint.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> with which the <see cref="T:System.Net.Sockets.Socket" /> is communicating.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000A327C File Offset: 0x000A147C
		public EndPoint RemoteEndPoint
		{
			get
			{
				this.ThrowIfDisposedAndClosed();
				if (!this.is_connected || this.seed_endpoint == null)
				{
					return null;
				}
				int num;
				SocketAddress socketAddress = Socket.RemoteEndPoint_internal(this.m_Handle, (int)this.addressFamily, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
				return this.seed_endpoint.Create(socketAddress);
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000A32CC File Offset: 0x000A14CC
		private static SocketAddress RemoteEndPoint_internal(SafeSocketHandle safeHandle, int family, out int error)
		{
			bool flag = false;
			SocketAddress socketAddress;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				socketAddress = Socket.RemoteEndPoint_icall(safeHandle.DangerousGetHandle(), family, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return socketAddress;
		}

		// Token: 0x06002E16 RID: 11798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SocketAddress RemoteEndPoint_icall(IntPtr socket, int family, out int error);

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000A3310 File Offset: 0x000A1510
		internal SafeHandle SafeHandle
		{
			get
			{
				return this.m_Handle;
			}
		}

		/// <summary>Determines the status of one or more sockets.</summary>
		/// <param name="checkRead">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for readability. </param>
		/// <param name="checkWrite">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for writability. </param>
		/// <param name="checkError">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Net.Sockets.Socket" /> instances to check for errors. </param>
		/// <param name="microSeconds">The time-out value, in microseconds. A -1 value indicates an infinite time-out.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="checkRead" /> parameter is null or empty.-and- The <paramref name="checkWrite" /> parameter is null or empty -and- The <paramref name="checkError" /> parameter is null or empty. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002E18 RID: 11800 RVA: 0x000A3318 File Offset: 0x000A1518
		public static void Select(IList checkRead, IList checkWrite, IList checkError, int microSeconds)
		{
			List<Socket> list = new List<Socket>();
			Socket.AddSockets(list, checkRead, "checkRead");
			Socket.AddSockets(list, checkWrite, "checkWrite");
			Socket.AddSockets(list, checkError, "checkError");
			if (list.Count == 3)
			{
				throw new ArgumentNullException("checkRead, checkWrite, checkError", "All the lists are null or empty.");
			}
			Socket[] array = list.ToArray();
			int num;
			Socket.Select_icall(ref array, microSeconds, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (array == null)
			{
				if (checkRead != null)
				{
					checkRead.Clear();
				}
				if (checkWrite != null)
				{
					checkWrite.Clear();
				}
				if (checkError != null)
				{
					checkError.Clear();
				}
				return;
			}
			int num2 = 0;
			int num3 = array.Length;
			IList list2 = checkRead;
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				Socket socket = array[i];
				if (socket == null)
				{
					if (list2 != null)
					{
						int num5 = list2.Count - num4;
						for (int j = 0; j < num5; j++)
						{
							list2.RemoveAt(num4);
						}
					}
					list2 = ((num2 == 0) ? checkWrite : checkError);
					num4 = 0;
					num2++;
				}
				else
				{
					if (num2 == 1 && list2 == checkWrite && !socket.is_connected && (int)socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
					{
						socket.is_connected = true;
					}
					while ((Socket)list2[num4] != socket)
					{
						list2.RemoveAt(num4);
					}
					num4++;
				}
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000A3460 File Offset: 0x000A1660
		private static void AddSockets(List<Socket> sockets, IList list, string name)
		{
			if (list != null)
			{
				foreach (object obj in list)
				{
					Socket socket = (Socket)obj;
					if (socket == null)
					{
						throw new ArgumentNullException(name, "Contains a null element");
					}
					sockets.Add(socket);
				}
			}
			sockets.Add(null);
		}

		// Token: 0x06002E1A RID: 11802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Select_icall(ref Socket[] sockets, int microSeconds, out int error);

		/// <summary>Determines the status of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The status of the <see cref="T:System.Net.Sockets.Socket" /> based on the polling mode value passed in the <paramref name="mode" /> parameter.Mode Return Value <see cref="F:System.Net.Sockets.SelectMode.SelectRead" />true if <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> has been called and a connection is pending; -or- true if data is available for reading; -or- true if the connection has been closed, reset, or terminated; otherwise, returns false. <see cref="F:System.Net.Sockets.SelectMode.SelectWrite" />true, if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" />, and the connection has succeeded; -or- true if data can be sent; otherwise, returns false. <see cref="F:System.Net.Sockets.SelectMode.SelectError" />true if processing a <see cref="M:System.Net.Sockets.Socket.Connect(System.Net.EndPoint)" /> that does not block, and the connection has failed; -or- true if <see cref="F:System.Net.Sockets.SocketOptionName.OutOfBandInline" /> is not set and out-of-band data is available; otherwise, returns false. </returns>
		/// <param name="microSeconds">The time to wait for a response, in microseconds. </param>
		/// <param name="mode">One of the <see cref="T:System.Net.Sockets.SelectMode" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="mode" /> parameter is not one of the <see cref="T:System.Net.Sockets.SelectMode" /> values. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E1B RID: 11803 RVA: 0x000A34D0 File Offset: 0x000A16D0
		public bool Poll(int microSeconds, SelectMode mode)
		{
			this.ThrowIfDisposedAndClosed();
			if (mode != SelectMode.SelectRead && mode != SelectMode.SelectWrite && mode != SelectMode.SelectError)
			{
				throw new NotSupportedException("'mode' parameter is not valid.");
			}
			int num;
			bool flag = Socket.Poll_internal(this.m_Handle, mode, microSeconds, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (mode == SelectMode.SelectWrite && flag && !this.is_connected && (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
			{
				this.is_connected = true;
			}
			return flag;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000A3544 File Offset: 0x000A1744
		private static bool Poll_internal(SafeSocketHandle safeHandle, SelectMode mode, int timeout, out int error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = Socket.Poll_icall(safeHandle.DangerousGetHandle(), mode, timeout, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06002E1D RID: 11805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Poll_icall(IntPtr socket, SelectMode mode, int timeout, out int error);

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> for a newly created connection.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.Accept" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E1E RID: 11806 RVA: 0x000A3588 File Offset: 0x000A1788
		public Socket Accept()
		{
			this.ThrowIfDisposedAndClosed();
			int num = 0;
			SafeSocketHandle safeSocketHandle = Socket.Accept_internal(this.m_Handle, out num, this.is_blocking);
			if (num != 0)
			{
				if (this.is_closed)
				{
					num = 10004;
				}
				throw new SocketException(num);
			}
			return new Socket(this.AddressFamily, this.SocketType, this.ProtocolType, safeSocketHandle)
			{
				seed_endpoint = this.seed_endpoint,
				Blocking = this.Blocking
			};
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000A35FC File Offset: 0x000A17FC
		internal void Accept(Socket acceptSocket)
		{
			this.ThrowIfDisposedAndClosed();
			int num = 0;
			SafeSocketHandle safeSocketHandle = Socket.Accept_internal(this.m_Handle, out num, this.is_blocking);
			if (num != 0)
			{
				if (this.is_closed)
				{
					num = 10004;
				}
				throw new SocketException(num);
			}
			acceptSocket.addressFamily = this.AddressFamily;
			acceptSocket.socketType = this.SocketType;
			acceptSocket.protocolType = this.ProtocolType;
			acceptSocket.m_Handle = safeSocketHandle;
			acceptSocket.is_connected = true;
			acceptSocket.seed_endpoint = this.seed_endpoint;
			acceptSocket.Blocking = this.Blocking;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation.Returns false if the I/O operation completed synchronously. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if the buffer provided is not large enough. The buffer must be at least 2 * (sizeof(SOCKADDR_STORAGE + 16) bytes. This exception also occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument is out of range. The exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Count" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was requested. This exception occurs if the accepting <see cref="T:System.Net.Sockets.Socket" /> is not listening for connections or the accepted socket is bound. You must call the <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> method before calling the <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.This exception also occurs if the socket is already connected or a socket operation was already in progress using the specified <paramref name="e" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E20 RID: 11808 RVA: 0x000A3688 File Offset: 0x000A1888
		public bool AcceptAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_bound)
			{
				throw new InvalidOperationException("You must call the Bind method before performing this operation.");
			}
			if (!this.is_listening)
			{
				throw new InvalidOperationException("You must call the Listen method before performing this operation.");
			}
			if (e.BufferList != null)
			{
				throw new ArgumentException("Multiple buffers cannot be used with this method.");
			}
			if (e.Count < 0)
			{
				throw new ArgumentOutOfRangeException("e.Count");
			}
			Socket acceptSocket = e.AcceptSocket;
			if (acceptSocket != null && (acceptSocket.is_bound || acceptSocket.is_connected))
			{
				throw new InvalidOperationException("AcceptSocket: The socket must not be bound or connected.");
			}
			this.InitSocketAsyncEventArgs(e, Socket.AcceptAsyncCallback, e, SocketOperation.Accept);
			this.QueueIOSelectorJob(this.ReadSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginAcceptCallback, e.socket_async_result));
			return true;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> creation.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E21 RID: 11809 RVA: 0x000A3744 File Offset: 0x000A1944
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_bound || !this.is_listening)
			{
				throw new InvalidOperationException();
			}
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.Accept);
			this.QueueIOSelectorJob(this.ReadSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginAcceptCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt from a specified socket and receives the first block of data sent by the client application.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous <see cref="T:System.Net.Sockets.Socket" /> object creation.</returns>
		/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be null. </param>
		/// <param name="receiveSize">The maximum number of bytes to receive. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The accepting socket is not listening for connections. You must call <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> and <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" /> before calling <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />.-or- The accepted socket is bound. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="receiveSize" /> is less than 0. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E22 RID: 11810 RVA: 0x000A3798 File Offset: 0x000A1998
		public IAsyncResult BeginAccept(Socket acceptSocket, int receiveSize, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (receiveSize < 0)
			{
				throw new ArgumentOutOfRangeException("receiveSize", "receiveSize is less than zero");
			}
			if (acceptSocket != null)
			{
				this.ThrowIfDisposedAndClosed(acceptSocket);
				if (acceptSocket.IsBound)
				{
					throw new InvalidOperationException();
				}
				if (acceptSocket.ProtocolType != ProtocolType.Tcp)
				{
					throw new SocketException(10022);
				}
			}
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.AcceptReceive)
			{
				Buffer = new byte[receiveSize],
				Offset = 0,
				Size = receiveSize,
				SockFlags = SocketFlags.None,
				AcceptSocket = acceptSocket
			};
			this.QueueIOSelectorJob(this.ReadSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginAcceptReceiveCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E23 RID: 11811 RVA: 0x000A3844 File Offset: 0x000A1A44
		public Socket EndAccept(IAsyncResult asyncResult)
		{
			byte[] array;
			int num;
			return this.EndAccept(out array, out num, asyncResult);
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> object to handle remote host communication. This method returns a buffer that contains the initial data and the number of bytes transferred.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> object to handle communication with the remote host.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the bytes transferred. </param>
		/// <param name="bytesTransferred">The number of bytes transferred. </param>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation as well as any user defined data. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndAccept(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E24 RID: 11812 RVA: 0x000A385C File Offset: 0x000A1A5C
		public Socket EndAccept(out byte[] buffer, out int bytesTransferred, IAsyncResult asyncResult)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndAccept", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			buffer = socketAsyncResult.Buffer.ToArray();
			bytesTransferred = socketAsyncResult.Total;
			return socketAsyncResult.AcceptedSocket;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000A38B8 File Offset: 0x000A1AB8
		private static SafeSocketHandle Accept_internal(SafeSocketHandle safeHandle, out int error, bool blocking)
		{
			SafeSocketHandle safeSocketHandle;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				safeSocketHandle = new SafeSocketHandle(Socket.Accept_icall(safeHandle.DangerousGetHandle(), out error, blocking), true);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return safeSocketHandle;
		}

		// Token: 0x06002E26 RID: 11814
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Accept_icall(IntPtr sock, out int error, bool blocking);

		/// <summary>Associates a <see cref="T:System.Net.Sockets.Socket" /> with a local endpoint.</summary>
		/// <param name="localEP">The local <see cref="T:System.Net.EndPoint" /> to associate with the <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E27 RID: 11815 RVA: 0x000A38FC File Offset: 0x000A1AFC
		public void Bind(EndPoint localEP)
		{
			this.ThrowIfDisposedAndClosed();
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			IPEndPoint ipendPoint = localEP as IPEndPoint;
			if (ipendPoint != null)
			{
				localEP = this.RemapIPEndPoint(ipendPoint);
			}
			int num;
			Socket.Bind_internal(this.m_Handle, localEP.Serialize(), out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (num == 0)
			{
				this.is_bound = true;
			}
			this.seed_endpoint = localEP;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000A3960 File Offset: 0x000A1B60
		private static void Bind_internal(SafeSocketHandle safeHandle, SocketAddress sa, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.Bind_icall(safeHandle.DangerousGetHandle(), sa, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E29 RID: 11817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Bind_icall(IntPtr sock, SocketAddress sa, out int error);

		/// <summary>Places a <see cref="T:System.Net.Sockets.Socket" /> in a listening state.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000A39A0 File Offset: 0x000A1BA0
		public void Listen(int backlog)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_bound)
			{
				throw new SocketException(10022);
			}
			int num;
			Socket.Listen_internal(this.m_Handle, backlog, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.is_listening = true;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000A39E8 File Offset: 0x000A1BE8
		private static void Listen_internal(SafeSocketHandle safeHandle, int backlog, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.Listen_icall(safeHandle.DangerousGetHandle(), backlog, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E2C RID: 11820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Listen_icall(IntPtr sock, int backlog, out int error);

		/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
		/// <param name="address">The IP address of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000A3A28 File Offset: 0x000A1C28
		public void Connect(IPAddress address, int port)
		{
			this.Connect(new IPEndPoint(address, port));
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E2E RID: 11822 RVA: 0x000A3A37 File Offset: 0x000A1C37
		public void Connect(string host, int port)
		{
			this.Connect(Dns.GetHostAddresses(host), port);
		}

		/// <summary>Establishes a connection to a remote host.</summary>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E2F RID: 11823 RVA: 0x000A3A48 File Offset: 0x000A1C48
		public void Connect(EndPoint remoteEP)
		{
			this.ThrowIfDisposedAndClosed();
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null && this.socketType != SocketType.Dgram && (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any)))
			{
				throw new SocketException(10049);
			}
			if (this.is_listening)
			{
				throw new InvalidOperationException();
			}
			if (ipendPoint != null)
			{
				remoteEP = this.RemapIPEndPoint(ipendPoint);
			}
			SocketAddress socketAddress = remoteEP.Serialize();
			int num = 0;
			Socket.Connect_internal(this.m_Handle, socketAddress, out num, this.is_blocking);
			if (num == 0 || num == 10035)
			{
				this.seed_endpoint = remoteEP;
			}
			if (num != 0)
			{
				if (this.is_closed)
				{
					num = 10004;
				}
				throw new SocketException(num);
			}
			this.is_connected = this.socketType != SocketType.Dgram || ipendPoint == null || (!ipendPoint.Address.Equals(IPAddress.Any) && !ipendPoint.Address.Equals(IPAddress.IPv6Any));
			this.is_bound = true;
		}

		/// <summary>Begins an asynchronous request for a connection to a remote host.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation. </returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument is not valid. This exception occurs if multiple buffers are specified, the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is not null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is listening or a socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the local endpoint and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> are not the same address family.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x06002E30 RID: 11824 RVA: 0x000A3B50 File Offset: 0x000A1D50
		public bool ConnectAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (this.is_listening)
			{
				throw new InvalidOperationException("You may not perform this operation after calling the Listen method.");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			this.InitSocketAsyncEventArgs(e, null, e, SocketOperation.Connect);
			bool flag2;
			try
			{
				IPAddress[] array;
				SocketAsyncResult socketAsyncResult;
				bool flag;
				if (!this.GetCheckedIPs(e, out array))
				{
					socketAsyncResult = new SocketAsyncResult(this, Socket.ConnectAsyncCallback, e, SocketOperation.Connect)
					{
						EndPoint = e.RemoteEndPoint
					};
					flag = Socket.BeginSConnect(socketAsyncResult);
				}
				else
				{
					DnsEndPoint dnsEndPoint = (DnsEndPoint)e.RemoteEndPoint;
					if (array == null)
					{
						throw new ArgumentNullException("addresses");
					}
					if (array.Length == 0)
					{
						throw new ArgumentException("Empty addresses list");
					}
					if (this.AddressFamily != AddressFamily.InterNetwork && this.AddressFamily != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This method is only valid for addresses in the InterNetwork or InterNetworkV6 families");
					}
					if (dnsEndPoint.Port <= 0 || dnsEndPoint.Port > 65535)
					{
						throw new ArgumentOutOfRangeException("port", "Must be > 0 and < 65536");
					}
					socketAsyncResult = new SocketAsyncResult(this, Socket.ConnectAsyncCallback, e, SocketOperation.Connect)
					{
						Addresses = array,
						Port = dnsEndPoint.Port
					};
					this.is_connected = false;
					flag = Socket.BeginMConnect(socketAsyncResult);
				}
				if (!flag)
				{
					e.CurrentSocket.EndConnect(socketAsyncResult);
				}
				flag2 = flag;
			}
			catch (SocketException ex)
			{
				e.SocketError = ex.SocketErrorCode;
				e.socket_async_result.Complete(ex, true);
				flag2 = false;
			}
			catch (Exception ex2)
			{
				e.socket_async_result.Complete(ex2, true);
				flag2 = false;
			}
			return flag2;
		}

		/// <summary>Cancels an asynchronous request for a remote host connection.</summary>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object used to request the connection to the remote host by calling one of the <see cref="M:System.Net.Sockets.Socket.ConnectAsync(System.Net.Sockets.SocketType,System.Net.Sockets.ProtocolType,System.Net.Sockets.SocketAsyncEventArgs)" /> methods.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation.</exception>
		// Token: 0x06002E31 RID: 11825 RVA: 0x000A3CCC File Offset: 0x000A1ECC
		public static void CancelConnectAsync(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.in_progress != 0 && e.LastOperation == SocketAsyncOperation.Connect)
			{
				Socket currentSocket = e.CurrentSocket;
				if (currentSocket == null)
				{
					return;
				}
				currentSocket.Close();
			}
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by a host name and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets in the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> families.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E32 RID: 11826 RVA: 0x000A3D00 File Offset: 0x000A1F00
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (this.addressFamily != AddressFamily.InterNetwork && this.addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException("This method is valid only for sockets in the InterNetwork and InterNetworkV6 families");
			}
			if (port <= 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port", "Must be > 0 and < 65536");
			}
			if (this.is_listening)
			{
				throw new InvalidOperationException();
			}
			SocketAsyncResult sockares = new SocketAsyncResult(this, callback, state, SocketOperation.Connect)
			{
				Port = port
			};
			Dns.GetHostAddressesAsync(host).ContinueWith(delegate(Task<IPAddress[]> t)
			{
				if (t.IsFaulted)
				{
					sockares.Complete(t.Exception.InnerException);
					return;
				}
				if (t.IsCanceled)
				{
					sockares.Complete(new OperationCanceledException());
					return;
				}
				sockares.Addresses = t.Result;
				Socket.BeginMConnect(sockares);
			}, TaskScheduler.Default);
			return sockares;
		}

		/// <summary>Begins an asynchronous request for a remote host connection.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connection.</returns>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote host. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000A3DA9 File Offset: 0x000A1FA9
		public IAsyncResult BeginConnect(EndPoint remoteEP, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			if (this.is_listening)
			{
				throw new InvalidOperationException();
			}
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.Connect);
			socketAsyncResult.EndPoint = remoteEP;
			Socket.BeginSConnect(socketAsyncResult);
			return socketAsyncResult;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The host is specified by an <see cref="T:System.Net.IPAddress" /> array and a port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous connections.</returns>
		/// <param name="addresses">At least one <see cref="T:System.Net.IPAddress" />, designating the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the connect operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addresses" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is valid for sockets that use <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="address" /> is zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> is <see cref="M:System.Net.Sockets.Socket.Listen(System.Int32)" />ing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E34 RID: 11828 RVA: 0x000A3DE4 File Offset: 0x000A1FE4
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses.Length == 0)
			{
				throw new ArgumentException("Empty addresses list");
			}
			if (this.AddressFamily != AddressFamily.InterNetwork && this.AddressFamily != AddressFamily.InterNetworkV6)
			{
				throw new NotSupportedException("This method is only valid for addresses in the InterNetwork or InterNetworkV6 families");
			}
			if (port <= 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port", "Must be > 0 and < 65536");
			}
			if (this.is_listening)
			{
				throw new InvalidOperationException();
			}
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, requestCallback, state, SocketOperation.Connect);
			socketAsyncResult.Addresses = addresses;
			socketAsyncResult.Port = port;
			this.is_connected = false;
			Socket.BeginMConnect(socketAsyncResult);
			return socketAsyncResult;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000A3E84 File Offset: 0x000A2084
		private static bool BeginMConnect(SocketAsyncResult sockares)
		{
			Exception ex = null;
			for (int i = sockares.CurrentAddress; i < sockares.Addresses.Length; i++)
			{
				try
				{
					sockares.CurrentAddress++;
					sockares.EndPoint = new IPEndPoint(sockares.Addresses[i], sockares.Port);
					if (sockares.socket.CanTryAddressFamily(sockares.EndPoint.AddressFamily))
					{
						return Socket.BeginSConnect(sockares);
					}
				}
				catch (Exception ex)
				{
				}
			}
			sockares.Complete(ex, true);
			return false;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000A3F14 File Offset: 0x000A2114
		private static bool BeginSConnect(SocketAsyncResult sockares)
		{
			EndPoint endPoint = sockares.EndPoint;
			if (endPoint is IPEndPoint)
			{
				IPEndPoint ipendPoint = (IPEndPoint)endPoint;
				if (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any))
				{
					sockares.Complete(new SocketException(10049), true);
					return false;
				}
				endPoint = (sockares.EndPoint = sockares.socket.RemapIPEndPoint(ipendPoint));
			}
			if (!sockares.socket.CanTryAddressFamily(sockares.EndPoint.AddressFamily))
			{
				sockares.Complete(new ArgumentException("None of the discovered or specified addresses match the socket address family."), true);
				return false;
			}
			int num = 0;
			if (sockares.socket.connect_in_progress)
			{
				sockares.socket.connect_in_progress = false;
				sockares.socket.m_Handle.Dispose();
				sockares.socket.m_Handle = new SafeSocketHandle(Socket.Socket_icall(sockares.socket.addressFamily, sockares.socket.socketType, sockares.socket.protocolType, out num), true);
				if (num != 0)
				{
					sockares.Complete(new SocketException(num), true);
					return false;
				}
			}
			bool flag = sockares.socket.is_blocking;
			if (flag)
			{
				sockares.socket.Blocking = false;
			}
			Socket.Connect_internal(sockares.socket.m_Handle, endPoint.Serialize(), out num, false);
			if (flag)
			{
				sockares.socket.Blocking = true;
			}
			if (num == 0)
			{
				sockares.socket.is_connected = true;
				sockares.socket.is_bound = true;
				sockares.Complete(true);
				return false;
			}
			if (num != 10036 && num != 10035)
			{
				sockares.socket.is_connected = false;
				sockares.socket.is_bound = false;
				sockares.Complete(new SocketException(num), true);
				return false;
			}
			sockares.socket.is_connected = false;
			sockares.socket.is_bound = false;
			sockares.socket.connect_in_progress = true;
			IOSelector.Add(sockares.Handle, new IOSelectorJob(IOOperation.Write, Socket.BeginConnectCallback, sockares));
			return true;
		}

		/// <summary>Ends a pending asynchronous connection request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginConnect(System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndConnect(System.IAsyncResult)" /> was previously called for the asynchronous connection. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E37 RID: 11831 RVA: 0x000A40FC File Offset: 0x000A22FC
		public void EndConnect(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndConnect", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000A413C File Offset: 0x000A233C
		private static void Connect_internal(SafeSocketHandle safeHandle, SocketAddress sa, out int error, bool blocking)
		{
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				Socket.Connect_icall(safeHandle.DangerousGetHandle(), sa, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
		}

		// Token: 0x06002E39 RID: 11833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Connect_icall(IntPtr sock, SocketAddress sa, out int error, bool blocking);

		// Token: 0x06002E3A RID: 11834 RVA: 0x000A4178 File Offset: 0x000A2378
		private bool GetCheckedIPs(SocketAsyncEventArgs e, out IPAddress[] addresses)
		{
			addresses = null;
			DnsEndPoint dnsEndPoint = e.RemoteEndPoint as DnsEndPoint;
			if (dnsEndPoint == null)
			{
				e.SetConnectByNameError(null);
				return false;
			}
			addresses = Dns.GetHostAddresses(dnsEndPoint.Host);
			if (dnsEndPoint.AddressFamily == AddressFamily.Unspecified)
			{
				return true;
			}
			int num = 0;
			for (int i = 0; i < addresses.Length; i++)
			{
				if (addresses[i].AddressFamily == dnsEndPoint.AddressFamily)
				{
					addresses[num++] = addresses[i];
				}
			}
			if (num != addresses.Length)
			{
				Array.Resize<IPAddress>(ref addresses, num);
			}
			return true;
		}

		/// <summary>Closes the socket connection and allows reuse of the socket.</summary>
		/// <param name="reuseSocket">true if this socket can be reused after the current connection is closed; otherwise, false. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">This method requires Windows 2000 or earlier, or the exception will be thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E3B RID: 11835 RVA: 0x000A41F4 File Offset: 0x000A23F4
		public void Disconnect(bool reuseSocket)
		{
			this.ThrowIfDisposedAndClosed();
			int num = 0;
			Socket.Disconnect_internal(this.m_Handle, reuseSocket, out num);
			if (num == 0)
			{
				this.is_connected = false;
				return;
			}
			if (num == 50)
			{
				throw new PlatformNotSupportedException();
			}
			throw new SocketException(num);
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x06002E3C RID: 11836 RVA: 0x000A4235 File Offset: 0x000A2435
		public bool DisconnectAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			this.InitSocketAsyncEventArgs(e, Socket.DisconnectAsyncCallback, e, SocketOperation.Disconnect);
			IOSelector.Add(e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Write, Socket.BeginDisconnectCallback, e.socket_async_result));
			return true;
		}

		/// <summary>Begins an asynchronous request to disconnect from a remote endpoint.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous operation.</returns>
		/// <param name="reuseSocket">true if this socket can be reused after the connection is closed; otherwise, false. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E3D RID: 11837 RVA: 0x000A4270 File Offset: 0x000A2470
		public IAsyncResult BeginDisconnect(bool reuseSocket, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.Disconnect)
			{
				ReuseSocket = reuseSocket
			};
			IOSelector.Add(socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Write, Socket.BeginDisconnectCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Ends a pending asynchronous disconnect request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information and any user-defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginDisconnect(System.Boolean,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndDisconnect(System.IAsyncResult)" /> was previously called for the asynchronous connection. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.Net.WebException">The disconnect request has timed out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000A42AC File Offset: 0x000A24AC
		public void EndDisconnect(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndDisconnect", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000A42EC File Offset: 0x000A24EC
		private static void Disconnect_internal(SafeSocketHandle safeHandle, bool reuse, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.Disconnect_icall(safeHandle.DangerousGetHandle(), reuse, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E40 RID: 11840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Disconnect_icall(IntPtr sock, bool reuse, out int error);

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into a receive buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property is not set.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		// Token: 0x06002E41 RID: 11841 RVA: 0x000A432C File Offset: 0x000A252C
		public unsafe int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			int num2;
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = Socket.Receive_internal(this.m_Handle, ptr + offset, size, socketFlags, out num2, this.is_blocking);
			}
			errorCode = (SocketError)num2;
			if (errorCode != SocketError.Success && errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
			{
				this.is_connected = false;
				this.is_bound = false;
				return num;
			}
			this.is_connected = true;
			return num;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000A43B8 File Offset: 0x000A25B8
		private unsafe int Receive(Memory<byte> buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			int num2;
			int num;
			using (MemoryHandle memoryHandle = buffer.Slice(offset, size).Pin())
			{
				num = Socket.Receive_internal(this.m_Handle, (byte*)memoryHandle.Pointer, size, socketFlags, out num2, this.is_blocking);
			}
			errorCode = (SocketError)num2;
			if (errorCode != SocketError.Success && errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
			{
				this.is_connected = false;
				this.is_bound = false;
			}
			else
			{
				this.is_connected = true;
			}
			return num;
		}

		/// <summary>Receives data from a bound <see cref="T:System.Net.Sockets.Socket" /> into the list of receive buffers, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.-or-<paramref name="buffers" />.Count is zero.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000A4450 File Offset: 0x000A2650
		[CLSCompliant(false)]
		public unsafe int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			if (buffers == null || buffers.Count == 0)
			{
				throw new ArgumentNullException("buffers");
			}
			int count = buffers.Count;
			GCHandle[] array = new GCHandle[count];
			int num2;
			int num;
			try
			{
				try
				{
					Socket.WSABUF[] array2;
					Socket.WSABUF* ptr;
					if ((array2 = new Socket.WSABUF[count]) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					for (int i = 0; i < count; i++)
					{
						ArraySegment<byte> arraySegment = buffers[i];
						if (arraySegment.Offset < 0 || arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
						{
							throw new ArgumentOutOfRangeException("segment");
						}
						try
						{
						}
						finally
						{
							array[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
						}
						ptr[i].len = arraySegment.Count;
						ptr[i].buf = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(arraySegment.Array, arraySegment.Offset);
					}
					num = Socket.Receive_internal(this.m_Handle, ptr, count, socketFlags, out num2, this.is_blocking);
				}
				finally
				{
					Socket.WSABUF[] array2 = null;
				}
			}
			finally
			{
				for (int j = 0; j < count; j++)
				{
					if (array[j].IsAllocated)
					{
						array[j].Free();
					}
				}
			}
			errorCode = (SocketError)num2;
			return num;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000A45D0 File Offset: 0x000A27D0
		public int Receive(Span<byte> buffer, SocketFlags socketFlags, out SocketError errorCode)
		{
			byte[] array = new byte[buffer.Length];
			int num = this.Receive(array, 0, array.Length, socketFlags, out errorCode);
			array.CopyTo(buffer);
			return num;
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000A4600 File Offset: 0x000A2800
		public int Send(ReadOnlySpan<byte> buffer, SocketFlags socketFlags, out SocketError errorCode)
		{
			byte[] array = buffer.ToArray();
			return this.Send(array, 0, array.Length, socketFlags, out errorCode);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000A4624 File Offset: 0x000A2824
		public int Receive(Span<byte> buffer, SocketFlags socketFlags)
		{
			byte[] array = new byte[buffer.Length];
			int num = this.Receive(array, SocketFlags.None);
			array.CopyTo(buffer);
			return num;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000A464D File Offset: 0x000A284D
		public int Receive(Span<byte> buffer)
		{
			return this.Receive(buffer, SocketFlags.None);
		}

		/// <summary>Begins an asynchronous request to receive data from a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">An argument was invalid. The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002E48 RID: 11848 RVA: 0x000A4658 File Offset: 0x000A2858
		public bool ReceiveAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (e.MemoryBuffer.Equals(default(Memory<byte>)) && e.BufferList == null)
			{
				throw new NullReferenceException("Either e.Buffer or e.BufferList must be valid buffers.");
			}
			if (e.BufferList != null)
			{
				this.InitSocketAsyncEventArgs(e, Socket.ReceiveAsyncCallback, e, SocketOperation.ReceiveGeneric);
				e.socket_async_result.Buffers = e.BufferList;
				this.QueueIOSelectorJob(this.ReadSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveGenericCallback, e.socket_async_result));
			}
			else
			{
				this.InitSocketAsyncEventArgs(e, Socket.ReceiveAsyncCallback, e, SocketOperation.Receive);
				e.socket_async_result.Buffer = e.MemoryBuffer;
				e.socket_async_result.Offset = e.Offset;
				e.socket_async_result.Size = e.Count;
				this.QueueIOSelectorJob(this.ReadSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveCallback, e.socket_async_result));
			}
			return true;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		// Token: 0x06002E49 RID: 11849 RVA: 0x000A4754 File Offset: 0x000A2954
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			errorCode = SocketError.Success;
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.Receive)
			{
				Buffer = buffer,
				Offset = offset,
				Size = size,
				SockFlags = socketFlags
			};
			this.QueueIOSelectorJob(this.ReadSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Begins to asynchronously receive data from a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000A47C8 File Offset: 0x000A29C8
		[CLSCompliant(false)]
		public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			errorCode = SocketError.Success;
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.ReceiveGeneric)
			{
				Buffers = buffers,
				SockFlags = socketFlags
			};
			this.QueueIOSelectorJob(this.ReadSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveGenericCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Ends a pending asynchronous read.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000A4828 File Offset: 0x000A2A28
		public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndReceive", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			if (errorCode != SocketError.Success && errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
			{
				this.is_connected = false;
			}
			if (errorCode == SocketError.Success)
			{
				socketAsyncResult.CheckIfThrowDelayedException();
			}
			return socketAsyncResult.Total;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000A4898 File Offset: 0x000A2A98
		private unsafe static int Receive_internal(SafeSocketHandle safeHandle, Socket.WSABUF* bufarray, int count, SocketFlags flags, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.Receive_array_icall(safeHandle.DangerousGetHandle(), bufarray, count, flags, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E4D RID: 11853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int Receive_array_icall(IntPtr sock, Socket.WSABUF* bufarray, int count, SocketFlags flags, out int error, bool blocking);

		// Token: 0x06002E4E RID: 11854 RVA: 0x000A48D8 File Offset: 0x000A2AD8
		private unsafe static int Receive_internal(SafeSocketHandle safeHandle, byte* buffer, int count, SocketFlags flags, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.Receive_icall(safeHandle.DangerousGetHandle(), buffer, count, flags, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E4F RID: 11855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int Receive_icall(IntPtr sock, byte* buffer, int count, SocketFlags flags, out int error, bool blocking);

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data. </param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E50 RID: 11856 RVA: 0x000A4918 File Offset: 0x000A2B18
		public int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			SocketError socketError;
			int num = this.ReceiveFrom(buffer, offset, size, socketFlags, ref remoteEP, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException(socketError);
			}
			return num;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000A4964 File Offset: 0x000A2B64
		internal unsafe int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, out SocketError errorCode)
		{
			SocketAddress socketAddress = remoteEP.Serialize();
			int num2;
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = Socket.ReceiveFrom_internal(this.m_Handle, ptr + offset, size, socketFlags, ref socketAddress, out num2, this.is_blocking);
			}
			errorCode = (SocketError)num2;
			if (errorCode != SocketError.Success)
			{
				if (errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
				{
					this.is_connected = false;
				}
				else if (errorCode == SocketError.WouldBlock && this.is_blocking)
				{
					errorCode = SocketError.TimedOut;
				}
				return 0;
			}
			this.is_connected = true;
			this.is_bound = true;
			if (socketAddress != null)
			{
				remoteEP = remoteEP.Create(socketAddress);
			}
			this.seed_endpoint = remoteEP;
			return num;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000A4A20 File Offset: 0x000A2C20
		private unsafe int ReceiveFrom(Memory<byte> buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, out SocketError errorCode)
		{
			SocketAddress socketAddress = remoteEP.Serialize();
			int num2;
			int num;
			using (MemoryHandle memoryHandle = buffer.Slice(offset, size).Pin())
			{
				num = Socket.ReceiveFrom_internal(this.m_Handle, (byte*)memoryHandle.Pointer, size, socketFlags, ref socketAddress, out num2, this.is_blocking);
			}
			errorCode = (SocketError)num2;
			if (errorCode != SocketError.Success)
			{
				if (errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
				{
					this.is_connected = false;
				}
				else if (errorCode == SocketError.WouldBlock && this.is_blocking)
				{
					errorCode = SocketError.TimedOut;
				}
				return 0;
			}
			this.is_connected = true;
			this.is_bound = true;
			if (socketAddress != null)
			{
				remoteEP = remoteEP.Create(socketAddress);
			}
			this.seed_endpoint = remoteEP;
			return num;
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x06002E53 RID: 11859 RVA: 0x000A4AF4 File Offset: 0x000A2CF4
		public bool ReceiveFromAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (e.BufferList != null)
			{
				throw new NotSupportedException("Mono doesn't support using BufferList at this point.");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP", "Value cannot be null.");
			}
			this.InitSocketAsyncEventArgs(e, Socket.ReceiveFromAsyncCallback, e, SocketOperation.ReceiveFrom);
			e.socket_async_result.Buffer = e.Buffer;
			e.socket_async_result.Offset = e.Offset;
			e.socket_async_result.Size = e.Count;
			e.socket_async_result.EndPoint = e.RemoteEndPoint;
			e.socket_async_result.SockFlags = e.SocketFlags;
			this.QueueIOSelectorJob(this.ReadSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveFromCallback, e.socket_async_result));
			return true;
		}

		/// <summary>Begins to asynchronously receive data from a specified network device.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data. </param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E54 RID: 11860 RVA: 0x000A4BC4 File Offset: 0x000A2DC4
		public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.ReceiveFrom)
			{
				Buffer = buffer,
				Offset = offset,
				Size = size,
				SockFlags = socketFlags,
				EndPoint = remoteEP
			};
			this.QueueIOSelectorJob(this.ReadSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Read, Socket.BeginReceiveFromCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E55 RID: 11861 RVA: 0x000A4C50 File Offset: 0x000A2E50
		public int EndReceiveFrom(IAsyncResult asyncResult, ref EndPoint endPoint)
		{
			this.ThrowIfDisposedAndClosed();
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndReceiveFrom", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			endPoint = socketAsyncResult.EndPoint;
			return socketAsyncResult.Total;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000A4CAC File Offset: 0x000A2EAC
		private int EndReceiveFrom_internal(SocketAsyncResult sockares, SocketAsyncEventArgs ares)
		{
			this.ThrowIfDisposedAndClosed();
			if (Interlocked.CompareExchange(ref sockares.EndCalled, 1, 0) == 1)
			{
				throw new InvalidOperationException("EndReceiveFrom can only be called once per asynchronous operation");
			}
			if (!sockares.IsCompleted)
			{
				sockares.AsyncWaitHandle.WaitOne();
			}
			sockares.CheckIfThrowDelayedException();
			ares.RemoteEndPoint = sockares.EndPoint;
			return sockares.Total;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000A4D08 File Offset: 0x000A2F08
		private unsafe static int ReceiveFrom_internal(SafeSocketHandle safeHandle, byte* buffer, int count, SocketFlags flags, ref SocketAddress sockaddr, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.ReceiveFrom_icall(safeHandle.DangerousGetHandle(), buffer, count, flags, ref sockaddr, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E58 RID: 11864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int ReceiveFrom_icall(IntPtr sock, byte* buffer, int count, SocketFlags flags, ref SocketAddress sockaddr, out int error, bool blocking);

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <returns>The number of bytes received.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for received data.</param>
		/// <param name="offset">The position in the <paramref name="buffer" /> parameter to store the received data.</param>
		/// <param name="size">The number of bytes to receive.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" />, passed by reference, that represents the remote server.</param>
		/// <param name="ipPacketInformation">An <see cref="T:System.Net.Sockets.IPPacketInformation" /> holding address and interface information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.- or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of the <paramref name="buffer" /> minus the value of the offset parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- The <see cref="P:System.Net.Sockets.Socket.LocalEndPoint" /> property was not set.-or- The .NET Framework is running on an AMD 64-bit processor.-or- An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x06002E59 RID: 11865 RVA: 0x000A4D4C File Offset: 0x000A2F4C
		[MonoTODO("Not implemented")]
		public int ReceiveMessageFrom(byte[] buffer, int offset, int size, ref SocketFlags socketFlags, ref EndPoint remoteEP, out IPPacketInformation ipPacketInformation)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			throw new NotImplementedException();
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location in the data buffer, using the specified <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. </exception>
		// Token: 0x06002E5A RID: 11866 RVA: 0x000A4D79 File Offset: 0x000A2F79
		[MonoTODO("Not implemented")]
		public bool ReceiveMessageFromAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			throw new NotImplementedException();
		}

		/// <summary>Begins to asynchronously receive the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information..</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous read.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the storage location for the received data. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to store the data.</param>
		/// <param name="size">The number of bytes to receive. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the source of the data.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows 2000 or earlier, and this method requires Windows XP.</exception>
		// Token: 0x06002E5B RID: 11867 RVA: 0x000A4D4C File Offset: 0x000A2F4C
		[MonoTODO]
		public IAsyncResult BeginReceiveMessageFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			throw new NotImplementedException();
		}

		/// <summary>Ends a pending asynchronous read from a specific endpoint. This method also reveals more information about the packet than <see cref="M:System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)" />.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values for the received packet.</param>
		/// <param name="endPoint">The source <see cref="T:System.Net.EndPoint" />.</param>
		/// <param name="ipPacketInformation">The <see cref="T:System.Net.IPAddress" /> and interface of the received packet.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null-or- <paramref name="endPoint" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E5C RID: 11868 RVA: 0x000A4D86 File Offset: 0x000A2F86
		[MonoTODO]
		public int EndReceiveMessageFrom(IAsyncResult asyncResult, ref SocketFlags socketFlags, ref EndPoint endPoint, out IPPacketInformation ipPacketInformation)
		{
			this.ThrowIfDisposedAndClosed();
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			this.ValidateEndIAsyncResult(asyncResult, "EndReceiveMessageFrom", "asyncResult");
			throw new NotImplementedException();
		}

		/// <summary>Sends the specified number of bytes of data to a connected <see cref="T:System.Net.Sockets.Socket" />, starting at the specified offset, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" /></summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E5D RID: 11869 RVA: 0x000A4DB4 File Offset: 0x000A2FB4
		public unsafe int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (size == 0)
			{
				errorCode = SocketError.Success;
				return 0;
			}
			int num = 0;
			for (;;)
			{
				int num2;
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num += Socket.Send_internal(this.m_Handle, ptr + (offset + num), size - num, socketFlags, out num2, this.is_blocking);
				}
				errorCode = (SocketError)num2;
				if (errorCode != SocketError.Success && errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
				{
					break;
				}
				this.is_connected = true;
				if (num >= size)
				{
					return num;
				}
			}
			this.is_connected = false;
			this.is_bound = false;
			return num;
		}

		/// <summary>Sends the set of buffers in the list to a connected <see cref="T:System.Net.Sockets.Socket" />, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="buffers">A list of <see cref="T:System.ArraySegment`1" />s of type <see cref="T:System.Byte" /> that contains the data to be sent.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E5E RID: 11870 RVA: 0x000A4E58 File Offset: 0x000A3058
		[CLSCompliant(false)]
		public unsafe int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (buffers.Count == 0)
			{
				throw new ArgumentException("Buffer is empty", "buffers");
			}
			int count = buffers.Count;
			GCHandle[] array = new GCHandle[count];
			int num2;
			int num;
			try
			{
				try
				{
					Socket.WSABUF[] array2;
					Socket.WSABUF* ptr;
					if ((array2 = new Socket.WSABUF[count]) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					for (int i = 0; i < count; i++)
					{
						ArraySegment<byte> arraySegment = buffers[i];
						if (arraySegment.Offset < 0 || arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
						{
							throw new ArgumentOutOfRangeException("segment");
						}
						try
						{
						}
						finally
						{
							array[i] = GCHandle.Alloc(arraySegment.Array, GCHandleType.Pinned);
						}
						ptr[i].len = arraySegment.Count;
						ptr[i].buf = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(arraySegment.Array, arraySegment.Offset);
					}
					num = Socket.Send_internal(this.m_Handle, ptr, count, socketFlags, out num2, this.is_blocking);
				}
				finally
				{
					Socket.WSABUF[] array2 = null;
				}
			}
			finally
			{
				for (int j = 0; j < count; j++)
				{
					if (array[j].IsAllocated)
					{
						array[j].Free();
					}
				}
			}
			errorCode = (SocketError)num2;
			return num;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000A4FE8 File Offset: 0x000A31E8
		public int Send(ReadOnlySpan<byte> buffer, SocketFlags socketFlags)
		{
			return this.Send(buffer.ToArray(), socketFlags);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000A4FF8 File Offset: 0x000A31F8
		public int Send(ReadOnlySpan<byte> buffer)
		{
			return this.Send(buffer, SocketFlags.None);
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> or <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> properties on the <paramref name="e" /> parameter must reference valid buffers. One or the other of these properties may be set, but not both at the same time.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The <see cref="T:System.Net.Sockets.Socket" /> is not yet connected or was not obtained via an <see cref="M:System.Net.Sockets.Socket.Accept" />, <see cref="M:System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)" />,or <see cref="Overload:System.Net.Sockets.Socket.BeginAccept" />, method.</exception>
		// Token: 0x06002E61 RID: 11873 RVA: 0x000A5004 File Offset: 0x000A3204
		public bool SendAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (e.MemoryBuffer.Equals(default(Memory<byte>)) && e.BufferList == null)
			{
				throw new NullReferenceException("Either e.Buffer or e.BufferList must be valid buffers.");
			}
			if (e.BufferList != null)
			{
				this.InitSocketAsyncEventArgs(e, Socket.SendAsyncCallback, e, SocketOperation.SendGeneric);
				e.socket_async_result.Buffers = e.BufferList;
				this.QueueIOSelectorJob(this.WriteSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Write, Socket.BeginSendGenericCallback, e.socket_async_result));
			}
			else
			{
				this.InitSocketAsyncEventArgs(e, Socket.SendAsyncCallback, e, SocketOperation.Send);
				e.socket_async_result.Buffer = e.MemoryBuffer;
				e.socket_async_result.Offset = e.Offset;
				e.socket_async_result.Size = e.Count;
				this.QueueIOSelectorJob(this.WriteSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
				{
					Socket.BeginSendCallback((SocketAsyncResult)s, 0);
				}, e.socket_async_result));
			}
			return true;
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in the <paramref name="buffer" /> parameter at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is less than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E62 RID: 11874 RVA: 0x000A511C File Offset: 0x000A331C
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (!this.is_connected)
			{
				errorCode = SocketError.NotConnected;
				return null;
			}
			errorCode = SocketError.Success;
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.Send)
			{
				Buffer = buffer,
				Offset = offset,
				Size = size,
				SockFlags = socketFlags
			};
			this.QueueIOSelectorJob(this.WriteSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
			{
				Socket.BeginSendCallback((SocketAsyncResult)s, 0);
			}, socketAsyncResult));
			return socketAsyncResult;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000A51BC File Offset: 0x000A33BC
		private unsafe static void BeginSendCallback(SocketAsyncResult sockares, int sent_so_far)
		{
			int num = 0;
			try
			{
				using (MemoryHandle memoryHandle = sockares.Buffer.Slice(sockares.Offset, sockares.Size).Pin())
				{
					num = Socket.Send_internal(sockares.socket.m_Handle, (byte*)memoryHandle.Pointer, sockares.Size, sockares.SockFlags, out sockares.error, false);
				}
			}
			catch (Exception ex)
			{
				sockares.Complete(ex);
				return;
			}
			if (sockares.error == 0)
			{
				sent_so_far += num;
				sockares.Offset += num;
				sockares.Size -= num;
				if (sockares.socket.CleanedUp)
				{
					sockares.Complete(sent_so_far);
					return;
				}
				if (sockares.Size > 0)
				{
					IOSelector.Add(sockares.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
					{
						Socket.BeginSendCallback((SocketAsyncResult)s, sent_so_far);
					}, sockares));
					return;
				}
				sockares.Total = sent_so_far;
			}
			sockares.Complete(sent_so_far);
		}

		/// <summary>Sends data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffers">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffers" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffers" /> is empty.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E64 RID: 11876 RVA: 0x000A52EC File Offset: 0x000A34EC
		[CLSCompliant(false)]
		public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			if (!this.is_connected)
			{
				errorCode = SocketError.NotConnected;
				return null;
			}
			errorCode = SocketError.Success;
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.SendGeneric)
			{
				Buffers = buffers,
				SockFlags = socketFlags
			};
			this.QueueIOSelectorJob(this.WriteSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Write, Socket.BeginSendGenericCallback, socketAsyncResult));
			return socketAsyncResult;
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.Socket" />; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information for this asynchronous operation.</param>
		/// <param name="errorCode">A <see cref="T:System.Net.Sockets.SocketError" /> object that stores the socket error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002E65 RID: 11877 RVA: 0x000A535C File Offset: 0x000A355C
		public int EndSend(IAsyncResult asyncResult, out SocketError errorCode)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndSend", "asyncResult");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			if (errorCode != SocketError.Success && errorCode != SocketError.WouldBlock && errorCode != SocketError.InProgress)
			{
				this.is_connected = false;
			}
			if (errorCode == SocketError.Success)
			{
				socketAsyncResult.CheckIfThrowDelayedException();
			}
			return socketAsyncResult.Total;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000A53CC File Offset: 0x000A35CC
		private unsafe static int Send_internal(SafeSocketHandle safeHandle, Socket.WSABUF* bufarray, int count, SocketFlags flags, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.Send_array_icall(safeHandle.DangerousGetHandle(), bufarray, count, flags, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E67 RID: 11879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int Send_array_icall(IntPtr sock, Socket.WSABUF* bufarray, int count, SocketFlags flags, out int error, bool blocking);

		// Token: 0x06002E68 RID: 11880 RVA: 0x000A540C File Offset: 0x000A360C
		private unsafe static int Send_internal(SafeSocketHandle safeHandle, byte* buffer, int count, SocketFlags flags, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.Send_icall(safeHandle.DangerousGetHandle(), buffer, count, flags, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E69 RID: 11881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int Send_icall(IntPtr sock, byte* buffer, int count, SocketFlags flags, out int error, bool blocking);

		/// <summary>Sends the specified number of bytes of data to the specified endpoint, starting at the specified location in the buffer, and using the specified <see cref="T:System.Net.Sockets.SocketFlags" />.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to be sent. </param>
		/// <param name="offset">The position in the data buffer at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">The <see cref="T:System.Net.EndPoint" /> that represents the destination location for the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="socketFlags" /> is not a valid combination of values.-or- An operating system error occurs while accessing the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E6A RID: 11882 RVA: 0x000A544C File Offset: 0x000A364C
		public unsafe int SendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			int num2;
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = Socket.SendTo_internal(this.m_Handle, ptr + offset, size, socketFlags, remoteEP.Serialize(), out num2, this.is_blocking);
			}
			SocketError socketError = (SocketError)num2;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.is_connected = false;
				}
				throw new SocketException(num2);
			}
			this.is_connected = true;
			this.is_bound = true;
			this.seed_endpoint = remoteEP;
			return num;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000A54F4 File Offset: 0x000A36F4
		private unsafe int SendTo(Memory<byte> buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP)
		{
			this.ThrowIfDisposedAndClosed();
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			int num2;
			int num;
			using (MemoryHandle memoryHandle = buffer.Slice(offset, size).Pin())
			{
				num = Socket.SendTo_internal(this.m_Handle, (byte*)memoryHandle.Pointer, size, socketFlags, remoteEP.Serialize(), out num2, this.is_blocking);
			}
			SocketError socketError = (SocketError)num2;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.is_connected = false;
				}
				throw new SocketException(num2);
			}
			this.is_connected = true;
			this.is_bound = true;
			this.seed_endpoint = remoteEP;
			return num;
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> cannot be null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The protocol specified is connection-oriented, but the <see cref="T:System.Net.Sockets.Socket" /> is not yet connected.</exception>
		// Token: 0x06002E6C RID: 11884 RVA: 0x000A55A8 File Offset: 0x000A37A8
		public bool SendToAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			if (e.BufferList != null)
			{
				throw new NotSupportedException("Mono doesn't support using BufferList at this point.");
			}
			if (e.RemoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEP", "Value cannot be null.");
			}
			this.InitSocketAsyncEventArgs(e, Socket.SendToAsyncCallback, e, SocketOperation.SendTo);
			e.socket_async_result.Buffer = e.Buffer;
			e.socket_async_result.Offset = e.Offset;
			e.socket_async_result.Size = e.Count;
			e.socket_async_result.SockFlags = e.SocketFlags;
			e.socket_async_result.EndPoint = e.RemoteEndPoint;
			this.QueueIOSelectorJob(this.WriteSem, e.socket_async_result.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
			{
				Socket.BeginSendToCallback((SocketAsyncResult)s, 0);
			}, e.socket_async_result));
			return true;
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous send.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to send. </param>
		/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin sending data. </param>
		/// <param name="size">The number of bytes to send. </param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values. </param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.-or- <paramref name="remoteEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or- <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.-or- <paramref name="size" /> is less than 0.-or- <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E6D RID: 11885 RVA: 0x000A5694 File Offset: 0x000A3894
		public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			this.ThrowIfBufferNull(buffer);
			this.ThrowIfBufferOutOfRange(buffer, offset, size);
			SocketAsyncResult socketAsyncResult = new SocketAsyncResult(this, callback, state, SocketOperation.SendTo)
			{
				Buffer = buffer,
				Offset = offset,
				Size = size,
				SockFlags = socketFlags,
				EndPoint = remoteEP
			};
			this.QueueIOSelectorJob(this.WriteSem, socketAsyncResult.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
			{
				Socket.BeginSendToCallback((SocketAsyncResult)s, 0);
			}, socketAsyncResult));
			return socketAsyncResult;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000A5728 File Offset: 0x000A3928
		private static void BeginSendToCallback(SocketAsyncResult sockares, int sent_so_far)
		{
			try
			{
				int num = sockares.socket.SendTo(sockares.Buffer, sockares.Offset, sockares.Size, sockares.SockFlags, sockares.EndPoint);
				if (sockares.error == 0)
				{
					sent_so_far += num;
					sockares.Offset += num;
					sockares.Size -= num;
				}
				if (sockares.Size > 0)
				{
					IOSelector.Add(sockares.Handle, new IOSelectorJob(IOOperation.Write, delegate(IOAsyncResult s)
					{
						Socket.BeginSendToCallback((SocketAsyncResult)s, sent_so_far);
					}, sockares));
					return;
				}
				sockares.Total = sent_so_far;
			}
			catch (Exception ex)
			{
				sockares.Complete(ex);
				return;
			}
			sockares.Complete();
		}

		/// <summary>Ends a pending asynchronous send to a specific location.</summary>
		/// <returns>If successful, the number of bytes sent; otherwise, an invalid <see cref="T:System.Net.Sockets.Socket" /> error.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendTo(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendTo(System.IAsyncResult)" /> was previously called for the asynchronous send. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E6F RID: 11887 RVA: 0x000A57F8 File Offset: 0x000A39F8
		public int EndSendTo(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposedAndClosed();
			SocketAsyncResult socketAsyncResult = this.ValidateEndIAsyncResult(asyncResult, "EndSendTo", "result");
			if (!socketAsyncResult.IsCompleted)
			{
				socketAsyncResult.AsyncWaitHandle.WaitOne();
			}
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000A5840 File Offset: 0x000A3A40
		private unsafe static int SendTo_internal(SafeSocketHandle safeHandle, byte* buffer, int count, SocketFlags flags, SocketAddress sa, out int error, bool blocking)
		{
			int num;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				num = Socket.SendTo_icall(safeHandle.DangerousGetHandle(), buffer, count, flags, sa, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return num;
		}

		// Token: 0x06002E71 RID: 11889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int SendTo_icall(IntPtr sock, byte* buffer, int count, SocketFlags flags, SocketAddress sa, out int error, bool blocking);

		/// <summary>Sends the file <paramref name="fileName" /> and buffers of data to a connected <see cref="T:System.Net.Sockets.Socket" /> object using the specified <see cref="T:System.Net.Sockets.TransmitFileOptions" /> value.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be null. </param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be null. </param>
		/// <param name="flags">One or more of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.- or - The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.Socket" /> object is not in blocking mode and cannot accept this synchronous call. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002E72 RID: 11890 RVA: 0x000A5884 File Offset: 0x000A3A84
		public void SendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_connected)
			{
				throw new NotSupportedException();
			}
			if (!this.is_blocking)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			if (Socket.SendFile_internal(this.m_Handle, fileName, preBuffer, postBuffer, flags, out num, this.is_blocking) && num == 0)
			{
				return;
			}
			SocketException ex = new SocketException(num);
			if (ex.ErrorCode == 2 || ex.ErrorCode == 3)
			{
				throw new FileNotFoundException();
			}
			throw ex;
		}

		/// <summary>Sends a file and buffers of data asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous operation.</returns>
		/// <param name="fileName">A string that contains the path and name of the file to be sent. This parameter can be null. </param>
		/// <param name="preBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent before the file is sent. This parameter can be null. </param>
		/// <param name="postBuffer">A <see cref="T:System.Byte" /> array that contains data to be sent after the file is sent. This parameter can be null. </param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values. </param>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate to be invoked when this operation completes. This parameter can be null. </param>
		/// <param name="state">A user-defined object that contains state information for this request. This parameter can be null. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is not Windows NT or later.- or - The socket is not connected to a remote host. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> was not found. </exception>
		// Token: 0x06002E73 RID: 11891 RVA: 0x000A58F4 File Offset: 0x000A3AF4
		public IAsyncResult BeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_connected)
			{
				throw new NotSupportedException();
			}
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}
			Socket.SendFileHandler handler = new Socket.SendFileHandler(this.SendFile);
			return new Socket.SendFileAsyncResult(handler, handler.BeginInvoke(fileName, preBuffer, postBuffer, flags, delegate(IAsyncResult ar)
			{
				callback(new Socket.SendFileAsyncResult(handler, ar));
			}, state));
		}

		/// <summary>Ends a pending asynchronous send of a file.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that stores state information for this asynchronous operation. </param>
		/// <exception cref="T:System.NotSupportedException">Windows NT is required for this method. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is empty. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSendFile(System.IAsyncResult)" /> was previously called for the asynchronous <see cref="M:System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See remarks section below. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E74 RID: 11892 RVA: 0x000A596C File Offset: 0x000A3B6C
		public void EndSendFile(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposedAndClosed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SendFileAsyncResult sendFileAsyncResult = asyncResult as Socket.SendFileAsyncResult;
			if (sendFileAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			sendFileAsyncResult.Delegate.EndInvoke(sendFileAsyncResult.Original);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000A59B8 File Offset: 0x000A3BB8
		private static bool SendFile_internal(SafeSocketHandle safeHandle, string filename, byte[] pre_buffer, byte[] post_buffer, TransmitFileOptions flags, out int error, bool blocking)
		{
			bool flag;
			try
			{
				safeHandle.RegisterForBlockingSyscall();
				flag = Socket.SendFile_icall(safeHandle.DangerousGetHandle(), filename, pre_buffer, post_buffer, flags, out error, blocking);
			}
			finally
			{
				safeHandle.UnRegisterForBlockingSyscall();
			}
			return flag;
		}

		// Token: 0x06002E76 RID: 11894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendFile_icall(IntPtr sock, string filename, byte[] pre_buffer, byte[] post_buffer, TransmitFileOptions flags, out int error, bool blocking);

		/// <summary>Sends a collection of files or in memory data buffers asynchronously to a connected <see cref="T:System.Net.Sockets.Socket" /> object.</summary>
		/// <returns>Returns true if the I/O operation is pending. The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will be raised upon completion of the operation. Returns false if the I/O operation completed synchronously. In this case, The <see cref="E:System.Net.Sockets.SocketAsyncEventArgs.Completed" /> event on the <paramref name="e" /> parameter will not be raised and the <paramref name="e" /> object passed as a parameter may be examined immediately after the method call returns to retrieve the result of the operation.</returns>
		/// <param name="e">The <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object to use for this asynchronous socket operation.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <see cref="P:System.Net.Sockets.SendPacketsElement.FilePath" /> property was not found. </exception>
		/// <exception cref="T:System.InvalidOperationException">A socket operation was already in progress using the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> object specified in the <paramref name="e" /> parameter.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows XP or later is required for this method. This exception also occurs if the <see cref="T:System.Net.Sockets.Socket" /> is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">A connectionless <see cref="T:System.Net.Sockets.Socket" /> is being used and the file being sent exceeds the maximum packet size of the underlying transport.</exception>
		// Token: 0x06002E77 RID: 11895 RVA: 0x000A4D79 File Offset: 0x000A2F79
		[MonoTODO("Not implemented")]
		public bool SendPacketsAsync(SocketAsyncEventArgs e)
		{
			this.ThrowIfDisposedAndClosed();
			throw new NotImplementedException();
		}

		// Token: 0x06002E78 RID: 11896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Duplicate_icall(IntPtr handle, int targetProcessId, out IntPtr duplicateHandle, out MonoIOError error);

		/// <summary>Duplicates the socket reference for the target process, and closes the socket for this process.</summary>
		/// <returns>The socket reference to be passed to the target process.</returns>
		/// <param name="targetProcessId">The ID of the target process where a duplicate of the socket reference is created.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="targetProcessID" /> is not a valid process id.-or- Duplication of the socket reference failed. </exception>
		// Token: 0x06002E79 RID: 11897 RVA: 0x000A59FC File Offset: 0x000A3BFC
		[MonoLimitation("We do not support passing sockets across processes, we merely allow this API to pass the socket across AppDomains")]
		public SocketInformation DuplicateAndClose(int targetProcessId)
		{
			SocketInformation socketInformation = default(SocketInformation);
			socketInformation.Options = (this.is_listening ? SocketInformationOptions.Listening : ((SocketInformationOptions)0)) | (this.is_connected ? SocketInformationOptions.Connected : ((SocketInformationOptions)0)) | (this.is_blocking ? ((SocketInformationOptions)0) : SocketInformationOptions.NonBlocking) | (this.useOverlappedIO ? SocketInformationOptions.UseOnlyOverlappedIO : ((SocketInformationOptions)0));
			IntPtr intPtr;
			MonoIOError monoIOError;
			if (!Socket.Duplicate_icall(this.Handle, targetProcessId, out intPtr, out monoIOError))
			{
				throw MonoIO.GetException(monoIOError);
			}
			socketInformation.ProtocolInformation = DataConverter.Pack("iiiil", new object[]
			{
				(int)this.addressFamily,
				(int)this.socketType,
				(int)this.protocolType,
				this.is_bound ? 1 : 0,
				(long)intPtr
			});
			this.m_Handle = null;
			return socketInformation;
		}

		/// <summary>Returns the specified <see cref="T:System.Net.Sockets.Socket" /> option setting, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that is to receive the option setting. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. - or -In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E7A RID: 11898 RVA: 0x000A5AD0 File Offset: 0x000A3CD0
		public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			this.ThrowIfDisposedAndClosed();
			if (optionValue == null)
			{
				throw new SocketException(10014, "Error trying to dereference an invalid pointer");
			}
			int num;
			Socket.GetSocketOption_arr_internal(this.m_Handle, optionLevel, optionName, ref optionValue, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		/// <summary>Returns the value of the specified <see cref="T:System.Net.Sockets.Socket" /> option in an array.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the value of the socket option.</returns>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionLength">The length, in bytes, of the expected return value. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. - or -In .NET Compact Framework applications, the Windows CE default buffer space is set to 32768 bytes. You can change the per socket buffer space by calling <see cref="Overload:System.Net.Sockets.Socket.SetSocketOption" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E7B RID: 11899 RVA: 0x000A5B14 File Offset: 0x000A3D14
		public byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionLength)
		{
			this.ThrowIfDisposedAndClosed();
			byte[] array = new byte[optionLength];
			int num;
			Socket.GetSocketOption_arr_internal(this.m_Handle, optionLevel, optionName, ref array, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			return array;
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Net.Sockets.Socket" /> option, represented as an object.</summary>
		/// <returns>An object that represents the value of the option. When the <paramref name="optionName" /> parameter is set to <see cref="F:System.Net.Sockets.SocketOptionName.Linger" /> the return value is an instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class. When <paramref name="optionName" /> is set to <see cref="F:System.Net.Sockets.SocketOptionName.AddMembership" /> or <see cref="F:System.Net.Sockets.SocketOptionName.DropMembership" />, the return value is an instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class. When <paramref name="optionName" /> is any other value, the return value is an integer.</returns>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information.-or-<paramref name="optionName" /> was set to the unsupported value <see cref="F:System.Net.Sockets.SocketOptionName.MaxConnections" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000A5B4C File Offset: 0x000A3D4C
		public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			this.ThrowIfDisposedAndClosed();
			object obj;
			int num;
			Socket.GetSocketOption_obj_internal(this.m_Handle, optionLevel, optionName, out obj, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (optionName == SocketOptionName.Linger)
			{
				return (LingerOption)obj;
			}
			if (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership)
			{
				return (MulticastOption)obj;
			}
			if (obj is int)
			{
				return (int)obj;
			}
			return obj;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000A5BB0 File Offset: 0x000A3DB0
		private static void GetSocketOption_arr_internal(SafeSocketHandle safeHandle, SocketOptionLevel level, SocketOptionName name, ref byte[] byte_val, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.GetSocketOption_arr_icall(safeHandle.DangerousGetHandle(), level, name, ref byte_val, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E7E RID: 11902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSocketOption_arr_icall(IntPtr socket, SocketOptionLevel level, SocketOptionName name, ref byte[] byte_val, out int error);

		// Token: 0x06002E7F RID: 11903 RVA: 0x000A5BF4 File Offset: 0x000A3DF4
		private static void GetSocketOption_obj_internal(SafeSocketHandle safeHandle, SocketOptionLevel level, SocketOptionName name, out object obj_val, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.GetSocketOption_obj_icall(safeHandle.DangerousGetHandle(), level, name, out obj_val, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E80 RID: 11904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSocketOption_obj_icall(IntPtr socket, SocketOptionLevel level, SocketOptionName name, out object obj_val, out int error);

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as a byte array.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">An array of type <see cref="T:System.Byte" /> that represents the value of the option. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E81 RID: 11905 RVA: 0x000A5C38 File Offset: 0x000A3E38
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue)
		{
			this.ThrowIfDisposedAndClosed();
			if (optionValue == null)
			{
				throw new SocketException(10014, "Error trying to dereference an invalid pointer");
			}
			int num;
			Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, null, optionValue, 0, out num);
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified value, represented as an object.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">A <see cref="T:System.Net.Sockets.LingerOption" /> or <see cref="T:System.Net.Sockets.MulticastOption" /> that contains the value of the option. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="optionValue" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E82 RID: 11906 RVA: 0x000A5C88 File Offset: 0x000A3E88
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue)
		{
			this.ThrowIfDisposedAndClosed();
			if (optionValue == null)
			{
				throw new ArgumentNullException("optionValue");
			}
			int num;
			if (optionLevel == SocketOptionLevel.Socket && optionName == SocketOptionName.Linger)
			{
				LingerOption lingerOption = optionValue as LingerOption;
				if (lingerOption == null)
				{
					throw new ArgumentException("A 'LingerOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, lingerOption, null, 0, out num);
			}
			else if (optionLevel == SocketOptionLevel.IP && (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership))
			{
				MulticastOption multicastOption = optionValue as MulticastOption;
				if (multicastOption == null)
				{
					throw new ArgumentException("A 'MulticastOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, multicastOption, null, 0, out num);
			}
			else
			{
				if (optionLevel != SocketOptionLevel.IPv6 || (optionName != SocketOptionName.AddMembership && optionName != SocketOptionName.DropMembership))
				{
					throw new ArgumentException("Invalid value specified.", "optionValue");
				}
				IPv6MulticastOption pv6MulticastOption = optionValue as IPv6MulticastOption;
				if (pv6MulticastOption == null)
				{
					throw new ArgumentException("A 'IPv6MulticastOption' value must be specified.", "optionValue");
				}
				Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, pv6MulticastOption, null, 0, out num);
			}
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">The value of the option, represented as a <see cref="T:System.Boolean" />. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> object has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E83 RID: 11907 RVA: 0x000A5D8C File Offset: 0x000A3F8C
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
		{
			int num = (optionValue ? 1 : 0);
			this.SetSocketOption(optionLevel, optionName, num);
		}

		/// <summary>Sets the specified <see cref="T:System.Net.Sockets.Socket" /> option to the specified integer value.</summary>
		/// <param name="optionLevel">One of the <see cref="T:System.Net.Sockets.SocketOptionLevel" /> values. </param>
		/// <param name="optionName">One of the <see cref="T:System.Net.Sockets.SocketOptionName" /> values. </param>
		/// <param name="optionValue">A value of the option. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E84 RID: 11908 RVA: 0x000A5DAC File Offset: 0x000A3FAC
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			this.ThrowIfDisposedAndClosed();
			int num;
			Socket.SetSocketOption_internal(this.m_Handle, optionLevel, optionName, null, null, optionValue, out num);
			if (num == 0)
			{
				return;
			}
			if (num == 10022)
			{
				throw new ArgumentException();
			}
			throw new SocketException(num);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000A5DEC File Offset: 0x000A3FEC
		private static void SetSocketOption_internal(SafeSocketHandle safeHandle, SocketOptionLevel level, SocketOptionName name, object obj_val, byte[] byte_val, int int_val, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.SetSocketOption_icall(safeHandle.DangerousGetHandle(), level, name, obj_val, byte_val, int_val, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E86 RID: 11910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSocketOption_icall(IntPtr socket, SocketOptionLevel level, SocketOptionName name, object obj_val, byte[] byte_val, int int_val, out int error);

		/// <summary>Sets low-level operating modes for the <see cref="T:System.Net.Sockets.Socket" /> using numerical control codes.</summary>
		/// <returns>The number of bytes in the <paramref name="optionOutValue" /> parameter.</returns>
		/// <param name="ioControlCode">An <see cref="T:System.Int32" /> value that specifies the control code of the operation to perform. </param>
		/// <param name="optionInValue">A <see cref="T:System.Byte" /> array that contains the input data required by the operation. </param>
		/// <param name="optionOutValue">A <see cref="T:System.Byte" /> array that contains the output data returned by the operation. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to change the blocking mode without using the <see cref="P:System.Net.Sockets.Socket.Blocking" /> property. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call stack does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E87 RID: 11911 RVA: 0x000A5E34 File Offset: 0x000A4034
		public int IOControl(int ioControlCode, byte[] optionInValue, byte[] optionOutValue)
		{
			if (this.CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num2;
			int num = Socket.IOControl_internal(this.m_Handle, ioControlCode, optionInValue, optionOutValue, out num2);
			if (num2 != 0)
			{
				throw new SocketException(num2);
			}
			if (num == -1)
			{
				throw new InvalidOperationException("Must use Blocking property instead.");
			}
			return num;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000A5E84 File Offset: 0x000A4084
		private static int IOControl_internal(SafeSocketHandle safeHandle, int ioctl_code, byte[] input, byte[] output, out int error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = Socket.IOControl_icall(safeHandle.DangerousGetHandle(), ioctl_code, input, output, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002E89 RID: 11913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IOControl_icall(IntPtr sock, int ioctl_code, byte[] input, byte[] output, out int error);

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E8A RID: 11914 RVA: 0x000A5ECC File Offset: 0x000A40CC
		public void Close()
		{
			this.linger_timeout = 0;
			this.Dispose();
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.Socket" /> connection and releases all associated resources with a specified timeout to allow queued data to be sent. </summary>
		/// <param name="timeout">Wait up to <paramref name="timeout" /> seconds to send any remaining data, then close the socket.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E8B RID: 11915 RVA: 0x000A5EDB File Offset: 0x000A40DB
		public void Close(int timeout)
		{
			this.linger_timeout = timeout;
			this.Dispose();
		}

		// Token: 0x06002E8C RID: 11916
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Close_icall(IntPtr socket, out int error);

		/// <summary>Disables sends and receives on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="how">One of the <see cref="T:System.Net.Sockets.SocketShutdown" /> values that specifies the operation that will no longer be allowed. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002E8D RID: 11917 RVA: 0x000A5EEC File Offset: 0x000A40EC
		public void Shutdown(SocketShutdown how)
		{
			this.ThrowIfDisposedAndClosed();
			if (!this.is_connected)
			{
				throw new SocketException(10057);
			}
			int num;
			Socket.Shutdown_internal(this.m_Handle, how, out num);
			if (num == 10057)
			{
				return;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000A5F34 File Offset: 0x000A4134
		private static void Shutdown_internal(SafeSocketHandle safeHandle, SocketShutdown how, out int error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				Socket.Shutdown_icall(safeHandle.DangerousGetHandle(), how, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06002E8F RID: 11919
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Shutdown_icall(IntPtr socket, SocketShutdown how, out int error);

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.Socket" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x06002E90 RID: 11920 RVA: 0x000A5F74 File Offset: 0x000A4174
		protected virtual void Dispose(bool disposing)
		{
			if (this.CleanedUp)
			{
				return;
			}
			this.m_IntCleanedUp = 1;
			bool flag = this.is_connected;
			this.is_connected = false;
			if (this.m_Handle != null)
			{
				this.is_closed = true;
				IntPtr handle = this.Handle;
				if (flag)
				{
					this.Linger(handle);
				}
				this.m_Handle.Dispose();
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000A5FCC File Offset: 0x000A41CC
		private void Linger(IntPtr handle)
		{
			if (!this.is_connected || this.linger_timeout <= 0)
			{
				return;
			}
			int num;
			Socket.Shutdown_icall(handle, SocketShutdown.Receive, out num);
			if (num != 0)
			{
				return;
			}
			int num2 = this.linger_timeout / 1000;
			int num3 = this.linger_timeout % 1000;
			if (num3 > 0)
			{
				Socket.Poll_icall(handle, SelectMode.SelectRead, num3 * 1000, out num);
				if (num != 0)
				{
					return;
				}
			}
			if (num2 > 0)
			{
				LingerOption lingerOption = new LingerOption(true, num2);
				Socket.SetSocketOption_icall(handle, SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption, null, 0, out num);
			}
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000A604C File Offset: 0x000A424C
		private void ThrowIfDisposedAndClosed(Socket socket)
		{
			if (socket.CleanedUp && socket.is_closed)
			{
				throw new ObjectDisposedException(socket.GetType().ToString());
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000A606F File Offset: 0x000A426F
		private void ThrowIfDisposedAndClosed()
		{
			if (this.CleanedUp && this.is_closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000A6092 File Offset: 0x000A4292
		private void ThrowIfBufferNull(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000A60A4 File Offset: 0x000A42A4
		private void ThrowIfBufferOutOfRange(byte[] buffer, int offset, int size)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be >= 0");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be <= buffer.Length");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "size must be >= 0");
			}
			if (size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size", "size must be <= buffer.Length - offset");
			}
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000A6107 File Offset: 0x000A4307
		private void ThrowIfUdp()
		{
			if (this.protocolType == ProtocolType.Udp)
			{
				throw new SocketException(10042);
			}
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000A6120 File Offset: 0x000A4320
		private SocketAsyncResult ValidateEndIAsyncResult(IAsyncResult ares, string methodName, string argName)
		{
			if (ares == null)
			{
				throw new ArgumentNullException(argName);
			}
			SocketAsyncResult socketAsyncResult = ares as SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", argName);
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw new InvalidOperationException(methodName + " can only be called once per asynchronous operation");
			}
			return socketAsyncResult;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000A6170 File Offset: 0x000A4370
		private void QueueIOSelectorJob(SemaphoreSlim sem, IntPtr handle, IOSelectorJob job)
		{
			Task task = sem.WaitAsync();
			if (!task.IsCompleted)
			{
				task.ContinueWith(delegate(Task t)
				{
					if (this.CleanedUp)
					{
						job.MarkDisposed();
						return;
					}
					IOSelector.Add(handle, job);
				});
				return;
			}
			if (this.CleanedUp)
			{
				job.MarkDisposed();
				return;
			}
			IOSelector.Add(handle, job);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000A61E0 File Offset: 0x000A43E0
		private void InitSocketAsyncEventArgs(SocketAsyncEventArgs e, AsyncCallback callback, object state, SocketOperation operation)
		{
			e.socket_async_result.Init(this, callback, state, operation);
			if (e.AcceptSocket != null)
			{
				e.socket_async_result.AcceptSocket = e.AcceptSocket;
			}
			e.SetCurrentSocket(this);
			e.SetLastOperation(this.SocketOperationToSocketAsyncOperation(operation));
			e.SocketError = SocketError.Success;
			e.SetBytesTransferred(0);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000A623C File Offset: 0x000A443C
		private SocketAsyncOperation SocketOperationToSocketAsyncOperation(SocketOperation op)
		{
			switch (op)
			{
			case SocketOperation.Accept:
				return SocketAsyncOperation.Accept;
			case SocketOperation.Connect:
				return SocketAsyncOperation.Connect;
			case SocketOperation.Receive:
			case SocketOperation.ReceiveGeneric:
				return SocketAsyncOperation.Receive;
			case SocketOperation.ReceiveFrom:
				return SocketAsyncOperation.ReceiveFrom;
			case SocketOperation.Send:
			case SocketOperation.SendGeneric:
				return SocketAsyncOperation.Send;
			case SocketOperation.SendTo:
				return SocketAsyncOperation.SendTo;
			case SocketOperation.Disconnect:
				return SocketAsyncOperation.Disconnect;
			}
			throw new NotImplementedException(string.Format("Operation {0} is not implemented", op));
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000A62A5 File Offset: 0x000A44A5
		private IPEndPoint RemapIPEndPoint(IPEndPoint input)
		{
			if (this.IsDualMode && input.AddressFamily == AddressFamily.InterNetwork)
			{
				return new IPEndPoint(input.Address.MapToIPv6(), input.Port);
			}
			return input;
		}

		// Token: 0x06002E9C RID: 11932
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void cancel_blocking_socket_operation(Thread thread);

		// Token: 0x06002E9D RID: 11933
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SupportsPortReuse(ProtocolType proto);

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000A62D0 File Offset: 0x000A44D0
		internal static int FamilyHint
		{
			get
			{
				int num = 0;
				if (Socket.OSSupportsIPv4)
				{
					num = 1;
				}
				if (Socket.OSSupportsIPv6)
				{
					num = ((num == 0) ? 2 : 0);
				}
				return num;
			}
		}

		// Token: 0x06002E9F RID: 11935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsProtocolSupported_internal(NetworkInterfaceComponent networkInterface);

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000A62F8 File Offset: 0x000A44F8
		private static bool IsProtocolSupported(NetworkInterfaceComponent networkInterface)
		{
			return Socket.IsProtocolSupported_internal(networkInterface);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00003917 File Offset: 0x00001B17
		internal void ReplaceHandleIfNecessaryAfterFailedConnect()
		{
		}

		// Token: 0x04001AE6 RID: 6886
		private static readonly EventHandler<SocketAsyncEventArgs> AcceptCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteAccept((Socket)s, (Socket.TaskSocketAsyncEventArgs<Socket>)e);
		};

		// Token: 0x04001AE7 RID: 6887
		private static readonly EventHandler<SocketAsyncEventArgs> ReceiveCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteSendReceive((Socket)s, (Socket.Int32TaskSocketAsyncEventArgs)e, true);
		};

		// Token: 0x04001AE8 RID: 6888
		private static readonly EventHandler<SocketAsyncEventArgs> SendCompletedHandler = delegate(object s, SocketAsyncEventArgs e)
		{
			Socket.CompleteSendReceive((Socket)s, (Socket.Int32TaskSocketAsyncEventArgs)e, false);
		};

		// Token: 0x04001AE9 RID: 6889
		private static readonly Socket.TaskSocketAsyncEventArgs<Socket> s_rentedSocketSentinel = new Socket.TaskSocketAsyncEventArgs<Socket>();

		// Token: 0x04001AEA RID: 6890
		private static readonly Socket.Int32TaskSocketAsyncEventArgs s_rentedInt32Sentinel = new Socket.Int32TaskSocketAsyncEventArgs();

		// Token: 0x04001AEB RID: 6891
		private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);

		// Token: 0x04001AEC RID: 6892
		private Socket.CachedEventArgs _cachedTaskEventArgs;

		// Token: 0x04001AED RID: 6893
		private static object s_InternalSyncObject;

		// Token: 0x04001AEE RID: 6894
		internal static volatile bool s_SupportsIPv4;

		// Token: 0x04001AEF RID: 6895
		internal static volatile bool s_SupportsIPv6;

		// Token: 0x04001AF0 RID: 6896
		internal static volatile bool s_OSSupportsIPv6;

		// Token: 0x04001AF1 RID: 6897
		internal static volatile bool s_Initialized;

		// Token: 0x04001AF2 RID: 6898
		private static volatile bool s_LoggingEnabled;

		// Token: 0x04001AF3 RID: 6899
		internal static volatile bool s_PerfCountersEnabled;

		// Token: 0x04001AF4 RID: 6900
		internal const int DefaultCloseTimeout = -1;

		// Token: 0x04001AF5 RID: 6901
		private const int SOCKET_CLOSED_CODE = 10004;

		// Token: 0x04001AF6 RID: 6902
		private const string TIMEOUT_EXCEPTION_MSG = "A connection attempt failed because the connected party did not properly respondafter a period of time, or established connection failed because connected host has failed to respond";

		// Token: 0x04001AF7 RID: 6903
		private bool is_closed;

		// Token: 0x04001AF8 RID: 6904
		private bool is_listening;

		// Token: 0x04001AF9 RID: 6905
		private bool useOverlappedIO;

		// Token: 0x04001AFA RID: 6906
		private int linger_timeout;

		// Token: 0x04001AFB RID: 6907
		private AddressFamily addressFamily;

		// Token: 0x04001AFC RID: 6908
		private SocketType socketType;

		// Token: 0x04001AFD RID: 6909
		private ProtocolType protocolType;

		// Token: 0x04001AFE RID: 6910
		internal SafeSocketHandle m_Handle;

		// Token: 0x04001AFF RID: 6911
		internal EndPoint seed_endpoint;

		// Token: 0x04001B00 RID: 6912
		internal SemaphoreSlim ReadSem;

		// Token: 0x04001B01 RID: 6913
		internal SemaphoreSlim WriteSem;

		// Token: 0x04001B02 RID: 6914
		internal bool is_blocking;

		// Token: 0x04001B03 RID: 6915
		internal bool is_bound;

		// Token: 0x04001B04 RID: 6916
		internal bool is_connected;

		// Token: 0x04001B05 RID: 6917
		private int m_IntCleanedUp;

		// Token: 0x04001B06 RID: 6918
		internal bool connect_in_progress;

		// Token: 0x04001B07 RID: 6919
		internal readonly int ID;

		// Token: 0x04001B08 RID: 6920
		private static AsyncCallback AcceptAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs.AcceptSocket = socketAsyncEventArgs.CurrentSocket.EndAccept(ares);
			}
			catch (SocketException ex)
			{
				socketAsyncEventArgs.SocketError = ex.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				if (socketAsyncEventArgs.AcceptSocket == null)
				{
					socketAsyncEventArgs.AcceptSocket = new Socket(socketAsyncEventArgs.CurrentSocket.AddressFamily, socketAsyncEventArgs.CurrentSocket.SocketType, socketAsyncEventArgs.CurrentSocket.ProtocolType, null);
				}
				socketAsyncEventArgs.Complete_internal();
			}
		};

		// Token: 0x04001B09 RID: 6921
		private static IOAsyncCallback BeginAcceptCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult = (SocketAsyncResult)ares;
			Socket socket = null;
			try
			{
				if (socketAsyncResult.AcceptSocket == null)
				{
					socket = socketAsyncResult.socket.Accept();
				}
				else
				{
					socket = socketAsyncResult.AcceptSocket;
					socketAsyncResult.socket.Accept(socket);
				}
			}
			catch (Exception ex2)
			{
				socketAsyncResult.Complete(ex2);
				return;
			}
			socketAsyncResult.Complete(socket);
		};

		// Token: 0x04001B0A RID: 6922
		private static IOAsyncCallback BeginAcceptReceiveCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult2 = (SocketAsyncResult)ares;
			Socket socket2 = null;
			try
			{
				if (socketAsyncResult2.AcceptSocket == null)
				{
					socket2 = socketAsyncResult2.socket.Accept();
				}
				else
				{
					socket2 = socketAsyncResult2.AcceptSocket;
					socketAsyncResult2.socket.Accept(socket2);
				}
			}
			catch (Exception ex3)
			{
				socketAsyncResult2.Complete(ex3);
				return;
			}
			int num = 0;
			if (socketAsyncResult2.Size > 0)
			{
				try
				{
					SocketError socketError;
					num = socket2.Receive(socketAsyncResult2.Buffer, socketAsyncResult2.Offset, socketAsyncResult2.Size, socketAsyncResult2.SockFlags, out socketError);
					if (socketError != SocketError.Success)
					{
						socketAsyncResult2.Complete(new SocketException((int)socketError));
						return;
					}
				}
				catch (Exception ex4)
				{
					socketAsyncResult2.Complete(ex4);
					return;
				}
			}
			socketAsyncResult2.Complete(socket2, num);
		};

		// Token: 0x04001B0B RID: 6923
		private static AsyncCallback ConnectAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs2 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs2.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs2.CurrentSocket.EndConnect(ares);
			}
			catch (SocketException ex5)
			{
				socketAsyncEventArgs2.SocketError = ex5.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs2.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs2.Complete_internal();
			}
		};

		// Token: 0x04001B0C RID: 6924
		private static IOAsyncCallback BeginConnectCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult3 = (SocketAsyncResult)ares;
			if (socketAsyncResult3.EndPoint == null)
			{
				socketAsyncResult3.Complete(new SocketException(10049));
				return;
			}
			try
			{
				int num2 = (int)socketAsyncResult3.socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
				if (num2 == 0)
				{
					socketAsyncResult3.socket.seed_endpoint = socketAsyncResult3.EndPoint;
					socketAsyncResult3.socket.is_connected = true;
					socketAsyncResult3.socket.is_bound = true;
					socketAsyncResult3.socket.connect_in_progress = false;
					socketAsyncResult3.error = 0;
					socketAsyncResult3.Complete();
				}
				else if (socketAsyncResult3.Addresses == null)
				{
					socketAsyncResult3.socket.connect_in_progress = false;
					socketAsyncResult3.Complete(new SocketException(num2));
				}
				else if (socketAsyncResult3.CurrentAddress >= socketAsyncResult3.Addresses.Length)
				{
					socketAsyncResult3.Complete(new SocketException(num2));
				}
				else
				{
					Socket.BeginMConnect(socketAsyncResult3);
				}
			}
			catch (Exception ex6)
			{
				socketAsyncResult3.socket.connect_in_progress = false;
				socketAsyncResult3.Complete(ex6);
			}
		};

		// Token: 0x04001B0D RID: 6925
		private static AsyncCallback DisconnectAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs3 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs3.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs3.CurrentSocket.EndDisconnect(ares);
			}
			catch (SocketException ex7)
			{
				socketAsyncEventArgs3.SocketError = ex7.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs3.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs3.Complete_internal();
			}
		};

		// Token: 0x04001B0E RID: 6926
		private static IOAsyncCallback BeginDisconnectCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult4 = (SocketAsyncResult)ares;
			try
			{
				socketAsyncResult4.socket.Disconnect(socketAsyncResult4.ReuseSocket);
			}
			catch (Exception ex8)
			{
				socketAsyncResult4.Complete(ex8);
				return;
			}
			socketAsyncResult4.Complete();
		};

		// Token: 0x04001B0F RID: 6927
		private static AsyncCallback ReceiveAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs4 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs4.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs4.SetBytesTransferred(socketAsyncEventArgs4.CurrentSocket.EndReceive(ares));
			}
			catch (SocketException ex9)
			{
				socketAsyncEventArgs4.SocketError = ex9.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs4.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs4.Complete_internal();
			}
		};

		// Token: 0x04001B10 RID: 6928
		private static IOAsyncCallback BeginReceiveCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult5 = (SocketAsyncResult)ares;
			int num3 = 0;
			try
			{
				using (MemoryHandle memoryHandle = socketAsyncResult5.Buffer.Slice(socketAsyncResult5.Offset, socketAsyncResult5.Size).Pin())
				{
					num3 = Socket.Receive_internal(socketAsyncResult5.socket.m_Handle, (byte*)memoryHandle.Pointer, socketAsyncResult5.Size, socketAsyncResult5.SockFlags, out socketAsyncResult5.error, socketAsyncResult5.socket.is_blocking);
				}
			}
			catch (Exception ex10)
			{
				socketAsyncResult5.Complete(ex10);
				return;
			}
			socketAsyncResult5.Complete(num3);
		};

		// Token: 0x04001B11 RID: 6929
		private static IOAsyncCallback BeginReceiveGenericCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult6 = (SocketAsyncResult)ares;
			int num4 = 0;
			try
			{
				num4 = socketAsyncResult6.socket.Receive(socketAsyncResult6.Buffers, socketAsyncResult6.SockFlags);
			}
			catch (Exception ex11)
			{
				socketAsyncResult6.Complete(ex11);
				return;
			}
			socketAsyncResult6.Complete(num4);
		};

		// Token: 0x04001B12 RID: 6930
		private static AsyncCallback ReceiveFromAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs5 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs5.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs5.SetBytesTransferred(socketAsyncEventArgs5.CurrentSocket.EndReceiveFrom_internal((SocketAsyncResult)ares, socketAsyncEventArgs5));
			}
			catch (SocketException ex12)
			{
				socketAsyncEventArgs5.SocketError = ex12.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs5.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs5.Complete_internal();
			}
		};

		// Token: 0x04001B13 RID: 6931
		private static IOAsyncCallback BeginReceiveFromCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult7 = (SocketAsyncResult)ares;
			int num5 = 0;
			try
			{
				SocketError socketError2;
				num5 = socketAsyncResult7.socket.ReceiveFrom(socketAsyncResult7.Buffer, socketAsyncResult7.Offset, socketAsyncResult7.Size, socketAsyncResult7.SockFlags, ref socketAsyncResult7.EndPoint, out socketError2);
				if (socketError2 != SocketError.Success)
				{
					socketAsyncResult7.Complete(new SocketException(socketError2));
					return;
				}
			}
			catch (Exception ex13)
			{
				socketAsyncResult7.Complete(ex13);
				return;
			}
			socketAsyncResult7.Complete(num5);
		};

		// Token: 0x04001B14 RID: 6932
		private static AsyncCallback SendAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs6 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs6.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs6.SetBytesTransferred(socketAsyncEventArgs6.CurrentSocket.EndSend(ares));
			}
			catch (SocketException ex14)
			{
				socketAsyncEventArgs6.SocketError = ex14.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs6.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs6.Complete_internal();
			}
		};

		// Token: 0x04001B15 RID: 6933
		private static IOAsyncCallback BeginSendGenericCallback = delegate(IOAsyncResult ares)
		{
			SocketAsyncResult socketAsyncResult8 = (SocketAsyncResult)ares;
			int num6 = 0;
			try
			{
				num6 = socketAsyncResult8.socket.Send(socketAsyncResult8.Buffers, socketAsyncResult8.SockFlags);
			}
			catch (Exception ex15)
			{
				socketAsyncResult8.Complete(ex15);
				return;
			}
			socketAsyncResult8.Complete(num6);
		};

		// Token: 0x04001B16 RID: 6934
		private static AsyncCallback SendToAsyncCallback = delegate(IAsyncResult ares)
		{
			SocketAsyncEventArgs socketAsyncEventArgs7 = (SocketAsyncEventArgs)((SocketAsyncResult)ares).AsyncState;
			if (Interlocked.Exchange(ref socketAsyncEventArgs7.in_progress, 0) != 1)
			{
				throw new InvalidOperationException("No operation in progress");
			}
			try
			{
				socketAsyncEventArgs7.SetBytesTransferred(socketAsyncEventArgs7.CurrentSocket.EndSendTo(ares));
			}
			catch (SocketException ex16)
			{
				socketAsyncEventArgs7.SocketError = ex16.SocketErrorCode;
			}
			catch (ObjectDisposedException)
			{
				socketAsyncEventArgs7.SocketError = SocketError.OperationAborted;
			}
			finally
			{
				socketAsyncEventArgs7.Complete_internal();
			}
		};

		// Token: 0x020005A1 RID: 1441
		private class StateTaskCompletionSource<TField1, TResult> : TaskCompletionSource<TResult>
		{
			// Token: 0x06002EA3 RID: 11939 RVA: 0x000A64A6 File Offset: 0x000A46A6
			public StateTaskCompletionSource(object baseState)
				: base(baseState)
			{
			}

			// Token: 0x04001B17 RID: 6935
			internal TField1 _field1;
		}

		// Token: 0x020005A2 RID: 1442
		private class StateTaskCompletionSource<TField1, TField2, TResult> : Socket.StateTaskCompletionSource<TField1, TResult>
		{
			// Token: 0x06002EA4 RID: 11940 RVA: 0x000A64AF File Offset: 0x000A46AF
			public StateTaskCompletionSource(object baseState)
				: base(baseState)
			{
			}

			// Token: 0x04001B18 RID: 6936
			internal TField2 _field2;
		}

		// Token: 0x020005A3 RID: 1443
		private sealed class CachedEventArgs
		{
			// Token: 0x04001B19 RID: 6937
			public Socket.TaskSocketAsyncEventArgs<Socket> TaskAccept;

			// Token: 0x04001B1A RID: 6938
			public Socket.Int32TaskSocketAsyncEventArgs TaskReceive;

			// Token: 0x04001B1B RID: 6939
			public Socket.Int32TaskSocketAsyncEventArgs TaskSend;

			// Token: 0x04001B1C RID: 6940
			public Socket.AwaitableSocketAsyncEventArgs ValueTaskReceive;

			// Token: 0x04001B1D RID: 6941
			public Socket.AwaitableSocketAsyncEventArgs ValueTaskSend;
		}

		// Token: 0x020005A4 RID: 1444
		private class TaskSocketAsyncEventArgs<TResult> : SocketAsyncEventArgs
		{
			// Token: 0x06002EA6 RID: 11942 RVA: 0x000A64B8 File Offset: 0x000A46B8
			internal TaskSocketAsyncEventArgs()
				: base(false)
			{
			}

			// Token: 0x06002EA7 RID: 11943 RVA: 0x000A64C4 File Offset: 0x000A46C4
			internal AsyncTaskMethodBuilder<TResult> GetCompletionResponsibility(out bool responsibleForReturningToPool)
			{
				AsyncTaskMethodBuilder<TResult> builder;
				lock (this)
				{
					responsibleForReturningToPool = this._accessed;
					this._accessed = true;
					Task<TResult> task = this._builder.Task;
					builder = this._builder;
				}
				return builder;
			}

			// Token: 0x04001B1E RID: 6942
			internal AsyncTaskMethodBuilder<TResult> _builder;

			// Token: 0x04001B1F RID: 6943
			internal bool _accessed;
		}

		// Token: 0x020005A5 RID: 1445
		private sealed class Int32TaskSocketAsyncEventArgs : Socket.TaskSocketAsyncEventArgs<int>
		{
			// Token: 0x04001B20 RID: 6944
			internal bool _wrapExceptionsInIOExceptions;
		}

		// Token: 0x020005A6 RID: 1446
		internal sealed class AwaitableSocketAsyncEventArgs : SocketAsyncEventArgs, IValueTaskSource, IValueTaskSource<int>
		{
			// Token: 0x06002EA9 RID: 11945 RVA: 0x000A6524 File Offset: 0x000A4724
			public AwaitableSocketAsyncEventArgs()
				: base(false)
			{
			}

			// Token: 0x17000AEF RID: 2799
			// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000A6538 File Offset: 0x000A4738
			// (set) Token: 0x06002EAB RID: 11947 RVA: 0x000A6540 File Offset: 0x000A4740
			public bool WrapExceptionsInIOExceptions { get; set; }

			// Token: 0x06002EAC RID: 11948 RVA: 0x000A6549 File Offset: 0x000A4749
			public bool Reserve()
			{
				return Interlocked.CompareExchange<Action<object>>(ref this._continuation, null, Socket.AwaitableSocketAsyncEventArgs.s_availableSentinel) == Socket.AwaitableSocketAsyncEventArgs.s_availableSentinel;
			}

			// Token: 0x06002EAD RID: 11949 RVA: 0x000A6563 File Offset: 0x000A4763
			private void Release()
			{
				this._token += 1;
				Volatile.Write<Action<object>>(ref this._continuation, Socket.AwaitableSocketAsyncEventArgs.s_availableSentinel);
			}

			// Token: 0x06002EAE RID: 11950 RVA: 0x000A6584 File Offset: 0x000A4784
			protected override void OnCompleted(SocketAsyncEventArgs _)
			{
				Action<object> action = this._continuation;
				if (action != null || (action = Interlocked.CompareExchange<Action<object>>(ref this._continuation, Socket.AwaitableSocketAsyncEventArgs.s_completedSentinel, null)) != null)
				{
					object userToken = base.UserToken;
					base.UserToken = null;
					this._continuation = Socket.AwaitableSocketAsyncEventArgs.s_completedSentinel;
					ExecutionContext executionContext = this._executionContext;
					if (executionContext == null)
					{
						this.InvokeContinuation(action, userToken, false);
						return;
					}
					this._executionContext = null;
					ExecutionContext.Run(executionContext, delegate(object runState)
					{
						Tuple<Socket.AwaitableSocketAsyncEventArgs, Action<object>, object> tuple = (Tuple<Socket.AwaitableSocketAsyncEventArgs, Action<object>, object>)runState;
						tuple.Item1.InvokeContinuation(tuple.Item2, tuple.Item3, false);
					}, Tuple.Create<Socket.AwaitableSocketAsyncEventArgs, Action<object>, object>(this, action, userToken));
				}
			}

			// Token: 0x06002EAF RID: 11951 RVA: 0x000A6614 File Offset: 0x000A4814
			public ValueTask<int> ReceiveAsync(Socket socket)
			{
				if (socket.ReceiveAsync(this))
				{
					return new ValueTask<int>(this, this._token);
				}
				int bytesTransferred = base.BytesTransferred;
				SocketError socketError = base.SocketError;
				this.Release();
				if (socketError != SocketError.Success)
				{
					return new ValueTask<int>(Task.FromException<int>(this.CreateException(socketError)));
				}
				return new ValueTask<int>(bytesTransferred);
			}

			// Token: 0x06002EB0 RID: 11952 RVA: 0x000A6668 File Offset: 0x000A4868
			public ValueTask<int> SendAsync(Socket socket)
			{
				if (socket.SendAsync(this))
				{
					return new ValueTask<int>(this, this._token);
				}
				int bytesTransferred = base.BytesTransferred;
				SocketError socketError = base.SocketError;
				this.Release();
				if (socketError != SocketError.Success)
				{
					return new ValueTask<int>(Task.FromException<int>(this.CreateException(socketError)));
				}
				return new ValueTask<int>(bytesTransferred);
			}

			// Token: 0x06002EB1 RID: 11953 RVA: 0x000A66BC File Offset: 0x000A48BC
			public ValueTask SendAsyncForNetworkStream(Socket socket)
			{
				if (socket.SendAsync(this))
				{
					return new ValueTask(this, this._token);
				}
				SocketError socketError = base.SocketError;
				this.Release();
				if (socketError != SocketError.Success)
				{
					return new ValueTask(Task.FromException(this.CreateException(socketError)));
				}
				return default(ValueTask);
			}

			// Token: 0x06002EB2 RID: 11954 RVA: 0x000A670A File Offset: 0x000A490A
			public ValueTaskSourceStatus GetStatus(short token)
			{
				if (token != this._token)
				{
					this.ThrowIncorrectTokenException();
				}
				if (this._continuation != Socket.AwaitableSocketAsyncEventArgs.s_completedSentinel)
				{
					return ValueTaskSourceStatus.Pending;
				}
				if (base.SocketError != SocketError.Success)
				{
					return ValueTaskSourceStatus.Faulted;
				}
				return ValueTaskSourceStatus.Succeeded;
			}

			// Token: 0x06002EB3 RID: 11955 RVA: 0x000A6738 File Offset: 0x000A4938
			public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
			{
				if (token != this._token)
				{
					this.ThrowIncorrectTokenException();
				}
				if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
				{
					this._executionContext = ExecutionContext.Capture();
				}
				if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
				{
					SynchronizationContext synchronizationContext = SynchronizationContext.Current;
					if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
					{
						this._scheduler = synchronizationContext;
					}
					else
					{
						TaskScheduler taskScheduler = TaskScheduler.Current;
						if (taskScheduler != TaskScheduler.Default)
						{
							this._scheduler = taskScheduler;
						}
					}
				}
				base.UserToken = state;
				Action<object> action = Interlocked.CompareExchange<Action<object>>(ref this._continuation, continuation, null);
				if (action == Socket.AwaitableSocketAsyncEventArgs.s_completedSentinel)
				{
					this._executionContext = null;
					base.UserToken = null;
					this.InvokeContinuation(continuation, state, true);
					return;
				}
				if (action != null)
				{
					this.ThrowMultipleContinuationsException();
				}
			}

			// Token: 0x06002EB4 RID: 11956 RVA: 0x000A67E8 File Offset: 0x000A49E8
			private void InvokeContinuation(Action<object> continuation, object state, bool forceAsync)
			{
				object scheduler = this._scheduler;
				this._scheduler = null;
				if (scheduler != null)
				{
					SynchronizationContext synchronizationContext = scheduler as SynchronizationContext;
					if (synchronizationContext != null)
					{
						synchronizationContext.Post(delegate(object s)
						{
							Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
							tuple.Item1(tuple.Item2);
						}, Tuple.Create<Action<object>, object>(continuation, state));
						return;
					}
					Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, (TaskScheduler)scheduler);
					return;
				}
				else
				{
					if (forceAsync)
					{
						ThreadPool.QueueUserWorkItem<object>(continuation, state, true);
						return;
					}
					continuation(state);
					return;
				}
			}

			// Token: 0x06002EB5 RID: 11957 RVA: 0x000A686C File Offset: 0x000A4A6C
			public int GetResult(short token)
			{
				if (token != this._token)
				{
					this.ThrowIncorrectTokenException();
				}
				SocketError socketError = base.SocketError;
				int bytesTransferred = base.BytesTransferred;
				this.Release();
				if (socketError != SocketError.Success)
				{
					this.ThrowException(socketError);
				}
				return bytesTransferred;
			}

			// Token: 0x06002EB6 RID: 11958 RVA: 0x000A68A8 File Offset: 0x000A4AA8
			void IValueTaskSource.GetResult(short token)
			{
				if (token != this._token)
				{
					this.ThrowIncorrectTokenException();
				}
				SocketError socketError = base.SocketError;
				this.Release();
				if (socketError != SocketError.Success)
				{
					this.ThrowException(socketError);
				}
			}

			// Token: 0x06002EB7 RID: 11959 RVA: 0x000A68DB File Offset: 0x000A4ADB
			private void ThrowIncorrectTokenException()
			{
				throw new InvalidOperationException("The result of the operation was already consumed and may not be used again.");
			}

			// Token: 0x06002EB8 RID: 11960 RVA: 0x000A68E7 File Offset: 0x000A4AE7
			private void ThrowMultipleContinuationsException()
			{
				throw new InvalidOperationException("Another continuation was already registered.");
			}

			// Token: 0x06002EB9 RID: 11961 RVA: 0x000A68F3 File Offset: 0x000A4AF3
			private void ThrowException(SocketError error)
			{
				throw this.CreateException(error);
			}

			// Token: 0x06002EBA RID: 11962 RVA: 0x000A68FC File Offset: 0x000A4AFC
			private Exception CreateException(SocketError error)
			{
				SocketException ex = new SocketException((int)error);
				if (!this.WrapExceptionsInIOExceptions)
				{
					return ex;
				}
				return new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}

			// Token: 0x04001B21 RID: 6945
			internal static readonly Socket.AwaitableSocketAsyncEventArgs Reserved = new Socket.AwaitableSocketAsyncEventArgs
			{
				_continuation = null
			};

			// Token: 0x04001B22 RID: 6946
			private static readonly Action<object> s_completedSentinel = delegate(object state)
			{
				throw new Exception("s_completedSentinel");
			};

			// Token: 0x04001B23 RID: 6947
			private static readonly Action<object> s_availableSentinel = delegate(object state)
			{
				throw new Exception("s_availableSentinel");
			};

			// Token: 0x04001B24 RID: 6948
			private Action<object> _continuation = Socket.AwaitableSocketAsyncEventArgs.s_availableSentinel;

			// Token: 0x04001B25 RID: 6949
			private ExecutionContext _executionContext;

			// Token: 0x04001B26 RID: 6950
			private object _scheduler;

			// Token: 0x04001B27 RID: 6951
			private short _token;
		}

		// Token: 0x020005A8 RID: 1448
		// (Invoke) Token: 0x06002EC3 RID: 11971
		private delegate void SendFileHandler(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags);

		// Token: 0x020005A9 RID: 1449
		private sealed class SendFileAsyncResult : IAsyncResult
		{
			// Token: 0x06002EC6 RID: 11974 RVA: 0x000A69E5 File Offset: 0x000A4BE5
			public SendFileAsyncResult(Socket.SendFileHandler d, IAsyncResult ares)
			{
				this.d = d;
				this.ares = ares;
			}

			// Token: 0x17000AF0 RID: 2800
			// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000A69FB File Offset: 0x000A4BFB
			public object AsyncState
			{
				get
				{
					return this.ares.AsyncState;
				}
			}

			// Token: 0x17000AF1 RID: 2801
			// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000A6A08 File Offset: 0x000A4C08
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.ares.AsyncWaitHandle;
				}
			}

			// Token: 0x17000AF2 RID: 2802
			// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000A6A15 File Offset: 0x000A4C15
			public bool CompletedSynchronously
			{
				get
				{
					return this.ares.CompletedSynchronously;
				}
			}

			// Token: 0x17000AF3 RID: 2803
			// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000A6A22 File Offset: 0x000A4C22
			public bool IsCompleted
			{
				get
				{
					return this.ares.IsCompleted;
				}
			}

			// Token: 0x17000AF4 RID: 2804
			// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000A6A2F File Offset: 0x000A4C2F
			public Socket.SendFileHandler Delegate
			{
				get
				{
					return this.d;
				}
			}

			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000A6A37 File Offset: 0x000A4C37
			public IAsyncResult Original
			{
				get
				{
					return this.ares;
				}
			}

			// Token: 0x04001B2C RID: 6956
			private IAsyncResult ares;

			// Token: 0x04001B2D RID: 6957
			private Socket.SendFileHandler d;
		}

		// Token: 0x020005AA RID: 1450
		private struct WSABUF
		{
			// Token: 0x04001B2E RID: 6958
			public int len;

			// Token: 0x04001B2F RID: 6959
			public IntPtr buf;
		}
	}
}
