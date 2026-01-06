using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200064C RID: 1612
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000BB643 File Offset: 0x000B9843
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000BB650 File Offset: 0x000B9850
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000BB65D File Offset: 0x000B985D
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000BB66A File Offset: 0x000B986A
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000BB677 File Offset: 0x000B9877
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x000BB684 File Offset: 0x000B9884
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x000BB692 File Offset: 0x000B9892
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x000BB69F File Offset: 0x000B989F
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000BB6AD File Offset: 0x000B98AD
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000BB6BA File Offset: 0x000B98BA
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000BB6C7 File Offset: 0x000B98C7
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000BB6D5 File Offset: 0x000B98D5
		protected DelegatingStream(Stream innerStream)
		{
			this._innerStream = innerStream;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000BB6E4 File Offset: 0x000B98E4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000BB6FB File Offset: 0x000B98FB
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000BB70A File Offset: 0x000B990A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000BB71A File Offset: 0x000B991A
		public override int Read(Span<byte> buffer)
		{
			return this._innerStream.Read(buffer);
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000BB728 File Offset: 0x000B9928
		public override int ReadByte()
		{
			return this._innerStream.ReadByte();
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000BB735 File Offset: 0x000B9935
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000BB747 File Offset: 0x000B9947
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000BB756 File Offset: 0x000B9956
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000BB76A File Offset: 0x000B996A
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._innerStream.EndRead(asyncResult);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000BB778 File Offset: 0x000B9978
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000BB785 File Offset: 0x000B9985
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000BB793 File Offset: 0x000B9993
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000BB7A1 File Offset: 0x000B99A1
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000BB7B1 File Offset: 0x000B99B1
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._innerStream.Write(buffer);
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000BB7BF File Offset: 0x000B99BF
		public override void WriteByte(byte value)
		{
			this._innerStream.WriteByte(value);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000BB7CD File Offset: 0x000B99CD
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000BB7DF File Offset: 0x000B99DF
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000BB7EE File Offset: 0x000B99EE
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000BB802 File Offset: 0x000B9A02
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._innerStream.EndWrite(asyncResult);
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000BB810 File Offset: 0x000B9A10
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this._innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x04001F86 RID: 8070
		private readonly Stream _innerStream;
	}
}
