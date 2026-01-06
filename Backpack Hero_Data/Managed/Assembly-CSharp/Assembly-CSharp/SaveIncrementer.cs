using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class SaveIncrementer : MonoBehaviour
{
	// Token: 0x06000370 RID: 880 RVA: 0x00013FAC File Offset: 0x000121AC
	public static ES3Settings IncrementSaveFile(string filePath, string suffix = ".sav")
	{
		string text = Application.persistentDataPath + "/";
		string filenameForSlot = SaveIncrementer.GetFilenameForSlot(SaveIncrementer.GetSaveFilesForSlot(filePath, suffix, true), filePath, suffix, true);
		ES3Settings es3Settings = new ES3Settings(new Enum[] { ES3.Location.Cache });
		es3Settings.path = text + filenameForSlot;
		Debug.Log("Incrementing save file to " + es3Settings.path);
		return es3Settings;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00014014 File Offset: 0x00012214
	public static ES3Settings GetSettings(string filePath, string suffix = ".sav")
	{
		string text = Application.persistentDataPath + "/";
		ES3Settings es3Settings = new ES3Settings(null, null);
		es3Settings.path = text + filePath + suffix;
		List<string> saveFilesForSlot = SaveIncrementer.GetSaveFilesForSlot(filePath, suffix, true);
		if (saveFilesForSlot.Count > 0)
		{
			foreach (string text2 in saveFilesForSlot)
			{
				if (!SaveIncrementer.blacklist.Contains(Path.GetFileName(text2)))
				{
					es3Settings.path = text2;
					break;
				}
				Debug.LogWarning(text2 + " is in blacklist because previous load failure");
			}
		}
		Debug.Log("Loading save file at " + es3Settings.path);
		return es3Settings;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000140D8 File Offset: 0x000122D8
	public static byte[] GetBytes(ES3Settings settings, bool encrypted)
	{
		byte[] array = ES3.LoadRawBytes(settings);
		if (!encrypted || settings.location != ES3.Location.Cache)
		{
			return array;
		}
		return ES3.EncryptBytes(array, null);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00014104 File Offset: 0x00012304
	public static void FlushToDisk(ES3Settings settings, bool retry = true)
	{
		Debug.Log("Flushing save to disk at : " + settings.path);
		byte[] bytes = SaveIncrementer.GetBytes(settings, true);
		try
		{
			FileStream fileStream = File.Create(settings.path);
			fileStream.Write(bytes);
			fileStream.Flush();
			fileStream.Close();
		}
		catch (Exception ex)
		{
			string text = "Error while writing overworld save (attempt 1): ";
			Exception ex2 = ex;
			Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
			Thread.Sleep(1000);
			try
			{
				FileStream fileStream2 = File.Create(settings.path);
				fileStream2.Write(bytes);
				fileStream2.Flush();
				fileStream2.Close();
			}
			catch (Exception ex3)
			{
				string text2 = "Error while writing overworld save (attempt 2): ";
				Exception ex4 = ex3;
				Debug.LogError(text2 + ((ex4 != null) ? ex4.ToString() : null));
				Thread.Sleep(1000);
				try
				{
					FileStream fileStream3 = File.Create(settings.path);
					fileStream3.Write(bytes);
					fileStream3.Flush();
					fileStream3.Close();
				}
				catch (Exception ex5)
				{
					string text3 = "Error while writing overworld save (attempt 3): ";
					Exception ex6 = ex5;
					Debug.LogError(text3 + ((ex6 != null) ? ex6.ToString() : null));
					throw;
				}
			}
		}
		if (!SaveIncrementer.IsValidSave(settings.path))
		{
			if (retry)
			{
				SaveIncrementer.FlushToDisk(settings, retry = false);
				return;
			}
			Debug.LogError("COULD NOT SAVE " + settings.path);
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00014268 File Offset: 0x00012468
	public static bool IsValidSave(string filePath)
	{
		bool flag;
		try
		{
			ES3Settings es3Settings = new ES3Settings(new Enum[] { ES3.Location.Cache });
			byte[] array = ES3.DecryptBytes(File.ReadAllBytes(filePath), null);
			ES3.LoadRawString(Encoding.UTF8.GetString(array), es3Settings);
			Debug.Log(filePath + " seems valid...");
			flag = true;
		}
		catch (Exception ex)
		{
			string text = "Could not validate save file ";
			string text2 = ": ";
			Exception ex2 = ex;
			Debug.LogError(text + filePath + text2 + ((ex2 != null) ? ex2.ToString() : null));
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000142F8 File Offset: 0x000124F8
	public static void DeleteAllSaveFiles(string prefix, string suffix)
	{
		foreach (string text in SaveIncrementer.GetSaveFilesForSlot(prefix, suffix, true))
		{
			try
			{
				Debug.Log("Deleting save file: " + text);
				File.Delete(text);
			}
			catch
			{
				Debug.LogWarning("Couldn't delete save file: " + text);
			}
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00014380 File Offset: 0x00012580
	public static List<string> GetSaveFilesForSlot(string prefix, string suffix, bool fullPath = false)
	{
		List<string> list = (from f in Directory.GetFiles(Application.persistentDataPath)
			where f.EndsWith(suffix) && (f.Contains(prefix + suffix) || f.Contains(prefix + "_"))
			select f).ToList<string>();
		list = list.OrderBy((string f) => SaveIncrementer.GetCounterFromFilename(f)).Reverse<string>().ToList<string>();
		while (list.Count<string>() > 10)
		{
			int num = list.Count<string>() - 1;
			string text = list[num];
			try
			{
				Debug.Log("Deleting old save file: " + text);
				File.Delete(text);
				list.RemoveAt(num);
			}
			catch
			{
				Debug.LogWarning("Couldn't delete old save file: " + text);
				break;
			}
		}
		if (!fullPath)
		{
			List<string> list2 = new List<string>();
			foreach (string text2 in list)
			{
				list2.Add(Path.GetFileName(text2));
			}
			list = list2;
		}
		return list;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000144A8 File Offset: 0x000126A8
	public static int GetCounterFromFilename(string filename)
	{
		int num;
		try
		{
			string fileName = Path.GetFileName(filename);
			if (!fileName.Contains("_"))
			{
				num = -1;
			}
			else
			{
				num = int.Parse(fileName.Substring(fileName.LastIndexOf("_") + 1).Replace(".sav", ""));
			}
		}
		catch
		{
			Debug.LogWarning("Weird filename found: " + filename);
			num = -1;
		}
		return num;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00014520 File Offset: 0x00012720
	public static string GetFilenameForSlot(List<string> list, string prefix, string suffix, bool open)
	{
		if (list.Count == 0)
		{
			return prefix + "_0" + suffix;
		}
		list.OrderBy((string f) => SaveIncrementer.GetCounterFromFilename(f)).ToList<string>().Reverse();
		if (!open)
		{
			return list[0];
		}
		return prefix + "_" + (SaveIncrementer.GetCounterFromFilename(list[0]) + 1).ToString() + suffix;
	}

	// Token: 0x0400027B RID: 635
	public static List<string> blacklist = new List<string>();
}
