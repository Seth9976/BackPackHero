using System;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000602 RID: 1538
	internal static class MailBnfHelper
	{
		// Token: 0x06003152 RID: 12626 RVA: 0x000B0544 File Offset: 0x000AE744
		private static bool[] CreateCharactersAllowedInAtoms()
		{
			bool[] array = new bool[128];
			for (int i = 48; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 65; j <= 90; j++)
			{
				array[j] = true;
			}
			for (int k = 97; k <= 122; k++)
			{
				array[k] = true;
			}
			array[33] = true;
			array[35] = true;
			array[36] = true;
			array[37] = true;
			array[38] = true;
			array[39] = true;
			array[42] = true;
			array[43] = true;
			array[45] = true;
			array[47] = true;
			array[61] = true;
			array[63] = true;
			array[94] = true;
			array[95] = true;
			array[96] = true;
			array[123] = true;
			array[124] = true;
			array[125] = true;
			array[126] = true;
			return array;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000B05F4 File Offset: 0x000AE7F4
		private static bool[] CreateCharactersAllowedInQuotedStrings()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 9; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 33; j++)
			{
				array[j] = true;
			}
			for (int k = 35; k <= 91; k++)
			{
				array[k] = true;
			}
			for (int l = 93; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000B0664 File Offset: 0x000AE864
		private static bool[] CreateCharactersAllowedInDomainLiterals()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 90; k++)
			{
				array[k] = true;
			}
			for (int l = 94; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000B06D4 File Offset: 0x000AE8D4
		private static bool[] CreateCharactersAllowedInHeaderNames()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 59; j <= 126; j++)
			{
				array[j] = true;
			}
			return array;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000B0714 File Offset: 0x000AE914
		private static bool[] CreateCharactersAllowedInTokens()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 126; i++)
			{
				array[i] = true;
			}
			array[40] = false;
			array[41] = false;
			array[60] = false;
			array[62] = false;
			array[64] = false;
			array[44] = false;
			array[59] = false;
			array[58] = false;
			array[92] = false;
			array[34] = false;
			array[47] = false;
			array[91] = false;
			array[93] = false;
			array[63] = false;
			array[61] = false;
			return array;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000B078C File Offset: 0x000AE98C
		private static bool[] CreateCharactersAllowedInComments()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 39; k++)
			{
				array[k] = true;
			}
			for (int l = 42; l <= 91; l++)
			{
				array[l] = true;
			}
			for (int m = 93; m <= 127; m++)
			{
				array[m] = true;
			}
			return array;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000B0814 File Offset: 0x000AEA14
		internal static bool SkipCFWS(string data, ref int offset)
		{
			int num = 0;
			while (offset < data.Length)
			{
				if (data[offset] > '\u007f')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (data[offset] == '\\' && num > 0)
				{
					offset += 2;
				}
				else if (data[offset] == '(')
				{
					num++;
				}
				else if (data[offset] == ')')
				{
					num--;
				}
				else if (data[offset] != ' ' && data[offset] != '\t' && num == 0)
				{
					return true;
				}
				if (num < 0)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			return false;
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000B08E0 File Offset: 0x000AEAE0
		internal static void ValidateHeaderName(string data)
		{
			int i;
			for (i = 0; i < data.Length; i++)
			{
				if ((int)data[i] > MailBnfHelper.Ftext.Length || !MailBnfHelper.Ftext[(int)data[i]])
				{
					throw new FormatException("An invalid character was found in header name.");
				}
			}
			if (i == 0)
			{
				throw new FormatException("An invalid character was found in header name.");
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000B0936 File Offset: 0x000AEB36
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder)
		{
			return MailBnfHelper.ReadQuotedString(data, ref offset, builder, false, false);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000B0944 File Offset: 0x000AEB44
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder, bool doesntRequireQuotes, bool permitUnicodeInDisplayName)
		{
			if (!doesntRequireQuotes)
			{
				offset++;
			}
			int num = offset;
			StringBuilder stringBuilder = ((builder != null) ? builder : new StringBuilder());
			while (offset < data.Length)
			{
				if (data[offset] == '\\')
				{
					stringBuilder.Append(data, num, offset - num);
					int num2 = offset + 1;
					offset = num2;
					num = num2;
				}
				else if (data[offset] == '"')
				{
					stringBuilder.Append(data, num, offset - num);
					offset++;
					if (builder == null)
					{
						return stringBuilder.ToString();
					}
					return null;
				}
				else if (data[offset] == '=' && data.Length > offset + 3 && data[offset + 1] == '\r' && data[offset + 2] == '\n' && (data[offset + 3] == ' ' || data[offset + 3] == '\t'))
				{
					offset += 3;
				}
				else if (permitUnicodeInDisplayName)
				{
					if ((int)data[offset] <= MailBnfHelper.Ascii7bitMaxValue && !MailBnfHelper.Qtext[(int)data[offset]])
					{
						throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
					}
				}
				else if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Qtext[(int)data[offset]])
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			if (!doesntRequireQuotes)
			{
				throw new FormatException("The mail header is malformed.");
			}
			stringBuilder.Append(data, num, offset - num);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000B0ACE File Offset: 0x000AECCE
		internal static string ReadParameterAttribute(string data, ref int offset, StringBuilder builder)
		{
			if (!MailBnfHelper.SkipCFWS(data, ref offset))
			{
				return null;
			}
			return MailBnfHelper.ReadToken(data, ref offset, null);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000B0AE4 File Offset: 0x000AECE4
		internal static string ReadToken(string data, ref int offset, StringBuilder builder)
		{
			int num = offset;
			while (offset < data.Length)
			{
				if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (!MailBnfHelper.Ttext[(int)data[offset]])
				{
					break;
				}
				offset++;
			}
			if (num == offset)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
			}
			return data.Substring(num, offset - num);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000B0B70 File Offset: 0x000AED70
		internal static string GetDateTimeString(DateTime value, StringBuilder builder)
		{
			StringBuilder stringBuilder = ((builder != null) ? builder : new StringBuilder());
			stringBuilder.Append(value.Day);
			stringBuilder.Append(' ');
			stringBuilder.Append(MailBnfHelper.s_months[value.Month]);
			stringBuilder.Append(' ');
			stringBuilder.Append(value.Year);
			stringBuilder.Append(' ');
			if (value.Hour <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Hour);
			stringBuilder.Append(':');
			if (value.Minute <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Minute);
			stringBuilder.Append(':');
			if (value.Second <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Second);
			string text = TimeZoneInfo.Local.GetUtcOffset(value).ToString();
			if (text[0] != '-')
			{
				stringBuilder.Append(" +");
			}
			else
			{
				stringBuilder.Append(' ');
			}
			string[] array = text.Split(MailBnfHelper.s_colonSeparator);
			stringBuilder.Append(array[0]);
			stringBuilder.Append(array[1]);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		internal static void GetTokenOrQuotedString(string data, StringBuilder builder, bool allowUnicode)
		{
			int i = 0;
			int num = 0;
			while (i < data.Length)
			{
				if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode) && (!MailBnfHelper.Ttext[(int)data[i]] || data[i] == ' '))
				{
					builder.Append('"');
					while (i < data.Length)
					{
						if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode))
						{
							if (MailBnfHelper.IsFWSAt(data, i))
							{
								i += 2;
							}
							else if (!MailBnfHelper.Qtext[(int)data[i]])
							{
								builder.Append(data, num, i - num);
								builder.Append('\\');
								num = i;
							}
						}
						i++;
					}
					builder.Append(data, num, i - num);
					builder.Append('"');
					return;
				}
				i++;
			}
			if (data.Length == 0)
			{
				builder.Append("\"\"");
			}
			builder.Append(data);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000B0D94 File Offset: 0x000AEF94
		private static bool CheckForUnicode(char ch, bool allowUnicode)
		{
			if ((int)ch < MailBnfHelper.Ascii7bitMaxValue)
			{
				return false;
			}
			if (!allowUnicode)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", ch));
			}
			return true;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000B0DBA File Offset: 0x000AEFBA
		internal static bool IsAllowedWhiteSpace(char c)
		{
			return c == MailBnfHelper.Tab || c == MailBnfHelper.Space || c == MailBnfHelper.CR || c == MailBnfHelper.LF;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000B0DE0 File Offset: 0x000AEFE0
		internal static bool HasCROrLF(string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] == '\r' || data[i] == '\n')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000B0E18 File Offset: 0x000AF018
		internal static bool IsFWSAt(string data, int index)
		{
			return data[index] == MailBnfHelper.CR && index + 2 < data.Length && data[index + 1] == MailBnfHelper.LF && (data[index + 2] == MailBnfHelper.Space || data[index + 2] == MailBnfHelper.Tab);
		}

		// Token: 0x04001E1E RID: 7710
		internal static readonly bool[] Atext = MailBnfHelper.CreateCharactersAllowedInAtoms();

		// Token: 0x04001E1F RID: 7711
		internal static readonly bool[] Qtext = MailBnfHelper.CreateCharactersAllowedInQuotedStrings();

		// Token: 0x04001E20 RID: 7712
		internal static readonly bool[] Dtext = MailBnfHelper.CreateCharactersAllowedInDomainLiterals();

		// Token: 0x04001E21 RID: 7713
		internal static readonly bool[] Ftext = MailBnfHelper.CreateCharactersAllowedInHeaderNames();

		// Token: 0x04001E22 RID: 7714
		internal static readonly bool[] Ttext = MailBnfHelper.CreateCharactersAllowedInTokens();

		// Token: 0x04001E23 RID: 7715
		internal static readonly bool[] Ctext = MailBnfHelper.CreateCharactersAllowedInComments();

		// Token: 0x04001E24 RID: 7716
		internal static readonly int Ascii7bitMaxValue = 127;

		// Token: 0x04001E25 RID: 7717
		internal static readonly char Quote = '"';

		// Token: 0x04001E26 RID: 7718
		internal static readonly char Space = ' ';

		// Token: 0x04001E27 RID: 7719
		internal static readonly char Tab = '\t';

		// Token: 0x04001E28 RID: 7720
		internal static readonly char CR = '\r';

		// Token: 0x04001E29 RID: 7721
		internal static readonly char LF = '\n';

		// Token: 0x04001E2A RID: 7722
		internal static readonly char StartComment = '(';

		// Token: 0x04001E2B RID: 7723
		internal static readonly char EndComment = ')';

		// Token: 0x04001E2C RID: 7724
		internal static readonly char Backslash = '\\';

		// Token: 0x04001E2D RID: 7725
		internal static readonly char At = '@';

		// Token: 0x04001E2E RID: 7726
		internal static readonly char EndAngleBracket = '>';

		// Token: 0x04001E2F RID: 7727
		internal static readonly char StartAngleBracket = '<';

		// Token: 0x04001E30 RID: 7728
		internal static readonly char StartSquareBracket = '[';

		// Token: 0x04001E31 RID: 7729
		internal static readonly char EndSquareBracket = ']';

		// Token: 0x04001E32 RID: 7730
		internal static readonly char Comma = ',';

		// Token: 0x04001E33 RID: 7731
		internal static readonly char Dot = '.';

		// Token: 0x04001E34 RID: 7732
		private static readonly char[] s_colonSeparator = new char[] { ':' };

		// Token: 0x04001E35 RID: 7733
		private static string[] s_months = new string[]
		{
			null, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep",
			"Oct", "Nov", "Dec"
		};
	}
}
