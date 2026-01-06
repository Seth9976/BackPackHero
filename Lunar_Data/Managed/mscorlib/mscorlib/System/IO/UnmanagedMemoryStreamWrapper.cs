using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000B2A RID: 2858
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x060066B9 RID: 26297 RVA: 0x0015EF3C File Offset: 0x0015D13C
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x0015EF4B File Offset: 0x0015D14B
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x0015EF58 File Offset: 0x0015D158
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x0015EF65 File Offset: 0x0015D165
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x0015EF74 File Offset: 0x0015D174
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0015EFAC File Offset: 0x0015D1AC
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0015EFB9 File Offset: 0x0015D1B9
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x0015EFC5 File Offset: 0x0015D1C5
		public override bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			buffer = default(ArraySegment<byte>);
			return false;
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x060066C1 RID: 26305 RVA: 0x0015EFCF File Offset: 0x0015D1CF
		// (set) Token: 0x060066C2 RID: 26306 RVA: 0x0015EFDD File Offset: 0x0015D1DD
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060066C3 RID: 26307 RVA: 0x0015EFE9 File Offset: 0x0015D1E9
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060066C4 RID: 26308 RVA: 0x0015EFF6 File Offset: 0x0015D1F6
		// (set) Token: 0x060066C5 RID: 26309 RVA: 0x0015F003 File Offset: 0x0015D203
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0015F011 File Offset: 0x0015D211
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x0015F021 File Offset: 0x0015D221
		public override int Read(Span<byte> buffer)
		{
			return this._unmanagedStream.Read(buffer);
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x0015F02F File Offset: 0x0015D22F
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x0015F03C File Offset: 0x0015D23C
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x0015F04C File Offset: 0x0015D24C
		public override byte[] ToArray()
		{
			byte[] array = new byte[this._unmanagedStream.Length];
			this._unmanagedStream.Read(array, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x0015F086 File Offset: 0x0015D286
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x0015F096 File Offset: 0x0015D296
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._unmanagedStream.Write(buffer);
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x0015F0A4 File Offset: 0x0015D2A4
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x0015F0B4 File Offset: 0x0015D2B4
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Stream cannot be null.");
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x0015F0E6 File Offset: 0x0015D2E6
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x0015F0F0 File Offset: 0x0015D2F0
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", "Cannot access a closed Stream.");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x0015F18F File Offset: 0x0015D38F
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x0015F19D File Offset: 0x0015D39D
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x0015F1AF File Offset: 0x0015D3AF
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0015F1BE File Offset: 0x0015D3BE
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x0015F1D0 File Offset: 0x0015D3D0
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x04003C1D RID: 15389
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
