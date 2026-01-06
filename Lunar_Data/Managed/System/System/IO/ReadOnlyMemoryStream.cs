using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200080B RID: 2059
	internal sealed class ReadOnlyMemoryStream : Stream
	{
		// Token: 0x0600420A RID: 16906 RVA: 0x000E5AB5 File Offset: 0x000E3CB5
		public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
		{
			this._content = content;
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000E5AC4 File Offset: 0x000E3CC4
		public override long Length
		{
			get
			{
				return (long)this._content.Length;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000E5AD2 File Offset: 0x000E3CD2
		// (set) Token: 0x06004210 RID: 16912 RVA: 0x000E5ADB File Offset: 0x000E3CDB
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				if (value < 0L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000E5B00 File Offset: 0x000E3D00
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.Current)
				{
					if (origin != SeekOrigin.End)
					{
						throw new ArgumentOutOfRangeException("origin");
					}
					num = (long)this._content.Length + offset;
				}
				else
				{
					num = (long)this._position + offset;
				}
			}
			else
			{
				num = offset;
			}
			long num2 = num;
			if (num2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (num2 < 0L)
			{
				throw new IOException("An attempt was made to move the position before the beginning of the stream.");
			}
			this._position = (int)num2;
			return (long)this._position;
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000E5B74 File Offset: 0x000E3D74
		public unsafe override int ReadByte()
		{
			ReadOnlySpan<byte> span = this._content.Span;
			if (this._position >= span.Length)
			{
				return -1;
			}
			int position = this._position;
			this._position = position + 1;
			return (int)(*span[position]);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000E5BB7 File Offset: 0x000E3DB7
		public override int Read(byte[] buffer, int offset, int count)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			return this.Read(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000E5BD0 File Offset: 0x000E3DD0
		public override int Read(Span<byte> buffer)
		{
			int num = this._content.Length - this._position;
			if (num <= 0 || buffer.Length == 0)
			{
				return 0;
			}
			if (num <= buffer.Length)
			{
				this._content.Span.Slice(this._position).CopyTo(buffer);
				this._position = this._content.Length;
				return num;
			}
			this._content.Span.Slice(this._position, buffer.Length).CopyTo(buffer);
			this._position += buffer.Length;
			return buffer.Length;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E5C82 File Offset: 0x000E3E82
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult<int>(this.Read(new Span<byte>(buffer, offset, count)));
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000E5CB0 File Offset: 0x000E3EB0
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(this.Read(buffer.Span));
			}
			return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000E5CD9 File Offset: 0x000E3ED9
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(base.ReadAsync(buffer, offset, count), callback, state);
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000E5CF0 File Offset: 0x000E3EF0
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length > this._position)
			{
				destination.Write(this._content.Span.Slice(this._position));
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x000E5D38 File Offset: 0x000E3F38
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length <= this._position)
			{
				return Task.CompletedTask;
			}
			return destination.WriteAsync(this._content.Slice(this._position), cancellationToken).AsTask();
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000A1490 File Offset: 0x0009F690
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000E5D86 File Offset: 0x000E3F86
		private static void ValidateReadArrayArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x04002761 RID: 10081
		private readonly ReadOnlyMemory<byte> _content;

		// Token: 0x04002762 RID: 10082
		private int _position;
	}
}
