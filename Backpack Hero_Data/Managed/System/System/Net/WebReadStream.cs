using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004DC RID: 1244
	internal abstract class WebReadStream : Stream
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x0009638A File Offset: 0x0009458A
		public WebOperation Operation { get; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00096392 File Offset: 0x00094592
		protected Stream InnerStream { get; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00002F6A File Offset: 0x0000116A
		internal string ME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0009639A File Offset: 0x0009459A
		public WebReadStream(WebOperation operation, Stream innerStream)
		{
			this.Operation = operation;
			this.InnerStream = innerStream;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000963B0 File Offset: 0x000945B0
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

		// Token: 0x06002876 RID: 10358 RVA: 0x000963E8 File Offset: 0x000945E8
		public override int Read(byte[] buffer, int offset, int size)
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
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, size, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00096480 File Offset: 0x00094680
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
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
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, size, CancellationToken.None), cb, state);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000964FC File Offset: 0x000946FC
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

		// Token: 0x06002879 RID: 10361 RVA: 0x0009653C File Offset: 0x0009473C
		public sealed override async Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			this.Operation.ThrowIfDisposed(cancellationToken);
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int num3;
			try
			{
				int num2 = await this.ProcessReadAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (num2 != 0)
				{
					num3 = num2;
				}
				else
				{
					await this.FinishReading(cancellationToken).ConfigureAwait(false);
					num3 = 0;
				}
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
			}
			return num3;
		}

		// Token: 0x0600287A RID: 10362
		protected abstract Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken);

		// Token: 0x0600287B RID: 10363 RVA: 0x000965A0 File Offset: 0x000947A0
		internal virtual Task FinishReading(CancellationToken cancellationToken)
		{
			this.Operation.ThrowIfDisposed(cancellationToken);
			WebReadStream webReadStream = this.InnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000965D5 File Offset: 0x000947D5
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.InnerStream != null)
				{
					this.InnerStream.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001793 RID: 6035
		private bool disposed;
	}
}
