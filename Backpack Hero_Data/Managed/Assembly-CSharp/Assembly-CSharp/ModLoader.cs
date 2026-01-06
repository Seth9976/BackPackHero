using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Steamworks;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class ModLoader : MonoBehaviour
{
	// Token: 0x06000B61 RID: 2913 RVA: 0x000777F0 File Offset: 0x000759F0
	public string AddTextKey(JToken json, string key, string use, bool reload, bool dryRun, ModLoader.ModpackInfo modpack = null, ModdedItem moddedItem = null)
	{
		JTokenType type = json.Type;
		Dictionary<string, string> dictionary2;
		if (type != JTokenType.Object)
		{
			if (type != JTokenType.String)
			{
				throw new ModUtils.SyntaxException(use + " must be a string or object");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["english"] = json.ToString();
			dictionary2 = dictionary;
		}
		else
		{
			dictionary2 = json.ToObject<Dictionary<string, string>>().ToDictionary((KeyValuePair<string, string> k) => k.Key.ToLower(), (KeyValuePair<string, string> k) => k.Value);
		}
		if (!dictionary2.ContainsKey("english"))
		{
			throw new ModUtils.SyntaxException(use + " must contain an english translation");
		}
		if (dryRun)
		{
			return dictionary2["english"];
		}
		if (moddedItem)
		{
			moddedItem.textKeys.Add(key.ToLower().Trim(), ModUtils.ToSerializedDictionary(dictionary2));
		}
		foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
		{
			if (!this.languageTerms.ContainsKey(keyValuePair.Key))
			{
				this.languageTerms.Add(keyValuePair.Key, new Dictionary<string, string>());
			}
			if (this.languageTerms[keyValuePair.Key].ContainsKey(key.ToLower().Trim()))
			{
				if (!reload)
				{
					throw new ModUtils.DuplicateException(key + " already exists for langauge " + keyValuePair.Key);
				}
				this.languageTerms[keyValuePair.Key][key.ToLower().Trim()] = keyValuePair.Value;
			}
			else
			{
				this.languageTerms[keyValuePair.Key].Add(key.ToLower().Trim(), keyValuePair.Value);
			}
		}
		if (!dryRun && modpack != null)
		{
			modpack.keysAdded.Add(key.ToLower().Trim());
		}
		return dictionary2["english"];
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00077A04 File Offset: 0x00075C04
	public void RemoveTextKey(string key)
	{
		foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in this.languageTerms)
		{
			keyValuePair.Value.Remove(key);
		}
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00077A60 File Offset: 0x00075C60
	private ModLoader.ModpackInfo LoadModpackMeta(string baseDir, string directory, bool reload = false, ModLoader.ModpackInfo.Origin origin = ModLoader.ModpackInfo.Origin.Folder, ulong steamFileId = 0UL)
	{
		Dictionary<string, ValueTuple<JToken, string>> dictionary = new Dictionary<string, ValueTuple<JToken, string>>();
		string text = baseDir + directory + ModLoader.FILENAME_META;
		if (!File.Exists(text))
		{
			throw new ModUtils.LoadingException("Modpack has no valid metadata file (" + ModLoader.FILENAME_META + ")" + text);
		}
		string text2;
		try
		{
			text2 = File.ReadAllText(text);
		}
		catch (Exception ex)
		{
			throw new ModUtils.LoadingException(text, ex);
		}
		JObject jobject;
		try
		{
			jobject = JObject.Parse(text2);
		}
		catch (Exception ex2)
		{
			throw new ModUtils.ParseException(text, ex2);
		}
		ModLoader.ModpackInfo meta = new ModLoader.ModpackInfo();
		meta.baseDir = Path.GetFullPath(baseDir + directory);
		if (!ModUtils.IsNullEmpty(jobject["internal_name"]))
		{
			meta.internalName = ModUtils.CastValueOrDefault<string>(jobject["internal_name"], directory);
		}
		else
		{
			meta.internalName = directory;
		}
		if (this.modpacks.Exists((ModLoader.ModpackInfo x) => x.internalName == meta.internalName))
		{
			meta.loadable = false;
			meta.errorCause = "Modpack is incompatible with \"" + LangaugeManager.main.GetTextByKey(this.modpacks.Find((ModLoader.ModpackInfo x) => x.internalName == meta.internalName).displayName) + "\"\n\nError: Duplicate Internal Name";
			meta.internalName = Guid.NewGuid().ToString();
		}
		if (this.modpacks.Exists((ModLoader.ModpackInfo x) => x.internalName == meta.internalName))
		{
			throw new ModUtils.LoadingException(string.Concat(new string[] { "Cannot load modpack at", text, ". Modpack with the same internal name ", meta.internalName, " already exists." }));
		}
		string text3 = meta.baseDir + ModLoader.FILENAME_ICON;
		if (File.Exists(text3))
		{
			try
			{
				meta.icon = ModUtils.LoadNewSprite(text3, 32f);
				meta.icon = ModUtils.LoadNewSprite(text3, (float)meta.icon.texture.width);
			}
			catch (Exception ex3)
			{
				throw new ModUtils.LoadingException("Could not load icon", ex3);
			}
			if ((meta.icon.texture.width != 32 || meta.icon.texture.height != 32) && (meta.icon.texture.width != 64 || meta.icon.texture.height != 64))
			{
				throw new ModUtils.SyntaxException("Icon needs to be 32x32 or 64x64 pixels big");
			}
		}
		else
		{
			meta.icon = this.modpackDefaultIcon;
		}
		if (jobject["name"] != null)
		{
			string text4 = "MP_" + directory + "_name";
			dictionary.Add(text4, new ValueTuple<JToken, string>(jobject["name"], "Modpack name"));
			meta.displayName = text4.ToLower();
		}
		if (jobject["description"] != null)
		{
			string text5 = "MP_" + directory + "_desc";
			dictionary.Add(text5, new ValueTuple<JToken, string>(jobject["description"], "Modpack description"));
			meta.description = text5.ToLower();
		}
		if (jobject["author"] != null)
		{
			string text6 = "MP_" + directory + "_author";
			dictionary.Add(text6, new ValueTuple<JToken, string>(jobject["author"], "Modpack author"));
			meta.author = text6.ToLower();
		}
		if (jobject["website"] != null)
		{
			string text7 = jobject["website"].ToObject<string>();
			Uri uri;
			if (!Uri.TryCreate(text7, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
			{
				throw new ModUtils.ParseException("Modpack Website URL " + text7 + " is invalid");
			}
			meta.website = text7;
		}
		if (jobject["mod_version"] != null)
		{
			meta.modVersion = jobject["mod_version"].ToObject<string>();
		}
		if (jobject["version"] != null)
		{
			meta.modVersion = jobject["version"].ToObject<string>();
		}
		if (meta.modVersion == "")
		{
			meta.modVersion = "1";
		}
		meta.origin = origin;
		if (SteamManager.enabled)
		{
			if (origin == ModLoader.ModpackInfo.Origin.SteamWorkshop)
			{
				meta.workshop = new WorkshopModpack
				{
					fileId = (PublishedFileId_t)steamFileId,
					modpack = meta
				};
			}
			else if (jobject["steam_workshop_id"] != null)
			{
				string text8 = " is actually a local copy of steam workshop item ";
				JToken jtoken = jobject["steam_workshop_id"];
				ModLog.Log(directory + text8 + ((jtoken != null) ? jtoken.ToString() : null));
				meta.origin = ModLoader.ModpackInfo.Origin.SteamWorkshop;
				meta.workshop = new WorkshopModpack
				{
					fileId = (PublishedFileId_t)((ulong)jobject["steam_workshop_id"]),
					modpack = meta,
					local = true
				};
			}
			if (jobject["steam_workshop_tags"] != null && meta.workshop != null)
			{
				meta.workshop.tags = jobject["steam_workshop_tags"].ToObject<List<string>>();
			}
		}
		meta.modCount.Add(ModLoader.ModpackInfo.ModType.Sprite, Directory.GetFiles(meta.baseDir, "*.png", SearchOption.AllDirectories).Count<string>() - (File.Exists(text3) ? 1 : 0));
		if (Directory.Exists(meta.baseDir + ModLoader.DIR_ITEMS))
		{
			meta.modCount.Add(ModLoader.ModpackInfo.ModType.AddItem, Directory.GetFiles(meta.baseDir + ModLoader.DIR_ITEMS, "*.json", SearchOption.AllDirectories).Count<string>());
		}
		foreach (KeyValuePair<string, ValueTuple<JToken, string>> keyValuePair in dictionary)
		{
			this.AddTextKey(keyValuePair.Value.Item1, keyValuePair.Key, keyValuePair.Value.Item2, reload, false, meta, null);
		}
		return meta;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00078108 File Offset: 0x00076308
	private List<ModLoader.ModpackInfo> ScanForModpacks()
	{
		ModLog.Log("Begin Scanning for Modpacks");
		this.modpacks = new List<ModLoader.ModpackInfo>();
		ModLog.Log("Scanning for Modpacks from Mods Folder");
		List<ulong> list = new List<ulong>();
		foreach (string text in Directory.GetDirectories(ModLoader.BASE_DIR))
		{
			try
			{
				string name = new DirectoryInfo(text).Name;
				ModLog.Log("Adding " + name);
				ModLoader.ModpackInfo modpackInfo = this.LoadModpackMeta(ModLoader.BASE_DIR, name, false, ModLoader.ModpackInfo.Origin.Folder, 0UL);
				if (modpackInfo.origin == ModLoader.ModpackInfo.Origin.SteamWorkshop)
				{
					list.Add((ulong)modpackInfo.workshop.fileId);
				}
				this.modpacks.Add(modpackInfo);
			}
			catch (Exception ex)
			{
				ModLog.LogError(ex.ToString());
			}
		}
		ModLog.Log("Scanning for Zipped Modpacks from Mods Folder");
		foreach (string text2 in Directory.GetFiles(ModLoader.BASE_DIR, "*.zip"))
		{
			ModLog.Log("Found zipped modpack " + text2);
		}
		if (SteamManager.Initialized && !SteamManager.Failed)
		{
			ModLog.Log("Scanning for Modpacks from Steam Workshop");
			uint num = SteamUGC.GetNumSubscribedItems();
			PublishedFileId_t[] array2 = new PublishedFileId_t[num];
			num = SteamUGC.GetSubscribedItems(array2, num);
			PublishedFileId_t[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				PublishedFileId_t subbedItem = array3[i];
				if (list.Exists((ulong x) => x == (ulong)subbedItem))
				{
					string text3 = "Skipping ";
					PublishedFileId_t publishedFileId_t = subbedItem;
					ModLog.Log(text3 + publishedFileId_t.ToString() + ". Local copy in Mods folder");
				}
				else
				{
					PublishedFileId_t publishedFileId_t;
					ulong num2;
					string text4;
					uint num3;
					if (SteamUGC.GetItemInstallInfo(subbedItem, out num2, out text4, 1024U, out num3))
					{
						try
						{
							string text5 = "Adding ";
							publishedFileId_t = subbedItem;
							ModLog.Log(text5 + publishedFileId_t.ToString() + " installed at " + text4);
							string name2 = new DirectoryInfo(text4).Name;
							string text6 = Path.GetDirectoryName(text4) + "/";
							ModLoader.ModpackInfo modpackInfo2 = this.LoadModpackMeta(text6, name2, false, ModLoader.ModpackInfo.Origin.SteamWorkshop, (ulong)subbedItem);
							this.modpacks.Add(modpackInfo2);
							goto IL_0256;
						}
						catch (Exception ex2)
						{
							ModLog.LogError(ex2.ToString());
							goto IL_0256;
						}
					}
					publishedFileId_t = subbedItem;
					ModLog.Log(publishedFileId_t.ToString() + " is not installed.");
					SteamUGC.DownloadItem(subbedItem, true);
				}
				IL_0256:;
			}
		}
		else
		{
			ModLog.LogWarning("Steam Client was never initialized or failed to initialize. Skipping Loading Items from Steam.");
		}
		ModLog.Log("End Scanning for Modpacks");
		return this.modpacks;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000783B4 File Offset: 0x000765B4
	public void LoadModpack(ModLoader.ModpackInfo modpack)
	{
		if (modpack.loaded)
		{
			return;
		}
		if (!modpack.loadable)
		{
			return;
		}
		ModLog.Log(modpack.internalName, "", "Loading " + modpack.internalName);
		if (Directory.Exists(modpack.baseDir + ModLoader.DIR_ITEMS))
		{
			ModLog.Log(modpack.internalName, "", "Begin Loading Items");
			foreach (string text in Directory.GetFiles(modpack.baseDir + ModLoader.DIR_ITEMS, "*.json", SearchOption.AllDirectories))
			{
				try
				{
					modpack.content.Add(ModItemLoader.main.LoadItemFromFile(modpack, text));
				}
				catch (Exception ex)
				{
					string internalName = modpack.internalName;
					string text2 = "item";
					string text3 = "Cannot create item ";
					string text4 = text;
					string text5 = " --> ";
					Exception ex2 = ex;
					ModLog.LogError(internalName, text2, text3 + text4 + text5 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			ModLog.Log(modpack.internalName, "", "End Loading Items");
			ModLog.Log(modpack.internalName, "", "Begin Resolving Item Placeholders");
			foreach (Item2 item in ModItemLoader.main.modItems)
			{
				try
				{
					ModItemLoader.main.ResolvePlaceholders(item);
				}
				catch (Exception ex3)
				{
					string internalName2 = modpack.internalName;
					string text6 = "item";
					string text7 = "Cannot resolve item placeholder --> ";
					Exception ex4 = ex3;
					ModLog.LogError(internalName2, text6, text7 + ((ex4 != null) ? ex4.ToString() : null));
				}
			}
			ModLog.Log(modpack.internalName, "", "End Resolving Item Placeholders");
			if (GameManager.main != null)
			{
				ModLog.Log("Reloading Item list");
				GameManager.main.BuildItemLists();
				GameManager.main.SetupValidItems();
			}
		}
		base.StartCoroutine(this.FixRefsLate());
		modpack.loaded = true;
		ModLog.Log(modpack.internalName, "", "Finished Loading " + modpack.internalName);
		ModMetaSave.SaveModData();
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x000785D4 File Offset: 0x000767D4
	public void UnloadModpack(ModLoader.ModpackInfo modpack, bool reloadIntended, bool unloadCompletely = false)
	{
		if (unloadCompletely)
		{
			foreach (string text in modpack.keysAdded)
			{
				this.RemoveTextKey(text);
			}
		}
		if (!modpack.loaded)
		{
			return;
		}
		ModLog.Log(modpack.internalName, "", "Unoading " + modpack.internalName);
		foreach (object obj in modpack.content)
		{
			if (obj.GetType() == typeof(GameObject))
			{
				GameObject gameObject = (GameObject)obj;
				if (gameObject.GetComponent<ModdedItem>() != null)
				{
					ModItemLoader.main.UnloadItem(gameObject);
				}
			}
		}
		modpack.content = new List<object>();
		foreach (string text2 in modpack.keysAdded)
		{
			if ((!(text2 == modpack.displayName) && !(text2 == modpack.description) && !(text2 == modpack.author)) || unloadCompletely)
			{
				this.RemoveTextKey(text2);
			}
		}
		modpack.loaded = false;
		if (!reloadIntended)
		{
			base.StartCoroutine(this.FixRefsLate());
			ModMetaSave.SaveModData();
		}
		if (GameManager.main != null)
		{
			ModLog.Log("Reloading Item list");
			GameManager.main.BuildItemLists();
			GameManager.main.SetupValidItems();
		}
		ModLog.Log(modpack.internalName, "", "Finished Unloading " + modpack.internalName);
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000787B0 File Offset: 0x000769B0
	public void ReloadModpack(ModLoader.ModpackInfo modpack)
	{
		this.UnloadModpack(modpack, true, false);
		this.LoadModpack(modpack);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000787C4 File Offset: 0x000769C4
	private void Awake()
	{
		if (ModLoader.main != null && ModLoader.main != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			ModLoader.main = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		ModLoader.BASE_DIR_TEMP = Application.persistentDataPath + "/TempMods";
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00078820 File Offset: 0x00076A20
	private void OnDestroy()
	{
		this.languageTerms = new Dictionary<string, Dictionary<string, string>>();
		foreach (ModLoader.ModpackInfo modpackInfo in this.modpacks)
		{
			try
			{
				this.UnloadModpack(modpackInfo, true, false);
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00078894 File Offset: 0x00076A94
	private void Start()
	{
		ModLoader.main = this;
		ModLog.Log("ModLoader active");
		if (!Directory.Exists(ModLoader.BASE_DIR))
		{
			Directory.CreateDirectory(ModLoader.BASE_DIR);
		}
		if (!Directory.Exists(ModLoader.BASE_DIR_TEMP))
		{
			Directory.CreateDirectory(ModLoader.BASE_DIR_TEMP);
		}
		base.StartCoroutine(this.LoadMetaFromSave());
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x000788EC File Offset: 0x00076AEC
	private IEnumerator LoadMetaFromSave()
	{
		if (SteamManager.enabled)
		{
			while (!SteamManager.Initialized && !SteamManager.Failed)
			{
				yield return null;
			}
		}
		this.modpacks = this.ScanForModpacks();
		using (List<ModLoader.ModpackInfo>.Enumerator enumerator = ModMetaSave.LoadModData().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ModLoader.ModpackInfo modpack = enumerator.Current;
				Debug.Log(modpack);
				ModLoader.ModpackInfo modpackInfo = this.modpacks.Find((ModLoader.ModpackInfo m) => m.internalName == modpack.internalName);
				if (modpackInfo != null)
				{
					this.LoadModpack(modpackInfo);
				}
			}
		}
		this.dataReady = true;
		base.StartCoroutine(this.FixRefsLate());
		ModMetaSave.SaveModData();
		this.dataReady = true;
		yield return null;
		yield break;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000788FC File Offset: 0x00076AFC
	public void ReloadModpacks()
	{
		foreach (ModLoader.ModpackInfo modpackInfo in this.modpacks)
		{
			this.UnloadModpack(modpackInfo, true, true);
		}
		this.modpacks = new List<ModLoader.ModpackInfo>();
		base.StartCoroutine(this.LoadMetaFromSave());
		base.StartCoroutine(this.FixRefsLate());
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00078978 File Offset: 0x00076B78
	private IEnumerator FixRefsLate()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		ModLog.Log("Begin Generating Card Text");
		ModTextGen.main.SetupCards();
		foreach (Item2 item in ModItemLoader.main.modItems)
		{
			ModTextGen.main.DetectEmptyDescriptions(item);
		}
		ModTextGen.main.DestroyCards();
		ModLog.Log("End Generating Card Text");
		ModItemLoader.main.ResolveCreateEffectDescriptions(false);
		yield break;
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00078980 File Offset: 0x00076B80
	private void Update()
	{
	}

	// Token: 0x04000948 RID: 2376
	public static string BASE_DIR = "Mods/";

	// Token: 0x04000949 RID: 2377
	public static string BASE_DIR_TEMP = null;

	// Token: 0x0400094A RID: 2378
	public static string FILENAME_META = "/modpack.json";

	// Token: 0x0400094B RID: 2379
	public static string FILENAME_ICON = "/icon.png";

	// Token: 0x0400094C RID: 2380
	public static string DIR_ITEMS = "/Items";

	// Token: 0x0400094D RID: 2381
	public static ModLoader main;

	// Token: 0x0400094E RID: 2382
	public Sprite modpackDefaultIcon;

	// Token: 0x0400094F RID: 2383
	public Dictionary<string, Dictionary<string, string>> languageTerms = new Dictionary<string, Dictionary<string, string>>();

	// Token: 0x04000950 RID: 2384
	public List<ModLoader.ModpackInfo> modpacks;

	// Token: 0x04000951 RID: 2385
	public bool dataReady;

	// Token: 0x020003D5 RID: 981
	[Serializable]
	public class ModpackInfo
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x000CD6A1 File Offset: 0x000CB8A1
		public ModLoader.ModpackInfo ShallowCopy()
		{
			return (ModLoader.ModpackInfo)base.MemberwiseClone();
		}

		// Token: 0x040016F6 RID: 5878
		public string displayName = "";

		// Token: 0x040016F7 RID: 5879
		public string internalName = "";

		// Token: 0x040016F8 RID: 5880
		public string description = "";

		// Token: 0x040016F9 RID: 5881
		public string author = "";

		// Token: 0x040016FA RID: 5882
		public Sprite icon;

		// Token: 0x040016FB RID: 5883
		public string modVersion = "1.0";

		// Token: 0x040016FC RID: 5884
		public string website;

		// Token: 0x040016FD RID: 5885
		public string baseDir;

		// Token: 0x040016FE RID: 5886
		public ModLoader.ModpackInfo.Origin origin;

		// Token: 0x040016FF RID: 5887
		public bool loadable = true;

		// Token: 0x04001700 RID: 5888
		public string errorCause = "";

		// Token: 0x04001701 RID: 5889
		[NonSerialized]
		public WorkshopModpack workshop;

		// Token: 0x04001702 RID: 5890
		public bool loaded;

		// Token: 0x04001703 RID: 5891
		public List<object> content = new List<object>();

		// Token: 0x04001704 RID: 5892
		public List<string> keysAdded = new List<string>();

		// Token: 0x04001705 RID: 5893
		[SerializeField]
		public Dictionary<ModLoader.ModpackInfo.ModType, int> modCount = new Dictionary<ModLoader.ModpackInfo.ModType, int>();

		// Token: 0x020004BA RID: 1210
		public enum ModType
		{
			// Token: 0x04001C55 RID: 7253
			Sprite,
			// Token: 0x04001C56 RID: 7254
			Sound,
			// Token: 0x04001C57 RID: 7255
			AddItem,
			// Token: 0x04001C58 RID: 7256
			AddEnemy,
			// Token: 0x04001C59 RID: 7257
			ReplaceAsset
		}

		// Token: 0x020004BB RID: 1211
		public enum Origin
		{
			// Token: 0x04001C5B RID: 7259
			SteamWorkshop,
			// Token: 0x04001C5C RID: 7260
			Zip,
			// Token: 0x04001C5D RID: 7261
			Folder
		}
	}
}
