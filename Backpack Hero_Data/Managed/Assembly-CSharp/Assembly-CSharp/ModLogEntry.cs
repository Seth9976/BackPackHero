using System;

// Token: 0x02000132 RID: 306
public class ModLogEntry
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00078EBF File Offset: 0x000770BF
	public DateTime Timestamp { get; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00078EC7 File Offset: 0x000770C7
	public LogLevel Level { get; }

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00078ECF File Offset: 0x000770CF
	public string Modpack { get; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00078ED7 File Offset: 0x000770D7
	public string Component { get; }

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00078EDF File Offset: 0x000770DF
	public string Message { get; }

	// Token: 0x06000B85 RID: 2949 RVA: 0x00078EE7 File Offset: 0x000770E7
	public ModLogEntry(DateTime timestamp, LogLevel level, string modpack, string component, string message)
	{
		this.Timestamp = timestamp;
		this.Level = level;
		this.Modpack = modpack;
		this.Component = component;
		this.Message = message;
	}
}
