using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an accept-encoding header value.</summary>
	// Token: 0x02000068 RID: 104
	public class TransferCodingHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		// Token: 0x060003AF RID: 943 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public TransferCodingHeaderValue(string value)
		{
			Parser.Token.Check(value);
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object used to initialize the new instance. </param>
		// Token: 0x060003B0 RID: 944 RVA: 0x0000CD04 File Offset: 0x0000AF04
		protected TransferCodingHeaderValue(TransferCodingHeaderValue source)
		{
			this.value = source.value;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add(new NameValueHeaderValue(nameValueHeaderValue));
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00002300 File Offset: 0x00000500
		internal TransferCodingHeaderValue()
		{
		}

		/// <summary>Gets the transfer-coding parameters.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The transfer-coding parameters.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000CD7C File Offset: 0x0000AF7C
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

		/// <summary>Gets the transfer-coding value.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The transfer-coding value.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x060003B4 RID: 948 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		object ICloneable.Clone()
		{
			return new TransferCodingHeaderValue(this);
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060003B5 RID: 949 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		public override bool Equals(object obj)
		{
			TransferCodingHeaderValue transferCodingHeaderValue = obj as TransferCodingHeaderValue;
			return transferCodingHeaderValue != null && string.Equals(this.value, transferCodingHeaderValue.value, StringComparison.OrdinalIgnoreCase) && this.parameters.SequenceEqual(transferCodingHeaderValue.parameters);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x060003B6 RID: 950 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public override int GetHashCode()
		{
			int num = this.value.ToLowerInvariant().GetHashCode();
			if (this.parameters != null)
			{
				num ^= HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" />.An <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents transfer-coding header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid transfer-coding header value information.</exception>
		// Token: 0x060003B7 RID: 951 RVA: 0x0000CE2C File Offset: 0x0000B02C
		public static TransferCodingHeaderValue Parse(string input)
		{
			TransferCodingHeaderValue transferCodingHeaderValue;
			if (TransferCodingHeaderValue.TryParse(input, out transferCodingHeaderValue))
			{
				return transferCodingHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000CE4B File Offset: 0x0000B04B
		public override string ToString()
		{
			return this.value + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> version of the string.</param>
		// Token: 0x060003B9 RID: 953 RVA: 0x0000CE64 File Offset: 0x0000B064
		public static bool TryParse(string input, out TransferCodingHeaderValue parsedValue)
		{
			Token token;
			if (TransferCodingHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000CE90 File Offset: 0x0000B090
		internal static bool TryParse(string input, int minimalCount, out List<TransferCodingHeaderValue> result)
		{
			return CollectionParser.TryParse<TransferCodingHeaderValue>(input, minimalCount, new ElementTryParser<TransferCodingHeaderValue>(TransferCodingHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		private static bool TryParseElement(Lexer lexer, out TransferCodingHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			TransferCodingHeaderValue transferCodingHeaderValue = new TransferCodingHeaderValue();
			transferCodingHeaderValue.value = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out transferCodingHeaderValue.parameters, out t) || t != Token.Type.End))
			{
				return false;
			}
			parsedValue = transferCodingHeaderValue;
			return true;
		}

		// Token: 0x0400014E RID: 334
		internal string value;

		// Token: 0x0400014F RID: 335
		internal List<NameValueHeaderValue> parameters;
	}
}
