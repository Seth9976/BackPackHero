using System;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	/// <summary>Provides HTTP content based on a string.</summary>
	// Token: 0x02000031 RID: 49
	public class StringContent : ByteArrayContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		// Token: 0x06000189 RID: 393 RVA: 0x0000692F File Offset: 0x00004B2F
		public StringContent(string content)
			: this(content, null, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		// Token: 0x0600018A RID: 394 RVA: 0x0000693A File Offset: 0x00004B3A
		public StringContent(string content, Encoding encoding)
			: this(content, encoding, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		/// <param name="mediaType">The media type to use for the content.</param>
		// Token: 0x0600018B RID: 395 RVA: 0x00006945 File Offset: 0x00004B45
		public StringContent(string content, Encoding encoding, string mediaType)
			: base(StringContent.GetByteArray(content, encoding))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue(mediaType ?? "text/plain")
			{
				CharSet = (encoding ?? Encoding.UTF8).WebName
			};
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006983 File Offset: 0x00004B83
		private static byte[] GetByteArray(string content, Encoding encoding)
		{
			return (encoding ?? Encoding.UTF8).GetBytes(content);
		}
	}
}
