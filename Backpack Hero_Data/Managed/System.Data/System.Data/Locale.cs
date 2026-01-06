using System;

// Token: 0x02000018 RID: 24
internal sealed class Locale
{
	// Token: 0x060000C3 RID: 195 RVA: 0x00003D55 File Offset: 0x00001F55
	private Locale()
	{
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000565A File Offset: 0x0000385A
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000565D File Offset: 0x0000385D
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
