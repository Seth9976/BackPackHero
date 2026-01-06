using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200019F RID: 415
	public class fsJsonParser
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		private fsJsonParser(string input)
		{
			this._input = input;
			this._start = 0;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		private fsResult MakeFailure(string message)
		{
			int num = Math.Max(0, this._start - 20);
			int num2 = Math.Min(50, this._input.Length - num);
			return fsResult.Fail(string.Concat(new string[]
			{
				"Error while parsing: ",
				message,
				"; context = <",
				this._input.Substring(num, num2),
				">"
			}));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002D65E File Offset: 0x0002B85E
		private bool TryMoveNext()
		{
			if (this._start < this._input.Length)
			{
				this._start++;
				return true;
			}
			return false;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002D684 File Offset: 0x0002B884
		private bool HasValue()
		{
			return this.HasValue(0);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002D68D File Offset: 0x0002B88D
		private bool HasValue(int offset)
		{
			return this._start + offset >= 0 && this._start + offset < this._input.Length;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002D6B1 File Offset: 0x0002B8B1
		private char Character()
		{
			return this.Character(0);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002D6BA File Offset: 0x0002B8BA
		private char Character(int offset)
		{
			return this._input[this._start + offset];
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002D6D0 File Offset: 0x0002B8D0
		private void SkipSpace()
		{
			while (this.HasValue())
			{
				if (char.IsWhiteSpace(this.Character()))
				{
					this.TryMoveNext();
				}
				else
				{
					if (!this.HasValue(1) || this.Character(0) != '/')
					{
						break;
					}
					if (this.Character(1) == '/')
					{
						while (this.HasValue())
						{
							if (Environment.NewLine.Contains(this.Character().ToString() ?? ""))
							{
								break;
							}
							this.TryMoveNext();
						}
					}
					else if (this.Character(1) == '*')
					{
						this.TryMoveNext();
						this.TryMoveNext();
						while (this.HasValue(1))
						{
							if (this.Character(0) == '*' && this.Character(1) == '/')
							{
								this.TryMoveNext();
								this.TryMoveNext();
								this.TryMoveNext();
								break;
							}
							this.TryMoveNext();
						}
					}
				}
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002D7BC File Offset: 0x0002B9BC
		private fsResult TryParseExact(string content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				if (this.Character() != content[i])
				{
					return this.MakeFailure("Expected " + content[i].ToString());
				}
				if (!this.TryMoveNext())
				{
					return this.MakeFailure("Unexpected end of content when parsing " + content);
				}
			}
			return fsResult.Success;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002D828 File Offset: 0x0002BA28
		private fsResult TryParseTrue(out fsData data)
		{
			fsResult fsResult = this.TryParseExact("true");
			if (fsResult.Succeeded)
			{
				data = new fsData(true);
				return fsResult.Success;
			}
			data = null;
			return fsResult;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002D85C File Offset: 0x0002BA5C
		private fsResult TryParseFalse(out fsData data)
		{
			fsResult fsResult = this.TryParseExact("false");
			if (fsResult.Succeeded)
			{
				data = new fsData(false);
				return fsResult.Success;
			}
			data = null;
			return fsResult;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002D890 File Offset: 0x0002BA90
		private fsResult TryParseNull(out fsData data)
		{
			fsResult fsResult = this.TryParseExact("null");
			if (fsResult.Succeeded)
			{
				data = new fsData();
				return fsResult.Success;
			}
			data = null;
			return fsResult;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002D8C3 File Offset: 0x0002BAC3
		private bool IsSeparator(char c)
		{
			return char.IsWhiteSpace(c) || c == ',' || c == '}' || c == ']';
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		private fsResult TryParseNumber(out fsData data)
		{
			int start = this._start;
			while (this.TryMoveNext() && this.HasValue() && !this.IsSeparator(this.Character()))
			{
			}
			string text = this._input.Substring(start, this._start - start);
			if (text.Contains(".") || text.Contains("e") || text.Contains("E") || text == "Infinity" || text == "-Infinity" || text == "NaN")
			{
				double num;
				if (!double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
				{
					data = null;
					return this.MakeFailure("Bad double format with " + text);
				}
				data = new fsData(num);
				return fsResult.Success;
			}
			else
			{
				long num2;
				if (!long.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
				{
					data = null;
					return this.MakeFailure("Bad Int64 format with " + text);
				}
				data = new fsData(num2);
				return fsResult.Success;
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
		private fsResult TryParseString(out string str)
		{
			this._cachedStringBuilder.Length = 0;
			if (this.Character() != '"' || !this.TryMoveNext())
			{
				str = string.Empty;
				return this.MakeFailure("Expected initial \" when parsing a string");
			}
			while (this.HasValue() && this.Character() != '"')
			{
				char c = this.Character();
				if (c == '\\')
				{
					char c2;
					fsResult fsResult = this.TryUnescapeChar(out c2);
					if (fsResult.Failed)
					{
						str = string.Empty;
						return fsResult;
					}
					this._cachedStringBuilder.Append(c2);
				}
				else
				{
					this._cachedStringBuilder.Append(c);
					if (!this.TryMoveNext())
					{
						str = string.Empty;
						return this.MakeFailure("Unexpected end of input when reading a string");
					}
				}
			}
			if (!this.HasValue() || this.Character() != '"' || !this.TryMoveNext())
			{
				str = string.Empty;
				return this.MakeFailure("No closing \" when parsing a string");
			}
			str = this._cachedStringBuilder.ToString();
			return fsResult.Success;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
		private fsResult TryParseArray(out fsData arr)
		{
			if (this.Character() != '[')
			{
				arr = null;
				return this.MakeFailure("Expected initial [ when parsing an array");
			}
			if (!this.TryMoveNext())
			{
				arr = null;
				return this.MakeFailure("Unexpected end of input when parsing an array");
			}
			this.SkipSpace();
			List<fsData> list = new List<fsData>();
			while (this.HasValue() && this.Character() != ']')
			{
				fsData fsData;
				fsResult fsResult = this.RunParse(out fsData);
				if (fsResult.Failed)
				{
					arr = null;
					return fsResult;
				}
				list.Add(fsData);
				this.SkipSpace();
				if (this.HasValue() && this.Character() == ',')
				{
					if (!this.TryMoveNext())
					{
						break;
					}
					this.SkipSpace();
				}
			}
			if (!this.HasValue() || this.Character() != ']' || !this.TryMoveNext())
			{
				arr = null;
				return this.MakeFailure("No closing ] for array");
			}
			arr = new fsData(list);
			return fsResult.Success;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		private fsResult TryParseObject(out fsData obj)
		{
			if (this.Character() != '{')
			{
				obj = null;
				return this.MakeFailure("Expected initial { when parsing an object");
			}
			if (!this.TryMoveNext())
			{
				obj = null;
				return this.MakeFailure("Unexpected end of input when parsing an object");
			}
			this.SkipSpace();
			Dictionary<string, fsData> dictionary = new Dictionary<string, fsData>(fsGlobalConfig.IsCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			while (this.HasValue() && this.Character() != '}')
			{
				this.SkipSpace();
				string text;
				fsResult fsResult = this.TryParseString(out text);
				if (fsResult.Failed)
				{
					obj = null;
					return fsResult;
				}
				this.SkipSpace();
				if (!this.HasValue() || this.Character() != ':' || !this.TryMoveNext())
				{
					obj = null;
					return this.MakeFailure("Expected : after key \"" + text + "\"");
				}
				this.SkipSpace();
				fsData fsData;
				fsResult = this.RunParse(out fsData);
				if (fsResult.Failed)
				{
					obj = null;
					return fsResult;
				}
				dictionary.Add(text, fsData);
				this.SkipSpace();
				if (this.HasValue() && this.Character() == ',')
				{
					if (!this.TryMoveNext())
					{
						break;
					}
					this.SkipSpace();
				}
			}
			if (!this.HasValue() || this.Character() != '}' || !this.TryMoveNext())
			{
				obj = null;
				return this.MakeFailure("No closing } for object");
			}
			obj = new fsData(dictionary);
			return fsResult.Success;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002DCFC File Offset: 0x0002BEFC
		private fsResult RunParse(out fsData data)
		{
			this.SkipSpace();
			if (!this.HasValue())
			{
				data = null;
				return this.MakeFailure("Unexpected end of input");
			}
			char c = this.Character();
			if (c <= '[')
			{
				if (c <= 'I')
				{
					switch (c)
					{
					case '"':
					{
						string text;
						fsResult fsResult = this.TryParseString(out text);
						if (fsResult.Failed)
						{
							data = null;
							return fsResult;
						}
						data = new fsData(text);
						return fsResult.Success;
					}
					case '#':
					case '$':
					case '%':
					case '&':
					case '\'':
					case '(':
					case ')':
					case '*':
					case ',':
					case '/':
						goto IL_011F;
					case '+':
					case '-':
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						break;
					default:
						if (c != 'I')
						{
							goto IL_011F;
						}
						break;
					}
				}
				else if (c != 'N')
				{
					if (c != '[')
					{
						goto IL_011F;
					}
					return this.TryParseArray(out data);
				}
				return this.TryParseNumber(out data);
			}
			if (c <= 'n')
			{
				if (c == 'f')
				{
					return this.TryParseFalse(out data);
				}
				if (c == 'n')
				{
					return this.TryParseNull(out data);
				}
			}
			else
			{
				if (c == 't')
				{
					return this.TryParseTrue(out data);
				}
				if (c == '{')
				{
					return this.TryParseObject(out data);
				}
			}
			IL_011F:
			data = null;
			return this.MakeFailure("unable to parse; invalid token \"" + this.Character().ToString() + "\"");
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002DE4E File Offset: 0x0002C04E
		public static fsResult Parse(string input, out fsData data)
		{
			if (string.IsNullOrEmpty(input))
			{
				data = null;
				return fsResult.Fail("No input");
			}
			return new fsJsonParser(input).RunParse(out data);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002DE74 File Offset: 0x0002C074
		public static fsData Parse(string input)
		{
			fsData fsData;
			fsJsonParser.Parse(input, out fsData).AssertSuccess();
			return fsData;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002DE93 File Offset: 0x0002C093
		private bool IsHex(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002DEBC File Offset: 0x0002C0BC
		private uint ParseSingleChar(char c1, uint multipliyer)
		{
			uint num = 0U;
			if (c1 >= '0' && c1 <= '9')
			{
				num = (uint)(c1 - '0') * multipliyer;
			}
			else if (c1 >= 'A' && c1 <= 'F')
			{
				num = (uint)(c1 - 'A' + '\n') * multipliyer;
			}
			else if (c1 >= 'a' && c1 <= 'f')
			{
				num = (uint)(c1 - 'a' + '\n') * multipliyer;
			}
			return num;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002DF0C File Offset: 0x0002C10C
		private uint ParseUnicode(char c1, char c2, char c3, char c4)
		{
			uint num = this.ParseSingleChar(c1, 4096U);
			uint num2 = this.ParseSingleChar(c2, 256U);
			uint num3 = this.ParseSingleChar(c3, 16U);
			uint num4 = this.ParseSingleChar(c4, 1U);
			return num + num2 + num3 + num4;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002DF4C File Offset: 0x0002C14C
		private fsResult TryUnescapeChar(out char escaped)
		{
			this.TryMoveNext();
			if (!this.HasValue())
			{
				escaped = ' ';
				return this.MakeFailure("Unexpected end of input after \\");
			}
			char c = this.Character();
			if (c <= '\\')
			{
				if (c <= '/')
				{
					if (c == '"')
					{
						this.TryMoveNext();
						escaped = '"';
						return fsResult.Success;
					}
					if (c == '/')
					{
						this.TryMoveNext();
						escaped = '/';
						return fsResult.Success;
					}
				}
				else
				{
					if (c == '0')
					{
						this.TryMoveNext();
						escaped = '\0';
						return fsResult.Success;
					}
					if (c == '\\')
					{
						this.TryMoveNext();
						escaped = '\\';
						return fsResult.Success;
					}
				}
			}
			else if (c <= 'b')
			{
				if (c == 'a')
				{
					this.TryMoveNext();
					escaped = '\a';
					return fsResult.Success;
				}
				if (c == 'b')
				{
					this.TryMoveNext();
					escaped = '\b';
					return fsResult.Success;
				}
			}
			else
			{
				if (c == 'f')
				{
					this.TryMoveNext();
					escaped = '\f';
					return fsResult.Success;
				}
				if (c == 'n')
				{
					this.TryMoveNext();
					escaped = '\n';
					return fsResult.Success;
				}
				switch (c)
				{
				case 'r':
					this.TryMoveNext();
					escaped = '\r';
					return fsResult.Success;
				case 't':
					this.TryMoveNext();
					escaped = '\t';
					return fsResult.Success;
				case 'u':
					this.TryMoveNext();
					if (this.IsHex(this.Character(0)) && this.IsHex(this.Character(1)) && this.IsHex(this.Character(2)) && this.IsHex(this.Character(3)))
					{
						uint num = this.ParseUnicode(this.Character(0), this.Character(1), this.Character(2), this.Character(3));
						this.TryMoveNext();
						this.TryMoveNext();
						this.TryMoveNext();
						this.TryMoveNext();
						escaped = (char)num;
						return fsResult.Success;
					}
					escaped = '\0';
					return this.MakeFailure(string.Format("invalid escape sequence '\\u{0}{1}{2}{3}'\n", new object[]
					{
						this.Character(0),
						this.Character(1),
						this.Character(2),
						this.Character(3)
					}));
				}
			}
			escaped = '\0';
			return this.MakeFailure(string.Format("Invalid escape sequence \\{0}", this.Character()));
		}

		// Token: 0x04000286 RID: 646
		private readonly StringBuilder _cachedStringBuilder = new StringBuilder(256);

		// Token: 0x04000287 RID: 647
		private int _start;

		// Token: 0x04000288 RID: 648
		private string _input;
	}
}
