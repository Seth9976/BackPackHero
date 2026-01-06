using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an If-Range header value which can either be a date/time or an entity-tag value.</summary>
	// Token: 0x02000063 RID: 99
	public class RangeConditionHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="date">A date value used to initialize the new instance.</param>
		// Token: 0x06000372 RID: 882 RVA: 0x0000BF97 File Offset: 0x0000A197
		public RangeConditionHeaderValue(DateTimeOffset date)
		{
			this.Date = new DateTimeOffset?(date);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x06000373 RID: 883 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		public RangeConditionHeaderValue(EntityTagHeaderValue entityTag)
		{
			if (entityTag == null)
			{
				throw new ArgumentNullException("entityTag");
			}
			this.EntityTag = entityTag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An entity tag represented as a string used to initialize the new instance.</param>
		// Token: 0x06000374 RID: 884 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public RangeConditionHeaderValue(string entityTag)
			: this(new EntityTagHeaderValue(entityTag))
		{
		}

		/// <summary>Gets the date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BFD6 File Offset: 0x0000A1D6
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000BFDE File Offset: 0x0000A1DE
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets the entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.The entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000BFE7 File Offset: 0x0000A1E7
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000BFEF File Offset: 0x0000A1EF
		public EntityTagHeaderValue EntityTag { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000379 RID: 889 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x0600037A RID: 890 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		public override bool Equals(object obj)
		{
			RangeConditionHeaderValue rangeConditionHeaderValue = obj as RangeConditionHeaderValue;
			if (rangeConditionHeaderValue == null)
			{
				return false;
			}
			if (this.EntityTag == null)
			{
				return this.Date == rangeConditionHeaderValue.Date;
			}
			return this.EntityTag.Equals(rangeConditionHeaderValue.EntityTag);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x0600037B RID: 891 RVA: 0x0000C06C File Offset: 0x0000A26C
		public override int GetHashCode()
		{
			if (this.EntityTag == null)
			{
				return this.Date.GetHashCode();
			}
			return this.EntityTag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" />.An <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents range condition header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid range Condition header value information.</exception>
		// Token: 0x0600037C RID: 892 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public static RangeConditionHeaderValue Parse(string input)
		{
			RangeConditionHeaderValue rangeConditionHeaderValue;
			if (RangeConditionHeaderValue.TryParse(input, out rangeConditionHeaderValue))
			{
				return rangeConditionHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> version of the string.</param>
		// Token: 0x0600037D RID: 893 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			bool flag;
			if (token == Token.Type.Token)
			{
				if (lexer.GetStringValue(token) != "W")
				{
					DateTimeOffset dateTimeOffset;
					if (!Lexer.TryGetDateValue(input, out dateTimeOffset))
					{
						return false;
					}
					parsedValue = new RangeConditionHeaderValue(dateTimeOffset);
					return true;
				}
				else
				{
					if (lexer.PeekChar() != 47)
					{
						return false;
					}
					flag = true;
					lexer.EatChar();
					token = lexer.Scan(false);
				}
			}
			else
			{
				flag = false;
			}
			if (token != Token.Type.QuotedString)
			{
				return false;
			}
			if (lexer.Scan(false) != Token.Type.End)
			{
				return false;
			}
			parsedValue = new RangeConditionHeaderValue(new EntityTagHeaderValue
			{
				Tag = lexer.GetStringValue(token),
				IsWeak = flag
			});
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600037E RID: 894 RVA: 0x0000C174 File Offset: 0x0000A374
		public override string ToString()
		{
			if (this.EntityTag != null)
			{
				return this.EntityTag.ToString();
			}
			return this.Date.Value.ToString("r", CultureInfo.InvariantCulture);
		}
	}
}
