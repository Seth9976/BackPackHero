using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>Provides HTTP content based on a byte array.</summary>
	// Token: 0x02000011 RID: 17
	public class ByteArrayContent : HttpContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.ByteArrayContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> parameter is null. </exception>
		// Token: 0x060000B7 RID: 183 RVA: 0x000039FF File Offset: 0x00001BFF
		public ByteArrayContent(byte[] content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.content = content;
			this.count = content.Length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.ByteArrayContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <param name="offset">The offset, in bytes, in the <paramref name="content" />  parameter used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <param name="count">The number of bytes in the <paramref name="content" /> starting from the <paramref name="offset" /> parameter used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.-or-The <paramref name="offset" /> parameter is greater than the length of content specified by the <paramref name="content" /> parameter.-or-The <paramref name="count " /> parameter is less than zero.-or-The <paramref name="count" /> parameter is greater than the length of content specified by the <paramref name="content" /> parameter - minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x060000B8 RID: 184 RVA: 0x00003A28 File Offset: 0x00001C28
		public ByteArrayContent(byte[] content, int offset, int count)
			: this(content)
		{
			if (offset < 0 || offset > this.count)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > this.count - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.offset = offset;
			this.count = count;
		}

		/// <summary>Creates an HTTP content stream as an asynchronous operation for reading whose backing store is memory from the <see cref="T:System.Net.Http.ByteArrayContent" />.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		// Token: 0x060000B9 RID: 185 RVA: 0x00003A7C File Offset: 0x00001C7C
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult<Stream>(new MemoryStream(this.content, this.offset, this.count));
		}

		/// <summary>Serialize and write the byte array provided in the constructor to an HTTP content stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />. The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport, like channel binding token. This parameter may be null.</param>
		// Token: 0x060000BA RID: 186 RVA: 0x00003A9A File Offset: 0x00001C9A
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return stream.WriteAsync(this.content, this.offset, this.count);
		}

		/// <summary>Determines whether a byte array has a valid length in bytes.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="length" /> is a valid length; otherwise, false.</returns>
		/// <param name="length">The length in bytes of the byte array.</param>
		// Token: 0x060000BB RID: 187 RVA: 0x00003AB4 File Offset: 0x00001CB4
		protected internal override bool TryComputeLength(out long length)
		{
			length = (long)this.count;
			return true;
		}

		// Token: 0x0400004B RID: 75
		private readonly byte[] content;

		// Token: 0x0400004C RID: 76
		private readonly int offset;

		// Token: 0x0400004D RID: 77
		private readonly int count;
	}
}
