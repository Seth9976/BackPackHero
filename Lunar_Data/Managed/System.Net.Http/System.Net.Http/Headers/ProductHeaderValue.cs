using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a product token value in a User-Agent header.</summary>
	// Token: 0x0200005F RID: 95
	public class ProductHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name.</param>
		// Token: 0x0600034E RID: 846 RVA: 0x0000BA19 File Offset: 0x00009C19
		public ProductHeaderValue(string name)
		{
			Parser.Token.Check(name);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name value.</param>
		/// <param name="version">The product version value.</param>
		// Token: 0x0600034F RID: 847 RVA: 0x0000BA2E File Offset: 0x00009C2E
		public ProductHeaderValue(string name, string version)
			: this(name)
		{
			if (!string.IsNullOrEmpty(version))
			{
				Parser.Token.Check(version);
			}
			this.Version = version;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00002300 File Offset: 0x00000500
		internal ProductHeaderValue()
		{
		}

		/// <summary>Gets the name of the product token.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The name of the product token.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000BA4C File Offset: 0x00009C4C
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000BA54 File Offset: 0x00009C54
		public string Name { get; internal set; }

		/// <summary>Gets the version of the product token.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The version of the product token. </returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BA5D File Offset: 0x00009C5D
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000BA65 File Offset: 0x00009C65
		public string Version { get; internal set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000355 RID: 853 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000356 RID: 854 RVA: 0x0000BA70 File Offset: 0x00009C70
		public override bool Equals(object obj)
		{
			ProductHeaderValue productHeaderValue = obj as ProductHeaderValue;
			return productHeaderValue != null && string.Equals(productHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(productHeaderValue.Version, this.Version, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000357 RID: 855 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public override int GetHashCode()
		{
			int num = this.Name.ToLowerInvariant().GetHashCode();
			if (this.Version != null)
			{
				num ^= this.Version.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ProductHeaderValue" />.An <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents product header value information.</param>
		// Token: 0x06000358 RID: 856 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public static ProductHeaderValue Parse(string input)
		{
			ProductHeaderValue productHeaderValue;
			if (ProductHeaderValue.TryParse(input, out productHeaderValue))
			{
				return productHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> version of the string.</param>
		// Token: 0x06000359 RID: 857 RVA: 0x0000BB10 File Offset: 0x00009D10
		public static bool TryParse(string input, out ProductHeaderValue parsedValue)
		{
			Token token;
			if (ProductHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BB3C File Offset: 0x00009D3C
		internal static bool TryParse(string input, int minimalCount, out List<ProductHeaderValue> result)
		{
			return CollectionParser.TryParse<ProductHeaderValue>(input, minimalCount, new ElementTryParser<ProductHeaderValue>(ProductHeaderValue.TryParseElement), out result);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BB54 File Offset: 0x00009D54
		private static bool TryParseElement(Lexer lexer, out ProductHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new ProductHeaderValue();
			parsedValue.Name = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSlash)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token)
				{
					return false;
				}
				parsedValue.Version = lexer.GetStringValue(t);
				t = lexer.Scan(false);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600035C RID: 860 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		public override string ToString()
		{
			if (this.Version != null)
			{
				return this.Name + "/" + this.Version;
			}
			return this.Name;
		}
	}
}
