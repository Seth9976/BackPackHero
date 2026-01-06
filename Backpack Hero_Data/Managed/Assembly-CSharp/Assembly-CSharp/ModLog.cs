using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000131 RID: 305
public static class ModLog
{
	// Token: 0x06000B75 RID: 2933 RVA: 0x00078BA6 File Offset: 0x00076DA6
	static ModLog()
	{
		ModLog.StartNewSession();
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00078BE8 File Offset: 0x00076DE8
	private static void StartNewSession()
	{
		try
		{
			ModLog.EndSession();
			ModLog.currentLogFile = ModLog.root + "mods.log";
			string text = ModLog.root + "mods.old.log";
			if (File.Exists(ModLog.currentLogFile))
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Move(ModLog.currentLogFile, text);
			}
			else
			{
				File.Create(ModLog.currentLogFile).Close();
			}
			StreamWriter streamWriter = ModLog.fileWriter;
			if (streamWriter != null)
			{
				streamWriter.Dispose();
			}
			ModLog.fileWriter = new StreamWriter(new FileStream(ModLog.currentLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00078C94 File Offset: 0x00076E94
	public static void EndSession()
	{
		StreamWriter streamWriter = ModLog.fileWriter;
		if (streamWriter == null)
		{
			return;
		}
		streamWriter.Dispose();
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00078CA5 File Offset: 0x00076EA5
	public static void Log(string message)
	{
		Debug.Log("ModLoader: " + message);
		ModLog.AddLog(LogLevel.Debug, "", "", message);
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00078CC8 File Offset: 0x00076EC8
	public static void LogWarning(string message)
	{
		Debug.LogWarning("ModLoader: " + message);
		ModLog.AddLog(LogLevel.Warning, "", "", message);
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00078CEB File Offset: 0x00076EEB
	public static void LogError(string message)
	{
		Debug.LogError("ModLoader: " + message);
		ModLog.AddLog(LogLevel.Warning, "", "", message);
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00078D0E File Offset: 0x00076F0E
	public static void Log(string modpack, string component, string message)
	{
		Debug.Log(string.Concat(new string[] { "ModLoader: ", modpack, " - ", component, ": ", message }));
		ModLog.AddLog(LogLevel.Debug, modpack, component, message);
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00078D4D File Offset: 0x00076F4D
	public static void LogWarning(string modpack, string component, string message)
	{
		Debug.LogWarning(string.Concat(new string[] { "ModLoader: ", modpack, " - ", component, ": ", message }));
		ModLog.AddLog(LogLevel.Warning, modpack, component, message);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00078D8C File Offset: 0x00076F8C
	public static void LogError(string modpack, string component, string message)
	{
		Debug.LogError(string.Concat(new string[] { "ModLoader: ", modpack, " - ", component, ": ", message }));
		ModLog.AddLog(LogLevel.Error, modpack, component, message);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00078DCC File Offset: 0x00076FCC
	public static void AddLog(LogLevel level, string modpack, string component, string message)
	{
		try
		{
			ModLog.fileWriter.WriteLine(string.Format("{0} {1} - {2}: {3}", new object[] { level, modpack, component, message }));
			ModLog.fileWriter.Flush();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		ModLogEntry modLogEntry = new ModLogEntry(DateTime.Now, level, modpack, component, message);
		ModLog.buffer[(ModLog.startIndex + ModLog.count) % ModLog.bufferSize] = modLogEntry;
		if (ModLog.count < ModLog.bufferSize)
		{
			ModLog.count++;
			return;
		}
		ModLog.startIndex = (ModLog.startIndex + 1) % ModLog.bufferSize;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00078E80 File Offset: 0x00077080
	public static List<ModLogEntry> GetLogs()
	{
		List<ModLogEntry> list = new List<ModLogEntry>();
		for (int i = 0; i < ModLog.count; i++)
		{
			int num = (ModLog.startIndex + i) % ModLog.bufferSize;
			list.Add(ModLog.buffer[num]);
		}
		return list;
	}

	// Token: 0x04000957 RID: 2391
	private static ModLogEntry[] buffer = new ModLogEntry[ModLog.bufferSize];

	// Token: 0x04000958 RID: 2392
	private static int startIndex = 0;

	// Token: 0x04000959 RID: 2393
	private static int count = 0;

	// Token: 0x0400095A RID: 2394
	private static int bufferSize = 5000;

	// Token: 0x0400095B RID: 2395
	private static string currentLogFile;

	// Token: 0x0400095C RID: 2396
	private static string previousLogFile;

	// Token: 0x0400095D RID: 2397
	private static StreamWriter fileWriter;

	// Token: 0x0400095E RID: 2398
	private static string root = Application.persistentDataPath + "/";
}
