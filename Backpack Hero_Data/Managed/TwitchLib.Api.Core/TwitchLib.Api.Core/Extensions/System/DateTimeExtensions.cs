using System;
using System.Globalization;

namespace TwitchLib.Api.Core.Extensions.System
{
	// Token: 0x02000010 RID: 16
	public static class DateTimeExtensions
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000035D4 File Offset: 0x000017D4
		public static string ToRfc3339String(this DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
		}
	}
}
