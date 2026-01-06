using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a Retry-After header value which can either be a date/time or a timespan value.</summary>
	// Token: 0x02000066 RID: 102
	public class RetryConditionHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="date">The date and time offset used to initialize the new instance.</param>
		// Token: 0x06000394 RID: 916 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		public RetryConditionHeaderValue(DateTimeOffset date)
		{
			this.Date = new DateTimeOffset?(date);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="delta">The delta, in seconds, used to initialize the new instance.</param>
		// Token: 0x06000395 RID: 917 RVA: 0x0000C801 File Offset: 0x0000AA01
		public RetryConditionHeaderValue(TimeSpan delta)
		{
			if (delta.TotalSeconds > 4294967295.0)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			this.Delta = new TimeSpan?(delta);
		}

		/// <summary>Gets the date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000C832 File Offset: 0x0000AA32
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000C83A File Offset: 0x0000AA3A
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets the delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C843 File Offset: 0x0000AA43
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000C84B File Offset: 0x0000AA4B
		public TimeSpan? Delta { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x0600039A RID: 922 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x0000C854 File Offset: 0x0000AA54
		public override bool Equals(object obj)
		{
			RetryConditionHeaderValue retryConditionHeaderValue = obj as RetryConditionHeaderValue;
			return retryConditionHeaderValue != null && retryConditionHeaderValue.Date == this.Date && retryConditionHeaderValue.Delta == this.Delta;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x0600039C RID: 924 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public override int GetHashCode()
		{
			return this.Date.GetHashCode() ^ this.Delta.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" />.An <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents retry condition header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid retry condition header value information.</exception>
		// Token: 0x0600039D RID: 925 RVA: 0x0000C92C File Offset: 0x0000AB2C
		public static RetryConditionHeaderValue Parse(string input)
		{
			RetryConditionHeaderValue retryConditionHeaderValue;
			if (RetryConditionHeaderValue.TryParse(input, out retryConditionHeaderValue))
			{
				return retryConditionHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> version of the string.</param>
		// Token: 0x0600039E RID: 926 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return false;
			}
			TimeSpan? timeSpan = lexer.TryGetTimeSpanValue(token);
			if (timeSpan != null)
			{
				if (lexer.Scan(false) != Token.Type.End)
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(timeSpan.Value);
			}
			else
			{
				DateTimeOffset dateTimeOffset;
				if (!Lexer.TryGetDateValue(input, out dateTimeOffset))
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(dateTimeOffset);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600039F RID: 927 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public override string ToString()
		{
			if (this.Delta == null)
			{
				return this.Date.Value.ToString("r", CultureInfo.InvariantCulture);
			}
			return this.Delta.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
		}
	}
}
