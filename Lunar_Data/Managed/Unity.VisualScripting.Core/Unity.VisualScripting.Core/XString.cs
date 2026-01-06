using System;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x0200004D RID: 77
	internal static class XString
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00005F1B File Offset: 0x0000411B
		internal static string Inject(this string format, params object[] formattingArgs)
		{
			return string.Format(format, formattingArgs);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005F24 File Offset: 0x00004124
		internal static string Inject(this string format, params string[] formattingArgs)
		{
			return string.Format(format, formattingArgs.Select((string a) => a).ToArray<object>());
		}
	}
}
