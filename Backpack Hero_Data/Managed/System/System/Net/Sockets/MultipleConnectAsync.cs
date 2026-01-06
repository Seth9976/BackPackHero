using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x0200059B RID: 1435
	internal abstract class MultipleConnectAsync
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x000A0260 File Offset: 0x0009E460
		public bool StartConnectAsync(SocketAsyncEventArgs args, DnsEndPoint endPoint)
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				if (endPoint.AddressFamily != AddressFamily.Unspecified && endPoint.AddressFamily != AddressFamily.InterNetwork && endPoint.AddressFamily != AddressFamily.InterNetworkV6)
				{
					NetEventSource.Fail(this, FormattableStringFactory.Create("Unexpected endpoint address family: {0}", new object[] { endPoint.AddressFamily }), "StartConnectAsync");
				}
				this._userArgs = args;
				this._endPoint = endPoint;
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					this.SyncFail(new SocketException(995));
					flag2 = false;
				}
				else
				{
					if (this._state != MultipleConnectAsync.State.NotStarted)
					{
						NetEventSource.Fail(this, "MultipleConnectAsync.StartConnectAsync(): Unexpected object state", "StartConnectAsync");
					}
					this._state = MultipleConnectAsync.State.DnsQuery;
					IAsyncResult asyncResult = Dns.BeginGetHostAddresses(endPoint.Host, new AsyncCallback(this.DnsCallback), null);
					if (asyncResult.CompletedSynchronously)
					{
						flag2 = this.DoDnsCallback(asyncResult, true);
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000A0358 File Offset: 0x0009E558
		private void DnsCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.DoDnsCallback(result, false);
			}
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000A036C File Offset: 0x0009E56C
		private bool DoDnsCallback(IAsyncResult result, bool sync)
		{
			Exception ex = null;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					return true;
				}
				if (this._state != MultipleConnectAsync.State.DnsQuery)
				{
					NetEventSource.Fail(this, "MultipleConnectAsync.DoDnsCallback(): Unexpected object state", "DoDnsCallback");
				}
				try
				{
					this._addressList = Dns.EndGetHostAddresses(result);
					if (this._addressList == null)
					{
						NetEventSource.Fail(this, "MultipleConnectAsync.DoDnsCallback(): EndGetHostAddresses returned null!", "DoDnsCallback");
					}
				}
				catch (Exception ex2)
				{
					this._state = MultipleConnectAsync.State.Completed;
					ex = ex2;
				}
				if (ex == null)
				{
					this._state = MultipleConnectAsync.State.ConnectAttempt;
					this._internalArgs = new SocketAsyncEventArgs();
					this._internalArgs.Completed += this.InternalConnectCallback;
					this._internalArgs.CopyBufferFrom(this._userArgs);
					ex = this.AttemptConnection();
					if (ex != null)
					{
						this._state = MultipleConnectAsync.State.Completed;
					}
				}
			}
			return ex == null || this.Fail(sync, ex);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000A046C File Offset: 0x0009E66C
		private void InternalConnectCallback(object sender, SocketAsyncEventArgs args)
		{
			Exception ex = null;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					ex = new SocketException(995);
				}
				else if (args.SocketError == SocketError.Success)
				{
					this._state = MultipleConnectAsync.State.Completed;
				}
				else if (args.SocketError == SocketError.OperationAborted)
				{
					ex = new SocketException(995);
					this._state = MultipleConnectAsync.State.Canceled;
				}
				else
				{
					SocketError socketError = args.SocketError;
					args.in_progress = 0;
					Exception ex2 = this.AttemptConnection();
					if (ex2 == null)
					{
						return;
					}
					SocketException ex3 = ex2 as SocketException;
					if (ex3 != null && ex3.SocketErrorCode == SocketError.NoData)
					{
						ex = new SocketException((int)socketError);
					}
					else
					{
						ex = ex2;
					}
					this._state = MultipleConnectAsync.State.Completed;
				}
			}
			if (ex == null)
			{
				this.Succeed();
				return;
			}
			this.AsyncFail(ex);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000A0550 File Offset: 0x0009E750
		private Exception AttemptConnection()
		{
			Exception ex;
			try
			{
				Socket socket;
				IPAddress nextAddress = this.GetNextAddress(out socket);
				if (nextAddress == null)
				{
					ex = new SocketException(11004);
				}
				else
				{
					this._internalArgs.RemoteEndPoint = new IPEndPoint(nextAddress, this._endPoint.Port);
					ex = this.AttemptConnection(socket, this._internalArgs);
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is ObjectDisposedException)
				{
					NetEventSource.Fail(this, "unexpected ObjectDisposedException", "AttemptConnection");
				}
				ex = ex2;
			}
			return ex;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000A05D0 File Offset: 0x0009E7D0
		private Exception AttemptConnection(Socket attemptSocket, SocketAsyncEventArgs args)
		{
			try
			{
				if (attemptSocket == null)
				{
					NetEventSource.Fail(null, "attemptSocket is null!", "AttemptConnection");
				}
				if (!attemptSocket.ConnectAsync(args))
				{
					this.InternalConnectCallback(null, args);
				}
			}
			catch (ObjectDisposedException)
			{
				return new SocketException(995);
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}

		// Token: 0x06002D4F RID: 11599
		protected abstract void OnSucceed();

		// Token: 0x06002D50 RID: 11600 RVA: 0x000A0634 File Offset: 0x0009E834
		private void Succeed()
		{
			this.OnSucceed();
			this._userArgs.FinishWrapperConnectSuccess(this._internalArgs.ConnectSocket, this._internalArgs.BytesTransferred, this._internalArgs.SocketFlags);
			this._internalArgs.Dispose();
		}

		// Token: 0x06002D51 RID: 11601
		protected abstract void OnFail(bool abortive);

		// Token: 0x06002D52 RID: 11602 RVA: 0x000A0673 File Offset: 0x0009E873
		private bool Fail(bool sync, Exception e)
		{
			if (sync)
			{
				this.SyncFail(e);
				return false;
			}
			this.AsyncFail(e);
			return true;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000A068C File Offset: 0x0009E88C
		private void SyncFail(Exception e)
		{
			this.OnFail(false);
			if (this._internalArgs != null)
			{
				this._internalArgs.Dispose();
			}
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				this._userArgs.FinishConnectByNameSyncFailure(ex, 0, SocketFlags.None);
				return;
			}
			ExceptionDispatchInfo.Throw(e);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000A06D2 File Offset: 0x0009E8D2
		private void AsyncFail(Exception e)
		{
			this.OnFail(false);
			if (this._internalArgs != null)
			{
				this._internalArgs.Dispose();
			}
			this._userArgs.FinishOperationAsyncFailure(e, 0, SocketFlags.None);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000A06FC File Offset: 0x0009E8FC
		public void Cancel()
		{
			bool flag = false;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				switch (this._state)
				{
				case MultipleConnectAsync.State.NotStarted:
					flag = true;
					break;
				case MultipleConnectAsync.State.DnsQuery:
					Task.Factory.StartNew(delegate(object s)
					{
						this.CallAsyncFail(s);
					}, null, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
					flag = true;
					break;
				case MultipleConnectAsync.State.ConnectAttempt:
					flag = true;
					break;
				case MultipleConnectAsync.State.Completed:
					break;
				default:
					NetEventSource.Fail(this, "Unexpected object state", "Cancel");
					break;
				}
				this._state = MultipleConnectAsync.State.Canceled;
			}
			if (flag)
			{
				this.OnFail(true);
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000A07A8 File Offset: 0x0009E9A8
		private void CallAsyncFail(object ignored)
		{
			this.AsyncFail(new SocketException(995));
		}

		// Token: 0x06002D57 RID: 11607
		protected abstract IPAddress GetNextAddress(out Socket attemptSocket);

		// Token: 0x04001ACD RID: 6861
		protected SocketAsyncEventArgs _userArgs;

		// Token: 0x04001ACE RID: 6862
		protected SocketAsyncEventArgs _internalArgs;

		// Token: 0x04001ACF RID: 6863
		protected DnsEndPoint _endPoint;

		// Token: 0x04001AD0 RID: 6864
		protected IPAddress[] _addressList;

		// Token: 0x04001AD1 RID: 6865
		protected int _nextAddress;

		// Token: 0x04001AD2 RID: 6866
		private MultipleConnectAsync.State _state;

		// Token: 0x04001AD3 RID: 6867
		private object _lockObject = new object();

		// Token: 0x0200059C RID: 1436
		private enum State
		{
			// Token: 0x04001AD5 RID: 6869
			NotStarted,
			// Token: 0x04001AD6 RID: 6870
			DnsQuery,
			// Token: 0x04001AD7 RID: 6871
			ConnectAttempt,
			// Token: 0x04001AD8 RID: 6872
			Completed,
			// Token: 0x04001AD9 RID: 6873
			Canceled
		}
	}
}
