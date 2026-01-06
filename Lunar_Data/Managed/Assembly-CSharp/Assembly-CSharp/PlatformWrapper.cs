using System;
using System.IO;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class PlatformWrapper
{
	// Token: 0x06000306 RID: 774 RVA: 0x0000F867 File Offset: 0x0000DA67
	public static PlatformWrapper.Platform GetCurrentPlatform()
	{
		return PlatformWrapper.Platform.Standalone;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000F86A File Offset: 0x0000DA6A
	public static byte[] LoadFile(string file_path)
	{
		if (!PlatformWrapper.FileExists(file_path))
		{
			return null;
		}
		return File.ReadAllBytes(PlatformWrapper.ApplyCWD(file_path));
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000F881 File Offset: 0x0000DA81
	public static void SaveFile(string file_path, byte[] bytes)
	{
		File.WriteAllBytes(PlatformWrapper.ApplyCWD(file_path), bytes);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000F88F File Offset: 0x0000DA8F
	public static bool FileExists(string file_path)
	{
		return File.Exists(PlatformWrapper.ApplyCWD(file_path));
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000F89C File Offset: 0x0000DA9C
	public static void ChangeToDefaultCWD()
	{
		PlatformWrapper.virtualCWD = Application.persistentDataPath;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
	public static string ApplyCWD(string file_path)
	{
		if (!Path.IsPathRooted(file_path))
		{
			return PlatformWrapper.virtualCWD + "/" + file_path;
		}
		return file_path;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
	public static byte[] LoadFileDefaultDir(string file_path)
	{
		string text = PlatformWrapper.virtualCWD;
		PlatformWrapper.ChangeToDefaultCWD();
		byte[] array = PlatformWrapper.LoadFile(file_path);
		PlatformWrapper.virtualCWD = text;
		return array;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
	public static void SaveFileDefaultDir(string file_path, byte[] bytes)
	{
		string text = PlatformWrapper.virtualCWD;
		PlatformWrapper.ChangeToDefaultCWD();
		PlatformWrapper.SaveFile(file_path, bytes);
		PlatformWrapper.virtualCWD = text;
	}

	// Token: 0x04000250 RID: 592
	public static string virtualCWD = "";

	// Token: 0x020000F7 RID: 247
	public enum Platform
	{
		// Token: 0x04000494 RID: 1172
		Standalone,
		// Token: 0x04000495 RID: 1173
		WebGL,
		// Token: 0x04000496 RID: 1174
		Mobile,
		// Token: 0x04000497 RID: 1175
		Switch,
		// Token: 0x04000498 RID: 1176
		Unknown
	}
}
