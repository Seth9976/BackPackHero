using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000419 RID: 1049
	internal static class WebExceptionMapping
	{
		// Token: 0x0600212E RID: 8494 RVA: 0x00078C68 File Offset: 0x00076E68
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x0400132D RID: 4909
		private static readonly string[] s_Mapping = new string[21];
	}
}
