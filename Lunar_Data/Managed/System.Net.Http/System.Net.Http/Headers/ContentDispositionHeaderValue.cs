using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of the Content-Disposition header.</summary>
	// Token: 0x02000038 RID: 56
	public class ContentDispositionHeaderValue : ICloneable
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00002300 File Offset: 0x00000500
		private ContentDispositionHeaderValue()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="dispositionType">A string that contains a <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.</param>
		// Token: 0x060001D1 RID: 465 RVA: 0x00007B1C File Offset: 0x00005D1C
		public ContentDispositionHeaderValue(string dispositionType)
		{
			this.DispositionType = dispositionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />. </param>
		// Token: 0x060001D2 RID: 466 RVA: 0x00007B2C File Offset: 0x00005D2C
		protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.dispositionType = source.dispositionType;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add(new NameValueHeaderValue(nameValueHeaderValue));
				}
			}
		}

		/// <summary>The date at which   the file was created.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The file creation date.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007BB4 File Offset: 0x00005DB4
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007BC1 File Offset: 0x00005DC1
		public DateTimeOffset? CreationDate
		{
			get
			{
				return this.GetDateValue("creation-date");
			}
			set
			{
				this.SetDateValue("creation-date", value);
			}
		}

		/// <summary>The disposition type for a content body part.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The disposition type. </returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007BCF File Offset: 0x00005DCF
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007BD7 File Offset: 0x00005DD7
		public string DispositionType
		{
			get
			{
				return this.dispositionType;
			}
			set
			{
				Parser.Token.Check(value);
				this.dispositionType = value;
			}
		}

		/// <summary>A suggestion for how to construct a filename for   storing the message payload to be used if the entity is   detached and stored in a separate file.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A suggested filename.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007BE8 File Offset: 0x00005DE8
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00007C0D File Offset: 0x00005E0D
		public string FileName
		{
			get
			{
				string text = this.FindParameter("filename");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, false);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeBase64Value(value);
				}
				this.SetValue("filename", value);
			}
		}

		/// <summary>A suggestion for how to construct filenames for   storing message payloads to be used if the entities are    detached and stored in a separate files.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A suggested filename of the form filename*.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007C28 File Offset: 0x00005E28
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00007C4D File Offset: 0x00005E4D
		public string FileNameStar
		{
			get
			{
				string text = this.FindParameter("filename*");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, true);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeRFC5987(value);
				}
				this.SetValue("filename*", value);
			}
		}

		/// <summary>The date at   which the file was last modified. </summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The file modification date.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007C66 File Offset: 0x00005E66
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007C73 File Offset: 0x00005E73
		public DateTimeOffset? ModificationDate
		{
			get
			{
				return this.GetDateValue("modification-date");
			}
			set
			{
				this.SetDateValue("modification-date", value);
			}
		}

		/// <summary>The name for a content body part.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The name for the content body part.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00007CA9 File Offset: 0x00005EA9
		public string Name
		{
			get
			{
				string text = this.FindParameter("name");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, false);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeBase64Value(value);
				}
				this.SetValue("name", value);
			}
		}

		/// <summary>A set of parameters included the Content-Disposition header.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.ICollection`1" />.A collection of parameters. </returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007CC4 File Offset: 0x00005EC4
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

		/// <summary>The date the file was last read.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The last read date.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007CE9 File Offset: 0x00005EE9
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00007CF6 File Offset: 0x00005EF6
		public DateTimeOffset? ReadDate
		{
			get
			{
				return this.GetDateValue("read-date");
			}
			set
			{
				this.SetDateValue("read-date", value);
			}
		}

		/// <summary>The approximate size, in bytes, of the file. </summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The approximate size, in bytes.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007D04 File Offset: 0x00005F04
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007D38 File Offset: 0x00005F38
		public long? Size
		{
			get
			{
				long num;
				if (Parser.Long.TryParse(this.FindParameter("size"), out num))
				{
					return new long?(num);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.SetValue("size", null);
					return;
				}
				long? num = value;
				long num2 = 0L;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetValue("size", value.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />  instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x060001E4 RID: 484 RVA: 0x00007D9E File Offset: 0x00005F9E
		object ICloneable.Clone()
		{
			return new ContentDispositionHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />  object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public override bool Equals(object obj)
		{
			ContentDispositionHeaderValue contentDispositionHeaderValue = obj as ContentDispositionHeaderValue;
			return contentDispositionHeaderValue != null && string.Equals(contentDispositionHeaderValue.dispositionType, this.dispositionType, StringComparison.OrdinalIgnoreCase) && contentDispositionHeaderValue.parameters.SequenceEqual(this.parameters);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007DE8 File Offset: 0x00005FE8
		private string FindParameter(string name)
		{
			if (this.parameters == null)
			{
				return null;
			}
			foreach (NameValueHeaderValue nameValueHeaderValue in this.parameters)
			{
				if (string.Equals(nameValueHeaderValue.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return nameValueHeaderValue.Value;
				}
			}
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007E5C File Offset: 0x0000605C
		private DateTimeOffset? GetDateValue(string name)
		{
			string text = this.FindParameter(name);
			if (text == null || text == null)
			{
				return null;
			}
			if (text.Length < 3)
			{
				return null;
			}
			if (text[0] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			DateTimeOffset dateTimeOffset;
			if (Lexer.TryGetDateValue(text, out dateTimeOffset))
			{
				return new DateTimeOffset?(dateTimeOffset);
			}
			return null;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007ECC File Offset: 0x000060CC
		private static string EncodeBase64Value(string value)
		{
			bool flag = value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"';
			if (flag)
			{
				value = value.Substring(1, value.Length - 2);
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] > '\u007f')
				{
					Encoding utf = Encoding.UTF8;
					return string.Format("\"=?{0}?B?{1}?=\"", utf.WebName, Convert.ToBase64String(utf.GetBytes(value)));
				}
			}
			if (flag || !Lexer.IsValidToken(value))
			{
				return "\"" + value + "\"";
			}
			return value;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007F74 File Offset: 0x00006174
		private static string EncodeRFC5987(string value)
		{
			Encoding utf = Encoding.UTF8;
			StringBuilder stringBuilder = new StringBuilder(value.Length + 11);
			stringBuilder.Append(utf.WebName);
			stringBuilder.Append('\'');
			stringBuilder.Append('\'');
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					foreach (byte b in utf.GetBytes(new char[] { c }))
					{
						stringBuilder.Append('%');
						stringBuilder.Append(b.ToString("X2"));
					}
				}
				else if (!Lexer.IsValidCharacter(c) || c == '*' || c == '?' || c == '%')
				{
					stringBuilder.Append(Uri.HexEscape(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008058 File Offset: 0x00006258
		private static string DecodeValue(string value, bool extendedNotation)
		{
			if (value.Length < 2)
			{
				return value;
			}
			string[] array;
			Encoding encoding;
			if (value[0] == '"')
			{
				array = value.Split('?', StringSplitOptions.None);
				if (array.Length != 5 || array[0] != "\"=" || array[4] != "=\"" || (array[2] != "B" && array[2] != "b"))
				{
					return value;
				}
				try
				{
					encoding = Encoding.GetEncoding(array[1]);
					return encoding.GetString(Convert.FromBase64String(array[3]));
				}
				catch
				{
					return value;
				}
			}
			if (!extendedNotation)
			{
				return value;
			}
			array = value.Split('\'', StringSplitOptions.None);
			if (array.Length != 3)
			{
				return null;
			}
			try
			{
				encoding = Encoding.GetEncoding(array[0]);
			}
			catch
			{
				return null;
			}
			value = array[2];
			if (value.IndexOf('%') < 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = null;
			int num = 0;
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c == '%')
				{
					char c2 = c;
					c = Uri.HexUnescape(value, ref i);
					if (c != c2)
					{
						if (array2 == null)
						{
							array2 = new byte[value.Length - i + 1];
						}
						array2[num++] = (byte)c;
						continue;
					}
				}
				else
				{
					i++;
				}
				if (num != 0)
				{
					stringBuilder.Append(encoding.GetChars(array2, 0, num));
					num = 0;
				}
				stringBuilder.Append(c);
			}
			if (num != 0)
			{
				stringBuilder.Append(encoding.GetChars(array2, 0, num));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x060001EB RID: 491 RVA: 0x000081F0 File Offset: 0x000063F0
		public override int GetHashCode()
		{
			return this.dispositionType.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />  instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.An <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />  instance.</returns>
		/// <param name="input">A string that represents content disposition header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid content disposition header value information.</exception>
		// Token: 0x060001EC RID: 492 RVA: 0x00008210 File Offset: 0x00006410
		public static ContentDispositionHeaderValue Parse(string input)
		{
			ContentDispositionHeaderValue contentDispositionHeaderValue;
			if (ContentDispositionHeaderValue.TryParse(input, out contentDispositionHeaderValue))
			{
				return contentDispositionHeaderValue;
			}
			throw new FormatException(input);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008230 File Offset: 0x00006430
		private void SetDateValue(string key, DateTimeOffset? value)
		{
			this.SetValue(key, (value == null) ? null : ("\"" + value.Value.ToString("r", CultureInfo.InvariantCulture) + "\""));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008278 File Offset: 0x00006478
		private void SetValue(string key, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new List<NameValueHeaderValue>();
			}
			this.parameters.SetValue(key, value);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060001EF RID: 495 RVA: 0x0000829A File Offset: 0x0000649A
		public override string ToString()
		{
			return this.dispositionType + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> version of the string.</param>
		// Token: 0x060001F0 RID: 496 RVA: 0x000082B4 File Offset: 0x000064B4
		public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token.Kind != Token.Type.Token)
			{
				return false;
			}
			List<NameValueHeaderValue> list = null;
			string stringValue = lexer.GetStringValue(token);
			token = lexer.Scan(false);
			Token.Type kind = token.Kind;
			if (kind != Token.Type.End)
			{
				if (kind != Token.Type.SeparatorSemicolon)
				{
					return false;
				}
				if (!NameValueHeaderValue.TryParseParameters(lexer, out list, out token) || token != Token.Type.End)
				{
					return false;
				}
			}
			parsedValue = new ContentDispositionHeaderValue
			{
				dispositionType = stringValue,
				parameters = list
			};
			return true;
		}

		// Token: 0x040000EB RID: 235
		private string dispositionType;

		// Token: 0x040000EC RID: 236
		private List<NameValueHeaderValue> parameters;
	}
}
