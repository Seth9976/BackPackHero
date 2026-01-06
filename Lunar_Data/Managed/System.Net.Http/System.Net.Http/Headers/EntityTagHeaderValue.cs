using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an entity-tag header value.</summary>
	// Token: 0x0200003A RID: 58
	public class EntityTagHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		// Token: 0x06000205 RID: 517 RVA: 0x0000880E File Offset: 0x00006A0E
		public EntityTagHeaderValue(string tag)
		{
			Parser.Token.CheckQuotedString(tag);
			this.Tag = tag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an  <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		/// <param name="isWeak">A value that indicates if this entity-tag header is a weak validator. If the entity-tag header is weak validator, then <paramref name="isWeak" /> should be set to true. If the entity-tag header is a strong validator, then <paramref name="isWeak" /> should be set to false.</param>
		// Token: 0x06000206 RID: 518 RVA: 0x00008823 File Offset: 0x00006A23
		public EntityTagHeaderValue(string tag, bool isWeak)
			: this(tag)
		{
			this.IsWeak = isWeak;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00002300 File Offset: 0x00000500
		internal EntityTagHeaderValue()
		{
		}

		/// <summary>Gets the entity-tag header value.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008833 File Offset: 0x00006A33
		public static EntityTagHeaderValue Any
		{
			get
			{
				return EntityTagHeaderValue.any;
			}
		}

		/// <summary>Gets whether the entity-tag is prefaced by a weakness indicator.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the entity-tag is prefaced by a weakness indicator; otherwise, false.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000883A File Offset: 0x00006A3A
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00008842 File Offset: 0x00006A42
		public bool IsWeak { get; internal set; }

		/// <summary>Gets the opaque quoted string. </summary>
		/// <returns>Returns <see cref="T:System.String" />.An opaque quoted string.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000884B File Offset: 0x00006A4B
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00008853 File Offset: 0x00006A53
		public string Tag { get; internal set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x0600020D RID: 525 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x0600020E RID: 526 RVA: 0x0000885C File Offset: 0x00006A5C
		public override bool Equals(object obj)
		{
			EntityTagHeaderValue entityTagHeaderValue = obj as EntityTagHeaderValue;
			return entityTagHeaderValue != null && entityTagHeaderValue.Tag == this.Tag && string.Equals(entityTagHeaderValue.Tag, this.Tag, StringComparison.Ordinal);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x0600020F RID: 527 RVA: 0x0000889C File Offset: 0x00006A9C
		public override int GetHashCode()
		{
			return this.IsWeak.GetHashCode() ^ this.Tag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents entity tag header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid entity tag header value information.</exception>
		// Token: 0x06000210 RID: 528 RVA: 0x000088C4 File Offset: 0x00006AC4
		public static EntityTagHeaderValue Parse(string input)
		{
			EntityTagHeaderValue entityTagHeaderValue;
			if (EntityTagHeaderValue.TryParse(input, out entityTagHeaderValue))
			{
				return entityTagHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> version of the string.</param>
		// Token: 0x06000211 RID: 529 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
		{
			Token token;
			if (EntityTagHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008910 File Offset: 0x00006B10
		private static bool TryParseElement(Lexer lexer, out EntityTagHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			bool flag = false;
			if (t == Token.Type.Token)
			{
				string stringValue = lexer.GetStringValue(t);
				if (stringValue == "*")
				{
					parsedValue = EntityTagHeaderValue.any;
					t = lexer.Scan(false);
					return true;
				}
				if (stringValue != "W" || lexer.PeekChar() != 47)
				{
					return false;
				}
				flag = true;
				lexer.EatChar();
				t = lexer.Scan(false);
			}
			if (t != Token.Type.QuotedString)
			{
				return false;
			}
			parsedValue = new EntityTagHeaderValue();
			parsedValue.Tag = lexer.GetStringValue(t);
			parsedValue.IsWeak = flag;
			t = lexer.Scan(false);
			return true;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000089DB File Offset: 0x00006BDB
		internal static bool TryParse(string input, int minimalCount, out List<EntityTagHeaderValue> result)
		{
			return CollectionParser.TryParse<EntityTagHeaderValue>(input, minimalCount, new ElementTryParser<EntityTagHeaderValue>(EntityTagHeaderValue.TryParseElement), out result);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x06000214 RID: 532 RVA: 0x000089F1 File Offset: 0x00006BF1
		public override string ToString()
		{
			if (!this.IsWeak)
			{
				return this.Tag;
			}
			return "W/" + this.Tag;
		}

		// Token: 0x040000F1 RID: 241
		private static readonly EntityTagHeaderValue any = new EntityTagHeaderValue
		{
			Tag = "*"
		};
	}
}
