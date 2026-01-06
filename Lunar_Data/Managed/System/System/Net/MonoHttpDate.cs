using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020004B9 RID: 1209
	internal class MonoHttpDate
	{
		// Token: 0x060026F2 RID: 9970 RVA: 0x00090B6C File Offset: 0x0008ED6C
		internal static DateTime Parse(string dateStr)
		{
			return DateTime.ParseExact(dateStr, MonoHttpDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
		}

		// Token: 0x0400169F RID: 5791
		private static readonly string rfc1123_date = "r";

		// Token: 0x040016A0 RID: 5792
		private static readonly string rfc850_date = "dddd, dd-MMM-yy HH:mm:ss G\\MT";

		// Token: 0x040016A1 RID: 5793
		private static readonly string asctime_date = "ddd MMM d HH:mm:ss yyyy";

		// Token: 0x040016A2 RID: 5794
		private static readonly string[] formats = new string[]
		{
			MonoHttpDate.rfc1123_date,
			MonoHttpDate.rfc850_date,
			MonoHttpDate.asctime_date
		};
	}
}
