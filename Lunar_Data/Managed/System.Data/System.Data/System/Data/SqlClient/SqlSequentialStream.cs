using System;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001D4 RID: 468
	internal sealed class SqlSequentialStream : Stream
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
		internal SqlSequentialStream(SqlDataReader reader, int columnIndex)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
			if (reader.Command != null && reader.Command.CommandTimeout != 0)
			{
				this._readTimeout = (int)Math.Min((long)reader.Command.CommandTimeout * 1000L, 2147483647L);
				return;
			}
			this._readTimeout = -1;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0006E86D File Offset: 0x0006CA6D
		public override bool CanRead
		{
			get
			{
				return this._reader != null && !this._reader.IsClosed;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void Flush()
		{
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00060F32 File Offset: 0x0005F132
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0006E887 File Offset: 0x0006CA87
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x0006E88F File Offset: 0x0006CA8F
		public override int ReadTimeout
		{
			get
			{
				return this._readTimeout;
			}
			set
			{
				if (value > 0 || value == -1)
				{
					this._readTimeout = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("value");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0006E8AB File Offset: 0x0006CAAB
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int bytesInternalSequential;
			try
			{
				bytesInternalSequential = this._reader.GetBytesInternalSequential(this._columnIndex, buffer, offset, count, new long?((long)this._readTimeout));
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return bytesInternalSequential;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006E924 File Offset: 0x0006CB24
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (!this.CanRead)
			{
				completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
			}
			else
			{
				try
				{
					if (Interlocked.CompareExchange<Task>(ref this._currentTask, completion.Task, null) != null)
					{
						completion.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					}
					else
					{
						CancellationTokenSource combinedTokenSource;
						if (!cancellationToken.CanBeCanceled)
						{
							combinedTokenSource = this._disposalTokenSource;
						}
						else
						{
							combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this._disposalTokenSource.Token);
						}
						int num = 0;
						Task<int> task = null;
						SqlDataReader reader = this._reader;
						if (reader != null && !cancellationToken.IsCancellationRequested && !this._disposalTokenSource.Token.IsCancellationRequested)
						{
							task = reader.GetBytesAsync(this._columnIndex, buffer, offset, count, this._readTimeout, combinedTokenSource.Token, out num);
						}
						if (task == null)
						{
							this._currentTask = null;
							if (cancellationToken.IsCancellationRequested)
							{
								completion.SetCanceled();
							}
							else if (!this.CanRead)
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
							else
							{
								completion.SetResult(num);
							}
							if (combinedTokenSource != this._disposalTokenSource)
							{
								combinedTokenSource.Dispose();
							}
						}
						else
						{
							task.ContinueWith(delegate(Task<int> t)
							{
								this._currentTask = null;
								if (t.Status == TaskStatus.RanToCompletion && this.CanRead)
								{
									completion.SetResult(t.Result);
								}
								else if (t.Status == TaskStatus.Faulted)
								{
									if (t.Exception.InnerException is SqlException)
									{
										completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
									}
									else
									{
										completion.SetException(t.Exception.InnerException);
									}
								}
								else if (!this.CanRead)
								{
									completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
								}
								else
								{
									completion.SetCanceled();
								}
								if (combinedTokenSource != this._disposalTokenSource)
								{
									combinedTokenSource.Dispose();
								}
							}, TaskScheduler.Default);
						}
					}
				}
				catch (Exception ex)
				{
					completion.TrySetException(ex);
					Interlocked.CompareExchange<Task>(ref this._currentTask, null, completion.Task);
					throw;
				}
			}
			return completion.Task;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0006EAFC File Offset: 0x0006CCFC
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0006EB15 File Offset: 0x0006CD15
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00060F32 File Offset: 0x0005F132
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00060F32 File Offset: 0x0005F132
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0006EB20 File Offset: 0x0006CD20
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0006EB55 File Offset: 0x0006CD55
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0006EB68 File Offset: 0x0006CD68
		internal static void ValidateReadParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw ADP.ArgumentOutOfRange("offset");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(offset + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x04000EE8 RID: 3816
		private SqlDataReader _reader;

		// Token: 0x04000EE9 RID: 3817
		private int _columnIndex;

		// Token: 0x04000EEA RID: 3818
		private Task _currentTask;

		// Token: 0x04000EEB RID: 3819
		private int _readTimeout;

		// Token: 0x04000EEC RID: 3820
		private CancellationTokenSource _disposalTokenSource;
	}
}
