using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a value which can either be a product or a comment in a User-Agent header.</summary>
	// Token: 0x02000060 RID: 96
	public class ProductInfoHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="product">A <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x0600035D RID: 861 RVA: 0x0000BC1B File Offset: 0x00009E1B
		public ProductInfoHeaderValue(ProductHeaderValue product)
		{
			if (product == null)
			{
				throw new ArgumentNullException();
			}
			this.Product = product;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="comment">A comment value.</param>
		// Token: 0x0600035E RID: 862 RVA: 0x0000BC33 File Offset: 0x00009E33
		public ProductInfoHeaderValue(string comment)
		{
			Parser.Token.CheckComment(comment);
			this.Comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="productName">The product name value.</param>
		/// <param name="productVersion">The product version value.</param>
		// Token: 0x0600035F RID: 863 RVA: 0x0000BC48 File Offset: 0x00009E48
		public ProductInfoHeaderValue(string productName, string productVersion)
		{
			this.Product = new ProductHeaderValue(productName, productVersion);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00002300 File Offset: 0x00000500
		private ProductInfoHeaderValue()
		{
		}

		/// <summary>Gets the comment from the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The comment value this <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" />.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000BC5D File Offset: 0x00009E5D
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000BC65 File Offset: 0x00009E65
		public string Comment { get; private set; }

		/// <summary>Gets the product from the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ProductHeaderValue" />.The product value from this <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" />.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BC6E File Offset: 0x00009E6E
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000BC76 File Offset: 0x00009E76
		public ProductHeaderValue Product { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000365 RID: 869 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000366 RID: 870 RVA: 0x0000BC80 File Offset: 0x00009E80
		public override bool Equals(object obj)
		{
			ProductInfoHeaderValue productInfoHeaderValue = obj as ProductInfoHeaderValue;
			if (productInfoHeaderValue == null)
			{
				return false;
			}
			if (this.Product == null)
			{
				return productInfoHeaderValue.Comment == this.Comment;
			}
			return this.Product.Equals(productInfoHeaderValue.Product);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000367 RID: 871 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		public override int GetHashCode()
		{
			if (this.Product == null)
			{
				return this.Comment.GetHashCode();
			}
			return this.Product.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" />.An <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents product info header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid product info header value information.</exception>
		// Token: 0x06000368 RID: 872 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public static ProductInfoHeaderValue Parse(string input)
		{
			ProductInfoHeaderValue productInfoHeaderValue;
			if (ProductInfoHeaderValue.TryParse(input, out productInfoHeaderValue))
			{
				return productInfoHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> version of the string.</param>
		// Token: 0x06000369 RID: 873 RVA: 0x0000BD08 File Offset: 0x00009F08
		public static bool TryParse(string input, out ProductInfoHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			if (!ProductInfoHeaderValue.TryParseElement(lexer, out parsedValue) || parsedValue == null)
			{
				return false;
			}
			if (lexer.Scan(false) != Token.Type.End)
			{
				parsedValue = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000BD44 File Offset: 0x00009F44
		internal static bool TryParse(string input, int minimalCount, out List<ProductInfoHeaderValue> result)
		{
			List<ProductInfoHeaderValue> list = new List<ProductInfoHeaderValue>();
			Lexer lexer = new Lexer(input);
			result = null;
			ProductInfoHeaderValue productInfoHeaderValue;
			while (ProductInfoHeaderValue.TryParseElement(lexer, out productInfoHeaderValue))
			{
				if (productInfoHeaderValue != null)
				{
					list.Add(productInfoHeaderValue);
					int num = lexer.PeekChar();
					if (num != -1)
					{
						if (num == 9 || num == 32)
						{
							lexer.EatChar();
							continue;
						}
					}
					else if (minimalCount <= list.Count)
					{
						result = list;
						return true;
					}
					return false;
				}
				if (list != null && minimalCount <= list.Count)
				{
					result = list;
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		private static bool TryParseElement(Lexer lexer, out ProductInfoHeaderValue parsedValue)
		{
			parsedValue = null;
			string text;
			Token token;
			if (lexer.ScanCommentOptional(out text, out token))
			{
				if (text == null)
				{
					return false;
				}
				parsedValue = new ProductInfoHeaderValue();
				parsedValue.Comment = text;
				return true;
			}
			else
			{
				if (token == Token.Type.End)
				{
					return true;
				}
				if (token != Token.Type.Token)
				{
					return false;
				}
				ProductHeaderValue productHeaderValue = new ProductHeaderValue();
				productHeaderValue.Name = lexer.GetStringValue(token);
				int position = lexer.Position;
				token = lexer.Scan(false);
				if (token == Token.Type.SeparatorSlash)
				{
					token = lexer.Scan(false);
					if (token != Token.Type.Token)
					{
						return false;
					}
					productHeaderValue.Version = lexer.GetStringValue(token);
				}
				else
				{
					lexer.Position = position;
				}
				parsedValue = new ProductInfoHeaderValue(productHeaderValue);
				return true;
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600036C RID: 876 RVA: 0x0000BE5D File Offset: 0x0000A05D
		public override string ToString()
		{
			if (this.Product == null)
			{
				return this.Comment;
			}
			return this.Product.ToString();
		}
	}
}
