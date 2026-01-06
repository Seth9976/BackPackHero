using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000409 RID: 1033
	internal class HttpProtocolUtils
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x0000219B File Offset: 0x0000039B
		private HttpProtocolUtils()
		{
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00078228 File Offset: 0x00076428
		internal static DateTime string2date(string S)
		{
			DateTime dateTime;
			if (HttpDateParse.ParseHttpDate(S, out dateTime))
			{
				return dateTime;
			}
			throw new ProtocolViolationException(SR.GetString("The value of the date string in the header is invalid."));
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00078250 File Offset: 0x00076450
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", dateTimeFormatInfo);
		}
	}
}
