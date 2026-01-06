using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an Accept-Encoding header value.with optional quality factor.</summary>
	// Token: 0x02000069 RID: 105
	public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		// Token: 0x060003BC RID: 956 RVA: 0x0000CF2B File Offset: 0x0000B12B
		public TransferCodingWithQualityHeaderValue(string value)
			: base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		/// <param name="quality">A value for the quality factor.</param>
		// Token: 0x060003BD RID: 957 RVA: 0x0000CF34 File Offset: 0x0000B134
		public TransferCodingWithQualityHeaderValue(string value, double quality)
			: this(value)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000CF49 File Offset: 0x0000B149
		private TransferCodingWithQualityHeaderValue()
		{
		}

		/// <summary>Gets the quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</summary>
		/// <returns>Returns <see cref="T:System.Double" />.The quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000CF51 File Offset: 0x0000B151
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000CF5E File Offset: 0x0000B15E
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

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.An <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents transfer-coding value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid transfer-coding with quality header value information.</exception>
		// Token: 0x060003C1 RID: 961 RVA: 0x0000CF6C File Offset: 0x0000B16C
		public new static TransferCodingWithQualityHeaderValue Parse(string input)
		{
			TransferCodingWithQualityHeaderValue transferCodingWithQualityHeaderValue;
			if (TransferCodingWithQualityHeaderValue.TryParse(input, out transferCodingWithQualityHeaderValue))
			{
				return transferCodingWithQualityHeaderValue;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> version of the string.</param>
		// Token: 0x060003C2 RID: 962 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public static bool TryParse(string input, out TransferCodingWithQualityHeaderValue parsedValue)
		{
			Token token;
			if (TransferCodingWithQualityHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		internal static bool TryParse(string input, int minimalCount, out List<TransferCodingWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse<TransferCodingWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<TransferCodingWithQualityHeaderValue>(TransferCodingWithQualityHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		private static bool TryParseElement(Lexer lexer, out TransferCodingWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			TransferCodingWithQualityHeaderValue transferCodingWithQualityHeaderValue = new TransferCodingWithQualityHeaderValue();
			transferCodingWithQualityHeaderValue.value = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out transferCodingWithQualityHeaderValue.parameters, out t) || t != Token.Type.End))
			{
				return false;
			}
			parsedValue = transferCodingWithQualityHeaderValue;
			return true;
		}
	}
}
