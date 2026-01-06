using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a name/value pair with parameters used in various headers as defined in RFC 2616.</summary>
	// Token: 0x02000052 RID: 82
	public class NameValueWithParametersHeaderValue : NameValueHeaderValue, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		// Token: 0x0600032B RID: 811 RVA: 0x0000B52F File Offset: 0x0000972F
		public NameValueWithParametersHeaderValue(string name)
			: base(name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		// Token: 0x0600032C RID: 812 RVA: 0x0000B538 File Offset: 0x00009738
		public NameValueWithParametersHeaderValue(string name, string value)
			: base(name, value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x0600032D RID: 813 RVA: 0x0000B544 File Offset: 0x00009744
		protected NameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue source)
			: base(source)
		{
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add(nameValueHeaderValue);
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B5AC File Offset: 0x000097AC
		private NameValueWithParametersHeaderValue()
		{
		}

		/// <summary>Gets the parameters from the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.A collection containing the parameters.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B5B4 File Offset: 0x000097B4
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

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000330 RID: 816 RVA: 0x0000B5D9 File Offset: 0x000097D9
		object ICloneable.Clone()
		{
			return new NameValueWithParametersHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000331 RID: 817 RVA: 0x0000B5E4 File Offset: 0x000097E4
		public override bool Equals(object obj)
		{
			NameValueWithParametersHeaderValue nameValueWithParametersHeaderValue = obj as NameValueWithParametersHeaderValue;
			return nameValueWithParametersHeaderValue != null && base.Equals(obj) && nameValueWithParametersHeaderValue.parameters.SequenceEqual(this.parameters);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000332 RID: 818 RVA: 0x0000B619 File Offset: 0x00009819
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" />.An <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents name value with parameter header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid name value with parameter header value information.</exception>
		// Token: 0x06000333 RID: 819 RVA: 0x0000B630 File Offset: 0x00009830
		public new static NameValueWithParametersHeaderValue Parse(string input)
		{
			NameValueWithParametersHeaderValue nameValueWithParametersHeaderValue;
			if (NameValueWithParametersHeaderValue.TryParse(input, out nameValueWithParametersHeaderValue))
			{
				return nameValueWithParametersHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x06000334 RID: 820 RVA: 0x0000B64F File Offset: 0x0000984F
		public override string ToString()
		{
			if (this.parameters == null || this.parameters.Count == 0)
			{
				return base.ToString();
			}
			return base.ToString() + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> version of the string.</param>
		// Token: 0x06000335 RID: 821 RVA: 0x0000B684 File Offset: 0x00009884
		public static bool TryParse(string input, out NameValueWithParametersHeaderValue parsedValue)
		{
			Token token;
			if (NameValueWithParametersHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B6B0 File Offset: 0x000098B0
		internal static bool TryParse(string input, int minimalCount, out List<NameValueWithParametersHeaderValue> result)
		{
			return CollectionParser.TryParse<NameValueWithParametersHeaderValue>(input, minimalCount, new ElementTryParser<NameValueWithParametersHeaderValue>(NameValueWithParametersHeaderValue.TryParseElement), out result);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B6C8 File Offset: 0x000098C8
		private static bool TryParseElement(Lexer lexer, out NameValueWithParametersHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new NameValueWithParametersHeaderValue
			{
				Name = lexer.GetStringValue(t)
			};
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorEqual)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token && t != Token.Type.QuotedString)
				{
					return false;
				}
				parsedValue.value = lexer.GetStringValue(t);
				t = lexer.Scan(false);
			}
			if (t == Token.Type.SeparatorSemicolon)
			{
				List<NameValueHeaderValue> list;
				if (!NameValueHeaderValue.TryParseParameters(lexer, out list, out t))
				{
					return false;
				}
				parsedValue.parameters = list;
			}
			return true;
		}

		// Token: 0x04000139 RID: 313
		private List<NameValueHeaderValue> parameters;
	}
}
