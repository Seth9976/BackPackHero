using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a media type used in a Content-Type header as defined in the RFC 2616.</summary>
	// Token: 0x0200004E RID: 78
	public class MediaTypeHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="mediaType">The source represented as a string to initialize the new instance. </param>
		// Token: 0x060002FE RID: 766 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public MediaTypeHeaderValue(string mediaType)
		{
			this.MediaType = mediaType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="source"> A <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x060002FF RID: 767 RVA: 0x0000AD4C File Offset: 0x00008F4C
		protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.media_type = source.media_type;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add(new NameValueHeaderValue(nameValueHeaderValue));
				}
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00002300 File Offset: 0x00000500
		internal MediaTypeHeaderValue()
		{
		}

		/// <summary>Gets or sets the character set.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The character set.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000AE21 File Offset: 0x00009021
		public string CharSet
		{
			get
			{
				if (this.parameters == null)
				{
					return null;
				}
				NameValueHeaderValue nameValueHeaderValue = this.parameters.Find((NameValueHeaderValue l) => string.Equals(l.Name, "charset", StringComparison.OrdinalIgnoreCase));
				if (nameValueHeaderValue == null)
				{
					return null;
				}
				return nameValueHeaderValue.Value;
			}
			set
			{
				if (this.parameters == null)
				{
					this.parameters = new List<NameValueHeaderValue>();
				}
				this.parameters.SetValue("charset", value);
			}
		}

		/// <summary>Gets or sets the media-type header value.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The media-type header value.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000AE47 File Offset: 0x00009047
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000AE50 File Offset: 0x00009050
		public string MediaType
		{
			get
			{
				return this.media_type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MediaType");
				}
				string text;
				Token? token = MediaTypeHeaderValue.TryParseMediaType(new Lexer(value), out text);
				if (token == null || token.Value.Kind != Token.Type.End)
				{
					throw new FormatException();
				}
				this.media_type = text;
			}
		}

		/// <summary>Gets or sets the media-type header value parameters.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The media-type header value parameters.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
				List<NameValueHeaderValue> list;
				if ((list = this.parameters) == null)
				{
					list = (this.parameters = new List<NameValueHeaderValue>());
				}
				return list;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000306 RID: 774 RVA: 0x0000AEC9 File Offset: 0x000090C9
		object ICloneable.Clone()
		{
			return new MediaTypeHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000307 RID: 775 RVA: 0x0000AED4 File Offset: 0x000090D4
		public override bool Equals(object obj)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = obj as MediaTypeHeaderValue;
			return mediaTypeHeaderValue != null && string.Equals(mediaTypeHeaderValue.media_type, this.media_type, StringComparison.OrdinalIgnoreCase) && mediaTypeHeaderValue.parameters.SequenceEqual(this.parameters);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000308 RID: 776 RVA: 0x0000AF14 File Offset: 0x00009114
		public override int GetHashCode()
		{
			return this.media_type.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" />.An <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents media type header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type header value information.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x0000AF34 File Offset: 0x00009134
		public static MediaTypeHeaderValue Parse(string input)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue;
			if (MediaTypeHeaderValue.TryParse(input, out mediaTypeHeaderValue))
			{
				return mediaTypeHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600030A RID: 778 RVA: 0x0000AF53 File Offset: 0x00009153
		public override string ToString()
		{
			if (this.parameters == null)
			{
				return this.media_type;
			}
			return this.media_type + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> version of the string.</param>
		// Token: 0x0600030B RID: 779 RVA: 0x0000AF7C File Offset: 0x0000917C
		public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			List<NameValueHeaderValue> list = null;
			string text;
			Token? token = MediaTypeHeaderValue.TryParseMediaType(lexer, out text);
			if (token == null)
			{
				return false;
			}
			Token.Type kind = token.Value.Kind;
			if (kind != Token.Type.End)
			{
				if (kind != Token.Type.SeparatorSemicolon)
				{
					return false;
				}
				Token token2;
				if (!NameValueHeaderValue.TryParseParameters(lexer, out list, out token2) || token2 != Token.Type.End)
				{
					return false;
				}
			}
			parsedValue = new MediaTypeHeaderValue
			{
				media_type = text,
				parameters = list
			};
			return true;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AFF8 File Offset: 0x000091F8
		internal static Token? TryParseMediaType(Lexer lexer, out string media)
		{
			media = null;
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return null;
			}
			if (lexer.Scan(false) != Token.Type.SeparatorSlash)
			{
				return null;
			}
			Token token2 = lexer.Scan(false);
			if (token2 != Token.Type.Token)
			{
				return null;
			}
			media = lexer.GetStringValue(token) + "/" + lexer.GetStringValue(token2);
			return new Token?(lexer.Scan(false));
		}

		// Token: 0x04000133 RID: 307
		internal List<NameValueHeaderValue> parameters;

		// Token: 0x04000134 RID: 308
		internal string media_type;
	}
}
