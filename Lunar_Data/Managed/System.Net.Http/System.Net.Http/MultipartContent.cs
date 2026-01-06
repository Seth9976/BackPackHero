using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>Provides a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
	// Token: 0x0200002D RID: 45
	public class MultipartContent : HttpContent, IEnumerable<HttpContent>, IEnumerable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		// Token: 0x06000170 RID: 368 RVA: 0x00005F0E File Offset: 0x0000410E
		public MultipartContent()
			: this("mixed")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was null or contains only white space characters.</exception>
		// Token: 0x06000171 RID: 369 RVA: 0x00005F1C File Offset: 0x0000411C
		public MultipartContent(string subtype)
			: this(subtype, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture))
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <param name="boundary">The boundary string for the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was null or an empty string.The <paramref name="boundary" /> was null or contains only white space characters.-or-The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.OutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		// Token: 0x06000172 RID: 370 RVA: 0x00005F48 File Offset: 0x00004148
		public MultipartContent(string subtype, string boundary)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw new ArgumentException("boundary");
			}
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw new ArgumentException("boundary");
			}
			if (boundary.Length > 70)
			{
				throw new ArgumentOutOfRangeException("boundary");
			}
			if (boundary.Last<char>() == ' ' || !MultipartContent.IsValidRFC2049(boundary))
			{
				throw new ArgumentException("boundary");
			}
			this.boundary = boundary;
			this.nested_content = new List<HttpContent>(2);
			base.Headers.ContentType = new MediaTypeHeaderValue("multipart/" + subtype)
			{
				Parameters = 
				{
					new NameValueHeaderValue("boundary", "\"" + boundary + "\"")
				}
			};
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006008 File Offset: 0x00004208
		private static bool IsValidRFC2049(string s)
		{
			foreach (char c in s)
			{
				if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9'))
				{
					if (c <= ':')
					{
						switch (c)
						{
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
							goto IL_0071;
						case '*':
							break;
						default:
							if (c == ':')
							{
								goto IL_0071;
							}
							break;
						}
					}
					else if (c == '=' || c == '?')
					{
						goto IL_0071;
					}
					return false;
				}
				IL_0071:;
			}
			return true;
		}

		/// <summary>Add multipart HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was null.</exception>
		// Token: 0x06000174 RID: 372 RVA: 0x00006094 File Offset: 0x00004294
		public virtual void Add(HttpContent content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.nested_content == null)
			{
				this.nested_content = new List<HttpContent>();
			}
			this.nested_content.Add(content);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.MultipartContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06000175 RID: 373 RVA: 0x000060C4 File Offset: 0x000042C4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpContent httpContent in this.nested_content)
				{
					httpContent.Dispose();
				}
				this.nested_content = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Serialize the multipart HTTP content to a stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
		// Token: 0x06000176 RID: 374 RVA: 0x00006128 File Offset: 0x00004328
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append('-').Append('-');
			sb.Append(this.boundary);
			sb.Append('\r').Append('\n');
			byte[] array;
			for (int i = 0; i < this.nested_content.Count; i++)
			{
				HttpContent c = this.nested_content[i];
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in c.Headers)
				{
					sb.Append(keyValuePair.Key);
					sb.Append(':').Append(' ');
					foreach (string text in keyValuePair.Value)
					{
						sb.Append(text);
					}
					sb.Append('\r').Append('\n');
				}
				sb.Append('\r').Append('\n');
				array = Encoding.ASCII.GetBytes(sb.ToString());
				sb.Clear();
				await stream.WriteAsync(array, 0, array.Length).ConfigureAwait(false);
				await c.SerializeToStreamAsync_internal(stream, context).ConfigureAwait(false);
				if (i != this.nested_content.Count - 1)
				{
					sb.Append('\r').Append('\n');
					sb.Append('-').Append('-');
					sb.Append(this.boundary);
					sb.Append('\r').Append('\n');
				}
				c = null;
			}
			sb.Append('\r').Append('\n');
			sb.Append('-').Append('-');
			sb.Append(this.boundary);
			sb.Append('-').Append('-');
			sb.Append('\r').Append('\n');
			array = Encoding.ASCII.GetBytes(sb.ToString());
			await stream.WriteAsync(array, 0, array.Length).ConfigureAwait(false);
		}

		/// <summary>Determines whether the HTTP multipart content has a valid length in bytes.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="length" /> is a valid length; otherwise, false.</returns>
		/// <param name="length">The length in bytes of the HHTP content.</param>
		// Token: 0x06000177 RID: 375 RVA: 0x0000617C File Offset: 0x0000437C
		protected internal override bool TryComputeLength(out long length)
		{
			length = (long)(12 + 2 * this.boundary.Length);
			for (int i = 0; i < this.nested_content.Count; i++)
			{
				HttpContent httpContent = this.nested_content[i];
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in httpContent.Headers)
				{
					length += (long)keyValuePair.Key.Length;
					length += 4L;
					foreach (string text in keyValuePair.Value)
					{
						length += (long)text.Length;
					}
				}
				long num;
				if (!httpContent.TryComputeLength(out num))
				{
					return false;
				}
				length += 2L;
				length += num;
				if (i != this.nested_content.Count - 1)
				{
					length += 6L;
					length += (long)this.boundary.Length;
				}
			}
			return true;
		}

		/// <summary>Returns an enumerator that iterates through the collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification..</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerator`1" />.An object that can be used to iterate through the collection.</returns>
		// Token: 0x06000178 RID: 376 RVA: 0x000062A4 File Offset: 0x000044A4
		public IEnumerator<HttpContent> GetEnumerator()
		{
			return this.nested_content.GetEnumerator();
		}

		/// <summary>The explicit implementation of the <see cref="M:System.Net.Http.MultipartContent.GetEnumerator" /> method.</summary>
		/// <returns>Returns <see cref="T:System.Collections.IEnumerator" />.An object that can be used to iterate through the collection.</returns>
		// Token: 0x06000179 RID: 377 RVA: 0x000062A4 File Offset: 0x000044A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.nested_content.GetEnumerator();
		}

		// Token: 0x040000C9 RID: 201
		private List<HttpContent> nested_content;

		// Token: 0x040000CA RID: 202
		private readonly string boundary;
	}
}
