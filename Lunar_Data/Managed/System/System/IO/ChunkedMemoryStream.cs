using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000808 RID: 2056
	internal sealed class ChunkedMemoryStream : Stream
	{
		// Token: 0x060041EC RID: 16876 RVA: 0x000E5583 File Offset: 0x000E3783
		internal ChunkedMemoryStream()
		{
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000E558C File Offset: 0x000E378C
		public byte[] ToArray()
		{
			byte[] array = new byte[this._totalLength];
			int num = 0;
			for (ChunkedMemoryStream.MemoryChunk memoryChunk = this._headChunk; memoryChunk != null; memoryChunk = memoryChunk._next)
			{
				Buffer.BlockCopy(memoryChunk._buffer, 0, array, num, memoryChunk._freeOffset);
				num += memoryChunk._freeOffset;
			}
			return array;
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000E55D8 File Offset: 0x000E37D8
		public override void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				if (this._currentChunk != null)
				{
					int num = this._currentChunk._buffer.Length - this._currentChunk._freeOffset;
					if (num > 0)
					{
						int num2 = Math.Min(num, count);
						Buffer.BlockCopy(buffer, offset, this._currentChunk._buffer, this._currentChunk._freeOffset, num2);
						count -= num2;
						offset += num2;
						this._totalLength += num2;
						this._currentChunk._freeOffset += num2;
						continue;
					}
				}
				this.AppendChunk((long)count);
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000E5672 File Offset: 0x000E3872
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			this.Write(buffer, offset, count);
			return Task.CompletedTask;
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000E5694 File Offset: 0x000E3894
		private void AppendChunk(long count)
		{
			int num = ((this._currentChunk != null) ? (this._currentChunk._buffer.Length * 2) : 1024);
			if (count > (long)num)
			{
				num = (int)Math.Min(count, 1048576L);
			}
			ChunkedMemoryStream.MemoryChunk memoryChunk = new ChunkedMemoryStream.MemoryChunk(num);
			if (this._currentChunk == null)
			{
				this._headChunk = (this._currentChunk = memoryChunk);
				return;
			}
			this._currentChunk._next = memoryChunk;
			this._currentChunk = memoryChunk;
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x000E5706 File Offset: 0x000E3906
		public override long Length
		{
			get
			{
				return (long)this._totalLength;
			}
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000A1490 File Offset: 0x0009F690
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x060041F8 RID: 16888 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x060041F9 RID: 16889 RVA: 0x000044FA File Offset: 0x000026FA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000E570F File Offset: 0x000E390F
		public override void SetLength(long value)
		{
			if (this._currentChunk != null)
			{
				throw new NotSupportedException();
			}
			this.AppendChunk(value);
		}

		// Token: 0x04002751 RID: 10065
		private ChunkedMemoryStream.MemoryChunk _headChunk;

		// Token: 0x04002752 RID: 10066
		private ChunkedMemoryStream.MemoryChunk _currentChunk;

		// Token: 0x04002753 RID: 10067
		private const int InitialChunkDefaultSize = 1024;

		// Token: 0x04002754 RID: 10068
		private const int MaxChunkSize = 1048576;

		// Token: 0x04002755 RID: 10069
		private int _totalLength;

		// Token: 0x02000809 RID: 2057
		private sealed class MemoryChunk
		{
			// Token: 0x060041FC RID: 16892 RVA: 0x000E5726 File Offset: 0x000E3926
			internal MemoryChunk(int bufferSize)
			{
				this._buffer = new byte[bufferSize];
			}

			// Token: 0x04002756 RID: 10070
			internal readonly byte[] _buffer;

			// Token: 0x04002757 RID: 10071
			internal int _freeOffset;

			// Token: 0x04002758 RID: 10072
			internal ChunkedMemoryStream.MemoryChunk _next;
		}
	}
}
