using System;

// Token: 0x0200001A RID: 26
internal class DiagnosticListener
{
	// Token: 0x060000D1 RID: 209 RVA: 0x00003D55 File Offset: 0x00001F55
	internal DiagnosticListener(string s)
	{
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00005701 File Offset: 0x00003901
	internal bool IsEnabled(string s)
	{
		return DiagnosticListener.DiagnosticListenerEnabled;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00005708 File Offset: 0x00003908
	internal void Write(string s1, object s2)
	{
		Console.WriteLine(string.Format("|| {0},  {1}", s1, s2));
	}

	// Token: 0x040003FC RID: 1020
	internal static bool DiagnosticListenerEnabled;
}
