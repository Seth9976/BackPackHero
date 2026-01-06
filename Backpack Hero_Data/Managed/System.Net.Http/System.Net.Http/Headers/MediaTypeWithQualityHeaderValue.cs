using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a media type with an additional quality factor used in a Content-Type header.</summary>
	// Token: 0x02000050 RID: 80
	public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance. </param>
		// Token: 0x06000310 RID: 784 RVA: 0x0000B09C File Offset: 0x0000929C
		public MediaTypeWithQualityHeaderValue(string mediaType)
			: base(mediaType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance.</param>
		/// <param name="quality">The quality associated with this header value.</param>
		// Token: 0x06000311 RID: 785 RVA: 0x0000B0A5 File Offset: 0x000092A5
		public MediaTypeWithQualityHeaderValue(string mediaType, double quality)
			: this(mediaType)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B0BA File Offset: 0x000092BA
		private MediaTypeWithQualityHeaderValue()
		{
		}

		/// <summary>Get or set the quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" />.</summary>
		/// <returns>Returns <see cref="T:System.Double" />.The quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> object.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B0C2 File Offset: 0x000092C2
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000B0CF File Offset: 0x000092CF
		public double? Quality
		{
			get
			{
				return QualityValue.GetValue(this.parameters);
			}
			set
			{
				QualityValue.SetValue(ref this.parameters, value);
			}
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" />.An <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents media type with quality header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type with quality header value information.</exception>
		// Token: 0x06000315 RID: 789 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public new static MediaTypeWithQualityHeaderValue Parse(string input)
		{
			MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue;
			if (MediaTypeWithQualityHeaderValue.TryParse(input, out mediaTypeWithQualityHeaderValue))
			{
				return mediaTypeWithQualityHeaderValue;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> version of the string.</param>
		// Token: 0x06000316 RID: 790 RVA: 0x0000B100 File Offset: 0x00009300
		public static bool TryParse(string input, out MediaTypeWithQualityHeaderValue parsedValue)
		{
			Token token;
			if (MediaTypeWithQualityHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B12C File Offset: 0x0000932C
		private static bool TryParseElement(Lexer lexer, out MediaTypeWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			List<NameValueHeaderValue> list = null;
			string text;
			Token? token = MediaTypeHeaderValue.TryParseMediaType(lexer, out text);
			if (token == null)
			{
				t = Token.Empty;
				return false;
			}
			t = token.Value;
			if (t == Token.Type.SeparatorSemicolon && !NameValueHeaderValue.TryParseParameters(lexer, out list, out t))
			{
				return false;
			}
			parsedValue = new MediaTypeWithQualityHeaderValue();
			parsedValue.media_type = text;
			parsedValue.parameters = list;
			return true;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B19D File Offset: 0x0000939D
		internal static bool TryParse(string input, int minimalCount, out List<MediaTypeWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse<MediaTypeWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<MediaTypeWithQualityHeaderValue>(MediaTypeWithQualityHeaderValue.TryParseElement), out result);
		}
	}
}
