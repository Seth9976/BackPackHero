using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A base class representing an HTTP entity body and content headers.</summary>
	// Token: 0x0200001E RID: 30
	public abstract class HttpContent : IDisposable
	{
		/// <summary>Gets the HTTP content headers as defined in RFC 2616.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpContentHeaders" />.The content headers as defined in RFC 2616.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004E20 File Offset: 0x00003020
		public HttpContentHeaders Headers
		{
			get
			{
				HttpContentHeaders httpContentHeaders;
				if ((httpContentHeaders = this.headers) == null)
				{
					httpContentHeaders = (this.headers = new HttpContentHeaders(this));
				}
				return httpContentHeaders;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004E48 File Offset: 0x00003048
		internal long? LoadedBufferLength
		{
			get
			{
				if (this.buffer != null)
				{
					return new long?(this.buffer.Length);
				}
				return null;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004E77 File Offset: 0x00003077
		internal void CopyTo(Stream stream)
		{
			this.CopyToAsync(stream).Wait();
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		// Token: 0x06000107 RID: 263 RVA: 0x00004E85 File Offset: 0x00003085
		public Task CopyToAsync(Stream stream)
		{
			return this.CopyToAsync(stream, null);
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
		// Token: 0x06000108 RID: 264 RVA: 0x00004E8F File Offset: 0x0000308F
		public Task CopyToAsync(Stream stream, TransportContext context)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.buffer != null)
			{
				return this.buffer.CopyToAsync(stream);
			}
			return this.SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a memory stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x06000109 RID: 265 RVA: 0x00004EBC File Offset: 0x000030BC
		protected virtual async Task<Stream> CreateContentReadStreamAsync()
		{
			await this.LoadIntoBufferAsync().ConfigureAwait(false);
			return this.buffer;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004EFF File Offset: 0x000030FF
		private static HttpContent.FixedMemoryStream CreateFixedMemoryStream(long maxBufferSize)
		{
			return new HttpContent.FixedMemoryStream(maxBufferSize);
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpContent" />.</summary>
		// Token: 0x0600010B RID: 267 RVA: 0x00004F07 File Offset: 0x00003107
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x0600010C RID: 268 RVA: 0x00004F10 File Offset: 0x00003110
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.buffer != null)
				{
					this.buffer.Dispose();
				}
			}
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x0600010D RID: 269 RVA: 0x00004F37 File Offset: 0x00003137
		public Task LoadIntoBufferAsync()
		{
			return this.LoadIntoBufferAsync(2147483647L);
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="maxBufferSize">The maximum size, in bytes, of the buffer to use.</param>
		// Token: 0x0600010E RID: 270 RVA: 0x00004F48 File Offset: 0x00003148
		public async Task LoadIntoBufferAsync(long maxBufferSize)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.buffer == null)
			{
				this.buffer = HttpContent.CreateFixedMemoryStream(maxBufferSize);
				await this.SerializeToStreamAsync(this.buffer, null).ConfigureAwait(false);
				this.buffer.Seek(0L, SeekOrigin.Begin);
			}
		}

		/// <summary>Serialize the HTTP content and return a stream that represents the content as an asynchronous operation. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x0600010F RID: 271 RVA: 0x00004F94 File Offset: 0x00003194
		public async Task<Stream> ReadAsStreamAsync()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			Stream stream;
			if (this.buffer != null)
			{
				stream = new MemoryStream(this.buffer.GetBuffer(), 0, (int)this.buffer.Length, false);
			}
			else
			{
				if (this.stream == null)
				{
					Stream stream2 = await this.CreateContentReadStreamAsync().ConfigureAwait(false);
					this.stream = stream2;
				}
				stream = this.stream;
			}
			return stream;
		}

		/// <summary>Serialize the HTTP content to a byte array as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x06000110 RID: 272 RVA: 0x00004FD8 File Offset: 0x000031D8
		public async Task<byte[]> ReadAsByteArrayAsync()
		{
			await this.LoadIntoBufferAsync().ConfigureAwait(false);
			return this.buffer.ToArray();
		}

		/// <summary>Serialize the HTTP content to a string as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x06000111 RID: 273 RVA: 0x0000501C File Offset: 0x0000321C
		public async Task<string> ReadAsStringAsync()
		{
			await this.LoadIntoBufferAsync().ConfigureAwait(false);
			string text;
			if (this.buffer.Length == 0L)
			{
				text = string.Empty;
			}
			else
			{
				byte[] array = this.buffer.GetBuffer();
				int num = (int)this.buffer.Length;
				int num2 = 0;
				Encoding encoding;
				if (this.headers != null && this.headers.ContentType != null && this.headers.ContentType.CharSet != null)
				{
					encoding = Encoding.GetEncoding(this.headers.ContentType.CharSet);
					num2 = HttpContent.StartsWith(array, num, encoding.GetPreamble());
				}
				else
				{
					encoding = HttpContent.GetEncodingFromBuffer(array, num, ref num2) ?? Encoding.UTF8;
				}
				text = encoding.GetString(array, num2, num - num2);
			}
			return text;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005060 File Offset: 0x00003260
		private static Encoding GetEncodingFromBuffer(byte[] buffer, int length, ref int preambleLength)
		{
			foreach (Encoding encoding in new Encoding[]
			{
				Encoding.UTF8,
				Encoding.UTF32,
				Encoding.Unicode
			})
			{
				if ((preambleLength = HttpContent.StartsWith(buffer, length, encoding.GetPreamble())) != 0)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000050B8 File Offset: 0x000032B8
		private static int StartsWith(byte[] array, int length, byte[] value)
		{
			if (length < value.Length)
			{
				return 0;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (array[i] != value[i])
				{
					return 0;
				}
			}
			return value.Length;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000050E8 File Offset: 0x000032E8
		internal Task SerializeToStreamAsync_internal(Stream stream, TransportContext context)
		{
			return this.SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
		// Token: 0x06000115 RID: 277
		protected abstract Task SerializeToStreamAsync(Stream stream, TransportContext context);

		/// <summary>Determines whether the HTTP content has a valid length in bytes.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="length" /> is a valid length; otherwise, false.</returns>
		/// <param name="length">The length in bytes of the HTTP content.</param>
		// Token: 0x06000116 RID: 278
		protected internal abstract bool TryComputeLength(out long length);

		// Token: 0x0400008F RID: 143
		private HttpContent.FixedMemoryStream buffer;

		// Token: 0x04000090 RID: 144
		private Stream stream;

		// Token: 0x04000091 RID: 145
		private bool disposed;

		// Token: 0x04000092 RID: 146
		private HttpContentHeaders headers;

		// Token: 0x0200001F RID: 31
		private sealed class FixedMemoryStream : MemoryStream
		{
			// Token: 0x06000118 RID: 280 RVA: 0x000050F2 File Offset: 0x000032F2
			public FixedMemoryStream(long maxSize)
			{
				this.maxSize = maxSize;
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00005101 File Offset: 0x00003301
			private void CheckOverflow(int count)
			{
				if (this.Length + (long)count > this.maxSize)
				{
					throw new HttpRequestException(string.Format("Cannot write more bytes to the buffer than the configured maximum buffer size: {0}", this.maxSize));
				}
			}

			// Token: 0x0600011A RID: 282 RVA: 0x0000512F File Offset: 0x0000332F
			public override void WriteByte(byte value)
			{
				this.CheckOverflow(1);
				base.WriteByte(value);
			}

			// Token: 0x0600011B RID: 283 RVA: 0x0000513F File Offset: 0x0000333F
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckOverflow(count);
				base.Write(buffer, offset, count);
			}

			// Token: 0x04000093 RID: 147
			private readonly long maxSize;
		}
	}
}
