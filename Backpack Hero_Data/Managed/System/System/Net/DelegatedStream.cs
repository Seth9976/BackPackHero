using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000387 RID: 903
	internal class DelegatedStream : Stream
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x0006C2DD File Offset: 0x0006A4DD
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._netStream = stream as NetworkStream;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0006C306 File Offset: 0x0006A506
		protected Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0006C30E File Offset: 0x0006A50E
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0006C31B File Offset: 0x0006A51B
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0006C328 File Offset: 0x0006A528
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0006C335 File Offset: 0x0006A535
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Length;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0006C355 File Offset: 0x0006A555
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0006C375 File Offset: 0x0006A575
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				this._stream.Position = value;
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0006C398 File Offset: 0x0006A598
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			IAsyncResult asyncResult;
			if (this._netStream != null)
			{
				asyncResult = this._netStream.BeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this._stream.BeginRead(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			IAsyncResult asyncResult;
			if (this._netStream != null)
			{
				asyncResult = this._netStream.BeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this._stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0006C43F File Offset: 0x0006A63F
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006C44C File Offset: 0x0006A64C
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.EndRead(asyncResult);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0006C46D File Offset: 0x0006A66D
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0006C48E File Offset: 0x0006A68E
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0006C49B File Offset: 0x0006A69B
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006C4A9 File Offset: 0x0006A6A9
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.Read(buffer, offset, count);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0006C4F1 File Offset: 0x0006A6F1
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			return this._stream.Seek(offset, origin);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0006C513 File Offset: 0x0006A713
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			this._stream.SetLength(value);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006C534 File Offset: 0x0006A734
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006C557 File Offset: 0x0006A757
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04000F65 RID: 3941
		private readonly Stream _stream;

		// Token: 0x04000F66 RID: 3942
		private readonly NetworkStream _netStream;
	}
}
