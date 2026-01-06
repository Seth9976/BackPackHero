using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000625 RID: 1573
	internal static class MailAddressParser
	{
		// Token: 0x0600326B RID: 12907 RVA: 0x000B5140 File Offset: 0x000B3340
		internal static MailAddress ParseAddress(string data)
		{
			int num = data.Length - 1;
			return MailAddressParser.ParseAddress(data, false, ref num);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000B5160 File Offset: 0x000B3360
		internal static List<MailAddress> ParseMultipleAddresses(string data)
		{
			List<MailAddress> list = new List<MailAddress>();
			for (int i = data.Length - 1; i >= 0; i--)
			{
				list.Insert(0, MailAddressParser.ParseAddress(data, true, ref i));
			}
			return list;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000B5198 File Offset: 0x000B3398
		private static MailAddress ParseAddress(string data, bool expectMultipleAddresses, ref int index)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			bool flag = false;
			if (data[index] == MailBnfHelper.EndAngleBracket)
			{
				flag = true;
				index--;
			}
			string text = MailAddressParser.ParseDomain(data, ref index);
			if (data[index] != MailBnfHelper.At)
			{
				throw new FormatException("The specified string is not in the form required for an e-mail address.");
			}
			index--;
			string text2 = MailAddressParser.ParseLocalPart(data, ref index, flag, expectMultipleAddresses);
			if (flag)
			{
				if (index < 0 || data[index] != MailBnfHelper.StartAngleBracket)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", (index >= 0) ? data[index] : MailBnfHelper.EndAngleBracket));
				}
				index--;
				index = WhitespaceReader.ReadFwsReverse(data, index);
			}
			string text3;
			if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
			{
				text3 = MailAddressParser.ParseDisplayName(data, ref index, expectMultipleAddresses);
			}
			else
			{
				text3 = string.Empty;
			}
			return new MailAddress(text3, text2, text);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000B5282 File Offset: 0x000B3482
		private static int ReadCfwsAndThrowIfIncomplete(string data, int index)
		{
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			if (index < 0)
			{
				throw new FormatException("The specified string is not in the form required for an e-mail address.");
			}
			return index;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000B52A0 File Offset: 0x000B34A0
		private static string ParseDomain(string data, ref int index)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.EndSquareBracket)
			{
				index = DomainLiteralReader.ReadReverse(data, index);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
			}
			string text = data.Substring(index + 1, num - index);
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000B52FC File Offset: 0x000B34FC
		private static string ParseLocalPart(string data, ref int index, bool expectAngleBracket, bool expectMultipleAddresses)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, index, true);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
				if (index >= 0 && !MailBnfHelper.IsAllowedWhiteSpace(data[index]) && data[index] != MailBnfHelper.EndComment && (!expectAngleBracket || data[index] != MailBnfHelper.StartAngleBracket) && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma) && data[index] != MailBnfHelper.Quote)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
				}
			}
			string text = data.Substring(index + 1, num - index);
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000B53CC File Offset: 0x000B35CC
		private static string ParseDisplayName(string data, ref int index, bool expectMultipleAddresses)
		{
			int num = WhitespaceReader.ReadCfwsReverse(data, index);
			string text;
			if (num >= 0 && data[num] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, num, true);
				int num2 = index + 2;
				text = data.Substring(num2, num - num2);
				index = WhitespaceReader.ReadCfwsReverse(data, index);
				if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
				}
			}
			else
			{
				int num3 = index;
				index = QuotedStringFormatReader.ReadReverseUnQuoted(data, index, true, expectMultipleAddresses);
				text = data.SubstringTrim(index + 1, num3 - index);
			}
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000B5470 File Offset: 0x000B3670
		internal static string NormalizeOrThrow(string input)
		{
			string text;
			try
			{
				text = input.Normalize(NormalizationForm.FormC);
			}
			catch (ArgumentException ex)
			{
				throw new FormatException("The specified string is not in the form required for an e-mail address.", ex);
			}
			return text;
		}
	}
}
