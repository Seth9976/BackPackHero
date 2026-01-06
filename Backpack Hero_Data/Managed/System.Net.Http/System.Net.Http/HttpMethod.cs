using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	/// <summary>A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods.</summary>
	// Token: 0x02000027 RID: 39
	public class HttpMethod : IEquatable<HttpMethod>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpMethod" /> class with a specific HTTP method.</summary>
		/// <param name="method">The HTTP method.</param>
		// Token: 0x0600012F RID: 303 RVA: 0x0000575A File Offset: 0x0000395A
		public HttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				throw new ArgumentException("method");
			}
			Parser.Token.Check(method);
			this.method = method;
		}

		/// <summary>Represents an HTTP DELETE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005782 File Offset: 0x00003982
		public static HttpMethod Delete
		{
			get
			{
				return HttpMethod.delete_method;
			}
		}

		/// <summary>Represents an HTTP GET protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005789 File Offset: 0x00003989
		public static HttpMethod Get
		{
			get
			{
				return HttpMethod.get_method;
			}
		}

		/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005790 File Offset: 0x00003990
		public static HttpMethod Head
		{
			get
			{
				return HttpMethod.head_method;
			}
		}

		/// <summary>An HTTP method. </summary>
		/// <returns>Returns <see cref="T:System.String" />.An HTTP method represented as a <see cref="T:System.String" />.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005797 File Offset: 0x00003997
		public string Method
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Represents an HTTP OPTIONS protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000579F File Offset: 0x0000399F
		public static HttpMethod Options
		{
			get
			{
				return HttpMethod.options_method;
			}
		}

		/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000057A6 File Offset: 0x000039A6
		public static HttpMethod Post
		{
			get
			{
				return HttpMethod.post_method;
			}
		}

		/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000057AD File Offset: 0x000039AD
		public static HttpMethod Put
		{
			get
			{
				return HttpMethod.put_method;
			}
		}

		/// <summary>Represents an HTTP TRACE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000057B4 File Offset: 0x000039B4
		public static HttpMethod Trace
		{
			get
			{
				return HttpMethod.trace_method;
			}
		}

		/// <summary>The equality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <paramref name="left" /> and <paramref name="right" /> parameters are equal; otherwise, false.</returns>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		// Token: 0x06000138 RID: 312 RVA: 0x000057BB File Offset: 0x000039BB
		public static bool operator ==(HttpMethod left, HttpMethod right)
		{
			if (left == null || right == null)
			{
				return left == right;
			}
			return left.Equals(right);
		}

		/// <summary>The inequality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <paramref name="left" /> and <paramref name="right" /> parameters are inequal; otherwise, false.</returns>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		// Token: 0x06000139 RID: 313 RVA: 0x000057CF File Offset: 0x000039CF
		public static bool operator !=(HttpMethod left, HttpMethod right)
		{
			return !(left == right);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Net.Http.HttpMethod" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified object is equal to the current object; otherwise, false.</returns>
		/// <param name="other">The HTTP method to compare with the current object.</param>
		// Token: 0x0600013A RID: 314 RVA: 0x000057DB File Offset: 0x000039DB
		public bool Equals(HttpMethod other)
		{
			return string.Equals(this.method, other.method, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified object is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x0600013B RID: 315 RVA: 0x000057F0 File Offset: 0x000039F0
		public override bool Equals(object obj)
		{
			HttpMethod httpMethod = obj as HttpMethod;
			return httpMethod != null && this.Equals(httpMethod);
		}

		/// <summary>Serves as a hash function for this type.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600013C RID: 316 RVA: 0x00005810 File Offset: 0x00003A10
		public override int GetHashCode()
		{
			return this.method.GetHashCode();
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string representing the current object.</returns>
		// Token: 0x0600013D RID: 317 RVA: 0x00005797 File Offset: 0x00003997
		public override string ToString()
		{
			return this.method;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000027DD File Offset: 0x000009DD
		public static HttpMethod Patch
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x040000AB RID: 171
		private static readonly HttpMethod delete_method = new HttpMethod("DELETE");

		// Token: 0x040000AC RID: 172
		private static readonly HttpMethod get_method = new HttpMethod("GET");

		// Token: 0x040000AD RID: 173
		private static readonly HttpMethod head_method = new HttpMethod("HEAD");

		// Token: 0x040000AE RID: 174
		private static readonly HttpMethod options_method = new HttpMethod("OPTIONS");

		// Token: 0x040000AF RID: 175
		private static readonly HttpMethod post_method = new HttpMethod("POST");

		// Token: 0x040000B0 RID: 176
		private static readonly HttpMethod put_method = new HttpMethod("PUT");

		// Token: 0x040000B1 RID: 177
		private static readonly HttpMethod trace_method = new HttpMethod("TRACE");

		// Token: 0x040000B2 RID: 178
		private readonly string method;
	}
}
