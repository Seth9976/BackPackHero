using System;
using System.Text;

namespace TwitchLib.PubSub.Common
{
	// Token: 0x02000060 RID: 96
	public static class Helpers
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000750C File Offset: 0x0000570C
		public static DateTime DateTimeStringToObject(string dateTime)
		{
			if (dateTime != null)
			{
				return Convert.ToDateTime(dateTime);
			}
			return default(DateTime);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000752C File Offset: 0x0000572C
		public static string Base64Encode(string plainText)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}
	}
}
