using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a Range header value.</summary>
	// Token: 0x02000064 RID: 100
	public class RangeHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> class.</summary>
		// Token: 0x0600037F RID: 895 RVA: 0x0000C1B5 File Offset: 0x0000A3B5
		public RangeHeaderValue()
		{
			this.unit = "bytes";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> class with a byte range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		// Token: 0x06000380 RID: 896 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public RangeHeaderValue(long? from, long? to)
			: this()
		{
			this.Ranges.Add(new RangeItemHeaderValue(from, to));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		private RangeHeaderValue(RangeHeaderValue source)
			: this()
		{
			if (source.ranges != null)
			{
				foreach (RangeItemHeaderValue rangeItemHeaderValue in source.ranges)
				{
					this.Ranges.Add(rangeItemHeaderValue);
				}
			}
		}

		/// <summary>Gets the ranges specified from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.The ranges from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C24C File Offset: 0x0000A44C
		public ICollection<RangeItemHeaderValue> Ranges
		{
			get
			{
				List<RangeItemHeaderValue> list;
				if ((list = this.ranges) == null)
				{
					list = (this.ranges = new List<RangeItemHeaderValue>());
				}
				return list;
			}
		}

		/// <summary>Gets the unit from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The unit from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C271 File Offset: 0x0000A471
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000C279 File Offset: 0x0000A479
		public string Unit
		{
			get
			{
				return this.unit;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Unit");
				}
				Parser.Token.Check(value);
				this.unit = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000385 RID: 901 RVA: 0x0000C296 File Offset: 0x0000A496
		object ICloneable.Clone()
		{
			return new RangeHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000386 RID: 902 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public override bool Equals(object obj)
		{
			RangeHeaderValue rangeHeaderValue = obj as RangeHeaderValue;
			return rangeHeaderValue != null && string.Equals(rangeHeaderValue.Unit, this.Unit, StringComparison.OrdinalIgnoreCase) && rangeHeaderValue.ranges.SequenceEqual(this.ranges);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000387 RID: 903 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public override int GetHashCode()
		{
			return this.Unit.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<RangeItemHeaderValue>(this.ranges);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.RangeHeaderValue" />.An <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents range header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid range header value information.</exception>
		// Token: 0x06000388 RID: 904 RVA: 0x0000C300 File Offset: 0x0000A500
		public static RangeHeaderValue Parse(string input)
		{
			RangeHeaderValue rangeHeaderValue;
			if (RangeHeaderValue.TryParse(input, out rangeHeaderValue))
			{
				return rangeHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">he string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> version of the string.</param>
		// Token: 0x06000389 RID: 905 RVA: 0x0000C320 File Offset: 0x0000A520
		public static bool TryParse(string input, out RangeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return false;
			}
			RangeHeaderValue rangeHeaderValue = new RangeHeaderValue();
			rangeHeaderValue.unit = lexer.GetStringValue(token);
			token = lexer.Scan(false);
			if (token != Token.Type.SeparatorEqual)
			{
				return false;
			}
			for (;;)
			{
				long? num = null;
				long? num2 = null;
				bool flag = false;
				token = lexer.Scan(true);
				Token.Type kind = token.Kind;
				if (kind != Token.Type.Token)
				{
					if (kind != Token.Type.SeparatorDash)
					{
						return false;
					}
					token = lexer.Scan(false);
					long num3;
					if (!lexer.TryGetNumericValue(token, out num3))
					{
						break;
					}
					num2 = new long?(num3);
				}
				else
				{
					string stringValue = lexer.GetStringValue(token);
					string[] array = stringValue.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
					long num3;
					if (!Parser.Long.TryParse(array[0], out num3))
					{
						return false;
					}
					int num4 = array.Length;
					if (num4 != 1)
					{
						if (num4 != 2)
						{
							return false;
						}
						num = new long?(num3);
						if (!Parser.Long.TryParse(array[1], out num3))
						{
							return false;
						}
						num2 = new long?(num3);
						long? num5 = num2;
						long? num6 = num;
						if ((num5.GetValueOrDefault() < num6.GetValueOrDefault()) & ((num5 != null) & (num6 != null)))
						{
							return false;
						}
					}
					else
					{
						token = lexer.Scan(true);
						num = new long?(num3);
						Token.Type kind2 = token.Kind;
						if (kind2 != Token.Type.End)
						{
							if (kind2 != Token.Type.SeparatorDash)
							{
								if (kind2 != Token.Type.SeparatorComma)
								{
									return false;
								}
								flag = true;
							}
							else
							{
								token = lexer.Scan(false);
								if (token != Token.Type.Token)
								{
									flag = true;
								}
								else
								{
									if (!lexer.TryGetNumericValue(token, out num3))
									{
										return false;
									}
									num2 = new long?(num3);
									long? num6 = num2;
									long? num5 = num;
									if ((num6.GetValueOrDefault() < num5.GetValueOrDefault()) & ((num6 != null) & (num5 != null)))
									{
										return false;
									}
								}
							}
						}
						else
						{
							if (stringValue.Length > 0 && stringValue[stringValue.Length - 1] != '-')
							{
								return false;
							}
							flag = true;
						}
					}
				}
				rangeHeaderValue.Ranges.Add(new RangeItemHeaderValue(num, num2));
				if (!flag)
				{
					token = lexer.Scan(false);
				}
				if (token != Token.Type.SeparatorComma)
				{
					goto Block_20;
				}
			}
			return false;
			Block_20:
			if (token != Token.Type.End)
			{
				return false;
			}
			parsedValue = rangeHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600038A RID: 906 RVA: 0x0000C548 File Offset: 0x0000A748
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.unit);
			stringBuilder.Append("=");
			for (int i = 0; i < this.Ranges.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(this.ranges[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000146 RID: 326
		private List<RangeItemHeaderValue> ranges;

		// Token: 0x04000147 RID: 327
		private string unit;
	}
}
