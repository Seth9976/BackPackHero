using System;
using System.IO;

namespace Unity.VisualScripting
{
	// Token: 0x02000057 RID: 87
	public static class DebugUtility
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000654C File Offset: 0x0000474C
		public static string logPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ludiq.log");
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000655E File Offset: 0x0000475E
		public static void LogToFile(string message)
		{
			File.AppendAllText(DebugUtility.logPath, message + Environment.NewLine);
		}
	}
}
