using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000624 RID: 1572
	internal static class DotAtomReader
	{
		// Token: 0x0600326A RID: 12906 RVA: 0x000B50B0 File Offset: 0x000B32B0
		internal static int ReadReverse(string data, int index)
		{
			int num = index;
			while (0 <= index && ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || data[index] == MailBnfHelper.Dot || MailBnfHelper.Atext[(int)data[index]]))
			{
				index--;
			}
			if (num == index)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			}
			if (data[index + 1] == MailBnfHelper.Dot)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.Dot));
			}
			return index;
		}
	}
}
