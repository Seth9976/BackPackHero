using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000626 RID: 1574
	internal static class QuotedPairReader
	{
		// Token: 0x06003273 RID: 12915 RVA: 0x000B54A8 File Offset: 0x000B36A8
		internal static int CountQuotedChars(string data, int index, bool permitUnicodeEscaping)
		{
			if (index <= 0 || data[index - 1] != MailBnfHelper.Backslash)
			{
				return 0;
			}
			int num = QuotedPairReader.CountBackslashes(data, index - 1);
			if (num % 2 == 0)
			{
				return 0;
			}
			if (!permitUnicodeEscaping && (int)data[index] > MailBnfHelper.Ascii7bitMaxValue)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			}
			return num + 1;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000B550C File Offset: 0x000B370C
		private static int CountBackslashes(string data, int index)
		{
			int num = 0;
			do
			{
				num++;
				index--;
			}
			while (index >= 0 && data[index] == MailBnfHelper.Backslash);
			return num;
		}
	}
}
