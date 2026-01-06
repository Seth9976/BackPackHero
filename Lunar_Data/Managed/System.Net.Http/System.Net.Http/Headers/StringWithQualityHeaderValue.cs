using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a string header value with an optional quality.</summary>
	// Token: 0x02000067 RID: 103
	public class StringWithQualityHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">The string used to initialize the new instance.</param>
		// Token: 0x060003A0 RID: 928 RVA: 0x0000CA22 File Offset: 0x0000AC22
		public StringWithQualityHeaderValue(string value)
		{
			Parser.Token.Check(value);
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		/// <param name="quality">A quality factor used to initialize the new instance.</param>
		// Token: 0x060003A1 RID: 929 RVA: 0x0000CA37 File Offset: 0x0000AC37
		public StringWithQualityHeaderValue(string value, double quality)
			: this(value)
		{
			if (quality < 0.0 || quality > 1.0)
			{
				throw new ArgumentOutOfRangeException("quality");
			}
			this.Quality = new double?(quality);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00002300 File Offset: 0x00000500
		private StringWithQualityHeaderValue()
		{
		}

		/// <summary>Gets the quality factor from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Double" />.The quality factor from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000CA6F File Offset: 0x0000AC6F
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000CA77 File Offset: 0x0000AC77
		public double? Quality { get; private set; }

		/// <summary>Gets the string value from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The string value from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000CA80 File Offset: 0x0000AC80
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public string Value { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x060003A7 RID: 935 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060003A8 RID: 936 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public override bool Equals(object obj)
		{
			StringWithQualityHeaderValue stringWithQualityHeaderValue = obj as StringWithQualityHeaderValue;
			if (stringWithQualityHeaderValue != null && string.Equals(stringWithQualityHeaderValue.Value, this.Value, StringComparison.OrdinalIgnoreCase))
			{
				double? quality = stringWithQualityHeaderValue.Quality;
				double? quality2 = this.Quality;
				return (quality.GetValueOrDefault() == quality2.GetValueOrDefault()) & (quality != null == (quality2 != null));
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public override int GetHashCode()
		{
			return this.Value.ToLowerInvariant().GetHashCode() ^ this.Quality.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" />.An <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents quality header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid string with quality header value information.</exception>
		// Token: 0x060003AA RID: 938 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public static StringWithQualityHeaderValue Parse(string input)
		{
			StringWithQualityHeaderValue stringWithQualityHeaderValue;
			if (StringWithQualityHeaderValue.TryParse(input, out stringWithQualityHeaderValue))
			{
				return stringWithQualityHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> version of the string.</param>
		// Token: 0x060003AB RID: 939 RVA: 0x0000CB44 File Offset: 0x0000AD44
		public static bool TryParse(string input, out StringWithQualityHeaderValue parsedValue)
		{
			Token token;
			if (StringWithQualityHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000CB70 File Offset: 0x0000AD70
		internal static bool TryParse(string input, int minimalCount, out List<StringWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse<StringWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<StringWithQualityHeaderValue>(StringWithQualityHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000CB88 File Offset: 0x0000AD88
		private static bool TryParseElement(Lexer lexer, out StringWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			StringWithQualityHeaderValue stringWithQualityHeaderValue = new StringWithQualityHeaderValue();
			stringWithQualityHeaderValue.Value = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSemicolon)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token)
				{
					return false;
				}
				string stringValue = lexer.GetStringValue(t);
				if (stringValue != "q" && stringValue != "Q")
				{
					return false;
				}
				t = lexer.Scan(false);
				if (t != Token.Type.SeparatorEqual)
				{
					return false;
				}
				t = lexer.Scan(false);
				double num;
				if (!lexer.TryGetDoubleValue(t, out num))
				{
					return false;
				}
				if (num > 1.0)
				{
					return false;
				}
				stringWithQualityHeaderValue.Quality = new double?(num);
				t = lexer.Scan(false);
			}
			parsedValue = stringWithQualityHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060003AE RID: 942 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public override string ToString()
		{
			if (this.Quality != null)
			{
				return this.Value + "; q=" + this.Quality.Value.ToString("0.0##", CultureInfo.InvariantCulture);
			}
			return this.Value;
		}
	}
}
