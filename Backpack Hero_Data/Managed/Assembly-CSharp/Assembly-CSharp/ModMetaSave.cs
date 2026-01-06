using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class ModMetaSave
{
	// Token: 0x06000B71 RID: 2929 RVA: 0x000789C8 File Offset: 0x00076BC8
	public static void SaveModData()
	{
		List<ModLoader.ModpackInfo> list = ModLoader.main.modpacks.Where((ModLoader.ModpackInfo m) => m.loaded).ToList<ModLoader.ModpackInfo>();
		List<ModLoader.ModpackInfo> list2 = new List<ModLoader.ModpackInfo>();
		foreach (ModLoader.ModpackInfo modpackInfo in list)
		{
			ModLoader.ModpackInfo modpackInfo2 = modpackInfo.ShallowCopy();
			modpackInfo2.content = new List<object>();
			modpackInfo2.modCount = new Dictionary<ModLoader.ModpackInfo.ModType, int>();
			modpackInfo2.keysAdded = new List<string>();
			modpackInfo2.baseDir = "";
			modpackInfo2.website = "";
			list2.Add(modpackInfo2);
		}
		ES3Settings es3Settings = new ES3Settings(new Enum[] { ES3.EncryptionType.None });
		es3Settings.path = ModMetaSave.filename;
		ES3.Save<bool>("modDebugging", Singleton.Instance.modDebugging, es3Settings);
		ES3.Save<List<ModLoader.ModpackInfo>>("modpacks", list2, es3Settings);
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00078ACC File Offset: 0x00076CCC
	public static List<ModLoader.ModpackInfo> LoadModData()
	{
		ES3Settings es3Settings = new ES3Settings(new Enum[] { ES3.EncryptionType.None });
		Singleton.Instance.modDebugging = false;
		es3Settings.path = ModMetaSave.filename;
		List<ModLoader.ModpackInfo> list;
		try
		{
			if (!ES3.FileExists(es3Settings.path))
			{
				Debug.Log("No ModData.sav found");
				list = new List<ModLoader.ModpackInfo>();
			}
			else
			{
				Singleton.Instance.modDebugging = ES3.Load<bool>("modDebugging", false, es3Settings);
				list = ES3.Load<List<ModLoader.ModpackInfo>>("modpacks", es3Settings);
			}
		}
		catch (Exception ex)
		{
			string text = "ModData.sav seems to be corrupted. Deleting it and starting fresh. ";
			Exception ex2 = ex;
			ModLog.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
			ES3.DeleteFile(es3Settings.path);
			list = new List<ModLoader.ModpackInfo>();
		}
		return list;
	}

	// Token: 0x04000952 RID: 2386
	private static string filename = Application.persistentDataPath + "/ModData.sav";
}
