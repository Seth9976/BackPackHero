using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000C8 RID: 200
	public static class ES3IO
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		public static DateTime GetTimestamp(string filePath)
		{
			if (!ES3IO.FileExists(filePath))
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			}
			return File.GetLastWriteTime(filePath).ToUniversalTime();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001ECDD File Offset: 0x0001CEDD
		public static string GetExtension(string path)
		{
			return Path.GetExtension(path);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001ECE5 File Offset: 0x0001CEE5
		public static void DeleteFile(string filePath)
		{
			if (ES3IO.FileExists(filePath))
			{
				File.Delete(filePath);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001ECF5 File Offset: 0x0001CEF5
		public static bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001ECFD File Offset: 0x0001CEFD
		public static void MoveFile(string sourcePath, string destPath)
		{
			File.Move(sourcePath, destPath);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001ED06 File Offset: 0x0001CF06
		public static void CopyFile(string sourcePath, string destPath)
		{
			File.Copy(sourcePath, destPath);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001ED0F File Offset: 0x0001CF0F
		public static void MoveDirectory(string sourcePath, string destPath)
		{
			Directory.Move(sourcePath, destPath);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001ED18 File Offset: 0x0001CF18
		public static void CreateDirectory(string directoryPath)
		{
			Directory.CreateDirectory(directoryPath);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001ED21 File Offset: 0x0001CF21
		public static bool DirectoryExists(string directoryPath)
		{
			return Directory.Exists(directoryPath);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		public static string GetDirectoryPath(string path, char seperator = '/')
		{
			char c = (ES3IO.UsesForwardSlash(path) ? '/' : '\\');
			int num = path.LastIndexOf(c);
			if (num == path.Length - 1)
			{
				num = path.Substring(0, num).LastIndexOf(c);
			}
			if (num == -1)
			{
				ES3Debug.LogError("Path provided is not a directory path as it contains no slashes.", null, 0);
			}
			return path.Substring(0, num);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001ED82 File Offset: 0x0001CF82
		public static bool UsesForwardSlash(string path)
		{
			return path.Contains("/");
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001ED94 File Offset: 0x0001CF94
		public static string CombinePathAndFilename(string directoryPath, string fileOrDirectoryName)
		{
			if (directoryPath[directoryPath.Length - 1] != '/' && directoryPath[directoryPath.Length - 1] != '\\')
			{
				directoryPath += "/";
			}
			return directoryPath + fileOrDirectoryName;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001EDD0 File Offset: 0x0001CFD0
		public static string[] GetDirectories(string path, bool getFullPaths = true)
		{
			string[] directories = Directory.GetDirectories(path);
			for (int i = 0; i < directories.Length; i++)
			{
				if (!getFullPaths)
				{
					directories[i] = Path.GetFileName(directories[i]);
				}
				directories[i].Replace("\\", "/");
			}
			return directories;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001EE14 File Offset: 0x0001D014
		public static void DeleteDirectory(string directoryPath)
		{
			if (ES3IO.DirectoryExists(directoryPath))
			{
				Directory.Delete(directoryPath, true);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001EE28 File Offset: 0x0001D028
		public static string[] GetFiles(string path, bool getFullPaths = true)
		{
			string[] files = Directory.GetFiles(path);
			if (!getFullPaths)
			{
				for (int i = 0; i < files.Length; i++)
				{
					files[i] = Path.GetFileName(files[i]);
				}
			}
			return files;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001EE59 File Offset: 0x0001D059
		public static byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001EE61 File Offset: 0x0001D061
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001EE6C File Offset: 0x0001D06C
		public static void CommitBackup(ES3Settings settings)
		{
			ES3Debug.Log("Committing backup for " + settings.path + " to storage location " + settings.location.ToString(), null, 0);
			string text = settings.FullPath + ".tmp";
			if (settings.location != ES3.Location.File)
			{
				if (settings.location == ES3.Location.PlayerPrefs)
				{
					PlayerPrefs.SetString(settings.FullPath, PlayerPrefs.GetString(text));
					PlayerPrefs.DeleteKey(text);
					PlayerPrefs.Save();
				}
				return;
			}
			string text2 = settings.FullPath + ".tmp.bak";
			if (ES3IO.FileExists(settings.FullPath))
			{
				ES3IO.DeleteFile(text2);
				ES3IO.MoveFile(settings.FullPath, text2);
				try
				{
					ES3IO.MoveFile(text, settings.FullPath);
				}
				catch (Exception ex)
				{
					try
					{
						ES3IO.DeleteFile(settings.FullPath);
					}
					catch
					{
					}
					ES3IO.MoveFile(text2, settings.FullPath);
					throw ex;
				}
				ES3IO.DeleteFile(text2);
				return;
			}
			ES3IO.MoveFile(text, settings.FullPath);
		}

		// Token: 0x04000107 RID: 263
		internal static readonly string persistentDataPath = Application.persistentDataPath;

		// Token: 0x04000108 RID: 264
		internal const string backupFileSuffix = ".bac";

		// Token: 0x04000109 RID: 265
		internal const string temporaryFileSuffix = ".tmp";

		// Token: 0x020000F6 RID: 246
		public enum ES3FileMode
		{
			// Token: 0x040001C6 RID: 454
			Read,
			// Token: 0x040001C7 RID: 455
			Write,
			// Token: 0x040001C8 RID: 456
			Append
		}
	}
}
