using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>Provides HTTP content based on a stream.</summary>
	// Token: 0x02000030 RID: 48
	public class StreamContent : HttpContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StreamContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StreamContent" />.</param>
		// Token: 0x06000182 RID: 386 RVA: 0x0000680E File Offset: 0x00004A0E
		public StreamContent(Stream content)
			: this(content, 16384)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StreamContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StreamContent" />.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer for the <see cref="T:System.Net.Http.StreamContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was null.</exception>
		/// <exception cref="T:System.OutOfRangeException">The <paramref name="bufferSize" /> was less than or equal to zero. </exception>
		// Token: 0x06000183 RID: 387 RVA: 0x0000681C File Offset: 0x00004A1C
		public StreamContent(Stream content, int bufferSize)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.content = content;
			this.bufferSize = bufferSize;
			if (content.CanSeek)
			{
				this.startPosition = content.Position;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000686E File Offset: 0x00004A6E
		internal StreamContent(Stream content, CancellationToken cancellationToken)
			: this(content)
		{
			this.cancellationToken = cancellationToken;
		}

		/// <summary>Write the HTTP stream content to a memory stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x06000185 RID: 389 RVA: 0x0000687E File Offset: 0x00004A7E
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult<Stream>(this.content);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.StreamContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06000186 RID: 390 RVA: 0x0000688B File Offset: 0x00004A8B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.content.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Serialize the HTTP content to a stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
		// Token: 0x06000187 RID: 391 RVA: 0x000068A4 File Offset: 0x00004AA4
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			if (this.contentCopied)
			{
				if (!this.content.CanSeek)
				{
					throw new InvalidOperationException("The stream was already consumed. It cannot be read again.");
				}
				this.content.Seek(this.startPosition, SeekOrigin.Begin);
			}
			else
			{
				this.contentCopied = true;
			}
			return this.content.CopyToAsync(stream, this.bufferSize, this.cancellationToken);
		}

		/// <summary>Determines whether the stream content has a valid length in bytes.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="length" /> is a valid length; otherwise, false.</returns>
		/// <param name="length">The length in bytes of the stream content.</param>
		// Token: 0x06000188 RID: 392 RVA: 0x00006905 File Offset: 0x00004B05
		protected internal override bool TryComputeLength(out long length)
		{
			if (!this.content.CanSeek)
			{
				length = 0L;
				return false;
			}
			length = this.content.Length - this.startPosition;
			return true;
		}

		// Token: 0x040000D4 RID: 212
		private readonly Stream content;

		// Token: 0x040000D5 RID: 213
		private readonly int bufferSize;

		// Token: 0x040000D6 RID: 214
		private readonly CancellationToken cancellationToken;

		// Token: 0x040000D7 RID: 215
		private readonly long startPosition;

		// Token: 0x040000D8 RID: 216
		private bool contentCopied;
	}
}
