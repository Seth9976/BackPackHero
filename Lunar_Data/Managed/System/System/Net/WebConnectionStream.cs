using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004D4 RID: 1236
	internal abstract class WebConnectionStream : Stream
	{
		// Token: 0x0600280A RID: 10250 RVA: 0x00094CDC File Offset: 0x00092EDC
		protected WebConnectionStream(WebConnection cnc, WebOperation operation)
		{
			this.Connection = cnc;
			this.Operation = operation;
			this.Request = operation.Request;
			this.read_timeout = this.Request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x00094D31 File Offset: 0x00092F31
		internal HttpWebRequest Request { get; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00094D39 File Offset: 0x00092F39
		internal WebConnection Connection { get; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x00094D41 File Offset: 0x00092F41
		internal WebOperation Operation { get; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00094D49 File Offset: 0x00092F49
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.Connection.ServicePoint;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x00094D56 File Offset: 0x00092F56
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x00094D5E File Offset: 0x00092F5E
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x00094D76 File Offset: 0x00092F76
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x00094D7E File Offset: 0x00092F7E
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00094D96 File Offset: 0x00092F96
		protected Exception GetException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			if (e is WebException)
			{
				return e;
			}
			if (this.Operation.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return e;
		}

		// Token: 0x06002815 RID: 10261
		protected abstract bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result);

		// Token: 0x06002816 RID: 10262 RVA: 0x00094DD0 File Offset: 0x00092FD0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num2;
			if (this.TryReadFromBufferedContent(buffer, offset, count, out num2))
			{
				return num2;
			}
			this.Operation.ThrowIfClosedOrDisposed();
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, count, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00094E74 File Offset: 0x00093074
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00094EF0 File Offset: 0x000930F0
		public override int EndRead(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			int num;
			try
			{
				num = TaskToApm.End<int>(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return num;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00094F30 File Offset: 0x00093130
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00094FAC File Offset: 0x000931AC
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			try
			{
				TaskToApm.End(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x00094FEC File Offset: 0x000931EC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			try
			{
				base.WriteAsync(buffer, offset, count).Wait();
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0009507C File Offset: 0x0009327C
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00095093 File Offset: 0x00093293
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x0600281F RID: 10271
		protected abstract void Close_internal(ref bool disposed);

		// Token: 0x06002820 RID: 10272 RVA: 0x0009509C File Offset: 0x0009329C
		public override void Close()
		{
			this.Close_internal(ref this.disposed);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000950AA File Offset: 0x000932AA
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000950AA File Offset: 0x000932AA
		public override void SetLength(long a)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x000950AA File Offset: 0x000932AA
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x000950AA File Offset: 0x000932AA
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x000950AA File Offset: 0x000932AA
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x04001749 RID: 5961
		protected bool closed;

		// Token: 0x0400174A RID: 5962
		private bool disposed;

		// Token: 0x0400174B RID: 5963
		private object locker = new object();

		// Token: 0x0400174C RID: 5964
		private int read_timeout;

		// Token: 0x0400174D RID: 5965
		private int write_timeout;

		// Token: 0x0400174E RID: 5966
		internal bool IgnoreIOErrors;
	}
}
